Public Class Main
    ' ELM interface
    Public ELM As New ELM()

    Dim header_text As String = "ECU Performance Information Report"
    Dim copyright_text As String = "T4e ECU Dumper V" & Application.ProductVersion.Substring(0, 3) & "  (c) Georg S / (cn@warp.at) - http://www.warp.at/ecu_dumper"
    Public ECUType As String
    Public ECUInfo As String
    Public VIN As String
    Public CALID As String
    ' print document settings for autosizing
    Dim arrColumnLefts As New List(Of Integer)
    Dim arrColumnWidths As New List(Of Integer)
    Dim strFormat As System.Drawing.StringFormat
    Dim bfirstPage As Boolean = True
    Dim bnewPage As Boolean = True
    Dim iTotalWidth As Integer = 0
    Dim iHeaderHeight As Integer = 0
    Dim iCellHeight As Integer = 0
    Dim iRow As Integer = 0
    Dim iCount As Integer = 0
    Dim initRowcount As Integer
    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox.Show()

    End Sub

    Private Sub OBDAdapterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OBDAdapterToolStripMenuItem.Click
        OBDAdapter.ShowDialog()

    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ToolStripStatusLabel.Text = ""
        ' fake row only for debugging
        'DataGridView.Rows.Add(New String() {"Time at TPS ", "Test", "Blah", "Time (H:M:S)"})
    End Sub

    Private Function DownloadPerformanceData()
        Dim data As String = ""
        Dim i As Integer = 0
        Dim Time_at_TPS_Categories As String() = New String() {"0", "1.5", "15", "25", "35", "50", "65", "80", "100"}
        Dim Time_at_RPM_Categories As String() = New String() {"500", "1500", "2500", "3500", "4500", "5500", "6500", "7000", "7000 RPM +"}
        Dim Time_at_KMH_Categories As String() = New String() {"0", "30", "60", "90", "120", "150", "180", "210", "210 KMH +"}
        Dim Time_at_TEMP_Categories As String() = New String() {"105", "110", "115", "119", "119 Deg C +"}

        PrintToolStripMenuItem.Enabled = False


        '=========================================================================================
        ' Add preloaded data
        '=========================================================================================
        ' add info/type of ECU
        DataGridView.Rows.Add(New String() {"ECU Info", "", ECUInfo, ""})
        DataGridView.Rows.Add(New String() {"ECU Type", "", ECUType, ""})
        DataGridView.Rows.Add(New String() {"VIN", "", VIN, ""})
        DataGridView.Rows.Add(New String() {"Calibration ID", "", CALID, ""})
        initRowcount = DataGridView.Rows.Count

        'ELM.Reset()
        'ELM.CanMode()
        'Debug.DebugFlag = False

        ToolStripStatusLabel.Text = "Downloading ECU Data"
        '=========================================================================================
        ' Read "Time at TPS" Table                
        '=========================================================================================
        Debug.Status("Time at TPS")
        i = 0
        Do While i < 8
            data = Convert_DHMS(ELM.ReadPid(&H22, &H301 + i))
            DataGridView.Rows.Add(New String() {"Time at TPS " & (i + 1).ToString, Time_at_TPS_Categories(i) & " - " & Time_at_TPS_Categories(i + 1) & " %", data, "Time (D H:M:S)"})
            i = i + 1
        Loop

        '=========================================================================================
        ' Read "Time at RPM" Table
        '=========================================================================================
        Debug.Status("Time at RPM")
        i = 0
        Do While i < 7
            data = Convert_DHMS(ELM.ReadPid(&H22, &H309 + i))
            DataGridView.Rows.Add(New String() {"Time at RPM " & (i + 1).ToString, Time_at_RPM_Categories(i) & " - " & Time_at_RPM_Categories(i + 1) & " RPM", data, "Time (D H:M:S)"})
            i = i + 1
        Loop
        data = Convert_DHMS(ELM.ReadPid(&H22, &H309 + i))
        DataGridView.Rows.Add(New String() {"Time at RPM " & (i + 1).ToString, Time_at_RPM_Categories(i + 1), data, "Time (D H:M:S)"})

        '=========================================================================================
        ' Read "Time at SPEED" Table
        '=========================================================================================
        Debug.Status("Time at SPEED")
        i = 0
        Do While i < 7
            data = Convert_DHMS(ELM.ReadPid(&H22, &H311 + i))
            DataGridView.Rows.Add(New String() {"Time at KMH " & (i + 1).ToString, Time_at_KMH_Categories(i) & " - " & Time_at_KMH_Categories(i + 1) & " KMH", data, "Time (D H:M:S)"})
            i = i + 1
        Loop
        data = Convert_DHMS(ELM.ReadPid(&H22, &H311 + i))
        DataGridView.Rows.Add(New String() {"Time at KMH " & (i + 1).ToString, Time_at_KMH_Categories(i + 1), data, "Time (D H:M:S)"})

        '=========================================================================================
        ' Read "Time at Coolant Temp" Table
        '=========================================================================================
        Debug.Status("Time at Coolant Temp")
        i = 0
        Do While i < 3
            data = Convert_DHMS(ELM.ReadPid(&H22, &H31A + i))
            DataGridView.Rows.Add(New String() {"Time at Coolant Temp " & (i + 1).ToString, Time_at_TEMP_Categories(i) & " - " & Time_at_TEMP_Categories(i + 1) & " Deg C", data, "Time (D H:M:S)"})
            i = i + 1
        Loop
        data = Convert_DHMS(ELM.ReadPid(&H22, &H31A + i))
        DataGridView.Rows.Add(New String() {"Time at Coolant Temp " & (i + 1).ToString, Time_at_TEMP_Categories(i + 1), data, "Time (D H:M:S)"})

        '=========================================================================================
        ' Read "Max Engine Speed / RPM" Table
        '=========================================================================================
        Debug.Status("MAX Engine Speed")
        data = Convert_RPM(ELM.ReadPid(&H22, &H323))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 5", "", data, "RPM"})
        data = Convert_RPM(ELM.ReadPid(&H22, &H322))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 4", "", data, "RPM"})
        data = Convert_RPM(ELM.ReadPid(&H22, &H321))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 3", "", data, "RPM"})
        data = Convert_RPM(ELM.ReadPid(&H22, &H31F))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 2", "", data, "RPM"})
        data = Convert_RPM(ELM.ReadPid(&H22, &H31E))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 1", "", data, "RPM"})
        '=========================================================================================
        ' Read "Max Engine Speed / Coolant Temp" Table
        '=========================================================================================
        Debug.Status("MAX Engine Speed/Coolant Temp")
        data = Convert_DEGC(ELM.ReadPid(&H22, &H32C))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 5", "Coolant Temp", data, "Deg C"})
        data = Convert_DEGC(ELM.ReadPid(&H22, &H32A))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 4", "Coolant Temp", data, "Deg C"})
        data = Convert_DEGC(ELM.ReadPid(&H22, &H328))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 3", "Coolant Temp", data, "Deg C"})
        data = Convert_DEGC(ELM.ReadPid(&H22, &H326))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 2", "Coolant Temp", data, "Deg C"})
        data = Convert_DEGC(ELM.ReadPid(&H22, &H324))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 1", "Coolant Temp", data, "Deg C"})
        '=========================================================================================
        ' Read "Max Engine Speed / Engine Run Timer" Table
        '=========================================================================================
        Debug.Status("MAX Engine Speed/Engine Run Timer")
        data = Convert_DHMS(ELM.ReadPid(&H22, &H32E))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 5", "Engine Run Timer", data, "Time (D H:M:S)"})
        data = Convert_DHMS(ELM.ReadPid(&H22, &H32B))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 4", "Engine Run Timer", data, "Time (D H:M:S)"})
        data = Convert_DHMS(ELM.ReadPid(&H22, &H329))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 3", "Engine Run Timer", data, "Time (D H:M:S)"})
        data = Convert_DHMS(ELM.ReadPid(&H22, &H327))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 2", "Engine Run Timer", data, "Time (D H:M:S)"})
        data = Convert_DHMS(ELM.ReadPid(&H22, &H325))
        DataGridView.Rows.Add(New String() {"Max Engine Speed 1", "Engine Run Timer", data, "Time (D H:M:S)"})
        '=========================================================================================
        ' Read "Max Vehicle Speed" Table
        '=========================================================================================
        Debug.Status("MAX Vehicle Speed")
        data = Convert_KMH(ELM.ReadPid(&H22, &H333))
        DataGridView.Rows.Add(New String() {"Max Vehicle Speed 5", "", data, "KMH"})
        data = Convert_KMH(ELM.ReadPid(&H22, &H332))
        DataGridView.Rows.Add(New String() {"Max Vehicle Speed 4", "", data, "KMH"})
        data = Convert_KMH(ELM.ReadPid(&H22, &H331))
        DataGridView.Rows.Add(New String() {"Max Vehicle Speed 3", "", data, "KMH"})
        data = Convert_KMH(ELM.ReadPid(&H22, &H330))
        DataGridView.Rows.Add(New String() {"Max Vehicle Speed 2", "", data, "KMH"})
        data = Convert_KMH(ELM.ReadPid(&H22, &H32F))
        DataGridView.Rows.Add(New String() {"Max Vehicle Speed 1", "", data, "KMH"})
        '=========================================================================================
        ' Read "Fastest Standing Start" Table
        '=========================================================================================
        Debug.Status("Fastest Standing Start 0-100")
        data = Convert_SEC(ELM.ReadPid(&H22, &H334))
        DataGridView.Rows.Add(New String() {"Fastest Standing Start", "0 - 100 KMH", data, "SEC"})
        Debug.Status("Fastest Standing Start 0-160")
        data = Convert_SEC(ELM.ReadPid(&H22, &H335))
        DataGridView.Rows.Add(New String() {"Fastest Standing Start", "0 - 160 KMH", data, "SEC"})
        '=========================================================================================
        ' Read "Last Standing Start" Table
        '=========================================================================================
        Debug.Status("Last Standing Start 0-100")
        data = Convert_SEC(ELM.ReadPid(&H22, &H336))
        DataGridView.Rows.Add(New String() {"Last Standing Start", "0 - 100 KMH", data, "SEC"})
        Debug.Status("Last Standing Start 0-160")
        data = Convert_SEC(ELM.ReadPid(&H22, &H337))
        DataGridView.Rows.Add(New String() {"Last Standing Start", "0 - 160 KMH", data, "SEC"})
        '=========================================================================================
        ' Read "Engine Run Timer" 
        '=========================================================================================
        Debug.Status("Engine Run Timer (overall)")
        data = Convert_DHMS(ELM.ReadPid(&H22, &H338))
        DataGridView.Rows.Add(New String() {"Engine Run Timer", "Total", data, "Time (D H:M:S)"})
        '=========================================================================================
        ' Read "Number of Standing Starts" 
        '=========================================================================================
        Debug.Status("Number of Standing Starts")
        data = Convert_COUNT(ELM.ReadPid(&H22, &H339))
        DataGridView.Rows.Add(New String() {"Number of Standing Starts", "", data, "#"})

        'If COMPort IsNot Nothing Then
        '    COMPort.Close()
        'End If
        PrintToolStripMenuItem.Enabled = True
        SaveAsHTMLToolStripMenuItem.Enabled = True

    End Function
    Private Sub ReadDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadDataToolStripMenuItem.Click

        ' clear datagrid
        DataGridView.Rows.Clear()
        ToolStripProgressBar.Value = 0

        Try
            DownloadPerformanceData()
        Catch Exc As System.Exception
            MsgBox(Exc.Message)
        End Try

        ' auto resize grid
        ' make non sortable
        For j As Integer = 0 To DataGridView.ColumnCount - 1
            DataGridView.Columns(j).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DataGridView.Columns(j).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        'DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        ' update tooltip status
        ToolStripStatusLabel.Text = "Download completed"
        ToolStripProgressBar.Value = 0
    End Sub
    Function Convert_HMS(ByVal data As String)
        Dim DataBytes() As String = Split(data)
        Dim hex_string As String
        Dim val As Integer
        Dim seconds As Integer

        hex_string = DataBytes(3) & DataBytes(4) & DataBytes(5) & DataBytes(6)
        val = Convert.ToInt32(hex_string, 16)
        seconds = val / 10
        Dim ts As TimeSpan = TimeSpan.FromSeconds(seconds)
        Dim hms As DateTime = New DateTime(ts.Ticks)
        Return (hms.ToString("HH:mm:ss"))
    End Function

    Function Convert_DHMS(ByVal data As String)
        Dim DataBytes() As String = Split(data)
        Dim hex_string As String
        Dim val As Integer
        Dim seconds As Integer
        Dim days As Integer

        hex_string = DataBytes(3) & DataBytes(4) & DataBytes(5) & DataBytes(6)
        val = Convert.ToInt32(hex_string, 16)
        seconds = val / 10
        ' get days
        days = seconds \ 84600
        ' get remaining seconds within a day
        seconds = seconds - (days * 84600)
        Dim ts As TimeSpan = TimeSpan.FromSeconds(seconds)
        Dim hms As DateTime = New DateTime(ts.Ticks)
        Return (days & " " & hms.ToString("HH:mm:ss"))
    End Function

    Function Convert_RPM(ByVal data As String)
        Dim DataBytes() As String = Split(data)
        Dim hex_string As String
        Dim val As Integer

        hex_string = DataBytes(3) & DataBytes(4) & DataBytes(5) & DataBytes(6)
        val = Convert.ToInt32(hex_string, 16)
        ' Return (data & " = " & val.tostring)
        Return (val.ToString)
    End Function


    Function Convert_DEGC(ByVal data As String)
        Dim DataBytes() As String = Split(data)
        Dim hex_string As String
        Dim val As Double

        hex_string = DataBytes(3) & DataBytes(4) & DataBytes(5) & DataBytes(6)
        val = GetCelsiusForFahrenheit(Convert.ToInt32(hex_string, 16) - 14)
        ' Return (data & " = " & val.tostring)
        Return (val.ToString("F1"))
    End Function

    Function Convert_KMH(ByVal data As String)
        Dim DataBytes() As String = Split(data)
        Dim hex_string As String
        Dim val As Integer

        hex_string = DataBytes(3)
        val = Convert.ToInt32(hex_string, 16)
        ' Return (data & " = " & val.tostring)
        Return (val.ToString)
    End Function

    Function Convert_SEC(ByVal data As String)
        Dim DataBytes() As String = Split(data)
        Dim hex_string As String
        Dim val As Double

        hex_string = DataBytes(3)
        val = Convert.ToInt32(hex_string, 16)
        val = val / 10
        ' Return (data & " = " & val.tostring)
        Return (val.ToString("F1"))
    End Function

    Function Convert_COUNT(ByVal data As String)
        Dim DataBytes() As String = Split(data)
        Dim hex_string As String
        Dim val As Integer

        hex_string = DataBytes(3) & DataBytes(4)
        val = Convert.ToInt32(hex_string, 16)
        ' Return (data & " = " & val.tostring)
        Return (val.ToString)
    End Function


    Private Sub DataGridView_RowsAdded(ByVal sender As Object, ByVal e As DataGridViewRowsAddedEventArgs) Handles DataGridView.RowsAdded
        ' update progress counter
        ' 54 rows are performance data, any other stuff is initRowcount 
        Dim expected As Integer = 54 + initRowcount
        Dim progress As Integer = 0

        progress = (DataGridView.RowCount / expected) * 100
        ToolStripProgressBar.Value = progress
        ' autoscroll
        DataGridView.FirstDisplayedCell = DataGridView.Item(0, DataGridView.RowCount - 1)
        Application.DoEvents()
    End Sub

    Private Sub PrintDocument_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument.BeginPrint
        Try

            strFormat = New StringFormat()
            strFormat.Alignment = StringAlignment.Near
            strFormat.LineAlignment = StringAlignment.Center
            strFormat.Trimming = StringTrimming.EllipsisCharacter

            arrColumnLefts.Clear()
            arrColumnWidths.Clear()
            iCellHeight = 0
            iCount = 0
            bfirstPage = True
            bnewPage = True

            'Calculating Total Widths
            iTotalWidth = 0
            For Each GridCol As DataGridViewColumn In DataGridView.Columns
                iTotalWidth += GridCol.Width
            Next
        

        Catch Exc As System.Exception

            MessageBox.Show(Exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

  

    Private Sub PrintDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument.PrintPage

        Dim iLeftMargin As Integer = e.MarginBounds.Left
        Dim iTopMargin As Integer = e.MarginBounds.Top
        Dim bMorePagesToPrint As Boolean = False
        Dim iTmpWidth As Integer = 0

        Try
            'For the first page to print set the cell width and header height
            If (bfirstPage) Then
                For Each GridCol As DataGridViewColumn In DataGridView.Columns
                    iTmpWidth = (Math.Floor(GridCol.Width / iTotalWidth * iTotalWidth * (e.MarginBounds.Width / iTotalWidth)))

                    iHeaderHeight = (e.Graphics.MeasureString(GridCol.HeaderText, GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11
                    ' Save width and height of headers

                    arrColumnLefts.Add(iLeftMargin)
                    arrColumnWidths.Add(iTmpWidth)
                    iLeftMargin += iTmpWidth

                Next
            End If


            ' Loop till all the grid rows not get printed
            While (iRow <= DataGridView.Rows.Count - 1)

                Dim GridRow As DataGridViewRow = DataGridView.Rows(iRow)
                ' Set the cell height
                iCellHeight = GridRow.Height + 5
                Dim iCount As Integer = 0
                ' Check whether the current page settings allows more rows to print
                If (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top) Then
                    bnewPage = True
                    bfirstPage = False
                    bMorePagesToPrint = True
                    Exit While
                Else

                    If (bnewPage) Then
                        ' Draw Header                        
                        e.Graphics.DrawString(header_text, New Font(DataGridView.Font, FontStyle.Bold), Brushes.Black, _
                        e.MarginBounds.Left, _
                        e.MarginBounds.Top - e.Graphics.MeasureString(header_text, New Font(DataGridView.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13)
                        Dim strDate As String = DateTime.Now.ToLongDateString() & " " & DateTime.Now.ToShortTimeString()

                        ' Draw Date
                        e.Graphics.DrawString(strDate, _
                        New Font(DataGridView.Font, FontStyle.Bold), Brushes.Black, _
                        e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate, New Font(DataGridView.Font, FontStyle.Bold), e.MarginBounds.Width).Width), _
                        e.MarginBounds.Top - e.Graphics.MeasureString(header_text, New Font(New Font(DataGridView.Font, FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13)

                        ' Draw Copyright
                        e.Graphics.DrawString(copyright_text, _
                        New Font(DataGridView.Font, FontStyle.Bold), Brushes.Gray, _
                        e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(copyright_text, New Font(DataGridView.Font, FontStyle.Bold), e.MarginBounds.Width).Width), _
                        e.MarginBounds.Top - e.Graphics.MeasureString(header_text, New Font(New Font(DataGridView.Font, FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height * 3 - 13)

                        ' Draw Columns                 
                        iTopMargin = e.MarginBounds.Top
                        For Each GridCol As DataGridViewColumn In DataGridView.Columns

                            e.Graphics.FillRectangle(New SolidBrush(Color.LightGray), _
                                New Rectangle(arrColumnLefts(iCount), iTopMargin, _
                                arrColumnWidths(iCount), iHeaderHeight))

                            e.Graphics.DrawRectangle(Pens.Black, _
                                New Rectangle(arrColumnLefts(iCount), iTopMargin, _
                                arrColumnWidths(iCount), iHeaderHeight))

                            e.Graphics.DrawString(GridCol.HeaderText, _
                                GridCol.InheritedStyle.Font, _
                                New SolidBrush(GridCol.InheritedStyle.ForeColor), _
                                New RectangleF(arrColumnLefts(iCount), iTopMargin, _
                                arrColumnWidths(iCount), iHeaderHeight), strFormat)
                            iCount = iCount + 1
                        Next
                        bnewPage = False
                        iTopMargin += iHeaderHeight
                    End If
                    iCount = 0
                    ' Draw Columns Contents                
                    For Each Cel As DataGridViewCell In GridRow.Cells

                        If Cel.Value <> Nothing Then

                            e.Graphics.DrawString(Cel.Value.ToString(), _
                                Cel.InheritedStyle.Font, _
                                New SolidBrush(Cel.InheritedStyle.ForeColor), _
                                New RectangleF(arrColumnLefts(iCount), _
                                iTopMargin, _
                                arrColumnWidths(iCount), iCellHeight), _
                                strFormat)
                        End If
                        ' Drawing Cells Borders 
                        e.Graphics.DrawRectangle(Pens.Black, _
                        New Rectangle(arrColumnLefts(iCount), iTopMargin, _
                        arrColumnWidths(iCount), iCellHeight))
                        iCount = iCount + 1
                    Next
                End If
                iRow = iRow + 1
                iTopMargin += iCellHeight                
            End While
            ' If more lines exist, print another page.
            If (bMorePagesToPrint) Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
            End If

        Catch Exc As System.Exception
            'Dim st As New StackTrace(True)
            'st = New StackTrace(Exc, True)
            'MessageBox.Show("Line: " & st.GetFrame(0).GetFileLineNumber().ToString, "Error")
            MessageBox.Show(Exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Dim objPPdialog As PrintPreviewDialogSelectPrinter = New PrintPreviewDialogSelectPrinter()
        objPPdialog.Document = PrintDocument
        objPPdialog.ShowDialog()
    End Sub

    Private Sub SaveAsHTMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsHTMLToolStripMenuItem.Click
        Dim html As String
        Dim strDate As String = DateTime.Now.ToLongDateString() & " " & DateTime.Now.ToShortTimeString()

        html = "<HTML>"
        html &= "<FONT FACE=Arial SIZE=5><B>" & header_text & "</B></FONT><P>" & vbCrLf
        html &= "<FONT FACE=Arial SIZE=4><B>" & strDate & "</B></FONT><P>" & vbCrLf
        html &= "<FONT FACE=Arial SIZE=3>" & vbCrLf
        html &= "<TABLE CELLSPACING=4 CELLPADDING=4 BORDER=1><TR>"
        For Each GridCol As DataGridViewColumn In DataGridView.Columns
            html &= "<TH>" & GridCol.HeaderText & "</TH>"
        Next
        html &= "</TR>" & vbCrLf

        iRow = 0
        While (iRow <= DataGridView.Rows.Count - 1)
            Dim GridRow As DataGridViewRow = DataGridView.Rows(iRow)
            html &= "<TR>"
            For Each Cel As DataGridViewCell In GridRow.Cells
                If Cel.Value <> Nothing Then
                    html &= "<TD>" & Cel.Value.ToString() & "</TD>"
                Else
                    html &= "<TD></TD>"
                End If
            Next
            html &= "</TR>" & vbCrLf
            iRow += 1
        End While

        html &= "</TABLE></FONT>" & vbCrLf
        html &= "<FONT FACE=Arial SIZE=1><B>" & copyright_text & "</B></FONT><P>" & vbCrLf
        html &= "</HTML>"
        SaveFileDialogHTML.Filter = "HTML Files (*.html)|*.html"
        If (SaveFileDialogHTML.ShowDialog = Windows.Forms.DialogResult.OK) Then
            My.Computer.FileSystem.WriteAllText(SaveFileDialogHTML.FileName, html, False)
        End If
    End Sub

    Function GetCelsiusForFahrenheit(ByVal fahrenheit As Double) As Double
        Return (5 / 9) * (fahrenheit - 32)
    End Function
End Class


