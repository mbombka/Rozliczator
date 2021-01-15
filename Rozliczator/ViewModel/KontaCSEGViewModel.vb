Imports System.ComponentModel
Imports System.Data
Public Class KontaCSEGViewModel
    Implements INotifyPropertyChanged
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region
#Region "Properties"
    'set start of circle and radius
    Public Radius As Decimal = 100
    Public X0 As Decimal = 350
    Public Y0 As Decimal = 150

    Public TextLineLengh = 200
    'base offset from middle of arc to start of label
    Public TextLineYOffset = 30
    Public TextLineXOffset = 30
    Public ArcSizeWithLongerXYOffset = 0.1
    Public SmallArcLabelExtraOffset = 28
    'offset from middle of arc to start of label for each arc

    Dim TextLine2XOffset = TextLineXOffset
    Dim TextLine2YOffset = TextLineYOffset
    Dim TextLine3XOffset = TextLineXOffset
    Dim TextLine3YOffset = TextLineYOffset
    Dim TextLine4XOffset = TextLineXOffset
    Dim TextLine4YOffset = TextLineYOffset
    Dim TextLine5XOffset = TextLineXOffset
    Dim TextLine5YOffset = TextLineYOffset



    Private _KontaCSEGDataTable As DataTable
    Public Property KontaCSEGDataTable() As DataTable
        Get
            Return _KontaCSEGDataTable
        End Get

        Set(ByVal value As DataTable)
            _KontaCSEGDataTable = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontaCSEGDataTable"))
        End Set
    End Property

    Private _KontaCSEGDataView As DataView
    Public Property KontaCSEGDataView As DataView
        Get
            _KontaCSEGDataView = New DataView(KontaCSEGDataTable)
            Return _KontaCSEGDataView
        End Get
        Set(value As DataView)
            _KontaCSEGDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontaCSEGDataView"))
        End Set
    End Property

    Private Property _KontoCSEGHandle As KontaCSEG
    Public Property KontoCSEGHandle As KontaCSEG
        Get
            Dim tempRow = KontaCSEGDataTable.Rows.Item(KontaCSEGDataTable.Rows.Count - 1)
            _KontoCSEGHandle = DataBaseModel.FillKontaCSEG(tempRow)
            Return _KontoCSEGHandle
        End Get
        Set(value As KontaCSEG)
            _KontoCSEGHandle = value
            MajatekSpolkiPLN = MajatekSpolkiPLN ' update value on change
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontoCSEGHandle"))
        End Set
    End Property

    Private Property _MajatekSpolkiPLN As Decimal
    Public Property MajatekSpolkiPLN As Decimal
        Get
            _MajatekSpolkiPLN = KontoCSEGHandle.SubKontoSpolka + KontoCSEGHandle.SubKontoWspolnicy + KontoCSEGHandle.SubKontoVAT + KontoCSEGHandle.SubKontoCIT + KontoCSEGHandle.SubKontoPIT
            Return _MajatekSpolkiPLN
        End Get
        Set(value As Decimal)
            _MajatekSpolkiPLN = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("MajatekSpolkiPLN"))
        End Set
    End Property

    'temporary value for correct displaying pie chat
    Private Property _MajatekABS As Decimal
    Public Property MajatekABS As Decimal
        Get
            _MajatekABS = 0
            If KontoCSEGHandle.SubKontoSpolka > 0 Then
                _MajatekABS += KontoCSEGHandle.SubKontoSpolka
            End If
            If KontoCSEGHandle.SubKontoWspolnicy > 0 Then
                _MajatekABS += KontoCSEGHandle.SubKontoWspolnicy
            End If
            If KontoCSEGHandle.SubKontoVAT > 0 Then
                _MajatekABS += KontoCSEGHandle.SubKontoVAT
            End If
            If KontoCSEGHandle.SubKontoCIT > 0 Then
                _MajatekABS += KontoCSEGHandle.SubKontoCIT
            End If
            If KontoCSEGHandle.SubKontoPIT > 0 Then
                _MajatekABS += KontoCSEGHandle.SubKontoPIT
            End If

            Return _MajatekABS
        End Get
        Set(value As Decimal)
            _MajatekABS = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("MajatekABS"))
        End Set
    End Property

    Private Property _RodzajOperacji As String
    Public Property RodzajOperacji As String
        Get
            Return _RodzajOperacji
        End Get
        Set(value As String)
            _RodzajOperacji = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("RodzajOperacji"))
        End Set
    End Property
    'kwota recznej operacji
    Private _KwotaOperacji As Decimal
    Public Property KwotaOperacji() As Decimal
        Get
            Return _KwotaOperacji
        End Get
        Set(ByVal value As Decimal)
            _KwotaOperacji = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KwotaOperacji"))
        End Set
    End Property

    Private Property _OpisOperacji As String
    Public Property OpisOperacji As String
        Get
            Return _OpisOperacji
        End Get
        Set(value As String)
            _OpisOperacji = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("OpisOperacji"))
        End Set
    End Property


#Region "Pie chart"

    'set fractions of cirles
    'first arc - rachunek bieżący spólki
    Private _Arc1Fraction As Decimal
    Public Property Arc1Fraction() As Decimal
        Get
            If MajatekABS > 0 And KontoCSEGHandle.SubKontoSpolka > 0 Then
                _Arc1Fraction = KontoCSEGHandle.SubKontoSpolka / MajatekABS
            Else _Arc1Fraction = 0
            End If
            Return _Arc1Fraction
        End Get
        Set(ByVal value As Decimal)
            _Arc1Fraction = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc1Fraction"))
        End Set
    End Property

    'pieniądze wspolnikow
    Private _Arc2Fraction As Decimal
    Public Property Arc2Fraction() As Decimal
        Get
            If MajatekABS > 0 And KontoCSEGHandle.SubKontoWspolnicy > 0 Then
                _Arc2Fraction = KontoCSEGHandle.SubKontoWspolnicy / MajatekABS
            Else _Arc2Fraction = 0
            End If
            Return _Arc2Fraction
        End Get
        Set(ByVal value As Decimal)
            _Arc2Fraction = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc2Fraction"))
        End Set
    End Property

    'pieniądze z Vatu
    Private _Arc3Fraction As Decimal
    Public Property Arc3Fraction() As Decimal
        Get
            If MajatekABS > 0 And KontoCSEGHandle.SubKontoVAT > 0 Then
                _Arc3Fraction = KontoCSEGHandle.SubKontoVAT / MajatekABS
            Else _Arc3Fraction = 0
            End If
            Return _Arc3Fraction
        End Get
        Set(ByVal value As Decimal)
            _Arc3Fraction = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc3Fraction"))
        End Set
    End Property

    'prawdopodobny podatek CIT
    Private _Arc4Fraction As Decimal
    Public Property Arc4Fraction() As Decimal
        Get
            If MajatekABS > 0 And KontoCSEGHandle.SubKontoCIT > 0 Then
                _Arc4Fraction = KontoCSEGHandle.SubKontoCIT / MajatekABS
            Else _Arc4Fraction = 0
            End If
            Return _Arc4Fraction
        End Get
        Set(ByVal value As Decimal)
            _Arc4Fraction = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc4Fraction"))
        End Set
    End Property

    'prawdopodobny podatek PIT
    Private _Arc5Fraction As Decimal
    Public Property Arc5Fraction() As Decimal
        Get
            If MajatekABS > 0 And KontoCSEGHandle.SubKontoPIT > 0 Then
                _Arc5Fraction = KontoCSEGHandle.SubKontoPIT / MajatekABS
            Else _Arc5Fraction = 0
            End If
            Return _Arc5Fraction
        End Get
        Set(ByVal value As Decimal)
            _Arc5Fraction = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc5Fraction"))
        End Set
    End Property
    'Arc 5 fraction is not neccesary because fifth arc should end cirle


    'property Radius just for display
    Public ReadOnly Property ArcRadius() As Size
        Get
            ArcRadius = New Size(Radius, Radius)
            Return ArcRadius
        End Get
    End Property

    'property to display correct arc ( large arc = > 50%)
    Private Property _Arc1Big As Boolean
    Public Property Arc1Big() As Boolean
        Get
            _Arc1Big = Arc1Fraction > 0.5
            Return _Arc1Big
        End Get
        Set(value As Boolean)
            _Arc1Big = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc1Big"))
        End Set
    End Property

    Private Property _Arc2Big As Boolean
    Public Property Arc2Big() As Boolean
        Get
            _Arc2Big = Arc2Fraction > 0.5
            Return _Arc2Big
        End Get
        Set(value As Boolean)
            _Arc2Big = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc2Big"))
        End Set
    End Property

    Private Property _Arc3Big As Boolean
    Public Property Arc3Big() As Boolean
        Get
            _Arc3Big = Arc3Fraction > 0.5

            Return _Arc3Big
        End Get
        Set(value As Boolean)
            _Arc3Big = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc3Big"))
        End Set
    End Property

    Private Property _Arc4Big As Boolean
    Public Property Arc4Big() As Boolean
        Get
            _Arc4Big = Arc4Fraction > 0.5
            Return _Arc4Big
        End Get
        Set(value As Boolean)
            _Arc4Big = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc4Big"))
        End Set
    End Property

    Private Property _Arc5Big As Boolean
    Public Property Arc5Big() As Boolean
        Get
            _Arc5Big = Arc5Fraction > 0.5
            Return _Arc5Big
        End Get
        Set(value As Boolean)
            _Arc5Big = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc5Big"))
        End Set
    End Property

    ' start point of each arc

    Private _Arc1Start As Point
    Public Property Arc1Start() As Point
        Get
            _Arc1Start = New Point(X0 + Radius, Y0)
            Return _Arc1Start
        End Get
        Set(ByVal value As Point)
            _Arc1Start = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc1Start"))
        End Set
    End Property
    Private _Arc2Start As Point
    Public Property Arc2Start() As Point
        Get
            Dim ArcFraction = Arc1Fraction
            Dim alpha = 2 * Math.PI * Arc1Fraction
            Dim cos = Math.Cos(alpha)

            _Arc2Start = New Point() With {
            .X = (X0 + (Radius * Math.Cos(2 * Math.PI * ArcFraction))),
            .Y = -(-Y0 + (Radius * Math.Sin(2 * Math.PI * ArcFraction)))
            }
            ' _Arc2Start = New Point(350, 200)
            Return _Arc2Start
        End Get
        Set(ByVal value As Point)
            _Arc2Start = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc2Start"))
        End Set
    End Property
    Private _Arc3Start As Point
    Public Property Arc3Start() As Point
        Get
            Dim ArcFraction = Arc1Fraction + Arc2Fraction
            _Arc3Start = New Point() With {
          .X = (X0 + (Radius * Math.Cos(2 * Math.PI * ArcFraction))),
          .Y = -(-Y0 + (Radius * Math.Sin(2 * Math.PI * ArcFraction)))
          }

            Return _Arc3Start
        End Get
        Set(ByVal value As Point)
            _Arc3Start = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc3Start"))
        End Set
    End Property
    Private _Arc4Start As Point
    Public Property Arc4Start() As Point
        Get
            Dim ArcFraction = Arc1Fraction + Arc2Fraction + Arc3Fraction
            _Arc4Start = New Point() With {
          .X = (X0 + (Radius * Math.Cos(2 * Math.PI * ArcFraction))),
          .Y = -(-Y0 + (Radius * Math.Sin(2 * Math.PI * ArcFraction)))
          }
            Return _Arc4Start
        End Get
        Set(ByVal value As Point)
            _Arc4Start = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc4Start"))
        End Set
    End Property
    Private _Arc5Start As Point
    Public Property Arc5Start() As Point
        Get
            Dim ArcFraction = Arc1Fraction + Arc2Fraction + Arc3Fraction + Arc4Fraction
            _Arc5Start = New Point() With {
          .X = (X0 + (Radius * Math.Cos(2 * Math.PI * ArcFraction))),
          .Y = -(-Y0 + (Radius * Math.Sin(2 * Math.PI * ArcFraction)))
          }
            Return _Arc5Start
        End Get
        Set(ByVal value As Point)
            _Arc5Start = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc5Start"))
        End Set
    End Property

    '************** this part is for drawing nice labels ( we need for that middle of arc, then start of line, end of line, and position for label
    'middle points of arch *to get plece wher line should point
    Private _Arc1Middle As Point
    Public Property Arc1Middle() As Point
        Get
            Dim ArcFraction = Arc1Fraction / 2
            _Arc1Middle = New Point() With {
            .X = (X0 + (Radius * Math.Cos(2 * Math.PI * ArcFraction))),
            .Y = -(-Y0 + (Radius * Math.Sin(2 * Math.PI * ArcFraction)))
            }
            Return _Arc1Middle
        End Get
        Set(ByVal value As Point)
            _Arc1Middle = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc1Middle"))
        End Set
    End Property
    Private _Arc2Middle As Point
    Public Property Arc2Middle() As Point
        Get

            Dim ArcFraction = (Arc2Fraction / 2) + Arc1Fraction
            _Arc2Middle = New Point() With {
            .X = (X0 + (Radius * Math.Cos(2 * Math.PI * ArcFraction))),
            .Y = -(-Y0 + (Radius * Math.Sin(2 * Math.PI * ArcFraction)))
            }

            Return _Arc2Middle
        End Get
        Set(ByVal value As Point)
            _Arc2Middle = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc2Middle"))
        End Set
    End Property
    Private _Arc3Middle As Point
    Public Property Arc3Middle() As Point
        Get
            Dim ArcFraction = (Arc3Fraction / 2) + Arc1Fraction + Arc2Fraction
            _Arc3Middle = New Point() With {
            .X = (X0 + (Radius * Math.Cos(2 * Math.PI * ArcFraction))),
            .Y = -(-Y0 + (Radius * Math.Sin(2 * Math.PI * ArcFraction)))
            }

            Return _Arc3Middle
        End Get
        Set(ByVal value As Point)
            _Arc3Middle = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc3Middle"))
        End Set
    End Property
    Private _Arc4Middle As Point
    Public Property Arc4Middle() As Point
        Get
            Dim ArcFraction = (Arc4Fraction / 2) + Arc1Fraction + Arc2Fraction + Arc3Fraction
            _Arc4Middle = New Point() With {
            .X = (X0 + (Radius * Math.Cos(2 * Math.PI * ArcFraction))),
            .Y = -(-Y0 + (Radius * Math.Sin(2 * Math.PI * ArcFraction)))
            }
            Return _Arc4Middle
        End Get
        Set(ByVal value As Point)
            _Arc4Middle = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc4Middle"))
        End Set
    End Property
    Private _Arc5Middle As Point
    Public Property Arc5Middle() As Point
        Get
            Dim ArcFraction = (Arc5Fraction / 2) + Arc1Fraction + Arc2Fraction + Arc3Fraction + Arc4Fraction
            _Arc5Middle = New Point() With {
            .X = (X0 + (Radius * Math.Cos(2 * Math.PI * ArcFraction))),
            .Y = -(-Y0 + (Radius * Math.Sin(2 * Math.PI * ArcFraction)))
            }
            Return _Arc5Middle
        End Get
        Set(ByVal value As Point)
            _Arc5Middle = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc5Middle"))
        End Set
    End Property


    'line and label for Arc1
    Private _Arc1LineStart As Point
    Public Property Arc1LineStart() As Point
        Get


            If Arc1Middle.X > X0 Then
                _Arc1LineStart.X = Arc1Middle.X + TextLineXOffset
            Else
                _Arc1LineStart.X = Arc1Middle.X + -TextLineXOffset
            End If
            If Arc1Middle.Y > Y0 Then
                _Arc1LineStart.Y = Arc1Middle.Y + TextLineYOffset
            Else
                _Arc1LineStart.Y = Arc1Middle.Y - TextLineYOffset
            End If
            Return _Arc1LineStart
        End Get
        Set(value As Point)
            _Arc1LineStart = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc1LineStart"))
        End Set
    End Property
    Private _Arc1LineEnd As Point
    Public Property Arc1LineEnd() As Point
        Get
            If Arc1Middle.X > X0 Then
                _Arc1LineEnd = New Point(Arc1LineStart.X + TextLineLengh, Arc1LineStart.Y)
            Else
                _Arc1LineEnd = New Point(Arc1LineStart.X - TextLineLengh, Arc1LineStart.Y)
            End If
            Return _Arc1LineEnd
        End Get
        Set(value As Point)
            _Arc1LineEnd = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc1LineEnd"))
        End Set
    End Property
    Private _Arc1Label As Point
    Public Property Arc1Label() As Point
        Get
            If Arc1Middle.X > X0 Then
                _Arc1Label = New Point(Arc1LineStart.X + 5, Arc1LineStart.Y - 30)
            Else
                _Arc1Label = New Point(Arc1LineEnd.X + 5, Arc1LineEnd.Y - 30)
            End If
            Return _Arc1Label
        End Get
        Set(value As Point)
            _Arc1Label = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc1Label"))
        End Set
    End Property

    'line and label for Arc2
    Private _Arc2LineStart As Point
    Public Property Arc2LineStart() As Point

        Get

            If Arc2Middle.X > X0 Then
                _Arc2LineStart.X = Arc2Middle.X + TextLine2XOffset
            Else
                _Arc2LineStart.X = Arc2Middle.X + -TextLine2XOffset
            End If
            If Arc2Middle.Y > Y0 Then
                _Arc2LineStart.Y = Arc2Middle.Y + TextLine2YOffset
            Else
                _Arc2LineStart.Y = Arc2Middle.Y - TextLine2YOffset
            End If
            'Check if label is not over previous label. If yes then ad offet
            If ((Arc2Middle.X > 0 And Arc1Middle.X > 0) Or (Arc2Middle.X < 0 And Arc1Middle.X < 0)) And
                 Math.Abs(_Arc2LineStart.Y - _Arc1LineStart.Y) < TextLineYOffset And
                    _Arc2LineStart.Y > _Arc1LineStart.Y Then
                _Arc2LineStart.Y += SmallArcLabelExtraOffset
            ElseIf ((Arc2Middle.X > 0 And Arc1Middle.X > 0) Or (Arc2Middle.X < 0 And Arc1Middle.X < 0)) And
                  Math.Abs(_Arc2LineStart.Y - _Arc1LineStart.Y) < TextLineYOffset And
                    _Arc2LineStart.Y < _Arc1LineStart.Y Then
                _Arc2LineStart.Y -= SmallArcLabelExtraOffset
            End If
            Return _Arc2LineStart
        End Get
        Set(value As Point)
            _Arc2LineStart = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc2LineStart"))
        End Set
    End Property
    Private _Arc2LineEnd As Point
    Public Property Arc2LineEnd() As Point
        Get
            If Arc2Middle.X > X0 Then
                _Arc2LineEnd = New Point(Arc2LineStart.X + TextLineLengh, Arc2LineStart.Y)
            Else
                _Arc2LineEnd = New Point(Arc2LineStart.X - TextLineLengh, Arc2LineStart.Y)
            End If
            Return _Arc2LineEnd
        End Get
        Set(value As Point)
            _Arc2LineEnd = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc2LineEnd"))
        End Set
    End Property
    Private _Arc2Label As Point
    Public Property Arc2Label() As Point
        Get
            If Arc2Middle.X > X0 Then
                _Arc2Label = New Point(Arc2LineStart.X + 5, Arc2LineStart.Y - 30)
            Else
                _Arc2Label = New Point(Arc2LineEnd.X + 5, Arc2LineEnd.Y - 30)
            End If
            Return _Arc2Label
        End Get
        Set(value As Point)
            _Arc2Label = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc2Label"))
        End Set
    End Property

    'line and label for Arc3 (VAT)
    Private _Arc3LineStart As Point
    Public Property Arc3LineStart() As Point
        Get

            If Arc3Middle.X > X0 Then
                _Arc3LineStart.X = Arc3Middle.X + TextLineXOffset
            Else
                _Arc3LineStart.X = Arc3Middle.X + -TextLineXOffset
            End If
            If Arc3Middle.Y > Y0 Then
                _Arc3LineStart.Y = Arc3Middle.Y + TextLineYOffset
            Else
                _Arc3LineStart.Y = Arc3Middle.Y - TextLineYOffset
            End If
            'Check if label is not over previous label. If yes then ad offet
            If ((Arc3Middle.X > 0 And Arc2Middle.X > 0) Or (Arc3Middle.X < 0 And Arc2Middle.X < 0)) And
                 Math.Abs(_Arc3LineStart.Y - _Arc2LineStart.Y) < TextLineYOffset And
                    _Arc3LineStart.Y > _Arc2LineStart.Y Then
                _Arc3LineStart.Y += SmallArcLabelExtraOffset
            ElseIf ((Arc3Middle.X > 0 And Arc2Middle.X > 0) Or (Arc3Middle.X < 0 And Arc2Middle.X < 0)) And
                  Math.Abs(_Arc3LineStart.Y - _Arc2LineStart.Y) < TextLineYOffset And
                    _Arc3LineStart.Y < _Arc2LineStart.Y Then
                _Arc3LineStart.Y -= SmallArcLabelExtraOffset
            End If
            Return _Arc3LineStart
        End Get
        Set(value As Point)
            _Arc3LineStart = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc3LineStart"))
        End Set
    End Property
    Private _Arc3LineEnd As Point
    Public Property Arc3LineEnd() As Point
        Get
            If Arc3Middle.X > X0 Then
                _Arc3LineEnd = New Point(Arc3LineStart.X + TextLineLengh, Arc3LineStart.Y)
            Else
                _Arc3LineEnd = New Point(Arc3LineStart.X - TextLineLengh, Arc3LineStart.Y)
            End If
            Return _Arc3LineEnd
        End Get
        Set(value As Point)
            _Arc3LineEnd = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc3LineEnd"))
        End Set
    End Property
    Private _Arc3Label As Point
    Public Property Arc3Label() As Point
        Get
            If Arc3Middle.X > X0 Then
                _Arc3Label = New Point(Arc3LineStart.X + 5, Arc3LineStart.Y - 30)
            Else
                _Arc3Label = New Point(Arc3LineEnd.X + 5, Arc3LineEnd.Y - 30)
            End If
            Return _Arc3Label
        End Get
        Set(value As Point)
            _Arc3Label = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc3Label"))
        End Set
    End Property

    'line and label for Arc4 (CIT)
    Private _Arc4LineStart As Point
    Public Property Arc4LineStart() As Point
        Get
            If Arc4Middle.X > X0 Then
                _Arc4LineStart.X = Arc4Middle.X + TextLineXOffset
            Else
                _Arc4LineStart.X = Arc4Middle.X - TextLineXOffset
            End If
            If Arc4Middle.Y > Y0 Then
                _Arc4LineStart.Y = Arc4Middle.Y + TextLineYOffset
            Else
                _Arc4LineStart.Y = Arc4Middle.Y - TextLineYOffset
            End If
            'Check if label is not over previous label. If yes then ad offet
            If ((Arc4Middle.X > 0 And Arc3Middle.X > 0) Or (Arc4Middle.X < 0 And Arc3Middle.X < 0)) And
                 Math.Abs(_Arc4LineStart.Y - _Arc3LineStart.Y) < TextLineYOffset And
                    _Arc4LineStart.Y > _Arc3LineStart.Y Then
                _Arc4LineStart.Y += SmallArcLabelExtraOffset
            ElseIf ((Arc4Middle.X > 0 And Arc3Middle.X > 0) Or (Arc4Middle.X < 0 And Arc3Middle.X < 0)) And
                  Math.Abs(_Arc4LineStart.Y - _Arc3LineStart.Y) < TextLineYOffset And
                    _Arc4LineStart.Y < _Arc3LineStart.Y Then
                _Arc4LineStart.Y -= SmallArcLabelExtraOffset
            End If
            Return _Arc4LineStart
        End Get
        Set(value As Point)
            _Arc4LineStart = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc4LineStart"))
        End Set
    End Property
    Private _Arc4LineEnd As Point
    Public Property Arc4LineEnd() As Point
        Get
            If Arc4Middle.X > X0 Then
                _Arc4LineEnd = New Point(Arc4LineStart.X + TextLineLengh, Arc4LineStart.Y)
            Else
                _Arc4LineEnd = New Point(Arc4LineStart.X - TextLineLengh, Arc4LineStart.Y)
            End If
            Return _Arc4LineEnd
        End Get
        Set(value As Point)
            _Arc4LineEnd = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc4LineEnd"))
        End Set
    End Property
    Private _Arc4Label As Point
    Public Property Arc4Label() As Point
        Get
            If Arc4Middle.X > X0 Then
                _Arc4Label = New Point(Arc4LineStart.X + 5, Arc4LineStart.Y - 30)
            Else
                _Arc4Label = New Point(Arc4LineEnd.X + 5, Arc4LineEnd.Y - 30)
            End If
            Return _Arc4Label
        End Get
        Set(value As Point)
            _Arc4Label = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc4Label"))
        End Set
    End Property

    'line and label for Arc5 ( PIT)
    Private _Arc5LineStart As Point
    Public Property Arc5LineStart() As Point
        Get

            If Arc5Middle.X > X0 Then
                _Arc5LineStart.X = Arc5Middle.X + TextLineXOffset
            Else
                _Arc5LineStart.X = Arc5Middle.X - TextLineXOffset
            End If
            If Arc5Middle.Y > Y0 Then
                _Arc5LineStart.Y = Arc5Middle.Y + TextLineYOffset
            Else
                _Arc5LineStart.Y = Arc5Middle.Y - TextLineYOffset
            End If
            'Check if label is not over previous label. If yes then ad offet
            If ((Arc5Middle.X > 0 And Arc4Middle.X > 0) Or (Arc5Middle.X < 0 And Arc4Middle.X < 0)) And
                 Math.Abs(_Arc5LineStart.Y - _Arc4LineStart.Y) < TextLineYOffset And
                    _Arc5LineStart.Y > _Arc4LineStart.Y Then
                _Arc5LineStart.Y += SmallArcLabelExtraOffset
            ElseIf ((Arc5Middle.X > 0 And Arc4Middle.X > 0) Or (Arc5Middle.X < 0 And Arc4Middle.X < 0)) And
                  Math.Abs(_Arc5LineStart.Y - _Arc4LineStart.Y) < TextLineYOffset And
                    _Arc5LineStart.Y < _Arc4LineStart.Y Then
                _Arc5LineStart.Y -= SmallArcLabelExtraOffset
            End If
            ''Check if label is not over pre-previous label. If yes then ad offet
            'If ((Arc5Middle.X > 0 And Arc3Middle.X > 0) Or (Arc5Middle.X < 0 And Arc3Middle.X < 0)) And
            '     Math.Abs(_Arc5LineStart.Y - _Arc3LineStart.Y) < SmallArcLabelExtraOffset And
            '        _Arc5LineStart.Y > _Arc3LineStart.Y Then
            '    _Arc5LineStart.Y += SmallArcLabelExtraOffset
            'ElseIf ((Arc5Middle.X > 0 And Arc3Middle.X > 0) Or (Arc5Middle.X < 0 And Arc3Middle.X < 0)) And
            '      Math.Abs(_Arc5LineStart.Y - _Arc3LineStart.Y) < SmallArcLabelExtraOffset And
            '        _Arc5LineStart.Y < _Arc3LineStart.Y Then
            '    _Arc5LineStart.Y -= SmallArcLabelExtraOffset
            'End If

            Return _Arc5LineStart
        End Get
        Set(value As Point)
            _Arc5LineStart = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc5LineStart"))
        End Set
    End Property
    Private _Arc5LineEnd As Point
    Public Property Arc5LineEnd() As Point
        Get
            If Arc5Middle.X > X0 Then
                _Arc5LineEnd = New Point(Arc5LineStart.X + TextLineLengh, Arc5LineStart.Y)
            Else
                _Arc5LineEnd = New Point(Arc5LineStart.X - TextLineLengh, Arc5LineStart.Y)
            End If
            Return _Arc5LineEnd
        End Get
        Set(value As Point)
            _Arc5LineEnd = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc5LineEnd"))
        End Set
    End Property
    Private _Arc5Label As Point
    Public Property Arc5Label() As Point
        Get
            If Arc5Middle.X > X0 Then
                _Arc5Label = New Point(Arc5LineStart.X + 5, Arc5LineStart.Y - 30)
            Else
                _Arc5Label = New Point(Arc5LineEnd.X + 5, Arc5LineEnd.Y - 30)
            End If
            Return _Arc5Label
        End Get
        Set(value As Point)
            _Arc5Label = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Arc5Label"))
        End Set
    End Property
    'label po środku koła z całkowitą kwotą
    Public ReadOnly Property MiddleLabel() As Point
        Get
            MiddleLabel = New Point(X0 - 20, Y0)

            Return MiddleLabel
        End Get
    End Property


#End Region


#End Region


#Region "Subs"

    Sub RecznaOperacjaCSEG()
        Dim _konto = KontoCSEGHandle
        If KsiegowyModel.OperacjaKontoCSEG(_konto, RodzajOperacji, KwotaOperacji) Then
            Startup.MainDataBaseModel.AddOperationCSEG(_konto)
            KontoCSEGHandle = KontoCSEGHandle
            RereshPie()
            KwotaOperacji = 0
            RodzajOperacji = ""
            OpisOperacji = ""
        End If
    End Sub
    Public Shared Sub RefreshKonta()

    End Sub

    Sub RereshPie()
        TextLine2XOffset = TextLineXOffset
        TextLine2YOffset = TextLineYOffset
        TextLine3XOffset = TextLineXOffset
        TextLine3YOffset = TextLineYOffset
        TextLine4XOffset = TextLineXOffset
        TextLine4YOffset = TextLineYOffset
        TextLine5XOffset = TextLineXOffset
        TextLine5YOffset = TextLineYOffset

        Arc1Fraction = Arc1Fraction
        Arc2Fraction = Arc2Fraction
        Arc3Fraction = Arc3Fraction
        Arc4Fraction = Arc4Fraction
        Arc5Fraction = Arc5Fraction
        Arc1Big = Arc1Big
        Arc2Big = Arc2Big
        Arc3Big = Arc3Big
        Arc4Big = Arc4Big
        Arc5Big = Arc5Big
        Arc1Start = Arc1Start
        Arc2Start = Arc2Start
        Arc3Start = Arc3Start
        Arc4Start = Arc4Start
        Arc5Start = Arc5Start
        Arc1Middle = Arc1Middle
        Arc2Middle = Arc2Middle
        Arc3Middle = Arc3Middle
        Arc4Middle = Arc4Middle
        Arc5Middle = Arc5Middle
        Arc1LineStart = Arc1LineStart
        Arc2LineStart = Arc2LineStart
        Arc3LineStart = Arc3LineStart
        Arc4LineStart = Arc4LineStart
        Arc5LineStart = Arc5LineStart
        Arc1LineEnd = Arc1LineEnd
        Arc2LineEnd = Arc2LineEnd
        Arc3LineEnd = Arc3LineEnd
        Arc4LineEnd = Arc4LineEnd
        Arc5LineEnd = Arc5LineEnd
        Arc1Label = Arc1Label
        Arc2Label = Arc2Label
        Arc3Label = Arc3Label
        Arc4Label = Arc4Label
        Arc5Label = Arc5Label
        MajatekSpolkiPLN = MajatekSpolkiPLN

    End Sub

    '************ fill from data row **************************

#End Region

End Class
