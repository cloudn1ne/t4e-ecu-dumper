Imports System.Windows.Forms
Imports System.Text.RegularExpressions


imports System.Threading
imports System.Net
imports System.Net.Sockets
imports System.Text
imports System.IO

Public Class OBDAdapter

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OBDAdapter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (Debug.DebugFlag = True) Then
            CheckBoxDebug.Checked = True
        Else
            CheckBoxDebug.Checked = False
        End If
        TextBoxECUType.Text = ""
        TextBoxECUInfo.Text = ""
        TextBoxCALID.Text = ""
        TextBoxVIN.Text = ""
        StatusTextBox.Text = "Turn on Ignition and click Test Connection"
        StatusTextBox.BackColor = Color.White
        ' OK button is disabled by default, only enabled if ELM is found
        OK_Button.Enabled = False
        GetSerialPortNames()
        ' set default baudrate for ELM327
        BaudRateComboBox.SelectedIndex = BaudRateComboBox.FindString("115200")
        ' set first COM Port
        Try
            COMPortComboBox.SelectedIndex = COMPortComboBox.Items.Count - 1
        Catch ex As Exception
            MsgBox("No COM Ports have been found !")
        End Try

    End Sub

    Sub GetSerialPortNames()
        ' Show all available COM ports.
        COMPortComboBox.Items.Clear()
        For Each sp As String In My.Computer.Ports.SerialPortNames
            COMPortComboBox.Items.Add(sp)
        Next
    End Sub

 

    Private Sub ButtonTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTest.Click        
        ' reset status boxes
        TextBoxECUType.Text = ""
        TextBoxECUInfo.Text = ""
        TextBoxCALID.Text = ""
        TextBoxVIN.Text = ""
        StatusTextBox.Text = "Turn on Ignition and click Test Connection"
        StatusTextBox.BackColor = Color.White

        If (Debug.DebugFlag = True) Then
            Debug.TextBox.Text = "Connecting to ELM" & vbCrLf
            Debug.Show()
        Else
            Debug.Hide()
        End If

        If (RadioButtonSerial.Checked) Then
            Init_Serial()
        Else
            Init_TCP()
        End If

        Main.ELM.Reset()
        'Main.ELM.CanMode()
        Main.ELM.AutoMode()
        Debug.Status("START ADAPTER INFO")
        StatusTextBox.Text = Main.ELM.CommandSingle("ATI")
        Debug.Status("END ADAPTER INFO")        
        Debug.Status("START ECU TYPE")
        TextBoxECUType.Text = Main.ELM.ReadString(&H22, &H211).Trim
        Debug.Status("END ECU TYPE")
        If TextBoxECUType.Text.Contains("T4E") Then
            Debug.Status("START ECU INFO")
            TextBoxECUInfo.Text = Main.ELM.ReadString(&H22, &H21C) & Main.ELM.ReadString(&H22, &H21D) _
                                & Main.ELM.ReadString(&H22, &H21E) & Main.ELM.ReadString(&H22, &H21F) _
                                & Main.ELM.ReadString(&H22, &H221) & Main.ELM.ReadString(&H22, &H222) _
                                & Main.ELM.ReadString(&H22, &H223) & Main.ELM.ReadString(&H22, &H224)
            Debug.Status("END ECU INFO")
        Else
            TextBoxECUInfo.Text = "n/a"
        End If
        TextBoxVIN.Text = Main.ELM.ReadVIN()
        TextBoxCALID.Text = Main.ELM.ReadCALID()
        ' enable text boxes
        TextBoxECUType.Enabled = True
        TextBoxECUInfo.Enabled = True
        TextBoxVIN.Enabled = True
        TextBoxCALID.Enabled = True
        ' pass back type/info so we dont need to re-read this
        Main.ECUType = TextBoxECUType.Text
        Main.ECUInfo = TextBoxECUInfo.Text
        Main.VIN = TextBoxVIN.Text
        Main.CALID = TextBoxCALID.Text
        ' set status box green
        StatusTextBox.BackColor = Color.Green
        ' enable OK button
        OK_Button.Enabled = True
        ' enable Read ECU menu entry
        Main.ReadDataToolStripMenuItem.Enabled = True
    End Sub

    Private Sub Init_TCP()
        Main.ELM.ConnectTCPIP(TextBoxIPAddr.Text, TextBoxTCPPort.Text)
    End Sub
    Private Sub Init_Serial()
        Main.ELM.ConnectSerial(COMPortComboBox.SelectedItem.ToString, BaudRateComboBox.SelectedItem.ToString)       
    End Sub




    Private Sub CheckBoxDebug_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxDebug.CheckedChanged
        Debug.DebugFlag = CheckBoxDebug.CheckState
    End Sub

    Private Sub TextBoxTCPPort_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxTCPPort.KeyPress
        ' accept only numbers
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TextBoxIPAddr_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxIPAddr.KeyPress
        If Char.IsDigit(e.KeyChar) Or e.KeyChar = "." Or Asc(e.KeyChar) = Keys.Delete Or _
         Asc(e.KeyChar) = Keys.Right Or Asc(e.KeyChar) = Keys.Left Or Asc(e.KeyChar) = Keys.Delete Or Asc(e.KeyChar) = Keys.Back Then
            Return
        End If
        e.Handled = True
    End Sub



    Private Sub TextBoxIPAddr_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxIPAddr.LostFocus
        Dim rx As New Regex("^(?:(?:25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)(?(?=\.?\d)\.)){4}$")

        If Not rx.IsMatch(TextBoxIPAddr.Text) Then
            MessageBox.Show("The IP address is not in proper format!")
            TextBoxIPAddr.SelectAll()
        End If
    End Sub

    Private Sub RadioButtonTCP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonTCP.CheckedChanged
        If RadioButtonTCP.Checked Then
            TextBoxIPAddr.Enabled = True
            TextBoxTCPPort.Enabled = True
        Else
            TextBoxIPAddr.Enabled = False
            TextBoxTCPPort.Enabled = False
        End If
    End Sub

    Private Sub RadioButtonSerial_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonSerial.CheckedChanged
        If RadioButtonSerial.Checked Then
            COMPortComboBox.Enabled = True
            BaudRateComboBox.Enabled = True
        Else
            COMPortComboBox.Enabled = False
            BaudRateComboBox.Enabled = False
        End If
    End Sub

    Private Sub TextBoxIPAddr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxIPAddr.TextChanged

    End Sub
End Class
