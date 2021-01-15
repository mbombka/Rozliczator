Imports System.Data
Public Class FakturyKosztoweView
    'Declare instance of ViewModel to hold instance
    Public Shared WithEvents VM As New FakturyKosztoweViewModel

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
            VM = Startup.VMLocator.VMFakturyKosztowe
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel


            DataGridKosztowe.ItemsSource = VM.FakturyKosztoweDataView '         VM.FakturyKosztoweTable.DefaultView.         RowFilter("Sprzedawca = 'brak1'")  'Bind the datagrid to table
            CzyjKosztComboBox.ItemsSource = VM.Osoby
            SprzedawcaComboBox.ItemsSource = VM.SprzedawcyList
            ZaplaconoComboBox.ItemsSource = VM.ZaplaconoList
            '  VM.FilterDataOd = DateTime.Now.Date
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub Button_DodajFakture(sender As Object, e As RoutedEventArgs)
        Dim DodajFaktureKosztowa = New DodajFaktureKosztowaView()
        DodajFaktureKosztowa.Show()
    End Sub

    Private Sub Button_Wczytaj(sender As Object, e As RoutedEventArgs)
        VM.ReadTable()
    End Sub

    Private Sub Button_Zapisz(sender As Object, e As RoutedEventArgs)
        VM.SaveTable()
    End Sub


    Private Sub DataGridKosztowe_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        e.Handled = True    ' to fix new window going behind
        VM.FakturaKosztowaRowView = DataGridKosztowe.SelectedItem

        Dim DodajFaktureKosztowa = New DodajFaktureKosztowaView(VM.FakturaKosztowaRowView.Row)
        DodajFaktureKosztowa.Show()

    End Sub
    Private Sub DataGridKosztowe_RClick_Dodaj(sender As Object, e As RoutedEventArgs)

        Dim DodajFaktureKosztowa = New DodajFaktureKosztowaView()
        DodajFaktureKosztowa.Show()

    End Sub

    Private Sub DataGridKosztowe_RClick_Edytuj(sender As Object, e As RoutedEventArgs)
        VM.FakturaKosztowaRowView = DataGridKosztowe.SelectedItem
        If VM.FakturaKosztowaRowView IsNot Nothing Then
            Dim DodajFaktureKosztowa = New DodajFaktureKosztowaView(VM.FakturaKosztowaRowView.Row)
            DodajFaktureKosztowa.Show()
        End If
    End Sub

    Private Sub DataGridKosztowe_RClick_Usun(sender As Object, e As RoutedEventArgs)
        VM.FakturaKosztowaRowView = DataGridKosztowe.CurrentItem
        If VM.FakturaKosztowaRowView IsNot Nothing Then
            VM.RemoveFakturaKosztowa(VM.FakturaKosztowaRowView.Row)
        End If
        ' MessageBox.Show("Czy oby na pewno?")
        ' Dim DodajFaktureKosztowa = New DodajFaktureKosztowaView(VM.FakturaKosztowaRowView.Row)
        '  DodajFaktureKosztowa.Show()

    End Sub

    Private Sub Button_Reset(sender As Object, e As RoutedEventArgs)
        'reset dataview filter
        CzyjKosztComboBox.Text = ""     'just to keep it nice and tidy after filter reset
        SprzedawcaComboBox.Text = ""     'just to keep it nice and tidy after filter reset
        VM.ResetFilters()


    End Sub
End Class
