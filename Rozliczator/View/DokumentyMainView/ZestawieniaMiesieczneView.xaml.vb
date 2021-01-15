Public Class ZestawieniaMiesieczneView
    Public Shared WithEvents VM As New ZestawieniaMiesieczneViewModel

    Public Sub New()


        MyBase.New()

        Try
            Me.InitializeComponent()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        ' Insert code required on object creation below this point.

        Try
            VM = Startup.VMLocator.VMZestawieniaMiesieczne
            Me.DataContext = VM     'Set the DataContext of the Page to the ViewModel


            OsobaComboBox.ItemsSource = KsiegowyModel.Wspolnicy
            MiesiacComboBox.ItemsSource = KsiegowyModel.Miesiace

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub



    Private Sub Eksportuj_Click(sender As Object, e As RoutedEventArgs)
        If VM.Miesiac <> "" And VM.Osoba <> "" Then
            VM.ZestawienieToExcel(VM.ZestawieniaDataView)
        Else
            Dim newMSG = New MessageBoxCustom("Ale czyje i za jaki miesiąc?")
            newMSG.Show()
        End If
    End Sub

    Private Sub Kosztowe_RClick_Dodaj(sender As Object, e As RoutedEventArgs)

        Dim DodajKosztowa = New DodajFaktureKosztowaView()
        DodajKosztowa.Show()

    End Sub

    Private Sub Kosztowe_RClick_Edytuj(sender As Object, e As RoutedEventArgs)

        Dim _RowView As System.Data.DataRowView = DataGridZestawienie.CurrentItem
        If _RowView IsNot Nothing Then
            Dim DodajKosztowa = New DodajFaktureKosztowaView(_RowView.Row)
            DodajKosztowa.Show()
        End If


    End Sub

    Private Sub Kosztowe_RClick_Usun(sender As Object, e As RoutedEventArgs)

        Dim _RowView As System.Data.DataRowView = DataGridZestawienie.CurrentItem
        If _RowView IsNot Nothing Then
            Startup.VMLocator.VMFakturyKosztowe.RemoveFakturaKosztowa(_RowView.Row)
        End If

    End Sub


End Class
