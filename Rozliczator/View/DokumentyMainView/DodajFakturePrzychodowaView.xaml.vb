Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports Microsoft.Win32
Public Class DodajFakturePrzychodowaView
    Public WithEvents VM As New FakturyPrzychodoweViewModel
    Public FakturaPrzychodowaHandle As FakturaPrzychodowa = New FakturaPrzychodowa()

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMFakturyPrzychodowe


            Me.DataContext = FakturaPrzychodowaHandle 'Startup.VMLocator.VMFakturyKosztowe.FakturaKosztowa    'Set the DataContext of the Page to the ViewModel



            KlientComboBox.ItemsSource = VM.KlientList

            czyjZyskComboBox.ItemsSource = VM.Osoby
            listaWalutComboBox.ItemsSource = VM.WalutyList
            OpisComboBox.ItemsSource = VM.OpisList
            listaVatCombobox.ItemsSource = KsiegowyModel.StawkiVAT
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Sub New(_Osoba As String, _NumerUmowy As String) 'same as above but filled with czyjzysk and numer umowy

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMFakturyPrzychodowe


            Me.DataContext = FakturaPrzychodowaHandle 'Startup.VMLocator.VMFakturyKosztowe.FakturaKosztowa    'Set the DataContext of the Page to the ViewModel
            FakturaPrzychodowaHandle.CzyjZysk = _Osoba
            FakturaPrzychodowaHandle.NumerUmowy = _NumerUmowy


            KlientComboBox.ItemsSource = VM.KlientList
            'listaUmow.ItemsSource = VM.UmowyList
            czyjZyskComboBox.ItemsSource = VM.Osoby
            listaWalutComboBox.ItemsSource = VM.WalutyList
            OpisComboBox.ItemsSource = VM.OpisList
            listaVatCombobox.ItemsSource = KsiegowyModel.StawkiVAT
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Public Sub New(_DataRow As Data.DataRow)    'exact the same as above but with added function of filling object faktury kosztowe with data from datarow ( selected from datagrid)


        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMFakturyPrzychodowe
            Me.DataContext = FakturaPrzychodowaHandle   'Set the DataContext of the Page to the ViewModel

            KlientComboBox.ItemsSource = VM.KlientList
            ' listaUmow.ItemsSource = VM.UmowyList
            czyjZyskComboBox.ItemsSource = VM.Osoby
            listaWalutComboBox.ItemsSource = VM.WalutyList
            OpisComboBox.ItemsSource = VM.OpisList
            listaVatCombobox.ItemsSource = KsiegowyModel.StawkiVAT
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM

            DataBaseModel.FillFakturaPrzychodowa(_DataRow, FakturaPrzychodowaHandle)  'fill object ' faktura kosztowa' with values from selected row from datagrid
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try


    End Sub


    Private Sub Button_Zapisz(sender As Object, e As RoutedEventArgs)
        If VM.AddfakturaPrzychodowa(FakturaPrzychodowaHandle) Then
            Close()
        End If
    End Sub

    '*********************obsługa plikow ********

    'drop Plik1 in image
    Private Sub Plik1_Drop(sender As Object, e As DragEventArgs)
        Dim filestring() As String = e.Data.GetData(DataFormats.FileDrop)
        If filestring.GetLength(0) > 1 Then
            MessageBox.Show("opanuj się... pojedynczo")
        ElseIf FakturaPrzychodowaHandle.NumerFaktury = "" Or FakturaPrzychodowaHandle.DataWystawienia.Year < 1999 Then
            MessageBox.Show("Uzupełnij najpierw numer faktury oraz date wystawienia")
        Else
            VM.UploadFile1ToFTP(filestring(0), FakturaPrzychodowaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik1Dodaj(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = True Then
            If FakturaPrzychodowaHandle.NumerFaktury = "" Or FakturaPrzychodowaHandle.DataWystawienia.Year < 1999 Then
                MessageBox.Show("Uzupełnij najpierw numer faktury oraz date wystawienia")
            Else
                VM.UploadFile1ToFTP(openFileDialog.FileName, FakturaPrzychodowaHandle)
            End If
        End If
    End Sub

    Private Sub RClikck_Plik1Usun(sender As Object, e As RoutedEventArgs)
        If FakturaPrzychodowaHandle.Plik1 <> "" Then
            VM.DeleteFile1FromFTP(FakturaPrzychodowaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik1Pobierz(sender As Object, e As RoutedEventArgs)
        If FakturaPrzychodowaHandle.Plik1 <> "" Then
            Dim saveFileDialog = New SaveFileDialog()
            saveFileDialog.Title = "Zapisz plik jako"
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(FakturaPrzychodowaHandle.Plik1)
            saveFileDialog.DefaultExt = Path.GetExtension(FakturaPrzychodowaHandle.Plik1)

            If saveFileDialog.ShowDialog() = True Then
                VM.DownoadFileFromFTP(saveFileDialog.FileName, FakturaPrzychodowaHandle.Plik1)
            End If
        End If

    End Sub

    'drop Plik2 in image
    Private Sub Plik2_Drop(sender As Object, e As DragEventArgs)
        Dim filestring() As String = e.Data.GetData(DataFormats.FileDrop)
        If filestring.GetLength(0) > 1 Then
            MessageBox.Show("opanuj się... pojedynczo")
        ElseIf FakturaPrzychodowaHandle.NumerFaktury = "" Or FakturaPrzychodowaHandle.DataWystawienia.Year < 1999 Then
            MessageBox.Show("Uzupełnij najpierw numer faktury oraz date wystawienia")
        Else
            VM.UploadFile2ToFTP(filestring(0), FakturaPrzychodowaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik2Dodaj(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = True Then
            If FakturaPrzychodowaHandle.NumerFaktury = "" Or FakturaPrzychodowaHandle.DataWystawienia.Year < 1999 Then
                MessageBox.Show("Uzupełnij najpierw numer faktury oraz date wystawienia")
            Else
                VM.UploadFile2ToFTP(openFileDialog.FileName, FakturaPrzychodowaHandle)
            End If
        End If
    End Sub

    Private Sub RClikck_Plik2Usun(sender As Object, e As RoutedEventArgs)
        If FakturaPrzychodowaHandle.Plik2 <> "" Then
            VM.DeleteFile2FromFTP(FakturaPrzychodowaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik2Pobierz(sender As Object, e As RoutedEventArgs)
        If FakturaPrzychodowaHandle.Plik2 <> "" Then
            Dim saveFileDialog = New SaveFileDialog()
            saveFileDialog.Title = "Zapisz plik jako"
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(FakturaPrzychodowaHandle.Plik2)
            saveFileDialog.DefaultExt = Path.GetExtension(FakturaPrzychodowaHandle.Plik2)

            If saveFileDialog.ShowDialog() = True Then
                VM.DownoadFileFromFTP(saveFileDialog.FileName, FakturaPrzychodowaHandle.Plik2)
            End If
        End If

    End Sub
End Class
