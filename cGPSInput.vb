Option Explicit On
Option Strict On

Public Class cGPSInput

    Public Event NewMessage(ByVal Text As String)

    Dim COM_port As IO.Ports.SerialPort
    Dim ReadBuffer As String = String.Empty

    Public StopChars As String = Chr(13) & Chr(10)

    Public Function Init(ByVal SerialPort As String, ByVal Speed As Integer) As String

        Dim InitCOMNow As Boolean = False
        If IsNothing(COM_port) = True Then
            COM_port = New IO.Ports.SerialPort
            InitCOMNow = True
        Else
            If COM_port.IsOpen = False Then InitCOMNow = True
        End If

        If InitCOMNow And (String.IsNullOrEmpty(SerialPort) = False) Then
            COM_port = New IO.Ports.SerialPort(SerialPort, Speed, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
            COM_port.ReadTimeout = 1000
            COM_port.Handshake = IO.Ports.Handshake.None
            AddHandler COM_port.DataReceived, New IO.Ports.SerialDataReceivedEventHandler(AddressOf COM_port_DataReceived)
            Try
                COM_port.Open()
            Catch ex As Exception
                Return ex.Message
            End Try
        End If

        Return String.Empty

    End Function

    Public Sub [Stop]()
        Try
            COM_port.Close()
        Catch ex As Exception
            'COM port not available ...
        End Try
    End Sub

    Private Sub COM_port_DataReceived(ByVal sender As Object, e As IO.Ports.SerialDataReceivedEventArgs)
        'Read all data and fill buffer
        Dim str As String = CType(sender, IO.Ports.SerialPort).ReadExisting
        For i As Integer = 0 To str.Length - 1
            ReadBuffer &= str.Chars(i)
        Next i
        'Split all messages
        Do
            Dim NextEnd As Integer = ReadBuffer.IndexOf(StopChars)
            If NextEnd > 0 Then
                Dim NewMessage As String = ReadBuffer.Substring(0, NextEnd - StopChars.Length)
                ReadBuffer = ReadBuffer.Substring(NextEnd + StopChars.Length)
                RaiseEvent NewMessage(NewMessage)
            Else
                Exit Do
            End If
        Loop
    End Sub

End Class
