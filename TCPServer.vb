Option Explicit On
Option Strict On

Public Class cTCPServer

    Private tcpClientConnected As New Threading.ManualResetEvent(False)
    Public Client As Net.Sockets.TcpClient = Nothing


    'Accept one client connection asynchronously.
    Public Sub DoBeginAcceptTcpClient(listener As Net.Sockets.TcpListener)

        'Set the event to nonsignaled state.
        tcpClientConnected.Reset()

        'Start to listen for connections from a client.
        Console.WriteLine("Waiting for a connection...")

        'Accept the connection. 
        'BeginAcceptSocket() creates the accepted socket.
        listener.BeginAcceptTcpClient(New AsyncCallback(AddressOf DoAcceptTcpClientCallback), listener)

        'Wait until a connection is made and processed before continuing.
        tcpClientConnected.WaitOne()

    End Sub

    ' Process the client connection.
    Private Sub DoAcceptTcpClientCallback(ar As IAsyncResult)

        'Get the listener that handles the client request.
        Dim listener As Net.Sockets.TcpListener = CType(ar.AsyncState, Net.Sockets.TcpListener)

        'End the operation and display the received data on the console.
        Client = listener.EndAcceptTcpClient(ar)

        'Process the connection here. (Add the client to a server table, read data, etc.)
        Console.WriteLine("Client connected completed")

        'Signal the calling thread to continue.
        tcpClientConnected.Set()

    End Sub

    Public Function IsListening(ByRef Listener As Net.Sockets.TcpListener) As Boolean

    End Function

End Class

Public Class cUDPTalker

    Dim UDPSender As Net.Sockets.UdpClient = Nothing
    Dim GroupEP As Net.IPEndPoint

    Public Sub New(ByVal Port As Integer)
        Dim GroupIP As Net.IPAddress = Net.IPAddress.Parse("127.0.0.1")
        GroupEP = New Net.IPEndPoint(GroupIP, Port)
        UDPSender = New Net.Sockets.UdpClient
        UDPSender.EnableBroadcast = True
        'UDPSender.JoinMulticastGroup(GroupIP, 12)
    End Sub

    Public Sub Send(ByVal Message As String)
        Dim data As [Byte]() = System.Text.Encoding.ASCII.GetBytes(Message)
        Dim RetVal As Integer = UDPSender.Send(data, data.Length, GroupEP)
        'send1.Close()

    End Sub

End Class