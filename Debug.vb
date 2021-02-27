Public Class Debug
    Public DebugFlag As Boolean = False

    Public Sub Status(ByVal status As String)
        If (DebugFlag) Then
            TextBox.Text &= "=========== " & status & " ===========" & vbCrLf
        End If
    End Sub

    Public Sub Query(ByVal command As String)
        If (DebugFlag) Then
            TextBox.Text &= vbCrLf & ">>> '" & command & "' (" & StrToHex(command) & ")" & vbCrLf
        End If
    End Sub

    Public Sub Reply(ByVal command As String)
        If (DebugFlag) Then
            TextBox.Text &= "<<< '" & command & "' (" & StrToHex(command) & ")" & vbCrLf
        End If
    End Sub

    Public Function StrToHex(ByRef Data As String) As String
        Dim sVal As String
        Dim sHex As String = ""
        While Data.Length > 0
            sVal = Conversion.Hex(Strings.Asc(Data.Substring(0, 1).ToString()))
            Data = Data.Substring(1, Data.Length - 1)
            sHex = sHex & " " & sVal
        End While
        Return sHex
    End Function

End Class

