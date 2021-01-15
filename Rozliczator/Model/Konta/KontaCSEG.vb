Imports System.ComponentModel
Imports System.Data
Public Class KontaCSEG
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

    'stan głównego konta złotówkowego , powinien odpowiadać rzeczywistemu saldo w banku
    Private _KontoPLN As Decimal
    Public Property KontoPLN() As Decimal
        Get
            Return _KontoPLN
        End Get
        Set(ByVal value As Decimal)
            _KontoPLN = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontoPLN"))
        End Set
    End Property

    'stan konta euro w banku, powinien odpowiadać rzeczywistemu saldo w banku
    Private _KontoEUR As Decimal
    Public Property KontoEUR() As Decimal
        Get
            Return _KontoEUR
        End Get
        Set(ByVal value As Decimal)
            _KontoEUR = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontoEUR"))
        End Set
    End Property

    'stan konta GBP w banku, powinien odpowiadać rzeczywistemu saldo w banku
    Private _KontoGBP As Decimal
    Public Property KontoGBP() As Decimal
        Get
            Return _KontoGBP
        End Get
        Set(ByVal value As Decimal)
            _KontoGBP = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontoGBP"))
        End Set
    End Property

    'stan  wirtualnego sub konta spółki - #rachunek bieżący. Czyli ile z pieniędzy na koncie głównym należy do spółki
    Private _SubKontoSpolka As Decimal
    Public Property SubKontoSpolka() As Decimal
        Get
            Return _SubKontoSpolka
        End Get
        Set(ByVal value As Decimal)
            _SubKontoSpolka = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SubKontoSpolka"))
        End Set
    End Property

    'stan  wirtualnego sub konta wspolnikow - Czyli ile z pieniędzy na koncie głównym należy do wspólników
    Private _SubKontoWspolnicy As Decimal
    Public Property SubKontoWspolnicy() As Decimal
        Get
            Return _SubKontoWspolnicy
        End Get
        Set(ByVal value As Decimal)
            _SubKontoWspolnicy = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SubKontoWspolnicy"))
        End Set
    End Property

    'stan  wirtualnego sub konta VAT - Czyli ile z pieniędzy na koncie głównym jest VATem (czyli trzeba będzie wydać w fakturach lub zapłacić do US
    Private _SubKontoVAT As Decimal
    Public Property SubKontoVAT() As Decimal
        Get
            Return _SubKontoVAT
        End Get
        Set(ByVal value As Decimal)
            _SubKontoVAT = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SubKontoVAT"))
        End Set
    End Property

    'stan  wirtualnego sub konta CIT - prognozowany podatek CIT do zapłacenia ( przychody-koszty, narastająco)
    Private _SubKontoCIT As Decimal
    Public Property SubKontoCIT() As Decimal
        Get
            Return _SubKontoCIT
        End Get
        Set(ByVal value As Decimal)
            _SubKontoCIT = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SubKontoCIT"))
        End Set
    End Property

    'stan  wirtualnego sub konta PIT - prognozowany podatek PIT do zapłacenia ( m.in podatek od umów o dzieło, spółka płaci go tylko w imieniu zleceniobiorcy)
    Private _SubKontoPIT As Decimal
    Public Property SubKontoPIT() As Decimal
        Get
            Return _SubKontoPIT
        End Get
        Set(ByVal value As Decimal)
            _SubKontoPIT = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SubKontoPIT"))
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
