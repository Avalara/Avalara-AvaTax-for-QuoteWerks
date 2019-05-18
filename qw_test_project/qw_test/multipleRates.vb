Public Class multipleRates
	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If (ava_ignore_multiple_rates.Checked) Then
			SettingsSet("ava_ignore_multiple_rates", "True")
		End If

		Me.Close()
	End Sub
End Class