Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.IO

Public Class ELM
    ' serial port settings
    Private COMPort As IO.Ports.SerialPort
    Private COMPortName As String
    Private BaudRate As String
    ' tcp/ip settings
    Private TCP As TcpClient
    Private TCPPort As String
    Private IPAddress As String
    Private Stream As NetworkStream

    ' general flags
    Private ModeSerial As Boolean = False
    Private ModeTCPIP As Boolean = False
    Public EchoEnabled As Boolean = True    
    Private ConnOpen As Boolean = False



    Public Sub ConnectSerial(ByVal COMPortName As String, ByVal BaudRate As String)
        Me.COMPortName = COMPortName
        Me.BaudRate = BaudRate

        Try
            If (ConnOpen) Then
                Main.ELM.Close()
            End If

            Me.COMPort = My.Computer.Ports.OpenSerialPort(Me.COMPortName)
            Me.COMPort.NewLine = vbCrLf
            Me.COMPort.ReadTimeout = 5000
            Me.ModeSerial = True
            Debug.Status("COM Port " & Me.COMPortName & " opened")
            ConnOpen = True
        Catch Exc As System.Exception
            MsgBox("ConnectSerial Error: '" & Exc.Message & "'")
        End Try
    End Sub

    Public Sub ConnectTCPIP(ByVal IPAddress As String, ByVal TCPPort As String)
        Me.IPAddress = IPAddress
        Me.TCPPort = TCPPort

        Try
            If (ConnOpen) Then
                Main.ELM.Close()
            End If

            Me.TCP = New TcpClient(IPAddress, TCPPort)            
            Me.TCP.ReceiveTimeout = 5000
            While Not TCP.Connected
                ' do nothing
                Thread.Sleep(20)
            End While            
            Stream = TCP.GetStream()
            Me.ModeTCPIP = True
            Debug.Status("TCP Port " & Me.IPAddress & ":" & Me.TCPPort & " opened")
            ConnOpen = True
        Catch Exc As System.Exception
            MsgBox("ConnectTCPIP Error: '" & Exc.Message & "'")
        End Try
    End Sub


    Private Sub WriteLine(ByVal msg As String)

        Debug.Query(msg)
        If (ModeSerial) Then
            Me.COMPort.WriteLine(msg)
        End If
        If (ModeTCPIP) Then
            If Stream.CanWrite Then
                Dim data As [Byte]() = System.Text.Encoding.ASCII.GetBytes(msg & vbCr)
                Stream.Write(data, 0, data.Length)
            End If
            Stream.Flush()
        End If
    End Sub

    Private Function ReadLine()
        Dim line As String = Nothing
        If (ModeSerial) Then
            Try              
                line = Me.COMPort.ReadLine()
            Catch Exc As System.Exception
                line = Nothing
                'MsgBox("ReadLine Error '" & Exc.Message & "'")
            End Try
        End If
        If (ModeTCPIP) Then
            Do
                Dim inStream(1024) As Byte
                Dim recv = Stream.Read(inStream, 0, inStream.Length)
                line = Encoding.GetEncoding("Windows-1252").GetString(inStream, 0, recv)
            Loop While Stream.DataAvailable
        End If
        Debug.Reply(line)        
        Return line
    End Function


    Public Sub Reset()
        ' only reset in serial mode otherwise we could loose WLAN/Bluetooth connection
        Debug.Status("START RESET")
        'If (ModeSerial) Then
        '    Me.CommandSingle("ATZ")
        'End If
        ' enable linebreaks
        Me.CommandSingle("ATL1")
        ' enable echo
        Me.CommandSingle("ATE1")
        Me.EchoEnabled = True
        Debug.Status("END RESET")
    End Sub

    Public Sub CanMode1()
        Debug.Status("START CANMODE")
        Me.CommandSingle("ATSP6")
        Debug.Status("END CANMODE")
    End Sub

    Public Sub AutoMode()
        Debug.Status("START AUTOMODE")
        Me.CommandSingle("ATSP0")
        Debug.Status("END AUTOMODE")
    End Sub

    Public Function ReadPid(ByVal mode As Integer, ByVal pid As Integer)
        Debug.Status("START READ PID")
        Dim data As String = CommandSingle(ModePidToCommand(mode, pid))
        Debug.Status("END READ PID")
        Return (data)
    End Function
    Public Function ReadVIN()
        Debug.Status("START VIN")
        Return CommandMulti(ModePidToCommand(&H9, &H2))
        Debug.Status("END VIN")
    End Function

    Public Function ReadCALID()
        Debug.Status("START CALID")
        Return CommandMulti(ModePidToCommand(&H9, &H4))
        Debug.Status("END CALID")
    End Function

    Public Function ReadString(ByVal mode As Integer, ByVal pid As Integer)
        Dim cmd As String = ModePidToCommand(mode, pid)
        Dim reply As String = ""
        Dim str As String = "INVALID DATA"
        Dim i As Integer

        reply = Me.CommandSingle(cmd)

        Dim DataBytes() As String = Split(reply)        
        If (DataBytes.Length > 3) Then
            str = ""
            i = 3
            While (i < DataBytes.Length)
                Try
                    str &= Chr(Convert.ToInt32(DataBytes(i), 16))
                Catch ex As Exception
                    MsgBox("ReadString - Error converting Hex to Char i=" & i & " str= '" & reply & "'")
                End Try
                i += 1
            End While
        End If
        Return (str)
    End Function

    Public Sub Close()
        Debug.Status("START ELM CLOSE")
        If Me.COMPort IsNot Nothing And Me.ModeSerial Then
            Me.COMPort.Close()
        End If
        If Me.TCP IsNot Nothing And Me.ModeTCPIP Then
            Me.Stream.Close()
            Me.TCP.Close()
        End If
        Debug.Status("END ELM CLOSED")
    End Sub

    Public Function CommandSingle(ByVal cmd As String)
        Dim reply As String = ""
        Dim echo_received As Boolean

        Try
            Me.WriteLine(cmd)
            Do
                Dim line As String = Me.ReadLine()
                If line Is Nothing Then
                    Exit Do
                End If
                ' handle echo if enabled
                If (Me.EchoEnabled) Then
                    If echo_received Then
                        reply = line
                        Exit Do
                    End If
                    If (line.Contains(cmd)) Then
                        echo_received = True
                    End If
                Else
                    reply = line
                    Exit Do
                End If
            Loop
        Catch Exc As System.Exception
            MsgBox("cmd: '" & cmd & "' Error: '" & Exc.Message & "'")
        End Try

        reply = reply.Replace(Chr(10), "")
        reply = reply.Replace(Chr(13), "")
        reply = reply.Trim
        Debug.Reply(reply)
        Return (reply)
    End Function

    Public Function CommandMulti(ByVal cmd As String)
        Dim Str As String = ""
        Dim reply As String = ""
        Dim echo_received As Boolean

        Try
            Me.WriteLine(cmd)
            Do
                Dim line As String = Me.ReadLine()
                If line Is Nothing Then
                    Exit Do
                End If
                ' handle echo if enabled
                If (Me.EchoEnabled) Then
                    If echo_received Then
                        reply = line
                        Exit Do
                    End If
                    If (line.Contains(cmd)) Then
                        echo_received = True
                    End If
                Else
                    reply = line
                    Exit Do
                End If
            Loop
            ' First reply is the length
            reply = reply.Replace(Chr(10), "")
            reply = reply.Replace(Chr(13), "")
            reply = reply.Trim
            reply = reply.Replace(">", "")
            Dim len As Integer = Convert.ToInt32(reply, 16)
            Debug.Status("Length= " & len)

            Dim data As String = ""
            Dim i As Integer = 0
            ' calc number of rows from reply length
            While (i < len \ 8)
                Dim line As String = Me.ReadLine()
                If line Is Nothing Then
                    Exit While
                End If
                If (line.Contains(":")) Then
                    line = line.Substring(3)
                End If
                line = line.Replace(Chr(10), "")
                line = line.Replace(Chr(13), "")
                line = line.Trim
                data &= line & " "
                i += 1
            End While
            ' take care of any remainder row
            If (len Mod 8) Then
                Dim line As String = Me.ReadLine()
                If (line.Contains(":")) Then
                    line = line.Substring(3)
                End If
                line = line.Replace(Chr(10), "")
                line = line.Replace(Chr(13), "")
                line = line.Trim
                data &= line & " "
            End If
            Debug.Status("Data= " & data)
            Dim DataBytes() As String = Split(data)
            Debug.Status("SplitLen= " & DataBytes.Length)
            If (DataBytes.Length >= 8) Then
                i = 8
                While (i < DataBytes.Length - 1)
                    Try
                        Str &= Chr(Convert.ToInt32(DataBytes(i), 16))
                    Catch ex As Exception
                        MsgBox("CommandMulti - Error converting Hex to Char i=" & i & " str= '" & reply & "'")
                    End Try                    
                    i += 1
                End While
            End If
            Debug.Status("Str= " & Str & vbCrLf)

        Catch Exc As System.Exception
            MsgBox("cmd: '" & cmd & "' Error: '" & Exc.Message & "'")
        End Try

        Debug.Reply(Str)
        Return (Str)
    End Function

    Private Function ModePidToCommand(ByVal mode As Integer, ByVal pid As Integer)
        Dim Command As String

        ' Format ELM compliant command
        ' XX YY ZZ 
        ' XX = Mode
        ' YY = PID upper byte
        ' ZZ = PID lower byte
        ' *** all values need to be HEX, and with a leading 0
        Command = String.Format("{0:X2}", mode) & " " & String.Format("{0:X2}", (pid And &HFF00) >> 8) & " " & String.Format("{0:X2}", (pid And &HFF))
        Return (Command)
    End Function
End Class
