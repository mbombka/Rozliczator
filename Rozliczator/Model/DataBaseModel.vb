Imports System.IO
'Imports System.Data.SQLite
Imports System.Data
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.SQLite.Linq


Public Class DataBaseModel
#Region "ViewModel"

    Public Shared WithEvents VMLocator As New ViewModelLocator
    'Declare instance of ViewModelLocator to Hold the ViewModel passed from Startup Class
    'Private WithEvents VMLocator As New ViewModelLocator

#End Region
    Public Shared MyDataSet As New DataSet()


    'Public DataBaseFile As String = "MyDatabase.sqlite"
    Public Shared cs As String = "server=s64.hekko.net.pl;database=cseg_dane; user = " & Credentials.DbUser &
        ";port=3306;password= " & Credentials.txtPassword
    Public Shared m_dbConnection As MySqlConnection



    Public Sub New(ByRef ViewModelLocator As ViewModelLocator)

        ' insert code required on object creation below this point.

        Try
            'assign the viewmodel to the local instance
            'CreateDatabase()
            FillDataSet("FakturyPrzychodowe")
            FillDataSet("FakturyKosztowe")
            FillDataSet("Delegacje")
            FillDataSet("UmowyDzielo")
            FillDataSet("CSEG")
            FillDataSet("KontaCSEG")
            FillDataSet("KontaWspolnicy")
            FillDataSet("Kontrahenci")
            VMLocator = ViewModelLocator
            CopyTables(Startup.VMLocator)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
    Public Sub New()
        MyBase.New()

        ' Insert code required on object creation below this point.
    End Sub


    Public Shared Function TestConnection() As Boolean
        Return True
    End Function


    'Fill data set from table

    Public Overloads Function FillDataSet(_TableName As String) As Boolean
        Using con As New MySqlConnection(cs)
            Try
                con.Open()
                Dim sql As String = "Select * FROM " & _TableName

                Using da As New MySqlDataAdapter(sql, con)
                    Using cb As New MySqlCommandBuilder(da)

                        da.Fill(MyDataSet, _TableName)
                        da.Update(MyDataSet, _TableName)
                    End Using
                End Using

                con.Close()
                Return True
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                Return False
            End Try

        End Using
    End Function

    Private Sub CopyTables(ByRef VM As ViewModelLocator)
        Try
            VM.VMDelegacje.DelegacjeTable = MyDataSet.Tables("Delegacje") 'main datatable for delegacje
            VM.VMFakturyKosztowe.FakturyKosztoweTable = MyDataSet.Tables("FakturyKosztowe")
            VM.VMFakturyPrzychodowe.FakturyPrzychodoweTable = MyDataSet.Tables("FakturyPrzychodowe")
            VM.VMUmowyDzielo.UmowyDzieloTable = MyDataSet.Tables("UmowyDzielo")
            VM.VMKontaCSEG.KontaCSEGDataTable = MyDataSet.Tables("KontaCSEG")
            VM.VMKontaWspolnicy.KontaWspolnicyDataTable = MyDataSet.Tables("KontaWspolnicy")
            VM.VMKontrahenci.KontrahenciDataTable = MyDataSet.Tables("Kontrahenci")
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

#Region "General operations On tables"
    'sub to insert into database actual dataset
    Public Sub InsertDataSetIntoTable(_TableName As String)
        Try
            Using con As New MySqlConnection(cs)
                con.Open()
                Dim sql As String = "Select * FROM " & _TableName

                Using MyAdapter As New MySqlDataAdapter(sql, con)
                    MyAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey

                    Using cb As New MySqlCommandBuilder(MyAdapter)

                        MyAdapter.Update(MyDataSet, _TableName) ' first update sql table from data set
                        MyDataSet.Tables(_TableName).Clear() ' clear data set
                        MyAdapter.Fill(MyDataSet, _TableName) ' update data set from sql table

                    End Using
                End Using

                con.Close()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub



    'save table from dataset to database
    Public Sub SaveTableToDatabase(_TableName As String)
        Try
            Using con As New MySqlConnection(cs)
                Dim sql As String = "Select * FROM " & _TableName
                con.Open()
                Using MyAdapter As New MySqlDataAdapter(sql, con)
                    MyAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
                    Using cb As New MySqlCommandBuilder(MyAdapter)

                        MyAdapter.Update(MyDataSet, _TableName)
                        MyDataSet.Tables(_TableName).Clear() ' clear data set
                        MyAdapter.Fill(MyDataSet, _TableName) ' update data set from sql table
                        Dim break1 As Boolean = True
                    End Using
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub
    Public Sub ReadFromDatabase(_TableName As String)
        Try
            Using con As New MySqlConnection(cs)
                Dim sql As String = "Select * FROM " & _TableName
                con.Open()
                Using MyAdapter As New MySqlDataAdapter(sql, con)

                    Using cb As New MySqlCommandBuilder(MyAdapter)

                        MyDataSet.Tables(_TableName).Clear() ' clear data set
                        MyAdapter.Fill(MyDataSet, _TableName) ' update data set from sql table
                    End Using
                    con.Close()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub
    'remove from database - not used anymore
    Public Sub DeleteFromDataBase(_TableName As String)
        Try
            Using con As New MySqlConnection(cs)
                Dim sql As String = "Select * FROM " & _TableName
                con.Open()
                Using MyAdapter As New MySqlDataAdapter(sql, con)
                    Dim sqlText = String.Format("DELETE FROM  '{0}' WHERE Id = :Id", _TableName)
                    MyAdapter.DeleteCommand = New MySqlCommand(sqlText, con)
                    MyAdapter.DeleteCommand.Parameters.Add("Id", DbType.Int32, 0, "Id").SourceVersion = DataRowVersion.Original
                    Using cb As New MySqlCommandBuilder(MyAdapter)
                        MyAdapter.Update(MyDataSet, _TableName)


                        MyDataSet.Tables(_TableName).Clear() ' clear data set
                        MyAdapter.Fill(MyDataSet, _TableName) ' update data set from sql table
                    End Using
                    con.Close()
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "adding new rows"
    Public Overloads Sub AddFakturaKosztowa(ByRef _FakturaKosztowa As FakturaKosztowa)
        Dim row As DataRow

        If (_FakturaKosztowa.Id = 0) Or (_FakturaKosztowa.Id = vbNull) Then   'insert new row to datatable if row Id =0

            'dodaj dokument do rozrachunków księgowych
            KsiegowyModel.NowyDokumentRachunkowy(_FakturaKosztowa)
            'kontynuuj dodawanie nowego rzędu do dokumentów

            MyDataSet.Tables("FakturyKosztowe").Columns("Id").AllowDBNull = True
            row = MyDataSet.Tables("FakturyKosztowe").NewRow()

            row("NumerFaktury") = _FakturaKosztowa.NumerFaktury
            row("Sprzedawca") = _FakturaKosztowa.Sprzedawca
            row("DataWystawienia") = _FakturaKosztowa.DataWystawienia
            row("Opis") = _FakturaKosztowa.Opis
            row("Kwota") = _FakturaKosztowa.Kwota
            row("StawkaVAT") = _FakturaKosztowa.StawkaVAT
            row("KwotaPLN") = _FakturaKosztowa.KwotaPLN
            row("Waluta") = _FakturaKosztowa.Waluta
            row("Zaplacono") = _FakturaKosztowa.Zaplacono
            row("Konto") = _FakturaKosztowa.Konto
            row("KursZDnia") = _FakturaKosztowa.KursZDnia
            row("DoZwrotu") = _FakturaKosztowa.DoZwrotu
            row("CzyjKoszt") = _FakturaKosztowa.CzyjKoszt
            row("NumerUmowy") = _FakturaKosztowa.NumerUmowy
            row("Plik1") = _FakturaKosztowa.Plik1
            row("Plik2") = _FakturaKosztowa.Plik2
            row("User") = Credentials.DbUser

            MyDataSet.Tables("FakturyKosztowe").Rows.Add(row)
            InsertDataSetIntoTable("FakturyKosztowe")

        Else    'update existing row

            'aktualizuj najpierw rozrachunki księgowe przed zaktualizowaniem dokumentu
            KsiegowyModel.AktualizujDokumentRachunkowy(_FakturaKosztowa)
            'kontynuuj aktualizacje rzędu

            Dim SelectQuery = String.Format("ID = '{0}'", _FakturaKosztowa.Id.ToString())
            row = MyDataSet.Tables("FakturyKosztowe").Select(SelectQuery).First     'get row with give Id

            row.BeginEdit()

            row("NumerFaktury") = _FakturaKosztowa.NumerFaktury
            row("Sprzedawca") = _FakturaKosztowa.Sprzedawca
            row("DataWystawienia") = _FakturaKosztowa.DataWystawienia
            row("Opis") = _FakturaKosztowa.Opis
            row("Kwota") = _FakturaKosztowa.Kwota
            row("StawkaVAT") = _FakturaKosztowa.StawkaVAT
            row("KwotaPLN") = _FakturaKosztowa.KwotaPLN
            row("Waluta") = _FakturaKosztowa.Waluta
            row("Zaplacono") = _FakturaKosztowa.Zaplacono
            row("Konto") = _FakturaKosztowa.Konto
            row("KursZDnia") = _FakturaKosztowa.KursZDnia
            row("DoZwrotu") = _FakturaKosztowa.DoZwrotu
            row("CzyjKoszt") = _FakturaKosztowa.CzyjKoszt
            row("NumerUmowy") = _FakturaKosztowa.NumerUmowy
            row("Plik1") = _FakturaKosztowa.Plik1
            row("Plik2") = _FakturaKosztowa.Plik2
            row("User") = Credentials.DbUser

            row.EndEdit()
            InsertDataSetIntoTable("FakturyKosztowe")
        End If

    End Sub

    Public Overloads Sub AddFakturaPrzychodowa(ByRef _FakturaPrzychodowa As FakturaPrzychodowa)
        Dim row As DataRow

        If (_FakturaPrzychodowa.Id = 0) Or (_FakturaPrzychodowa.Id = vbNull) Then   'insert new row to datatable if row Id =0

            'dodaj dokument do rozrachunków księgowych
            KsiegowyModel.NowyDokumentRachunkowy(_FakturaPrzychodowa)
            'kontynuuj dodawanie nowego rzędu do dokumentów

            MyDataSet.Tables("FakturyPrzychodowe").Columns("Id").AllowDBNull = True
            row = MyDataSet.Tables("FakturyPrzychodowe").NewRow()

            row("NumerFaktury") = _FakturaPrzychodowa.NumerFaktury
            row("Klient") = _FakturaPrzychodowa.Klient
            row("DataWystawienia") = _FakturaPrzychodowa.DataWystawienia
            row("Opis") = _FakturaPrzychodowa.Opis
            row("Kwota") = _FakturaPrzychodowa.Kwota
            row("StawkaVAT") = _FakturaPrzychodowa.StawkaVAT
            row("KwotaPLN") = _FakturaPrzychodowa.KwotaPLN
            row("KursZDnia") = _FakturaPrzychodowa.KursZDnia
            row("Waluta") = _FakturaPrzychodowa.Waluta
            row("Zaplacono") = _FakturaPrzychodowa.Zaplacono
            row("Konto") = _FakturaPrzychodowa.Konto
            row("CzyjZysk") = _FakturaPrzychodowa.CzyjZysk
            row("NumerUmowy") = _FakturaPrzychodowa.NumerUmowy
            row("Plik1") = _FakturaPrzychodowa.Plik1
            row("Plik2") = _FakturaPrzychodowa.Plik2
            row("User") = Credentials.DbUser

            MyDataSet.Tables("FakturyPrzychodowe").Rows.Add(row)
            InsertDataSetIntoTable("FakturyPrzychodowe")



        Else    'update existing row

            'aktualizuj najpierw rozrachunki księgowe przed zaktualizowaniem dokumentu
            KsiegowyModel.AktualizujDokumentRachunkowy(_FakturaPrzychodowa)
            'kontynuuj aktualizacje rzędu

            Dim SelectQuery = String.Format("ID = '{0}'", _FakturaPrzychodowa.Id.ToString())
            row = MyDataSet.Tables("FakturyPrzychodowe").Select(SelectQuery).First     'get row with give Id



            row.BeginEdit()

            row("NumerFaktury") = _FakturaPrzychodowa.NumerFaktury
            row("Klient") = _FakturaPrzychodowa.Klient
            row("DataWystawienia") = _FakturaPrzychodowa.DataWystawienia
            row("Opis") = _FakturaPrzychodowa.Opis
            row("Kwota") = _FakturaPrzychodowa.Kwota
            row("StawkaVAT") = _FakturaPrzychodowa.StawkaVAT
            row("KwotaPLN") = _FakturaPrzychodowa.KwotaPLN
            row("KursZDnia") = _FakturaPrzychodowa.KursZDnia
            row("Waluta") = _FakturaPrzychodowa.Waluta
            row("Zaplacono") = _FakturaPrzychodowa.Zaplacono
            row("Konto") = _FakturaPrzychodowa.Konto
            row("CzyjZysk") = _FakturaPrzychodowa.CzyjZysk
            row("NumerUmowy") = _FakturaPrzychodowa.NumerUmowy
            row("Plik1") = _FakturaPrzychodowa.Plik1
            row("Plik2") = _FakturaPrzychodowa.Plik2
            row("User") = Credentials.DbUser

            row.EndEdit()
            InsertDataSetIntoTable("FakturyPrzychodowe")
        End If

    End Sub

    Public Overloads Sub AddDelegacja(ByRef _Delegacja As Delegacja)
        Dim row As DataRow

        If (_Delegacja.Id = 0) Or (_Delegacja.Id = vbNull) Then   'insert new row to datatable if row Id =0

            'dodaj dokument do rozrachunków księgowych
            KsiegowyModel.NowyDokumentRachunkowy(_Delegacja)
            'kontynuuj dodawanie nowego rzędu do dokumentów

            MyDataSet.Tables("Delegacje").Columns("Id").AllowDBNull = True
            row = MyDataSet.Tables("Delegacje").NewRow()

            row("Delegowany") = _Delegacja.Delegowany
            row("NumerDelegacji") = _Delegacja.NumerDelegacji
            row("NumerUmowy") = _Delegacja.NumerUmowy
            row("DataWyjazdu") = _Delegacja.DataWyjazdu
            row("WyjazdMiasto") = _Delegacja.WyjazdMiasto
            row("WyjazdTransport") = _Delegacja.WyjazdTransport
            row("DataPowrotu") = _Delegacja.DataPowrotu
            row("PowrotMiasto") = _Delegacja.PowrotMiasto
            row("PowrotTransport") = _Delegacja.PowrotTransport
            row("KrajWyjazdu") = _Delegacja.KrajWyjazdu
            row("MiejsceWyjazdu") = _Delegacja.MiejsceWyjazdu
            row("CelWyjazdu") = _Delegacja.CelWyjazdu
            row("CzasDelegacji") = _Delegacja.CzasDelegacji
            row("KwotaDelegacji") = _Delegacja.KwotaDelegacji
            row("DataRozliczenia") = _Delegacja.DataRozliczenia
            row("Waluta") = _Delegacja.Waluta
            row("KursZDnia") = _Delegacja.KursZDnia
            row("KwotaDelegacjiPLN") = _Delegacja.KwotaDelegacjiPLN
            row("Wyslano") = _Delegacja.Wyslano
            row("Wyplacono") = _Delegacja.Wyplacono
            row("Konto") = _Delegacja.Konto
            row("Plik1") = _Delegacja.Plik1
            row("Plik2") = _Delegacja.Plik2
            row("User") = Credentials.DbUser


            MyDataSet.Tables("Delegacje").Rows.Add(row)
            InsertDataSetIntoTable("Delegacje")

        Else    'update existing row

            'aktualizuj najpierw rozrachunki księgowe przed zaktualizowaniem dokumentu
            KsiegowyModel.AktualizujDokumentRachunkowy(_Delegacja)
            'kontynuuj aktualizacje rzędu

            Dim SelectQuery = String.Format("ID = '{0}'", _Delegacja.Id.ToString())
            row = MyDataSet.Tables("Delegacje").Select(SelectQuery).First     'get row with give Id

            row.BeginEdit()

            row("Delegowany") = _Delegacja.Delegowany
            row("NumerDelegacji") = _Delegacja.NumerDelegacji
            row("NumerUmowy") = _Delegacja.NumerUmowy
            row("DataWyjazdu") = _Delegacja.DataWyjazdu
            row("WyjazdMiasto") = _Delegacja.WyjazdMiasto
            row("WyjazdTransport") = _Delegacja.WyjazdTransport
            row("DataPowrotu") = _Delegacja.DataPowrotu
            row("PowrotMiasto") = _Delegacja.PowrotMiasto
            row("PowrotTransport") = _Delegacja.PowrotTransport
            row("KrajWyjazdu") = _Delegacja.KrajWyjazdu
            row("MiejsceWyjazdu") = _Delegacja.MiejsceWyjazdu
            row("CelWyjazdu") = _Delegacja.CelWyjazdu
            row("CzasDelegacji") = _Delegacja.CzasDelegacji
            row("KwotaDelegacji") = _Delegacja.KwotaDelegacji
            row("DataRozliczenia") = _Delegacja.DataRozliczenia
            row("Waluta") = _Delegacja.Waluta
            row("KursZDnia") = _Delegacja.KursZDnia
            row("KwotaDelegacjiPLN") = _Delegacja.KwotaDelegacjiPLN
            row("Wyslano") = _Delegacja.Wyslano
            row("Wyplacono") = _Delegacja.Wyplacono
            row("Konto") = _Delegacja.Konto
            row("Plik1") = _Delegacja.Plik1
            row("Plik2") = _Delegacja.Plik2
            row("User") = Credentials.DbUser

            row.EndEdit()
            InsertDataSetIntoTable("Delegacje")
        End If

    End Sub

    Public Overloads Sub AddUmowaDzielo(ByRef _UmowaDzielo As UmowaDzielo)
        Dim row As DataRow

        If (_UmowaDzielo.Id = 0) Or (_UmowaDzielo.Id = vbNull) Then   'insert new row to datatable if row Id =0

            'dodaj dokument do rozrachunków księgowych
            KsiegowyModel.NowyDokumentRachunkowy(_UmowaDzielo)
            'kontynuuj dodawanie nowego rzędu do dokumentów

            MyDataSet.Tables("UmowyDzielo").Columns("Id").AllowDBNull = True
            row = MyDataSet.Tables("UmowyDzielo").NewRow()

            row("NumerUmowy") = _UmowaDzielo.NumerUmowy
            row("Osoba") = _UmowaDzielo.Osoba
            row("DataPoczatek") = _UmowaDzielo.DataPoczatek
            row("DataKoniec") = _UmowaDzielo.DataKoniec
            row("SumaWydatkow") = _UmowaDzielo.SumaWydatkow
            row("SumaDiet") = _UmowaDzielo.SumaDiet
            row("SumaPrzychodow") = _UmowaDzielo.SumaPrzychodow
            row("Dziesiecina") = _UmowaDzielo.Dziesiecina
            row("KwotaBrutto") = _UmowaDzielo.KwotaBrutto
            row("KwotaNetto") = _UmowaDzielo.KwotaNetto
            row("KosztyUzyskPrzych") = _UmowaDzielo.KosztyUzyskPrzych
            row("ProgPodatkowy") = _UmowaDzielo.ProgPodatkowy
            row("Wyplacono") = _UmowaDzielo.Wyplacono
            row("Konto") = _UmowaDzielo.Konto
            row("Plik1") = _UmowaDzielo.Plik1
            row("Plik2") = _UmowaDzielo.Plik2
            row("User") = Credentials.DbUser

            MyDataSet.Tables("UmowyDzielo").Rows.Add(row)
            InsertDataSetIntoTable("UmowyDzielo")

        Else    'update existing row

            'aktualizuj najpierw rozrachunki księgowe przed zaktualizowaniem dokumentu
            KsiegowyModel.AktualizujDokumentRachunkowy(_UmowaDzielo)
            'kontynuuj aktualizacje rzędu

            Dim SelectQuery = String.Format("ID = '{0}'", _UmowaDzielo.Id.ToString())
            row = MyDataSet.Tables("UmowyDzielo").Select(SelectQuery).First     'get row with give Id

            row.BeginEdit()

            row("NumerUmowy") = _UmowaDzielo.NumerUmowy
            row("Osoba") = _UmowaDzielo.Osoba
            row("DataPoczatek") = _UmowaDzielo.DataPoczatek
            row("DataKoniec") = _UmowaDzielo.DataKoniec
            row("SumaWydatkow") = _UmowaDzielo.SumaWydatkow
            row("SumaDiet") = _UmowaDzielo.SumaDiet
            row("SumaPrzychodow") = _UmowaDzielo.SumaPrzychodow
            row("Dziesiecina") = _UmowaDzielo.Dziesiecina
            row("KwotaBrutto") = _UmowaDzielo.KwotaBrutto
            row("KwotaNetto") = _UmowaDzielo.KwotaNetto
            row("KosztyUzyskPrzych") = _UmowaDzielo.KosztyUzyskPrzych
            row("ProgPodatkowy") = _UmowaDzielo.ProgPodatkowy
            row("Wyplacono") = _UmowaDzielo.Wyplacono
            row("Konto") = _UmowaDzielo.Konto
            row("Plik1") = _UmowaDzielo.Plik1
            row("Plik2") = _UmowaDzielo.Plik2
            row("User") = Credentials.DbUser

            row.EndEdit()
            InsertDataSetIntoTable("UmowyDzielo")
        End If

    End Sub

    Public Overloads Sub AddOperationCSEG(ByRef _KontaCSEG As KontaCSEG)
        Dim row As DataRow


        MyDataSet.Tables("KontaCSEG").Columns("Id").AllowDBNull = True
        row = MyDataSet.Tables("KontaCSEG").NewRow()

        row("KontoPLN") = _KontaCSEG.KontoPLN
        row("KontoEUR") = _KontaCSEG.KontoEUR
        row("KontoGBP") = _KontaCSEG.KontoGBP
        row("SubKontoSpolka") = _KontaCSEG.SubKontoSpolka
        row("SubKontoWspolnicy") = _KontaCSEG.SubKontoWspolnicy
        row("SubKontoVAT") = _KontaCSEG.SubKontoVAT
        row("SubKontoCIT") = _KontaCSEG.SubKontoCIT
        row("SubKontoPIT") = _KontaCSEG.SubKontoPIT
        row("Operacja") = _KontaCSEG.Operacja
        row("ZKonta") = _KontaCSEG.ZKonta
        row("Kwota") = _KontaCSEG.Kwota
        row("Opis") = _KontaCSEG.Opis
        row("User") = Credentials.DbUser


        MyDataSet.Tables("KontaCSEG").Rows.Add(row)

        InsertDataSetIntoTable("KontaCSEG")
    End Sub

    Public Overloads Sub AddOperationWspolnicy(ByRef _Konto As KontoWspolnika)
        Dim row As DataRow

        MyDataSet.Tables("KontaWspolnicy").Columns("Id").AllowDBNull = True
        row = MyDataSet.Tables("KontaWspolnicy").NewRow()

        row("Osoba") = _Konto.Osoba
        row("Total") = _Konto.Total
        row("SubDelegacje") = _Konto.SubDelegacje
        row("SubUmowy") = _Konto.SubUmowy
        row("SubZwroty") = _Konto.SubZwroty
        row("Operacja") = _Konto.Operacja
        row("Kwota") = _Konto.Kwota
        row("Opis") = _Konto.Opis
        row("User") = Credentials.DbUser

        MyDataSet.Tables("KontaWspolnicy").Rows.Add(row)

        InsertDataSetIntoTable("KontaWspolnicy")
    End Sub

#End Region

#Region "Filling objects with data from row"

    '************ fill delegacja from data row **************************

    Public Shared Sub FillDelegacja(_DataRow As Data.DataRow, ByRef DelegacjaHandle As Delegacja)  'fill object faktura kosztowa with data from datarow
        If Not IsDBNull(_DataRow.Item("Id")) Then
            DelegacjaHandle.Id = _DataRow.Item("Id")
        End If
        If Not IsDBNull(_DataRow.Item("Delegowany")) Then
            DelegacjaHandle.Delegowany = _DataRow.Item("Delegowany")
        End If
        If Not IsDBNull(_DataRow.Item("NumerDelegacji")) Then
            DelegacjaHandle.NumerDelegacji = _DataRow.Item("NumerDelegacji")
        End If
        If Not IsDBNull(_DataRow.Item("NumerUmowy")) Then
            DelegacjaHandle.NumerUmowy = _DataRow.Item("NumerUmowy")
        End If
        If Not IsDBNull(_DataRow.Item("DataWyjazdu")) Then
            DelegacjaHandle.DataWyjazdu = _DataRow.Item("DataWyjazdu")
            DelegacjaHandle.GodzinaWyjazdu = _DataRow.Item("DataWyjazdu")
        End If
        If Not IsDBNull(_DataRow.Item("WyjazdMiasto")) Then
            DelegacjaHandle.WyjazdMiasto = _DataRow.Item("WyjazdMiasto")
        End If
        If Not IsDBNull(_DataRow.Item("WyjazdTransport")) Then
            DelegacjaHandle.WyjazdTransport = _DataRow.Item("WyjazdTransport")
        End If
        If Not IsDBNull(_DataRow.Item("DataPowrotu")) Then
            DelegacjaHandle.DataPowrotu = _DataRow.Item("DataPowrotu")
            DelegacjaHandle.GodzinaPowrotu = _DataRow.Item("DataPowrotu")
        End If
        If Not IsDBNull(_DataRow.Item("PowrotMiasto")) Then
            DelegacjaHandle.PowrotMiasto = _DataRow.Item("PowrotMiasto")
        End If
        If Not IsDBNull(_DataRow.Item("PowrotTransport")) Then
            DelegacjaHandle.PowrotTransport = _DataRow.Item("PowrotTransport")
        End If
        If Not IsDBNull(_DataRow.Item("MiejsceWyjazdu")) Then
            DelegacjaHandle.MiejsceWyjazdu = _DataRow.Item("MiejsceWyjazdu")
        End If
        If Not IsDBNull(_DataRow.Item("CelWyjazdu")) Then
            DelegacjaHandle.CelWyjazdu = _DataRow.Item("CelWyjazdu")
        End If
        If Not IsDBNull(_DataRow.Item("CzasDelegacji")) Then
            DelegacjaHandle.CzasDelegacji = _DataRow.Item("CzasDelegacji")
        End If
        If Not IsDBNull(_DataRow.Item("KwotaDelegacji")) Then
            DelegacjaHandle.KwotaDelegacji = _DataRow.Item("KwotaDelegacji")
        End If
        If Not IsDBNull(_DataRow.Item("DataRozliczenia")) Then
            DelegacjaHandle.DataRozliczenia = _DataRow.Item("DataRozliczenia")
        End If
        If Not IsDBNull(_DataRow.Item("Waluta")) Then
            DelegacjaHandle.Waluta = _DataRow.Item("Waluta")
        End If
        If Not IsDBNull(_DataRow.Item("KursZDnia")) Then
            DelegacjaHandle.KursZDnia = _DataRow.Item("KursZDnia")
        End If
        If Not IsDBNull(_DataRow.Item("KwotaDelegacjiPLN")) Then
            DelegacjaHandle.KwotaDelegacjiPLN = _DataRow.Item("KwotaDelegacjiPLN")
        End If
        If Not IsDBNull(_DataRow.Item("KrajWyjazdu")) Then
            DelegacjaHandle.KrajWyjazdu = _DataRow.Item("KrajWyjazdu")
        End If
        If Not IsDBNull(_DataRow.Item("Wyslano")) Then
            DelegacjaHandle.Wyslano = _DataRow.Item("Wyslano")
        End If
        If Not IsDBNull(_DataRow.Item("Wyplacono")) Then
            DelegacjaHandle.Wyplacono = _DataRow.Item("Wyplacono")
        End If
        If Not IsDBNull(_DataRow.Item("Konto")) Then
            DelegacjaHandle.Konto = _DataRow.Item("Konto")
        End If
        If Not IsDBNull(_DataRow.Item("Plik1")) Then
            DelegacjaHandle.Plik1 = _DataRow.Item("Plik1")
            ' Plik1Name = Path.GetFileName(DelegacjaHandle.Plik1)
        End If
        If Not IsDBNull(_DataRow.Item("Plik2")) Then
            DelegacjaHandle.Plik2 = _DataRow.Item("Plik2")
            ' Plik2Name = Path.GetFileName(DelegacjaHandle.Plik2)
        End If


    End Sub


    '************ fill faktura kosztowa from data row **************************

    Public Shared Sub FillFakturaKosztowa(_DataRow As Data.DataRow, ByRef FakturaKosztowaHandle As FakturaKosztowa)  'fill object faktura kosztowa with data from datarow
        If Not IsDBNull(_DataRow.Item("Id")) Then
            FakturaKosztowaHandle.Id = _DataRow.Item("Id")
        End If
        If Not IsDBNull(_DataRow.Item("NumerFaktury")) Then
            FakturaKosztowaHandle.NumerFaktury = _DataRow.Item("NumerFaktury")
        End If
        If Not IsDBNull(_DataRow.Item("Sprzedawca")) Then
            FakturaKosztowaHandle.Sprzedawca = _DataRow.Item("Sprzedawca")
        End If
        If Not IsDBNull(_DataRow.Item("DataWystawienia")) Then
            FakturaKosztowaHandle.DataWystawienia = _DataRow.Item("DataWystawienia")
        End If
        If Not IsDBNull(_DataRow.Item("Opis")) Then
            FakturaKosztowaHandle.Opis = _DataRow.Item("Opis")
        End If
        If Not IsDBNull(_DataRow.Item("Kwota")) Then
            FakturaKosztowaHandle.Kwota = _DataRow.Item("Kwota")
        End If
        If Not IsDBNull(_DataRow.Item("StawkaVAT")) Then
            FakturaKosztowaHandle.StawkaVAT = _DataRow.Item("StawkaVAT")
        End If
        If Not IsDBNull(_DataRow.Item("KwotaPLN")) Then
            FakturaKosztowaHandle.KwotaPLN = _DataRow.Item("KwotaPLN")
        End If
        If Not IsDBNull(_DataRow.Item("Waluta")) Then
            FakturaKosztowaHandle.Waluta = _DataRow.Item("Waluta")
        End If
        If Not IsDBNull(_DataRow.Item("Zaplacono")) Then
            FakturaKosztowaHandle.Zaplacono = _DataRow.Item("Zaplacono")
        End If
        If Not IsDBNull(_DataRow.Item("Konto")) Then
            FakturaKosztowaHandle.Konto = _DataRow.Item("Konto")
        End If
        If Not IsDBNull(_DataRow.Item("KursZDnia")) Then
            FakturaKosztowaHandle.KursZDnia = _DataRow.Item("KursZDnia")
        End If
        If Not IsDBNull(_DataRow.Item("DoZwrotu")) Then
            FakturaKosztowaHandle.DoZwrotu = _DataRow.Item("DoZwrotu")
        End If
        If Not IsDBNull(_DataRow.Item("CzyjKoszt")) Then
            FakturaKosztowaHandle.CzyjKoszt = _DataRow.Item("CzyjKoszt")
        End If
        If Not IsDBNull(_DataRow.Item("NumerUmowy")) Then
            FakturaKosztowaHandle.NumerUmowy = _DataRow.Item("NumerUmowy")
        End If
        If Not IsDBNull(_DataRow.Item("Plik1")) Then
            FakturaKosztowaHandle.Plik1 = _DataRow.Item("Plik1")
        End If
        If Not IsDBNull(_DataRow.Item("Plik2")) Then
            FakturaKosztowaHandle.Plik2 = _DataRow.Item("Plik2")
        End If
    End Sub


    '************ fill faktura przychodowa from data row **************************

    Public Shared Sub FillFakturaPrzychodowa(_DataRow As Data.DataRow, ByRef FakturaPrzychodowaHandle As FakturaPrzychodowa)  'fill object faktura kosztowa with data from datarow
        If Not IsDBNull(_DataRow.Item("Id")) Then
            FakturaPrzychodowaHandle.Id = _DataRow.Item("Id")
        End If
        If Not IsDBNull(_DataRow.Item("NumerFaktury")) Then
            FakturaPrzychodowaHandle.NumerFaktury = _DataRow.Item("NumerFaktury")
        End If
        If Not IsDBNull(_DataRow.Item("Klient")) Then
            FakturaPrzychodowaHandle.Klient = _DataRow.Item("Klient")
        End If
        If Not IsDBNull(_DataRow.Item("DataWystawienia")) Then
            FakturaPrzychodowaHandle.DataWystawienia = _DataRow.Item("DataWystawienia")
        End If
        If Not IsDBNull(_DataRow.Item("Opis")) Then
            FakturaPrzychodowaHandle.Opis = _DataRow.Item("Opis")
        End If
        If Not IsDBNull(_DataRow.Item("Kwota")) Then
            FakturaPrzychodowaHandle.Kwota = _DataRow.Item("Kwota")
        End If
        If Not IsDBNull(_DataRow.Item("StawkaVAT")) Then
            FakturaPrzychodowaHandle.StawkaVAT = _DataRow.Item("StawkaVAT")
        End If
        If Not IsDBNull(_DataRow.Item("KwotaPLN")) Then
            FakturaPrzychodowaHandle.KwotaPLN = _DataRow.Item("KwotaPLN")
        End If
        If Not IsDBNull(_DataRow.Item("KursZDnia")) Then
            FakturaPrzychodowaHandle.KursZDnia = _DataRow.Item("KursZDnia")
        End If
        If Not IsDBNull(_DataRow.Item("Waluta")) Then
            FakturaPrzychodowaHandle.Waluta = _DataRow.Item("Waluta")
        End If
        If Not IsDBNull(_DataRow.Item("Zaplacono")) Then
            FakturaPrzychodowaHandle.Zaplacono = _DataRow.Item("Zaplacono")
        End If
        If Not IsDBNull(_DataRow.Item("Konto")) Then
            FakturaPrzychodowaHandle.Konto = _DataRow.Item("Konto")
        End If
        If Not IsDBNull(_DataRow.Item("CzyjZysk")) Then
            FakturaPrzychodowaHandle.CzyjZysk = _DataRow.Item("CzyjZysk")
        End If
        If Not IsDBNull(_DataRow.Item("NumerUmowy")) Then
            FakturaPrzychodowaHandle.NumerUmowy = _DataRow.Item("NumerUmowy")
        End If
        If Not IsDBNull(_DataRow.Item("Plik1")) Then
            FakturaPrzychodowaHandle.Plik1 = _DataRow.Item("Plik1")
        End If
        If Not IsDBNull(_DataRow.Item("Plik2")) Then
            FakturaPrzychodowaHandle.Plik2 = _DataRow.Item("Plik2")
        End If
    End Sub


    '************ fill Umowa o dzieło from data row **************************
    Public Shared Sub FillUmowaDzielo(_DataRow As Data.DataRow, ByRef UmowaDzieloHandle As UmowaDzielo)  'fill object faktura kosztowa with data from datarow
        If Not IsDBNull(_DataRow.Item("Id")) Then
            UmowaDzieloHandle.Id = _DataRow.Item("Id")
        End If
        If Not IsDBNull(_DataRow.Item("NumerUmowy")) Then
            UmowaDzieloHandle.NumerUmowy = _DataRow.Item("NumerUmowy")
        End If
        If Not IsDBNull(_DataRow.Item("Osoba")) Then
            UmowaDzieloHandle.Osoba = _DataRow.Item("Osoba")
        End If
        If Not IsDBNull(_DataRow.Item("DataPoczatek")) Then
            UmowaDzieloHandle.DataPoczatek = _DataRow.Item("DataPoczatek")
        End If
        If Not IsDBNull(_DataRow.Item("DataKoniec")) Then
            UmowaDzieloHandle.DataKoniec = _DataRow.Item("DataKoniec")
        End If
        If Not IsDBNull(_DataRow.Item("SumaWydatkow")) Then
            UmowaDzieloHandle.SumaWydatkow = _DataRow.Item("SumaWydatkow")
        End If
        If Not IsDBNull(_DataRow.Item("SumaDiet")) Then
            UmowaDzieloHandle.SumaDiet = _DataRow.Item("SumaDiet")
        End If
        If Not IsDBNull(_DataRow.Item("SumaPrzychodow")) Then
            UmowaDzieloHandle.SumaPrzychodow = _DataRow.Item("SumaPrzychodow")
        End If
        If Not IsDBNull(_DataRow.Item("Dziesiecina")) Then
            UmowaDzieloHandle.Dziesiecina = _DataRow.Item("Dziesiecina")
        End If
        If Not IsDBNull(_DataRow.Item("KwotaBrutto")) Then
            UmowaDzieloHandle.KwotaBrutto = _DataRow.Item("KwotaBrutto")
        End If
        If Not IsDBNull(_DataRow.Item("KwotaNetto")) Then
            UmowaDzieloHandle.KwotaNetto = _DataRow.Item("KwotaNetto")
        End If
        If Not IsDBNull(_DataRow.Item("KosztyUzyskPrzych")) Then
            UmowaDzieloHandle.KosztyUzyskPrzych = _DataRow.Item("KosztyUzyskPrzych")
        End If
        If Not IsDBNull(_DataRow.Item("ProgPodatkowy")) Then
            UmowaDzieloHandle.ProgPodatkowy = _DataRow.Item("ProgPodatkowy")
        End If
        If Not IsDBNull(_DataRow.Item("Wyplacono")) Then
            UmowaDzieloHandle.Wyplacono = _DataRow.Item("Wyplacono")
        End If
        If Not IsDBNull(_DataRow.Item("Konto")) Then
            UmowaDzieloHandle.Konto = _DataRow.Item("Konto")
        End If
        If Not IsDBNull(_DataRow.Item("Plik1")) Then
            UmowaDzieloHandle.Plik1 = _DataRow.Item("Plik1")
        End If
        If Not IsDBNull(_DataRow.Item("Plik2")) Then
            UmowaDzieloHandle.Plik2 = _DataRow.Item("Plik2")
        End If

    End Sub

    '************ fill Konta CCSEG from data row **************************

    Public Shared Function FillKontaCSEG(_DataRow As Data.DataRow) As KontaCSEG  'fill object KontoCSEG with data from datarow
        Dim KontaCSEG = New KontaCSEG()
        If Not IsDBNull(_DataRow.Item("Id")) Then
            KontaCSEG.Id = _DataRow.Item("Id")
        End If
        If Not IsDBNull(_DataRow.Item("KontoPLN")) Then
            KontaCSEG.KontoPLN = _DataRow.Item("KontoPLN")
        End If
        If Not IsDBNull(_DataRow.Item("KontoEUR")) Then
            KontaCSEG.KontoEUR = _DataRow.Item("KontoEUR")
        End If
        If Not IsDBNull(_DataRow.Item("KontoGBP")) Then
            KontaCSEG.KontoGBP = _DataRow.Item("KontoGBP")
        End If
        If Not IsDBNull(_DataRow.Item("SubKontoSpolka")) Then
            KontaCSEG.SubKontoSpolka = _DataRow.Item("SubKontoSpolka")
        End If
        If Not IsDBNull(_DataRow.Item("SubKontoWspolnicy")) Then
            KontaCSEG.SubKontoWspolnicy = _DataRow.Item("SubKontoWspolnicy")
        End If
        If Not IsDBNull(_DataRow.Item("SubKontoVAT")) Then
            KontaCSEG.SubKontoVAT = _DataRow.Item("SubKontoVAT")
        End If
        If Not IsDBNull(_DataRow.Item("SubKontoCIT")) Then
            KontaCSEG.SubKontoCIT = _DataRow.Item("SubKontoCIT")
        End If
        If Not IsDBNull(_DataRow.Item("SubKontoPIT")) Then
            KontaCSEG.SubKontoPIT = _DataRow.Item("SubKontoPIT")
        End If
        If Not IsDBNull(_DataRow.Item("Operacja")) Then
            KontaCSEG.Operacja = _DataRow.Item("Operacja")
        End If
        If Not IsDBNull(_DataRow.Item("Kwota")) Then
            KontaCSEG.Kwota = _DataRow.Item("Kwota")
        End If
        If Not IsDBNull(_DataRow.Item("Opis")) Then
            KontaCSEG.Opis = _DataRow.Item("Opis")
        End If

        Return KontaCSEG
    End Function


    '************ fill KontoWspolnika from data row **************************

    Public Shared Function FillKontoWspolnika(Osoba As String) As KontoWspolnika  'fill object Kontowspolnika
        'declare temp data view to retrieve all records with specific osoba name
        Dim tempDataview As DataView
        tempDataview = New DataView(Startup.VMLocator.VMKontaWspolnicy.KontaWspolnicyDataTable) With {
                   .RowFilter = String.Format("Osoba LIKE '{0}' ", Osoba)
               }

        'get last row from this dataview ( last = most recent)
        Dim _DataRow = tempDataview.Item(tempDataview.Count - 1).Row

        'copy values frmo row to object kontowspolnika

        Dim Konto = New KontoWspolnika()
        If Not IsDBNull(_DataRow.Item("Id")) Then
            Konto.Id = _DataRow.Item("Id")
        End If
        If Not IsDBNull(_DataRow.Item("Osoba")) Then
            Konto.Osoba = _DataRow.Item("Osoba")
        End If
        If Not IsDBNull(_DataRow.Item("Total")) Then
            Konto.Total = _DataRow.Item("Total")
        End If
        If Not IsDBNull(_DataRow.Item("SubDelegacje")) Then
            Konto.SubDelegacje = _DataRow.Item("SubDelegacje")
        End If
        If Not IsDBNull(_DataRow.Item("SubUmowy")) Then
            Konto.SubUmowy = _DataRow.Item("SubUmowy")
        End If
        If Not IsDBNull(_DataRow.Item("SubZwroty")) Then
            Konto.SubZwroty = _DataRow.Item("SubZwroty")
        End If

        Return Konto
    End Function

#End Region

#Region "Removing Rows"

    Public Sub RemoveFakturaKosztowa(_Row As DataRow)

        'najpierw usun dokument z rozrachunkow
        'wypełnij obiekt  starego dokumentu wartościami z bazy danych
        Dim Dokument = New FakturaKosztowa()
        DataBaseModel.FillFakturaKosztowa(_Row, Dokument)

        If KsiegowyModel.UsunDokumentRachunkowy(Dokument) Then
            'usun Datarow
            _Row.Delete()
            'zapisz tabelę w bazie
            SaveTableToDatabase("FakturyKosztowe")
        Else
            Dim CustomMessagebo = New MessageBoxCustom("Nie udało sie usunąc dokumentu")
        End If
    End Sub

    Public Sub RemoveFakturaPrzychodowa(_Row As DataRow)
        'najpierw usun dokument z rozrachunkow
        'wypełnij obiekt  starego dokumentu wartościami z bazy danych
        Dim Dokument = New FakturaPrzychodowa()
        DataBaseModel.FillFakturaPrzychodowa(_Row, Dokument)

        If KsiegowyModel.UsunDokumentRachunkowy(Dokument) Then
            'usun Datarow
            _Row.Delete()
            'zapisz tabelę w bazie
            SaveTableToDatabase("FakturyPrzychodowe")
        Else
            Dim CustomMessagebo = New MessageBoxCustom("Nie udało sie usunąc dokumentu")
        End If
    End Sub

    Public Sub RemoveDelegacja(_Row As DataRow)
        'najpierw usun dokument z rozrachunkow
        'wypełnij obiekt  starego dokumentu wartościami z bazy danych
        Dim Dokument = New Delegacja()
        DataBaseModel.FillDelegacja(_Row, Dokument)

        If KsiegowyModel.UsunDokumentRachunkowy(Dokument) Then
            'usun Datarow
            _Row.Delete()
            'zapisz tabelę w bazie
            SaveTableToDatabase("Delegacje")
        Else
            Dim CustomMessagebo = New MessageBoxCustom("Nie udało sie usunąc dokumentu")
        End If
    End Sub

    Public Sub RemoveUmowaDzielo(_Row As DataRow)
        'najpierw usun dokument z rozrachunkow
        'wypełnij obiekt  starego dokumentu wartościami z bazy danych
        Dim Dokument = New UmowaDzielo()
        DataBaseModel.FillUmowaDzielo(_Row, Dokument)

        If KsiegowyModel.UsunDokumentRachunkowy(Dokument) Then
            'usun Datarow
            _Row.Delete()
            'zapisz tabelę w bazie
            SaveTableToDatabase("UmowyDzielo")
        Else
            Dim CustomMessagebo = New MessageBoxCustom("Nie udało sie usunąc dokumentu")
        End If
    End Sub
#End Region




End Class
