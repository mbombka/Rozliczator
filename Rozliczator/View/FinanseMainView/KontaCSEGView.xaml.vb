Public Class KontaCSEGView
    'Declare instance of ViewModel to hold instance
    Public Shared WithEvents VM As New KontaCSEGViewModel



    Public Sub New()



        Try
            Me.InitializeComponent()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        ' Insert code required on object creation below this point.

        Try
            VM = Startup.VMLocator.VMKontaCSEG
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel

            DataGridKontaCSEG.ItemsSource = VM.KontaCSEGDataView  'Bind the datagrid to table
            OperacjeComboBox.ItemsSource = KsiegowyModel.OperacjeNaKoncieCSEG

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        VM.RecznaOperacjaCSEG()
    End Sub
End Class
