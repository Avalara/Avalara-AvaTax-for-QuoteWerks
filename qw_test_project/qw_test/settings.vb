Module settings

	Dim settingsFile = Nothing
	Dim SettingsObj = Nothing

	Public Sub LoadSettings()
		'Instantiate an FSO and create or open the logfile
		Const ForReading = 1

		Dim objFSO
		Try
			objFSO = CreateObject("Scripting.FileSystemObject")
		Catch ex As Exception
			fail(ex, Err, "Settings ERROR 'Dim objFSO = CreateObject(Scripting.FileSystemObject)' ")
			Application.Exit()
			End
		End Try

		Try
			SettingsObj = New Dictionary(Of String, String)
		Catch ex As Exception
			fail(ex, Err, "Settings ERROR 'SettingsObj = New Dictionary(Of String, String)' ")
			Application.Exit()
			End
		End Try

		Try
			objFSO.FileExists(Application.StartupPath() & "\settings.ini")
		Catch ex As Exception
			fail(ex, Err, "Settings ERROR 'objFSO.FileExists(Application.StartupPath() & \settings.ini)' ")
			Application.Exit()
			End
		End Try

		If objFSO.FileExists(Application.StartupPath() & "\settings.ini") Then
			Try
				settingsFile = objFSO.OpenTextFile(Application.StartupPath() & "\settings.ini", ForReading)
			Catch ex As Exception
				fail(ex, Err, "Settings ERROR 'settingsFile = objFSO.OpenTextFile(Application.StartupPath() & \settings.ini, ForReading)' ")
				Application.Exit()
				End
			End Try
		Else
			Dim mb = "Settings ERROR 'Settings file not found: " & Application.StartupPath() & "\settings.ini"
			MsgBox(mb, MessageBoxOptions.ServiceNotification, "Settings Missing")
			Application.Exit()
			End
			Return
		End If

		Try
			Dim foo = settingsFile.AtEndOfStream
		Catch ex As Exception
			fail(ex, Err, "Settings ERROR 'settingsFile.AtEndOfStream")
			Application.Exit()
			End
		End Try

		If (Not settingsFile.AtEndOfStream) Then
			Dim sf
			Dim pattern
			Dim matches

			Try
				sf = settingsFile.ReadAll()
			Catch ex As Exception
				fail(ex, Err, "Settings ERROR 'sf = settingsFile.ReadAll()'")
				Application.Exit()
				End
			End Try

			Try
				pattern = New System.Text.RegularExpressions.Regex("([^\n=]*)=([^\n]*)")
			Catch ex As Exception
				fail(ex, Err, "Settings ERROR 'pattern = New System.Text.RegularExpressions.Regex(([^\n=]*) = ([^\n]*))'")
				Application.Exit()
				End
			End Try

			Try
				matches = pattern.Matches(sf)
			Catch ex As Exception
				fail(ex, Err, "Settings ERROR 'matches = pattern.Matches(sf)'")
				Application.Exit()
				End
			End Try


			For Each match As System.Text.RegularExpressions.Match In matches

				Try
					SettingsObj.Add(match.Result("$1").Trim(), match.Result("$2").Trim())
				Catch ex As Exception
					fail(ex, Err, "Settings ERROR 'SettingsObj.Add(match.Result($1).Trim(), match.Result($2).Trim())' " & match.ToString())
					Application.Exit()
					End
				End Try

			Next
		End If

		Try
			settingsFile.Close()
		Catch ex As Exception
			fail(ex, Err, "Settings ERROR 'settingsFile.Close()'")
			Application.Exit()
			End
		End Try

	End Sub

	Public Function SettingsGet(ByVal Name As String, Optional ByVal reload As Boolean = True) As String
		Dim ret As String = ""
		If (reload Or SettingsObj Is Nothing) Then
			LoadSettings()
		End If

		SettingsObj.TryGetValue(Name, ret)

		If (ret Is Nothing) Then
			ret = ""
		End If

		Return ret
	End Function

	Public Sub SettingsSet(ByVal Name As String, ByVal Value As String, Optional ByVal commit As Boolean = True)
		If (SettingsObj Is Nothing) Then
			LoadSettings()
		End If

		If (SettingsObj.ContainsKey(Name)) Then
			SettingsObj.Remove(Name)
		End If

		SettingsObj.Add(Name, Value)

		If (commit) Then
			SaveSettings()
		End If
	End Sub

	Public Sub SaveSettings()
		Const ForWriting = 2
		Dim objFSO = CreateObject("Scripting.FileSystemObject")


		If objFSO.FileExists(Application.StartupPath() & "\settings.ini") Then
			settingsFile = objFSO.OpenTextFile(Application.StartupPath() & "\settings.ini", ForWriting)
		Else
			settingsFile = objFSO.CreateTextFile(Application.StartupPath() & "\settings.ini")
		End If

		For Each kvp As KeyValuePair(Of String, String) In SettingsObj
			settingsFile.write(kvp.Key & "=" & kvp.Value & vbCrLf)
		Next

		settingsFile.Close()
		settingsFile = Nothing
		SettingsObj = Nothing
	End Sub

End Module
