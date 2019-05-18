

Public Class exemption
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If (CheckQWDocInvoice()) Then ' If it's already committed, then don't do any lookups
            'Log("It's an invoice, stop doing things!  could do a msgbox notification if people are left wanting")
            Application.Exit()
            End
        End If

        Dim type = ""
        Dim foo

        If (custom_type.Text <> "") Then
            type = custom_type.Text
        Else
            type = exemption_type.Text
            foo = Split(type, "-")
            type = Trim(foo(0))
        End If

        QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_exemption_type"), type, True)
        QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_tax_address"), "", True)
        'QWApp.DocFunctions.SetDocumentHeaderValue("LocalTaxRate", 0, True)
        CheckTax(False)

        Application.Exit()
        End
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()
        End
    End Sub
End Class