   M S B A M L   `   `   `  ���� F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  &     Rozliczator.DodajUmowaDzieloView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Rozliczator     V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  Umowa o Dzieło / Faktura�� � SizeToContent$ WidthAndHeight=� � WindowStartupLocation$ Manual=�   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � Icon$! /Images/cseg_icon_32_32.ico��.��� .+���  �� Orientation$ 
Horizontal=�.+��� $ Vertical=�.+��� $ 
Horizontal=�$��60��.+��� $��Bottom=�$��	20,0,10,0q�.��Osoba:�� -   $��OsobaComboBox��$	��100�� �� 
IsEditable .  $��Bottom=� �� Text �� *Osoba+�� $��Bottom=�$��	20,0,10,0q�.��Numer Umowy:�� -   $��
NumerUmowy��	 �� TextAlignment$	 Center=�$	��150��$��0,0,10,0q����� *
NumerUmowy+
 �� Mode$
 TwoWay=��� $��Bottom=�$��10,0,0,0q� � ToolTip$ Data Od��$��Od��(  "System.Windows.Controls.DatePicker  -     Name$ DataPoczatekDP��$��90��$��Bottom=�   SelectedDate �� *DataPoczatek+ �� StringFormat$ 
dd/MM/yyyy���� $��Bottom=�$��20,0,0,0q�$ Data Od��$��Do��  -   $ DataKoniecDP��$��90��$��Bottom=� �� *
DataKoniec+$ 
dd/MM/yyyy���� $ 
Horizontal=�$��60��.+��� $��Bottom=�$��	20,0,10,0q�.��Koszty uz. przych. :�� -   $��KosztyComboBox��$��50�� .  $= 7Koszty uzyskania przychodu 20% lub 50%(prawa autorskie)�� �� ItemStringFormat$ {0:0}% ��$��Bottom=� �� *KosztyUzyskPrzych+$
 TwoWay=��� $��Bottom=�$��	20,0,10,0q�.��Próg Podatkowy :�� -   $��ProgPodatkowyComboBox��$��50�� .  $! I próg: 18%, II próg: 32%��$ {0:0}% ��$��Bottom=� �� *ProgPodatkowy+$
 TwoWay=��� $��Bottom=�$! Czy dieta zostala wyplacona��$��Wypłacono?��$��10,0,0,0q��� -    ��Name$ 	Wyplacono��$��Bottom=�$��10q� d� 	IsChecked �� *	Wyplacono+$
 TwoWay=��� -    ��Name$ KontoComboBox��$	��120�� .  $1 +Wybierz konto bankowe z którego wypłacono��$��Bottom=� �� *Konto+{��� *	Wyplacono+$
 OneWay=��� $��20��9�  9� ExpandDirection$
 Down=� 9� 
IsExpanded .  ���� $ 
Horizontal=�.+��� $��0,0,0,0q�.��#!Faktury przychodowe > suma netto:�� $��20,0,0,5q� �� ContentStringFormat$ {0:0.00}PLN ��{�.  $	��100������ *SumaPrzychodow+�� $��20,0,0,0q�.��dziesięcina:�� $��20,0,0,5q�$ {0:0.00}PLN ��{�.  $	��100������ *Dziesiecina+.���� -	   $��StackPanelPrzychodowe��$ 
Horizontal=�$	��120��.+��� $
��Auto=�$��
4,0,0,-0.4q�$	��550��.��&   System.Windows.Controls.DataGrid  -
   $��DataGridPrzychodowe��   AutoGenerateColumns .     SelectionUnit$ FullRow=�   CanUserAddRows .     CanUserDeleteRows .  ��. $
��Auto=�$
��Auto=�����  �� RelativeSource �� *FindAncestor+ �� AncestorType   Rozliczator.MainWindow"   �� Path$ ActualHeight�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   
Visibility$ 	Collapsed=�1  +System.Windows.Controls.DataGridBoundColumn    Binding  �� *Id+  $ Klient��!   
IsReadOnly! .   �� *Klient+  $ Numer Faktury��! .   �� *NumerFaktury+  $ Data Wystawienia ��! .   �� *DataWystawienia+$ {0:dd.MM.yyyy}��  $
 Opis��! .   �� *Opis+  $ Kwota��! .   �� *Kwota+$ F2��  $ Waluta��! .   �� *Waluta+4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Zapł��! .   �� *	Zaplacono+" � ContextMenu" �� .@�m� -   $��Edytuj��$��12(�m� -   $��Dodaj��$��12(��� m� -   $��Usuń��$��12(��� $��20��9� $
 Down=� .  ���� $ 
Horizontal=�.+��� $��0,0,0,0q�.�� Faktury kosztowe > suma netto:�� $��20,0,0,5q�$ {0:0.00}PLN ��{�.  $	��100������ *SumaWydatkow+.���� -   $��StackPanelKosztowe��$ 
Horizontal=�$	��120��.+��� $
��Auto=�$��
4,0,0,-0.4q�$	��600��.��  -   $��DataGridKosztowe�� .  $ FullRow=� .   .  ��. $
��Auto=�$
��Auto=�����  �� *FindAncestor+"  $ ActualHeight�   $ Id��$ 	Collapsed=�  �� *Id+  $ 
Sprzedawca��! .   �� *
Sprzedawca+  $ Numer Faktury��! .   �� *NumerFaktury+  $ Data Sprzedazy ��! .   �� *DataWystawienia+$ {0:dd.MM.yyyy}��  $
 Opis��! .   �� *Opis+  $ Kwota��! .   �� *Kwota+$ F2��  $ Waluta��! .   �� *Waluta+  $ Zapł��! .   �� *	Zaplacono+" �� .@�m� -   $��Edytuj��$��12(�m� -   $��Dodaj��$��12(��� m� -   $��Usuń��$��12(��� $��20��9� $
 Down=� .  ���� $ 
Horizontal=�.+��� $��0,0,0,0q�.��Delegacje > suma diet:�� $��0,0,0,5q�$ {0:0.00}PLN ��{�.  $	��100������ *
SumaDiet+.���� -   $��StackPanelDelegacje��$ 
Horizontal=�$	��120��.+��� $
��Auto=�$��
4,0,0,-0.4q�$	��650��.��  -   $��DataGridDelegacje�� .  $ FullRow=� .   .  ��. $
��Auto=�$
��Auto=�����  �� *FindAncestor+"  $ ActualHeight�   $ Id��$ 	Collapsed=�  �� *Id+  $ Numer Delegacji��! .   �� *NumerDelegacji+  $ Data Wyjazdu��! .   �� *DataWyjazdu+$ {0:dd.MM.yyyy}��  $ Data Powrotu��! .   �� *DataPowrotu+$ {0:dd.MM.yyyy}��  $ Miesjce Wyjazdu��! .   �� *MiejsceWyjazdu+  $ Kwota Delegacji��! .   �� *KwotaDelegacji+  $ Wysł.��#   Width5	  /System.Windows.Controls.DataGridLengthConverter$# 60	 ! .   �� *	Wyslano+  $ Wypł.��$# 60	 ! .   �� *	Wyplacono+" �� .@�m� -   $��Edytuj��$��12(�m� -   $��Dodaj��$��12(��� m� -   $��Usuń��$��12(��� $��10���� $ 
Horizontal=�$��Right=�$��80��$��10q�.+��� $ Vertical=�$	��400��.+��� $ 
Horizontal=�$��0,0,0,15q�.+��� $��Center=�$��0,0,0,0q�.��Sugerowana Kwota Netto:�� $��Center=�$ {0:0.00}PLN ��$��90������ *KwotaNettoSugerowana+�� $��Center=�$��0,0,0,0q�y��� *	WalutaPLN+.��	Brutto:�� $��Center=�$ {0:0.00}PLN ��$��90������ *KwotaBruttoSugerowana+�� $ 
Horizontal=�.+��� $��Center=�$��0,0,0,0q�.��Kwota Netto:�� $��Center=�$	��100������ *
KwotaNetto+$ {0:0.00}PLN���� $��Center=�$��10,0,0,0q�y��� *	WalutaPLN+.��Kwota Brutto:�� $��Center=�$	��100������ *KwotaBrutto+$ {0:0.00}PLN��.��" �� .@�m� -   $��Przelicz Brutto��$��12(�.���� -   $	��100��$��	10,0,20,0q�.��
Przelicz�� -   $	��150��$��	20,0,20,0q�.��Zapisz9� $ Right=� .  ���� $��Pliki��$ � LayoutTransform$ �� % �� Angle$	% -90Q�.���� .+��� .��Plik 1�� -   $��
Plik1Image��& J� 	AllowDrop& . $	��100��$	��100������ *	Plik1Icon+" �� .@�m� -   $��Dodaj��$��12(�m� -   $��Usuń��$��12(��� m� -   $��Pobierz��$��12(��� -   $��	Plik1Text��$	��100��$
��Wrap=�$��	0,10,0,20q����� *	Plik1Name+�� .��Plik 2�� -    $��
Plik2Image��& . $	��100��$	��100������ *	Plik2Icon+" �� .@�m� -!   $��Dodaj��$��12(�m� -"   $��Usuń��$��12(��� m� -#   $��Pobierz��$��12(��� -$   $��	Plik2Text��$	��100��$
��Wrap=�$��	0,10,0,20q����� *	Plik2Name+