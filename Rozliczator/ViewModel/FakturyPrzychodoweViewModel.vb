Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization
Imports System.IO

Public Class FakturyPrzychodoweViewModel


    Implements INotifyPropertyChanged
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region
#Region "Properties"


    '********************************************************************************
    Private _FakturyPrzychodoweTable As DataTable

    Public Property FakturyPrzychodoweTable() As DataTable
        Get
            Return _FakturyPrzychodoweTable
        End Get

        Set(ByVal value As DataTable)
            _FakturyPrzychodoweTable = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturyPrzychodowe"))
        End Set
    End Property

    Private _FakturyPrzychodoweDataView As DataView
    Public Property FakturyPrzychodoweDataView As DataView
        Get
            _FakturyPrzychodoweDataView = New DataView(_FakturyPrzychodoweTable)

            Return _FakturyPrzychodoweDataView
        End Get
        Set(value As DataView)
            _FakturyPrzychodoweDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturyPrzychodoweDataView"))
        End Set
    End Property

    Private _FakturaPrzychodowaRowView As DataRowView

    Public Property FakturaPrzychodowaRowView() As DataRowView
        Get
            Return _FakturaPrzychodowaRowView
        End Get

        Set(ByVal value As DataRowView)
            _FakturaPrzychodowaRowView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturaPrzychodowaRowView"))
        End Set
    End Property

    Private _FakturaPrzychodowaRow As DataRow

    Public Property FakturaPrzychodowaRow() As DataRow
        Get
            Return _FakturaPrzychodowaRow
        End Get

        Set(ByVal value As DataRow)
            _FakturaPrzychodowaRow = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturaPrzychodowaRow"))
        End Set
    End Property
    Private _FakturaPrzychodowa As FakturaPrzychodowa

    Public Property FakturaPrzychodowa() As FakturaPrzychodowa
        Get
            Return _FakturaPrzychodowa
        End Get

        Set(ByVal value As FakturaPrzychodowa)
            _FakturaPrzychodowa = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturaPrzychodowa"))
        End Set
    End Property

    Private Property _KlientList() As List(Of Object)
    Public Property KlientList() As List(Of Object)
        Get

            Dim ValuetoReturn = (From Rows In _FakturyPrzychodoweTable.AsEnumerable()
                                 Select Rows("Klient")).Distinct().ToList()

            _KlientList = ValuetoReturn

            Return _KlientList
        End Get

        Set(ByVal value As List(Of Object))
            _KlientList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KlientList"))
        End Set
    End Property
    Private Property _OpisList() As List(Of Object)
    Public Property OpisList() As List(Of Object)
        Get

            Dim ValuetoReturn = (From Rows In _FakturyPrzychodoweTable.AsEnumerable()
                                 Select Rows("Opis")).Distinct().ToList()
            _OpisList = ValuetoReturn

            Return _OpisList
        End Get

        Set(ByVal value As List(Of Object))
            _OpisList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("OpisList"))
        End Set
    End Property

    Public ReadOnly Property Osoby() As ObservableCollection(Of String)
        Get
            Osoby = KsiegowyModel.Osoby
            Return Osoby
        End Get

    End Property
    Public ReadOnly Property UmowyList As List(Of Object)
        Get
            UmowyList = Startup.VMLocator.VMUmowyDzielo.UmowyList

            Return UmowyList
        End Get
    End Property
    Public ReadOnly Property WalutyList As ObservableCollection(Of String)
        Get
            WalutyList = WalutyModel.Waluty
            Return WalutyList
        End Get
    End Property




    Private Property _FilterKlient As String
    Public Property FilterKlient As String
        Get
            Return _FilterKlient
        End Get
        Set(value As String)
            _FilterKlient = value
            'apply filter to dataview
            _FakturyPrzychodoweDataView.RowFilter = String.Format("Klient LIKE '{0}'", _FilterKlient)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FilterKlient"))
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
            _FakturyPrzychodoweDataView.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "DataWystawienia > #{0}#", _FilterDataOd)
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
            _FakturyPrzychodoweDataView.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "DataWystawienia < #{0}#", _FilterDataDo)
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
            _FakturyPrzychodoweDataView.RowFilter = String.Format("Zaplacono = '{0}'", tempZaplacono)
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
#Region "Subs"
    Public Sub ReadTable()
        Startup.MainDataBaseModel.ReadFromDatabase("FakturyPrzychodowe")
    End Sub
    Public Sub SaveTable()
        Startup.MainDataBaseModel.SaveTableToDatabase("FakturyPrzychodowe")
    End Sub

    Public Function AddfakturaPrzychodowa(_tempFakturaPrzychodowa As FakturaPrzychodowa) As Boolean '# TODO here should be check if values are in range and not empty.
        Startup.MainDataBaseModel.AddFakturaPrzychodowa(_tempFakturaPrzychodowa)

        Return True
    End Function

    Public Function RemoveFakturaPrzychodowa(_ID As DataRow) As Boolean '# TODO here should be check if values are in range and not empty.


        Startup.MainDataBaseModel.RemoveFakturaPrzychodowa(_ID)
        Return True
    End Function

    '************** handle of ftp*********

    'sent file to FTP and if succeed  update fields in database
    Public Sub UploadFile1ToFTP(_Filepath As String, ByRef _FakturaPrzychodowa As FakturaPrzychodowa)

        '#todo use real regular expresion to filter all not acceptable chars
        Dim _RegularNumer = _FakturaPrzychodowa.NumerFaktury.Replace("/", "_")
        _RegularNumer.Replace("\", "_")
        'set directory as something like: FakturyKosztowe/20180601_Nr_12_2018 (NumerFaktury)
        Dim tempData = _FakturaPrzychodowa.DataWystawienia.Year.ToString()

        If _FakturaPrzychodowa.DataWystawienia.Month < 10 Then
            tempData = tempData & "0" & _FakturaPrzychodowa.DataWystawienia.Month
        Else
            tempData = tempData + _FakturaPrzychodowa.DataWystawienia.Month
        End If

        If _FakturaPrzychodowa.DataWystawienia.Day < 10 Then
            tempData = tempData & "0" & _FakturaPrzychodowa.DataWystawienia.Day
        Else
            tempData = tempData & _FakturaPrzychodowa.DataWystawienia.Day
        End If

        Dim _Katalog = "FakturyPrzychodowe/" & tempData & "_" & "Nr_" & _RegularNumer & "/"


        If Startup.FTPMdel.UploadToFTP(_Filepath, _Katalog) Then
            Plik1Name = Path.GetFileName(_Filepath)
            _FakturaPrzychodowa.Plik1 = _Katalog + Plik1Name
        End If

    End Sub

    'remove file from ftp and clear field of delegacja
    Public Sub DeleteFile1FromFTP(ByRef _FakturaPrzychodowa As FakturaPrzychodowa)
        If Startup.FTPMdel.DeleteFromFTP(_FakturaPrzychodowa.Plik1) Then
            _FakturaPrzychodowa.Plik1 = ""
            Plik1Name = ""
        End If
    End Sub


    'sent file to FTP and if succeed  update fields of  delegacja handle
    Public Sub UploadFile2ToFTP(_Filepath As String, ByRef _FakturaPrzychodowa As FakturaPrzychodowa)

        '#todo use real regular expresion to filter all not acceptable chars
        Dim _RegularNumer = _FakturaPrzychodowa.NumerFaktury.Replace("/", "_")
        _RegularNumer.Replace("\", "_")
        'set directory as something like: FakturyPrzychodowe/20180601_Nr_12_2018 (NumerFaktury)

        Dim tempData = _FakturaPrzychodowa.DataWystawienia.Year.ToString()

        If _FakturaPrzychodowa.DataWystawienia.Month < 10 Then
            tempData = tempData & "0" & _FakturaPrzychodowa.DataWystawienia.Month
        Else
            tempData = tempData + _FakturaPrzychodowa.DataWystawienia.Month
        End If

        If _FakturaPrzychodowa.DataWystawienia.Day < 10 Then
            tempData = tempData & "0" & _FakturaPrzychodowa.DataWystawienia.Day
        Else
            tempData = tempData & _FakturaPrzychodowa.DataWystawienia.Day
        End If

        Dim _Katalog = "FakturyPrzychodowe/" & tempData & "_" & "Nr_" & _RegularNumer & "/"


        If Startup.FTPMdel.UploadToFTP(_Filepath, _Katalog) Then
            Plik2Name = Path.GetFileName(_Filepath)
            _FakturaPrzychodowa.Plik2 = _Katalog + Plik2Name
        End If

    End Sub

    'remove file from ftp and clear field of faktura
    Public Sub DeleteFile2FromFTP(ByRef _FakturaPrzychodowa As FakturaPrzychodowa)
        If Startup.FTPMdel.DeleteFromFTP(_FakturaPrzychodowa.Plik2) Then
            _FakturaPrzychodowa.Plik2 = ""
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
        FilterKlient = ""
        FilterZaplacono = ""
        _FakturyPrzychodoweDataView.RowFilter = Nothing
    End Sub
#End Region

End Class
