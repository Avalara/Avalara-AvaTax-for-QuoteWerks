Option Strict Off
Option Explicit On
Module modQWAPI

    Public Const gsQuoteWerksNotRunningMsg As String = "QuoteWerks is not running. QuoteWerks must be running in order to use this API"

    Public Enum qwLineTypeConstants
        qwLineTypeProductService = 1
        qwLineTypeComment = 2
        qwLineTypeSubTotal = 4
        qwLineTypeGroupHeader = 8
        qwLineTypeRunningSubTotal = 16
        'Why was the value of 32 skipped?
        qwLineTypePercentDiscount = 64
        qwLineTypePercentCharge = 128
        qwLineTypeHeading = 256
        qwLineTypeSummary = 512
    End Enum

    Public Enum qwLineAttributeConstants
        qwLineAttributeNone = 0
        qwLineAttributeExclude = 1
        qwLineAttributeHidePrice = 2
        qwLineAttributeDontPrint = 4
        qwLineAttributeGroupMember = 8
        qwLineAttributeOption = 16
        qwLineAttributeAltIsOverided = 32
        qwLineAttributePrintPicture = 64
        qwLineAttributeIsRecurring = 128
        qwLineAttributeOptionSelected = 256
        qwLineAttributeHideQuantity = 512
    End Enum

    Public Enum qwPrintMethod
        qwPrint = 0
        qwPreview = 1
        qwEmail = 2
        qwFaxWinFax = 3
        qwFaxFaxRush = 4
    End Enum
    Sub AddDelimitedFieldToString(ByRef sTarget As String, ByVal sDelimiter As String, ByVal sFieldValue As String)

        'Note, there is a limitation to this function. If the first sFieldValue passed to this
        'function is blank, the first field will be completly lost, in that there will be
        'no placeholder for it.

        If sTarget = "" Then
            sTarget = sTarget & sFieldValue
        Else
            sTarget = sTarget & sDelimiter & sFieldValue
        End If

    End Sub
    Function Rd(ByRef Value As Object) As Double

        If IsNumeric(Value) Then
            Rd = CDbl(Value)
        Else
            Rd = 0
        End If

    End Function

    Function IsItemAttributeSet(ByVal Attributes As Integer, ByVal TargetAttribute As Integer) As Boolean
        Dim bResult As Boolean

        bResult = ((Attributes And TargetAttribute) = TargetAttribute)

        IsItemAttributeSet = bResult

    End Function

    Function IsQuoteWerksRunning() As Boolean
        'Dim objQuoteWerks As QuoteWerks.Application
        Dim objQuoteWerks As Object

        On Error Resume Next
        objQuoteWerks = GetObject(, "QuoteWerks.Application")


		IsQuoteWerksRunning = Not (objQuoteWerks Is Nothing)
        objQuoteWerks = Nothing

    End Function

	Function IsMinimumVersion(ByVal VersionMajor As Decimal, ByVal VersionMinor As Decimal, ByVal minVersionMajor As Decimal, ByVal minVersionMinor As Decimal) As Boolean

		Dim boolReturnBuffer As Boolean

        'If version-major is less than required, then false
        If VersionMajor < minVersionMajor Then

			boolReturnBuffer = False

            'If version-major is as required and version-minor is as required or greater, or if version-major is greater than required, then true
        ElseIf (VersionMajor = minVersionMajor And VersionMinor >= minVersionMinor) Or (VersionMajor > minVersionMajor) Then

			boolReturnBuffer = True

		End If

		IsMinimumVersion = boolReturnBuffer

	End Function

	Public Function CheckQWDoc() As Boolean
		'Get active document index
		Dim iDocIndex = QWApp.DocFunctions.GetActiveDocumentIndex()
        'Log("Document Index: " & iDocIndex)

        If (iDocIndex <> -1) Then
			Return True
		Else
			Return False
		End If
	End Function

	Public Function CheckQWDocInvoice() As Boolean

        Dim docType As String = QWApp.DocFunctions.GetDocumentHeaderValue("DocType")
        Dim commitType As String = SettingsGet("ava_commit_doctype")
        If (commitType <> "INVOICE" And commitType <> "ORDER") Then
            commitType = "INVOICE"
        End If

        If (SettingsGet("ava_allow_lookup_after_commit", False) = "True") Then
            ' Some companies want to trigger a tax lookup ALL the time, regardless of doctype.  
            ' Should we limit this to only when managing tax as a line item?  The Admin implies it by nesting the setting
            Return False
        ElseIf (docType = "INVOICE") Then
            Return True
            ' Whether we commit on Invoice or Order, we should always stop updating
        ElseIf (commitType = "ORDER" And docType = commitType) Then
            Return True
            ' if we commit on Order and we have an Order, then we've reached "invoice" from a final record standpoint
        Else
            Return False
		End If

    End Function
End Module