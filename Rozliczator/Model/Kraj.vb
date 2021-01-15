Imports System.ComponentModel

Public Class Kraj
    Implements INotifyPropertyChanged

    Private _Nazwa As String
    Public Property Nazwa() As String
        Get
            Return _Nazwa
        End Get

        Set(ByVal value As String)
            _Nazwa = value

            Select Case _Nazwa
                Case "Polska"

            End Select


            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Nazwa"))
        End Set
    End Property

    Private _StawkaDiety As Decimal
    Public Property StawkaDiety() As Decimal
        Get
            Return _StawkaDiety
        End Get

        Set(ByVal value As Decimal)
            _StawkaDiety = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("StawkaDiety"))
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
