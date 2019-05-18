<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class admin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(admin))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.destination_address = New System.Windows.Forms.ComboBox()
        Me.address_zip_only = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.add_country = New System.Windows.Forms.Button()
        Me.remove_country = New System.Windows.Forms.Button()
        Me.address_countries = New System.Windows.Forms.ListBox()
        Me.address_countries_unselected = New System.Windows.Forms.ListBox()
        Me.address_confirm = New System.Windows.Forms.CheckBox()
        Me.address_validate = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.log_level = New System.Windows.Forms.ComboBox()
        Me.ava_tax_timeout = New System.Windows.Forms.TextBox()
        Me.ava_company_code = New System.Windows.Forms.TextBox()
        Me.TestOutput = New System.Windows.Forms.TextBox()
        Me.Test = New System.Windows.Forms.Button()
        Me.ava_url = New System.Windows.Forms.ComboBox()
        Me.ava_license = New System.Windows.Forms.TextBox()
        Me.ava_account = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.ava_commit_doctype = New System.Windows.Forms.ComboBox()
        Me.ava_vat_message = New System.Windows.Forms.CheckBox()
        Me.ava_vat_id_field = New System.Windows.Forms.ComboBox()
        Me.ava_enable_vat_id = New System.Windows.Forms.CheckBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.ava_override = New System.Windows.Forms.DataGridView()
        Me.QuoteWerksField = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.overrideValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.overrideTaxCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ava_item_code = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ava_country = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ava_state = New System.Windows.Forms.TextBox()
        Me.ava_zip = New System.Windows.Forms.TextBox()
        Me.ava_city = New System.Windows.Forms.TextBox()
        Me.ava_line2 = New System.Windows.Forms.TextBox()
        Me.ava_line1 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.thelabel = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ava_currency = New System.Windows.Forms.ComboBox()
        Me.ava_tax_commit = New System.Windows.Forms.CheckBox()
        Me.ava_tax_enable = New System.Windows.Forms.CheckBox()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.ava_custom_field_mapping = New System.Windows.Forms.DataGridView()
        Me.variable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.custom_field = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.ava_tax_included = New System.Windows.Forms.CheckBox()
        Me.ava_print_line = New System.Windows.Forms.CheckBox()
        Me.ava_line_item_tax_add_field = New System.Windows.Forms.ComboBox()
        Me.ava_line_item_tax_add_enable = New System.Windows.Forms.CheckBox()
        Me.ava_tax_add_detail = New System.Windows.Forms.CheckBox()
        Me.ava_tax_convert = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ava_custom_line_fields = New System.Windows.Forms.DataGridView()
        Me.Feild = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.ava_tax_allow_multiple = New System.Windows.Forms.CheckBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Cancel = New System.Windows.Forms.Button()
        Me.OK = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ava_allow_lookup_after_commit = New System.Windows.Forms.CheckBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.ava_override, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.ava_custom_field_mapping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        CType(Me.ava_custom_line_fields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(493, 416)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label29)
        Me.TabPage1.Controls.Add(Me.destination_address)
        Me.TabPage1.Controls.Add(Me.address_zip_only)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.address_confirm)
        Me.TabPage1.Controls.Add(Me.address_validate)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(485, 390)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Address Validation"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(17, 91)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(103, 13)
        Me.Label29.TabIndex = 8
        Me.Label29.Text = "Destination address "
        Me.ToolTip1.SetToolTip(Me.Label29, "Designates the address to be validated and used as the Destination Address sent t" &
        "o Avalara for tax calculations")
        '
        'destination_address
        '
        Me.destination_address.FormattingEnabled = True
        Me.destination_address.Items.AddRange(New Object() {"Sold To", "Ship To", "Bill To"})
        Me.destination_address.Location = New System.Drawing.Point(129, 88)
        Me.destination_address.Name = "destination_address"
        Me.destination_address.Size = New System.Drawing.Size(121, 21)
        Me.destination_address.TabIndex = 7
        '
        'address_zip_only
        '
        Me.address_zip_only.AutoSize = True
        Me.address_zip_only.Location = New System.Drawing.Point(20, 66)
        Me.address_zip_only.Name = "address_zip_only"
        Me.address_zip_only.Size = New System.Drawing.Size(75, 17)
        Me.address_zip_only.TabIndex = 5
        Me.address_zip_only.Text = "Use Zip+4"
        Me.ToolTip1.SetToolTip(Me.address_zip_only, "Enabling this option will add the 4 digit carrier code to the 5 digit zip. (dicta" &
        "ted not read)")
        Me.address_zip_only.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.add_country)
        Me.GroupBox1.Controls.Add(Me.remove_country)
        Me.GroupBox1.Controls.Add(Me.address_countries)
        Me.GroupBox1.Controls.Add(Me.address_countries_unselected)
        Me.GroupBox1.Location = New System.Drawing.Point(29, 152)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(357, 135)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Country Selector"
        Me.ToolTip1.SetToolTip(Me.GroupBox1, "Add to enable validation for US and Canada as well as tax calculations across oth" &
        "er countries.  Validating addresses outside the US requires an Avalara account a" &
        "nd consumes a transaction.")
        '
        'add_country
        '
        Me.add_country.Location = New System.Drawing.Point(143, 33)
        Me.add_country.Name = "add_country"
        Me.add_country.Size = New System.Drawing.Size(69, 23)
        Me.add_country.TabIndex = 5
        Me.add_country.Text = "Add ->"
        Me.add_country.UseVisualStyleBackColor = True
        '
        'remove_country
        '
        Me.remove_country.Location = New System.Drawing.Point(143, 62)
        Me.remove_country.Name = "remove_country"
        Me.remove_country.Size = New System.Drawing.Size(69, 23)
        Me.remove_country.TabIndex = 6
        Me.remove_country.Text = "<- Remove"
        Me.remove_country.UseVisualStyleBackColor = True
        '
        'address_countries
        '
        Me.address_countries.FormattingEnabled = True
        Me.address_countries.Location = New System.Drawing.Point(218, 19)
        Me.address_countries.Name = "address_countries"
        Me.address_countries.Size = New System.Drawing.Size(120, 95)
        Me.address_countries.TabIndex = 3
        '
        'address_countries_unselected
        '
        Me.address_countries_unselected.FormattingEnabled = True
        Me.address_countries_unselected.Items.AddRange(New Object() {"United States", "Canada", "United Kingdom", "European Union", "Australia", "India", "New Zealand", "South Africa"})
        Me.address_countries_unselected.Location = New System.Drawing.Point(17, 19)
        Me.address_countries_unselected.Name = "address_countries_unselected"
        Me.address_countries_unselected.Size = New System.Drawing.Size(120, 95)
        Me.address_countries_unselected.TabIndex = 2
        '
        'address_confirm
        '
        Me.address_confirm.AutoSize = True
        Me.address_confirm.Location = New System.Drawing.Point(20, 43)
        Me.address_confirm.Name = "address_confirm"
        Me.address_confirm.Size = New System.Drawing.Size(249, 17)
        Me.address_confirm.TabIndex = 1
        Me.address_confirm.Text = "Require user confirmation on corrected address"
        Me.ToolTip1.SetToolTip(Me.address_confirm, "Selecting this option will result in the user being prompted to approve the desti" &
        "nation address correction.")
        Me.address_confirm.UseVisualStyleBackColor = True
        '
        'address_validate
        '
        Me.address_validate.AutoSize = True
        Me.address_validate.Location = New System.Drawing.Point(20, 20)
        Me.address_validate.Name = "address_validate"
        Me.address_validate.Size = New System.Drawing.Size(149, 17)
        Me.address_validate.TabIndex = 0
        Me.address_validate.Text = "Enable Address Validation"
        Me.ToolTip1.SetToolTip(Me.address_validate, "Enabling will result in the Destination address being validated and updated using" &
        " the USPS address database.  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Canadian addresses are validated through Avalara." &
        "")
        Me.address_validate.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.PictureBox1)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.log_level)
        Me.TabPage2.Controls.Add(Me.ava_tax_timeout)
        Me.TabPage2.Controls.Add(Me.ava_company_code)
        Me.TabPage2.Controls.Add(Me.TestOutput)
        Me.TabPage2.Controls.Add(Me.Test)
        Me.TabPage2.Controls.Add(Me.ava_url)
        Me.TabPage2.Controls.Add(Me.ava_license)
        Me.TabPage2.Controls.Add(Me.ava_account)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(485, 390)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "AvaTax Connection"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ConnectTaxQW.My.Resources.Resources.Avalara_Logo_small
        Me.PictureBox1.Location = New System.Drawing.Point(295, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(123, 32)
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 302)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Log Level"
        Me.ToolTip1.SetToolTip(Me.Label6, "Setting the log level to Debug will maximize the level of detail shown in the log" &
        "s to include round trip timings.")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 266)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "AvaTax Timeout"
        Me.ToolTip1.SetToolTip(Me.Label5, "The number of seconds to wait for a response from Avalara before logging the tran" &
        "saction as failed.  Default is 5 seconds.")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 228)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Company Code"
        Me.ToolTip1.SetToolTip(Me.Label4, "The Company Code must match the Company Code set in the Avalara Admin Console.  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Please contact Avalara if you need assistance logging into the Admin Console.")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Service URL"
        Me.ToolTip1.SetToolTip(Me.Label3, "The base Avalara URL used to calculate tax and commit invoices.")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "License Key"
        Me.ToolTip1.SetToolTip(Me.Label2, "License key provided by Avalara")
        '
        'log_level
        '
        Me.log_level.FormattingEnabled = True
        Me.log_level.Items.AddRange(New Object() {"Standard", "Debug"})
        Me.log_level.Location = New System.Drawing.Point(104, 299)
        Me.log_level.Name = "log_level"
        Me.log_level.Size = New System.Drawing.Size(213, 21)
        Me.log_level.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.log_level, "Selects the level of detail in the log file")
        '
        'ava_tax_timeout
        '
        Me.ava_tax_timeout.Location = New System.Drawing.Point(104, 263)
        Me.ava_tax_timeout.Name = "ava_tax_timeout"
        Me.ava_tax_timeout.Size = New System.Drawing.Size(57, 20)
        Me.ava_tax_timeout.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.ava_tax_timeout, "Maximum time to wait for AvaTax request")
        '
        'ava_company_code
        '
        Me.ava_company_code.Location = New System.Drawing.Point(104, 225)
        Me.ava_company_code.Name = "ava_company_code"
        Me.ava_company_code.Size = New System.Drawing.Size(213, 20)
        Me.ava_company_code.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.ava_company_code, "Your Company Code from Avalara")
        '
        'TestOutput
        '
        Me.TestOutput.Location = New System.Drawing.Point(14, 151)
        Me.TestOutput.Multiline = True
        Me.TestOutput.Name = "TestOutput"
        Me.TestOutput.Size = New System.Drawing.Size(303, 59)
        Me.TestOutput.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TestOutput, "Test Results")
        '
        'Test
        '
        Me.Test.Location = New System.Drawing.Point(104, 112)
        Me.Test.Name = "Test"
        Me.Test.Size = New System.Drawing.Size(100, 23)
        Me.Test.TabIndex = 4
        Me.Test.Text = "Test Connection"
        Me.Test.UseVisualStyleBackColor = True
        '
        'ava_url
        '
        Me.ava_url.FormattingEnabled = True
        Me.ava_url.Items.AddRange(New Object() {"https://development.avalara.net/", "https://avatax.avalara.net/"})
        Me.ava_url.Location = New System.Drawing.Point(104, 74)
        Me.ava_url.Name = "ava_url"
        Me.ava_url.Size = New System.Drawing.Size(213, 21)
        Me.ava_url.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.ava_url, "Avalara Service URL")
        '
        'ava_license
        '
        Me.ava_license.Location = New System.Drawing.Point(104, 40)
        Me.ava_license.Name = "ava_license"
        Me.ava_license.Size = New System.Drawing.Size(175, 20)
        Me.ava_license.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.ava_license, "Your Avalara License Key")
        '
        'ava_account
        '
        Me.ava_account.Location = New System.Drawing.Point(104, 7)
        Me.ava_account.Name = "ava_account"
        Me.ava_account.Size = New System.Drawing.Size(175, 20)
        Me.ava_account.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.ava_account, "Your Avalara Account Number")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Account Number"
        Me.ToolTip1.SetToolTip(Me.Label1, "Account Number provided by Avalara")
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.ava_commit_doctype)
        Me.TabPage3.Controls.Add(Me.ava_vat_message)
        Me.TabPage3.Controls.Add(Me.ava_vat_id_field)
        Me.TabPage3.Controls.Add(Me.ava_enable_vat_id)
        Me.TabPage3.Controls.Add(Me.Label27)
        Me.TabPage3.Controls.Add(Me.ava_override)
        Me.TabPage3.Controls.Add(Me.ava_item_code)
        Me.TabPage3.Controls.Add(Me.Label26)
        Me.TabPage3.Controls.Add(Me.GroupBox2)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.ava_currency)
        Me.TabPage3.Controls.Add(Me.ava_tax_commit)
        Me.TabPage3.Controls.Add(Me.ava_tax_enable)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(485, 390)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "AvaTax Processing"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ava_commit_doctype
        '
        Me.ava_commit_doctype.FormattingEnabled = True
        Me.ava_commit_doctype.Items.AddRange(New Object() {"INVOICE", "ORDER"})
        Me.ava_commit_doctype.Location = New System.Drawing.Point(293, 27)
        Me.ava_commit_doctype.Name = "ava_commit_doctype"
        Me.ava_commit_doctype.Size = New System.Drawing.Size(94, 21)
        Me.ava_commit_doctype.TabIndex = 18
        '
        'ava_vat_message
        '
        Me.ava_vat_message.AutoSize = True
        Me.ava_vat_message.Location = New System.Drawing.Point(220, 62)
        Me.ava_vat_message.Name = "ava_vat_message"
        Me.ava_vat_message.Size = New System.Drawing.Size(194, 17)
        Me.ava_vat_message.TabIndex = 17
        Me.ava_vat_message.Text = "Add EU VAT message to line Notes"
        Me.ToolTip1.SetToolTip(Me.ava_vat_message, "If EU VAT or Reverse Charge messages are returned, adds message to the Notes fiel" &
        "d on each applicable line item.")
        Me.ava_vat_message.UseVisualStyleBackColor = True
        '
        'ava_vat_id_field
        '
        Me.ava_vat_id_field.FormattingEnabled = True
        Me.ava_vat_id_field.Items.AddRange(New Object() {"BillToCompany", "BillToContact", "BillToTitle", "ShipToCompany", "ShipToContact", "ShipToTitle", "SoldToCompany", "SoldToContact", "SoldToTitle", "CustomText01", "CustomText02", "CustomText03", "CustomText04", "CustomText05"})
        Me.ava_vat_id_field.Location = New System.Drawing.Point(355, 85)
        Me.ava_vat_id_field.Name = "ava_vat_id_field"
        Me.ava_vat_id_field.Size = New System.Drawing.Size(121, 21)
        Me.ava_vat_id_field.TabIndex = 16
        '
        'ava_enable_vat_id
        '
        Me.ava_enable_vat_id.AutoSize = True
        Me.ava_enable_vat_id.Location = New System.Drawing.Point(220, 89)
        Me.ava_enable_vat_id.Name = "ava_enable_vat_id"
        Me.ava_enable_vat_id.Size = New System.Drawing.Size(129, 17)
        Me.ava_enable_vat_id.TabIndex = 15
        Me.ava_enable_vat_id.Text = "Acquire VAT ID from :"
        Me.ToolTip1.SetToolTip(Me.ava_enable_vat_id, "Often required for tax calculations outside U.S. and Canada")
        Me.ava_enable_vat_id.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(17, 135)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(97, 13)
        Me.Label27.TabIndex = 14
        Me.Label27.Text = "Tax Code Mapping"
        Me.ToolTip1.SetToolTip(Me.Label27, resources.GetString("Label27.ToolTip"))
        '
        'ava_override
        '
        Me.ava_override.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ava_override.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ava_override.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.QuoteWerksField, Me.overrideValue, Me.overrideTaxCode})
        Me.ava_override.Location = New System.Drawing.Point(20, 151)
        Me.ava_override.Name = "ava_override"
        Me.ava_override.Size = New System.Drawing.Size(459, 129)
        Me.ava_override.TabIndex = 11
        '
        'QuoteWerksField
        '
        Me.QuoteWerksField.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.QuoteWerksField.HeaderText = "QuoteWerks Field"
        Me.QuoteWerksField.Items.AddRange(New Object() {"ManufacturerPartNumber", "VendorPartNumber", "ItemType", "InternalPartNumber", "Description", "Vendor ", "Manufacturer"})
        Me.QuoteWerksField.Name = "QuoteWerksField"
        '
        'overrideValue
        '
        Me.overrideValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.overrideValue.HeaderText = "Value"
        Me.overrideValue.Name = "overrideValue"
        '
        'overrideTaxCode
        '
        Me.overrideTaxCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.overrideTaxCode.HeaderText = "Tax Code"
        Me.overrideTaxCode.Name = "overrideTaxCode"
        '
        'ava_item_code
        '
        Me.ava_item_code.FormattingEnabled = True
        Me.ava_item_code.Items.AddRange(New Object() {"ManufacturerPartNumber", "VendorPartNumber", "InternalPartNumber", "ItemType"})
        Me.ava_item_code.Location = New System.Drawing.Point(100, 112)
        Me.ava_item_code.Name = "ava_item_code"
        Me.ava_item_code.Size = New System.Drawing.Size(188, 21)
        Me.ava_item_code.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.ava_item_code, "The QuoteWerks field used to map to the ItemCode in AvaTax")
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(17, 112)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(55, 13)
        Me.Label26.TabIndex = 3
        Me.Label26.Text = "Item Code"
        Me.ToolTip1.SetToolTip(Me.Label26, "The QuoteWerks field used to map to the ItemCode in AvaTax")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ava_country)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.ava_state)
        Me.GroupBox2.Controls.Add(Me.ava_zip)
        Me.GroupBox2.Controls.Add(Me.ava_city)
        Me.GroupBox2.Controls.Add(Me.ava_line2)
        Me.GroupBox2.Controls.Add(Me.ava_line1)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.thelabel)
        Me.GroupBox2.Location = New System.Drawing.Point(20, 286)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(373, 96)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Origin Address"
        Me.ToolTip1.SetToolTip(Me.GroupBox2, "Address associated to your business location or the origin of shipped goods")
        '
        'ava_country
        '
        Me.ava_country.Location = New System.Drawing.Point(267, 70)
        Me.ava_country.Name = "ava_country"
        Me.ava_country.Size = New System.Drawing.Size(100, 20)
        Me.ava_country.TabIndex = 8
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(218, 70)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(43, 13)
        Me.Label28.TabIndex = 11
        Me.Label28.Text = "Country"
        Me.ToolTip1.SetToolTip(Me.Label28, "Optional")
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(197, 47)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(64, 13)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Postal Code"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(190, 21)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 13)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "State/Region"
        '
        'ava_state
        '
        Me.ava_state.Location = New System.Drawing.Point(267, 18)
        Me.ava_state.Name = "ava_state"
        Me.ava_state.Size = New System.Drawing.Size(100, 20)
        Me.ava_state.TabIndex = 6
        '
        'ava_zip
        '
        Me.ava_zip.Location = New System.Drawing.Point(267, 44)
        Me.ava_zip.Name = "ava_zip"
        Me.ava_zip.Size = New System.Drawing.Size(100, 20)
        Me.ava_zip.TabIndex = 7
        '
        'ava_city
        '
        Me.ava_city.Location = New System.Drawing.Point(84, 70)
        Me.ava_city.Name = "ava_city"
        Me.ava_city.Size = New System.Drawing.Size(100, 20)
        Me.ava_city.TabIndex = 5
        '
        'ava_line2
        '
        Me.ava_line2.Location = New System.Drawing.Point(84, 44)
        Me.ava_line2.Name = "ava_line2"
        Me.ava_line2.Size = New System.Drawing.Size(100, 20)
        Me.ava_line2.TabIndex = 4
        '
        'ava_line1
        '
        Me.ava_line1.Location = New System.Drawing.Point(84, 18)
        Me.ava_line1.Name = "ava_line1"
        Me.ava_line1.Size = New System.Drawing.Size(100, 20)
        Me.ava_line1.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(1, 47)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Address Line 2"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 73)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 13)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "City"
        '
        'thelabel
        '
        Me.thelabel.AutoSize = True
        Me.thelabel.Location = New System.Drawing.Point(1, 21)
        Me.thelabel.Name = "thelabel"
        Me.thelabel.Size = New System.Drawing.Size(77, 13)
        Me.thelabel.TabIndex = 0
        Me.thelabel.Text = "Address Line 1"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Currency Code"
        Me.ToolTip1.SetToolTip(Me.Label7, "The ISO 4217 Currency Code sent to AvaTax")
        '
        'ava_currency
        '
        Me.ava_currency.FormattingEnabled = True
        Me.ava_currency.Items.AddRange(New Object() {"USD", "CAD", "GBP", "EUR", "INR", "AUD", "NZD", "ZAR"})
        Me.ava_currency.Location = New System.Drawing.Point(100, 62)
        Me.ava_currency.Name = "ava_currency"
        Me.ava_currency.Size = New System.Drawing.Size(84, 21)
        Me.ava_currency.TabIndex = 2
        '
        'ava_tax_commit
        '
        Me.ava_tax_commit.AutoSize = True
        Me.ava_tax_commit.Location = New System.Drawing.Point(20, 29)
        Me.ava_tax_commit.Name = "ava_tax_commit"
        Me.ava_tax_commit.Size = New System.Drawing.Size(265, 17)
        Me.ava_tax_commit.TabIndex = 1
        Me.ava_tax_commit.Text = "Enable AvaTax Document Commit on Convert To :"
        Me.ToolTip1.SetToolTip(Me.ava_tax_commit, resources.GetString("ava_tax_commit.ToolTip"))
        Me.ava_tax_commit.UseVisualStyleBackColor = True
        '
        'ava_tax_enable
        '
        Me.ava_tax_enable.AutoSize = True
        Me.ava_tax_enable.Location = New System.Drawing.Point(20, 6)
        Me.ava_tax_enable.Name = "ava_tax_enable"
        Me.ava_tax_enable.Size = New System.Drawing.Size(164, 17)
        Me.ava_tax_enable.TabIndex = 0
        Me.ava_tax_enable.Text = "Enable AvaTax Rate Lookup"
        Me.ToolTip1.SetToolTip(Me.ava_tax_enable, "Selecting this option will perform rooftop level tax rate lookup when saving base" &
        "d on the Destination address. ")
        Me.ava_tax_enable.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Label30)
        Me.TabPage5.Controls.Add(Me.ava_custom_field_mapping)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(485, 390)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Field Mapping"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'ava_custom_field_mapping
        '
        Me.ava_custom_field_mapping.AllowUserToAddRows = False
        Me.ava_custom_field_mapping.AllowUserToDeleteRows = False
        Me.ava_custom_field_mapping.AllowUserToResizeRows = False
        Me.ava_custom_field_mapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ava_custom_field_mapping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.variable, Me.custom_field})
        Me.ava_custom_field_mapping.Location = New System.Drawing.Point(6, 6)
        Me.ava_custom_field_mapping.Name = "ava_custom_field_mapping"
        Me.ava_custom_field_mapping.Size = New System.Drawing.Size(473, 218)
        Me.ava_custom_field_mapping.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.ava_custom_field_mapping, "Determines which custom fields are used on the QuoteWerks document.  Allows flexi" &
        "bility to work with other QuoteWerks addons.")
        '
        'variable
        '
        Me.variable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.variable.HeaderText = "Value"
        Me.variable.Name = "variable"
        Me.variable.ReadOnly = True
        '
        'custom_field
        '
        Me.custom_field.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.custom_field.HeaderText = "Custom Field"
        Me.custom_field.Name = "custom_field"
        Me.custom_field.Sorted = True
        Me.custom_field.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.ava_allow_lookup_after_commit)
        Me.TabPage6.Controls.Add(Me.ava_tax_included)
        Me.TabPage6.Controls.Add(Me.ava_print_line)
        Me.TabPage6.Controls.Add(Me.ava_line_item_tax_add_field)
        Me.TabPage6.Controls.Add(Me.ava_line_item_tax_add_enable)
        Me.TabPage6.Controls.Add(Me.ava_tax_add_detail)
        Me.TabPage6.Controls.Add(Me.ava_tax_convert)
        Me.TabPage6.Controls.Add(Me.Label8)
        Me.TabPage6.Controls.Add(Me.ava_custom_line_fields)
        Me.TabPage6.Controls.Add(Me.LinkLabel1)
        Me.TabPage6.Controls.Add(Me.ava_tax_allow_multiple)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(485, 390)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Advanced"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'ava_tax_included
        '
        Me.ava_tax_included.AutoSize = True
        Me.ava_tax_included.Location = New System.Drawing.Point(6, 186)
        Me.ava_tax_included.Name = "ava_tax_included"
        Me.ava_tax_included.Size = New System.Drawing.Size(174, 17)
        Me.ava_tax_included.TabIndex = 18
        Me.ava_tax_included.Text = "Tax Included in Extended Price"
        Me.ToolTip1.SetToolTip(Me.ava_tax_included, "Indicates that tax is already included in the price, triggering AvaTax to back-ca" &
        "lculate the tax from the price.  Should not be used with Allow Multiple Rates as" &
        " it already takes it into account.")
        Me.ava_tax_included.UseVisualStyleBackColor = True
        '
        'ava_print_line
        '
        Me.ava_print_line.AutoSize = True
        Me.ava_print_line.Checked = True
        Me.ava_print_line.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ava_print_line.Location = New System.Drawing.Point(6, 140)
        Me.ava_print_line.Name = "ava_print_line"
        Me.ava_print_line.Size = New System.Drawing.Size(206, 17)
        Me.ava_print_line.TabIndex = 17
        Me.ava_print_line.Text = "Omit tax line on document (Don't Print)"
        Me.ToolTip1.SetToolTip(Me.ava_print_line, "If tax is represented as a line item, determines whether the sales tax line item " &
        "will print on the quote template and in Quote Valet.  Checked = will not print o" &
        "n document")
        Me.ava_print_line.UseVisualStyleBackColor = True
        '
        'ava_line_item_tax_add_field
        '
        Me.ava_line_item_tax_add_field.FormattingEnabled = True
        Me.ava_line_item_tax_add_field.Items.AddRange(New Object() {"Notes", "CustomText01", "CustomText02", "CustomText03", "CustomText04", "CustomText05", "CustomText06", "CustomText07", "CustomText08", "CustomText09", "CustomText10"})
        Me.ava_line_item_tax_add_field.Location = New System.Drawing.Point(211, 70)
        Me.ava_line_item_tax_add_field.Name = "ava_line_item_tax_add_field"
        Me.ava_line_item_tax_add_field.Size = New System.Drawing.Size(121, 21)
        Me.ava_line_item_tax_add_field.TabIndex = 15
        '
        'ava_line_item_tax_add_enable
        '
        Me.ava_line_item_tax_add_enable.AutoSize = True
        Me.ava_line_item_tax_add_enable.Location = New System.Drawing.Point(41, 72)
        Me.ava_line_item_tax_add_enable.Name = "ava_line_item_tax_add_enable"
        Me.ava_line_item_tax_add_enable.Size = New System.Drawing.Size(164, 17)
        Me.ava_line_item_tax_add_enable.TabIndex = 14
        Me.ava_line_item_tax_add_enable.Text = "Add line item level tax rate to:"
        Me.ToolTip1.SetToolTip(Me.ava_line_item_tax_add_enable, "Adds the line item tax rate percentage applied on each line item.  For user refer" &
        "ence only.  ")
        Me.ava_line_item_tax_add_enable.UseVisualStyleBackColor = True
        '
        'ava_tax_add_detail
        '
        Me.ava_tax_add_detail.AutoSize = True
        Me.ava_tax_add_detail.Location = New System.Drawing.Point(6, 163)
        Me.ava_tax_add_detail.Name = "ava_tax_add_detail"
        Me.ava_tax_add_detail.Size = New System.Drawing.Size(218, 17)
        Me.ava_tax_add_detail.TabIndex = 13
        Me.ava_tax_add_detail.Text = "Add tax rate detail to line item description"
        Me.ToolTip1.SetToolTip(Me.ava_tax_add_detail, "If converting tax to a line item, this adds the jurisdiction level tax rate detai" &
        "ls to the Description field.")
        Me.ava_tax_add_detail.UseVisualStyleBackColor = True
        '
        'ava_tax_convert
        '
        Me.ava_tax_convert.AutoSize = True
        Me.ava_tax_convert.Location = New System.Drawing.Point(6, 117)
        Me.ava_tax_convert.Name = "ava_tax_convert"
        Me.ava_tax_convert.Size = New System.Drawing.Size(255, 17)
        Me.ava_tax_convert.TabIndex = 12
        Me.ava_tax_convert.Text = "Convert tax rate to line item after AvaTax Commit"
        Me.ToolTip1.SetToolTip(Me.ava_tax_convert, resources.GetString("ava_tax_convert.ToolTip"))
        Me.ava_tax_convert.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 206)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Tax Line Item Fields"
        Me.ToolTip1.SetToolTip(Me.Label8, " If converting tax to a line item, custom values may be set for one or more field" &
        "s on the sales tax line item")
        '
        'ava_custom_line_fields
        '
        Me.ava_custom_line_fields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ava_custom_line_fields.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Feild, Me.Value})
        Me.ava_custom_line_fields.Location = New System.Drawing.Point(6, 222)
        Me.ava_custom_line_fields.Name = "ava_custom_line_fields"
        Me.ava_custom_line_fields.Size = New System.Drawing.Size(373, 162)
        Me.ava_custom_line_fields.TabIndex = 10
        '
        'Feild
        '
        Me.Feild.HeaderText = "Field"
        Me.Feild.Name = "Feild"
        Me.Feild.ReadOnly = True
        Me.Feild.Width = 155
        '
        'Value
        '
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        Me.Value.Width = 155
        '
        'LinkLabel1
        '
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.Black
        Me.LinkLabel1.Location = New System.Drawing.Point(17, 26)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(462, 56)
        Me.LinkLabel1.TabIndex = 9
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = resources.GetString("LinkLabel1.Text")
        '
        'ava_tax_allow_multiple
        '
        Me.ava_tax_allow_multiple.AutoSize = True
        Me.ava_tax_allow_multiple.Location = New System.Drawing.Point(6, 6)
        Me.ava_tax_allow_multiple.Name = "ava_tax_allow_multiple"
        Me.ava_tax_allow_multiple.Size = New System.Drawing.Size(216, 17)
        Me.ava_tax_allow_multiple.TabIndex = 8
        Me.ava_tax_allow_multiple.Text = "Allow multiple tax rates on a single quote"
        Me.ToolTip1.SetToolTip(Me.ava_tax_allow_multiple, "Allows different tax rates to be applied to different line items automatically ba" &
        "sed on rules defined in AvaTax")
        Me.ava_tax_allow_multiple.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label25)
        Me.TabPage4.Controls.Add(Me.Label24)
        Me.TabPage4.Controls.Add(Me.Label23)
        Me.TabPage4.Controls.Add(Me.Label22)
        Me.TabPage4.Controls.Add(Me.Label21)
        Me.TabPage4.Controls.Add(Me.Label20)
        Me.TabPage4.Controls.Add(Me.Label19)
        Me.TabPage4.Controls.Add(Me.Label18)
        Me.TabPage4.Controls.Add(Me.Label17)
        Me.TabPage4.Controls.Add(Me.Label16)
        Me.TabPage4.Controls.Add(Me.Label15)
        Me.TabPage4.Controls.Add(Me.Label14)
        Me.TabPage4.Controls.Add(Me.Label9)
        Me.TabPage4.Controls.Add(Me.PictureBox3)
        Me.TabPage4.Controls.Add(Me.PictureBox2)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(485, 390)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "About"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label25.Location = New System.Drawing.Point(215, 251)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(111, 13)
        Me.Label25.TabIndex = 14
        Me.Label25.Text = "support@avalara.com"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label24.Location = New System.Drawing.Point(215, 229)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(168, 13)
        Me.Label24.TabIndex = 13
        Me.Label24.Text = "https://www.avalara.com/support"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(215, 207)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(82, 13)
        Me.Label23.TabIndex = 12
        Me.Label23.Text = "1-877-780-4848"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label22.Location = New System.Drawing.Point(215, 118)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(151, 13)
        Me.Label22.TabIndex = 10
        Me.Label22.Text = "https://www.connecttax.com/"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label21.Location = New System.Drawing.Point(215, 95)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(161, 13)
        Me.Label21.TabIndex = 9
        Me.Label21.Text = "connecttax@greystonetech.com"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(215, 20)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(22, 13)
        Me.Label20.TabIndex = 7
        Me.Label20.Text = "2.2"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(20, 251)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(114, 13)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "Avalara Support Email:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(20, 229)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(119, 13)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Avalara Support Online:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(20, 207)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(136, 13)
        Me.Label17.TabIndex = 4
        Me.Label17.Text = "Avalara Sales and Support:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(20, 118)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(102, 13)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "Connector Updates:"
        Me.ToolTip1.SetToolTip(Me.Label16, "Go here to download the latest Avalara for QuoteWerks module")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(20, 95)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(110, 13)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "Connector Feedback:"
        Me.ToolTip1.SetToolTip(Me.Label15, "Should we turn this thing into a self-driving car?")
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(20, 48)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(105, 13)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Connector Publisher:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(20, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Connector Version:"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.ConnectTaxQW.My.Resources.Resources.Avalara_Logo_small
        Me.PictureBox3.Location = New System.Drawing.Point(218, 171)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(190, 33)
        Me.PictureBox3.TabIndex = 11
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.ConnectTaxQW.My.Resources.Resources.greystone_small1
        Me.PictureBox2.Location = New System.Drawing.Point(218, 36)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(190, 46)
        Me.PictureBox2.TabIndex = 8
        Me.PictureBox2.TabStop = False
        '
        'Cancel
        '
        Me.Cancel.Location = New System.Drawing.Point(426, 434)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Cancel.TabIndex = 1
        Me.Cancel.Text = "Cancel"
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(345, 434)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(75, 23)
        Me.OK.TabIndex = 2
        Me.OK.Text = "OK"
        Me.OK.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'ava_allow_lookup_after_commit
        '
        Me.ava_allow_lookup_after_commit.AutoSize = True
        Me.ava_allow_lookup_after_commit.Location = New System.Drawing.Point(41, 94)
        Me.ava_allow_lookup_after_commit.Name = "ava_allow_lookup_after_commit"
        Me.ava_allow_lookup_after_commit.Size = New System.Drawing.Size(204, 17)
        Me.ava_allow_lookup_after_commit.TabIndex = 19
        Me.ava_allow_lookup_after_commit.Text = "Allow tax lookup after AvaTax Commit"
        Me.ToolTip1.SetToolTip(Me.ava_allow_lookup_after_commit, resources.GetString("ava_allow_lookup_after_commit.ToolTip"))
        Me.ava_allow_lookup_after_commit.UseVisualStyleBackColor = True
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(16, 239)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(420, 65)
        Me.Label30.TabIndex = 1
        Me.Label30.Text = resources.GetString("Label30.Text")
        '
        'admin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 469)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "admin"
        Me.Text = "Avalara for QuoteWerks"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.ava_override, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.ava_custom_field_mapping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        CType(Me.ava_custom_line_fields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Cancel As Button
    Friend WithEvents OK As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ava_account As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents log_level As ComboBox
    Friend WithEvents ava_tax_timeout As TextBox
    Friend WithEvents ava_company_code As TextBox
    Friend WithEvents TestOutput As TextBox
    Friend WithEvents Test As Button
    Friend WithEvents ava_url As ComboBox
    Friend WithEvents ava_license As TextBox
    Friend WithEvents address_confirm As CheckBox
    Friend WithEvents address_validate As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents add_country As Button
    Friend WithEvents remove_country As Button
    Friend WithEvents address_countries As ListBox
    Friend WithEvents address_countries_unselected As ListBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents ava_city As TextBox
    Friend WithEvents ava_line2 As TextBox
    Friend WithEvents ava_line1 As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents thelabel As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents ava_currency As ComboBox
    Friend WithEvents ava_tax_commit As CheckBox
    Friend WithEvents ava_tax_enable As CheckBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents ava_state As TextBox
    Friend WithEvents ava_zip As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label26 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ava_item_code As ComboBox
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents ava_custom_field_mapping As DataGridView
    Friend WithEvents custom_field As DataGridViewComboBoxColumn
    Friend WithEvents variable As DataGridViewTextBoxColumn
    Friend WithEvents address_zip_only As CheckBox
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents ava_tax_allow_multiple As CheckBox
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents Value As DataGridViewTextBoxColumn
    Friend WithEvents Feild As DataGridViewTextBoxColumn
    Friend WithEvents ava_custom_line_fields As DataGridView
    Friend WithEvents Label8 As Label
    Friend WithEvents ava_tax_convert As CheckBox
    Friend WithEvents ava_tax_add_detail As CheckBox
    Friend WithEvents ava_line_item_tax_add_enable As CheckBox
    Friend WithEvents ava_line_item_tax_add_field As ComboBox
    Friend WithEvents ava_override As DataGridView
    Friend WithEvents Label27 As Label
    Friend WithEvents ava_print_line As CheckBox
    Friend WithEvents ava_vat_id_field As ComboBox
    Friend WithEvents ava_enable_vat_id As CheckBox
    Friend WithEvents QuoteWerksField As DataGridViewComboBoxColumn
    Friend WithEvents overrideValue As DataGridViewTextBoxColumn
    Friend WithEvents overrideTaxCode As DataGridViewTextBoxColumn
    Friend WithEvents ava_country As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents ava_tax_included As CheckBox
    Friend WithEvents ava_vat_message As CheckBox
    Friend WithEvents Label29 As Label
    Friend WithEvents destination_address As ComboBox
    Friend WithEvents ava_commit_doctype As ComboBox
    Friend WithEvents ava_allow_lookup_after_commit As CheckBox
    Friend WithEvents Label30 As Label
End Class
