Imports System.IO
Imports Microsoft.Win32

Public Class DodajUmowaDzieloView
    Public WithEvents VM As New UmowyDzieloViewModel
    Public UmowaDzieloHandle As UmowaDzielo = New UmowaDzielo()
    Public UmowaDzielo As UmowaDzielo
    Public NowaUmowaOdzielo As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMUmowyDzielo


            Me.DataContext = UmowaDzieloHandle    'Set the DataContext of the Page to the ViewModel
            VM.UmowaDzielo = UmowaDzieloHandle  'to pass object umowa o dzielo for tableview in viewmodel



            '*****  set itemsource for controls
            OsobaComboBox.ItemsSource = VM.ZleceniobiorcaList
            KosztyComboBox.ItemsSource = KsiegowyModel.KosztyUzyskPrzychList
            ProgPodatkowyComboBox.ItemsSource = KsiegowyModel.ProgiPodatkowe
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            DataGridPrzychodowe.ItemsSource = VM.FakturyPrzychodoweDataView
            DataGridDelegacje.ItemsSource = VM.DelegacjeDataView
            DataGridKosztowe.ItemsSource = VM.FakturyKosztoweDataView
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM


            NowaUmowaOdzielo = True    'set flag that this is new delegacja

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Public Sub New(_DataRow As Data.DataRow)    'exact the same as above but with added function of filling object faktury kosztowe with data from datarow ( selected from datagrid)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMUmowyDzielo


            Me.DataContext = UmowaDzieloHandle    'Set the DataContext of the Page to the ViewModel

            DataBaseModel.FillUmowaDzielo(_DataRow, UmowaDzieloHandle)  'fill object ' faktura kosztowa' with values from selected row from datagrid
            VM.UmowaDzielo = UmowaDzieloHandle  'to pass object umowa o dzielo for tableview in viewmodel



            '*****  set itemsource for controls
            OsobaComboBox.ItemsSource = VM.ZleceniobiorcaList
            KosztyComboBox.ItemsSource = KsiegowyModel.KosztyUzyskPrzychList
            ProgPodatkowyComboBox.ItemsSource = KsiegowyModel.ProgiPodatkowe


            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            DataGridPrzychodowe.ItemsSource = VM.FakturyPrzychodoweDataView
            DataGridDelegacje.ItemsSource = VM.DelegacjeDataView
            DataGridKosztowe.ItemsSource = VM.FakturyKosztoweDataView
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM


            NowaUmowaOdzielo = False    'set flag that this is not new 
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try


    End Sub


    Private Sub Button_Zapisz(sender As Object, e As RoutedEventArgs)
        If NowaUmowaOdzielo And VM.UmowyList.Contains(NumerUmowy.Text) Then    'check if during adding new delegacja, already exist delegacja with the same unique key
            MessageBox.Show("Numer Umowy już istnieje")
        Else
            If VM.AddUmowaDzielo(UmowaDzieloHandle) Then
                Close()
            End If
        End If

    End Sub

#Region "actions for embeded tables"
    '********************handling datatables in new window of umowa o dzielo*************

    Private Sub Przychodowe_RClick_Dodaj(sender As Object, e As RoutedEventArgs)

        Dim DodajPrzychodowe = New DodajFakturePrzychodowaView(UmowaDzieloHandle.Osoba, UmowaDzieloHandle.NumerUmowy)
        DodajPrzychodowe.Show()

    End Sub

    Private Sub Przychodowe_RClick_Edytuj(sender As Object, e As RoutedEventArgs)
        Dim _RowView As System.Data.DataRowView = DataGridPrzychodowe.CurrentItem

        Dim DodajPrzychodowe = New DodajFakturePrzychodowaView(_RowView.Row)
        DodajPrzychodowe.Show()

    End Sub

    Private Sub Przychodowe_RClick_Usun(sender As Object, e As RoutedEventArgs)
        Dim _RowView As System.Data.DataRowView = DataGridPrzychodowe.CurrentItem
        Startup.VMLocator.VMFakturyPrzychodowe.RemoveFakturaPrzychodowa(_RowView.Row)

    End Sub

    Private Sub Kosztowe_RClick_Dodaj(sender As Object, e As RoutedEventArgs)

        Dim DodajKosztowa = New DodajFaktureKosztowaView(UmowaDzieloHandle.Osoba, UmowaDzieloHandle.NumerUmowy)
        DodajKosztowa.Show()

    End Sub

    Private Sub Kosztowe_RClick_Edytuj(sender As Object, e As RoutedEventArgs)
        Dim _RowView As System.Data.DataRowView = DataGridKosztowe.CurrentItem
        If _RowView IsNot Nothing Then
            Dim DodajKosztowa = New DodajFaktureKosztowaView(_RowView.Row)
            DodajKosztowa.Show()
        End If
    End Sub

    Private Sub Kosztowe_RClick_Usun(sender As Object, e As RoutedEventArgs)

        Dim _RowView As System.Data.DataRowView = DataGridKosztowe.CurrentItem
        If _RowView IsNot Nothing Then
            Startup.VMLocator.VMFakturyKosztowe.RemoveFakturaKosztowa(_RowView.Row)
            '  VM.RemoveDelegacja(VM.DelegacjaRowView.Row)
        End If

    End Sub
    Private Sub Delegacje_RClick_Dodaj(sender As Object, e As RoutedEventArgs)
        Dim DodajDelegacje = New DodajDelegacjeView(UmowaDzieloHandle.Osoba, UmowaDzieloHandle.NumerUmowy)
        DodajDelegacje.Show()

    End Sub

    Private Sub Delegacje_RClick_Edytuj(sender As Object, e As RoutedEventArgs)
        Dim _RowView As System.Data.DataRowView = DataGridDelegacje.CurrentItem
        If _RowView IsNot Nothing Then
            Dim DodajDelegacje = New DodajDelegacjeView(_RowView.Row)
            DodajDelegacje.Show()
        End If
    End Sub

    Private Sub Delegacje_RClick_Usun(sender As Object, e As RoutedEventArgs)
        Dim _RowView As System.Data.DataRowView = DataGridDelegacje.CurrentItem
        If _RowView IsNot Nothing Then
            Startup.VMLocator.VMDelegacje.RemoveDelegacja(_RowView.Row)
        End If
    End Sub

    Private Sub Button_Przelicz(sender As Object, e As RoutedEventArgs)
        KsiegowyModel.PrzeliczUmowe(UmowaDzieloHandle)

    End Sub

    '*********************obsługa plikow ********
    'drop Plik1 in image
    Private Sub Plik1_Drop(sender As Object, e As DragEventArgs)
        Dim filestring() As String = e.Data.GetData(DataFormats.FileDrop)
        If filestring.GetLength(0) > 1 Then
            MessageBox.Show("opanuj się... pojedynczo")
        ElseIf UmowaDzieloHandle.NumerUmowy = "" Then
            MessageBox.Show("Uzupełnij najpierw numer umowy")
        Else
            VM.UploadFile1ToFTP(filestring(0), UmowaDzieloHandle)
        End If
    End Sub

    Private Sub RClikck_Plik1Dodaj(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = True Then
            If UmowaDzieloHandle.NumerUmowy = "" Then
                MessageBox.Show("Uzupełnij najpierw numer  umowy")
            Else
                VM.UploadFile1ToFTP(openFileDialog.FileName, UmowaDzieloHandle)
            End If
        End If
    End Sub

    Private Sub RClikck_Plik1Usun(sender As Object, e As RoutedEventArgs)
        If UmowaDzieloHandle.Plik1 <> "" Then
            VM.DeleteFile1FromFTP(UmowaDzieloHandle)
        End If
    End Sub

    Private Sub RClikck_Plik1Pobierz(sender As Object, e As RoutedEventArgs)
        If UmowaDzieloHandle.Plik1 <> "" Then
            Dim saveFileDialog = New SaveFileDialog()
            saveFileDialog.Title = "Zapisz plik jako"
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(UmowaDzieloHandle.Plik1)
            saveFileDialog.DefaultExt = Path.GetExtension(UmowaDzieloHandle.Plik1)

            If saveFileDialog.ShowDialog() = True Then
                VM.DownoadFileFromFTP(saveFileDialog.FileName, UmowaDzieloHandle.Plik1)
            End If
        End If

    End Sub

    'drop Plik2 in image
    Private Sub Plik2_Drop(sender As Object, e As DragEventArgs)
        Dim filestring() As String = e.Data.GetData(DataFormats.FileDrop)
        If filestring.GetLength(0) > 1 Then
            MessageBox.Show("opanuj się... pojedynczo")
        ElseIf UmowaDzieloHandle.NumerUmowy = "" Then
            MessageBox.Show("Uzupełnij najpierw numer umowy")
        Else
            VM.UploadFile2ToFTP(filestring(0), UmowaDzieloHandle)
        End If
    End Sub

    Private Sub RClikck_Plik2Dodaj(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = True Then
            If UmowaDzieloHandle.NumerUmowy = "" Then
                MessageBox.Show("Uzupełnij najpierw numer umowy")
            Else
                VM.UploadFile2ToFTP(openFileDialog.FileName, UmowaDzieloHandle)
            End If
        End If
    End Sub

    Private Sub RClikck_Plik2Usun(sender As Object, e As RoutedEventArgs)
        If UmowaDzieloHandle.Plik2 <> "" Then
            VM.DeleteFile2FromFTP(UmowaDzieloHandle)
        End If
    End Sub

    Private Sub RClikck_Plik2Pobierz(sender As Object, e As RoutedEventArgs)
        If UmowaDzieloHandle.Plik2 <> "" Then
            Dim saveFileDialog = New SaveFileDialog()
            saveFileDialog.Title = "Zapisz plik jako"
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(UmowaDzieloHandle.Plik2)
            saveFileDialog.DefaultExt = Path.GetExtension(UmowaDzieloHandle.Plik2)

            If saveFileDialog.ShowDialog() = True Then
                VM.DownoadFileFromFTP(saveFileDialog.FileName, UmowaDzieloHandle.Plik2)
            End If
        End If

    End Sub

    Private Sub PrzeliczBrutto(sender As Object, e As RoutedEventArgs)
        KsiegowyModel.PrzeliczUmoweBrutto(UmowaDzieloHandle)
    End Sub





#End Region
End Class
