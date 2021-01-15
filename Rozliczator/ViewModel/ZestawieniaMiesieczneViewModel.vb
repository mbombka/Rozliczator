Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Globalization
Imports System.Threading
Imports Microsoft.Office.Interop
Imports Microsoft.Win32

Public Class ZestawieniaMiesieczneViewModel

    Implements INotifyPropertyChanged
#Region "Events"

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region
#Region "Properties"
    Private _ZestawienieDataTable As DataTable

    Public Property ZestawienieDataTable() As DataTable
        Get
            _ZestawienieDataTable = Startup.VMLocator.VMFakturyKosztowe.FakturyKosztoweTable.Copy()

            Return _ZestawienieDataTable
        End Get

        Set(ByVal value As DataTable)
            _ZestawienieDataTable = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ZestawienieDataTable"))
        End Set
    End Property
    Private _ZestawieniaDataView As DataView
    Public Property ZestawieniaDataView As DataView
        Get
            '_ZestawieniaDataView = New DataView(_FakturyPrzychodoweTable)

            Return _ZestawieniaDataView
        End Get
        Set(value As DataView)
            _ZestawieniaDataView = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ZestawieniaDataView"))
        End Set
    End Property



    Private Property _Osoba As String
    Public Property Osoba As String
        Get
            Return _Osoba
        End Get
        Set(value As String)

            _Osoba = value
            FillDataView()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Osoba"))
        End Set
    End Property


    Private Property _Miesiac As String
    Public Property Miesiac As String
        Get
            Return _Miesiac
        End Get
        Set(value As String)

            _Miesiac = value
            FillDataView()
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("Miesiac"))
        End Set
    End Property
#End Region
#Region "Subs"
    Public Sub FillDataView()

        'fill dataview only when there are values
        If Miesiac <> "" And Osoba <> "" Then

            'get first and last day of filtered month
            Dim firstDay As Date
            'calculate date of 12 months from today
            If KsiegowyModel.MiesiacNumer(Miesiac) <= DateAndTime.Now.Month Then
                firstDay = New DateTime(Today.Year, KsiegowyModel.MiesiacNumer(Miesiac), 1)
            Else
                firstDay = New DateTime(Today.Year - 1, KsiegowyModel.MiesiacNumer(Miesiac), 1)
            End If

            'first day Of Next month minus one day
            Dim lastDay = firstDay.AddMonths(1).AddDays(-1)

            ZestawieniaDataView = New DataView(ZestawienieDataTable) With {
                   .RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "CzyjKoszt LIKE '{0}' AND DataWystawienia > #{1}#  AND DataWystawienia < #{2}#", Osoba, firstDay, lastDay)
               }
        End If


    End Sub
#End Region
#Region "exportToExcel"

    'create separate thread for exporting excel
    Public Sub ZestawienieToExcel(_ZestawienieDataView As DataView)
        Dim newThread As New Thread(New ThreadStart(Sub() EksportujZestawienie(_ZestawienieDataView)))
        'start thread, closing of thread is implemented at the end of sub
        newThread.Start()

    End Sub


    Private Sub EksportujZestawienie(_zestawienieDataView As DataView)
        Dim _excel As New Excel.Application
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim i As Integer
        'Dim j As Integer


        wBook = _excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        If KsiegowyModel.MiesiacNumer(Miesiac) <= DateAndTime.Now.Month Then
            wSheet.Name = Miesiac & "." & DateAndTime.Now.Year.ToString()
        Else
            wSheet.Name = Miesiac & "." & (DateAndTime.Now.Year - 1).ToString()
        End If

        '**** Tytułowa komórka****
        wSheet.Range("A2:B2").Merge()
        wSheet.Range("A2").Value = "Osoba Ponosząca wydatki:"
        wSheet.Range("A2").Font.Size = 14
        wSheet.Range("A2").Font.Bold = True
        wSheet.Range("A2").VerticalAlignment = Excel.Constants.xlCenter
        wSheet.Range("A2").HorizontalAlignment = Excel.Constants.xlCenter

        wSheet.Range("C2:D2").Merge()
        wSheet.Range("C2").VerticalAlignment = Excel.Constants.xlCenter
        wSheet.Range("C2").HorizontalAlignment = Excel.Constants.xlCenter


        'format row height
        wSheet.Range("A1:A4").RowHeight = 15
        wSheet.Range("A5").RowHeight = 30
        wSheet.Range("A6:A100").RowHeight = 15
        'format fonts
        wSheet.Range("A5:E5").Font.Size = 11
        wSheet.Range("A5:E5").Font.Bold = True


        'format cell width
        wSheet.Range("A1").ColumnWidth = 8
        wSheet.Range("B1").ColumnWidth = 25
        wSheet.Range("C1").ColumnWidth = 15
        wSheet.Range("D1").ColumnWidth = 10
        wSheet.Range("E1").ColumnWidth = 10
        wSheet.Range("F1").ColumnWidth = 10

        'format borders
        'first small borders in all cells
        wSheet.Range("A5:G100").Cells.Borders.LineStyle = Excel.XlLineStyle.xlContinuous
        wSheet.Range("A5:G100").Cells.Borders.Weight = Excel.XlBorderWeight.xlThin

        'fill with gray
        wSheet.Range("A5:F5").Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

        'fill cells with values
        wSheet.Range("C2").Value = KsiegowyModel.DelegowanyFullName(Osoba)

        wSheet.Range("A5").Value = "Lp."
        wSheet.Range("B5").Value = "Sprzedawca"
        wSheet.Range("C5").Value = "Data wystawienia"
        wSheet.Range("D5").Value = "Rodzaj Kosztu"
        wSheet.Range("E5").Value = "Kwota"
        wSheet.Range("F5").Value = "Waluta"

        'fill cells from dataview
        Dim offset = 6 'worksheet row  where to load data
        Dim tempDataTable = _zestawienieDataView.ToTable()
        Dim tempData As Date

        For i = 0 To _zestawienieDataView.Count - 1
            wSheet.Cells(offset + i, 1) = i + 1 'index
            wSheet.Cells(offset + i, 2) = _zestawienieDataView.Item(i).Item("Sprzedawca")
            tempData = _zestawienieDataView.Item(i).Item("DataWystawienia").ToString()
            wSheet.Cells(offset + i, 3) = tempData.ToShortDateString()
            wSheet.Cells(offset + i, 4) = _zestawienieDataView.Item(i).Item("Opis").ToString()
            wSheet.Cells(offset + i, 5) = _zestawienieDataView.Item(i).Item("Kwota")
            wSheet.Cells(offset + i, 6) = _zestawienieDataView.Item(i).Item("Waluta").ToString()
        Next


        'open save winow

        Dim saveFileDialog = New SaveFileDialog()
        saveFileDialog.Title = "Zapisz zestawienie jako"
        saveFileDialog.FileName = "Wydatki Służbwe" & Osoba
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
