Public Class KontrahenciView
    'Declare instance of ViewModel to hold instance
    Public Shared WithEvents VM As New KontrahenciViewModel

    'Pass the ViewModel in the Constructor

    Public Sub New()

        MyBase.New()

        Try
            Me.InitializeComponent()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        ' Insert code required on object creation below this point.

        Try
            VM = Startup.VMLocator.VMKontrahenci
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel

            DataGridKontrahenci.ItemsSource = VM.KontrahenciDataView  'Bind the datagrid to table

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub



    Private Sub Button_Wczytaj(sender As Object, e As RoutedEventArgs)
        VM.ReadTable()
    End Sub

    Private Sub Button_Zapisz(sender As Object, e As RoutedEventArgs)
        VM.SaveTable()
    End Sub
End Class
