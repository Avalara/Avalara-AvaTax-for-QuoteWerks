Module logging
	Dim objFile
	Dim logFail = "Unable to write log file. Check file permissions for " & GetUserName() & ". "

	Public Sub LoadLog()
		'Instantiate an FSO and create or open the logfile
		Const ForAppending = 8
		Dim objFSO
		Dim logFile

		Try
			objFSO = CreateObject("Scripting.FileSystemObject")
		Catch ex As Exception
			fail(ex, Err, logFail & "1", False)
			Application.Exit()
			End
		End Try

		Try
			logFile = SettingsGet("log_file", False)
		Catch ex As Exception
			fail(ex, Err, logFail & "2", False)
			Application.Exit()
			End
		End Try

		If objFSO.FileExists(Application.StartupPath() & "\" & logFile) Then
			Try
				objFile = objFSO.OpenTextFile(Application.StartupPath() & "\" & logFile, ForAppending)
			Catch ex As Exception
				fail(ex, Err, logFail & "3", False)
				Application.Exit()
				End
			End Try
		Else
			Try
				objFile = objFSO.CreateTextFile(Application.StartupPath() & "\" & logFile)
			Catch ex As Exception
				fail(ex, Err, logFail & "4", False)
				Application.Exit()
				End
			End Try

		End If
	End Sub

	Public Sub Log(ByVal Msg As String)
		If (objFile Is Nothing) Then
			LoadLog()
		End If

		Try
			objFile.Write(Now & " " & Msg & vbCrLf)
		Catch ex As Exception
			fail(ex, Err, logFail & "5", False)
			Application.Exit()
			End
		End Try

		Try
			CloseLog()
		Catch ex As Exception
			fail(ex, Err, logFail & "6", False)
			Application.Exit()
			End
		End Try

		'end of obsessive eror reporting now that we have a log file
	End Sub

	Public Sub CloseLog()
		objFile.Close()
		objFile = Nothing
	End Sub
End Module
