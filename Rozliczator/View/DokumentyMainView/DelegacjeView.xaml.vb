Public Class DelegacjeView
    'Declare instance of ViewModel to hold instance
    Public Shared WithEvents VM As New DelegacjeViewModel

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
            VM = Startup.VMLocator.VMDelegacje
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel

            DataGridDelegacje.ItemsSource = VM.DelegacjeDataView  'Bind the datagrid to table
            DelegowanyComboBox.ItemsSource = VM.Delegowany
            ZaplaconoComboBox.ItemsSource = VM.ZaplaconoList
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub Button_Delegacje(sender As Object, e As RoutedEventArgs)
        Dim DodajDelegacje = New DodajDelegacjeView()
        DodajDelegacje.Show()
    End Sub

    Private Sub Button_Wczytaj(sender As Object, e As RoutedEventArgs)
        VM.ReadTable()
    End Sub

    Private Sub Button_Zapisz(sender As Object, e As RoutedEventArgs)
        VM.SaveTable()
    End Sub

    Private Sub DataGrid_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        e.Handled = True    ' to fix new window going behind
        VM.DelegacjaRowView = DataGridDelegacje.SelectedItem

        Dim DodajDelegacje = New DodajDelegacjeView(VM.DelegacjaRowView.Row)
        DodajDelegacje.Show()

        DodajDelegacje.Activate()

    End Sub
    Private Sub DataGrid_RClick_Dodaj(sender As Object, e As RoutedEventArgs)

        Dim DodajDelegacje = New DodajDelegacjeView()
        DodajDelegacje.Show()

    End Sub

    Private Sub DataGrid_RClick_Edytuj(sender As Object, e As RoutedEventArgs)
        VM.DelegacjaRowView = DataGridDelegacje.SelectedItem
        If VM.DelegacjaRowView IsNot Nothing Then
            Dim DodajDelegacje = New DodajDelegacjeView(VM.DelegacjaRowView.Row)
            DodajDelegacje.Show()
        End If


    End Sub

    Private Sub DataGrid_RClick_Usun(sender As Object, e As RoutedEventArgs)
        VM.DelegacjaRowView = DataGridDelegacje.CurrentItem
        If VM.DelegacjaRowView IsNot Nothing Then
            VM.RemoveDelegacja(VM.DelegacjaRowView.Row)
        End If
        ' MessageBox.Show("Czy oby na pewno?")
        ' Dim DodajFaktureKosztowa = New DodajFaktureKosztowaView(VM.FakturaKosztowaRowView.Row)
        '  DodajFaktureKosztowa.Show()

    End Sub

    Private Sub Button_Reset(sender As Object, e As RoutedEventArgs)
        'reset dataview filter
        DelegowanyComboBox.Text = ""     'just to keep it nice and tidy after filter reset

        VM.ResetFilters()


    End Sub

End Class
