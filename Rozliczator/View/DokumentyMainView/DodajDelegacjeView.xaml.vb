Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports Microsoft.Win32 'for file dialog
Public Class DodajDelegacjeView
    Public WithEvents VM As New DelegacjeViewModel
    Public DelegacjaHandle As Delegacja = New Delegacja()
    Public delegacja As Delegacja
    Public walutaModel = New WalutyModel()
    Public NowaDelegacjaFlag As Boolean = False
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMDelegacje


            Me.DataContext = DelegacjaHandle 'Startup.VMLocator.VMFakturyKosztowe.FakturaKosztowa    'Set the DataContext of the Page to the ViewModel




            DelegowanyComboBox.ItemsSource = VM.Delegowany
            ' listaUmow.ItemsSource = VM.UmowyList
            listaKrajow.ItemsSource = VM.KrajeList
            WyjazdMiastoComboBox.ItemsSource = VM.MiastoList
            PowrotMiastoComboBox.ItemsSource = VM.MiastoList
            WyjazdTransportComboBox.ItemsSource = VM.TransportList
            PowrotTransportComboBox.ItemsSource = VM.TransportList
            CelComboBox.ItemsSource = VM.CelList
            MiejsceComboCox.ItemsSource = VM.MiejsceList
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM

            NowaDelegacjaFlag = True    'set flag that this is new delegacja
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Sub New(_Osoba As String, _NumerUmowy As String) 'likeabove but with filling numer umowy and osoba

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMDelegacje


            Me.DataContext = DelegacjaHandle 'Startup.VMLocator.VMFakturyKosztowe.FakturaKosztowa    'Set the DataContext of the Page to the ViewModel
            DelegacjaHandle.Delegowany = _Osoba
            DelegacjaHandle.NumerUmowy = _NumerUmowy

            DelegowanyComboBox.ItemsSource = VM.Delegowany
            'listaUmow.ItemsSource = VM.UmowyList
            listaKrajow.ItemsSource = VM.KrajeList
            WyjazdMiastoComboBox.ItemsSource = VM.MiastoList
            PowrotMiastoComboBox.ItemsSource = VM.MiastoList
            WyjazdTransportComboBox.ItemsSource = VM.TransportList
            PowrotTransportComboBox.ItemsSource = VM.TransportList
            CelComboBox.ItemsSource = VM.CelList
            MiejsceComboCox.ItemsSource = VM.MiejsceList
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM

            NowaDelegacjaFlag = True    'set flag that this is new delegacja
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Public Sub New(_DataRow As Data.DataRow)    'exact the same as above but with added function of filling object faktury kosztowe with data from datarow ( selected from datagrid)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            VM = Startup.VMLocator.VMDelegacje


            Me.DataContext = DelegacjaHandle 'Startup.VMLocator.VMFakturyKosztowe.FakturaKosztowa    'Set the DataContext of the Page to the ViewModel


            DataBaseModel.FillDelegacja(_DataRow, DelegacjaHandle)  'fill object ' faktura kosztowa' with values from selected row from datagrid

            'Dim ListItem = From v In WalutyModel.Kraje Where v.Nazwa = DelegacjaHandle.KrajWyjazdu  '  find element from Kraje list where nazwa will match KrajWyjazdu
            ' DelegacjaHandle.KrajModel = ListItem.First 'copy 
            ' listaKrajow.Text = DelegacjaHandle.KrajModel.Nazwa


            DelegowanyComboBox.ItemsSource = VM.Delegowany
            'listaUmow.ItemsSource = VM.UmowyList
            listaKrajow.ItemsSource = VM.KrajeList

            WyjazdMiastoComboBox.ItemsSource = VM.MiastoList
            PowrotMiastoComboBox.ItemsSource = VM.MiastoList
            WyjazdTransportComboBox.ItemsSource = VM.TransportList
            PowrotTransportComboBox.ItemsSource = VM.TransportList
            CelComboBox.ItemsSource = VM.CelList
            MiejsceComboCox.ItemsSource = VM.MiejsceList
            KontoComboBox.ItemsSource = KsiegowyModel.Konta
            Plik1Image.DataContext = VM
            Plik2Image.DataContext = VM
            Plik1Text.DataContext = VM
            Plik2Text.DataContext = VM

            NowaDelegacjaFlag = False    'set flag that this is update of existing delegacja
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try


    End Sub


    Private Sub Button_Zapisz(sender As Object, e As RoutedEventArgs)
        If NowaDelegacjaFlag And VM.DelegacjeList.Contains(NumerDelegacji.Text) Then    'check if during adding new delegacja, already exist delegacja with the same unique key
            MessageBox.Show("Numer delegacji już istnieje")
        Else
            If VM.AddDelegacja(DelegacjaHandle) Then
                Close()
            End If
        End If

    End Sub

    Private Sub listaKrajow_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        VM.UpdateValues()
    End Sub

    'drop Plik1 in image
    Private Sub Plik1_Drop(sender As Object, e As DragEventArgs)
        Dim filestring() As String = e.Data.GetData(DataFormats.FileDrop)
        If filestring.GetLength(0) > 1 Then
            MessageBox.Show("opanuj się... pojedynczo")
        ElseIf DelegacjaHandle.NumerDelegacji = "" Then
            MessageBox.Show("Uzupełnij najpierw numer delegacji")
        Else
            VM.UploadFile1ToFTP(filestring(0), DelegacjaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik1Dodaj(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = True Then
            If DelegacjaHandle.NumerDelegacji = "" Then
                MessageBox.Show("Uzupełnij najpierw numer delegacji")
            Else
                VM.UploadFile1ToFTP(openFileDialog.FileName, DelegacjaHandle)
            End If
        End If
    End Sub

    Private Sub RClikck_Plik1Usun(sender As Object, e As RoutedEventArgs)
        If DelegacjaHandle.Plik1 <> "" Then
            VM.DeleteFile1FromFTP(DelegacjaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik1Pobierz(sender As Object, e As RoutedEventArgs)
        If DelegacjaHandle.Plik1 <> "" Then
            Dim saveFileDialog = New SaveFileDialog()
            saveFileDialog.Title = "Zapisz plik jako"
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(DelegacjaHandle.Plik1)
            saveFileDialog.DefaultExt = Path.GetExtension(DelegacjaHandle.Plik1)

            If saveFileDialog.ShowDialog() = True Then
                VM.DownoadFileFromFTP(saveFileDialog.FileName, DelegacjaHandle.Plik1)
            End If
        End If

    End Sub

    'drop Plik2 in image
    Private Sub Plik2_Drop(sender As Object, e As DragEventArgs)
        Dim filestring() As String = e.Data.GetData(DataFormats.FileDrop)
        If filestring.GetLength(0) > 1 Then
            MessageBox.Show("opanuj się... pojedynczo")
        ElseIf DelegacjaHandle.NumerDelegacji = "" Then
            MessageBox.Show("Uzupełnij najpierw numer delegacji")
        Else
            VM.UploadFile2ToFTP(filestring(0), DelegacjaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik2Dodaj(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog = New OpenFileDialog()
        If openFileDialog.ShowDialog() = True Then
            If DelegacjaHandle.NumerDelegacji = "" Then
                MessageBox.Show("Uzupełnij najpierw numer delegacji")
            Else
                VM.UploadFile2ToFTP(openFileDialog.FileName, DelegacjaHandle)
            End If
        End If
    End Sub

    Private Sub RClikck_Plik2Usun(sender As Object, e As RoutedEventArgs)
        If DelegacjaHandle.Plik2 <> "" Then
            VM.DeleteFile2FromFTP(DelegacjaHandle)
        End If
    End Sub

    Private Sub RClikck_Plik2Pobierz(sender As Object, e As RoutedEventArgs)
        If DelegacjaHandle.Plik2 <> "" Then
            Dim saveFileDialog = New SaveFileDialog()
            saveFileDialog.Title = "Zapisz plik jako"
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(DelegacjaHandle.Plik2)
            saveFileDialog.DefaultExt = Path.GetExtension(DelegacjaHandle.Plik2)

            If saveFileDialog.ShowDialog() = True Then
                VM.DownoadFileFromFTP(saveFileDialog.FileName, DelegacjaHandle.Plik2)
            End If
        End If

    End Sub

    Private Sub Button_Export(sender As Object, e As RoutedEventArgs)
        VM.DelegacjaToExcel(DelegacjaHandle)
    End Sub

    Private Sub Button_NewNumber(sender As Object, e As RoutedEventArgs)
        DelegacjaHandle.NumerDelegacji = VM.FinNewNumber()
    End Sub
End Class
