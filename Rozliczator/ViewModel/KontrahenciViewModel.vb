Imports System.Data
Imports System.ComponentModel

Public Class KontrahenciViewModel

    Implements INotifyPropertyChanged
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region

#Region "Properties"


    '********************************************************************************
    Private _KontrahenciDataTable As DataTable

    Public Property KontrahenciDataTable() As DataTable
        Get
            Return _KontrahenciDataTable
        End Get

        Set(ByVal value As DataTable)
            _KontrahenciDataTable = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontrahenciDataTable"))
        End Set
    End Property

    Private _KontrahenciDataView As DataView
    Public Property KontrahenciDataView As DataView
        Get
            _KontrahenciDataView = New DataView(KontrahenciDataTable)

            Return _KontrahenciDataView
        End Get
        Set(value As DataView)
            _KontrahenciDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontrahenciDataView"))
        End Set
    End Property

    Public ReadOnly Property KontrahenciList As List(Of Object)
        Get
            Dim ValuetoReturn = (From Rows In _KontrahenciDataTable.AsEnumerable()
                                 Select Rows("NazwaFirmy")).Distinct().ToList()
            KontrahenciList = ValuetoReturn

            Return KontrahenciList
        End Get
    End Property
#End Region

#Region "Subs"

    Public Sub ReadTable()
        Startup.MainDataBaseModel.ReadFromDatabase("Kontrahenci")
    End Sub
    Public Sub SaveTable()
        Startup.MainDataBaseModel.SaveTableToDatabase("Kontrahenci")
    End Sub
#End Region
End Class
