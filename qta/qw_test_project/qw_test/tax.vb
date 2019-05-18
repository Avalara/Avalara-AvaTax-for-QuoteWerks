Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json.Linq

Module tax

	Sub CheckTax(commit As Boolean)
		Dim address As address.address
		Dim testAddress As address.address

		Log("checking tax")

		If (Not CheckQWDoc()) Then
			Log("No document open in QuoteWerks")
			Return
		End If

		'see if there is a destination address
		address = GetAddress()
        If (address.line1 = "" Or address.city = "" Or (address.state = "" And address.zip = "")) Then
            Log("no valid address to lookup tax against")
            Return
        End If


        Dim LITRMatch As Boolean = True
        'is LITR enabled - line item tax rate
        If (SettingsGet("ava_tax_allow_multiple") = "True") Then
			'build hash of line items 
			Dim hash As String = getHash()

			'get hash from previous save
			Dim oldHash As String = QWApp.DocFunctions.GetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_line_hash"))

			If (hash = oldHash) Then
				LITRMatch = True
			Else
				LITRMatch = False
			End If

        End If

		'see if tax has been retrieved for this address
		testAddress = GetTaxAddress()
		If (testAddress.match(address) And LITRMatch And Not commit) Then
            Log("tax has already been retrieved for this document")
            Return
		End If

		'if there is only 1 row and it is the tax row, delete it
		If (QWApp.ItemFunctions.LineItemCount() = 1 And (SettingsGet("ava_tax_allow_multiple", False) = "True" And QWApp.ItemFunctions.LineItemGetValue(0, "CustomText05") = SettingsGet("ava_tax_line_string", False))) Then
            'Log("Why am I in the delete tax line item loop?")
            QWApp.ItemFunctions.LineItemSetSelectedRows(0)
			QWApp.ItemFunctions.LineItemCutSelectedToMLIBuffer()
			QWApp.ItemFunctions.MLIBufferClear()
		End If

		'see if there are any line items in this order
		If (QWApp.ItemFunctions.LineItemCount() = 0) Then
			Log("there are no line items in this order")
			Return
		End If

		If (commit) Then
			Log("getting tax rate and committing")
		Else
            Log("getting tax rate")  'May Not always be committing
        End If

        'Log("get tax rate and update in QW")
        If (GetTax(commit)) Then
			'store address in custom QW field
			SetTaxAddress(address)
		End If
	End Sub

	Private Function getHash() As String
		'build hash of line items 
		Dim sSourceData As String
		Dim tmpSource() As Byte
		Dim tmpHash() As Byte

		sSourceData = QWApp.DocFunctions.GetDocumentHeaderValue("GrandTotal")

		For iRow = 0 To QWApp.ItemFunctions.LineItemCount() - 1
			sSourceData += QWApp.ItemFunctions.LineItemGetValue(iRow, "ManufacturerPartNumber")
            sSourceData += QWApp.ItemFunctions.LineItemGetValue(iRow, "QtyTotal")
            sSourceData += QWApp.ItemFunctions.LineItemGetValue(iRow, "TaxCode")
            sSourceData += QWApp.ItemFunctions.LineItemGetValue(iRow, "LineAttributes")
        Next iRow
		tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData)
		tmpHash = New MD5CryptoServiceProvider().ComputeHash(tmpSource)
		Return ByteArrayToString(tmpHash)
	End Function

	Private Function ByteArrayToString(ByVal arrInput() As Byte) As String
		Dim i As Integer
		Dim sOutput As New StringBuilder(arrInput.Length)
		For i = 0 To arrInput.Length - 1
			sOutput.Append(arrInput(i).ToString("X2"))
		Next
		Return sOutput.ToString()
	End Function

	Public Function GetTaxAddress()
		Dim testAddress As address.address
		testAddress.setString(QWApp.DocFunctions.GetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_tax_address")))
		Return testAddress
	End Function

    Public Sub SetTaxAddress(address As address.address)
        QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_tax_address", False), address.getString(), True)
    End Sub

    Public Function GetTaxRate()
        'get tax rate from QW
        Return QWApp.DocFunctions.GetDocumentHeaderValue("LocalTaxRate")
    End Function

    Public Function GetExemptDescription(exemptCode As String)
        Dim exemptDescription As String
        exemptCode = exemptCode.ToUpper()

        Select Case exemptCode
            Case "A"
                exemptDescription = "(A - Federal Government)"
            Case "B"
                exemptDescription = "(B - State/Local Government)"
            Case "C"
                exemptDescription = "(C - Tribal Government)"
            Case "D"
                exemptDescription = "(D - Foreign Diplomat)"
            Case "E"
                exemptDescription = "(E - Charitable Organization)"
            Case "F"
                exemptDescription = "(F - Religious / Education)"
            Case "G"
                exemptDescription = "(G - Resale)"
            Case "H"
                exemptDescription = "(H - Agricultural Production)"
            Case "I"
                exemptDescription = "(I - Industrial Prod/Mfg.)"
            Case "J"
                exemptDescription = "(J - Direct Pay Permit)"
            Case "K"
                exemptDescription = "(K - Direct Mail)"
            Case "L"
                exemptDescription = "(L - Other)"
            Case "N"
                exemptDescription = "(N - Local Government)"
            Case "P"
                exemptDescription = "(P - Commercial Aquaculture - Canada)"
            Case "Q"
                exemptDescription = "(Q - Commercial Fishery - Canada)"
            Case "R"
                exemptDescription = "(R - Non-resident - Canada)"
            Case Else
                exemptDescription = "(" & exemptCode & ")"
        End Select

        Return exemptDescription
    End Function

    Public Function GetTax(commit As Boolean) As Boolean
        Dim accountNumber = SettingsGet("ava_account")
        Dim licenseKey = SettingsGet("ava_license", False)

        If (accountNumber = "") Or (licenseKey = "") Then ' Performance - keeping these in memory would be faster than getting them every time, will not change.  just check at startup.
            Log("Skipping GetTax.  Avalara Account or License Key is blank.")
            Return False
        End If

        Dim serviceURL = SettingsGet("ava_url", False)
        Dim taxSvc As New Avalara.AvaTax.Adapter.TaxService.TaxSvc()
        Dim getTaxRequest = New Avalara.AvaTax.Adapter.TaxService.GetTaxRequest()
        Dim i
        Dim line As Avalara.AvaTax.Adapter.TaxService.Line
        Dim getTaxResult
        Dim message = ""
        Dim OriginAddress As New Avalara.AvaTax.Adapter.AddressService.Address()
        Dim taxLine As Avalara.AvaTax.Adapter.TaxService.TaxLine
        Dim taxRate As Double = Nothing
        Dim exemption = QWApp.DocFunctions.GetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_exemption_type")) 'anything palced in CustomText09 (or the mapped field) makes the invoice exempt
        Dim taxIncludedFlag As Boolean = False

        'Header Level Parameters
        'Required Header Parameters
        taxSvc.Configuration.Security.Account = accountNumber
        taxSvc.Configuration.Security.License = licenseKey
        taxSvc.Configuration.Url = serviceURL
        taxSvc.Configuration.ViaUrl = serviceURL
        taxSvc.Profile.Client = SettingsGet("ava_client", False)


        ' Optional Header Parameters
        taxSvc.Profile.Name = SettingsGet("ava_profile", False)

        'Document Level Parameters
        'Required Request Parameters
        'Log("getting customer code")
        Dim CustomerCode = ""
        'Log("customer code map = " & SettingsGet("ava_custom_field_mapping_customer_code", False))
        If Len(SettingsGet("ava_custom_field_mapping_customer_code", False)) > 0 Then
            CustomerCode = QWApp.DocFunctions.GetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_customer_code", False))
        End If

        If (CustomerCode = "") Then 'If the customer code is non-existent, fallback to SoldTo and/or ShipTo companies
            CustomerCode = QWApp.DocFunctions.GetDocumentHeaderValue("SoldToCompany")
            If (CustomerCode = "") Then
                CustomerCode = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToCompany")
                If (CustomerCode = "") Then
                    CustomerCode = SettingsGet("ava_customer_code", False)
                End If
            End If
        End If
        'Log("Customer Code = " & CustomerCode)
        getTaxRequest.CustomerCode = CustomerCode
        'Log("finished setting customer code")

        getTaxRequest.DocDate = QWApp.DocFunctions.GetDocumentHeaderValue("DocDate")
        'getTaxRequest.Lines Is also required, And Is presented later in this file.

        'Best Practice Request Parameters
        getTaxRequest.CompanyCode = SettingsGet("ava_company_code", False)
        getTaxRequest.DocCode = QWApp.DocFunctions.GetDocumentHeaderValue("DocNo")

        getTaxRequest.DetailLevel = Avalara.AvaTax.Adapter.TaxService.DetailLevel.Tax
        getTaxRequest.Commit = commit
        Dim c10 = Left(QWApp.DocFunctions.GetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_return_reason", False)), 6)
        If (c10 = "Return") Then
            getTaxRequest.TaxOverride.TaxOverrideType = Avalara.AvaTax.Adapter.TaxService.TaxOverrideType.TaxDate
            getTaxRequest.TaxOverride.TaxDate = QWApp.DocFunctions.GetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_return_date"))
            getTaxRequest.TaxOverride.Reason = QWApp.DocFunctions.GetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_return_reason"))
        End If
        If (commit) Then
            If (c10 = "Return") Then
                getTaxRequest.DocType = Avalara.AvaTax.Adapter.TaxService.DocumentType.ReturnInvoice
            Else
                getTaxRequest.DocType = Avalara.AvaTax.Adapter.TaxService.DocumentType.SalesInvoice
            End If
        Else
            If (c10 = "Return") Then
                getTaxRequest.DocType = Avalara.AvaTax.Adapter.TaxService.DocumentType.ReturnOrder
            Else
                getTaxRequest.DocType = Avalara.AvaTax.Adapter.TaxService.DocumentType.SalesOrder
            End If
        End If

        If (exemption <> "" And exemption <> Nothing) Then
            getTaxRequest.CustomerUsageType = exemption
            Log("AvaTax CustomerUsageType:  " & exemption)
        End If


        'Situational Request Parameters
        'getTaxRequest.BusinessIdentificationNo = "234243"
        'getTaxRequest.CustomerUsageType = "G";

        'getTaxRequest.Discount = 50;
        'getTaxRequest.LocationCode = "01";
        'getTaxRequest.TaxOverride.TaxOverrideType = TaxOverrideType.TaxDate;
        'getTaxRequest.TaxOverride.Reason = "Adjustment for return";
        'getTaxRequest.TaxOverride.TaxDate = DateTime.Parse("2013-07-01");
        'getTaxRequest.TaxOverride.TaxAmount = 0;
        'getTaxRequest.ServiceMode = ServiceMode.Automatic;

        'Optional Request Parameters
        'getTaxRequest.PurchaseOrderNo = "PO123456"
        'getTaxRequest.ReferenceCode = "ref123456";
        'getTaxRequest.PosLaneCode = "09";
        'getTaxRequest.CurrencyCode = "USD";
        'getTaxRequest.ExchangeRate = (Decimal)1.0;
        'getTaxRequest.ExchangeRateEffDate = DateTime.Parse("2013-01-01");
        'getTaxRequest.SalespersonCode = "Bill Sales";

        'Address Data
        getTaxRequest.DestinationAddress = GetAddressAva(GetAddress())
        If (SettingsGet("ava_enable_vat_id", False) = "True") Then
            getTaxRequest.BusinessIdentificationNo = QWApp.DocFunctions.GetDocumentHeaderValue(SettingsGet("ava_vat_id_field"))  ' If it's international, set the VAT ID
        End If

        'get origin address from config
        OriginAddress.Line1 = SettingsGet("ava_line1", False)
        OriginAddress.Line2 = SettingsGet("ava_line2", False)
        OriginAddress.Line3 = SettingsGet("ava_line3", False)

        OriginAddress.City = SettingsGet("ava_city", False)
        OriginAddress.Region = SettingsGet("ava_state", False)
        OriginAddress.Country = SettingsGet("ava_country", False)
        OriginAddress.PostalCode = SettingsGet("ava_zip", False)

        If (OriginAddress.Line1 = "") Or (OriginAddress.City = "") Or (OriginAddress.Region = "") Or (OriginAddress.PostalCode = "") Then ' Performance future improvement - keeping these in memory would be faster than getting them every time, will not change.  just check at startup.
            Log("Skipping GetTax.  One or more required Origin Address fields are blank (Addr Line 1, City, State, Zip).  Update the Origin Address in the Avalara for QuoteWerks Admin")
            Return False
        End If

        getTaxRequest.OriginAddress = OriginAddress

        Dim taxLineNo As New List(Of Integer)() ' Switching taxLineNo to an array as there might be more than one for some reason.  Troubleshoot root cause in the future.
        ' Line Data
        ' Required Parameters

        Dim t = SettingsGet("ava_override", False)
        Dim rows() As String = t.Split("|")
        Dim overrideCode
        Dim groupHeader As Int16 = -1
        ' Iterate over each line item to decide whether to send it and set the appropriate values
        For iRow = 0 To QWApp.ItemFunctions.LineItemCount() - 1
            Dim LineAttribute = QWApp.ItemFunctions.LineItemGetValue(iRow, "LineAttributes") ' temporarily put this here to check 
            'Log("LineAttribute for row " & iRow & " is " & LineAttribute)
            'Dim GroupTag = QWApp.ItemFunctions.LineItemGetValue(iRow, "GroupTag")
            'Log("maybe i can get the type of the GroupTag = " & GroupTag.GetType.ToString)
            'Log("QTYGroupMultiplier is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "QtyGroupMultiplier"))
            ' If it's a product or service, send it.  If it's our tax line item, skip it.
            Dim lineType = QWApp.ItemFunctions.LineItemGetValue(iRow, "LineType")
            'Log("LineType is " & lineType)
            If (lineType = 8) Then
                groupHeader = iRow ' sets the groupHeader which may be one or more rows above
                'Log("just set groupHeader to " & iRow)
            End If
            If (lineType = 1) Then
                'Dim LineAttribute = QWApp.ItemFunctions.LineItemGetValue(iRow, "LineAttributes")
                'Log("LineAttribute = " & LineAttribute)
                If (SettingsGet("ava_tax_allow_multiple", False) = "True" And QWApp.ItemFunctions.LineItemGetValue(iRow, "CustomText05") = SettingsGet("ava_tax_line_string", False)) Then
                    ' skip the tax row
                    taxLineNo.Add(iRow)
                ElseIf (LineAttribute And 16) = 16 Then ' Excludes optional unselected NON-GROUP items
                    Log("skipping optional unselected row: " & iRow)
                Else
                    If ((LineAttribute And 8) = 8) And (groupHeader > -1) Then ' we have a group member
                        'Log("WE have a group memeber and the groupHeader is greater than -1.")
                        'Log("LineAttributes for Group header, which is row " & groupHeader & " are " & (QWApp.ItemFunctions.LineItemGetValue(groupHeader, "LineAttributes")))
                        If (QWApp.ItemFunctions.LineItemGetValue(groupHeader, "LineAttributes") And 16) = 16 Then ' that group member is unselected
                            Log("skipping optional unselected GROUP row: " & iRow)
                            GoTo 20 'skip to the next iRow - is there a better way to do this? without using GoTo?
                        End If
                    End If

                    'Check if there are any Tax Code Mappings that should be applied to this fine row of yours
                    overrideCode = ""

                    'Log("Starting work on row " & iRow)
                    For i = 0 To rows.Count - 1
                        Dim cols = rows(i).Split(", ")
                        If (cols.Count = 3) Then
                            Dim stringToCheck As String = QWApp.ItemFunctions.LineItemGetValue(iRow, cols(0))
                            'ignore case
                            stringToCheck = stringToCheck.ToUpper
                            cols(1) = cols(1).ToUpper
                            'Log("checking " & stringToCheck & " against mapping table, row " & i)
                            If (stringToCheck = cols(1)) Then
                                overrideCode = cols(2)
                                GoTo 10 ' If you have a match, set the tax code and stop looping through the tax code mappings for that line 
                            ElseIf (cols(1)(0) = "*") And (cols(1)(cols(1).Length - 1) = "*") Then
                                If cols(1).Length > 2 Then
                                    'Log("Got ourselves a CONTAINS clause")
                                    Dim searchString As String = cols(1).Substring(1, cols(1).Length - 2)
                                    'Log("searchString is " & searchString)
                                    'Log("stringToCheck is " & stringToCheck)
                                    If stringToCheck.Contains(searchString) Then
                                        overrideCode = cols(2)
                                        GoTo 10 'Precedence rules!  If you have a "contains" wildcard match, set the tax code and move on
                                    End If
                                End If
                            ElseIf (cols(1)(0) = "*") Then
                                'Log("Oh my beloved joy.  A leading wildcard!")
                                cols(1) = cols(1).Substring(1)
                                If stringToCheck.EndsWith(cols(1)) Then
                                    overrideCode = cols(2)
                                    GoTo 10
                                End If
                            ElseIf (cols(1)(cols(1).Length - 1) = "*") Then
                                'Log("Someone has specified a trailing wildcard!")
                                'Log("stringToCheck is : " & stringToCheck)
                                'Log("cols(1).length is : " & cols(1).Length & " and is equal to " & cols(1)) ' for 123* the length is 4 ofcourse!
                                cols(1) = cols(1).Substring(0, cols(1).Length - 1) ' trim the * to prepare to compare
                                'Log("cols(1).length is : " & cols(1).Length & " and is equal to " & cols(1))
                                If stringToCheck.StartsWith(cols(1)) Then
                                    overrideCode = cols(2)
                                    GoTo 10
                                End If
                            End If
                        End If
                    Next


10:
                    line = New Avalara.AvaTax.Adapter.TaxService.Line()
                    ' lstLineItems.Items.Add("Item#" & iRow & ":  " & QWApp.ItemFunctions.LineItemGetValue(iRow, "Description"))
                    line.No = iRow
                    'line.ItemCode = QWApp.ItemFunctions.LineItemGetValue(iRow, "ManufacturerPartNumber")
                    line.ItemCode = QWApp.ItemFunctions.LineItemGetValue(iRow, SettingsGet("ava_item_code", False))
                    ' Detect Grouped items and set the appropriate AvaTax amounts and quantities
                    ' Can we move this deeper so that it only gets executed when it needs to???
                    ' If QWApp.ItemFunctions.LineItemGetValue(iRow, "LineAttributes") = 8 Or QWApp.ItemFunctions.LineItemGetValue(iRow, "LineAttributes") = 10 Then
                    Dim intGrpMultiplier As Integer = QWApp.ItemFunctions.LineItemGetValue(iRow, "QtyGroupMultiplier")
                    Dim snlTotalLineAmount As Single = QWApp.ItemFunctions.LineItemGetValue(iRow, "ExtendedPrice")
                    Dim intQtyTotal As Integer = QWApp.ItemFunctions.LineItemGetValue(iRow, "QtyTotal")

                    snlTotalLineAmount = intGrpMultiplier * snlTotalLineAmount ' Extended price already takes into account QtyTotal, but not the QtyGroupMultiplier!
                    line.Amount = snlTotalLineAmount
                    line.Qty = intQtyTotal * intGrpMultiplier
                    ' Log("It's a group!  line.Qty is " & line.Qty.ToString)

                    ' Log("line.qty is " & line.Qty.ToString)

                    If ((SettingsGet("ava_tax_allow_multiple", False) = "False") And SettingsGet("ava_tax_included", False) = "True") Then
                        line.TaxIncluded = True
                        taxIncludedFlag = True
                    End If

                    line.Description = Left(QWApp.ItemFunctions.LineItemGetValue(iRow, "Description"), 2047)
                    If (overrideCode <> "") Then
                        line.TaxCode = overrideCode
                        Log("override Tax Code = " & overrideCode & " for row " & iRow)
                    ElseIf (QWApp.ItemFunctions.LineItemGetValue(iRow, "TaxCode") = "N") Then
                        line.TaxCode = "NT"
                    End If

                    ' New line goes BOOM!
                    getTaxRequest.Lines.Add(line)
                End If
            End If







            'Discovery for supporting groups and international currency
            'Log("iRow is " & iRow.ToString)
            'Log("LineType is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "LineType"))
            'Log("LineAttributes are " & QWApp.ItemFunctions.LineItemGetValue(iRow, "LineAttributes")) ' Denotes whether it is a group member and beholden to QtyGroupMultiplier
            'Log("ItemType is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "ItemType"))
            'Log("Description is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "Description"))
            'Log("QtyGroupMultiplier is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "QtyGroupMultiplier"))
            'Log("QtyBase is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "QtyBase"))
            'Log("QtyMultiplier1 is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "QtyMultiplier1"))
            'Log("QtyTotal is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "QtyTotal"))
            'Log("Tag is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "Tag"))
            'Log("ID is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "ID"))
            'Log("PriceModifier is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "PriceModifier"))
            'Log("UnitOfPricing is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "UnitOfPricing"))
            'Log("UnitOfPricingFactor is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "UnitOfPricingFactor"))
            'Log("UnitOfMeasure is " & QWApp.ItemFunctions.LineItemGetValue(iRow, "UnitOfMeasure"))

            'line.OriginAddress = address1
            'line.DestinationAddress = address2

            'Best Practice Request Parameters


            ' Situational Request Parameters
            ' line.CustomerUsageType = "L";
            ' line.ExemptionNo = "12345";
            ' line.Discounted = true;
            ' line.TaxIncluded = QWApp.ItemFunctions.LineItemGetValue(iRow, "Description");
            ' line.TaxOverride.TaxOverrideType = TaxOverrideType.TaxDate;
            ' line.TaxOverride.Reason = "Adjustment for return";
            ' line.TaxOverride.TaxDate = DateTime.Parse("2013-07-01");
            ' line.TaxOverride.TaxAmount = 0;

            ' Optional Request Parameters
            'line.Ref1 = "ref123"
            'line.Ref2 = "ref456"
20:
        Next iRow

        Dim salesTaxLineNo = -1

        '  Create the shipping line item if necessary
        Dim shippingAmount = QWApp.DocFunctions.GetDocumentHeaderValue("ShippingAmount")
        If (shippingAmount <> 0) Then
            line = New Avalara.AvaTax.Adapter.TaxService.Line()
            line.No = QWApp.ItemFunctions.LineItemCount()
            salesTaxLineNo = line.No
            line.ItemCode = "Shipping"
            line.Qty = 1
            line.Amount = QWApp.DocFunctions.GetDocumentHeaderValue("ShippingAmount")
            line.Description = "Shipping"
            If (SettingsGet("ava_tax_shipping_override") = "Y") Then
                ' then do not set the taxcode.  This is a hidden shipping tax code override.  
            Else
                line.TaxCode = "FR"  ' Avalara indicated to ALWAYS set the Shipping TaxCode to FR.They will apply freight tax correctly by state
            End If
            If ((SettingsGet("ava_tax_allow_multiple", False) = "False") And SettingsGet("ava_tax_included", False) = "True") Then
                    line.TaxIncluded = True
                End If
                getTaxRequest.Lines.Add(line)
            End If

            If getTaxRequest.Lines.Count = 0 Then
            ' Log("there were no lines in the Ava call.  Adding a placeholder line.")
            line = New Avalara.AvaTax.Adapter.TaxService.Line()
            line.No = QWApp.ItemFunctions.LineItemCount() ' Using one greater than the last row.  Handles a scenario with many optional and unselected lines.  But isn't there a better way?
            getTaxRequest.Lines.Add(line)
        End If

        'log all the things
        If (SettingsGet("log_level") = "Debug") Then
            taxSvc.Configuration.LogTransactions = True
            taxSvc.Configuration.LogLevel = Avalara.AvaTax.Adapter.LogLevel.DEBUG
            taxSvc.Configuration.LogMessages = True
            taxSvc.Configuration.LogSoap = True
            taxSvc.Configuration.LogTransactions = True
            taxSvc.Configuration.LogFilePath = Application.StartupPath()
        End If

        taxSvc.Configuration.RequestTimeout = SettingsGet("ava_tax_timeout", False)
        getTaxRequest.CurrencyCode = SettingsGet("ava_currency", False)
        'get tax from Avalara
        Log("Calling AvaTax")
        Dim starttime = Now()
        getTaxResult = taxSvc.GetTax(getTaxRequest)
        Dim elapsed = Now().Subtract(starttime).TotalSeconds


        Log("GetTax Result: " + getTaxResult.ResultCode.ToString())
        Log("GetTax Time:" & elapsed)
        If (Not getTaxResult.ResultCode.Equals(Avalara.AvaTax.Adapter.SeverityLevel.Success)) Then
            Dim otherErrors As Int16 = 0
            For i = 0 To getTaxResult.Messages.Count - 1
                message &= getTaxResult.Messages(i).Summary & " "
                Log("Avalara Get Tax Error: " & getTaxResult.Messages(i).Summary)
                If (getTaxResult.Messages(i).Summary = "DocStatus is invalid for this operation.") Then
                    MsgBox("This Invoice ID has already been committed or voided in Avalara.  Use the Avalara Admin Console to make additional updates to this invoice.", MessageBoxOptions.ServiceNotification, "AvaTax Response")
                Else
                    otherErrors += 1
                    'MsgBox("Avalara Error: " & getTaxResult.Messages(i).Summary, MessageBoxOptions.ServiceNotification, "Avalara Commit")
                End If
            Next
            If (otherErrors > 0) Then
                MsgBox("Avalara Error: " & message, MessageBoxOptions.ServiceNotification, "AvaTax Response")
            End If
            Return False
        Else
            Log("Avalara Get Tax Success")
            Log("Document Code: " + getTaxResult.DocCode + " Total Tax: " + getTaxResult.TotalTax.ToString())

            If (QWApp.DocFunctions.GetDocumentHeaderValue("locked") <> 0) Then
                Log("Could not update tax rate in QuoteWerks: Document Locked")
            Else
                Dim rateline = Nothing
                Dim foo = False
                If SettingsGet("ava_vat_message", False) = "True" Then
                    For i = 0 To getTaxResult.Messages.Count - 1  ' Logic to handle the EU VAT Reverse Charge messages.  Should only happen once
                        If (getTaxResult.Messages(i).Summary = "Invoice  Messages for the transaction") Then
                            Dim rawMsgDetails As String = getTaxResult.Messages(i).Details
                            Dim invoiceMsgMaster As JObject = JObject.Parse(rawMsgDetails)  ' Ava sends a message list, then assigns each message to a line item
                            For j = 0 To getTaxResult.TaxLines.Count - 1
                                taxLine = getTaxResult.TaxLines(j)
                                Dim lineMsgCode As Integer = CType(invoiceMsgMaster.SelectToken("InvoiceMessageList")(j).SelectToken("MessageCode"), Integer)
                                If lineMsgCode > 0 Then ' MessageCode zero is a totally sweet message indicating "There is no message".
                                    Dim vat_message = invoiceMsgMaster.SelectToken("InvoiceMessageMasterList")(lineMsgCode).SelectToken("Message")
                                    QWApp.ItemFunctions.LineItemSetValue(taxLine.No, "Notes", vat_message)
                                End If
                            Next
                            'Log(invoiceMsgMaster.SelectToken("InvoiceMessageMasterList")(0).SelectToken("Message"))
                        End If
                    Next
                End If

                Dim exemptCertID = ""

                For i = 0 To getTaxResult.TaxLines.Count - 1
                    taxLine = getTaxResult.TaxLines(i)
                    Log("    " + "Line Number: " + taxLine.No + " Line Tax Rate: " + taxLine.Rate.ToString())
                    'Log(taxLine.Taxability)  '  Checking what this looks like before implementing QTA-123
                    If (taxRate <> Nothing And taxRate <> taxLine.Rate And foo = False) Then
                        foo = True
                        If (SettingsGet("ava_multirate_warned", False) <> "True") Then
                            MsgBox("Multiple tax rates have been returned from AvaTax. In order to accurately process multiple rates on a single quote, please review the Avalara for QuoteWerks setup guide and update the Advanced settings. This message will self destruct and not be shown again.", MessageBoxOptions.ServiceNotification, "Multiple Tax Rates Returned")
                            SettingsSet("ava_multirate_warned", "True")
                        End If

                    End If
                    If (taxRate = Nothing Or taxRate < taxLine.Rate) Then
                        taxRate = taxLine.Rate
                        rateline = taxLine
                    End If

                    If (SettingsGet("ava_tax_allow_multiple", False) = "True" And SettingsGet("ava_line_item_tax_add_enable", False) = "True" And taxLine.Tax <> 0) Then
                        QWApp.ItemFunctions.LineItemSetValue(taxLine.No, SettingsGet("ava_line_item_tax_add_field", False), taxLine.Rate.ToString("P"))
                    ElseIf (SettingsGet("ava_tax_allow_multiple", False) = "True" And SettingsGet("ava_line_item_tax_add_enable", False) = "True" And taxLine.Tax <> 0) Then
                        Dim nonTaxableRefRate = 0
                        QWApp.ItemFunctions.LineItemSetValue(taxLine.No, SettingsGet("ava_line_item_tax_add_field", False), nonTaxableRefRate.ToString("P"))
                    End If

                    If taxLine.TaxIncluded Then
                        taxIncludedFlag = True
                    End If

                    If SettingsGet("ava_tax_exempt_marker", False) = "True" And taxLine.ExemptCertId <> 0 Then
                        Log("ExemptCertId = " + taxLine.ExemptCertId.ToString())
                        exemptCertID = taxLine.ExemptCertId.ToString()
                        ' Need to figure out how often CertIDs come down.  Can they be trusted to exist if CertCapture is in use or are they optional?
                    End If

                    If (taxLine.No = salesTaxLineNo) Then  ' Should actually be shippingLineNo instead of salesTaxLineNo
                        'set taxability for shipping amount
                        Dim spm = Split(QWApp.DocFunctions.GetDocumentHeaderValue("ShippingPricingMethod"), vbTab)
                        ' Log("Shipping Pricing Method = " & spm.ToString) - this actually just returns system.string in the log.  doesn't work on the array.  
                        ' Should just log the raw ShippingPricingMethod, If anything at all.
                        If (taxLine.Taxability) Then
                            spm(2) = "Y"
                        Else
                            spm(2) = "N"
                        End If
                        QWApp.DocFunctions.SetDocumentHeaderValue("ShippingPricingMethod", Join(spm, vbTab), False)
                    ElseIf taxLine.No >= QWApp.ItemFunctions.LineItemCount() Then
                        '  Don't set anything, this is the placeholder line item allowing us to pick up a tax rate without any selected lines
                        '  Log("The taxLine.No is >= the number of line items.  Must be the placeholder")
                    Else
                        Dim tc = "N"
                        If (taxLine.Tax <> 0 Or taxLine.Taxability = "True") Then
                            tc = "Y"
                        End If
                        QWApp.ItemFunctions.LineItemSetValue(taxLine.No, "TaxCode", tc)

                    End If
                Next

                'If (getTaxResult.TotalTax = 0 And exemption <> "" And exemption <> Nothing) Then
                '	taxRate = 0
                'End If

                If ((commit And SettingsGet("ava_tax_convert") = "True") Or (SettingsGet("ava_tax_allow_multiple") = "True")) Then
                    Log("Converting tax rate to line item")

                    If taxLineNo.Count > 1 Then
                        taxLineNo.Reverse()

                    End If
                    For Each element In taxLineNo
                        QWApp.ItemFunctions.LineItemSetSelectedRows(element.ToString())
                        QWApp.ItemFunctions.LineItemCutSelectedToMLIBuffer()
                        QWApp.ItemFunctions.MLIBufferClear()
                    Next

                    'If (taxLineNo > -1) Then 'delete the old tax line item
                    'which Is apparently done thusly
                    'QWApp.ItemFunctions.LineItemSetSelectedRows(taxLineNo.ToString())
                    'QWApp.ItemFunctions.LineItemCutSelectedToMLIBuffer()
                    'QWApp.ItemFunctions.MLIBufferClear()
                    'End If

                    t = SettingsGet("ava_custom_line_fields")
                    Dim fields() As String = {"Description", "Vendor", "Manufacturer", "VendorPartNumber", "ManufacturerPartNumber", "Notes", "CustomText01", "CustomText02", "InternalPartNumber"}
                    Dim Description = "", Vendor = "", Manufacturer = "", VendorPartNumber = "", ManufacturerPartNumber = "", Notes = ""
                    Dim CustomText01 = "", CustomText02 = "", InternalPartNumber = ""
                    Dim LineAttribute As Integer = 0

                    If (((SettingsGet("ava_tax_allow_multiple") = "True") Or (SettingsGet("ava_tax_convert") = "True")) And (SettingsGet("ava_print_line") = "False")) Then   ' If it's false then set the DONT PRINT line Attribute.  Default is checked (True).
                        LineAttribute += 4
                    End If
                    Dim vals() As String = t.Split(", ")

                    'Create the new tax rate line item
                    Dim iNewRowNumber = QWApp.ItemFunctions.AddLineItemToDocument(1, "Tax", getTaxResult.TotalTax,
                                                                                      getTaxResult.TotalTax, getTaxResult.TotalTax,
                                                                                      "", Manufacturer, ManufacturerPartNumber,
                                                                                      Vendor, False,
                                                                                      1, LineAttribute)

                    For i = 0 To vals.Length - 1
                        If (i < fields.Length) Then
                            QWApp.ItemFunctions.LineItemSetValue(iNewRowNumber, fields(i), vals(i))
                        End If
                    Next

                    Description = QWApp.ItemFunctions.LineItemGetValue(iNewRowNumber, "Description") & " "   ' Ryan - what is the point of this line???

                    If (exemption <> "" And exemption <> Nothing And getTaxResult.TotalTax = 0) Then
                        Dim exemptDescr As String = GetExemptDescription(exemption)
                        Description &= "Tax Exempt 0.00% : " & exemptDescr
                    Else
                        Description &= "Sales Tax " & taxRate.ToString("P")
                        If (Not rateline Is Nothing And SettingsGet("ava_tax_add_detail") = "True") Then
                            Description &= ":("
                            For td = 0 To rateline.TaxDetails.Count - 1
                                Dim taxDetail = rateline.TaxDetails(td)
                                Dim rate As Double = taxDetail.Rate
                                Description &= taxDetail.JurisName + " " + rate.ToString("P") & ", "
                            Next
                            Description &= ")"
                        End If
                    End If
                    QWApp.ItemFunctions.LineItemSetValue(iNewRowNumber, "CustomText05", SettingsGet("ava_tax_line_string"))
                    QWApp.ItemFunctions.LineItemSetValue(iNewRowNumber, "Description", Description)
                    QWApp.ItemFunctions.LineItemSetValue(iNewRowNumber, "ItemType", "TAX")  ' Set this so that it can be automatically filtered out of the order Purchasing Window
                    QWApp.ItemFunctions.LineItemSetSelectedRows(0)


                    QWApp.DocFunctions.SetDocumentHeaderValue("LocalTaxRate", 0, True)
                    If (SettingsGet("ava_company_code") = "greystonetech") And (shippingAmount <> 0) Then
                        'We need the shipping amount in order to calculate optional selected items in quote valet accurately, including shipping tax
                        Dim shipping_tax_amount As Single
                        Dim shipping_line As String
                        shipping_line = getTaxResult.TaxLines.Count - 1
                        shipping_tax_amount = getTaxResult.TaxLines(shipping_line).tax
                        Log("shipping tax amount is " & shipping_tax_amount)
                        QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_shipping_tax_amount"), shipping_tax_amount, False)
                    End If
                    QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_line_hash"), getHash(), False)
                    QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_total_tax"), getTaxResult.TotalTax, True)


                    If commit Then
                        QWApp.DocFunctions.DocumentSave()
                    End If

                ElseIf taxIncludedFlag Then
                    Log("Tax is already included. Setting QW tax rate to zero")
                    QWApp.DocFunctions.SetDocumentHeaderValue("LocalTaxRate", 0, True)
                ElseIf getTaxResult.TotalTax = 0 And (exemption <> "" And exemption <> Nothing) Then
                    Log("Customer is fully tax exempt.  Setting QW tax rate to zero")
                    QWApp.DocFunctions.SetDocumentHeaderValue("LocalTaxRate", 0, True)
                ElseIf (getTaxResult.TotalAmount = getTaxResult.TotalExemption) And (getTaxResult.TotalExemption <> 0) Then
                    Log("Customer is fully tax exempt.  Setting QW tax rate to zero")  ' This could also happen if no line items were taxable?  The Zach issue.
                    ' Really we should be requiring a value in <ExemptCertID> but some people have it set to nothing (or atleast that's what's in the XML response)
                    QWApp.DocFunctions.SetDocumentHeaderValue("LocalTaxRate", 0, True)
                    If SettingsGet("ava_tax_exempt_marker") = "True" Then
                        Log("AvaTax CertCapture in use.")
                        ' So the EZUP sales people have a way to tell if a customer is tax exempt
                        ' hasTaxables avoids the case of the false positive (if all line items are marked as Tax Code = N)
                        QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_exempt_marker"), "Tax Exempt", False)
                    ElseIf SettingsGet("ava_custom_field_mapping_certcapture_marker") And exemptCertID <> 0 Then
                        Log("AvaTax CertCapture in use.")
                        QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_certcapture_marker"), "Tax Exempt", False)
                    End If
                Else
                    Log("Setting QW tax rate to " + taxRate.ToString())
                    QWApp.DocFunctions.SetDocumentHeaderValue("LocalTaxRate", taxRate, True)
                End If
            End If

            'This will refresh the data that is displayed on the 5 tabs of the quote workbook with the underlying data that we just set.
            QWApp.DocFunctions.RefreshDisplay()

            'foreach(TaxDetail taxDetail In taxLine.TaxDetails)
            '       {
            'Console.WriteLine("        " + "Jurisdiction: " + taxDetail.JurisName + " Tax: " + taxDetail.Tax.ToString());
            '       }
            '  }
            '}
        End If
        Return True
    End Function


    Public Sub GetTaxRate(quoteAddress As address.address)
        Dim iError
        Dim taxurl = ""
        Dim totalRate = 0
        Log("Tax Rate Lookup ")

        '  PREPARE THE AVALARA URL
        If Len(quoteAddress.state) > 0 Then
            taxurl = "&state=" & quoteAddress.state
        End If
        If Len(quoteAddress.city) > 0 Then
            taxurl = taxurl & "&city=" & quoteAddress.city
        End If
        If Len(quoteAddress.zip) > 0 Then
            taxurl = taxurl & "&postal=" & quoteAddress.zip
        End If
        If Len(quoteAddress.line1) > 0 Then
            taxurl = taxurl & "&street=" & quoteAddress.line1 & " " & quoteAddress.line2
        End If
        taxurl = taxurl & "&apikey=" & SettingsGet("ava_apikey")
        'We SKIP the ShipToAddress3, which is ELEMENT 2 because LOB can't take it as an input.  I suppose we could append it to address_line2...

        '  IF THERE IS A VALID ADDRESS PERFORM A TAX RATE LOOKUP WITH AVALARA
        If InStr(taxurl, "&state") > 0 Then

            Dim sUrl = SettingsGet("ava_taxurl")
            Dim objTaxCall = CreateObject("MSXML2.ServerXMLHTTP")
            objTaxCall.open("GET", sUrl & taxurl, False)
            'objTaxCall.setRequestHeader "Content-Type", "application/x-www-form-urlencoded"
            'objTaxCall.setRequestHeader "Content-Length", Len(sRequestBody)
            'Once the REST call is open and has all the headers set, send it with the payload
            objTaxCall.send
            'msgbox "Tax Rate lookup service response: " & VbCrLf & objTaxCall.responseText
            Dim strTaxReturn = objTaxCall.responseText
            Log("Tax Rate Lookup Response: " & vbCrLf & strTaxReturn)

            'msgbox strTaxReturn

            '  THEN WE PARSE THE TAX RATE LOOKUP RESPONSE INTO AN ARRAY
            Dim arrTaxLookup = Split(strTaxReturn, """")  ' Ha!  The escape character in VBScript is double quotes!
            'msgbox "The Tax lookup array has this many elements: " & UBound(arrTaxLookup)

            '  IF WE GET BACK A VALID TAX RATE, UPDATE THE RATE in QW
            If InStr(strTaxReturn, "totalRate") > 1 Then
                Log("The term totalRate was found")
                For i = 0 To UBound(arrTaxLookup)
                    Log(arrTaxLookup(i))
                    If arrTaxLookup(i) = "totalRate" Then
                        '  Trim the tax response to a 3 decimal float
                        totalRate = Mid(arrTaxLookup(i + 1), 3, 5) / 100
                        'msgbox "The tax rate about to be applied Is:  " & totalRate 
                    End If
                Next
                Dim intTaxReturn = MsgBox("The Tax Rate Lookup Service Returned the below aggregate sales tax rate for this rooftop:" & vbCrLf & vbCrLf _
                        & "totalRate = " & totalRate & vbCrLf _
                        & vbCrLf & "Would you like to update QuoteWerks with this tax rate?", vbYesNo, "Tax Rate Lookup")
                If intTaxReturn = vbYes Then
                    iError = QWApp.DocFunctions.SetDocumentHeaderValue("LocalTaxRate", totalRate, False)
                    'This will refresh the data that is displayed on the 5 tabs of the quote workbook with the underlying data that we just set.
                    iError = QWApp.DocFunctions.RefreshDisplay
                Else
                    ' Do nothing - Do not commit the tax rate lookup to QW  
                End If
            End If
        Else
            Log("Tax Rate Lookup canceled due to no address")
        End If

    End Sub

    Public Class InvoiceMessageMasterList
        Public Property MessageCode As Integer
        Public Property Message As String
    End Class

    Public Class InvoiceMessageList
        Public Property TaxLineNo As String
        Public Property MessageCode As Integer
    End Class

    Public Class rawMessage
        Public Property MasterList As InvoiceMessageMasterList()
        Public Property MessageList As InvoiceMessageList()

    End Class
End Module
