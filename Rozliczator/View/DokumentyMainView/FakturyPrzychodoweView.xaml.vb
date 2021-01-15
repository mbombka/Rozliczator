Imports System.Data
Public Class FakturyPrzychodoweView
    'Declare instance of ViewModel to hold instance
    Public Shared WithEvents VM As New FakturyPrzychodoweViewModel

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
            VM = Startup.VMLocator.VMFakturyPrzychodowe
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel

            DataGridPrzychodowe.ItemsSource = VM.FakturyPrzychodoweDataView  'Bind the datagrid to table
            KlientComboBox.ItemsSource = VM.KlientList
            ZaplaconoComboBox.ItemsSource = VM.ZaplaconoList
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub




    Private Sub Button_Wczytaj(sender As Object, e As RoutedEventArgs)
        VM.ReadTable()
    End Sub

    Private Sub Button_DodajFakture(sender As Object, e As RoutedEventArgs)
        Dim DodajFakturePrzychodowa = New DodajFakturePrzychodowaView()
        DodajFakturePrzychodowa.Show()
    End Sub

    Private Sub Button_Zapisz(sender As Object, e As RoutedEventArgs)
        VM.SaveTable()
    End Sub


    Private Sub DataGrid_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        e.Handled = True    ' to fix new window going behind
        VM.FakturaPrzychodowaRowView = DataGridPrzychodowe.SelectedItem

        Dim DodajFakturePrzychodowa = New DodajFakturePrzychodowaView(VM.FakturaPrzychodowaRowView.Row)
        DodajFakturePrzychodowa.Show()


    End Sub
    Private Sub DataGrid_RClick_Dodaj(sender As Object, e As RoutedEventArgs)

        Dim DodajFakturePrzychodowa = New DodajFakturePrzychodowaView()
        DodajFakturePrzychodowa.Show()

    End Sub

    Private Sub DataGrid_RClick_Edytuj(sender As Object, e As RoutedEventArgs)
        VM.FakturaPrzychodowaRowView = DataGridPrzychodowe.SelectedItem
        If VM.FakturaPrzychodowaRowView IsNot Nothing Then
            Dim DodajFakturePrzychodowa = New DodajFakturePrzychodowaView(VM.FakturaPrzychodowaRowView.Row)
            DodajFakturePrzychodowa.Show()
        End If
    End Sub

    Private Sub DataGrid_RClick_Usun(sender As Object, e As RoutedEventArgs)
        VM.FakturaPrzychodowaRowView = DataGridPrzychodowe.CurrentItem
        If VM.FakturaPrzychodowaRowView IsNot Nothing Then
            VM.RemoveFakturaPrzychodowa(VM.FakturaPrzychodowaRowView.Row)
        End If
        ' MessageBox.Show("Czy oby na pewno?")
        ' Dim DodajFaktureKosztowa = New DodajFaktureKosztowaView(VM.FakturaKosztowaRowView.Row)
        '  DodajFaktureKosztowa.Show()

    End Sub

    Private Sub Button_Reset(sender As Object, e As RoutedEventArgs)
        'reset dataview filter
        KlientComboBox.Text = ""     'just to keep it nice and tidy after filter reset

        VM.ResetFilters()


    End Sub
End Class
