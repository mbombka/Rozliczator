Imports System.ComponentModel
Imports System.Data
Public Class FakturaPrzychodowa
    Implements INotifyPropertyChanged

    Public Sub New()
        DataWystawienia = DateTime.Now.Date
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

    Private _NumerFaktury As String
    Public Property NumerFaktury() As String
        Get
            Return _NumerFaktury
        End Get

        Set(ByVal value As String)
            _NumerFaktury = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("NumerFaktury"))
        End Set
    End Property

    Private _Klient As String
    Public Property Klient() As String
        Get
            Return _Klient
        End Get

        Set(ByVal value As String)
            _Klient = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Klient"))
        End Set
    End Property

    Private _DataWystawienia As Date
    Public Property DataWystawienia() As Date
        Get
            Return _DataWystawienia
        End Get

        Set(ByVal value As Date)
            _DataWystawienia = value
            KursZDnia = KursZDnia  'update field KursZdnia
            KwotaPLN = KwotaPLN 'refresh value of KwotaPLN
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DataWystawienia"))
        End Set
    End Property

    Private _Opis As String
    Public Property Opis() As String
        Get
            Return _Opis
        End Get

        Set(ByVal value As String)
            _Opis = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Opis"))
        End Set
    End Property

    Private _Kwota As Decimal
    Public Property Kwota() As Decimal
        Get
            Return _Kwota
        End Get

        Set(ByVal value As Decimal)
            _Kwota = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Kwota"))
        End Set
    End Property

    Private _StawkaVAT As Integer
    Public Property StawkaVAT() As Integer
        Get
            Return _StawkaVAT
        End Get

        Set(ByVal value As Integer)
            _StawkaVAT = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("StawkaVAT"))
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

    Private _KwotaPLN As Decimal
    Public Property KwotaPLN() As Decimal
        Get
            _KwotaPLN = Kwota * _KursZDnia  'calculate Kwota PLN

            Return _KwotaPLN
        End Get

        Set(ByVal value As Decimal)
            _KwotaPLN = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KwotaPLN"))
        End Set
    End Property

    Private _Waluta As String
    Public Property Waluta() As String
        Get
            Return _Waluta
        End Get

        Set(ByVal value As String)
            _Waluta = value
            KursZDnia = KursZDnia   'refresh value of Kurs z dnia
            KwotaPLN = KwotaPLN 'refresh value of KwotaPLN
            WalutaPLN = WalutaPLN 'refresh value of WalutaPLN to change visibility of some objects
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Waluta"))
        End Set
    End Property

    Private _KursZDnia As Decimal
    Public Property KursZDnia() As Decimal
        Get
            _KursZDnia = WalutyModel.KursZDnia(DataWystawienia, Waluta)    'update field KursZdnia
            Return _KursZDnia
        End Get

        Set(ByVal value As Decimal)
            _KursZDnia = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KursZDnia"))
        End Set
    End Property

    Private _Zaplacono As Boolean
    Public Property Zaplacono() As Boolean
        Get
            Return _Zaplacono
        End Get

        Set(ByVal value As Boolean)
            _Zaplacono = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Zaplacono"))
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



    Private _CzyjZysk As String
    Public Property CzyjZysk() As String
        Get
            Return _CzyjZysk
        End Get

        Set(ByVal value As String)
            _CzyjZysk = value
            OsobaCSEG = OsobaCSEG 'update visibility of some objects
            UmowyListOfOsoba = UmowyListOfOsoba
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CzyjZysk"))
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

    Private Property _OsobaCSEG As Visibility
    Public Property OsobaCSEG As Visibility
        Get
            If CzyjZysk = "CSEG" Then
                _OsobaCSEG = Visibility.Hidden
            Else
                _OsobaCSEG = Visibility.Visible
            End If
            Return _OsobaCSEG
        End Get
        Set(value As Visibility)
            _OsobaCSEG = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("OsobaCSEG"))
        End Set
    End Property

    'create list of umowao dzielo but only for selected person
    Private Property _UmowyListOfOsoba As List(Of Object)
    Public Property UmowyListOfOsoba As List(Of Object)
        Get

            Dim ValuetoReturn = (From Rows In Startup.VMLocator.VMUmowyDzielo.UmowyDzieloTable.AsEnumerable()
                                 Where Rows("Osoba") = CzyjZysk
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


    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
