Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization
Imports System.Linq
Imports System.IO
Imports Microsoft.Office.Interop
Imports Microsoft.Win32
Imports System.Threading

Public Class DelegacjeViewModel
    Implements INotifyPropertyChanged
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region
#Region "Properties"



    '********************************************************************************
    Private _DelegacjeTable As DataTable

    Public Property DelegacjeTable() As DataTable
        Get
            Return _DelegacjeTable
        End Get

        Set(ByVal value As DataTable)
            _DelegacjeTable = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Delegacje"))
        End Set
    End Property


    Private _DelegacjeDataView As DataView
    Public Property DelegacjeDataView As DataView
        Get
            _DelegacjeDataView = New DataView(_DelegacjeTable)

            Return _DelegacjeDataView
        End Get
        Set(value As DataView)
            _DelegacjeDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DelegacjeDataView"))
        End Set
    End Property

    Private _DelegacjaRowView As DataRowView

    Public Property DelegacjaRowView() As DataRowView
        Get
            Return _DelegacjaRowView
        End Get

        Set(ByVal value As DataRowView)
            _DelegacjaRowView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DelegacjaRowView"))
        End Set
    End Property

    Private _DelegacjaRow As DataRow

    Public Property DelegacjaRow() As DataRow
        Get
            Return _DelegacjaRow
        End Get

        Set(ByVal value As DataRow)
            _DelegacjaRow = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DelegacjaRow"))
        End Set
    End Property
    Private _Delegacja As Delegacja

    Public Property Delegacja() As Delegacja
        Get
            Return _Delegacja
        End Get

        Set(ByVal value As Delegacja)

            _Delegacja = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Delegacja"))
        End Set
    End Property




    Public ReadOnly Property Delegowany() As ObservableCollection(Of String)
        Get
            Delegowany = KsiegowyModel.Wspolnicy
            Return Delegowany
        End Get

    End Property


    Public ReadOnly Property WalutyList As ObservableCollection(Of String)
        Get
            WalutyList = WalutyModel.Waluty
            Return WalutyList
        End Get
    End Property


    Public ReadOnly Property KrajeList As ObservableCollection(Of String)
        Get
            Dim tempKrajList = New ObservableCollection(Of String)  'fill temporary list of country names 
            For Each val1 In WalutyModel.Kraje
                If Not tempKrajList.Contains(val1.Nazwa) Then
                    tempKrajList.Add(val1.Nazwa)
                End If
            Next
            KrajeList = tempKrajList
            Return KrajeList
        End Get
    End Property

    Public ReadOnly Property UmowyList As List(Of Object)
        Get
            UmowyList = Startup.VMLocator.VMUmowyDzielo.UmowyList

            Return UmowyList
        End Get
    End Property
    'list of Miastowyjazd for displayin in combobox
    Private Property _MiastoList() As List(Of Object)
    Public Property MiastoList() As List(Of Object)
        Get

            Dim ValuetoReturn1 = (From Rows In _DelegacjeTable.AsEnumerable()
                                  Select Rows("WyjazdMiasto")).Distinct().ToList()
            Dim ValuetoReturn2 = (From Rows In _DelegacjeTable.AsEnumerable()
                                  Select Rows("PowrotMiasto")).Distinct().ToList()
            For Each val1 In ValuetoReturn1
                If Not ValuetoReturn2.Contains(val1) Then
                    ValuetoReturn2.Add(val1)
                End If
            Next

            _MiastoList = ValuetoReturn2

            Return _MiastoList
        End Get

        Set(ByVal value As List(Of Object))
            _MiastoList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("MiastoList"))
        End Set
    End Property



    'list of Miejsce for displaying in combobox
    Private Property _MiejsceList() As List(Of Object)
    Public Property MiejsceList() As List(Of Object)
        Get

            Dim ValuetoReturn = (From Rows In _DelegacjeTable.AsEnumerable()
                                 Select Rows("MiejsceWyjazdu")).Distinct().ToList()
            _MiejsceList = ValuetoReturn

            Return _MiejsceList
        End Get

        Set(ByVal value As List(Of Object))
            _MiejsceList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("MiejsceList"))
        End Set
    End Property

    'list of Cel for displaying in combobox
    Private Property _CelList() As List(Of Object)
    Public Property CelList() As List(Of Object)
        Get

            Dim ValuetoReturn = (From Rows In _DelegacjeTable.AsEnumerable()
                                 Select Rows("CelWyjazdu")).Distinct().ToList()
            _CelList = ValuetoReturn

            Return _CelList
        End Get

        Set(ByVal value As List(Of Object))
            _CelList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("CelList"))
        End Set
    End Property
    'list of Transport for displaying in combobox
    Private Property _TransportList() As List(Of Object)
    Public Property TransportList() As List(Of Object)
        Get

            Dim ValuetoReturn1 = (From Rows In _DelegacjeTable.AsEnumerable()
                                  Select Rows("PowrotTransport")).Distinct().ToList()
            Dim ValuetoReturn2 = (From Rows In _DelegacjeTable.AsEnumerable()
                                  Select Rows("WyjazdTransport")).Distinct().ToList()

            For Each val1 In ValuetoReturn1
                If Not ValuetoReturn2.Contains(val1) Then
                    ValuetoReturn2.Add(val1)
                End If
            Next

            _TransportList = ValuetoReturn2

            Return _TransportList
        End Get

        Set(ByVal value As List(Of Object))
            _TransportList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("TransportList"))
        End Set
    End Property
#Region "Filters"


    'list ofDelegacje for displayin in combobox
    Private Property _DelegacjeList() As List(Of Object)
    Public Property DelegacjeList() As List(Of Object)
        Get

            Dim ValuetoReturn = (From Rows In _DelegacjeTable.AsEnumerable()
                                 Select Rows("NumerDelegacji")).Distinct().ToList()

            _DelegacjeList = ValuetoReturn

            Return _DelegacjeList
        End Get

        Set(ByVal value As List(Of Object))
            _DelegacjeList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DelegacjeList"))
        End Set
    End Property

    Private Property _FilterDelegowany As String
    Public Property FilterDelegowany As String
        Get
            Return _FilterDelegowany
        End Get
        Set(value As String)
            _FilterDelegowany = value
            'apply filter to dataview
            _DelegacjeDataView.RowFilter = String.Format("Delegowany LIKE '{0}'", _FilterDelegowany)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FilterDelegowany"))
        End Set
    End Property


    Private Property _FilterDataOd As Date
    Public Property FilterDataOd As Date
        Get
            If _FilterDataOd.Year < 2000 Then 'to avoid showing date as 0.0.0000 at startup
                Return DateTime.Now.Date
            Else
                Return _FilterDataOd
            End If

        End Get
        Set(value As Date)
            _FilterDataOd = value
            'apply filter to dataview, try to use 
            _DelegacjeDataView.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "DataWyjazdu > #{0}#", _FilterDataOd)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FilterDataOd"))
        End Set
    End Property

    Private Property _FilterDataDo As Date
    Public Property FilterDataDo As Date
        Get
            If _FilterDataDo.Year < 2000 Then   'to avoid showing date as 0.0.0000 at startup
                Return DateTime.Now.Date
            Else
                Return _FilterDataDo
            End If

        End Get
        Set(value As Date)
            _FilterDataDo = value
            'apply filter to dataview, try to use 
            _DelegacjeDataView.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "DataPowrotu < #{0}#", _FilterDataDo)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FilterDataDo"))
        End Set
    End Property
    Private Property _FilterZaplacono As String
    Public Property FilterZaplacono As String
        Get
            Return _FilterDataDo
        End Get
        Set(value As String)
            _FilterZaplacono = value
            Dim tempZaplacono As Boolean
            If _FilterZaplacono = "Tak" Then
                tempZaplacono = True
            Else
                tempZaplacono = False
            End If
            'apply filter to dataview, try to use 
            _DelegacjeDataView.RowFilter = String.Format("Wyplacono = '{0}'", tempZaplacono)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FilterZaplacono"))
        End Set
    End Property
    Public ReadOnly Property ZaplaconoList As List(Of String)
        Get
            Dim list = New List(Of String)
            list.Add("Tak")
            list.Add("Nie")
            Return list
        End Get
    End Property

    Private Property _Plik1Name As String
    Public Property Plik1Name As String
        Get
            Return _Plik1Name
        End Get
        Set(value As String)

            _Plik1Name = value
            Plik1Icon = Plik1Icon
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Plik1Name"))
        End Set
    End Property

    Private Property _Plik2Name As String
    Public Property Plik2Name As String
        Get
            Return _Plik2Name
        End Get
        Set(value As String)

            _Plik2Name = value
            Plik2Icon = Plik2Icon
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Plik2Name"))
        End Set
    End Property


    Private Property _Plik1Icon As ImageSource
    Public Property Plik1Icon As ImageSource
        Get
            If Plik1Name = "" Then 'if there is no file name = no file , ergo change icon 
                Dim _imageSource = New BitmapImage(New Uri("/Images/file_add_grey.png", UriKind.Relative))
                _Plik1Icon = _imageSource
            Else
                Dim _imageSource = New BitmapImage(New Uri("/Images/file_download_grey.png", UriKind.Relative))
                _Plik1Icon = _imageSource
            End If
            Return _Plik1Icon
        End Get
        Set(value As ImageSource)
            _Plik1Icon = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Plik1Icon"))
        End Set
    End Property

    Private Property _Plik2Icon As ImageSource
    Public Property Plik2Icon As ImageSource
        Get
            If Plik2Name = "" Then
                Dim _imageSource = New BitmapImage(New Uri("/Images/file_add_grey.png", UriKind.Relative))
                _Plik2Icon = _imageSource
            Else
                Dim _imageSource = New BitmapImage(New Uri("/Images/file_download_grey.png", UriKind.Relative))
                _Plik2Icon = _imageSource
            End If

            Return _Plik2Icon
        End Get
        Set(value As ImageSource)
            _Plik2Icon = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Plik2Icon"))
        End Set
    End Property
#End Region
#End Region
#Region "Subs"

    Public Sub ReadTable()
        Startup.MainDataBaseModel.ReadFromDatabase("Delegacje")
    End Sub
    Public Sub SaveTable()
        Startup.MainDataBaseModel.SaveTableToDatabase("Delegacje")
    End Sub


    Public Function AddDelegacja(_tempDelegacja As Delegacja) As Boolean '# TODO here should be check if values are in range and not empty.
        Startup.MainDataBaseModel.AddDelegacja(_tempDelegacja)

        Return True
    End Function

    Public Function RemoveDelegacja(_ID As DataRow) As Boolean '# TODO here should be check if values are in range and not empty.


        Startup.MainDataBaseModel.RemoveDelegacja(_ID)
        Return True
    End Function
    Public Sub UpdateValues()
        ' Delegacja.KrajWyjazdu = _KrajModel.Nazwa
        '  Delegacja.KwotaDelegacji = Delegacja.KwotaDelegacji + 1
    End Sub
    Public Function FinNewNumber() As String
        'declare new delegacja string
        Dim newDelegacjaNumer As String = "error 777" & DateTime.Now.Millisecond.ToString()
        'get number of last added delegacja
        Dim lastDelegacjaNumer As String = DelegacjeTable.Rows.Item(DelegacjeTable.Rows.Count - 1).Item("NumerDelegacji")

        'remove backslash and others 
        lastDelegacjaNumer = KsiegowyModel.RemoveNotNumber(lastDelegacjaNumer)


        'check if lenght of last delegacja is longer that 6 chars ( should be xxYYYY x- number, Y-Year
        If lastDelegacjaNumer.Length < 6 Then
            MessageBox.Show("Nie można określić numeru poprzedniej delegacji")
            Return newDelegacjaNumer
        End If

        'get  year of lasts delegation ( last 4 digits)
        Dim lastDelegacjeYear = lastDelegacjaNumer.Substring(Math.Max(0, lastDelegacjaNumer.Length - 4))
        'check year of last number . if it was that year then inrement number
        If lastDelegacjeYear = DateAndTime.Today.Year.ToString() Then

            'actual number of last delegation (without Year)
            Dim trimLastDelegacja = lastDelegacjaNumer.Substring(0, lastDelegacjaNumer.Length - 4)

            'try to parse numbe of last delegation to integer
            Dim num As Integer
            If Not Integer.TryParse(trimLastDelegacja, num) Then
                newDelegacjaNumer = trimLastDelegacja & "1"
                MessageBox.Show("Nie udało się okreslić numeru ostatniej delegacji. Ergo wpisz numer tej ręcznie")
            End If
            'increment number of delegation
            num = num + 1
            'create new delegation number
            newDelegacjaNumer = num.ToString() & DateAndTime.Today.Year.ToString()
            Return newDelegacjaNumer
        Else ' its first number of current Year
            newDelegacjaNumer = "01" & DateAndTime.Today.Year.ToString()
            Return newDelegacjaNumer
        End If


    End Function

    '************** handle of ftp*********

    'sent file to FTP and if succeed  update fields of  delegacja handle
    Public Sub UploadFile1ToFTP(_Filepath As String, ByRef _delegacjaHandle As Delegacja)

        '#todo use real regular expresion to filter all not acceptable chars
        Dim _RegularNUmerDelegacji = KsiegowyModel.ReplaceCharacters(_delegacjaHandle.NumerDelegacji, "/\", "_")

        'set directory as something like: Delegacje/12_2018 (numerDelegacji
        Dim _Katalog = "Delegacje/" & "Delegacja_" & _RegularNUmerDelegacji & "/"


        If Startup.FTPMdel.UploadToFTP(_Filepath, _Katalog) Then
            Plik1Name = Path.GetFileName(_Filepath)
            _delegacjaHandle.Plik1 = _Katalog + Plik1Name
        End If

    End Sub

    'remove file from ftp and clear field of delegacja
    Public Sub DeleteFile1FromFTP(ByRef _delegacjaHandle As Delegacja)
        If Startup.FTPMdel.DeleteFromFTP(_delegacjaHandle.Plik1) Then
            _delegacjaHandle.Plik1 = ""
            Plik1Name = ""
        End If
    End Sub


    'sent file to FTP and if succeed  update fields of  delegacja handle
    Public Sub UploadFile2ToFTP(_Filepath As String, ByRef _delegacjaHandle As Delegacja)

        '#todo use real regular expresion to filter all not acceptable chars
        Dim _RegularNUmerDelegacji = KsiegowyModel.ReplaceCharacters(_delegacjaHandle.NumerDelegacji, "/\", "_")

        'set directory as something like: Delegacje/12_2018 (numerDelegacji
        Dim _Katalog = "Delegacje/" & "Delegacja_" & _RegularNUmerDelegacji & "/"


        If Startup.FTPMdel.UploadToFTP(_Filepath, _Katalog) Then
            Plik2Name = Path.GetFileName(_Filepath)
            _delegacjaHandle.Plik2 = _Katalog + Plik2Name
        End If

    End Sub

    'remove file from ftp and clear field of delegacja
    Public Sub DeleteFile2FromFTP(ByRef _delegacjaHandle As Delegacja)
        If Startup.FTPMdel.DeleteFromFTP(_delegacjaHandle.Plik2) Then
            _delegacjaHandle.Plik2 = ""
            Plik2Name = ""
        End If
    End Sub

    'download file from FTP
    Public Sub DownoadFileFromFTP(_Filepath As String, _FTPFilePath As String)
        Startup.FTPMdel.DownloadFromFTP(_Filepath, _FTPFilePath)


    End Sub



    Public Sub ResetFilters()
        FilterDataOd = DateTime.Now
        FilterDataDo = DateTime.Now
        FilterDelegowany = ""
        FilterZaplacono = ""
        _DelegacjeDataView.RowFilter = Nothing
    End Sub
#End Region
#Region "exportToExcel"
    'create separate thread for exporting excel
    Public Sub DelegacjaToExcel(_Delegacja As Delegacja)
        Dim newThread As New Thread(New ThreadStart(Sub() EksportujDelegacje(_Delegacja)))
        'start thread, closing of thread is implemented at the end of sub
        newThread.Start()

    End Sub


    Private Sub EksportujDelegacje(_Delegacja As Delegacja)
        Dim _excel As New Excel.Application
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet

        wBook = _excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()


        '**** Tytułowa komórka****
        wSheet.Range("A1:F1").Merge()
        wSheet.Range("A1").BorderAround()
        wSheet.Range("A1").Value = "ROZLICZENIE DIET" & vbCrLf & " Z TYTUŁU PODRÓZY SŁUŻBOWEJ"
        wSheet.Range("A1").RowHeight = 40
        wSheet.Range("A1").Font.Size = 14
        wSheet.Range("A1").Font.Bold = True
        wSheet.Range("A1").VerticalAlignment = Excel.Constants.xlCenter
        wSheet.Range("A1").HorizontalAlignment = Excel.Constants.xlCenter
        ' wSheet.Range("A1").BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium)

        'format cells font 
        wSheet.Range("A3:A20").RowHeight = 20
        wSheet.Range("A3:F8").Font.Size = 11
        wSheet.Range("A3:F8").Font.Bold = True
        wSheet.Range("A9:F16").Font.Size = 10
        wSheet.Range("A9:F16").Font.Bold = False
        wSheet.Range("A8:F9").VerticalAlignment = Excel.Constants.xlCenter
        wSheet.Range("A8:F9").HorizontalAlignment = Excel.Constants.xlCenter

        'format cell width
        wSheet.Range("A1").ColumnWidth = 20
        wSheet.Range("B1").ColumnWidth = 10
        wSheet.Range("C1").ColumnWidth = 10
        wSheet.Range("D1").ColumnWidth = 20
        wSheet.Range("E1").ColumnWidth = 10
        wSheet.Range("F1").ColumnWidth = 10

        'format borders
        'first small borders in all cells
        wSheet.Range("A3:F14").Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous
        wSheet.Range("A3:F14").Cells.Borders.Weight = Excel.XlBorderWeight.xlThin
        'thick brders 
        wSheet.Range("A3:A6").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("B3:F6").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("A7:F7").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("A8:C8").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("D8:F8").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("A9:A14").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("B9:B14").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("C9:C14").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("D9:D14").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("E9:E14").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        wSheet.Range("F9:F14").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)

        'merge cells
        wSheet.Range("B3:F3").Merge()
        wSheet.Range("B4:F4").Merge()
        wSheet.Range("B5:F5").Merge()
        wSheet.Range("B6:F6").Merge()
        wSheet.Range("A8:C8").Merge()
        wSheet.Range("A7:F7").Merge()
        wSheet.Range("D8:F8").Merge()
        'fill cells with values
        wSheet.Range("A3").Value = "DELEGOWANY"
        wSheet.Range("B3").Value = KsiegowyModel.DelegowanyFullName(_Delegacja.Delegowany)

        wSheet.Range("A4").Value2 = "CEL WYJAZDU"
        wSheet.Range("B4").Value2 = _Delegacja.CelWyjazdu

        wSheet.Range("A5").Value2 = "MIEJSCE WYJAZDU"
        wSheet.Range("B5").Value2 = _Delegacja.MiejsceWyjazdu

        wSheet.Range("A6").Value2 = "ŚRODEK LOKOMOCJI"
        If _Delegacja.WyjazdTransport = _Delegacja.PowrotTransport Then
            wSheet.Range("B6").Value2 = _Delegacja.WyjazdTransport
        Else
            wSheet.Range("B6").Value2 = _Delegacja.WyjazdTransport & " / " & _Delegacja.PowrotTransport
        End If

        'szary pasek 

        wSheet.Range("A7").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

        wSheet.Range("A8").Value2 = "WYJAZD"
        wSheet.Range("A9").Value2 = "miejscowość"
        wSheet.Range("A10").Value2 = _Delegacja.WyjazdMiasto
        wSheet.Range("B9").Value2 = "data"
        wSheet.Range("B10").Value2 = _Delegacja.DataWyjazdu.ToShortDateString()
        wSheet.Range("C9").Value2 = "godz."
        wSheet.Range("C10").Value2 = _Delegacja.DataWyjazdu.ToShortTimeString()

        wSheet.Range("D8").Value2 = "PRZYJAZD"
        wSheet.Range("D9").Value2 = "miejscowość"
        wSheet.Range("D10").Value2 = _Delegacja.PowrotMiasto
        wSheet.Range("E9").Value2 = "data"
        wSheet.Range("E10").Value2 = _Delegacja.DataPowrotu.ToShortDateString()
        wSheet.Range("F9").Value2 = "godz."
        wSheet.Range("F10").Value2 = _Delegacja.DataPowrotu.ToShortTimeString()

        '#todo use real regular expresion to filter all not acceptable chars
        Dim _RegularNUmerDelegacji = _Delegacja.NumerDelegacji.Replace("/", "_")
        _RegularNUmerDelegacji.Replace("\", "_")
        'open save winow

        Dim saveFileDialog = New SaveFileDialog()
        saveFileDialog.Title = "Zapisz delegacje jako"
        saveFileDialog.FileName = "Delegacja Nr " & _RegularNUmerDelegacji
        saveFileDialog.DefaultExt = ".xlsx"

        If saveFileDialog.ShowDialog() = True Then

            wBook.SaveAs(saveFileDialog.FileName)
            wBook.Close()
            _excel.Quit()
            'close current thread
            Thread.CurrentThread.Abort()
        End If

    End Sub


#End Region
End Class
