Imports System.Collections.ObjectModel
Imports MySql.Data
Imports System.Data
Imports System.Linq
Public Class KsiegowyModel
#Region "Properties"

    Public Const Dziesiecina As Decimal = 0.1
    Public Const ResztaZDziesieciny As Decimal = 1 - Dziesiecina

    Public Shared ReadOnly Property Konta() As ObservableCollection(Of String)
        Get
            Dim ListaKont = New ObjectModel.ObservableCollection(Of String) From {
                "brak",
                "Konto PLN",
                "Konto EUR",
                "Konto GBP"
            }

            Return ListaKont
        End Get
    End Property

    'lista wspolnikow do poprawnego wyswietlania w formatkach
    Public Shared ReadOnly Property Wspolnicy() As ObservableCollection(Of String)
        Get
            Dim ListaWspolnicy = New ObjectModel.ObservableCollection(Of String) From {
                "PPawlowski",
                "POstrowski",
                "MBabka"
            }

            Return ListaWspolnicy
        End Get

    End Property

    'lista wspolnikow + CSEG 
    Public Shared ReadOnly Property Osoby() As ObservableCollection(Of String)
        Get
            Dim ListaOsoby = New ObjectModel.ObservableCollection(Of String) From {
                "CSEG",
                "PPawlowski",
                "POstrowski",
                "MBabka"
            }

            Return ListaOsoby
        End Get

    End Property

    'lista kosztów uzyskania przychodów dla umów o dzielo w % (20% i 50%)
    Public Shared ReadOnly Property KosztyUzyskPrzychList() As ObservableCollection(Of Integer)
        Get
            Dim ListaKoszty = New ObjectModel.ObservableCollection(Of Integer) From {
                50,
                20
            }

            Return ListaKoszty
        End Get

    End Property

    'lista progów podatkowych dla PIT na rok 2018
    Public Shared ReadOnly Property ProgiPodatkowe() As ObservableCollection(Of Integer)
        Get
            Dim ListaProgi = New ObjectModel.ObservableCollection(Of Integer) From {
                18,
                32
            }

            Return ListaProgi
        End Get

    End Property

    'lista stawek VAT na rok 2018
    Public Shared ReadOnly Property StawkiVAT() As ObservableCollection(Of Integer)
        Get
            Dim ListaVAT = New ObjectModel.ObservableCollection(Of Integer) From {
                0,
                8,
                23
            }

            Return ListaVAT
        End Get

    End Property

    'pomocnicza lista miesięcy do ładnego wyświetlania
    Public Shared ReadOnly Property Miesiace() As ObservableCollection(Of String)
        Get
            Dim ListaMiesiace = New ObjectModel.ObservableCollection(Of String) From {
                "Styczeń",
                "Luty",
                "Marzec",
                "Kwiecień",
                "Maj",
                "Czerwiec",
                "Lipiec",
                "Sierpień",
                "Wrzesień",
                "Październik",
                "Listopad",
                "Grudzień"
            }

            Return ListaMiesiace
        End Get

    End Property

    'lista ręcznych operacji na koncie CSEG
    Public Shared ReadOnly Property OperacjeNaKoncieCSEG() As ObservableCollection(Of String)
        Get
            Dim lista = New ObjectModel.ObservableCollection(Of String) From {
                "Konto PLN",
                "Konto EUR",
                "Konto GBP",
                "Przelew podatku CIT",
                "Przelew podatku VAT",
                "Przelew podatku PIT",
                "SubKonto Spolka",
                "SubKonto Wspolnicy",
                "SubKonto VAT",
                "SubKonto CIT",
                "SubKonto PIT"
            }

            Return lista
        End Get
    End Property

    'lista ręcznych operacji na koncie wspolnika
    Public Shared ReadOnly Property OperacjeNaKoncieWspolnika() As ObservableCollection(Of String)
        Get
            Dim lista = New ObjectModel.ObservableCollection(Of String) From {
                "Total",
                "SubKonto Delegacje",
                "SubKonto Umowy",
                "SubKonto Zwroty"
            }
            Return lista
        End Get
    End Property
#End Region

#Region "Subs and funtions"

    'podlicz umowę o dzieło: kwota netto do wypłaty = przychód - delegacje - koszty
    Public Shared Sub PrzeliczUmowe(ByRef _UmowaHandle As UmowaDzielo)
        _UmowaHandle.SumaWydatkow = SumaKosztowUmowy(_UmowaHandle.NumerUmowy)
        _UmowaHandle.SumaPrzychodow = SumaPrzychUmowy(_UmowaHandle.NumerUmowy)
        _UmowaHandle.SumaDiet = SumaDelegacje(_UmowaHandle.NumerUmowy)
        _UmowaHandle.Dziesiecina = _UmowaHandle.SumaPrzychodow * 0.1
        _UmowaHandle.KwotaBruttoSugerowana = Math.Round(_UmowaHandle.SumaPrzychodow - _UmowaHandle.SumaWydatkow - _UmowaHandle.SumaDiet - _UmowaHandle.Dziesiecina, 2)
        _UmowaHandle.KwotaNettoSugerowana = Math.Round(_UmowaHandle.KwotaBruttoSugerowana - (_UmowaHandle.KwotaBruttoSugerowana * (1 - (_UmowaHandle.KosztyUzyskPrzych * 0.01)) * (_UmowaHandle.ProgPodatkowy * 0.01)), 2)
        _UmowaHandle.KwotaNetto = _UmowaHandle.KwotaNettoSugerowana
        _UmowaHandle.KwotaBrutto = _UmowaHandle.KwotaBruttoSugerowana
    End Sub

    'przelicz kwote brutto do umowy o dzieło bazujac na kwocie netto
    Public Shared Sub PrzeliczUmoweBrutto(ByRef _UmowaHandle As UmowaDzielo)
        _UmowaHandle.KwotaBrutto = Math.Round(_UmowaHandle.KwotaNetto / (1 - ((_UmowaHandle.KosztyUzyskPrzych * 0.01) * (_UmowaHandle.ProgPodatkowy * 0.01))), 2)

    End Sub

    'wykonaj ręczną operacje na koncie bankowym lub wirtualnym subkoncie CSEG
    Public Shared Function OperacjaKontoCSEG(ByRef Konto As KontaCSEG, operacja As String, kwota As Decimal) As Boolean
        If Konto Is Nothing Then
            Dim messagebox = New MessageBoxCustom("brak infomarcji o koncie")
            messagebox.Show()
            Return False
        End If

        Select Case operacja
            Case "Konto PLN"
                Konto.KontoPLN += kwota
                Return True
            Case "Konto EUR"
                Konto.KontoEUR += kwota
                Return True
            Case "Konto GBP"
                Konto.KontoGBP += kwota
                Return True
            Case "Przelew podatku CIT"
                Konto.KontoPLN -= kwota
                Konto.SubKontoCIT -= kwota
                Return True
            Case "Przelew podatku VAT"
                Konto.KontoPLN -= kwota
                Konto.SubKontoVAT -= kwota
                Return True
            Case "Przelew podatku PIT"
                Konto.KontoPLN -= kwota
                Konto.SubKontoPIT -= kwota
                Return True
            Case "SubKonto Spolka"
                Konto.SubKontoSpolka += kwota
                Return True
            Case "SubKonto Wspolnicy"
                Konto.SubKontoWspolnicy += kwota
                Return True
            Case "SubKonto VAT"
                Konto.SubKontoVAT += kwota
                Return True
            Case "SubKonto CIT"
                Konto.SubKontoCIT += kwota
                Return True
            Case "SubKonto PIT"
                Konto.SubKontoPIT += kwota
                Return True
            Case Else
                Return False
        End Select


    End Function


    'wykonaj ręczną operacje na koncie bankowym lub wirtualnym subkoncie Wspolnika
    Public Shared Function OperacjaKontoWspolnika(ByRef Konto As KontoWspolnika, operacja As String, kwota As Decimal) As Boolean
        If Konto Is Nothing Then
            Dim messagebox = New MessageBoxCustom("brak infomarcji o koncie")
            messagebox.Show()
            Return False
        End If

        Select Case operacja
            Case "Total"
                Konto.Total += kwota
                Return True
            Case "SubKonto Delegacje"
                Konto.SubDelegacje += kwota
                Konto.Total += kwota
                Return True
            Case "SubKonto Umowy"
                Konto.SubUmowy += kwota
                Konto.Total += kwota
                Return True
            Case "SubKonto Zwroty"
                Konto.SubZwroty -= kwota
                Konto.Total += kwota
                Return True
            Case Else
                Return False
        End Select


    End Function

    'not used wykonaj ręczną operacje na koncie bannkowym lub wirtualnym subkoncie
    Public Shared Sub SubOperacjaKontoCSEG(ByRef Konto As KontaCSEG, operacja As String, kwota As Decimal)
        If Konto Is Nothing Then
            Dim messagebox = New MessageBoxCustom("brak infomarcji o koncie")
            messagebox.Show()
            Return
        End If

        Select Case operacja
            Case "Konto PLN"
                Konto.KontoPLN += kwota

            Case "Konto EUR"
                Konto.KontoEUR += kwota

            Case "Konto GBP"
                Konto.KontoGBP += kwota

            Case "Przelew podatku CIT"
                Konto.KontoPLN -= kwota
                Konto.SubKontoCIT -= kwota

            Case "Przelew podatku VAT"
                Konto.KontoPLN -= kwota
                Konto.SubKontoVAT -= kwota

            Case "Przelew podatku PIT"
                Konto.KontoPLN -= kwota
                Konto.SubKontoPIT -= kwota

            Case "SubKonto Spolka"
                Konto.SubKontoSpolka += kwota

            Case "SubKonto Wspolnicy"
                Konto.SubKontoWspolnicy += kwota

            Case "SubKonto VAT"
                Konto.SubKontoVAT += kwota

            Case "SubKonto CIT"
                Konto.SubKontoCIT += kwota

            Case "SubKonto PIT"
                Konto.SubKontoPIT += kwota

            Case Else
                Dim messagebox = New MessageBoxCustom("nie zaimplementowano takiej operacji")
                messagebox.Show()
                Return
        End Select
    End Sub


#Region "Operacje automatyczne na kontach"


    '******* Ważne********
    'Ogolna idea na dzień 29.07.2018 : DLa każdego dokuemtnu typu faktura kosztowa, przychodowa, delegacja, umowa dzieło, mogą być 2 operacje:
    ' 1) dodanie nowego dokumentu => to aktualizuje stan sub kont ( bo np VAT oraz CIT liczy się od dnia wystawienia dokumentu)
    ' w czasie tej operacji flaga zapłacono może być ustawiona, i wtedy od razu konto rzeczywste zostanie zaktualizowane
    '2) usunięcie dokumentu - jak dodanie tylko że zamiast dodawania jest odejmowanie i szapoba ;)
    '3) zaktualizowanie dokumentu - usuwamy stary dokument z rozrachunków i wstawiamy nowy. Uznałem że jest to prostsze niż porównywanie wszystkich pól po kolei..


    'dodanie nowej faktury kosztowej do rozrachunku. update kont i sub kont
    Public Overloads Shared Function NowyDokumentRachunkowy(Dokument As FakturaKosztowa) As Boolean
        'get handles of current account status
        Dim kontoCSEG = Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle
        Dim KontoPP = Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski
        Dim KontoPO = Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski
        Dim KontoMB = Startup.VMLocator.VMKontaWspolnicy.KontoMBabka


        'zakutalizuj stan kont rzeczywistych (odejmij kwota nett + VAT)
        If Dokument.Zaplacono Then
            Select Case Dokument.Konto
                Case "Konto PLN"
                    kontoCSEG.KontoPLN -= (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case "Konto EUR"
                    kontoCSEG.KontoEUR -= (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case "Konto GBP"
                    kontoCSEG.KontoGBP -= (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case Else
                    Return False
            End Select
        End If

        'zaktualiz stan subkont wspólnych dla kazdej faktury ( każda faktura kosztowa to mniejszy CIT i VAT)
        kontoCSEG.SubKontoCIT -= Dokument.KwotaPLN
        kontoCSEG.SubKontoVAT -= Dokument.KwotaPLN * Dokument.StawkaVAT / 100


        'jeżeli faktura jest do zwrotu to zaktualizuj subkonto zwroty danego wspolnika
        'Jeżei nie jest do zwrotu to odejmij od głownego subkonta danego gagatka i zaktualizuj to konto
        Select Case Dokument.CzyjKoszt
            Case "CSEG"
                kontoCSEG.SubKontoSpolka -= Dokument.KwotaPLN
            Case "PPawlowski"
                If Dokument.DoZwrotu Then
                    KontoPP.SubZwroty += Dokument.KwotaPLN

                    KontoPP.Operacja = "Faktura Kosztowa"
                    KontoPP.Kwota = Dokument.KwotaPLN
                    KontoPP.Opis = "Zapłata Faktury Kosztowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPP)
                End If

            Case "POstrowski"
                If Dokument.DoZwrotu Then
                    KontoPO.SubZwroty += Dokument.KwotaPLN

                    KontoPO.Operacja = "Faktura Kosztowa"
                    KontoPO.Kwota = Dokument.KwotaPLN
                    KontoPO.Opis = "Zapłata Faktury Kosztowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPO)
                End If

            Case "MBabka"
                If Dokument.DoZwrotu Then
                    KontoMB.SubZwroty += Dokument.KwotaPLN

                    KontoMB.Operacja = "Faktura Kosztowa"
                    KontoMB.Kwota = Dokument.KwotaPLN
                    KontoMB.Opis = "Zapłata Faktury Kosztowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoMB)
                End If

            Case Else
                Dim messagebox As New MessageBoxCustom("Coś poszło nie tak przy sumowaniu subkont, sprawdź zgodność")
                Return False
        End Select


        kontoCSEG.Operacja = "Faktura Kosztowa"
        kontoCSEG.Opis = "Dodanie nowej Faktury Kosztowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
        'dodaj nowy record do tabeli kontoCSEG
        Startup.MainDataBaseModel.AddOperationCSEG(kontoCSEG)

        'update status of handles from updated values
        Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle = kontoCSEG
        Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski = KontoPP
        Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski = KontoPO
        Startup.VMLocator.VMKontaWspolnicy.KontoMBabka = KontoMB

        Return True

        Return True
    End Function

    'Usunięcie faktury kosztowej z rozrachunków. update kont i sub kont
    Public Overloads Shared Function UsunDokumentRachunkowy(Dokument As FakturaKosztowa) As Boolean
        'get handles of current account status
        Dim kontoCSEG = Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle
        Dim KontoPP = Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski
        Dim KontoPO = Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski
        Dim KontoMB = Startup.VMLocator.VMKontaWspolnicy.KontoMBabka


        'zakutalizuj stan kont rzeczywistych (Dodaj kwota nett + VAT)
        If Dokument.Zaplacono Then
            Select Case Dokument.Konto
                Case "Konto PLN"
                    kontoCSEG.KontoPLN += (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case "Konto EUR"
                    kontoCSEG.KontoEUR += (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case "Konto GBP"
                    kontoCSEG.KontoGBP += (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case Else
                    Return False
            End Select
        End If

        'zaktualiz stan subkont wspólnych dla kazdej faktury ( każda faktura kosztowa to mniejszy CIT i VAT)
        kontoCSEG.SubKontoCIT += Dokument.KwotaPLN
        kontoCSEG.SubKontoVAT += Dokument.KwotaPLN * Dokument.StawkaVAT / 100


        'jeżeli faktura jest do zwrotu to zaktualizuj subkonto zwroty danego wspolnika
        'Jeżei nie jest do zwrotu to odejmij od głownego subkonta danego gagatka i zaktualizuj to konto
        Select Case Dokument.CzyjKoszt
            Case "CSEG"
                kontoCSEG.SubKontoSpolka += Dokument.KwotaPLN
            Case "PPawlowski"
                If Dokument.DoZwrotu Then
                    KontoPP.SubZwroty -= Dokument.KwotaPLN

                    KontoPP.Operacja = "Usuniecie Faktura Kosztowa"
                    KontoPP.Kwota = Dokument.KwotaPLN
                    KontoPP.Opis = "Usuniecie Faktury Kosztowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPP)
                End If

            Case "POstrowski"
                If Dokument.DoZwrotu Then
                    KontoPO.SubZwroty -= Dokument.KwotaPLN

                    KontoPO.Operacja = "Usuniecie Faktura Kosztowa"
                    KontoPO.Kwota = Dokument.KwotaPLN
                    KontoPO.Opis = "Usuniecie Faktury Kosztowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPO)
                End If

            Case "MBabka"
                If Dokument.DoZwrotu Then
                    KontoMB.SubZwroty -= Dokument.KwotaPLN

                    KontoMB.Operacja = "Usuniecie Faktura Kosztowa"
                    KontoMB.Kwota = Dokument.KwotaPLN
                    KontoMB.Opis = "Usuniecie Faktury Kosztowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoMB)
                End If

            Case Else
                Dim messagebox As New MessageBoxCustom("Coś poszło nie tak przy sumowaniu subkont, sprawdź zgodność")
                Return False
        End Select


        kontoCSEG.Operacja = "Usuniecie Faktura Kosztowa"
        kontoCSEG.Opis = "Usuniecie Faktury Kosztowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
        'dodaj nowy record do tabeli kontoCSEG
        Startup.MainDataBaseModel.AddOperationCSEG(kontoCSEG)

        Return True
    End Function

    'zaktualizuj stan konta  gdy faktura kosztowa została zmodyfikowana
    Public Overloads Shared Function AktualizujDokumentRachunkowy(Dokument As FakturaKosztowa) As Boolean

        'pobierz oryginalną wersję dokumentu z bazy danych do datarow
        Dim SelectQuery = String.Format("ID = '{0}'", Dokument.Id.ToString())
        Dim DataRow = Startup.VMLocator.VMFakturyKosztowe.FakturyKosztoweTable.Select(SelectQuery).First     'get row with given Id

        'wypełnij obiekt  starego dokumentu wartościami z bazy danych
        Dim OldDokument = New FakturaKosztowa()
        DataBaseModel.FillFakturaKosztowa(DataRow, OldDokument)

        'usuń oryginalny dokument z rozrachunków
        If UsunDokumentRachunkowy(OldDokument) Then

            'dodaj zmodyfikowany dokument do rozrachunków
            If NowyDokumentRachunkowy(Dokument) Then
                Return True
            End If
            'jak się nie uda to no cóż..
        Else
            Dim CustoMessagebox As New MessageBoxCustom("nie udało się zaktualizować dokumentu")
            CustoMessagebox.Show()
            Return False
        End If

        Return True
    End Function

    'dodanie nowej faktury przychodowej do rozrachunku. update kont i sub kont
    Public Overloads Shared Function NowyDokumentRachunkowy(Dokument As FakturaPrzychodowa) As Boolean
        'get handles of current account status
        Dim kontoCSEG = Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle
        Dim KontoPP = Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski
        Dim KontoPO = Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski
        Dim KontoMB = Startup.VMLocator.VMKontaWspolnicy.KontoMBabka


        'zakutalizuj stan kont rzeczywistych (odejmij kwota nett + VAT)
        If Dokument.Zaplacono Then
            Select Case Dokument.Konto
                Case "Konto PLN"
                    kontoCSEG.KontoPLN += (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case "Konto EUR"
                    kontoCSEG.KontoEUR += (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case "Konto GBP"
                    kontoCSEG.KontoGBP += (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case Else
                    Return False
            End Select
        End If

        'zaktualiz stan subkont wspólnych dla kazdej faktury ( każda faktura przychodowa to większy CIT i VAT)
        kontoCSEG.SubKontoCIT += Dokument.KwotaPLN
        kontoCSEG.SubKontoVAT += Dokument.KwotaPLN * Dokument.StawkaVAT / 100


        'jeżeli faktura jest stricte zyskiem jakiegoś wspólnika to zaktualizuj subkonto  danego wspolnika o resztę z dziesiciny i subkonto CSEG o dziesięcine
        'jeżeli jest przychodem cseg to zaktualizuj tylko subkonto cseg
        Select Case Dokument.CzyjZysk
            Case "CSEG"
                kontoCSEG.SubKontoSpolka += Dokument.KwotaPLN
            Case "PPawlowski"
                kontoCSEG.SubKontoSpolka += Dokument.KwotaPLN * Dziesiecina

                KontoPP.Total += Dokument.KwotaPLN * ResztaZDziesieciny
                KontoPP.Operacja = "Faktura Przychodowa"
                KontoPP.Kwota = Dokument.KwotaPLN
                KontoPP.Opis = "Zapłata Faktury Przychodowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPP)

            Case "POstrowski"
                kontoCSEG.SubKontoSpolka += Dokument.KwotaPLN * Dziesiecina

                KontoPO.Total += Dokument.KwotaPLN * ResztaZDziesieciny
                KontoPO.Operacja = "Faktura Przychodowa"
                KontoPO.Kwota = Dokument.KwotaPLN
                KontoPO.Opis = "Zapłata Faktury Przychodowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPO)

            Case "MBabka"
                kontoCSEG.SubKontoSpolka += Dokument.KwotaPLN * Dziesiecina

                KontoMB.Total += Dokument.KwotaPLN * ResztaZDziesieciny
                KontoMB.Operacja = "Faktura Przychodowa"
                KontoMB.Kwota = Dokument.KwotaPLN
                KontoMB.Opis = "Zapłata Faktury Przychodowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoMB)

            Case Else
                Dim messagebox As New MessageBoxCustom("Coś poszło nie tak przy sumowaniu subkont, sprawdź zgodność")
                Return False
        End Select


        kontoCSEG.Operacja = "Faktura Przychodowa"
        kontoCSEG.Opis = "Dodanie nowej  Faktury Przychodowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
        'dodaj nowy record do tabeli kontoCSEG
        Startup.MainDataBaseModel.AddOperationCSEG(kontoCSEG)

        'update status of handles from updated values
        Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle = kontoCSEG
        Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski = KontoPP
        Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski = KontoPO
        Startup.VMLocator.VMKontaWspolnicy.KontoMBabka = KontoMB


        Return True
    End Function

    'Usuniecie faktury przychodowej z rozrachunku. update kont i sub kont
    Public Overloads Shared Function UsunDokumentRachunkowy(Dokument As FakturaPrzychodowa) As Boolean
        'get handles of current account status
        Dim kontoCSEG = Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle
        Dim KontoPP = Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski
        Dim KontoPO = Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski
        Dim KontoMB = Startup.VMLocator.VMKontaWspolnicy.KontoMBabka


        'zakutalizuj stan kont rzeczywistych (dodaj kwota nett + VAT)
        If Dokument.Zaplacono Then
            Select Case Dokument.Konto
                Case "Konto PLN"
                    kontoCSEG.KontoPLN -= (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case "Konto EUR"
                    kontoCSEG.KontoEUR -= (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case "Konto GBP"
                    kontoCSEG.KontoGBP -= (Dokument.Kwota + (Dokument.KwotaPLN * Dokument.StawkaVAT / 100))
                Case Else
                    Return False
            End Select
        End If

        'zaktualiz stan subkont wspólnych dla kazdej faktury ( każda faktura przychodowa to większy CIT i VAT)
        kontoCSEG.SubKontoCIT -= Dokument.KwotaPLN
        kontoCSEG.SubKontoVAT -= Dokument.KwotaPLN * Dokument.StawkaVAT / 100


        'jeżeli faktura jest stricte zyskiem jakiegoś wspólnika to zaktualizuj subkonto  danego wspolnika o resztę z dziesiciny i subkonto CSEG o dziesięcine
        'jeżeli jest przychodem cseg to zaktualizuj tylko subkonto cseg
        Select Case Dokument.CzyjZysk
            Case "CSEG"
                kontoCSEG.SubKontoSpolka -= Dokument.KwotaPLN
            Case "PPawlowski"
                kontoCSEG.SubKontoSpolka -= Dokument.KwotaPLN * Dziesiecina

                KontoPP.Total -= Dokument.KwotaPLN * ResztaZDziesieciny
                KontoPP.Operacja = " Usunięcie Faktura Przychodowa"
                KontoPP.Kwota = Dokument.KwotaPLN
                KontoPP.Opis = "Usunięcie Faktury Przychodowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPP)

            Case "POstrowski"
                kontoCSEG.SubKontoSpolka += Dokument.KwotaPLN * Dziesiecina

                KontoPO.Total -= Dokument.KwotaPLN * ResztaZDziesieciny
                KontoPO.Operacja = "Usunięcie Faktura Przychodowa"
                KontoPO.Kwota = Dokument.KwotaPLN
                KontoPO.Opis = "Usunięcie Faktury Przychodowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPO)

            Case "MBabka"
                kontoCSEG.SubKontoSpolka += Dokument.KwotaPLN * Dziesiecina

                KontoMB.Total -= Dokument.KwotaPLN * ResztaZDziesieciny
                KontoMB.Operacja = "Usunięcie Faktura Przychodowa"
                KontoMB.Kwota = Dokument.KwotaPLN
                KontoMB.Opis = "Usunięcie Faktury Przychodowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoMB)

            Case Else
                Dim messagebox As New MessageBoxCustom("Coś poszło nie tak przy sumowaniu subkont, sprawdź zgodność")
                Return False
        End Select


        kontoCSEG.Operacja = "Usunięcie Faktura Przychodowa"
        kontoCSEG.Opis = "Usunięcie  Faktury Przychodowej nr" & Dokument.NumerFaktury & "Dnia " & DateTime.Now.ToShortDateString
        'dodaj nowy record do tabeli kontoCSEG
        Startup.MainDataBaseModel.AddOperationCSEG(kontoCSEG)

        Return True
    End Function

    'zaktualizuj stan konta  gdy faktura przychodowa została zmodyfikowana
    Public Overloads Shared Function AktualizujDokumentRachunkowy(Dokument As FakturaPrzychodowa) As Boolean



        'pobierz oryginalną wersję dokumentu z bazy danych do datarow
        Dim SelectQuery = String.Format("ID = '{0}'", Dokument.Id.ToString())
        Dim DataRow = Startup.VMLocator.VMFakturyPrzychodowe.FakturyPrzychodoweTable.Select(SelectQuery).First     'get row with given Id

        'wypełnij obiekt  starego dokumentu wartościami z bazy danych
        Dim OldDokument = New FakturaPrzychodowa()
        DataBaseModel.FillFakturaPrzychodowa(DataRow, OldDokument)

        'usuń oryginalny dokument z rozrachunków
        If UsunDokumentRachunkowy(OldDokument) Then

            'dodaj zmodyfikowany dokument do rozrachunków
            If NowyDokumentRachunkowy(Dokument) Then
                Return True
            End If
            'jak się nie uda to no cóż..
        Else
            Dim CustoMessagebox As New MessageBoxCustom("nie udało się zaktualizować dokumentu")
            CustoMessagebox.Show()
            Return False
        End If

        Return True
    End Function

    'dodanie nowej delegacji do rozrachunku. update kont i sub kont
    Public Overloads Shared Function NowyDokumentRachunkowy(Dokument As Delegacja) As Boolean
        'get handles of current account status
        Dim kontoCSEG = Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle
        Dim KontoPP = Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski
        Dim KontoPO = Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski
        Dim KontoMB = Startup.VMLocator.VMKontaWspolnicy.KontoMBabka


        'zakutalizuj stan kont rzeczywistych (odejmij kwota delegacji)
        If Dokument.Wyplacono Then
            Select Case Dokument.Konto
                Case "Konto PLN"
                    kontoCSEG.KontoPLN -= Dokument.KwotaDelegacji
                Case "Konto EUR"
                    kontoCSEG.KontoEUR -= Dokument.KwotaDelegacji
                Case "Konto GBP"
                    kontoCSEG.KontoGBP -= Dokument.KwotaDelegacji
                Case Else
                    Return False
            End Select

        Else 'delegacja pozostała do wypłacenia więc zaktualizuj sub konta wspolnika
            Select Case Dokument.Delegowany
                Case "PPawlowski"
                    KontoPP.SubDelegacje += Dokument.KwotaDelegacjiPLN
                    KontoPP.Total += Dokument.KwotaDelegacjiPLN
                    KontoPP.Operacja = "Delegacja"
                    KontoPP.Kwota = Dokument.KwotaDelegacjiPLN
                    KontoPP.Opis = "Delegacja nr" & Dokument.NumerDelegacji & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPP)

                Case "POstrowski"
                    KontoPO.SubDelegacje += Dokument.KwotaDelegacjiPLN
                    KontoPO.Total += Dokument.KwotaDelegacjiPLN
                    KontoPO.Operacja = "Delegacja"
                    KontoPO.Kwota = Dokument.KwotaDelegacjiPLN
                    KontoPO.Opis = "Delegacja nr" & Dokument.NumerDelegacji & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPO)

                Case "MBabka"
                    KontoMB.SubDelegacje += Dokument.KwotaDelegacjiPLN
                    KontoMB.Total += Dokument.KwotaDelegacjiPLN
                    KontoMB.Operacja = "Delegacja"
                    KontoMB.Kwota = Dokument.KwotaDelegacjiPLN
                    KontoMB.Opis = "Delegacja nr" & Dokument.NumerDelegacji & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoMB)

                Case Else
                    Dim messagebox As New MessageBoxCustom("Coś poszło nie tak przy sumowaniu subkont, sprawdź zgodność")
                    Return False
            End Select

            'update status of handles from updated values
            Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle = kontoCSEG
            Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski = KontoPP
            Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski = KontoPO
            Startup.VMLocator.VMKontaWspolnicy.KontoMBabka = KontoMB

            Return True

        End If

        'zaktualiz stan subkont wspólnych dla kazdej delegacji ( każda delegacja to mniejszy CIT)
        kontoCSEG.SubKontoCIT -= Dokument.KwotaDelegacjiPLN


        kontoCSEG.Operacja = "Delegacja"
        kontoCSEG.Opis = "Dodanie nowej delegacji  Nr" & Dokument.NumerDelegacji & ", Delegowany: " & Dokument.Delegowany & " z dnia " & DateTime.Now.ToShortDateString
        'dodaj nowy record do tabeli kontoCSEG
        Startup.MainDataBaseModel.AddOperationCSEG(kontoCSEG)
        Return True
    End Function

    'Usunięcie delegacji z rozrachunku. update kont i sub kont
    Public Overloads Shared Function UsunDokumentRachunkowy(Dokument As Delegacja) As Boolean
        'get handles of current account status
        Dim kontoCSEG = Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle
        Dim KontoPP = Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski
        Dim KontoPO = Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski
        Dim KontoMB = Startup.VMLocator.VMKontaWspolnicy.KontoMBabka


        'zakutalizuj stan kont rzeczywistych (odejmij kwota delegacji)
        If Dokument.Wyplacono Then
            Select Case Dokument.Konto
                Case "Konto PLN"
                    kontoCSEG.KontoPLN += Dokument.KwotaDelegacji
                Case "Konto EUR"
                    kontoCSEG.KontoEUR += Dokument.KwotaDelegacji
                Case "Konto GBP"
                    kontoCSEG.KontoGBP += Dokument.KwotaDelegacji
                Case Else
                    Return False
            End Select

        Else 'delegacja pozostała do wypłacenia więc zaktualizuj sub konta wspolnika
            Select Case Dokument.Delegowany
                Case "PPawlowski"
                    KontoPP.SubDelegacje -= Dokument.KwotaDelegacjiPLN
                    KontoPP.Total -= Dokument.KwotaDelegacjiPLN
                    KontoPP.Operacja = "Usuniecie Delegacja"
                    KontoPP.Kwota = Dokument.KwotaDelegacjiPLN
                    KontoPP.Opis = "Usuniecie Delegacja nr" & Dokument.NumerDelegacji & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPP)

                Case "POstrowski"
                    KontoPO.SubDelegacje -= Dokument.KwotaDelegacjiPLN
                    KontoPO.Total -= Dokument.KwotaDelegacjiPLN
                    KontoPO.Operacja = "Usuniecie Delegacja"
                    KontoPO.Kwota = Dokument.KwotaDelegacjiPLN
                    KontoPO.Opis = "Usuniecie Delegacja nr" & Dokument.NumerDelegacji & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPO)

                Case "MBabka"
                    KontoMB.SubDelegacje -= Dokument.KwotaDelegacjiPLN
                    KontoMB.Total -= Dokument.KwotaDelegacjiPLN
                    KontoMB.Operacja = "Usuniecie Delegacja"
                    KontoMB.Kwota = Dokument.KwotaDelegacjiPLN
                    KontoMB.Opis = "Usuniecie Delegacja nr" & Dokument.NumerDelegacji & "Dnia " & DateTime.Now.ToShortDateString
                    'dodaj nowy record do tabeli kontawspolnicy
                    Startup.MainDataBaseModel.AddOperationWspolnicy(KontoMB)

                Case Else
                    Dim messagebox As New MessageBoxCustom("Coś poszło nie tak przy sumowaniu subkont, sprawdź zgodność")
                    Return False
            End Select
        End If

        'zaktualiz stan subkont wspólnych dla kazdej delegacji ( każda delegacja to mniejszy CIT)
        kontoCSEG.SubKontoCIT += Dokument.KwotaDelegacjiPLN


        kontoCSEG.Operacja = "Usuniecie Delegacja"
        kontoCSEG.Opis = "Usuniecie Dodanie nowej delegacji  Nr" & Dokument.NumerDelegacji & ", Delegowany: " & Dokument.Delegowany & " z dnia " & DateTime.Now.ToShortDateString
        'dodaj nowy record do tabeli kontoCSEG
        Startup.MainDataBaseModel.AddOperationCSEG(kontoCSEG)
        Return True
    End Function

    'zaktualizuj stan konta  gdy delegacja została zmodyfikowana
    Public Overloads Shared Function AktualizujDokumentRachunkowy(Dokument As Delegacja) As Boolean



        'pobierz oryginalną wersję dokumentu z bazy danych do datarow
        Dim SelectQuery = String.Format("ID = '{0}'", Dokument.Id.ToString())
        Dim DataRow = Startup.VMLocator.VMDelegacje.DelegacjeTable.Select(SelectQuery).First     'get row with given Id

        'wypełnij obiekt  starego dokumentu wartościami z bazy danych
        Dim OldDokument = New Delegacja()
        DataBaseModel.FillDelegacja(DataRow, OldDokument)

        'usuń oryginalny dokument z rozrachunków
        If UsunDokumentRachunkowy(OldDokument) Then

            'dodaj zmodyfikowany dokument do rozrachunków
            If NowyDokumentRachunkowy(Dokument) Then
                Return True
            End If
            'jak się nie uda to no cóż..
        Else
            Dim CustoMessagebox As New MessageBoxCustom("nie udało się zaktualizować dokumentu")
            CustoMessagebox.Show()
            Return False
        End If

        Return True
    End Function

    'Dodanie nowej umowy o dzieło do rozrachunków. update kont i sub kont
    Public Overloads Shared Function NowyDokumentRachunkowy(Dokument As UmowaDzielo) As Boolean
        'get handles of current account status
        Dim kontoCSEG = Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle
        Dim KontoPP = Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski
        Dim KontoPO = Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski
        Dim KontoMB = Startup.VMLocator.VMKontaWspolnicy.KontoMBabka


        'zakutalizuj stan kont rzeczywistych (odejmij kwota nett )
        If Dokument.Wyplacono Then
            Select Case Dokument.Konto
                Case "Konto PLN"
                    kontoCSEG.KontoPLN -= Dokument.KwotaNetto
                Case "Konto EUR"
                    kontoCSEG.KontoEUR -= Dokument.KwotaNetto
                Case "Konto GBP"
                    kontoCSEG.KontoGBP -= Dokument.KwotaNetto
                Case Else
                    Return False
            End Select
        End If

        'zaktualiz stan subkont wspólnych dla kazdej umowy ( każda umowa dzieło to mniejszy CIT ale większy PIT)
        kontoCSEG.SubKontoCIT -= Dokument.KwotaBrutto
        kontoCSEG.SubKontoPIT += Dokument.KwotaBrutto - Dokument.KwotaNetto


        'zaktalizuj sub konta danego gagatka
        ' jeżeli nie wypłacono a tylko dodano umowę to dodaj jej kwote netto do subkonta umowy
        'jeżeli wypłacono umowę to odejmij od konta total ( to jest zupełnie nowa umowa)
        Select Case Dokument.Osoba

            Case "PPawlowski"
                If Dokument.Wyplacono Then
                    KontoPP.Total -= Dokument.KwotaNetto
                    KontoPP.Operacja = "Umowa o dzieło"
                    KontoPP.Kwota = Dokument.KwotaNetto
                    KontoPP.Opis = "Zapłata Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString

                Else
                    KontoPP.SubUmowy += Dokument.KwotaNetto
                    KontoPP.Operacja = "Umowa o dzieło"
                    KontoPP.Kwota = Dokument.KwotaNetto
                    KontoPP.Opis = "Dodanie Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
                End If
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPP)
            Case "POstrowski"
                If Dokument.Wyplacono Then
                    KontoPO.Total -= Dokument.KwotaNetto
                    KontoPO.Operacja = "Umowa o dzieło"
                    KontoPO.Kwota = Dokument.KwotaNetto
                    KontoPO.Opis = "Zapłata Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString

                Else
                    KontoPO.SubUmowy += Dokument.KwotaNetto
                    KontoPO.Operacja = "Umowa o dzieło"
                    KontoPO.Kwota = Dokument.KwotaNetto
                    KontoPO.Opis = "Dodanie Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
                End If
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPO)

            Case "MBabka"
                If Dokument.Wyplacono Then
                    KontoMB.Total -= Dokument.KwotaNetto
                    KontoMB.Operacja = "Umowa o dzieło"
                    KontoMB.Kwota = Dokument.KwotaNetto
                    KontoMB.Opis = "Zapłata Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
                Else
                    KontoMB.SubUmowy += Dokument.KwotaNetto
                    KontoMB.Operacja = "Umowa o dzieło"
                    KontoMB.Kwota = Dokument.KwotaNetto
                    KontoMB.Opis = "Dodanie Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
                End If
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoMB)

            Case Else
                Dim messagebox As New MessageBoxCustom("Coś poszło nie tak przy sumowaniu subkont, sprawdź zgodność")
                Return False
        End Select


        kontoCSEG.Operacja = "Umowa Dzieło"
        kontoCSEG.Opis = "Dodanie umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
        'dodaj nowy record do tabeli kontoCSEG
        Startup.MainDataBaseModel.AddOperationCSEG(kontoCSEG)

        'update status of handles from updated values
        Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle = kontoCSEG
        Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski = KontoPP
        Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski = KontoPO
        Startup.VMLocator.VMKontaWspolnicy.KontoMBabka = KontoMB

        Return True
    End Function

    'Usuniecie umowy o dzieło z rozrachunków. update kont i sub kont
    Public Overloads Shared Function UsunDokumentRachunkowy(Dokument As UmowaDzielo) As Boolean
        'get handles of current account status
        Dim kontoCSEG = Startup.VMLocator.VMKontaCSEG.KontoCSEGHandle
        Dim KontoPP = Startup.VMLocator.VMKontaWspolnicy.KontoPPawlowski
        Dim KontoPO = Startup.VMLocator.VMKontaWspolnicy.KontoPOstrowski
        Dim KontoMB = Startup.VMLocator.VMKontaWspolnicy.KontoMBabka


        'zakutalizuj stan kont rzeczywistych (odejmij kwota nett )
        If Dokument.Wyplacono Then
            Select Case Dokument.Konto
                Case "Konto PLN"
                    kontoCSEG.KontoPLN += Dokument.KwotaNetto
                Case "Konto EUR"
                    kontoCSEG.KontoEUR += Dokument.KwotaNetto
                Case "Konto GBP"
                    kontoCSEG.KontoGBP += Dokument.KwotaNetto
                Case Else
                    Return False
            End Select
        End If

        'zaktualiz stan subkont wspólnych dla kazdej umowy ( każda umowa dzieło to mniejszy CIT ale większy PIT)
        kontoCSEG.SubKontoCIT += Dokument.KwotaBrutto
        kontoCSEG.SubKontoPIT -= Dokument.KwotaBrutto - Dokument.KwotaNetto


        'zaktalizuj sub konta danego gagatka
        ' jeżeli nie wypłacono a tylko dodano umowę to dodaj jej kwote netto do subkonta umowy
        'jeżeli wypłacono umowę to odejmij od konta total ( to jest zupełnie nowa umowa)
        Select Case Dokument.Osoba

            Case "PPawlowski"
                If Dokument.Wyplacono Then
                    KontoPP.Total += Dokument.KwotaNetto
                    KontoPP.Operacja = "Usuniecie Umowa o dzieło"
                    KontoPP.Kwota = Dokument.KwotaNetto
                    KontoPP.Opis = "Usuniecie Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString

                Else
                    KontoPP.SubUmowy -= Dokument.KwotaNetto
                    KontoPP.Operacja = "Usuniecie Umowa o dzieło"
                    KontoPP.Kwota = Dokument.KwotaNetto
                    KontoPP.Opis = "Usuniecie Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
                End If
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPP)
            Case "POstrowski"
                If Dokument.Wyplacono Then
                    KontoPO.Total += Dokument.KwotaNetto
                    KontoPO.Operacja = "Usuniecie Umowa o dzieło"
                    KontoPO.Kwota = Dokument.KwotaNetto
                    KontoPO.Opis = "Usuniecie Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString

                Else
                    KontoPO.SubUmowy -= Dokument.KwotaNetto
                    KontoPO.Operacja = "Usuniecie Umowa o dzieło"
                    KontoPO.Kwota = Dokument.KwotaNetto
                    KontoPO.Opis = "Usuniecie Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
                End If
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoPO)

            Case "MBabka"
                If Dokument.Wyplacono Then
                    KontoMB.Total += Dokument.KwotaNetto
                    KontoMB.Operacja = "Usuniecie Umowa o dzieło"
                    KontoMB.Kwota = Dokument.KwotaNetto
                    KontoMB.Opis = "Usuniecie Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
                Else
                    KontoMB.SubUmowy -= Dokument.KwotaNetto
                    KontoMB.Operacja = "Usuniecie Umowa o dzieło"
                    KontoMB.Kwota = Dokument.KwotaNetto
                    KontoMB.Opis = "Usuniecie  Umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
                End If
                'dodaj nowy record do tabeli kontawspolnicy
                Startup.MainDataBaseModel.AddOperationWspolnicy(KontoMB)

            Case Else
                Dim messagebox As New MessageBoxCustom("Coś poszło nie tak przy sumowaniu subkont, sprawdź zgodność")
                Return False
        End Select


        kontoCSEG.Operacja = "Umowa Dzieło"
        kontoCSEG.Opis = "Dodanie umowy o dzieło nr" & Dokument.NumerUmowy & "Dnia " & DateTime.Now.ToShortDateString
        'dodaj nowy record do tabeli kontoCSEG
        Startup.MainDataBaseModel.AddOperationCSEG(kontoCSEG)

        Return True
    End Function

    'zaktualizuj stan konta  gdy umowa o dzieło została zmodyfikowana
    Public Overloads Shared Function AktualizujDokumentRachunkowy(Dokument As UmowaDzielo) As Boolean



        'pobierz oryginalną wersję dokumentu z bazy danych do datarow
        Dim SelectQuery = String.Format("ID = '{0}'", Dokument.Id.ToString())
        Dim DataRow = Startup.VMLocator.VMUmowyDzielo.UmowyDzieloTable.Select(SelectQuery).First     'get row with given Id

        'wypełnij obiekt  starego dokumentu wartościami z bazy danych
        Dim OldDokument = New UmowaDzielo()
        DataBaseModel.FillUmowaDzielo(DataRow, OldDokument)

        'usuń oryginalny dokument z rozrachunków
        If UsunDokumentRachunkowy(OldDokument) Then

            'dodaj zmodyfikowany dokument do rozrachunków
            If NowyDokumentRachunkowy(Dokument) Then
                Return True
            End If
            'jak się nie uda to no cóż..
        Else
            Dim CustoMessagebox As New MessageBoxCustom("nie udało się zaktualizować dokumentu")
            CustoMessagebox.Show()
            Return False
        End If

        Return True
    End Function

#End Region


    Public Shared Function SumaKosztowUmowy(_NumerUmowy As String) As Decimal

        Dim _Sum As Decimal = 0
        'copy all faktury kosztowe with matchin 'numer umowy to temporary table'
        Dim SumTempTable = New DataView(Startup.VMLocator.VMFakturyKosztowe.FakturyKosztoweTable) With {
                .RowFilter = String.Format("NumerUmowy Like '{0}'", _NumerUmowy)
            }
        'select list of values from temporay table
        Dim ValuetoReturn1 = (From Rows In SumTempTable
                              Select Rows("KwotaPLN")).Distinct().ToList()

        For Each val1 In ValuetoReturn1
            _Sum = _Sum + val1
        Next

        Return Math.Round(_Sum, 2)
    End Function


    Public Shared Function SumaPrzychUmowy(_NumerUmowy As String) As Decimal

        Dim _Sum As Decimal = 0
        'copy all faktury przychodowe with matchin 'numer umowy to temporary table'
        Dim SumTempTable = New DataView(Startup.VMLocator.VMFakturyPrzychodowe.FakturyPrzychodoweTable) With {
                .RowFilter = String.Format("NumerUmowy LIKE '{0}'", _NumerUmowy)
            }
        'select list of values from temporay table
        Dim ValuetoReturn1 = (From Rows In SumTempTable
                              Select Rows("KwotaPLN")).Distinct().ToList()

        For Each val1 In ValuetoReturn1
            _Sum = _Sum + val1
        Next

        Return Math.Round(_Sum, 2)
    End Function

    Public Shared Function SumaDelegacje(_NumerUmowy As String) As Decimal

        Dim _Sum As Decimal = 0
        'copy all faktury przychodowe with matchin 'numer umowy to temporary table'
        Dim SumTempTable = New DataView(Startup.VMLocator.VMDelegacje.DelegacjeTable) With {
                .RowFilter = String.Format("NumerUmowy LIKE '{0}'", _NumerUmowy)
            }
        'select list of values from temporay table
        Dim ValuetoReturn1 = (From Rows In SumTempTable
                              Select Rows("KwotaDelegacjiPLN")).Distinct().ToList()

        For Each val1 In ValuetoReturn1
            _Sum = _Sum + val1
        Next

        Return Math.Round(_Sum, 2)
    End Function

    Public Shared Function DelegowanyFullName(_Delegowane As String) As String
        Select Case _Delegowane
            Case "PPawlowski"
                Return "Piotr Pawłowski"
            Case "POstrowski"
                Return "Piotr Ostrowski"
            Case "MBabka"
                Return "Mariusz Bąbka"
            Case Else
                Return "Brak osobnika"
        End Select
    End Function

    Public Shared Function MiesiacNumer(_Miesiac As String) As Integer
        Select Case _Miesiac
            Case "Styczeń"
                Return 1
            Case "Luty"
                Return 2
            Case "Marzec"
                Return 3
            Case "Kwiecień"
                Return 4
            Case "Maj"
                Return 5
            Case "Czerwiec"
                Return 6
            Case "Lipiec"
                Return 7
            Case "Sierpień"
                Return 8
            Case "Wrzesień"
                Return 9
            Case "Październik"
                Return 10
            Case "Listopad"
                Return 11
            Case "Grudzień"
                Return 12
            Case Else
                Return "0"
        End Select
    End Function
    'remove all characters that are not number
    Public Shared Function RemoveNotNumber(InputString As String) As String
        Dim ResulString As String = InputString
        Dim ForbidenString As String = "!@#$%^&*()_+-=qwertyuiop[]asdfghjkl;'zxcvbnm,./<>?:{}|"
        For Each n In ForbidenString
            ResulString = ResulString.Replace(n, "")
        Next
        Return ResulString
    End Function

    'replace all of given characters with some char
    Public Shared Function ReplaceCharacters(InputString As String, ToReplace As String, Replacement As Char) As String
        Dim ResulString As String = InputString

        For Each n In ToReplace
            ResulString = ResulString.Replace(n, Replacement)
        Next
        Return ResulString
    End Function
#End Region
End Class
