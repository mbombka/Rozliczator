   M S B A M L   `   `   `  ���� F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  -    'Rozliczator.DodajFakturePrzychodowaView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Rozliczator     V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$!  DodajFakturePrzychodowaView��$	��450��$	��700�� � SizeToContent$ WidthAndHeight=�   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � Icon$! /Images/cseg_icon_32_32.ico��.��� .+���  �� Orientation$ 
Horizontal=�.+��� $ Vertical=�.+��� $ 
Horizontal=�$��60��.+��� $��Bottom=�$��	20,0,10,0q�.��	Klient:�� -   $��KlientComboBox��$	��150�� �� 
IsEditable . $��Bottom=� �� Text �� *Klient+�� $��Bottom=�$��	20,0,10,0q�.��Numer Faktury:�� -   $��NumerFaktury�� �� TextAlignment$ Center=�$	��150��$��0,0,15,0q����� *NumerFaktury+	 �� Mode$	 TwoWay=��� $��Bottom=�$��30,0,0,0q�
 � ToolTip$ 
 Data z Faktury oczywiście��.��Data(  "System.Windows.Controls.DatePicker  -   $��dataFaktury��$	��100��$��Bottom=�   SelectedDate �� *DataWystawienia+�� $ 
Horizontal=�$��60��.+��� $��Bottom=�$��	20,0,40,0q�.��opis:�� -   $��OpisComboBox��$	��480�� . $��Bottom=� �� *Opis+�� $ 
Horizontal=�$��60��.+��� $��Bottom=�$��20,0,0,0q�.��Kwota:�� -   $��Kwota��$ Center=�$	��100��$��0,0,15,0q����� *Kwota+$	 TwoWay=� �� StringFormat$ {0:0.00}���� -   $��listaWalutComboBox��$��Bottom=�$��50�� �� SelectedItem �� *Waluta+$	 TwoWay=��� $��Bottom=�$��20,0,0,0q�.��VAT:�� -   $��listaVatCombobox��$��Bottom=� �� ItemStringFormat$ {0:0}% ��$��50�� �� *	StawkaVAT+$	 TwoWay=��� $��Bottom=�$��20,0,0,0q�y��� *	WalutaPLN+.��Kurs z dnia:�� -   $��	KursZDnia��$ Center=�$	��100������ *	KursZDnia+y��� *	WalutaPLN+�� $��Bottom=�$��20,0,0,0q�y��� *	WalutaPLN+.��
Kwota PLN:�� -	   $��KwotaPLN��$ Center=�$	��100������ *
KwotaPLN+$ {0:0.00}��y��� *	WalutaPLN+�� $ 
Horizontal=�$��60��.+��� $��Bottom=�$(
 "Dla kogo zakwalifikować ten zesk?��$��	20,0,20,0q�.��
Czyj zysk:�� -
   $��czyjZyskComboBox��$��Bottom=�$	��100��$��	20,0,20,0q� �� SelectedValue �� *
CzyjZysk+�� $��Bottom=�$?
 9Wybierz do której umowy o dzeło przypisać tą fakturę��y��� *	OsobaCSEG+.��Wybierz UD :�� -   $��	listaUmow��$��Bottom=� .  $	��150�� �� *
NumerUmowy+���� *UmowyListOfOsoba+y��� *	OsobaCSEG+�� $ 
Horizontal=�$��60��.+��� $��Bottom=�$"
 Czy Faktura zostala zalacona��$��Zapłacono?��$��20,0,0,0q��� -    ��Name$ 	Zaplacono��$��Bottom=�$��10q� d� 	IsChecked �� *	Zaplacono+$	 TwoWay=��� -    ��Name$ KontoComboBox��$	��120�� .  $1
 +Wybierz konto bankowe z którego wypłacono��$��Bottom=� �� *Konto+{��� *	Zaplacono+$	 OneWay=��� $ 
Horizontal=�$��Right=�$��80��$��30q�.+��� -   $	��150��$��
100,0,80,0q�.��Zapisz9�  9� ExpandDirection$ Right=� 9� 
IsExpanded .  ���� $��Pliki�� � LayoutTransform ��  �� Angle$	 -90Q�.���� .+��� .��Plik 1�� -   $��
Plik1Image�� J� 	AllowDrop . $	��100��$	��100������ *	Plik1Icon+ � ContextMenu �� .@�m� -   $��Dodaj��$��12(�m� -   $��Usuń��$��12(��� m� -   $��Pobierz��$��12(��� -   $��	Plik1Text��$	��100��$
��Wrap=�$��	0,10,0,20q����� *	Plik1Name+�� .��Plik 2�� -   $��
Plik2Image�� . $	��100��$	��100������ *	Plik2Icon+ �� .@�m� -   $��Dodaj��$��12(�m� -   $��Usuń��$��12(��� m� -   $��Pobierz��$��12(��� -   $��	Plik2Text��$	��100��$
��Wrap=�$��	0,10,0,20q����� *	Plik2Name+