Module returns

    Dim CopyFields = "BillToAddress1,BillToAddress2,BillToAddress3,BillToCity,BillToCMAccountNo,BillToCompany,BillToContact,BillToCountry,BillToEmail,BillToFax,BillToFaxExt,BillToPhone,BillToPhoneExt,BillToPostalCode,BillToState,BillToTitle,CustomDate01,CustomDate02,CustomDate03,CustomDate04,CustomDate05,CustomMemo01,CustomMemo02,CustomMemo03,CustomMemo04,CustomMemo05,CustomNumber01,CustomNumber02,CustomNumber03,CustomNumber04,CustomNumber05,CustomText01,CustomText02,CustomText03,CustomText04,CustomText05,CustomText06,CustomText07,CustomText09,CustomText10,CustomText11,CustomText12,CustomText13,CustomText14,CustomText15,CustomText16,CustomText17,CustomText18,CustomText19,CustomText20,CustomText21,CustomText22,CustomText23,CustomText24,ShipToAddress1,ShipToAddress2,ShipToAddress3,ShipToCity,ShipToCMAccountNo,ShipToCompany,ShipToContact,ShipToCountry,ShipToEmail,ShipToFax,ShipToFaxExt,ShipToPhone,ShipToPhoneExt,ShipToPostalCode,ShipToState,ShipToTitle,SoldToAddress1,SoldToAddress2,SoldToAddress3,SoldToCity,SoldToCMAccountNo,SoldToCMLinkRecID,SoldToCMOppRecID,SoldToCompany,SoldToContact,SoldToCountry,SoldToEmail,SoldToFax,SoldToFaxExt,SoldToPhone,SoldToPhoneExt,SoldToPONumber,SoldToPostalCode,SoldToPriceProfile,SoldToState,SoldToTitle"
    Dim FieldNames As String() = CopyFields.Split(New Char() {","c})

	Sub ConvertToReturn()
		Dim FieldValues(FieldNames.Count) As String
		Dim i = 0
		Dim t = ""
		Dim DocNo = QWApp.DocFunctions.GetDocumentHeaderValue("DocNo")
		Dim DocDate = QWApp.DocFunctions.GetDocumentHeaderValue("DocDate")

		Log("Creating return for " & DocNo)

		'copy document headers
		For Each name In FieldNames
			FieldValues(i) = QWApp.DocFunctions.GetDocumentHeaderValue(name)
			i += 1
		Next

		'copy document items
		Dim ava_tax_line_string = SettingsGet("ava_tax_line_string", False)
		For i = 0 To QWApp.ItemFunctions.LineItemCount - 1
			If (QWApp.ItemFunctions.LineItemGetValue(i, "CustomText05") <> ava_tax_line_string) Then 'skip the tax line item
				t &= i & ","
			End If
		Next

		If (t <> "") Then 'copy items from source doc
			QWApp.ItemFunctions.LineItemSetSelectedRows(t)
			QWApp.ItemFunctions.LineItemCopySelectedToMLIBuffer()
		End If

		'get shipping cost and amount
		Dim SA = QWApp.DocFunctions.GetDocumentHeaderValue("ShippingAmount")
		Dim SC = QWApp.DocFunctions.GetDocumentHeaderValue("ShippingCost")

		'get tax rate
		Dim tax = QWApp.DocFunctions.GetDocumentHeaderValue("LocalTaxRate")
		Dim spmv = QWApp.DocFunctions.GetDocumentHeaderValue("ShippingPricingMethod")

		QWApp.DocFunctions.DocumentClose()
		QWApp.DocFunctions.DocumentNew("Quote")

		QWApp.DocFunctions.SetDocumentHeaderValue("ShippingPricingMethod", spmv, False)
		Dim spm = Split(QWApp.DocFunctions.GetDocumentHeaderValue("ShippingPricingMethod"), vbTab)
		If (spm(0) = "M") Then
			QWApp.DocFunctions.SetDocumentHeaderValue("ShippingAmount", -SA, False)
			QWApp.DocFunctions.SetDocumentHeaderValue("ShippingCost", -SC, False)
		End If

		QWApp.DocFunctions.SetDocumentHeaderValue("LocalTaxRate", tax, False)


		'set new document headers
		i = 0
		For Each name In FieldNames
			QWApp.DocFunctions.SetDocumentHeaderValue(name, FieldValues(i), False)
			i += 1
		Next

		QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_return_date"), DocDate, False)
		QWApp.ItemFunctions.MLIBufferPaste() 'paste items from source document into new doc
		Dim p As Double 'negate prices for all items
		For i = 0 To QWApp.ItemFunctions.LineItemCount - 1
			p = QWApp.ItemFunctions.LineItemGetValue(i, "UnitPrice")
			QWApp.ItemFunctions.LineItemSetValue(i, "UnitPrice", -p)
		Next

        QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_tax_address"), "", False) ' Delete the Avatax Validated to force a lookup on next save
        QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_return_reason"), "Return generated based on " & DocNo, True)
	End Sub

End Module
