Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports MySql.Data
Imports MySql.Data.Entity
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization
Imports System.IO

Public Class FakturyKosztoweViewModel
    Implements INotifyPropertyChanged
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region
#Region "Properties"

    Public Shared _FakturaKosztowa = New FakturaKosztowa
    '********************************************************************************
    Private _FakturyKosztoweTable As DataTable

    Public Property FakturyKosztoweTable() As DataTable
        Get
            Return _FakturyKosztoweTable
        End Get

        Set(ByVal value As DataTable)
            _FakturyKosztoweTable = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturyKosztowe"))
        End Set
    End Property

    Private _FakturyKosztoweDataView As DataView
    Public Property FakturyKosztoweDataView As DataView
        Get
            _FakturyKosztoweDataView = New DataView(_FakturyKosztoweTable)

            Return _FakturyKosztoweDataView
        End Get
        Set(value As DataView)
            _FakturyKosztoweDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturyKosztoweDataView"))
        End Set
    End Property

    Private _FakturaKosztowaRowView As DataRowView

    Public Property FakturaKosztowaRowView() As DataRowView
        Get
            Return _FakturaKosztowaRowView
        End Get

        Set(ByVal value As DataRowView)
            _FakturaKosztowaRowView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturaKosztowaRowView"))
        End Set
    End Property

    Private _FakturaKosztowaRow As DataRow

    Public Property FakturaKosztowaRow() As DataRow
        Get
            Return _FakturaKosztowaRow
        End Get

        Set(ByVal value As DataRow)
            _FakturaKosztowaRow = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturaKosztowaRow"))
        End Set
    End Property
    ' Private _FakturaKosztowa As FakturaKosztowa

    Public Property FakturaKosztowa() As FakturaKosztowa
        Get
            Return _FakturaKosztowa
        End Get

        Set(ByVal value As FakturaKosztowa)
            _FakturaKosztowa = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturaKosztowa"))
        End Set
    End Property

    Private Property _SprzedawcyList() As List(Of Object)
    Public Property SprzedawcyList() As List(Of Object)
        Get

            Dim ValuetoReturn = (From Rows In _FakturyKosztoweTable.AsEnumerable()
                                 Select Rows("Sprzedawca")).Distinct().ToList()

            _SprzedawcyList = ValuetoReturn

            Return _SprzedawcyList
        End Get

        Set(ByVal value As List(Of Object))
            _SprzedawcyList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SprzedawcyList"))
        End Set
    End Property




    Private Property _OpisList() As List(Of Object)
    Public Property OpisList() As List(Of Object)
        Get

            Dim ValuetoReturn = (From Rows In _FakturyKosztoweTable.AsEnumerable()
                                 Select Rows("Opis")).Distinct().ToList()
            _OpisList = ValuetoReturn

            Return _OpisList
        End Get

        Set(ByVal value As List(Of Object))
            _SprzedawcyList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SprzedawcyList"))
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

    '********Kontrahneci part***************
    Public ReadOnly Property KontrahenciList As List(Of Object)
        Get
            KontrahenciList = Startup.VMLocator.VMKontrahenci.KontrahenciList
            Return KontrahenciList
        End Get
    End Property

    Private Property _Kontrahent As String
    Public Property Kontrahent As String
        Get
            Return _Kontrahent
        End Get

        Set(Value As String)
            _Kontrahent = Value
            'Get from talbe kontrahenci values stawka and waluta
            If _Kontrahent <> "" Then
                KontrahentIlosc = 0
                Dim ValuetoReturn1 = (From Rows In Startup.VMLocator.VMKontrahenci.KontrahenciDataTable.AsEnumerable()
                                      Where Rows("NazwaFirmy") = _Kontrahent
                                      Select Rows("Stawka")).Distinct().ToList()

                KontrahentStawka = ValuetoReturn1.First()

                Dim ValuetoReturn2 = (From Rows In Startup.VMLocator.VMKontrahenci.KontrahenciDataTable.AsEnumerable()
                                      Where Rows("NazwaFirmy") = _Kontrahent
                                      Select Rows("Waluta")).Distinct().ToList()

                KontrahentWaluta = ValuetoReturn2.First()

            End If
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Kontrahent"))
        End Set
    End Property

    Private Property _KontrahentWaluta As String
    Public Property KontrahentWaluta As String
        Get
            Return _KontrahentWaluta
        End Get

        Set(Value As String)
            _KontrahentWaluta = Value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontrahentWaluta"))
        End Set
    End Property

    Private Property _KontrahentStawka As Decimal
    Public Property KontrahentStawka As Decimal
        Get
            Return _KontrahentStawka
        End Get

        Set(Value As Decimal)
            _KontrahentStawka = Value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontrahentStawka"))
        End Set
    End Property

    Private Property _KontrahentIlosc As Decimal
    Public Property KontrahentIlosc As Decimal
        Get
            Return _KontrahentIlosc
        End Get

        Set(Value As Decimal)
            _KontrahentIlosc = Value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontrahentIlosc"))
        End Set
    End Property

    Private Property _KontrahentVisibility As Visibility
    Public Property KontrahentVisibility As Visibility
        Get
            Return _KontrahentVisibility
        End Get
        Set(value As Visibility)
            _KontrahentVisibility = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("KontrahentVisibility"))
        End Set
    End Property

    '************** Filters*******************
    Private Property _FilterCzyjKoszt As String
    Public Property FilterCzyjKoszt As String
        Get
            Return _FilterCzyjKoszt
        End Get
        Set(value As String)
            _FilterCzyjKoszt = value
            'apply filter to dataview

            _FakturyKosztoweDataView.RowFilter = String.Format("CzyjKoszt LIKE '{0}'", _FilterCzyjKoszt)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FilterCzyjKoszt"))
        End Set
    End Property


    Private Property _FilterSprzedawca As String
    Public Property FilterSprzedawca As String
        Get
            Return _FilterSprzedawca
        End Get
        Set(value As String)
            _FilterSprzedawca = value
            'apply filter to dataview
            _FakturyKosztoweDataView.RowFilter = String.Format("Sprzedawca LIKE '{0}'", _FilterSprzedawca)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FilterSprzedawca"))
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
            _FakturyKosztoweDataView.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "DataWystawienia > #{0}#", _FilterDataOd)
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
            _FakturyKosztoweDataView.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "DataWystawienia < #{0}#", _FilterDataDo)
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
            _FakturyKosztoweDataView.RowFilter = String.Format("Zaplacono = '{0}'", tempZaplacono)
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
        Startup.MainDataBaseModel.ReadFromDatabase("FakturyKosztowe")
    End Sub
    Public Sub SaveTable()
        Startup.MainDataBaseModel.SaveTableToDatabase("FakturyKosztowe")
    End Sub
    Public Function AddfakturaKosztowa(_tempFakturaKosztowa As FakturaKosztowa) As Boolean '# TODO here should be check if values are in range and not empty.
        Startup.MainDataBaseModel.AddFakturaKosztowa(_tempFakturaKosztowa)

        Return True
    End Function

    Public Function RemoveFakturaKosztowa(_ID As DataRow) As Boolean '# TODO here should be check if values are in range and not empty.


        Startup.MainDataBaseModel.RemoveFakturaKosztowa(_ID)
        Return True
    End Function

    '************** handle of ftp*********

    'sent file to FTP and if succeed  update fields of  delegacja handle
    Public Sub UploadFile1ToFTP(_Filepath As String, ByRef _FakturaKosztowa As FakturaKosztowa)

        '#todo use real regular expresion to filter all not acceptable chars
        Dim _RegularNumer = _FakturaKosztowa.NumerFaktury.Replace("/", "_")
        _RegularNumer.Replace("\", "_")
        'set directory as something like: FakturyKosztowe/20180601_Nr_12_2018 (NumerFaktury)
        Dim tempData = _FakturaKosztowa.DataWystawienia.Year.ToString()

        If _FakturaKosztowa.DataWystawienia.Month < 10 Then
            tempData = tempData & "0" & _FakturaKosztowa.DataWystawienia.Month
        Else
            tempData = tempData + _FakturaKosztowa.DataWystawienia.Month
        End If

        If _FakturaKosztowa.DataWystawienia.Day < 10 Then
            tempData = tempData & "0" & _FakturaKosztowa.DataWystawienia.Day
        Else
            tempData = tempData & _FakturaKosztowa.DataWystawienia.Day
        End If

        Dim _Katalog = "FakturyKosztowe/" & tempData & "_" & "Nr_" & _RegularNumer & "/"


        If Startup.FTPMdel.UploadToFTP(_Filepath, _Katalog) Then
            Plik1Name = Path.GetFileName(_Filepath)
            _FakturaKosztowa.Plik1 = _Katalog + Plik1Name
        End If

    End Sub

    'remove file from ftp and clear field of delegacja
    Public Sub DeleteFile1FromFTP(ByRef _FakturaKosztowa As FakturaKosztowa)
        If Startup.FTPMdel.DeleteFromFTP(_FakturaKosztowa.Plik1) Then
            _FakturaKosztowa.Plik1 = ""
            Plik1Name = ""
        End If
    End Sub


    'sent file to FTP and if succeed  update fields of  delegacja handle
    Public Sub UploadFile2ToFTP(_Filepath As String, ByRef _FakturaKosztowa As FakturaKosztowa)

        '#todo use real regular expresion to filter all not acceptable chars
        Dim _RegularNumer = _FakturaKosztowa.NumerFaktury.Replace("/", "_")
        _RegularNumer.Replace("\", "_")
        'set directory as something like: FakturyKosztowe/20180601_Nr_12_2018 (NumerFaktury)

        Dim tempData = _FakturaKosztowa.DataWystawienia.Year.ToString()

        If _FakturaKosztowa.DataWystawienia.Month < 10 Then
            tempData = tempData & "0" & _FakturaKosztowa.DataWystawienia.Month
        Else
            tempData = tempData + _FakturaKosztowa.DataWystawienia.Month
        End If

        If _FakturaKosztowa.DataWystawienia.Day < 10 Then
            tempData = tempData & "0" & _FakturaKosztowa.DataWystawienia.Day
        Else
            tempData = tempData & _FakturaKosztowa.DataWystawienia.Day
        End If

        Dim _Katalog = "FakturyKosztowe/" & tempData & "_" & "Nr_" & _RegularNumer & "/"


        If Startup.FTPMdel.UploadToFTP(_Filepath, _Katalog) Then
            Plik2Name = Path.GetFileName(_Filepath)
            _FakturaKosztowa.Plik2 = _Katalog + Plik2Name
        End If

    End Sub

    'remove file from ftp and clear field of delegacja
    Public Sub DeleteFile2FromFTP(ByRef _FakturaKosztowa As FakturaKosztowa)
        If Startup.FTPMdel.DeleteFromFTP(_FakturaKosztowa.Plik2) Then
            _FakturaKosztowa.Plik2 = ""
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
        FilterSprzedawca = ""
        FilterCzyjKoszt = ""
        FilterZaplacono = ""
        _FakturyKosztoweDataView.RowFilter = Nothing
    End Sub

    Private Sub PrzeliczKontrahenta()
        If Kontrahent <> "" Then

        End If
    End Sub
    Public Sub CleanKontrahent()
        Kontrahent = ""
        KontrahentIlosc = 0
        KontrahentStawka = 0
        KontrahentWaluta = ""
    End Sub
#End Region


End Class
