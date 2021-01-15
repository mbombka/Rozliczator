Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports Microsoft.Win32

Public Class DodajFaktureKosztowaView
    'Declare instance of ViewModel to hold instance
    Public WithEvents VM As New FakturyKosztoweViewModel
    Public FakturaKosztowaHandle As FakturaKosztowa = New FakturaKosztowa()


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMFakturyKosztowe


            Me.DataContext = FakturaKosztowaHandle 'Startup.VMLocator.VMFakturyKosztowe.FakturaKosztowa    'Set the DataContext of the Page to the ViewModel

            SprzedawcaComboBox.ItemsSource = VM.SprzedawcyList
            czyjKoszt.ItemsSource = VM.Osoby
            listaWalutComboBox.ItemsSource = VM.WalutyList
            OpisComboBox.ItemsSource = VM.OpisList
            listaVatCombobox.ItemsSource = KsiegowyModel.StawkiVAT
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM

            StackPanelKontr.DataContext = VM
            VM.KontrahentVisibility = Visibility.Collapsed
            IloscKontrahent.DataContext = VM
            Stawka.DataContext = VM
            WalutaKontrahent.DataContext = VM

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Sub New(_Osoba As String, _NumerUmowy As String) 'same as above but with filled czyjkoszt and numer umowy

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMFakturyKosztowe


            Me.DataContext = FakturaKosztowaHandle 'Startup.VMLocator.VMFakturyKosztowe.FakturaKosztowa    'Set the DataContext of the Page to the ViewModel
            FakturaKosztowaHandle.CzyjKoszt = _Osoba
            FakturaKosztowaHandle.NumerUmowy = _NumerUmowy
            SprzedawcaComboBox.ItemsSource = VM.SprzedawcyList
            czyjKoszt.ItemsSource = VM.Osoby
            listaWalutComboBox.ItemsSource = VM.WalutyList
            OpisComboBox.ItemsSource = VM.OpisList
            listaVatCombobox.ItemsSource = KsiegowyModel.StawkiVAT
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM
            KontrahentComboBox.DataContext = VM

            StackPanelKontr.DataContext = VM
            VM.KontrahentVisibility = Visibility.Collapsed
            IloscKontrahent.DataContext = VM
            Stawka.DataContext = VM
            WalutaKontrahent.DataContext = VM


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Public Sub New(_DataRow As Data.DataRow)    'exact the same as above but with added function of filling object faktury kosztowe with data from datarow ( selected from datagrid)


        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMFakturyKosztowe
            Me.DataContext = FakturaKosztowaHandle   'Set the DataContext of the Page to the ViewModel

            SprzedawcaComboBox.ItemsSource = VM.SprzedawcyList
            czyjKoszt.ItemsSource = VM.Osoby
            listaWalutComboBox.ItemsSource = VM.WalutyList
            OpisComboBox.ItemsSource = VM.OpisList
            listaVatCombobox.ItemsSource = KsiegowyModel.StawkiVAT
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM

            StackPanelKontr.DataContext = VM
            VM.KontrahentVisibility = Visibility.Collapsed
            IloscKontrahent.DataContext = VM
            Stawka.DataContext = VM
            WalutaKontrahent.DataContext = VM

            DataBaseModel.FillFakturaKosztowa(_DataRow, FakturaKosztowaHandle)  'fill object ' faktura kosztowa' with values from selected row from datagrid
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try


    End Sub


    Private Sub Button_Zapisz(sender As Object, e As RoutedEventArgs)
        If VM.AddfakturaKosztowa(FakturaKosztowaHandle) Then
            Close()
        End If
    End Sub

    '*********************obsługa plikow ********
    'drop Plik1 in image
    Private Sub Plik1_Drop(sender As Object, e As DragEventArgs)
        Dim filestring() As String = e.Data.GetData(DataFormats.FileDrop)
        If filestring.GetLength(0) > 1 Then
            MessageBox.Show("opanuj się... pojedynczo")
        ElseIf FakturaKosztowaHandle.NumerFaktury = "" Or FakturaKosztowaHandle.DataWystawienia.Year < 1999 Then
            MessageBox.Show("Uzupełnij najpierw numer faktury oraz date wystawienia")
        Else
            VM.UploadFile1ToFTP(filestring(0), FakturaKosztowaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik1Dodaj(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = True Then
            If FakturaKosztowaHandle.NumerFaktury = "" Or FakturaKosztowaHandle.DataWystawienia.Year < 1999 Then
                MessageBox.Show("Uzupełnij najpierw numer faktury oraz date wystawienia")
            Else
                VM.UploadFile1ToFTP(openFileDialog.FileName, FakturaKosztowaHandle)
            End If
        End If
    End Sub

    Private Sub RClikck_Plik1Usun(sender As Object, e As RoutedEventArgs)
        If FakturaKosztowaHandle.Plik1 <> "" Then
            VM.DeleteFile1FromFTP(FakturaKosztowaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik1Pobierz(sender As Object, e As RoutedEventArgs)
        If FakturaKosztowaHandle.Plik1 <> "" Then
            Dim saveFileDialog = New SaveFileDialog()
            saveFileDialog.Title = "Zapisz plik jako"
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(FakturaKosztowaHandle.Plik1)
            saveFileDialog.DefaultExt = Path.GetExtension(FakturaKosztowaHandle.Plik1)

            If saveFileDialog.ShowDialog() = True Then
                VM.DownoadFileFromFTP(saveFileDialog.FileName, FakturaKosztowaHandle.Plik1)
            End If
        End If

    End Sub

    'drop Plik2 in image
    Private Sub Plik2_Drop(sender As Object, e As DragEventArgs)
        Dim filestring() As String = e.Data.GetData(DataFormats.FileDrop)
        If filestring.GetLength(0) > 1 Then
            MessageBox.Show("opanuj się... pojedynczo")
        ElseIf FakturaKosztowaHandle.NumerFaktury = "" Or FakturaKosztowaHandle.DataWystawienia.Year < 1999 Then
            MessageBox.Show("Uzupełnij najpierw numer faktury oraz date wystawienia")
        Else
            VM.UploadFile2ToFTP(filestring(0), FakturaKosztowaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik2Dodaj(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = True Then
            If FakturaKosztowaHandle.NumerFaktury = "" Or FakturaKosztowaHandle.DataWystawienia.Year < 1999 Then
                MessageBox.Show("Uzupełnij najpierw numer faktury oraz date wystawienia")
            Else
                VM.UploadFile2ToFTP(openFileDialog.FileName, FakturaKosztowaHandle)
            End If
        End If
    End Sub

    Private Sub RClikck_Plik2Usun(sender As Object, e As RoutedEventArgs)
        If FakturaKosztowaHandle.Plik2 <> "" Then
            VM.DeleteFile2FromFTP(FakturaKosztowaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik2Pobierz(sender As Object, e As RoutedEventArgs)
        If FakturaKosztowaHandle.Plik2 <> "" Then
            Dim saveFileDialog = New SaveFileDialog()
            saveFileDialog.Title = "Zapisz plik jako"
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(FakturaKosztowaHandle.Plik2)
            saveFileDialog.DefaultExt = Path.GetExtension(FakturaKosztowaHandle.Plik2)

            If saveFileDialog.ShowDialog() = True Then
                VM.DownoadFileFromFTP(saveFileDialog.FileName, FakturaKosztowaHandle.Plik2)
            End If
        End If

    End Sub

    Private Sub Label_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)

    End Sub

    Private Sub SprzedawcaOpen_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        VM.KontrahentVisibility = Visibility.Visible
    End Sub

    Private Sub Button_PrzeliczKontr(sender As Object, e As RoutedEventArgs)
        FakturaKosztowaHandle.Sprzedawca = VM.Kontrahent
        FakturaKosztowaHandle.Kwota = VM.KontrahentIlosc * VM.KontrahentStawka
        FakturaKosztowaHandle.Waluta = VM.KontrahentWaluta

        Dim myStringBuilder = New Text.StringBuilder("Kontrahent: ")
        myStringBuilder.AppendFormat(" {0}", VM.Kontrahent)
        myStringBuilder.AppendFormat(" {0}", VM.KontrahentIlosc)
        myStringBuilder.AppendFormat(" x {0:00}", VM.KontrahentStawka)
        myStringBuilder.AppendFormat(" {0}", VM.KontrahentWaluta)
        FakturaKosztowaHandle.Opis = myStringBuilder.ToString()
    End Sub

    Private Sub DodajFaktureCLosing(sender As Object, e As CancelEventArgs)

        VM.CleanKontrahent()
    End Sub
End Class
