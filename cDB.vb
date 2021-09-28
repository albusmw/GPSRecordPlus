Option Explicit On
Option Strict On

Public Class cDB

    Public WithEvents GPSInput As New cGPSInput
    Public NMEAParser As New cNMEAParser
    Public ReadOnly Property MyPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)

    Public TCP_ConnectionListener As Net.Sockets.TcpListener
    Public TCPServer As New cTCPServer
    Public UDPTalker As cUDPTalker = Nothing

    Public Property LogFileName As String = "NMEAAll.log"

    Public Property COMPort As String = "COM3"
    Public Property COMSpeed As Integer = 115200

    Public Property Network_TCP As Integer = 4987
    Public Property Network_UDP As Integer = 9745

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
