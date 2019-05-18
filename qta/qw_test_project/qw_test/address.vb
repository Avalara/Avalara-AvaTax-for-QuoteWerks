Imports System.IO
Imports System.Net
Imports System.Text
'Imports System.Drawing
Imports Newtonsoft.Json.Linq
Imports System.Runtime.InteropServices



Module address

    '====
    '
    ' Summary: This script validates the address and tax rate against cloud services before saving
    ' Author:  Tom Montgomery
    '
    '====

    '==== Start of header

    ' Comment out below to *not* need to declare variables.  Should be left active when deployed
    'Option Explicit On
    ' Comment out below if debugging to see line failures.  Should be left active when deployed
    'On Error Resume Next

    Public Structure address
        Public line1 As String
        Public line2 As String
        Public line3 As String
        Public city As String
        Public state As String
        Public zip As String
        Public country As String

        Public iserror As Boolean
        Public message As String

        Public Sub init()
            line1 = ""
            line2 = ""
            line3 = ""
            city = ""
            state = ""
            zip = ""
            country = ""
            iserror = False
            message = ""
        End Sub



        Public Function getString()
            Dim s
            s = line1 + "|" + line2 + "|" + line3 + "|" + city + "|" + state + "|" + zip + "|" + country
            Return s
        End Function

        Public Sub setString(s) 'get address from delimited string
            Dim a = Split(s, "|") 'use a bar as delimiter
            Dim n = UBound(a)
            init()


            line1 = a(0)
            If (n > 0) Then
                line2 = a(1)
            End If
            If (n > 1) Then
                line3 = a(2)
            End If
            If (n > 2) Then
                city = a(3)
            End If
            If (n > 3) Then
                state = a(4)
            End If
            If (n > 4) Then
                zip = a(5)
            End If
            If (n > 5) Then
                country = a(6)
            End If
        End Sub

        Public Function match(o As address) As Boolean
            Dim zipa = Split(zip, "-")
            Dim zipb = Split(o.zip, "-")

            If (line1 <> o.line1 Or
                line2 <> o.line2 Or
                line3 <> o.line3 Or
                city <> o.city Or
                state <> o.state Or
                zipa(0) <> zipb(0)) Then
                'Log("Address has changed or not been checked yet.  USPS call needed.")
                Return False
            End If
            'Log("Address matches previous validation.  No USPS call needed.")
            Return True
        End Function
    End Structure

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr

    End Function

    <DllImport("user32.dll")>
    Public Function SetForegroundWindow(ByVal hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean

    End Function

    Public Sub CheckCWExempt()
        'Log("Entered the CheckCWExempt Function")
        Dim company As String = SettingsGet("destination_address")
        Select Case company
            Case "Ship To"
                company = "ShipToCompany"
            Case "Sold To"
                company = "SoldToCompany"
            Case "Bill To"
                company = "BillToCompany"
            Case Else
                company = "ShipToCompany"
        End Select

        Dim exactCompany As String = QWApp.DocFunctions.GetDocumentHeaderValue(company)
        Dim quote As String = Chr(34)
        Dim getCWCompany As WebRequest = WebRequest.Create("https://cw.greystonetech.com/v4_6_release/apis/3.0/company/companies?conditions=name" & " contains " &
            quote & exactCompany & quote)
        'user headers.add ("Authorization", "Basic 1234....") or whatever the syntax is - use a variable
        getCWCompany.Method = "GET"
        getCWCompany.ContentType = "application/json"
        getCWCompany.Headers.Add("Authorization", "Basic Z3RnK1hFSUhkMmxQSDdIU0l4c2o6d21qU3pPMFhTRTFGWGVtTA==")

        'Log("The contentlength of the request to CW is " & getCWCompany.ContentLength)

        'getCWCompany.BeginGetResponse(New AsyncCallback(AddressOf RespCallback), New Object())

        Dim responseCWCompany As WebResponse = getCWCompany.GetResponse()

        'Log("the contentlength of the response From CW is" & responseCWCompany.ContentLength)
        'Log("the contenttype of the response From CW is" & responseCWCompany.ContentType)

        Dim CWcompanystream As Stream = responseCWCompany.GetResponseStream()

        Dim readCWcompanyStream As New StreamReader(CWcompanystream, Encoding.UTF8)

        Dim rawCWMessage = readCWcompanyStream.ReadToEnd()
        Dim fixedCWMessage = "{" & quote & "Companies" & quote & ":" & rawCWMessage & "}"
        Dim CWMessage As JObject = JObject.Parse(fixedCWMessage)
        Dim nameCW = CType(CWMessage.SelectToken("Companies[0].name"), String)
        Dim exactID = CType(CWMessage.SelectToken("Companies[0].identifier"), String)
        ' should probably just use identifier for the compare below
        'Log(":" & nameCW & ":")
        'Log(":" & exactCompany & ":")
        'Log(exactID)

        responseCWCompany.Close()
        readCWcompanyStream.Close()

        If (exactCompany = nameCW) Then
            'Log("Exact Company Match!  Apply exemption.")
            Dim getCWEntityType As WebRequest = WebRequest.Create("https://cw.greystonetech.com/v4_6_release/apis/3.0/system/reports/Company?conditions=Company_ID" & "= " &
                                                quote & exactID & quote & "&columns=Company_ID,Customer_Usage_Type_RecID&pageSize=1000")
            'user headers.add ("Authorization", "Basic 1234....") or whatever the syntax is - use a variable
            ' https://cw.greystonetech.com/v4_6_release/apis/3.0/system/reports/Company?conditions=Customer_Usage_Type_RecID != "null"&columns=Company_ID,Customer_Usage_Type_RecID&pageSize=1000
            getCWEntityType.Method = "GET"
            getCWEntityType.ContentType = "application/json"
            getCWEntityType.Headers.Add("Authorization", "Basic Z3RnK1hFSUhkMmxQSDdIU0l4c2o6d21qU3pPMFhTRTFGWGVtTA==")

            Dim responseCWEntityType As WebResponse = getCWEntityType.GetResponse()
            Dim CWEntityTypestream As Stream = responseCWEntityType.GetResponseStream()
            Dim readCWEntityTypeStream As New StreamReader(CWEntityTypestream, Encoding.UTF8)
            Dim rawCWEntityTypeString = readCWEntityTypeStream.ReadToEnd()
            Dim CWEntityTypeResp As JObject = JObject.Parse(rawCWEntityTypeString)

            Dim CWEntityType = CWEntityTypeResp.SelectToken("row_values[0]")
            'Log(CWEntityType)
            ' substr or otherwise get the '5' out of that token
            ' map numbers to codes - static list for now
            ' Apply that damn code to custom text whatever
        Else
            'Log("Company did Not match, do not get entity type.")
        End If
    End Sub

    Public Sub CheckAddress()
        Dim retAddress As address
        Dim quoteAddress As address
        Dim testAddress As address
        Dim i = 0
        Dim addressChanged = False
        Dim mb = ""
        Dim isChanged = False
        Dim verifier
        Dim resDiag As Integer
        Dim t
        Dim countries

        Log("Checking Address")

        If (Not CheckQWDoc()) Then
            Log("No document open In QuoteWerks")
            Return
        End If
        'Log("moving along")
        If (CheckQWDocInvoice()) Then
            Log("Skipping Address Validation.  Quotewerks Document has already been converted To invoice")
            Return
        End If
        'Log("moving To GetAddress")
        'get address from quotewerks.  Pulls ShipTo, SoldTo or BillTo depending on settings.
        quoteAddress = GetAddress()

        'Log("moving To GetVerifiedAddress")
        'get previously validated address from QW if it exists.  Pulls string from designated custom field.
        testAddress = GetVerifiedAddress()

        'Log("checking If addresses match")
        'check if address has been changed since the last verification.  If addresses are identical, skip validation.
        If (quoteAddress.match(testAddress)) Then
            'Log("Address has already been validated")
            Return
        End If

        'Log("Moving To verify web Call")
        'verify address
        t = SettingsGet("address_countries")
        countries = t.split(", ")
        verifier = "USPS"
        Dim hascan = False
        For Each country In countries
            If (country = "Canada" Or country = "United Kingdom" Or country = "European Union" Or country = "Brazil" Or country = "Australia" Or country = "New Zealand" Or country = "South Africa" Or country = "India") Then
                ' Probably should put all the countries or update the logic and var names now that we support many countries.  good for the moment.
                hascan = True
                ' hascan now means that they have enabled ANY country outside the US
                ' would be faster with a Do While loop!  
            End If
        Next

        If (quoteAddress.country <> "") Then
            If quoteAddress.country = "CANADA" Then
                quoteAddress.country = "CA"
            End If

            If (quoteAddress.country <> "USA" And quoteAddress.country <> "US" And quoteAddress.country <> "UNITED STATES") Then
                If (hascan) Then
                    'check if they have set up Avalara
                    If (SettingsGet("ava_license") <> "") Then
                        'switch verifier to Avalara
                        verifier = "AVA"
                    Else
                        'throw error
                        mb = "Verifying non-US addresses requires an Avalara account"
                        Log(mb)
                        MsgBox(mb, MessageBoxOptions.ServiceNotification, "Address Validation")
                        Return
                    End If
                Else
                    'die quietly
                    Log("error: non-US address without country in address_countries")
                    Return
                End If
            End If
        Else
            quoteAddress.country = "US"
        End If

        If (verifier = "USPS") And (quoteAddress.state <> "" Or quoteAddress.zip <> "") Then
            'Log("about to verify against USPS")
            retAddress = VerifyAddressUSPS(quoteAddress)
            retAddress.country = quoteAddress.country 'usps dosen't return country so preserve existing one

        ElseIf (verifier = "LOB") Then
            'Log("about to verify against LOB")
            retAddress = VerifyAddressLOB(quoteAddress)
        ElseIf (verifier = "AVA") And (quoteAddress.country = "US" Or quoteAddress.country = "CA") Then
            'Log("about to verify against AVA")
            retAddress = VerifyAddressAva(quoteAddress)
        Else
            Return ' If the address is empty or is not US or Canada, then we don't do address validation.  Avalara will return an error.
        End If


        'if there was an error popup a message box
        If (retAddress.iserror) Then
            mb = "The Address Validation Service returned an error:" & vbCrLf & retAddress.message
            MsgBox(mb, MessageBoxOptions.ServiceNotification, "Address Validation")
            Log(mb)
        Else
            'if address changed ask user if they want to update address in quotewerks
            If (Not retAddress.match(quoteAddress)) Then
                Log("Update Address")
                resDiag = 6
                If (SettingsGet("address_confirm") = "True") Then
                    mb = "The Address Validation Service Returned the below address (updated fields marked with a '*'):" & vbCrLf & vbCrLf
                    If (retAddress.line1 <> quoteAddress.line1) Then
                        mb &= " * "
                    Else
                        mb &= "   "
                    End If
                    mb &= retAddress.line1 & vbCrLf

                    If (retAddress.line2 <> "") Then
                        If (retAddress.line2 <> quoteAddress.line2) Then
                            mb &= " * "
                        Else
                            mb &= "   "
                        End If
                        mb &= retAddress.line2 & vbCrLf
                    End If

                    If (retAddress.line3 <> "") Then
                        If (retAddress.line3 <> quoteAddress.line3) Then
                            mb &= " * "
                        Else
                            mb &= "   "
                        End If
                        mb &= retAddress.line3 & vbCrLf
                    End If

                    If (retAddress.city <> quoteAddress.city) Then
                        mb &= " * "
                    Else
                        mb &= "   "
                    End If
                    mb &= retAddress.city & vbCrLf

                    If (retAddress.state <> quoteAddress.state) Then
                        mb &= " * "
                    Else
                        mb &= "   "
                    End If
                    mb &= retAddress.state & vbCrLf

                    If (retAddress.zip <> quoteAddress.zip) Then
                        mb &= " * "
                    Else
                        mb &= "   "
                    End If
                    mb &= retAddress.zip & vbCrLf

                    If (retAddress.country <> quoteAddress.country) Then
                        mb &= " * "
                    Else
                        mb &= "   "
                    End If
                    mb &= retAddress.country & vbCrLf

                    If retAddress.message <> "" Then
                        mb &= vbCrLf & "HEADS UP! " & vbCrLf & retAddress.message & vbCrLf & vbCrLf
                    End If

                    mb &= "Would you like to update QuoteWerks with the corrected address?"
                    'Log("Just before I am " & resDiag)
                    'Log("QWApp window is " & QW)
                    resDiag = MsgBox(mb, vbYesNo + vbQuestion + vbMsgBoxSetForeground, "QuoteWerks Address Validation")
                    'Log("mb.length is " & mb.Length)

                    'resDiag = MessageBox.Show(mb, "Quoteworks Address Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    'resDiag = UserAddressConfirm(mb)
                    'Dim mytestresult = MessageBox.Show()
                    '   For some reason, using this overload of MessageBox.Show results in the DialogResult sometimes being NO, when YES is selected.  Falling back to MsgBox.
                    'Log("resDiag after MsgBox = " & resDiag)
                    'Log("systemfonts.messageboxfonts is " & SystemFonts.MessageBoxFont.ToString)
                End If

                If (resDiag = 6 Or resDiag = 1) Then
                    'update address in quotewerks
                    UpdateAddress(retAddress)
                    SetVerifiedAddress(retAddress)
                    Log("User accepted corrected address")
                    ' if we need to re-implement as regular form, then https://msdn.microsoft.com/en-us/library/system.windows.forms.form.dialogresult(v=vs.110).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-1 
                ElseIf resDiag = 7 Then
                    ' The user must have closed the box or clicked NO.  Do nothing - Do not commit the address validation to QW  
                    Log("User rejected corrected address. DialogResult = " & resDiag)
                    'resDiag = DialogResult.Yes
                    'Log("Just reset resDiag.  It is now = " & resDiag)
                Else
                    Log("Unhandled response from DialogResult = " & resDiag & " . Handling as User rejecting corrected address.")
                    'Log("I give up")
                End If

            Else
                'if address hasn't changed check for a message from verifier 
                If (retAddress.message <> "") Then
                    mb = "The Address Validation Service returned a message:" & vbCrLf & retAddress.message
                    MsgBox(mb, MessageBoxOptions.ServiceNotification, "Address Validation")
                    Log(mb)
                End If
                'store the verified address to avoid calling verify address again
                SetVerifiedAddress(retAddress)
                ' Log("Address unchanged")
            End If
        End If

    End Sub

    Public Function GetAddress() As address
        Dim quoteAddress As address
        quoteAddress.init()
        Dim destAddress = SettingsGet("destination_address")

        Select Case destAddress
            Case "Ship To"
                'Log("In SHIPTO case statement")
                quoteAddress.line1 = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToAddress1")
                quoteAddress.line2 = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToAddress2")
                'quoteAddress.line3 = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToAddress3") ' commenting out line3 everywhere because it is intended to be the "ATTN line" and does not need validation
                quoteAddress.city = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToCity")
                quoteAddress.state = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToState")
                quoteAddress.zip = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToPostalCode")
                quoteAddress.country = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToCountry")
            Case "Sold To"
                'Log("In SOLDTO case statement")
                quoteAddress.line1 = QWApp.DocFunctions.GetDocumentHeaderValue("SoldToAddress1")
                quoteAddress.line2 = QWApp.DocFunctions.GetDocumentHeaderValue("SoldToAddress2")
                'quoteAddress.line3 = QWApp.DocFunctions.GetDocumentHeaderValue("SoldToAddress3")
                quoteAddress.city = QWApp.DocFunctions.GetDocumentHeaderValue("SoldToCity")
                quoteAddress.state = QWApp.DocFunctions.GetDocumentHeaderValue("SoldToState")
                quoteAddress.zip = QWApp.DocFunctions.GetDocumentHeaderValue("SoldToPostalCode")
                quoteAddress.country = QWApp.DocFunctions.GetDocumentHeaderValue("SoldToCountry")
            Case "Bill To"
                'Log("In BILLTO case statement")
                quoteAddress.line1 = QWApp.DocFunctions.GetDocumentHeaderValue("BillToAddress1")
                quoteAddress.line2 = QWApp.DocFunctions.GetDocumentHeaderValue("BillToAddress2")
                'quoteAddress.line3 = QWApp.DocFunctions.GetDocumentHeaderValue("BillToAddress3")
                quoteAddress.city = QWApp.DocFunctions.GetDocumentHeaderValue("BillToCity")
                quoteAddress.state = QWApp.DocFunctions.GetDocumentHeaderValue("BillToState")
                quoteAddress.zip = QWApp.DocFunctions.GetDocumentHeaderValue("BillToPostalCode")
                quoteAddress.country = QWApp.DocFunctions.GetDocumentHeaderValue("BillToCountry")
            Case Else
                'Log("In CASEELSE statement")
                quoteAddress.line1 = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToAddress1")
                quoteAddress.line2 = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToAddress2")
                'quoteAddress.line3 = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToAddress3")
                quoteAddress.city = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToCity")
                quoteAddress.state = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToState")
                quoteAddress.zip = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToPostalCode")
                quoteAddress.country = QWApp.DocFunctions.GetDocumentHeaderValue("ShipToCountry")
        End Select

        If quoteAddress.country IsNot Nothing Then
            quoteAddress.country = quoteAddress.country.ToUpper()
        End If

        Return quoteAddress
    End Function

    Public Function GetVerifiedAddress()
        Dim testAddress As address
        testAddress.setString(QWApp.DocFunctions.GetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_verified_address", False)))
        Return testAddress
    End Function

    Public Sub SetVerifiedAddress(address As address)
        'store returned address in a custom field
        QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_verified_address", False), address.getString(), True)
    End Sub

    Public Function GetAddressAva(quoteAddress As address) As Avalara.AvaTax.Adapter.AddressService.Address
        Dim address = New Avalara.AvaTax.Adapter.AddressService.Address()

        ' Required Request Parameters
        address.Line1 = quoteAddress.line1
        address.City = quoteAddress.city
        address.Region = quoteAddress.state

        ' Optional Request Parameters
        address.Line2 = quoteAddress.line2
        address.Line3 = quoteAddress.line3

        Select Case quoteAddress.country
            Case "UNITED KINGDOM", "UK", "GREAT BRITAIN", "GREAT BRITIAN", "SCOTLAND", "WALES", "NORTHERN IRELAND", "ENGLAND"
                quoteAddress.country = "GB"
            Case "CA", "CANADA"
                quoteAddress.country = "CA"
            Case "AU", "AUSTRALIA"
                quoteAddress.country = "AU"
            Case "NZ", "NEW ZEALAND"
                quoteAddress.country = "NZ"
            Case "BR", "BRAZIL"
                quoteAddress.country = "BR"
            Case "ZA", "SOUTH AFRICA"
                quoteAddress.country = "ZA"
            Case "INDIA"
                quoteAddress.country = "IN"
            Case Else
                ' If it's a European Union or other country, then you must populate the country with the 2 character ISO 3166 code.  we do nothing.
        End Select

        'Log("The country is " & quoteAddress.country)
        address.Country = quoteAddress.country
        address.PostalCode = quoteAddress.zip

        Return address
    End Function

    Public Function VerifyAddressAva(quoteAddress As address) As address
        Dim i
        Dim addressSvc As New Avalara.AvaTax.Adapter.AddressService.AddressSvc()
        Dim ValidateRequest As New Avalara.AvaTax.Adapter.AddressService.ValidateRequest()
        Dim acct = SettingsGet("ava_account")
        Dim lic = SettingsGet("ava_license")
        Dim url = SettingsGet("ava_url")
        Dim ValidateResult
        Dim retAddress As address

        Dim Address = GetAddressAva(quoteAddress)

        'Header Level Parameters
        'Required Header Parameters
        addressSvc.Configuration.Security.Account = acct
        addressSvc.Configuration.Security.License = lic
        addressSvc.Configuration.Url = url
        addressSvc.Configuration.ViaUrl = url
        addressSvc.Profile.Client = "QuoteWerks Integration"

        ValidateRequest.Address = Address
        ValidateRequest.Coordinates = True
        ValidateRequest.Taxability = True
        ValidateRequest.TextCase = Avalara.AvaTax.Adapter.AddressService.TextCase.Upper

        Dim starttime = Now()
        ValidateResult = addressSvc.Validate(ValidateRequest)
        Dim elapsed = Now().Subtract(starttime).TotalSeconds
        Log("Avalara Address Validate Result: Line1:" & ValidateResult.Addresses(0).Line1 & " Line2:" & ValidateResult.Addresses(0).Line2 & " Line3" & ValidateResult.Addresses(0).Line3 & " City:" & ValidateResult.Addresses(0).City & " State:" & ValidateResult.Addresses(0).Region & " Zip:" & ValidateResult.Addresses(0).PostalCode)
        Log("Avalara Address Validate Time:" & elapsed)


        retAddress.init()
        retAddress.line1 = ValidateResult.Addresses(0).Line1
        retAddress.line2 = ValidateResult.Addresses(0).Line2
        retAddress.line3 = ValidateResult.Addresses(0).Line3

        retAddress.city = ValidateResult.Addresses(0).City
        retAddress.state = ValidateResult.Addresses(0).Region
        retAddress.zip = ValidateResult.Addresses(0).PostalCode
        retAddress.country = ValidateResult.Addresses(0).Country

        For i = 0 To ValidateResult.Messages.Count - 1
            retAddress.message &= ValidateResult.Messages(i).Summary & " "
        Next

        If (Not ValidateResult.ResultCode.Equals(Avalara.AvaTax.Adapter.SeverityLevel.Success)) Then
            retAddress.iserror = True
            Log("Avalara Address Validate Error: " & retAddress.message)
        ElseIf (retAddress.message <> "") Then
            Log("Avalara Address Validate Message: " & retAddress.message)
        End If

        Log("Avalara Address Validate Result: " & retAddress.getString())

        Return retAddress
    End Function

    Public Function esc(str As String) As String
        If (str Is "" Or str Is Nothing) Then
            Return str
        End If
        str = System.Security.SecurityElement.Escape(str)
        str = Uri.EscapeDataString(str)
        Return str
    End Function


    Public Function VerifyAddressUSPS(quoteAddress As address) As address
        'Dim wsClient As New System.Net.WebClient()
        Dim req As String
        Dim zip = Split(quoteAddress.zip, "-")
        Dim xmldoc As New System.Xml.XmlDocument()
        Dim address As New System.Xml.XmlDocument()
        Dim USPSusername As String = "473GREYS0190"
        If SettingsGet("USPSOverride") = "True" Then   ' Fallback to grab the USPS username from the settings file if they want to override the default
            USPSusername = SettingsGet("USPS_username")
        End If
        Dim AddressURL = SettingsGet("USPS_address")
        Dim retAddress As address
        Dim add

        Log("verifying address With USPS")

        req = AddressURL & "?API=Verify&XML=<AddressValidateRequest USERID=""" + USPSusername + """>"
        req &= "<Address ID=""0"">"
        req &= "<Address1>" & esc(quoteAddress.line1) & "</Address1>"
        req &= "<Address2>" & esc(quoteAddress.line2) & "</Address2>"
        req &= "<City>" & esc(quoteAddress.city) & "</City>"
        req &= "<State>" & esc(quoteAddress.state) & "</State>"
        req &= "<Zip5>" & esc(zip(0)) & "</Zip5>"
        req &= "<Zip4>"
        If (UBound(zip) > 0) Then
            req &= esc(zip(1))
        End If
        req &= "</Zip4>"
        req &= "</Address></AddressValidateRequest>"

        Log("USPS request: " & req)
        Dim starttime = Now()
        'Try
        xmldoc.Load(req)
        'Catch ex As Exception
        '	die gracefully
        'End Try


        Dim elapsed = Now().Subtract(starttime).TotalSeconds
        Log("USPS response " & xmldoc.InnerXml)
        Log("USPS Time: " & elapsed.ToString())


        retAddress.init()

        add = xmldoc.SelectSingleNode("AddressValidateResponse/Address[@ID='0']/Address1")
        If (add IsNot Nothing) Then
            retAddress.line1 = add.InnerXml
        End If

        add = xmldoc.SelectSingleNode("AddressValidateResponse/Address[@ID='0']/Address2")
        If (add IsNot Nothing) Then
            retAddress.line2 = add.InnerXml
        End If

        add = xmldoc.SelectSingleNode("AddressValidateResponse/Address[@ID='0']/City")
        If (add IsNot Nothing) Then
            retAddress.city = add.InnerXml
        End If

        add = xmldoc.SelectSingleNode("AddressValidateResponse/Address[@ID='0']/State")
        If (add IsNot Nothing) Then
            retAddress.state = add.InnerXml
        End If

        add = xmldoc.SelectSingleNode("AddressValidateResponse/Address[@ID='0']/Zip5")
        If (add IsNot Nothing) Then
            retAddress.zip = add.InnerXml
        End If

        add = xmldoc.SelectSingleNode("AddressValidateResponse/Address[@ID='0']/Zip4")
        If (add IsNot Nothing) Then
            If (add.InnerXml <> "") Then
                retAddress.zip &= "-" & add.InnerXml
            End If
        End If

        add = xmldoc.SelectSingleNode("AddressValidateResponse/Address[@ID='0']/ReturnText")
        If (add IsNot Nothing) Then
            retAddress.message = add.InnerXml & " "
        End If

        add = xmldoc.SelectSingleNode("AddressValidateResponse/Address[@ID='0']/Error/Description")
        If (add IsNot Nothing) Then
            retAddress.message &= add.InnerXml
            retAddress.iserror = True
        End If

        add = xmldoc.SelectSingleNode("Error/Description")
        If (add IsNot Nothing) Then
            retAddress.message &= add.InnerXml
            retAddress.iserror = True
        End If

        If (retAddress.line1 Is "") Then
            retAddress.line1 = retAddress.line2
            retAddress.line2 = ""
        End If

        If (retAddress.line1 <> "" And retAddress.line2 <> "") Then
            Dim t = retAddress.line1
            retAddress.line1 = retAddress.line2
            retAddress.line2 = t
        End If

        Return retAddress
    End Function

    Public Function UserAddressConfirm(correctedAddrMessage As String) As DialogResult
        ' This custom messagebox was created because YES would occasionally be interpreted as NO and alternative options would not respect BrintToFront - using many tactics
        ' It's a last resort and a learning exercise in custom message boxes!
        ' Create a new instance of the form.
        Dim form1 As New Form()
        If correctedAddrMessage.Contains("HEADS") Then
            form1.Size = New Size(500, 375)
        Else
            form1.Size = New Size(500, 275)
        End If

        ' Create two buttons to use as the accept and cancel buttons.
        Dim button1 As New Button()
        Dim button2 As New Button()

        ' Set the text of button1 to "OK".
        button1.Text = "Yes"
        ' Set the position of the button on the form.
        button2.Location = New Point(form1.Size.Width - 100, form1.Size.Height - 75)
        ' Set the text of button2 to "Cancel".
        button2.Text = "No"
        ' Set the position of the button based on the location of button1.
        'button2.Location = New Point(button1.Left, button1.Height + button1.Top + 10)
        ' Make button1's dialog result OK.
        button1.DialogResult = DialogResult.OK
        ' Make button2's dialog result Cancel.
        button2.DialogResult = DialogResult.Cancel
        'button2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right ' Anchors to the bottom right, go figure
        button1.Location = New Point(button2.Location.X - button2.Width - 25, button2.Location.Y)
        'button1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right


        'Log("owner is " & form1.Owner.ToString)
        ' Set the caption bar text of the form.   
        Dim label1 As New Label()
        label1.Text = correctedAddrMessage
        label1.Location = New Point(0, 20)
        form1.Text = "QuoteWerks Address Validation"
        label1.AutoSize = False
        label1.Size = New Size(form1.Size.Width, form1.Size.Height - 115)
        label1.BackColor = Color.White
        'label1.TextAlign = ContentAlignment.TopCenter
        'label1.Text = label1.Text.PadLeft(3)
        'Log("The font style and size are " & label1.Font.ToString & "the size is " & label1.Font.Size)
        'label1.Font = New Font("Microsoft Sans Serif", 10)
        label1.Font = SystemFonts.MessageBoxFont
        label1.Font = New Font("Segoe UI Semibold", 9)

        'label1.Font = New Font(label1.Font, SystemFonts.)
        Log("The font style and size are " & label1.Font.ToString & "the size is " & label1.Font.Size)
        Log("system font is " & label1.Font.SystemFontName)
        Log("label1 font size is " & label1.Font.Size)
        Log("systemfonts.MessageBoxFont is " & SystemFonts.MessageBoxFont.ToString & SystemFonts.MessageBoxFont.FontFamily.ToString)
        Log("font boldness is " & label1.Font.Bold)
        Log("TopLevel is " & form1.TopLevel)
        ' Define the border style of the form to a dialog box.
        form1.FormBorderStyle = FormBorderStyle.FixedDialog
        ' Set the accept button of the form to button1.
        form1.AcceptButton = button1
        ' Set the cancel button of the form to button2.
        form1.CancelButton = button2
        ' Set the start position of the form to the center of the screen.
        form1.StartPosition = FormStartPosition.CenterScreen
        'form1.BackColor = Color.White
        form1.Icon = SystemIcons.Information
        ' yay! i found how to set the icon without adding a .ico!
        ' Add button1 to the form.
        form1.Controls.Add(button1)
        ' Add button2 to the form.
        form1.Controls.Add(button2)
        form1.Controls.Add(label1)
        form1.Focus()
        form1.TopMost = True
        Log("TopMost is " & form1.TopMost)
        Log("button 2 location is " & button2.Location.ToString)
        Log("form size is " & form1.Size.ToString)
        form1.Activate()
        'form1.Name = "AddressUpdateForm"
        'form1.WindowState = FormWindowState.Minimized
        'form1.WindowState = FormWindowState.Normal
        'Dim handle = form1.Handle()
        'Log("Form Handle = " & handle.ToString)
        ' Display the form as a modal dialog box.
        Dim userConfirmAddr As DialogResult
        'form1.BringToFront()
        'Dim name As String = "AddressUpdateForm"
        'Dim ptr As IntPtr = FindWindow(Nothing, name)
        'SetForegroundWindow(ptr)
        userConfirmAddr = form1.ShowDialog()

        'System.Threading.Thread.Sleep(2000)
        form1.Activate()
        'form1.WindowState = FormWindowState.Minimized
        'form1.WindowState = FormWindowState.Normal
        'form1.BringToFront()
        ' Determine if the OK button was clicked on the dialog box.
        If userConfirmAddr = DialogResult.OK Then
            ' Display a message box indicating that the OK button was clicked.
            'MessageBox.Show("The OK button on the form was clicked.")
            ' Optional: Call the Dispose method when you are finished with the dialog box.
            form1.Dispose()
            Return userConfirmAddr
            ' Display a message box indicating that the Cancel button was clicked.
        Else
            'MessageBox.Show("The Cancel button on the form was clicked.")
            ' Optional: Call the Dispose method when you are finished with the dialog box.
            form1.Dispose()
            Return userConfirmAddr
        End If

    End Function

    Public Function VerifyAddressLOB(quoteAddress As address) As address
        Dim retAddress As address
        retAddress.init()
        Log("verifying address With LOB")
        'Build address verification REST URL string

        Dim AddressURL = "address_line1=" & quoteAddress.line1
        If Len(quoteAddress.line2) > 0 Then
            AddressURL = AddressURL & "&address_line2=" & quoteAddress.line2
        End If
        'We SKIP the ShipToAddress3, which is ELEMENT 2 because LOB can't take it as an input.  
        'I suppose we could append it to address_line2...
        If Len(quoteAddress.city) > 0 Then
            AddressURL = AddressURL & "&address_city=" & quoteAddress.city
        End If
        If Len(quoteAddress.state) > 0 Then
            AddressURL = AddressURL & "&address_state=" & quoteAddress.state
        End If
        If Len(quoteAddress.zip) > 0 Then
            AddressURL = AddressURL & "&address_zip=" & quoteAddress.zip
        End If

        'Check the address against LOB address validation service - update if different - with user confirmation
        Dim sUrl = SettingsGet("LOB_address")
        Dim sRequestBody = AddressURL '  should look like this:  "address_line1=3801 e Florida ave #815&address_city=Denver&address_state=CO"
        If Len(sRequestBody) > 0 Then

            '  FIRST WE MAKE THE CALL TO THE ADDRESS VALIDATION SERVICE
            Dim objAddressCall = CreateObject("MSXML2.ServerXMLHTTP")
            objAddressCall.open("POST", sUrl, False) '
            objAddressCall.setRequestHeader("Content-Type", "application/x-www-form-urlencoded")
            'objAddressCall.setRequestHeader "Content-Type", "application/xml"   This does not cause LOB to return the response in XML... too bad.
            objAddressCall.setRequestHeader("Content-Length", Len(sRequestBody))
            objAddressCall.setRequestHeader("Authorization", "Basic dGVzdF85ZGE2NjZlM2VlOWM2MTA3NDQ2MGVjN2U3Y2U3Y2YwZjkxYTo=")
            'Once the REST call is open and has all the headers set, send it with the payload
            objAddressCall.send(sRequestBody)
            'WScript.echo objAddressCall.responseText  'Shows what you got back.  Good for debugging
            'msgbox "Address validation service response: " & VbCrLf & objAddressCall.responseText
            Log("LOB response " & objAddressCall.responseText)
            Dim strAddressReturn = objAddressCall.responseText
            'msgbox strAddressReturn

            '  THEN WE PARSE THE ADDRESS RESPONSE INTO AN ARRAY
            Dim arrAddress = Split(strAddressReturn, """")  ' Ha!  The escape character in VBScript is double quotes!
            'msgbox "The address array has this many elements: " & UBound(arrAddress)
            Dim i

            '  IF THE RESPONSE IS AN ADDRESS, WE MAKE ALL OUR ADDRESS ASSIGNMENTS
            If InStr(strAddressReturn, "address") > 1 Then

                For i = 0 To UBound(arrAddress)
                    If arrAddress(i) = ": " Then
                        ' Do nothing and Skip this item 

                    End If
                    If arrAddress(i) = "address_line1" Then
                        retAddress.line1 = arrAddress(i + 2)
                        Log("Address Line 1 = " & arrAddress(i + 2))
                    End If
                    If arrAddress(i) = "address_line2" Then
                        retAddress.line2 = arrAddress(i + 2)
                        Log("Address Line 2 = " & arrAddress(i + 2))
                    End If
                    If arrAddress(i) = "address_city" Then
                        retAddress.city = arrAddress(i + 2)
                        Log("Address_city = " & arrAddress(i + 2))
                    End If
                    If arrAddress(i) = "address_state" Then
                        retAddress.state = arrAddress(i + 2)
                        Log("Address_state = " & arrAddress(i + 2))
                    End If
                    If arrAddress(i) = "address_zip" Then
                        retAddress.zip = arrAddress(i + 2)
                        Log("Address_zip = " & arrAddress(i + 2))
                    End If
                    If arrAddress(i) = "message" Then
                        retAddress.message = arrAddress(i + 2)
                        Log("Message = " & arrAddress(i + 2))
                    End If
                Next
            End If
        End If

        Return retAddress
    End Function


    Public Sub UpdateAddress(retAddress As address)
        Dim z = ""
        Dim addrToUpdate = SettingsGet("destination_address")
        Dim iError
        If (SettingsGet("address_zip_only") = "False") Then
            z = Split(retAddress.zip, "-")(0)
        Else
            z = retAddress.zip
        End If

        '  WE THEN DISPLAY A MESSAGE TO THE USER, ASKING IF THEY WANT TO SAVE THE CORRECTED ADDRESS

        Select Case addrToUpdate
            Case "Ship To"
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToAddress1", retAddress.line1, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToAddress2", retAddress.line2, False)
                'iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToAddress3", retAddress.line3, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToCity", retAddress.city, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToState", retAddress.state, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToPostalCode", z, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToCountry", retAddress.country, False)
            Case "Sold To"
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("SoldToAddress1", retAddress.line1, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("SoldToAddress2", retAddress.line2, False)
                'iError = QWApp.DocFunctions.SetDocumentHeaderValue("SoldToAddress3", retAddress.line3, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("SoldToCity", retAddress.city, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("SoldToState", retAddress.state, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("SoldToPostalCode", z, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("SoldToCountry", retAddress.country, False)
            Case "Bill To"
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("BillToAddress1", retAddress.line1, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("BillToAddress2", retAddress.line2, False)
                'iError = QWApp.DocFunctions.SetDocumentHeaderValue("BillToAddress3", retAddress.line3, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("BillToCity", retAddress.city, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("BillToState", retAddress.state, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("BillToPostalCode", z, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("BillToCountry", retAddress.country, False)
            Case Else
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToAddress1", retAddress.line1, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToAddress2", retAddress.line2, False)
                'iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToAddress3", retAddress.line3, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToCity", retAddress.city, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToState", retAddress.state, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToPostalCode", z, False)
                iError = QWApp.DocFunctions.SetDocumentHeaderValue("ShipToCountry", retAddress.country, False)
        End Select


        'This will refresh the data that is displayed on the 5 tabs of the quote workbook with the underlying data that we just set.
        iError = QWApp.DocFunctions.RefreshDisplay

    End Sub



End Module
