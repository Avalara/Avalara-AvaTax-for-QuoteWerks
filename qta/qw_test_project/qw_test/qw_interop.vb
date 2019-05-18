Imports System
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace QuoteWerks
	<Guid("D0231BA1-4676-4268-9A76-1996F2536EDD"), InterfaceType(CType(2, ComInterfaceType)), TypeLibType(CType(4240, TypeLibTypeFlags))>
	<ComImport()>
	Public Interface __Application
		<DispId(1)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforeSaveDocument(<[In]()> iSaveAction As Short, <[In]()> <Out()> ByRef bCancel As Boolean)

		<DispId(2)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub AfterSaveDocument(<[In]()> iResult As Short)

		<DispId(3)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforePrint(<[In]()> <Out()> ByRef bCancel As Boolean)

		<DispId(4)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforePrintDocument(<[In]()> iPrintMethod As Short, <[In]()> iLayoutType As Short, <MarshalAs(UnmanagedType.BStr)> <[In]()> sLayoutFileName As String, <[In]()> <Out()> ByRef bCancel As Boolean)

		<DispId(5)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforePreviewAction(<[In]()> iAction As Short, <MarshalAs(UnmanagedType.BStr)> <[In]()> sReference As String, <[In]()> iSource As Short, <[In]()> iLayoutType As Short, <MarshalAs(UnmanagedType.BStr)> <[In]()> sLayoutFileName As String, <[In]()> <Out()> ByRef bCancel As Boolean)

		<DispId(6)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub AfterOpenDocument()

		<DispId(7)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforeConvertDocument(<[In]()> <Out()> ByRef iDestinationType As Short, <[In]()> <Out()> ByRef bCancel As Boolean)

		<DispId(8)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub AfterConvertDocument(<[In]()> <Out()> ByRef iResult As Short)

		<DispId(9)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub AfterNewDocument(<[In]()> iSource As Short)

		<DispId(10)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforeRenameDocument(<MarshalAs(UnmanagedType.BStr)> <[In]()> sNewDocName As String, <[In]()> <Out()> ByRef bCancel As Boolean)

		<DispId(11)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub AfterRenameDocument(<[In]()> <Out()> ByRef iResult As Short)

		<DispId(12)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforeDeleteDocument(<[In]()> iSource As Short, <[In]()> lDocID As Integer, <[In]()> <Out()> ByRef bCancel As Boolean)

		<DispId(13)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub AfterDeleteDocument(<[In]()> lDocID As Integer, <[In]()> iResult As Short)

		<DispId(14)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub AfterAppExit()

		<DispId(15)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforeContactSelection(<[In]()> iType As Short, <[In]()> <Out()> ByRef iSource As Short)

		<DispId(16)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub AfterContactSelection(<[In]()> iType As Short)

		<DispId(17)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforeShippingSelection(<[In]()> <Out()> ByRef bCancel As Boolean)

		<DispId(18)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub AfterShippingSelection(<[In]()> iResult As Short)

		<DispId(19)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub BeforeEmailRecipientSelection(<[In]()> iRecipientType As Short, <MarshalAs(UnmanagedType.BStr)> <[In]()> <Out()> ByRef sTOList As String, <MarshalAs(UnmanagedType.BStr)> <[In]()> <Out()> ByRef sFrom As String, <MarshalAs(UnmanagedType.BStr)> <[In]()> <Out()> ByRef sCCList As String, <MarshalAs(UnmanagedType.BStr)> <[In]()> <Out()> ByRef sBCCList As String, <MarshalAs(UnmanagedType.BStr)> <[In]()> <Out()> ByRef sSubject As String, <[In]()> <Out()> ByRef bSuppressQWAction As Boolean)

		<DispId(20)>
		<MethodImpl(MethodImplOptions.PreserveSig Or MethodImplOptions.InternalCall)>
		Sub XMLEvent(<MarshalAs(UnmanagedType.BStr)> <[In]()> <Out()> ByRef sXMLEventData As String)
	End Interface


	<ClassInterface(ClassInterfaceType.None)>
	Public Class qw_SinkHelper
		Implements __Application

		Public Sub AfterAppExit() Implements __Application.AfterAppExit
			Application.Exit()
			End
		End Sub

        Public Sub AfterContactSelection(<[In]> iType As Short) Implements __Application.AfterContactSelection
            'At least in the near term, if it's not greystone, skip the connectwise integration
            If (SettingsGet("ava_company_code") <> "greystonetech") Then
                Return
            End If
            Log("QWApp_AfterContactSelection")

            Dim destType = 0
            Dim destAddr = SettingsGet("destination_address", False)

            Select Case destAddr
                Case "Sold To"
                    ' Do nothing.  destType is already 0
                Case "Ship To"
                    destType = 1
                Case "Bill To"
                    destType = 2
                Case Else
                    destType = 1 'Behave like the Dest Address picker in the Admin.  Shipping is the default in the case of bad settings data.
            End Select

            'If (iType = destType) And (SettingsGet("ava_company_code", False) = "greystonetech") Then
            'We may not care what the iType is.  No matter which address is populated, retrieve the exemption field if it's not already populated AND I have a CW integration
            If (iType = destType) Then
                CheckCWExempt()
            End If
        End Sub

        Public Sub AfterConvertDocument(<[In]> <Out> ByRef iResult As Short) Implements __Application.AfterConvertDocument
			Log("QWApp_AfterConvertDocument")
			If (iResult <> 0) Then 'only commit after a successful conversion
				Return
			End If

            'make sure tax checking and commiting are enabled in admin
            If (SettingsGet("ava_tax_enable", False) = "True" And SettingsGet("ava_tax_commit", False) = "True") Then
                ' first make sure the conversion is to the docType we want to commit on
                Dim docType As String = QWApp.DocFunctions.GetDocumentHeaderValue("DocType")
                Dim commitType As String = SettingsGet("ava_commit_doctype")
                If (commitType <> "INVOICE" And commitType <> "ORDER") Then
                    commitType = "INVOICE" ' handle a corrupt settings file
                End If
                If (SettingsGet("ava_allow_lookup_after_commit", False) = "True") And (docType = commitType) Then
                    ' Some companies want to trigger a tax lookup ALL the time, regardless of doctype. This is not great coding... 
                    ' Should we limit this to only when managing tax as a line item?  The Admin implies it by nesting the setting
                    CheckTax(True)
                    Return
                End If

                If (docType = "INVOICE" And commitType = "ORDER") Then
                    Return
                End If
                If (Not CheckQWDocInvoice()) Then
                    'Log("Quotewerks Document was converted to the docType (ORDER or INVOICE) that we are NOT committing on.  Skipping commit.")
                    Return
                End If
                'check if tax rate has been fetched and commit
                CheckTax(True)
            End If
        End Sub

		Public Sub AfterDeleteDocument(<[In]> lDocID As Integer, <[In]> iResult As Short) Implements __Application.AfterDeleteDocument
			''Throw New NotImplementedException()
		End Sub

		Public Sub AfterNewDocument(<[In]> iSource As Short) Implements __Application.AfterNewDocument
			''Throw New NotImplementedException()
		End Sub

		Public Sub AfterOpenDocument() Implements __Application.AfterOpenDocument
			''Throw New NotImplementedException()
		End Sub

		Public Sub AfterRenameDocument(<[In]> <Out> ByRef iResult As Short) Implements __Application.AfterRenameDocument
			''Throw New NotImplementedException()
		End Sub

		Public Sub AfterSaveDocument(<[In]> iResult As Short) Implements __Application.AfterSaveDocument
			''Throw New NotImplementedException()
		End Sub

		Public Sub AfterShippingSelection(<[In]> iResult As Short) Implements __Application.AfterShippingSelection

            If (iResult <> 0) Then
                Return
            End If

            If (CheckQWDocInvoice()) Then ' If it's already committed, then don't do any lookups
                Return
            End If

            If (QWApp.DocFunctions.GetDocumentHeaderValue("ShippingAmount") = 0) Then
				Return
			End If

			If (SettingsGet("address_validate") = "True") Then
				'check if address has been validated
				CheckAddress()
			End If

			If (SettingsGet("ava_tax_enable") = "True") Then
				QWApp.DocFunctions.SetDocumentHeaderValue(SettingsGet("ava_custom_field_mapping_tax_address"), "", True)
				'check if tax rate has been fetched but do not commit
				CheckTax(False)
			End If


		End Sub

		Public Sub BeforeContactSelection(<[In]> iType As Short, <[In]> <Out> ByRef iSource As Short) Implements __Application.BeforeContactSelection
			''Throw New NotImplementedException()
		End Sub

		Public Sub BeforeConvertDocument(<[In]> <Out> ByRef iDestinationType As Short, <[In]> <Out> ByRef bCancel As Boolean) Implements __Application.BeforeConvertDocument
			Log("QWApp_BeforeConvertDocument")
			If (iDestinationType <> 1) Then
				Return
			End If

			If (SettingsGet("address_validate") = "True") Then
				'check if address has been validated
				CheckAddress()
			End If
		End Sub

		Public Sub BeforeDeleteDocument(<[In]> iSource As Short, <[In]> lDocID As Integer, <[In]> <Out> ByRef bCancel As Boolean) Implements __Application.BeforeDeleteDocument
			''Throw New NotImplementedException()
		End Sub

		Public Sub BeforeEmailRecipientSelection(<[In]> iRecipientType As Short, <[In]> <MarshalAs(UnmanagedType.BStr)> <Out> ByRef sTOList As String, <[In]> <MarshalAs(UnmanagedType.BStr)> <Out> ByRef sFrom As String, <[In]> <MarshalAs(UnmanagedType.BStr)> <Out> ByRef sCCList As String, <[In]> <MarshalAs(UnmanagedType.BStr)> <Out> ByRef sBCCList As String, <[In]> <MarshalAs(UnmanagedType.BStr)> <Out> ByRef sSubject As String, <[In]> <Out> ByRef bSuppressQWAction As Boolean) Implements __Application.BeforeEmailRecipientSelection
			'Throw New NotImplementedException()
		End Sub

		Public Sub BeforePreviewAction(<[In]> iAction As Short, <[In]> <MarshalAs(UnmanagedType.BStr)> sReference As String, <[In]> iSource As Short, <[In]> iLayoutType As Short, <[In]> <MarshalAs(UnmanagedType.BStr)> sLayoutFileName As String, <[In]> <Out> ByRef bCancel As Boolean) Implements __Application.BeforePreviewAction
			'Throw New NotImplementedException()
		End Sub

		Public Sub BeforePrint(<[In]> <Out> ByRef bCancel As Boolean) Implements __Application.BeforePrint
			'Throw New NotImplementedException()
		End Sub

		Public Sub BeforePrintDocument(<[In]> iPrintMethod As Short, <[In]> iLayoutType As Short, <[In]> <MarshalAs(UnmanagedType.BStr)> sLayoutFileName As String, <[In]> <Out> ByRef bCancel As Boolean) Implements __Application.BeforePrintDocument
			'Throw New NotImplementedException()
		End Sub

		Public Sub BeforeRenameDocument(<[In]> <MarshalAs(UnmanagedType.BStr)> sNewDocName As String, <[In]> <Out> ByRef bCancel As Boolean) Implements __Application.BeforeRenameDocument
			'Throw New NotImplementedException()
		End Sub

        Public Sub BeforeSaveDocument(<[In]> iSaveAction As Short, <[In]> <Out> ByRef bCancel As Boolean) Implements __Application.BeforeSaveDocument
            If (CheckQWDocInvoice()) Then
                Log("Skipping Address Validation & Tax Calculation.  Quotewerks Document has already been converted To invoice")
                'Once something is an invoice, it is has gone from a document to a final record and should no longer be updated by the system.
                Return
            End If
            If (SettingsGet("address_validate") = "True") Then
                'check if address has been validated
                CheckAddress()
            End If

            If (SettingsGet("ava_tax_enable") = "True") Then
                'check if tax rate has been fetched but do not commit
                CheckTax(False)
            End If




            'If field [CustomText01] in QuoteWerks not populated with "5"
            'If LCase(QWApp.DocFunctions.GetDocumentHeaderValue("CustomText01")) <> "5" Then

            'Cancel save
            'bCancel = True
            '
            'Notify user
            'MsgBox("Save is cancelled by add-on application.", MsgBoxStyle.Exclamation)

            'Else

            'Create new frmSaveDoc
            'Since this QuoteWerks COM Object event is spawning the form, the form is in the same
            'thread, so it does not require the use of delegates.
            'frmSave = New Form1()
            'frmSave.ShowDialog()
            'Show frmSave so that the user can change some values before saving to the database.
            'The code for all this is in the frmSave.

            'If (frmSave.ShowDialog() = DialogResult.Cancel) Then

            'If dialog canceled, cancel save
            'bCancel = True

            'End If

            'End If

            'End If

        End Sub

        Public Sub BeforeShippingSelection(<[In]> <Out> ByRef bCancel As Boolean) Implements __Application.BeforeShippingSelection
			'Throw New NotImplementedException()
		End Sub

		Public Sub XMLEvent(<[In]> <MarshalAs(UnmanagedType.BStr)> <Out> ByRef sXMLEventData As String) Implements __Application.XMLEvent
			'Throw New NotImplementedException()
		End Sub
	End Class

End Namespace
