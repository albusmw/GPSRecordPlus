Option Explicit On
Option Strict On

Public Class cNMEAParser

    Public Enum eSVType
        Unknown
        GPS
        GLONASS
        Galileo
        BeiDou
    End Enum

    Public Structure sGSVInfo
        Public SV_Type As eSVType
        Public SV_PRN As Integer
        Public Elevation As Integer
        Public Azimuth As Integer
        Public SNR As Integer
    End Structure

    '''<summary>All received headers and the count of them.</summary>
    Public Header As New Concurrent.ConcurrentDictionary(Of String, Integer)
    '''<summary>All invalid headers.</summary>
    Public Invalid As New List(Of String)
    '''<summary>A list of all received messages.</summary>
    Public AllMessages As New List(Of String)
    '''<summary>All invalid headers.</summary>
    Public ConcurrentError As Integer = 0

    '''<summary>Buffer for all GSV messages.</summary>
    Public AllSVs As New List(Of sGSVInfo)

    '''<summary>Typ of fix.</summary>
    Public ReadOnly Property Fix As String
        Get
            Return MyFix
        End Get
    End Property
    Private MyFix As String = String.Empty

    '''<summary>UTC Time.</summary>
    Public ReadOnly Property UTC As TimeSpan
        Get
            Return MyUTC
        End Get
    End Property
    Private MyUTC As TimeSpan = TimeSpan.Zero

    '''<summary>Latitude.</summary>
    Public ReadOnly Property Latitude As Double
        Get
            Return MyLatitude
        End Get
    End Property
    Private MyLatitude As Double = Double.NaN

    ''<summary>Latitude.</summary>
    Public ReadOnly Property Longitude As Double
        Get
            Return MyLongitude
        End Get
    End Property
    Private MyLongitude As Double = Double.NaN

    ''<summary>Height.</summary>
    Public ReadOnly Property Height As Double
        Get
            Return MyHeight
        End Get
    End Property
    Private MyHeight As Double = Double.NaN

    '''<summary>Used sats for solution.</summary>
    Public ReadOnly Property UsedStat As Integer
        Get
            Return MyUsedStat
        End Get
    End Property
    Private MyUsedStat As Integer = -1

    '''<summary>Horizontal DOP.</summary>
    Public ReadOnly Property HDOP As Double
        Get
            Return MyHDOP
        End Get
    End Property
    Private MyHDOP As Double = Double.NaN

    '''<summary>Speed [kmh].</summary>
    Public ReadOnly Property Speed As Double
        Get
            Return MySpeed
        End Get
    End Property
    Private MySpeed As Double = Double.NaN

    '''<summary>Course.</summary>
    Public ReadOnly Property Course As Double
        Get
            Return MyCourse
        End Get
    End Property
    Private MyCourse As Double = Double.NaN

    Public Sub Process(ByVal Message As String)

        AllMessages.Add(Message)
        If Message.StartsWith("$") Then
            'Get message, split and store type
            Message = Message.Substring(1)
            Dim AllSplit As String() = Split(Message, ",")
            Dim NMEAType As String = AllSplit(0)
            Dim TalkerID As String = NMEAType.Substring(0, 2)
            Dim DataSetID As String = NMEAType.Substring(2)
            If Header.ContainsKey(NMEAType) = False Then
                If Header.TryAdd(NMEAType, 1) = False Then ConcurrentError += 1
            Else
                Header(NMEAType) += 1
            End If
            'Parse message
            Select Case DataSetID
                Case "GGA"
                    'Global Positioning System Fix Data
                    '$GPGGA,HHMMSS.ss,BBBB.BBBB,b,LLLLL.LLLL,l,Q,NN,D.D,H.H,h,G.G,g,A.A,RRRR*PP
                    '(1) HHMMSS.ss	            Uhrzeit des Position Fix (UTC)
                    '(2) BBBB.BBBB	            Breitengrad in Grad und Minuten (ddmm.mmmmmm)
                    '(3) b	                    Ausrichtung des Breitengrades (North=Norden oder South=Süden)
                    '(4) LLLLL.LLLL	            Längengrad in Grad und Minuten (dddmm.mmmmmm)
                    '(5) l	                    Ausrichtung des Längengrades (East=Osten oder West=Westen)
                    '(6) Q                      GPS-Qualität: 0 für ungültig, 1 für GPS fix, 2 für DGPS fix, 6 für geschätzt (nur bei NMEA-0183 ab Version 2.3)
                    '(7) NN	                    Anzahl der benutzten Satelliten (00 − 12)
                    '(8) D.D	                horizontale Abweichung (dilution of precision)
                    '(9) H.H	                Höhe der Antenne über Geoid oder MSL (mean sea level)
                    '(10) h	                    Einheit der Antennenhöhe (Meter)
                    '(11) G.G	                geoidal separation
                    '(12) g	                    Einheit der geoidal separation (Meter)
                    '(13) A.A	                Alter des DGPS-Datensatzes
                    '(14) RRRR	                DGPS-Referenzstation (0000 bis 1023)
                    MyUTC = New TimeSpan(0, CInt(AllSplit(1).Substring(0, 2)), CInt(AllSplit(1).Substring(2, 2)), CInt(AllSplit(1).Substring(4, 2)), CInt(AllSplit(1).Substring(7, 3)))
                    If AllSplit(2).Length > 0 Then MyLatitude = CInt(AllSplit(2).Substring(0, 2)) + (Val(AllSplit(2).Substring(2)) / 60)
                    If AllSplit(3) = "S" Then MyLatitude = -MyLatitude
                    If AllSplit(4).Length > 0 Then MyLongitude = CInt(AllSplit(4).Substring(0, 3)) + (Val(AllSplit(4).Substring(3)) / 60)
                    If AllSplit(5) = "W" Then MyLongitude = -MyLongitude
                    Select Case AllSplit(6).Trim
                        Case "0" : MyFix = "INVALID"
                        Case "1" : MyFix = "Fix - GPS"
                        Case "2" : MyFix = "Fix - D-GPS"
                        Case "6" : MyFix = "Estimated"
                        Case Else : MyFix = "??? <" & AllSplit(6).Trim & ">"
                    End Select
                    MyUsedStat = CInt(AllSplit(7))
                    MyHDOP = Val(AllSplit(8))
                    MyHeight = Val(AllSplit(9))
                Case "GSV"
                    'Satellite information
                    Dim SVType As eSVType = eSVType.Unknown
                    Select Case TalkerID
                        Case "GP" : SVType = eSVType.GPS
                        Case "GL" : SVType = eSVType.GLONASS
                        Case "GA" : SVType = eSVType.Galileo
                        Case "GB", "BD" : SVType = eSVType.BeiDou
                        Case Else : SVType = eSVType.Unknown
                    End Select
                    'Delete existing list
                    If CInt(AllSplit(2)) = 1 Then
                        Select Case SVType
                            Case eSVType.GPS
                                AllSVs.RemoveAll(AddressOf ClearGPS)
                            Case eSVType.GLONASS
                                AllSVs.RemoveAll(AddressOf ClearGLONASS)
                            Case eSVType.Galileo
                                AllSVs.RemoveAll(AddressOf ClearGalileo)
                            Case eSVType.BeiDou
                                AllSVs.RemoveAll(AddressOf ClearBeiDou)
                        End Select
                    End If
                    If AllSplit(3).Contains("*") = False Then
                        Dim Ptr As Integer = 0
                        Do
                            Ptr += 4
                            Dim Entry As New sGSVInfo
                            Entry.SV_Type = SVType
                            Entry.SV_PRN = GetSVInt(AllSplit(Ptr))
                            Entry.Elevation = GetSVInt(AllSplit(Ptr + 1))
                            Entry.Azimuth = GetSVInt(AllSplit(Ptr + 2))
                            Entry.SNR = GetSVInt(AllSplit(Ptr + 3))
                            AllSVs.Add(Entry)
                        Loop Until AllSplit(Ptr + 3).Contains("*")
                    End If
                Case "RMC"
                    MySpeed = Val(AllSplit(7)) * 1.852
                    MyCourse = Val(AllSplit(8))
            End Select

        Else
            Invalid.Add(Message)
        End If

    End Sub

    Private Function ClearGPS(obj As sGSVInfo) As Boolean
        If obj.SV_Type = eSVType.GPS Then Return True Else Return False
    End Function

    Private Function ClearGLONASS(obj As sGSVInfo) As Boolean
        If obj.SV_Type = eSVType.GLONASS Then Return True Else Return False
    End Function

    Private Function ClearGalileo(obj As sGSVInfo) As Boolean
        If obj.SV_Type = eSVType.Galileo Then Return True Else Return False
    End Function

    Private Function ClearBeiDou(obj As sGSVInfo) As Boolean
        If obj.SV_Type = eSVType.BeiDou Then Return True Else Return False
    End Function

    Private Function GetSVInt(ByVal Text As String) As Integer
        Text = Text.Replace("*", String.Empty)
        Dim RetVal As Integer = Integer.MinValue
        If Text.Length > 0 Then RetVal = CInt(Text)
        Return RetVal
    End Function

End Class
