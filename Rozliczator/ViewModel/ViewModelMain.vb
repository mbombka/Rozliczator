Imports System.ComponentModel


Public Class ViewModelMain
    Implements INotifyPropertyChanged
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region
    Private Property _FinanseCommand As New BaseCommand(AddressOf OpenFinanse)
    Public ReadOnly Property FinanseCommand As BaseCommand
        Get
            Return _FinanseCommand
        End Get
    End Property

    Private Property _DokumentyCommand As New BaseCommand(AddressOf OpenDokumenty)
    Public ReadOnly Property DokumentyCommand As BaseCommand
        Get
            Return _DokumentyCommand
        End Get
    End Property

    Private Property _KontrahenciCommand As New BaseCommand(AddressOf OpenKontrahenci)
    Public ReadOnly Property KontrahenciCommand As BaseCommand
        Get
            Return _KontrahenciCommand
        End Get
    End Property




    Private Property _selectedViewModel As Object
    Public Property SelectedViewModel As Object
        Get
            Return _selectedViewModel
        End Get
        Set(value As Object)
            _selectedViewModel = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectedViewModel"))
        End Set
    End Property



    Public Sub New()
        SelectedViewModel = New FInanseMainViewModel()

    End Sub


    Private Sub OpenFinanse()
        SelectedViewModel = New FInanseMainViewModel()
    End Sub

    Private Sub OpenDokumenty()
        SelectedViewModel = New DokumentyMainViewModel()
    End Sub

    Private Sub OpenKontrahenci()
        SelectedViewModel = New KontrahenciMainViewModel()
    End Sub


End Class



