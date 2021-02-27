<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OBDAdapter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OBDAdapter))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TextBoxTCPPort = New System.Windows.Forms.TextBox
        Me.TextBoxIPAddr = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.RadioButtonTCP = New System.Windows.Forms.RadioButton
        Me.RadioButtonSerial = New System.Windows.Forms.RadioButton
        Me.CheckBoxDebug = New System.Windows.Forms.CheckBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TextBoxCALID = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TextBoxVIN = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TextBoxECUInfo = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBoxECUType = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.StatusTextBox = New System.Windows.Forms.TextBox
        Me.ButtonTest = New System.Windows.Forms.Button
        Me.BaudRateComboBox = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.COMPortComboBox = New System.Windows.Forms.ComboBox
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(116, 446)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBoxTCPPort)
        Me.GroupBox1.Controls.Add(Me.TextBoxIPAddr)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.RadioButtonTCP)
        Me.GroupBox1.Controls.Add(Me.RadioButtonSerial)
        Me.GroupBox1.Controls.Add(Me.CheckBoxDebug)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.TextBoxCALID)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.TextBoxVIN)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.TextBoxECUInfo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TextBoxECUType)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.StatusTextBox)
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox1.Controls.Add(Me.ButtonTest)
        Me.GroupBox1.Controls.Add(Me.BaudRateComboBox)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.COMPortComboBox)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(268, 481)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Settings"
        '
        'TextBoxTCPPort
        '
        Me.TextBoxTCPPort.Enabled = False
        Me.TextBoxTCPPort.Location = New System.Drawing.Point(80, 154)
        Me.TextBoxTCPPort.Name = "TextBoxTCPPort"
        Me.TextBoxTCPPort.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxTCPPort.TabIndex = 24
        Me.TextBoxTCPPort.Text = "35000"
        Me.TextBoxTCPPort.Visible = False
        '
        'TextBoxIPAddr
        '
        Me.TextBoxIPAddr.Enabled = False
        Me.TextBoxIPAddr.Location = New System.Drawing.Point(80, 127)
        Me.TextBoxIPAddr.Name = "TextBoxIPAddr"
        Me.TextBoxIPAddr.Size = New System.Drawing.Size(121, 20)
        Me.TextBoxIPAddr.TabIndex = 23
        Me.TextBoxIPAddr.Text = "127.0.0.1"
        Me.TextBoxIPAddr.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(16, 157)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 13)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Port:"
        Me.Label9.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(16, 130)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 13)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "IP Address:"
        Me.Label10.Visible = False
        '
        'RadioButtonTCP
        '
        Me.RadioButtonTCP.AutoSize = True
        Me.RadioButtonTCP.Location = New System.Drawing.Point(9, 104)
        Me.RadioButtonTCP.Name = "RadioButtonTCP"
        Me.RadioButtonTCP.Size = New System.Drawing.Size(103, 17)
        Me.RadioButtonTCP.TabIndex = 19
        Me.RadioButtonTCP.Text = "TCP Connection"
        Me.RadioButtonTCP.UseVisualStyleBackColor = True
        Me.RadioButtonTCP.Visible = False
        '
        'RadioButtonSerial
        '
        Me.RadioButtonSerial.AutoSize = True
        Me.RadioButtonSerial.Checked = True
        Me.RadioButtonSerial.Location = New System.Drawing.Point(9, 19)
        Me.RadioButtonSerial.Name = "RadioButtonSerial"
        Me.RadioButtonSerial.Size = New System.Drawing.Size(108, 17)
        Me.RadioButtonSerial.TabIndex = 18
        Me.RadioButtonSerial.TabStop = True
        Me.RadioButtonSerial.Text = "Serial Connection"
        Me.RadioButtonSerial.UseVisualStyleBackColor = True
        '
        'CheckBoxDebug
        '
        Me.CheckBoxDebug.AutoSize = True
        Me.CheckBoxDebug.Location = New System.Drawing.Point(72, 189)
        Me.CheckBoxDebug.Name = "CheckBoxDebug"
        Me.CheckBoxDebug.Size = New System.Drawing.Size(129, 17)
        Me.CheckBoxDebug.TabIndex = 17
        Me.CheckBoxDebug.Text = "Show ELM Messages"
        Me.CheckBoxDebug.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 189)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(42, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Debug:"
        '
        'TextBoxCALID
        '
        Me.TextBoxCALID.Enabled = False
        Me.TextBoxCALID.Location = New System.Drawing.Point(11, 386)
        Me.TextBoxCALID.Name = "TextBoxCALID"
        Me.TextBoxCALID.Size = New System.Drawing.Size(247, 20)
        Me.TextBoxCALID.TabIndex = 15
        Me.TextBoxCALID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 370)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Calibration ID:"
        '
        'TextBoxVIN
        '
        Me.TextBoxVIN.Enabled = False
        Me.TextBoxVIN.Location = New System.Drawing.Point(11, 347)
        Me.TextBoxVIN.Name = "TextBoxVIN"
        Me.TextBoxVIN.Size = New System.Drawing.Size(247, 20)
        Me.TextBoxVIN.TabIndex = 13
        Me.TextBoxVIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 331)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "VIN:"
        '
        'TextBoxECUInfo
        '
        Me.TextBoxECUInfo.Enabled = False
        Me.TextBoxECUInfo.Location = New System.Drawing.Point(11, 308)
        Me.TextBoxECUInfo.Name = "TextBoxECUInfo"
        Me.TextBoxECUInfo.Size = New System.Drawing.Size(247, 20)
        Me.TextBoxECUInfo.TabIndex = 11
        Me.TextBoxECUInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 292)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "ECU Info:"
        '
        'TextBoxECUType
        '
        Me.TextBoxECUType.Enabled = False
        Me.TextBoxECUType.Location = New System.Drawing.Point(11, 269)
        Me.TextBoxECUType.Name = "TextBoxECUType"
        Me.TextBoxECUType.Size = New System.Drawing.Size(247, 20)
        Me.TextBoxECUType.TabIndex = 9
        Me.TextBoxECUType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 253)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "ECU ID:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "OBD Adapter:"
        '
        'StatusTextBox
        '
        Me.StatusTextBox.Location = New System.Drawing.Point(11, 230)
        Me.StatusTextBox.Name = "StatusTextBox"
        Me.StatusTextBox.Size = New System.Drawing.Size(247, 20)
        Me.StatusTextBox.TabIndex = 6
        Me.StatusTextBox.Text = "Turn on Ignition and click Test Connection"
        Me.StatusTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ButtonTest
        '
        Me.ButtonTest.Location = New System.Drawing.Point(150, 417)
        Me.ButtonTest.Name = "ButtonTest"
        Me.ButtonTest.Size = New System.Drawing.Size(112, 23)
        Me.ButtonTest.TabIndex = 5
        Me.ButtonTest.Text = "Test Connection"
        Me.ButtonTest.UseVisualStyleBackColor = True
        '
        'BaudRateComboBox
        '
        Me.BaudRateComboBox.FormattingEnabled = True
        Me.BaudRateComboBox.Items.AddRange(New Object() {"300", "1200", "2400", "4800", "9600", "14400", "19200", "28800", "38400", "57600", "115200", "230400"})
        Me.BaudRateComboBox.Location = New System.Drawing.Point(80, 69)
        Me.BaudRateComboBox.Name = "BaudRateComboBox"
        Me.BaudRateComboBox.Size = New System.Drawing.Size(121, 21)
        Me.BaudRateComboBox.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Baud Rate:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Serial Port:"
        '
        'COMPortComboBox
        '
        Me.COMPortComboBox.FormattingEnabled = True
        Me.COMPortComboBox.Location = New System.Drawing.Point(80, 42)
        Me.COMPortComboBox.Name = "COMPortComboBox"
        Me.COMPortComboBox.Size = New System.Drawing.Size(121, 21)
        Me.COMPortComboBox.TabIndex = 1
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'OBDAdapter
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(272, 484)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OBDAdapter"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "OBD Adapter Settings"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents COMPortComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BaudRateComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents StatusTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ButtonTest As System.Windows.Forms.Button
    Friend WithEvents TextBoxECUType As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxECUInfo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxVIN As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents TextBoxCALID As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxDebug As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTCPPort As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxIPAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents RadioButtonTCP As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSerial As System.Windows.Forms.RadioButton

End Class
