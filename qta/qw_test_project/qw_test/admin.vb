Public Class admin
    Dim fieldlist() As String = {"CustomText01", "CustomText02", "CustomText03", "CustomText04", "CustomText05", "CustomText06", "CustomText07", "CustomText08", "CustomText09", "CustomText10", "CustomText11", "CustomText12", "CustomText13", "CustomText14", "CustomText15", "CustomText16", "CustomText17", "CustomText18", "CustomText19", "CustomText20", "CustomText21", "CustomText22", "CustomText23", "CustomText24", "CustomNumber01", "CustomNumber02", "SoldToCompany", "ShipToCompany", "BillToCompany"}
    Dim vars() As String = {"verified_address", "tax_address", "exemption_type", "return_reason", "return_date", "total_tax", "line_hash", "customer_code", "certcapture_marker"}
    Dim pre = "ava_custom_field_mapping"
	Dim changed As Boolean = False
	Dim warn As Boolean = False

	Private Sub admin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
		Application.Exit()
		End
	End Sub


	Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
		warn = False
		Application.Exit()
		End
	End Sub

	Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
		Dim t = ""

		Log("updating configuration")

		SettingsSet("address_zip_only", address_zip_only.Checked.ToString(), False)

		SettingsSet("ava_account", ava_account.Text, False)
		SettingsSet("ava_license", ava_license.Text, False)
		SettingsSet("ava_url", ava_url.Text, False)
		SettingsSet("ava_company_code", ava_company_code.Text, False)
		SettingsSet("ava_tax_timeout", ava_tax_timeout.Text, False)
		SettingsSet("log_level", log_level.Text, False)

		SettingsSet("address_validate", address_validate.Checked.ToString(), False)
        SettingsSet("address_confirm", address_confirm.Checked.ToString(), False)
        SettingsSet("destination_address", destination_address.Text, False)

        Dim ta(address_countries.Items.Count) As String
		Dim i = 0
		For Each t In address_countries.Items
			ta(i) = t.ToString()
			i += 1
		Next
		SettingsSet("address_countries", Join(ta, ","), False)

		SettingsSet("ava_tax_enable", ava_tax_enable.Checked.ToString(), False)
        SettingsSet("ava_tax_commit", ava_tax_commit.Checked.ToString(), False)
        SettingsSet("ava_commit_doctype", ava_commit_doctype.Text, False)
        SettingsSet("ava_currency", ava_currency.Text, False)
        SettingsSet("ava_enable_vat_id", ava_enable_vat_id.Checked.ToString(), False)
        SettingsSet("ava_vat_id_field", ava_vat_id_field.Text, False)
        SettingsSet("ava_tax_convert", ava_tax_convert.Checked.ToString(), False)
		SettingsSet("ava_tax_add_detail", ava_tax_add_detail.Checked.ToString(), False)

        SettingsSet("ava_tax_allow_multiple", ava_tax_allow_multiple.Checked.ToString(), False)
        SettingsSet("ava_print_line", ava_print_line.Checked.ToString(), False)

        SettingsSet("ava_line_item_tax_add_enable", ava_line_item_tax_add_enable.Checked.ToString(), False)
        SettingsSet("ava_line_item_tax_add_field", ava_line_item_tax_add_field.Text, False)
        SettingsSet("ava_allow_lookup_after_commit", ava_allow_lookup_after_commit.Checked.ToString(), False) ' Added for EZUP
        SettingsSet("ava_tax_included", ava_tax_included.Checked.ToString(), False)
        SettingsSet("ava_vat_message", ava_vat_message.Checked.ToString(), False)
        ' SettingsSet("ava_tax_exempt_marker", ava_tax_exempt_marker.Checked.ToString(), False)  OLD MID-RELEASE SETTING.  REMOVE EVENTUALLY
        'SettingsSet("ava_custom_field_mapping_exempt_marker", ava_custom_field_mapping_exempt_marker.Text, False)  ' This is old too

        If (changed) Then
            Dim mb = “HEADS UP!  You are about to change the mapping Avalara for QuoteWerks uses to track, store and send data to Avalara.  Be aware that this could impact active quotes and orders.  Are you sure you want to continue?”
            Dim res = MessageBox.Show(mb, "Quoteworks", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If (res = System.Windows.Forms.DialogResult.No) Then
                'change the cell contents back
                For i = 0 To vars.Count - 1
                    Dim val As String = SettingsGet(pre & "_" & vars(i))
                    Dim cell As DataGridViewComboBoxCell = ava_custom_field_mapping.Rows(i).Cells(1)
                    cell.Value = val
                Next
                Return
            End If
        End If

        ' Save tax line item custom fields - Advanced tab
        t = ""
        Dim r As Integer
        For r = 0 To ava_custom_line_fields.Rows.Count - 1
            t &= ava_custom_line_fields.Rows(r).Cells(1).Value & ","
        Next
        SettingsSet("ava_custom_line_fields", t, False)

        ' Save the Field Mapping tab
        For i = 0 To vars.Count - 1
            Dim cf As String = ava_custom_field_mapping.Rows(i).Cells(1).Value
            ' each custom field value (cf) in the vars array gets written to the settings.ini
            SettingsSet(pre & "_" & vars(i), cf, False)
		Next

		Dim ov As String = ""
		For i = 0 To ava_override.RowCount - 1
            If (ava_override.Rows(i).Cells(0).Value <> "") And (ava_override.Rows(i).Cells(1).Value <> "") And (ava_override.Rows(i).Cells(2).Value <> "") Then
                ov &= ava_override.Rows(i).Cells(0).Value & "," & ava_override.Rows(i).Cells(1).Value & "," & ava_override.Rows(i).Cells(2).Value & "|"
            End If
        Next
		SettingsSet("ava_override", ov, False)

		SettingsSet("ava_item_code", ava_item_code.Text, False)

		SettingsSet("ava_line1", ava_line1.Text, False)
		SettingsSet("ava_line2", ava_line2.Text, False)
		SettingsSet("ava_city", ava_city.Text, False)
        SettingsSet("ava_state", ava_state.Text, False)
        SettingsSet("ava_country", ava_country.Text, False)
        SettingsSet("ava_zip", ava_zip.Text, True)

		Application.Exit()
		End
	End Sub

	Private Sub admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Log("Getting configuration")

		If (SettingsGet("address_zip_only", False) = "True") Then
			address_zip_only.Checked = True
		Else
			address_zip_only.Checked = False
		End If

		ava_account.Text = SettingsGet("ava_account")
		ava_license.Text = SettingsGet("ava_license", False)
		ava_url.Text = SettingsGet("ava_url", False)
		ava_company_code.Text = SettingsGet("ava_company_code", False)
		ava_tax_timeout.Text = SettingsGet("ava_tax_timeout", False)
		log_level.Text = SettingsGet("log_level", False)
        destination_address.Text = SettingsGet("destination_address", False)
        ava_commit_doctype.Text = SettingsGet("ava_commit_doctype", False)

        If (SettingsGet("address_validate", False) = "True") Then
			address_validate.Checked = True
		Else
			address_validate.Checked = False
		End If

        If (SettingsGet("ava_line_item_tax_add_enable", False) = "True") Then
			ava_line_item_tax_add_enable.Checked = True
		Else
			ava_line_item_tax_add_enable.Checked = False
		End If
		ava_line_item_tax_add_field.Text = SettingsGet("ava_line_item_tax_add_field", False)

        If (SettingsGet("ava_enable_vat_id", False) = "True") Then
            ava_enable_vat_id.Checked = True
        Else
            ava_enable_vat_id.Checked = False
        End If
        ava_vat_id_field.Text = SettingsGet("ava_vat_id_field", False)

        If (SettingsGet("ava_vat_message", False) = "True") Then
            ava_vat_message.Checked = True
        Else
            ava_vat_message.Checked = False
        End If

        'ava_tax_allow_multiple
        If (SettingsGet("ava_tax_allow_multiple", False) = "True") Then
            ava_tax_allow_multiple.Checked = True
        Else
            ava_tax_allow_multiple.Checked = False
        End If

        ' setting for EZUP to allow their sales folks the ability to update invoices (when managing tax as a line item)
        If (SettingsGet("ava_allow_lookup_after_commit", False) = "True") Then
            ava_allow_lookup_after_commit.Checked = True
        Else
            ava_allow_lookup_after_commit.Checked = False
        End If

        If (SettingsGet("ava_tax_included", False) = "True") Then
            ava_tax_included.Checked = True
        Else
            ava_tax_included.Checked = False
        End If

        If (SettingsGet("ava_print_line", False) = "True") Then
            ava_print_line.Checked = True
        Else
            ava_print_line.Checked = False
        End If

        If (SettingsGet("address_confirm", False) = "True") Then
			address_confirm.Checked = True
		Else
			address_confirm.Checked = False
		End If

		Dim c = SettingsGet("address_countries", False)
		Dim m
		For Each v In c.Split(",")
			m = address_countries_unselected.FindStringExact(v)
			If (Not m = ListBox.NoMatches) Then
				address_countries_unselected.Items.Remove(v)
				address_countries.Items.Add(v)
			End If
		Next


		If (SettingsGet("ava_tax_enable", False) = "True") Then
			ava_tax_enable.Checked = True
		Else
			ava_tax_enable.Checked = False
		End If

		If (SettingsGet("ava_tax_commit", False) = "True") Then
			ava_tax_commit.Checked = True
		Else
			ava_tax_commit.Checked = False
		End If
		ava_currency.Text = SettingsGet("ava_currency")

		If (SettingsGet("ava_tax_convert", False) = "True") Then
			ava_tax_convert.Checked = True
			ava_tax_add_detail.Enabled = True
			ava_custom_line_fields.Enabled = True
		Else
			ava_tax_convert.Checked = False
            'ava_tax_add_detail.Enabled = False
            'ava_custom_line_fields.Enabled = False
        End If

		If (SettingsGet("ava_tax_add_detail", False) = "True") Then
			ava_tax_add_detail.Checked = True
		Else
			ava_tax_add_detail.Checked = False
		End If

		Dim t = SettingsGet("ava_custom_line_fields", False)
		Dim fields() As String = {"Description", "Vendor", "Manufacturer", "VendorPartNumber", "ManufacturerPartNumber", "Notes", "CustomText01", "CustomText02", "InternalPartNumber"}
		Dim vals() As String = t.Split(", ")
		For i = 0 To fields.Count - 1
			If (UBound(vals) >= i) Then
				ava_custom_line_fields.Rows.Add(fields(i), vals(i))
			Else
				ava_custom_line_fields.Rows.Add(fields(i), "")
			End If
		Next

		t = SettingsGet("ava_override", False)
		Dim rows() As String = t.Split("|")
		For i = 0 To rows.Count - 1
			Dim cols = rows(i).Split(",")
			If (cols.Count = 3) Then
				ava_override.Rows.Add(cols(0), cols(1), cols(2))
			End If
		Next

        ' Populate the Field Mapping tab with the corresponding values.  'vars' is an array of settings names 
        ava_custom_field_mapping.Rows.Add(vars.Count)   ' Add the number of rows that exist in vars (above).  The ava_custom_field_mapping object is already defined as 2 columns
        For i = 0 To vars.Count - 1
			Dim val As String = SettingsGet(pre & "_" & vars(i))
			Dim cell As DataGridViewComboBoxCell = ava_custom_field_mapping.Rows(i).Cells(1)
            cell.Value = val  ' The second column (index 1) is the value from the settings file
            ava_custom_field_mapping.Rows(i).Cells(0).Value = vars(i) ' The first column (index 0)is the name of the variable
        Next
		warn = True

        ' OLD MID-RELEASE SETTING FOR THE CERTCAPTURE MARKER.  REMOVE WHEN READY.
        'If (SettingsGet("ava_tax_exempt_marker", False) = "True") Then
        'ava_tax_exempt_marker.Checked = True
        'Else
        'ava_tax_exempt_marker.Checked = False
        'End If

        ' ava_custom_field_mapping_exempt_marker.Text = SettingsGet("ava_custom_field_mapping_exempt_marker", False)  - removed checkbox and drop down.  Put them in Field Mapping DataGridView instead.
        ava_line1.Text = SettingsGet("ava_line1", False)
		ava_line2.Text = SettingsGet("ava_line2", False)
		ava_city.Text = SettingsGet("ava_city", False)
		ava_state.Text = SettingsGet("ava_state", False)
        ava_zip.Text = SettingsGet("ava_zip", False)
        ava_country.Text = SettingsGet("ava_country", False)

        ava_item_code.Text = SettingsGet("ava_item_code", False)

	End Sub

	Private Sub Test_Click(sender As Object, e As EventArgs) Handles Test.Click
		Dim taxSvc As New Avalara.AvaTax.Adapter.TaxService.TaxSvc()

		taxSvc.Configuration.Security.Account = ava_account.Text
		taxSvc.Configuration.Security.License = ava_license.Text
		taxSvc.Configuration.Url = ava_url.Text
		taxSvc.Configuration.ViaUrl = ava_url.Text
		taxSvc.Profile.Client = SettingsGet("ava_client")
		taxSvc.Profile.Name = SettingsGet("ava_profile", False)

		Dim Message = ""
		Dim pingResult
		Dim starttime
		Dim elapsed = 0

		Try
			starttime = Now()
			pingResult = taxSvc.Ping("")
			elapsed = Now().Subtract(starttime).TotalSeconds

		Catch ex As Exception
			Message = ex.Message
		End Try

		If (Not Message = "") Then
			Log("Connection Test Failed:   " & Message)
			TestOutput.Text = Message
		Else
			Log("Avalara Test Connection Success  Time:" & elapsed)
			TestOutput.Text = "Configuration validated successfully"
		End If

	End Sub

	Private Sub remove_country_Click(sender As Object, e As EventArgs) Handles remove_country.Click

		If (Not address_countries.SelectedItem Is Nothing) Then
			address_countries_unselected.Items.Add(address_countries.SelectedItem)
			address_countries.Items.Remove(address_countries.SelectedItem.ToString())
		End If

	End Sub

	Private Sub add_country_Click(sender As Object, e As EventArgs) Handles add_country.Click

		If (Not address_countries_unselected.SelectedItem Is Nothing) Then
			address_countries.Items.Add(address_countries_unselected.SelectedItem)
			address_countries_unselected.Items.Remove(address_countries_unselected.SelectedItem.ToString())
		End If

	End Sub

	Private Sub ava_tax_convert_CheckedChanged(sender As Object, e As EventArgs)
		If (ava_tax_convert.Checked) Then
			ava_tax_add_detail.Enabled = True
			ava_custom_line_fields.Enabled = True
		Else
            ' ava_tax_add_detail.Enabled = False
            ' ava_custom_line_fields.Enabled = False
        End If
	End Sub

	Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
		Try
			Process.Start("https://admin-avatax.avalara.net/login.aspx")
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click
		Try
			Process.Start("mailto:" & Label21.Text)
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click
		Try
			Process.Start(Label22.Text)
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click
		Try
			Process.Start(Label24.Text)
		Catch ex As Exception

		End Try
	End Sub

	Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click
		Try
			Process.Start("mailto:" & Label25.Text)
		Catch ex As Exception

		End Try
	End Sub

	Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
		Try
			Process.Start("http://www.greystonetech.com/")
		Catch ex As Exception

		End Try
	End Sub

	Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
		Try
            Process.Start("https://www.avalara.com/")
        Catch ex As Exception

		End Try
	End Sub

	Private Sub ava_custom_field_mapping_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles ava_custom_field_mapping.CellValueChanged

		If (ava_custom_field_mapping.Rows.Count <> vars.Count) Then
			Return
		End If

		If (changed = False And warn = True) Then


			'check for changes
			For i = 0 To vars.Count - 1
				Dim val As String = SettingsGet(pre & "_" & vars(i))
				Dim cell As DataGridViewComboBoxCell = ava_custom_field_mapping.Rows(i).Cells(1)
				If (cell.Value <> val) Then
					changed = True
				End If
			Next
			If (Not changed) Then
				Return
			End If


		End If

		For i = 0 To vars.Count - 1
			Dim cell As DataGridViewComboBoxCell = ava_custom_field_mapping.Rows(i).Cells(1)
			Dim j As Integer

			For j = 0 To fieldlist.Count - 1
				Dim ct = fieldlist(j)
				Dim k As Integer
				Dim skip As Boolean = False
				For k = 0 To vars.Count - 1
					If (k <> i And ava_custom_field_mapping.Rows(k).Cells(1).Value = ct) Then
						skip = True
					End If
				Next
				If (Not skip) Then
					If (Not cell.Items.Contains(ct)) Then
						cell.Items.Add(ct)
					End If
				Else
					If (cell.Items.Contains(ct)) Then
						cell.Items.Remove(ct)
					End If
				End If
			Next
		Next

	End Sub

	Private Sub ava_tax_allow_multiple_CheckedChanged(sender As Object, e As EventArgs) Handles ava_tax_allow_multiple.CheckedChanged
		If (ava_tax_allow_multiple.Checked) Then
			ava_tax_convert.Checked = False
			ava_tax_convert.Enabled = False
		Else
			'ava_tax_convert.Checked = False
			ava_tax_convert.Enabled = True
		End If
	End Sub

	Private Sub ava_override_add_Click(sender As Object, e As EventArgs)
		ava_override.Rows.Add()
	End Sub

	Private Sub ava_tax_convert_CheckedChanged_1(sender As Object, e As EventArgs) Handles ava_tax_convert.CheckedChanged

	End Sub



    Private Sub ava_currency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ava_currency.SelectedIndexChanged

    End Sub

    Private Sub ava_print_line_CheckedChanged(sender As Object, e As EventArgs) Handles ava_print_line.CheckedChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles ava_allow_lookup_after_commit.CheckedChanged

    End Sub
End Class
