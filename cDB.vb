Option Explicit On
Option Strict On

Public Class cDB

    <ComponentModel.Browsable(False)>
    Public WithEvents GPSInput As New cGPSInput

    Public NMEAParser As New cNMEAParser

    Public TCP_ConnectionListener As Net.Sockets.TcpListener
    Public TCPServer As New cTCPServer
    Public UDPTalker As cUDPTalker = Nothing

    <ComponentModel.Browsable(False)>
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    <ComponentModel.Category("1. GPS hardware")>
    <ComponentModel.DisplayName("a) COM port")>
    <ComponentModel.Description("COM port the physical GPS receiver is connected to")>
    <ComponentModel.DefaultValue("COM5")>
    Public Property COMPort As String = "COM5"

    <ComponentModel.Category("1. GPS hardware")>
    <ComponentModel.DisplayName("b) COM port speed")>
    <ComponentModel.Description("COM port speed")>
    <ComponentModel.DefaultValue(115200)>
    Public Property COMSpeed As Integer = 115200

    <ComponentModel.Category("2. GPS network transmission")>
    <ComponentModel.DisplayName("a) TCP port")>
    <ComponentModel.Description("TCP port to transmit NMEA messages")>
    <ComponentModel.DefaultValue(4987)>
    Public Property Network_TCP As Integer = 4987

    <ComponentModel.Category("2. GPS network transmission")>
    <ComponentModel.DisplayName("a) UDP port")>
    <ComponentModel.Description("UDP port to transmit NMEA messages")>
    <ComponentModel.DefaultValue(9745)>
    Public Property Network_UDP As Integer = 9745

    <ComponentModel.Category("3. GPS message logging")>
    <ComponentModel.DisplayName("a) LOG file")>
    <ComponentModel.Description("GPS message log file to generate")>
    <ComponentModel.DefaultValue("NMEAAll.log")>
    Public Property LogFileName As String = "NMEAAll.log"

    <ComponentModel.Category("4. Map behaviour")>
    <ComponentModel.DisplayName("a) LOG file")>
    <ComponentModel.Description("Auto center map on position update")>
    <ComponentModel.DefaultValue(True)>
    Public Property AutoCenterMap As Boolean = True

    Private Sub ProcessNMEAMessage(ByVal Message As String) Handles GPSInput.NewMessage
        NMEAParser.Process(Message)
        SendNMEAViaTCP(Message)
        If IsNothing(UDPTalker) Then UDPTalker = New cUDPTalker(Network_UDP)
        UDPTalker.Send(Message)
    End Sub

    Public Sub SendNMEAViaTCP(ByVal Message As String)
        If IsNothing(TCPServer.Client) = False Then
            Message &= GPSInput.StopChars
            Dim data As [Byte]() = System.Text.Encoding.ASCII.GetBytes(Message)
            If TCPServer.Client.Client.Connected Then
                TCPServer.Client.GetStream.Write(data, 0, data.Length)
            End If
            'DB.TCPServer.Client.Close()
        End If
    End Sub

End Class
