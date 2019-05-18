Imports System.Net
Module Module1

	'Public WithEvents QWApp As QuoteWerks.Application
	Public WithEvents QWApp As Object

	'Public frmSave As admin

	Public Sub Main(args As String())
		Log("app started")
		'Load system tray icon
		'CreateSysTrayIcon()

		Log("CommandLineArgs: {0} " & String.Join(", ", args))

        'force admin for debugging
        'ReDim Preserve args(0)
        'args(0) = "admin"

        If (args.Length > 0) Then
            If (args(0) = "admin") Then
                Dim a = New admin
                a.Show()
            ElseIf (args(0) = "exempt") Then

                If (IsQuoteWerksRunning()) Then
                    QWApp = GetObject(, "QuoteWerks.Application")
                    Dim e = New exemption
                    e.Show()
                End If
            ElseIf (args(0) = "return") Then

                If (IsQuoteWerksRunning()) Then
                    QWApp = GetObject(, "QuoteWerks.Application")
                    If (Not CheckQWDoc()) Then
                        Log("No document open in QuoteWerks to generate return for")
                        Return
                    End If
                    ConvertToReturn()
                    Application.Exit()
                    End
                End If
            Else
                Application.Exit()
                End
            End If
        ElseIf IsQuoteWerksRunning() Then 'If QuoteWerks is already running...
            Log("Quotewerks found")
            'Get running instance of QuoteWerks
            Try
                QWApp = GetObject(, "QuoteWerks.Application")


                Dim connectionPointContainer As System.Runtime.InteropServices.ComTypes.IConnectionPointContainer =
                CType(QWApp, System.Runtime.InteropServices.ComTypes.IConnectionPointContainer)

                'The GUID of the connection point to query for.
                ' This Is the same as the GUID of the IApplicationEvents2.
                Dim Guid = New Guid("D0231BA1-4676-4268-9A76-1996F2536EDD")

                ' Get the connection point of the given GUID.
                Dim connectionPoint As System.Runtime.InteropServices.ComTypes.IConnectionPoint
                connectionPointContainer.FindConnectionPoint(Guid, connectionPoint)

                ' Create the actual object to receive the event
                ' notifications.
                Dim sink = New QuoteWerks.qw_SinkHelper()

                ' Connect the sink.
                Dim sinkCookie As Integer
                connectionPoint.Advise(sink, sinkCookie)


            Catch ex As Exception
                fail(ex, Err, "ERROR Could not instantiate Quotewerks object: ")
                Application.Exit()
                End
            End Try


            If (QWApp Is Nothing) Then

                Log("	" + Err.Number + ": " + Err.Description)
            Else
                Log("Quotewerks object instantiated")

                Dim myHttpWebRequest As WebRequest = WebRequest.Create("http://telemetry.connecttax.com/usage-stats.php?companyCode=" + SettingsGet("ava_company_code") + "&accountNumber=" + SettingsGet("ava_account", False))
                myHttpWebRequest.BeginGetResponse(New AsyncCallback(AddressOf RespCallback), New Object())

            End If



            'Define & show sys tray balloon
            'AppIcon.Text = strAppIconText + vbCrLf + "Monitoring Events"
            'AppIcon.BalloonTipIcon = ToolTipIcon.Info
            'AppIcon.BalloonTipText = "Monitoring Events"
            'AppIcon.ShowBalloonTip(3000)

            'RegisterAllEvents(QWApp, "MyEventHandler")
        Else
            Log("Quotewerks not found, exiting")
            'Notify user
            MsgBox(gsQuoteWerksNotRunningMsg, MsgBoxStyle.Exclamation, "QuoteWerks API")
            Application.Exit()
            End

            'Define & show sys tray balloon
            'AppIcon.Text = strAppIconText + vbCrLf + "Connection with QuoteWerks failed"
            'AppIcon.BalloonTipIcon = ToolTipIcon.Error
            'AppIcon.BalloonTipText = "Connection with QuoteWerks failed"
            'AppIcon.ShowBalloonTip(3000)

        End If

        'check for NewtonSoft DLL - if it doesn't exist, GetTax will fail
        Dim objNewtonFSO
        Try
            objNewtonFSO = CreateObject("Scripting.FileSystemObject")
        Catch ex As Exception
            fail(ex, Err, "NewtonSoft ERROR 'Dim objFSO = CreateObject(Scripting.FileSystemObject)' ")
            Application.Exit()
            End
        End Try

        If objNewtonFSO.FileExists(Application.StartupPath() & "\Newtonsoft.Json.dll") Then
            'Log("NewtonSoft exists")
            ' Do Nothing - more efficient and best practice to just check for the negative.  2.2 baby!
        Else
            Log("ERROR: Newtonsoft.Json.dll does not exist.  GetTax will fail without this DLL.")
        End If

        'Continue application thread

        Application.Run()

    End Sub

	Sub RespCallback(asynchronousResult As IAsyncResult)

	End Sub

	Sub fail(ex As Exception, Err As ErrObject, msg As String, Optional logit As Boolean = True)
		msg &= vbCrLf
		msg &= vbTab & Err.Number & vbCrLf
		msg &= vbTab & Err.Description & vbCrLf
		msg &= vbTab & Err.Source & vbCrLf
		msg &= vbTab & Err.HelpFile & vbCrLf
		msg &= vbTab & Err.HelpContext & vbCrLf

		msg &= vbTab & ex.Message & vbCrLf
		msg &= vbTab & ex.ToString()

		Err.Clear()

		MsgBox(msg, MsgBoxStyle.Exclamation, "QuoteWerks API")

		If (logit) Then
			Log(msg)
		End If
	End Sub

	Function GetUserName() As String
		Dim ret As String
		If TypeOf My.User.CurrentPrincipal Is
		  System.Security.Principal.WindowsPrincipal Then
			' The application is using Windows authentication.
			' The name format is DOMAIN\USERNAME.
			Dim parts() As String = Split(My.User.Name, "\")
			Dim username As String = parts(1)
			ret = username
		Else
			' The application is using custom authentication.
			ret = My.User.Name
		End If

		If (ret = "") Then
			ret = "current user"
		End If

		Return ret
	End Function
End Module



