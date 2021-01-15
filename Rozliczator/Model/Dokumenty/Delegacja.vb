Imports System.ComponentModel
Imports System.Data
Public Class Delegacja
    Implements INotifyPropertyChanged

    'test'
    Dim testgodzina As Integer


    Public PotwierdzenieWyjazdu As Image
    Public PotwierdzeniePowrotu As Image

    'czas na delegacji dzielimy na 3 części : 1 do północy dnia pierwszego, 2: czas pomiędzy - same pełne doby ,3: czas od początku dnia do przekroczenia granicy, potem ich suma daje czas delegacji
    ' AD1) czas od początku podrózy do północy - do 8h - 1/3 diety, 8-12h: 1/2 diety, >12h:cała dieta
    'AD2) same pelne doby, każda liczona jako 1
    'Ad3) czas od początku dnia ( godzina0), do przekroczenia granicy: < 8h - 1/3 diety, 8-12h: 1/2 diety, >12h:cała dieta

    Dim CzasDelegacjiTimeSpan As TimeSpan
    Dim bLoading As Boolean = False 'temporary value to hold refreshing values from web services during filling delegacja from data row
    Public Sub New()
        DataWyjazdu = DateTime.Now.Date
        DataPowrotu = DateTime.Now.Date
        GodzinaWyjazdu = DateTime.Now.Date
        GodzinaPowrotu = DateTime.Now.Date
        DataRozliczenia = DateTime.Now.Date

        StawkaDiety = 0
        CzasDelegacji = 0
        KursZDnia = 1 ' set auto currency
        Waluta = "PLN" ' set auto currency 
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

    Private _Delegowany As String
    Public Property Delegowany() As String
        Get
            Return _Delegowany
        End Get

        Set(ByVal value As String)
            _Delegowany = value
            UmowyListOfOsoba = UmowyListOfOsoba
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Delegowany"))
        End Set
    End Property

    Private _NumerDelegacji As String
    Public Property NumerDelegacji() As String
        Get
            Return _NumerDelegacji
        End Get

        Set(ByVal value As String)
            _NumerDelegacji = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("NumerDelegacji"))
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

    Private _DataWyjazdu As Date
    Public Property DataWyjazdu() As Date
        Get
            Return _DataWyjazdu
        End Get

        Set(ByVal value As Date)

            If _DataWyjazdu <> value Or Not bLoading Then    'refresh value of Czas delegacji only on change
                _DataWyjazdu = value
                CzasDelegacji = CzasDelegacji 'refresh value
            Else
                _DataWyjazdu = value
            End If

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DataWyjazdu"))
        End Set
    End Property

    'Godzina wyjazdu is separate from data wyjazdu only for time picker. value of godzina wyjazdu is copied from datawyjazdu In DB is stored only data wyjazdu
    Private _GodzinaWyjazdu As Date
    Public Property GodzinaWyjazdu() As Date
        Get

            Return _GodzinaWyjazdu

        End Get

        Set(ByVal value As Date)

            If _GodzinaWyjazdu <> value Or Not bLoading Then 'refresh value of Czas Daa Wyjazdy only on change
                _GodzinaWyjazdu = value
                DataWyjazdu = DataWyjazdu.AddHours(GodzinaWyjazdu.Hour - DataWyjazdu.Hour)   'change time of data wyjazdu
                DataWyjazdu = DataWyjazdu.AddMinutes(GodzinaWyjazdu.Minute - DataWyjazdu.Minute)  'change hours of data wyjazdu
            Else
                _GodzinaWyjazdu = value
            End If



            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("GodzinaWyjazdu"))
        End Set
    End Property

    Private _WyjazdMiasto As String
    Public Property WyjazdMiasto() As String
        Get
            Return _WyjazdMiasto
        End Get

        Set(ByVal value As String)
            _WyjazdMiasto = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("WyjazdMiasto"))
        End Set
    End Property

    Private _WyjazdTransport As String
    Public Property WyjazdTransport() As String
        Get
            Return _WyjazdTransport
        End Get

        Set(ByVal value As String)
            _WyjazdTransport = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("WyjazdTransport"))
        End Set
    End Property

    Private _DataPowrotu As Date
    Public Property DataPowrotu() As Date
        Get
            Return _DataPowrotu
        End Get

        Set(ByVal value As Date)

            If _DataPowrotu <> value Or Not bLoading Then    'refresh value of Czas delegacji only on change
                _DataPowrotu = value
                CzasDelegacji = CzasDelegacji 'refresh value
            Else
                _DataPowrotu = value
            End If




            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DataPowrotu"))
        End Set
    End Property
    'Godzina powrotu is separate from data powrotu only for time picker. value of godzina powrotu is copied from datawyjazdu In DB is stored only data powrotu
    Private _GodzinaPowrotu As Date
    Public Property GodzinaPowrotu() As Date
        Get
            Return _GodzinaPowrotu
        End Get

        Set(ByVal value As Date)
            If _GodzinaPowrotu <> value Or Not bLoading Then 'refresh value of Czas Data Powrotu only on change
                _GodzinaPowrotu = value
                DataPowrotu = DataPowrotu.AddHours(GodzinaPowrotu.Hour - DataPowrotu.Hour)  'change hours of data wyjazdu
                DataPowrotu = DataPowrotu.AddMinutes(GodzinaPowrotu.Minute - DataPowrotu.Minute)  'change hours of data wyjazdu
            Else
                _GodzinaPowrotu = value
            End If

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("GodzinaPowrotu"))
        End Set
    End Property


    Private _PowrotMiasto As String
    Public Property PowrotMiasto() As String
        Get
            Return _PowrotMiasto
        End Get

        Set(ByVal value As String)
            _PowrotMiasto = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("PowrotMiasto"))
        End Set
    End Property

    Private _PowrotTransport As String
    Public Property PowrotTransport() As String
        Get
            Return _PowrotTransport
        End Get

        Set(ByVal value As String)
            _PowrotTransport = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("PowrotTransport"))
        End Set
    End Property

    Private _KrajWyjazdu As String
    Public Property KrajWyjazdu() As String
        Get
            Return _KrajWyjazdu
        End Get

        Set(ByVal value As String)

            If _KrajWyjazdu <> value Or Not bLoading Then    'refresh values  only on change
                _KrajWyjazdu = value
                Waluta = FindKraj(value).Waluta
                StawkaDiety = FindKraj(value).StawkaDiety
            Else
                _KrajWyjazdu = value
            End If



            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KrajWyjazdu"))
        End Set
    End Property


    Private _KrajModel As Kraj  '#ToRemove
    Public Property KrajModel() As Kraj
        Get
            Return _KrajModel
        End Get

        Set(ByVal value As Kraj)
            _KrajModel = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KrajModel"))
        End Set
    End Property



    Private _MiejsceWyjazdu As String
    Public Property MiejsceWyjazdu() As String
        Get
            Return _MiejsceWyjazdu
        End Get

        Set(ByVal value As String)
            _MiejsceWyjazdu = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("MiejsceWyjazdu"))
        End Set
    End Property

    Private _CelWyjazdu As String
    Public Property CelWyjazdu() As String
        Get
            Return _CelWyjazdu
        End Get

        Set(ByVal value As String)
            _CelWyjazdu = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CelWyjazdu"))
        End Set
    End Property



    Private _CzasDelegacji As Decimal
    Public Property CzasDelegacji() As Decimal


        Get
            CzasDelegacjiTimeSpan = DataPowrotu - DataWyjazdu
            If CzasDelegacjiTimeSpan.Hours > 12 Then    'ileś dni + więcej niż 12 godzin 
                _CzasDelegacji = CzasDelegacjiTimeSpan.Days + 1
            ElseIf CzasDelegacjiTimeSpan.Hours > 8 Then 'Ileś dni + 8-12h
                _CzasDelegacji = CzasDelegacjiTimeSpan.Days + 0.5
            Else                                         'ileś dni + mniej niż 8h
                _CzasDelegacji = CzasDelegacjiTimeSpan.Days + 0.333  'podróż trwająca niepełny dzień i mniej niż 12h
            End If

            Return _CzasDelegacji
        End Get

        Set(ByVal value As Decimal)

            If _CzasDelegacji <> value Or Not bLoading Then    'refresh values  only on change
                _CzasDelegacji = value
                KwotaDelegacji = KwotaDelegacji 'refresh
            Else
                _CzasDelegacji = value
            End If



            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CzasDelegacji"))
        End Set
    End Property

    Private _KwotaDelegacji As Decimal
    Public Property KwotaDelegacji() As Decimal
        Get
            Return _KwotaDelegacji
        End Get

        Set(ByVal value As Decimal)

            _KwotaDelegacji = Math.Round(StawkaDiety * CzasDelegacji, 4)
            KwotaDelegacjiPLN = KwotaDelegacjiPLN ' refresh
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KwotaDelegacji"))
        End Set
    End Property

    Private _DataRozliczenia As Date
    Public Property DataRozliczenia() As Date
        Get
            Return _DataRozliczenia
        End Get

        Set(ByVal value As Date)

            If _DataRozliczenia <> value Or Not bLoading Then    'refresh value of Czas delegacji only on change
                _DataRozliczenia = value
                KwotaDelegacjiPLN = KwotaDelegacjiPLN 'refresh value
                KursZDnia = KursZDnia
            Else
                _DataRozliczenia = value
            End If
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DataRozliczenia"))
        End Set
    End Property

    Private _StawkaDiety As Decimal
    Public Property StawkaDiety() As Decimal
        Get
            Return _StawkaDiety
        End Get

        Set(ByVal value As Decimal)
            If _StawkaDiety <> value Or Not bLoading Then    'refresh values  only on change
                _StawkaDiety = value
                KwotaDelegacji = KwotaDelegacji ' refresh kwota delegacji
            Else
                _StawkaDiety = value
            End If


            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("StawkaDiety"))
        End Set
    End Property


    Private _Waluta As String
    Public Property Waluta() As String
        Get
            Return _Waluta
        End Get

        Set(ByVal value As String)
            If _Waluta <> value Or Not bLoading Then    'refresh values  only on change
                _Waluta = value
                KursZDnia = KursZDnia   'refresh value of Kurs z dnia
                KwotaDelegacjiPLN = KwotaDelegacjiPLN 'refresh value of KwotaPLN
                WalutaPLN = WalutaPLN ' refresh visibility of elements when chagne waluta
            Else
                _Waluta = value
            End If
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Waluta"))
        End Set
    End Property

    Private _KursZDnia As Decimal
    Public Property KursZDnia() As Decimal
        Get

            _KursZDnia = WalutyModel.KursZDnia(DataRozliczenia, Waluta)    'update field KursZdnia
            Return _KursZDnia
        End Get

        Set(ByVal value As Decimal)
            _KursZDnia = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KursZDnia"))
        End Set
    End Property

    Private _KwotaDelegacjiPLN As Decimal
    Public Property KwotaDelegacjiPLN() As Decimal
        Get
            _KwotaDelegacjiPLN = Math.Round(KwotaDelegacji * _KursZDnia, 4)  'calculate Kwota PLN

            Return _KwotaDelegacjiPLN
        End Get

        Set(ByVal value As Decimal)
            _KwotaDelegacjiPLN = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KwotaDelegacjiPLN"))
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

    Private _Wyslano As Boolean
    Public Property Wyslano() As Boolean
        Get
            Return _Wyslano
        End Get

        Set(ByVal value As Boolean)
            _Wyslano = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Wyslano"))
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


    Private Property _WalutaPLN As Visibility
    Public Property WalutaPLN As Visibility
        Get
            If Waluta = "PLN" Then
                _WalutaPLN = Visibility.Hidden
            Else
                _WalutaPLN = Visibility.Visible
            End If
            Return _WalutaPLN
        End Get
        Set(value As Visibility)
            _WalutaPLN = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("WalutaPLN"))
        End Set
    End Property

    'create list of umowao dzielo but only for selected person
    Private Property _UmowyListOfOsoba As List(Of Object)
    Public Property UmowyListOfOsoba As List(Of Object)
        Get

            Dim ValuetoReturn = (From Rows In Startup.VMLocator.VMUmowyDzielo.UmowyDzieloTable.AsEnumerable()
                                 Where Rows("Osoba") = Delegowany
                                 Select Rows("NumerUmowy")).Distinct().ToList()
            _UmowyListOfOsoba = ValuetoReturn

            Return _UmowyListOfOsoba

        End Get
        Set(value As List(Of Object))
            _UmowyListOfOsoba = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("UmowyListOfOsoba"))
        End Set
    End Property

#End Region
    Public Shared Function FindKraj(nazwa As String) As Kraj  '  find element from Kraje list where nazwa will match 
        Dim ListItem = From v In WalutyModel.Kraje Where v.Nazwa = nazwa
        Return ListItem.First 'copy 


    End Function

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

End Class
