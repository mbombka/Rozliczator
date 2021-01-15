Imports System.ComponentModel

'************** help tools for graphic*****************************

Public Class GraphicHelper

End Class
Public Class Arc
    Implements INotifyPropertyChanged
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region

    Private _ArcStart As Point
    Public Property ArcStart() As Point
        Get
            Return _ArcStart
        End Get
        Set(ByVal value As Point)
            _ArcStart = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ArcStart"))
        End Set
    End Property
    Private _ArcEnd As Point
    Public Property ArcEnd() As Point
        Get
            Return _ArcEnd
        End Get
        Set(ByVal value As Point)
            _ArcEnd = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ArcEnd"))
        End Set
    End Property
End Class
