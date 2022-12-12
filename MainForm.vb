Option Explicit On
Option Strict On
Imports System.Device.Location

Public Class MainForm

    'https://github.com/AlexanderKrutov/WinFormsMapControl

    Private WithEvents DB As New cDB

    Private WithEvents watcher As Device.Location.GeoCoordinateWatcher

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mpMap.CacheFolder = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MapControl")
        mpMap.TileServer = New OpenTopoMapServer
        mpMap.Markers.Add(GetMarker)

        watcher = New Device.Location.GeoCoordinateWatcher
        AddHandler watcher.StatusChanged, AddressOf Watcher_StatusChanged
        watcher.Start()

        pgMain.SelectedObject = DB

    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Dim RetVal As String = DB.GPSInput.Init(DB.COMPort, DB.COMSpeed)
        If String.IsNullOrEmpty(RetVal) = False Then MsgBox(RetVal, MsgBoxStyle.Critical, "ERROR")
    End Sub

    Private Sub tGUIUpdate_Tick(sender As Object, e As EventArgs) Handles tGUIUpdate.Tick
        Dim RetVal As New List(Of String)
        For Each Entry As String In DB.NMEAParser.Header.Keys
            RetVal.Add(Entry.PadRight(5) & ":" & DB.NMEAParser.Header(Entry).ToString.Trim.PadLeft(6))
        Next Entry
        RetVal.Add("INVALID: " & DB.NMEAParser.Invalid.Count.ToString.Trim)
        RetVal.Add("Missed adds: " & DB.NMEAParser.ConcurrentError.ToString.Trim)
        tbNMEAMessages.Text = Join(RetVal.ToArray, System.Environment.NewLine)

        'Update table
        dgvMain.Rows.Clear()
        dgvMain.Rows.Add(New String() {"Fix", DB.NMEAParser.Fix})
        dgvMain.Rows.Add(New String() {"UTC", DB.NMEAParser.UTC.ToString})
        dgvMain.Rows.Add(New String() {"Sats used", DB.NMEAParser.UsedStat.ToString})
        dgvMain.Rows.Add(New String() {"SVs total", DB.NMEAParser.AllSVs.Count.ToString})
        dgvMain.Rows.Add(New String() {"Latitude", DB.NMEAParser.Latitude.ToString})
        dgvMain.Rows.Add(New String() {"Longitude", DB.NMEAParser.Longitude.ToString})
        dgvMain.Rows.Add(New String() {"Height", DB.NMEAParser.Height.ToString})
        dgvMain.Rows.Add(New String() {"HDOP", DB.NMEAParser.HDOP.ToString})
        dgvMain.Rows.Add(New String() {"Speed", DB.NMEAParser.Speed.ToString})
        dgvMain.Rows.Add(New String() {"Course", DB.NMEAParser.Course.ToString})

        Dim Pos As New GeoPoint(CSng(DB.NMEAParser.Longitude), CSng(DB.NMEAParser.Latitude))
        mpMap.Markers(0).Point = Pos
        If DB.AutoCenterMap = True Then mpMap.Center = Pos

        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        DB.GPSInput.Stop()
    End Sub

    Private Sub btnStoreMessages_Click(sender As Object, e As EventArgs) Handles btnStoreMessages.Click
        Dim DumpFile As String = System.IO.Path.Combine(DB.MyPath, DB.LogFileName)
        System.IO.File.WriteAllLines(DumpFile, DB.NMEAParser.AllMessages.ToArray)
        Process.Start(DumpFile)
    End Sub

    Private Sub btnZoomIn_Click(sender As Object, e As EventArgs) Handles btnZoomIn.Click
        mpMap.ZoomLevel += 1
    End Sub

    Private Sub btnZoomOut_Click(sender As Object, e As EventArgs) Handles btnZoomOut.Click
        mpMap.ZoomLevel -= 1
    End Sub

    Private Function GetMarker() As Marker
        Dim MyBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Red)
        Dim HereStyle As New MarkerStyle(10, MyBrush, New System.Drawing.Pen(MyBrush), MyBrush, New Font("Courier New", 10), StringFormat.GenericDefault)
        Return New Marker(New GeoPoint(0, 0), HereStyle, "HERE")
    End Function

    Private Sub tUpdateTCP_Tick(sender As Object, e As EventArgs) Handles tUpdateTCP.Tick
        'DB.SendNMEAViaTCP("$GNRMC,092548.500,A,4752.702033,N,01141.486644,E,0.13,230.34,050721,,,A*")
    End Sub

    Private Sub btnStartTCP_Click(sender As Object, e As EventArgs) Handles btnStartTCP.Click
        DB.TCP_ConnectionListener = New Net.Sockets.TcpListener(Net.IPAddress.Loopback, DB.Network_TCP)
        DB.TCP_ConnectionListener.Start()
        DB.TCPServer.DoBeginAcceptTcpClient(DB.TCP_ConnectionListener)
    End Sub

    Private Sub tsmiFile_End_Click(sender As Object, e As EventArgs) Handles tsmiFile_End.Click
        End
    End Sub

    Private Sub tsmiFile_EXELocation_Click(sender As Object, e As EventArgs) Handles tsmiFile_EXELocation.Click
        Diagnostics.Process.Start(DB.MyPath)
    End Sub

    Private Sub watcher_StatusChanged(sender As Object, e As GeoPositionStatusChangedEventArgs) Handles watcher.StatusChanged
        Select Case e.Status
            Case Device.Location.GeoPositionStatus.Disabled
                tsslWinPosition.Text = "Pos: DISABLED"
            Case Device.Location.GeoPositionStatus.Initializing
                tsslWinPosition.Text = "Pos: Initializing ..."
            Case Device.Location.GeoPositionStatus.NoData
                tsslWinPosition.Text = "Pos: ??? (no data)"
            Case Device.Location.GeoPositionStatus.Ready
                'Using Pos As Device.Location.GeoCoordinateWatcher = CType(sender, Device.Location.GeoCoordinateWatcher)
                '    If Pos.Position.Location.IsUnknown Then
                '        tsslWinPosition.Text = "Pos: UNKNOWN"
                '    Else
                '        tsslWinPosition.Text = "Pos: " & Pos.Position.Location.Latitude.ToString.Trim & ":" & Pos.Position.Location.Longitude.ToString.Trim & ", " & Pos.Position.Location.HorizontalAccuracy.ToString.Trim & " Acu"
                '    End If
                'End Using
        End Select
    End Sub

    Private Sub watcher_PositionChanged(sender As Object, e As GeoPositionChangedEventArgs(Of GeoCoordinate)) Handles watcher.PositionChanged
        If e.Position.Location.IsUnknown Then
            tsslWinPosition.Text = "Pos: UNKNOWN"
        Else
            tsslWinPosition.Text = "Pos: " & e.Position.Location.Latitude.ToString.Trim & ":" & e.Position.Location.Longitude.ToString.Trim & ", " & e.Position.Location.HorizontalAccuracy.ToString.Trim & " Acu"
        End If
    End Sub

End Class
