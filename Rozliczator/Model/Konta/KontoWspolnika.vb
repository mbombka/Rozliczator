Imports System.ComponentModel
Imports System.Data
Public Class KontoWspolnika
    Implements INotifyPropertyChanged


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

    'ktory wspolnik
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

    'stan zadłużenia CSEG u danego wspólnika albo inaczej ile pieniędzy na koncie CSEG należy bezpośrednio do danego wspólnika
    Private _Total As Decimal
    Public Property Total() As Decimal
        Get
            Return _Total
        End Get
        Set(ByVal value As Decimal)
            _Total = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Total"))
        End Set
    End Property



    'stan  wirtualnego sub konta delegacje - Czyli kwota diet służbowych pozostała do wypłacenia delikwentowi
    Private _SubDelegacje As Decimal
    Public Property SubDelegacje() As Decimal
        Get
            Return _SubDelegacje
        End Get
        Set(ByVal value As Decimal)
            _SubDelegacje = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SubDelegacje"))
        End Set
    End Property

    'stan  wirtualnego sub konta zwroty - kwota wszystkich rachunków/faktur które powinny być zwrócone przez CSEG
    Private _SubZwroty As Decimal
    Public Property SubZwroty() As Decimal
        Get
            Return _SubZwroty
        End Get
        Set(ByVal value As Decimal)
            _SubZwroty = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SubZwroty"))
        End Set
    End Property

    'stan  wirtualnego sub konta wszystkich umów(dzieło, zlecenie) oraz innych (np faktura wystawiona przez ACAB) - kwota do wypłaty. 
    Private _SubUmowy As Decimal
    Public Property SubUmowy() As Decimal
        Get
            Return _SubUmowy
        End Get
        Set(ByVal value As Decimal)
            _SubUmowy = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SubUmowy"))
        End Set
    End Property

    'rodzaj ostatniej wykonanej operacji np 'Konto PLN +500" lub "SubKontoVAT -1410" 'kusilo by zastosowac notacje odwrotna i pisac 500+ ...
    Private _Operacja As String
    Public Property Operacja() As String
        Get
            Return _Operacja
        End Get

        Set(ByVal value As String)
            _Operacja = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Operacja"))
        End Set
    End Property

    'na którym koncie została przeprowadzona operacja
    Private _ZKonta As String
    Public Property ZKonta() As String
        Get
            Return _ZKonta
        End Get
        Set(ByVal value As String)
            _ZKonta = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ZKonta"))
        End Set
    End Property

    'kwota ostatnniej wykonanej operacji
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

    'Opis ostatniej wykonanej operacji np 'na waciki dla Halinki z II oddziału Urzędu Skarbowego w Krakowie Krowodrza'
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










    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
#End Region
End Class
