Imports System.ComponentModel
Imports System.Data
Public Class KontaWspolnicyViewModel
    Implements INotifyPropertyChanged
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region


#Region "Properties"

    Private _KontaWspolnicyDataTable As DataTable
    Public Property KontaWspolnicyDataTable() As DataTable
        Get
            Return _KontaWspolnicyDataTable
        End Get
        Set(ByVal value As DataTable)
            _KontaWspolnicyDataTable = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontaWspolnicyDataTable"))
        End Set
    End Property


    Private _KontoPOstrowski As KontoWspolnika
    Public Property KontoPOstrowski() As KontoWspolnika
        Get
            Return _KontoPOstrowski
        End Get
        Set(ByVal value As KontoWspolnika)
            _KontoPOstrowski = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontoPOstrowski"))
        End Set
    End Property

    Private _KontoPPawlowski As KontoWspolnika
    Public Property KontoPPawlowski() As KontoWspolnika
        Get
            Return _KontoPPawlowski
        End Get
        Set(ByVal value As KontoWspolnika)
            _KontoPPawlowski = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontoPPawlowski"))
        End Set
    End Property

    Private _KontoMBabka As KontoWspolnika
    Public Property KontoMBabka() As KontoWspolnika
        Get
            Return _KontoMBabka
        End Get
        Set(ByVal value As KontoWspolnika)
            _KontoMBabka = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontoMBabka"))
        End Set
    End Property


    '*** properties for manual operations
    Private Property _Osoba As String
    Public Property Osoba As String
        Get
            Return _Osoba
        End Get
        Set(value As String)
            _Osoba = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Osoba"))
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
#End Region
#Region "Subs"
    Public Sub FillKonta()
        KontoPOstrowski = DataBaseModel.FillKontoWspolnika("POstrowski")
        KontoPPawlowski = DataBaseModel.FillKontoWspolnika("PPawlowski")
        KontoMBabka = DataBaseModel.FillKontoWspolnika("MBabka")
    End Sub

    Sub RecznaOperacja()
        If Osoba = "" Or KwotaOperacji = 0 Or RodzajOperacji = "" Then
            Dim messagebox As New MessageBoxCustom("Nie wypełniłeś wszystkich pól")
            messagebox.Show()
            Return
        End If
        'wybierz na którym koncie ma zostac wykonana operacja
        Dim konto As KontoWspolnika
        Select Case Osoba
            Case "POstrowski"
                konto = KontoPOstrowski
            Case "PPawlowski"
                konto = KontoPPawlowski
            Case "MBabka"
                konto = KontoMBabka
            Case Else
                Return
        End Select

        'pozwol ksiegowemu wykonac rachunki
        If KsiegowyModel.OperacjaKontoWspolnika(konto, RodzajOperacji, KwotaOperacji) Then
            konto.Operacja = RodzajOperacji
            konto.Kwota = KwotaOperacji
            konto.Opis = OpisOperacji
            'dodaj nowy record do bazy danych
            Startup.MainDataBaseModel.AddOperationWspolnicy(konto)
            'wyczysc pola wformatce
            KwotaOperacji = 0
            RodzajOperacji = ""
            OpisOperacji = ""
            'zaktualizuj obiekty konta wspolnikow
            ' FillKontoWspolnika("POstrowski")
            '  FillKontoWspolnika("PPawlowski")
            ' FillKontoWspolnika("MBabka")
        Else
            Dim messagebox As New MessageBoxCustom("Nie udałom się wykonać operacji na koncie wspólnika")
            messagebox.Show()
        End If
    End Sub
#End Region
End Class
