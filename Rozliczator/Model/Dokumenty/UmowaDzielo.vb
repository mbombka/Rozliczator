Imports System.ComponentModel
Public Class UmowaDzielo
    Implements INotifyPropertyChanged
    Public Sub New()
        NumerUmowy = ""
        DataPoczatek = DateTime.Now.Date
        DataKoniec = DateTime.Now.Date
        KosztyUzyskPrzych = 50
        ProgPodatkowy = 18
    End Sub
#Region "Properties"
    '********************************************************************************
    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get

        Set(ByVal value As Integer)
            _Id = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Id"))
        End Set
    End Property
    Private _NumerUmowy As String
    Public Property NumerUmowy() As String
        Get
            Return _NumerUmowy
        End Get

        Set(ByVal value As String)
            _NumerUmowy = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("NumerUmowy"))
        End Set
    End Property
    Private _Osoba As String
    Public Property Osoba() As String
        Get
            Return _Osoba
        End Get

        Set(ByVal value As String)
            _Osoba = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Osoba"))
        End Set
    End Property

    Private _DataPoczatek As Date
    Public Property DataPoczatek() As Date
        Get
            Return _DataPoczatek
        End Get

        Set(ByVal value As Date)
            _DataPoczatek = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DataPoczatek"))
        End Set
    End Property

    Private _DataKoniec As Date
    Public Property DataKoniec() As Date
        Get
            Return _DataKoniec
        End Get

        Set(ByVal value As Date)
            _DataKoniec = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DataKoniec"))
        End Set
    End Property

    Private _SumaWydatkow As Decimal
    Public Property SumaWydatkow() As Decimal
        Get
            Return _SumaWydatkow
        End Get

        Set(ByVal value As Decimal)
            _SumaWydatkow = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SumaWydatkow"))
        End Set
    End Property


    Private _SumaDiet As Decimal
    Public Property SumaDiet() As Decimal
        Get
            Return _SumaDiet
        End Get

        Set(ByVal value As Decimal)
            _SumaDiet = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SumaDiet"))
        End Set
    End Property

    Private _SumaPrzychodow As Decimal
    Public Property SumaPrzychodow() As Decimal
        Get
            Return _SumaPrzychodow
        End Get

        Set(ByVal value As Decimal)
            _SumaPrzychodow = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SumaPrzychodow"))
        End Set
    End Property

    Private _Dziesiecina As Decimal
    Public Property Dziesiecina() As Decimal
        Get
            Return _Dziesiecina
        End Get

        Set(ByVal value As Decimal)
            _Dziesiecina = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Dziesiecina"))
        End Set
    End Property

    Private _KwotaBrutto As Decimal
    Public Property KwotaBrutto() As Decimal
        Get
            Return _KwotaBrutto
        End Get

        Set(ByVal value As Decimal)
            _KwotaBrutto = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KwotaBrutto"))
        End Set
    End Property

    Private _KwotaNettoSugerowana As Decimal
    Public Property KwotaNettoSugerowana() As Decimal
        Get
            Return _KwotaNettoSugerowana
        End Get

        Set(ByVal value As Decimal)
            _KwotaNettoSugerowana = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KwotaNettoSugerowana"))
        End Set
    End Property

    Private _KwotaBruttoSugerowana As Decimal
    Public Property KwotaBruttoSugerowana() As Decimal
        Get
            Return _KwotaBruttoSugerowana
        End Get

        Set(ByVal value As Decimal)
            _KwotaBruttoSugerowana = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KwotaBruttoSugerowana"))
        End Set
    End Property

    Private _KwotaNetto As Decimal
    Public Property KwotaNetto() As Decimal
        Get
            Return _KwotaNetto
        End Get

        Set(ByVal value As Decimal)
            _KwotaNetto = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KwotaNetto"))
        End Set
    End Property

    Private _KosztyUzyskPrzych As Integer
    Public Property KosztyUzyskPrzych() As Integer
        Get
            Return _KosztyUzyskPrzych
        End Get

        Set(ByVal value As Integer)
            _KosztyUzyskPrzych = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KosztyUzyskPrzych"))
        End Set
    End Property
    Private _ProgPodatkowy As Integer
    Public Property ProgPodatkowy() As Integer
        Get
            Return _ProgPodatkowy
        End Get

        Set(ByVal value As Integer)
            _ProgPodatkowy = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ProgPodatkowy"))
        End Set
    End Property

    Private _Wyplacono As Boolean
    Public Property Wyplacono() As Boolean
        Get
            Return _Wyplacono
        End Get

        Set(ByVal value As Boolean)
            _Wyplacono = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Wyplacono"))
        End Set
    End Property

    Private _Konto As String
    Public Property Konto() As String
        Get
            Return _Konto
        End Get

        Set(ByVal value As String)
            _Konto = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Konto"))
        End Set
    End Property

    Private _Plik1 As String
    Public Property Plik1() As String
        Get
            Return _Plik1
        End Get

        Set(ByVal value As String)
            _Plik1 = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Plik1"))
        End Set
    End Property

    Private _Plik2 As String
    Public Property Plik2() As String
        Get
            Return _Plik2
        End Get

        Set(ByVal value As String)
            _Plik2 = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Plik2"))
        End Set
    End Property

    Public Zalacznik As Image
#End Region

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
