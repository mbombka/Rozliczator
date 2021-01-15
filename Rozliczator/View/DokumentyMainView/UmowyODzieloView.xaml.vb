Public Class UmowyODzieloView
    'Declare instance of ViewModel to hold instance
    Public Shared WithEvents VM As New UmowyDzieloViewModel

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
            VM = Startup.VMLocator.VMUmowyDzielo
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel

            DataGridUmowy.ItemsSource = VM.UmowaDzieloDataView 'Bind the datagrid to table
            OsobaComboBox.ItemsSource = KsiegowyModel.Wspolnicy
            ZaplaconoComboBox.ItemsSource = VM.ZaplaconoList
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub Button_Zapisz(sender As Object, e As RoutedEventArgs)
        VM.SaveTable()
    End Sub

    Private Sub Button_Wczytaj(sender As Object, e As RoutedEventArgs)
        VM.ReadTable()
    End Sub

    Private Sub Button_DodajUmowe(sender As Object, e As RoutedEventArgs)
        Dim DodajUmoweDzielo = New DodajUmowaDzieloView()
        DodajUmoweDzielo.Show()
    End Sub



    Private Sub DataGridUmowyDzielo_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        e.Handled = True    ' to fix new window going behind
        VM.UmowaDzieloRowView = DataGridUmowy.SelectedItem
        Dim DodajUmoweDzielo = New DodajUmowaDzieloView(VM.UmowaDzieloRowView.Row)
        DodajUmoweDzielo.Show()

    End Sub
    Private Sub DataGrid_RClick_Dodaj(sender As Object, e As RoutedEventArgs)

        Dim DodajUmoweDzielo = New DodajUmowaDzieloView()
        DodajUmoweDzielo.Show()

    End Sub

    Private Sub DataGrid_RClick_Edytuj(sender As Object, e As RoutedEventArgs)
        VM.UmowaDzieloRowView = DataGridUmowy.SelectedItem
        If VM.UmowaDzieloRowView IsNot Nothing Then
            Dim DodajUmoweDzielo = New DodajUmowaDzieloView(VM.UmowaDzieloRowView.Row)
            DodajUmoweDzielo.Show()
        End If
    End Sub

    Private Sub DataGrid_RClick_Usun(sender As Object, e As RoutedEventArgs)
        VM.UmowaDzieloRowView = DataGridUmowy.CurrentItem
        If VM.UmowaDzieloRowView IsNot Nothing Then
            VM.RemoveUmowaDzielo(VM.UmowaDzieloRowView.Row)
        End If
        ' MessageBox.Show("Czy oby na pewno?")
    End Sub



    Private Sub Button_Reset(sender As Object, e As RoutedEventArgs)
        'reset dataview filter
        OsobaComboBox.Text = ""     'just to keep it nice and tidy after filter reset
        VM.ResetFilters()
    End Sub
End Class
