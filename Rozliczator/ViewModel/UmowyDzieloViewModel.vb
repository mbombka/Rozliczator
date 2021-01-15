Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Globalization

Imports System.IO
Public Class UmowyDzieloViewModel
    ' Inherits UmowaDzielo
    Implements INotifyPropertyChanged
    Public UmowaDzieloHandle As UmowaDzielo = New UmowaDzielo()
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region
#Region "Properties"

    '************part for displaying faktury kosztowe, przychodowe and delegacje in new window***
    '********************************************************************************


    Private _DelegacjeDataView As DataView
    Public Property DelegacjeDataView As DataView
        Get

            _DelegacjeDataView = New DataView(Startup.VMLocator.VMDelegacje.DelegacjeTable) With {
                .RowFilter = String.Format("NumerUmowy LIKE '{0}'", UmowaDzielo.NumerUmowy)
            }
            Return _DelegacjeDataView
        End Get
        Set(value As DataView)
            _DelegacjeDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("DelegacjeDataView"))
        End Set
    End Property

    Private _FakturyKosztoweDataView As DataView
    Public Property FakturyKosztoweDataView As DataView
        Get
            _FakturyKosztoweDataView = New DataView(Startup.VMLocator.VMFakturyKosztowe.FakturyKosztoweTable)
            _FakturyKosztoweDataView.RowFilter = String.Format("NumerUmowy LIKE '{0}'", UmowaDzielo.NumerUmowy)

            Return _FakturyKosztoweDataView
        End Get
        Set(value As DataView)
            _FakturyKosztoweDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturyKosztoweDataView"))
        End Set
    End Property

    Private _FakturyPrzychodoweDataView As DataView
    Public Property FakturyPrzychodoweDataView As DataView
        Get

            _FakturyPrzychodoweDataView = New DataView(Startup.VMLocator.VMFakturyPrzychodowe.FakturyPrzychodoweTable) With {
                .RowFilter = String.Format("NumerUmowy LIKE '{0}'", UmowaDzielo.NumerUmowy)
            }
            Return _FakturyPrzychodoweDataView
        End Get
        Set(value As DataView)
            _FakturyPrzychodoweDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FakturyPrzychodoweDataView"))
        End Set
    End Property
    '********************************************************************************

    Private _UmowyDzieloTable As DataTable

    Public Property UmowyDzieloTable() As DataTable
        Get
            Return _UmowyDzieloTable
        End Get

        Set(ByVal value As DataTable)
            _UmowyDzieloTable = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("UmowyDzielo"))
        End Set
    End Property

    Private _UmowaDzieloDataView As DataView
    Public Property UmowaDzieloDataView As DataView
        Get
            _UmowaDzieloDataView = New DataView(UmowyDzieloTable)

            Return _UmowaDzieloDataView
        End Get
        Set(value As DataView)
            _UmowaDzieloDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("UmowaDzieloDataView"))
        End Set
    End Property

    Private _UmowaDzieloRowView As DataRowView

    Public Property UmowaDzieloRowView() As DataRowView
        Get
            Return _UmowaDzieloRowView
        End Get

        Set(ByVal value As DataRowView)
            _UmowaDzieloRowView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("UmowaDzieloRowView"))
        End Set
    End Property

    Private _UmowaDzieloRow As DataRow

    Public Property UmowaDzieloRow() As DataRow
        Get
            Return _UmowaDzieloRow
        End Get

        Set(ByVal value As DataRow)
            _UmowaDzieloRow = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("UmowaDzieloRow"))
        End Set
    End Property

    ' Private _UmowaDzielo As UmowaDzielo
    Public Property UmowaDzielo() As UmowaDzielo
        Get
            Return UmowaDzieloHandle
        End Get

        Set(ByVal value As UmowaDzielo)

            UmowaDzieloHandle = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("UmowaDzielo"))
        End Set
    End Property




    Public ReadOnly Property ZleceniobiorcaList() As ObservableCollection(Of String)
        Get
            ZleceniobiorcaList = KsiegowyModel.Wspolnicy
            Return ZleceniobiorcaList
        End Get

    End Property

    Public ReadOnly Property UmowyList As List(Of Object)
        Get
            Dim ValuetoReturn = (From Rows In _UmowyDzieloTable.AsEnumerable()
                                 Select Rows("NumerUmowy")).Distinct().ToList()
            UmowyList = ValuetoReturn

            Return UmowyList
        End Get
    End Property




#Region "Filters"



    Private Property _FilterZleceniobiorca As String
    Public Property FilterZleceniobiorca As String
        Get
            Return _FilterZleceniobiorca
        End Get
        Set(value As String)
            _FilterZleceniobiorca = value
            'apply filter to dataview
            _UmowaDzieloDataView.RowFilter = String.Format("Osoba LIKE '{0}'", _FilterZleceniobiorca)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("FilterZleceniobiorca"))
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
            _UmowaDzieloDataView.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "DataPoczatek > #{0}#", _FilterDataOd)
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
            _UmowaDzieloDataView.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "DataKoniec < #{0}#", _FilterDataDo)
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
            _UmowaDzieloDataView.RowFilter = String.Format("Wyplacono ='{0}'", tempZaplacono)
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

    '************** handle of ftp*********

    'sent file to FTP and if succeed  update fields in database
    Public Sub UploadFile1ToFTP(_Filepath As String, ByRef _UmowaDzielo As UmowaDzielo)

        '#todo use real regular expresion to filter all not acceptable chars
        Dim _RegularNumer = _UmowaDzielo.NumerUmowy.Replace("/", "_")
        _RegularNumer.Replace("\", "_")

        'set directory as something like: UmowyDzielo/Nr_12_2018 (NumerUmowy)
        Dim _Katalog = "UmowyDzielo/" & "Nr_" & _RegularNumer & "/"

        If Startup.FTPMdel.UploadToFTP(_Filepath, _Katalog) Then
            Plik1Name = Path.GetFileName(_Filepath)
            _UmowaDzielo.Plik1 = _Katalog + Plik1Name
        End If

    End Sub

    'remove file from ftp and clear field of delegacja
    Public Sub DeleteFile1FromFTP(ByRef _UmowaDzielo As UmowaDzielo)
        If Startup.FTPMdel.DeleteFromFTP(_UmowaDzielo.Plik1) Then
            _UmowaDzielo.Plik1 = ""
            Plik1Name = ""
        End If
    End Sub


    'sent file to FTP and if succeed  update fields of  delegacja handle
    Public Sub UploadFile2ToFTP(_Filepath As String, ByRef _UmowaDzielo As UmowaDzielo)

        '#todo use real regular expresion to filter all not acceptable chars
        Dim _RegularNumer = _UmowaDzielo.NumerUmowy.Replace("/", "_")
        _RegularNumer.Replace("\", "_")

        'set directory as something like: UmowyDzielo/Nr_12_2018 (NumerUmowy)
        Dim _Katalog = "UmowyDzielo/" & "Nr_" & _RegularNumer & "/"

        If Startup.FTPMdel.UploadToFTP(_Filepath, _Katalog) Then
            Plik2Name = Path.GetFileName(_Filepath)
            _UmowaDzielo.Plik2 = _Katalog + Plik2Name
        End If

    End Sub

    'remove file from ftp and clear field of faktura
    Public Sub DeleteFile2FromFTP(ByRef _UmowaDzielo As UmowaDzielo)
        If Startup.FTPMdel.DeleteFromFTP(_UmowaDzielo.Plik2) Then
            _UmowaDzielo.Plik2 = ""
            Plik2Name = ""
        End If
    End Sub

    'download file from FTP
    Public Sub DownoadFileFromFTP(_Filepath As String, _FTPFilePath As String)
        Startup.FTPMdel.DownloadFromFTP(_Filepath, _FTPFilePath)


    End Sub


    Public Function AddUmowaDzielo(_tempUmowadzielo As UmowaDzielo) As Boolean '# TODO here should be check if values are in range and not empty.
        Startup.MainDataBaseModel.AddUmowaDzielo(_tempUmowadzielo)
        Return True
    End Function
    Public Function RemoveUmowaDzielo(_ID As DataRow) As Boolean '# TODO here should be check if values are in range and not empty.
        Startup.MainDataBaseModel.RemoveUmowaDzielo(_ID)
        Return True
    End Function
    Public Sub ReadTable()
        Startup.MainDataBaseModel.ReadFromDatabase("UmowyDzielo")
    End Sub
    Public Sub SaveTable()
        Startup.MainDataBaseModel.SaveTableToDatabase("UmowyDzielo")
    End Sub

    Public Sub ResetFilters()
        FilterDataOd = DateTime.Now
        FilterDataDo = DateTime.Now
        FilterZleceniobiorca = ""
        FilterZaplacono = ""
        UmowaDzieloDataView.RowFilter = Nothing
    End Sub
#End Region
End Class
