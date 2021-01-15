Imports System.Collections.ObjectModel
Imports System.Globalization

Public Class WalutyModel
#Region "Property"


    Public Shared ReadOnly Property Waluty() As ObservableCollection(Of String)
        Get
            Dim listaWalut = New ObjectModel.ObservableCollection(Of String) From {
                "PLN",
                "EUR",
                "GBP",
                "CHF",
                "CZK",
                "NOK",
                "RUB",
                "USD"
            }

            Return listaWalut
        End Get

    End Property

    Public Shared ReadOnly Property listaKraje() As ObservableCollection(Of String)
        Get
            Dim ListKraji = New ObjectModel.ObservableCollection(Of String) From {
                "Polska",
                "Holandia",
                "Anglia",
                "Irlandia",
                "Niemcy",
                "Rosja",
                "Turcja",
                "Czechy",
                "Szwajcaria",
                "U.S.A."
            }

            Return ListKraji
        End Get

    End Property

    Public Shared ReadOnly Property Kraje() As IList(Of Kraj)
        Get
            Dim listaKrajow = New ObjectModel.ObservableCollection(Of Kraj)

            listaKrajow.Add(New Kraj() With {
             .Nazwa = "Polska",
             .StawkaDiety = 30,
             .Waluta = "PLN"
              })
            listaKrajow.Add(New Kraj() With {
             .Nazwa = "Holandia",
             .StawkaDiety = 50,
             .Waluta = "EUR"
              })
            listaKrajow.Add(New Kraj() With {
             .Nazwa = "Anglia",
             .StawkaDiety = 35,
             .Waluta = "GBP"
              })
            listaKrajow.Add(New Kraj() With {
             .Nazwa = "Irlandia",
             .StawkaDiety = 52,
             .Waluta = "EUR"
              })
            listaKrajow.Add(New Kraj() With {
             .Nazwa = "Niemcy",
             .StawkaDiety = 49,
             .Waluta = "EUR"
              })
            listaKrajow.Add(New Kraj() With {
             .Nazwa = "Rosja",
             .StawkaDiety = 48,
             .Waluta = "EUR"
              })
            listaKrajow.Add(New Kraj() With {
             .Nazwa = "Turcja",
             .StawkaDiety = 53,
             .Waluta = "USD"
              })
            listaKrajow.Add(New Kraj() With {
             .Nazwa = "Czechy",
             .StawkaDiety = 41,
             .Waluta = "EUR"
              })
            listaKrajow.Add(New Kraj() With {
             .Nazwa = "Szwajcaria",
             .StawkaDiety = 88,
             .Waluta = "CHF"
              })
            listaKrajow.Add(New Kraj() With {
             .Nazwa = "U.S.A.",
             .StawkaDiety = 59,
             .Waluta = "USD"
              })

            Return listaKrajow
        End Get

    End Property
#End Region

#Region "Funtions"
    Public Shared Function KursZDnia(_Date As Date, _Waluta As String) As Decimal

        If _Waluta = vbNullString Or            'nothign to pass 
            _Date.Year < 2000 Or                'to yearly to even check 
           _Date > DateTime.Now Then            'back in to future?
            Return 0
        End If
        If _Waluta = "PLN" Then
            Return 1
        End If

        Dim TempKurs As Decimal
        'declare specific date time culture
        Dim dtfi As DateTimeFormatInfo = CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat
        dtfi.DateSeparator = "-"        'specify separator
        dtfi.ShortDatePattern = "yyyy/MM/dd"

        Dim _DateString = _Date.ToString("d", dtfi) 'convert date to string with format: yyyy-mm-dd 

        'try to get actual currency 5 times , each time subtract one day ( in case that there NBP was not working that day
        Dim tempCounter As Integer
        While tempCounter < 5
            Try
                Dim connStr = String.Format("http://api.nbp.pl/api/exchangerates/rates/a/{0}/{1}/?format=xml", _Waluta, _DateString)    'string for NBP API
                Dim MyData As String    'temporary handle for API response
                Dim MyDoc As New System.Xml.XmlDocument
                Using WC As New System.Net.WebClient()
                    MyData = WC.DownloadString(connStr) 'get response string from NBP
                End Using
                MyDoc.LoadXml(MyData)                                                                               'convert response string toXML
                Dim SymbolText As String = MyDoc.SelectSingleNode("//ExchangeRatesSeries/Rates/Rate/Mid").InnerText 'get values of specifc node from XML ( średni kurs)
                TempKurs = Decimal.Parse(SymbolText, CultureInfo.InvariantCulture)                                   'convert string from xml to deimal value
                tempCounter = 10 'exit loop
            Catch ex As Exception   'if something goes wrong, like there is no cur in given day #TODO implement something more elegant that exceptions..
                tempCounter = tempCounter + 1
                _Date = _Date.AddDays(-1)   'subtract one day from date 
                _DateString = _Date.ToString("d", dtfi)
                TempKurs = 0

            End Try
        End While

        Return TempKurs


    End Function




#End Region
End Class
