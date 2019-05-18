<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class exemption
	Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.exemption_type = New System.Windows.Forms.ComboBox()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.custom_type = New System.Windows.Forms.TextBox()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'exemption_type
		'
		Me.exemption_type.FormattingEnabled = True
		Me.exemption_type.Items.AddRange(New Object() {"A - Federal Government", "B - State/Local Govt.", "C - Tribal Government", "D - Foreign Diplomat", "E - Charitable Organization", "F - Religious/Education", "G - Resale", "H - Agricultural Production", "I - Industrial Prod/Mfg.", "J - Direct Pay Permit", "K - Direct Mail", "L - Other", "N - Local Government", "P - Commercial Aquaculture (Canada)", "Q - Commercial Fishery (Canada)", "R - Non-resident (Canada)"})
		Me.exemption_type.Location = New System.Drawing.Point(12, 25)
		Me.exemption_type.Name = "exemption_type"
		Me.exemption_type.Size = New System.Drawing.Size(191, 21)
		Me.exemption_type.TabIndex = 0
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(12, 91)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(75, 23)
		Me.Button1.TabIndex = 1
		Me.Button1.Text = "OK"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(9, 9)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(83, 13)
		Me.Label1.TabIndex = 3
		Me.Label1.Text = "Exemption Type"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(12, 49)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(121, 13)
		Me.Label2.TabIndex = 4
		Me.Label2.Text = "Custom Exemption Type"
		'
		'custom_type
		'
		Me.custom_type.Location = New System.Drawing.Point(12, 65)
		Me.custom_type.Name = "custom_type"
		Me.custom_type.Size = New System.Drawing.Size(191, 20)
		Me.custom_type.TabIndex = 5
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(128, 91)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(75, 23)
		Me.Button2.TabIndex = 6
		Me.Button2.Text = "Cancel"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'exemption
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(215, 122)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.custom_type)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.exemption_type)
		Me.Name = "exemption"
		Me.Text = "Exemption"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents exemption_type As ComboBox
	Friend WithEvents Button1 As Button
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
	Friend WithEvents custom_type As TextBox
	Friend WithEvents Button2 As Button
End Class
