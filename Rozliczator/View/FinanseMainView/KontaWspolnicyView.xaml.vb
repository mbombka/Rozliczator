Public Class KontaWspolnicyView
    'Declare instance of ViewModel to hold instance
    Public Shared WithEvents VM As New KontaWspolnicyViewModel

    Public Sub New()

        Try
            Me.InitializeComponent()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        ' Insert code required on object creation below this point.

        Try
            VM = Startup.VMLocator.VMKontaWspolnicy

            VM.FillKonta()
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel

            OperacjeComboBox.ItemsSource = KsiegowyModel.OperacjeNaKoncieWspolnika
            OsobaComboBox.ItemsSource = KsiegowyModel.Wspolnicy
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        VM.RecznaOperacja()
    End Sub
End Class
