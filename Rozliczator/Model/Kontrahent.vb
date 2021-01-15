Imports System.ComponentModel
Public Class Kontrahent
    Implements INotifyPropertyChanged

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

    Private _NazwaFirmy As String
    Public Property NazwaFirmy() As String
        Get
            Return _NazwaFirmy
        End Get
        Set(ByVal value As String)
            _NazwaFirmy = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("NazwaFirmy"))
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

    Private _Stawka As Decimal
    Public Property Stawka() As Decimal
        Get
            Return _Stawka
        End Get
        Set(ByVal value As Decimal)
            _Stawka = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Stawka"))
        End Set
    End Property

    Private _Waluta As String
    Public Property Waluta() As String
        Get
            Return _Waluta
        End Get

        Set(ByVal value As String)
            _Waluta = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Waluta"))
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
