   M S B A M L   `   `   `  ���� F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  *    $Rozliczator.DodajFaktureKosztowaView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Rozliczator     V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     � Title$  DodajFaktureKosztowaView��$	��500��$	��600�� � SizeToContent$ WidthAndHeight=�   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � Icon$! /Images/cseg_icon_32_32.ico��.���  � ColumnDefinitions �� $
��374* ��� $	��23* �.+���  �� Orientation$ 
Horizontal=�.+��� $ Vertical=�.+��� $ 
Horizontal=�$��60��.+��� -   $��Bottom=�$��	20,0,10,0q�.��Sprzedawca:�� -   $��SprzedawcaComboBox��$	��150�� �� 
IsEditable . $��Bottom=� �� Text �� *
Sprzedawca+�� $��Bottom=�$��	20,0,10,0q�.��Numer Faktury:�� -   $��NumerFaktury��	 �� TextAlignment$	 Center=�$	��150��$��0,0,15,0q����� *NumerFaktury+
 �� Mode$
 TwoWay=��� $��Bottom=�$��10,0,0,0q� � ToolTip$  Data z Faktury oczywiście��.��Data(  "System.Windows.Controls.DatePicker  -   $��dataFaktury��$	��100��$��Bottom=�   SelectedDate �� *DataWystawienia+�� -   $��StackPanelKontr��$ 
Horizontal=�$��60��y��� *KontrahentVisibility+.+��� $��Bottom=�$��	20,0,10,0q�.��Kontrahent:�� -   $��KontrahentComboBox��$	��150�� . $��Bottom=� �� SelectedValue �� *
Kontrahent+���� *KontrahenciList+�� $��Bottom=�$��	20,0,10,0q�.��
Ilość:�� -   $��IloscKontrahent��$	 Center=�$��80��$��0q����� *KontrahentIlosc+$
 TwoWay=��� $��Bottom=�$��	10,0,10,0q�.��Stawka�� -	   $��Stawka��$	 Center=�$��50��$��0,0,5,0q����� *KontrahentStawka+$
 TwoWay=� �� StringFormat$ {0:00}���� -
   $��WalutaKontrahent��$��Bottom=�$��50������ *KontrahentWaluta+$
 OneWay=��� -   $	��100��$��30,0,0,0q� J� RenderTransformOrigin$ 	0.502,0.5&�.��
Przelicz�� $ 
Horizontal=�$��60��.+��� $��Bottom=�$��	20,0,40,0q�.��Rodzaj kosztu/opis:�� -   $��OpisComboBox��$	��480�� . $��Bottom=� �� *Opis+�� $ 
Horizontal=�$��60��.+��� $��Bottom=�$��20,0,0,0q�.��Kwota:�� -   $��Kwota��$	 Center=�$	��100��$��0,0,15,0q����� *Kwota+$
 TwoWay=�$ {0:0.00}���� -   $��listaWalutComboBox��$��Bottom=�$��50�� �� SelectedItem �� *Waluta+$
 TwoWay=��� $��Bottom=�$��20,0,0,0q�.��VAT:�� -   $��listaVatCombobox��$��Bottom=�$��Center=� �� ItemStringFormat$ {0:0}% ��$��50�� �� *	StawkaVAT+$
 TwoWay=��� $��Bottom=�$��20,0,0,0q�y��� *	WalutaPLN+.��Kurs z dnia:�� -   $��	KursZDnia��$	 Center=�$	��100������ *	KursZDnia+$ 
{0:0.0000}��y��� *	WalutaPLN+�� $��Bottom=�$��20,0,0,0q�y��� *	WalutaPLN+.��
Kwota PLN:�� -   $��KwotaPLN��$	 Center=�$	��100������ *
KwotaPLN+$ {0:0.00}��y��� *	WalutaPLN+�� $ 
Horizontal=�$��60��.+��� $��Bottom=�$��20,0,0,0q�$8 2Czy zakup został dokonany z prywatnych środków?��.��
Do zwrotu?�� -   $��DoZwrotu��$��Bottom=�$��	10,0,10,5q� d� 	IsChecked �� *
DoZwrotu+$
 TwoWay=��� $��Bottom=�$) #Dla kogo zakwalifikować ten koszt?��.��Czyj koszt:�� -   $��	czyjKoszt��$��Bottom=�$	��100��$��	20,0,20,0q� �� *	CzyjKoszt+�� $��Bottom=�$? 9Wybierz do której umowy o dzeło przypisać tą fakturę��y��� *	OsobaCSEG+.��Wybierz UD :�� -   $��	listaUmow��$��Bottom=� .  $	��150�� �� *
NumerUmowy+���� *UmowyListOfOsoba+y��� *	OsobaCSEG+�� $ 
Horizontal=�$��60��.+��� $��Bottom=�$" Czy Faktura zostala zalacona��$��Zapłacono?��$��20,0,0,0q��� -    ��Name$ 	Zaplacono��$��Bottom=�$��10q� �� *	Zaplacono+$
 TwoWay=��� -    ��Name$ KontoComboBox��$	��120�� .  $1 +Wybierz konto bankowe z którego wypłacono��$��Bottom=� �� *Konto+{��� *	Zaplacono+$
 OneWay=��� $ 
Horizontal=�$��Right=�$��80��$��0,30,29.6,30q�$	��328��.+��� -   $	��150��$��
100,0,80,0q�.��Zapisz9�  9� ExpandDirection$ Right=� 9� 
IsExpanded .  ���� $��Pliki�� � LayoutTransform ��  �� Angle$	 -90Q�.���� .+��� .��Plik 1�� -   $��
Plik1Image�� J� 	AllowDrop . $	��100��$	��100������ *	Plik1Icon+ � ContextMenu �� .@�m� -   $��Dodaj��$��12(�m� -   $��Usuń��$��12(��� m� -   $��Pobierz��$��12(��� -   $��	Plik1Text��$	��100��$
��Wrap=�$��	0,10,0,20q����� *	Plik1Name+�� .��Plik 2�� -   $��
Plik2Image�� . $	��100��$	��100������ *	Plik2Icon+ �� .@�m� -   $��Dodaj��$��12(�m� -   $��Usuń��$��12(��� m� -    $��Pobierz��$��12(��� -!   $��	Plik2Text��$	��100��$
��Wrap=�$��	0,10,0,20q����� *	Plik2Name+