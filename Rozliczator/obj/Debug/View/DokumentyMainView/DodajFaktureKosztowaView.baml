   M S B A M L   `   `   `  ����F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  5   	   *    $Rozliczator.DodajFaktureKosztowaView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Rozliczator     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   -   5          � Title$  DodajFaktureKosztowaView��5   	   $	��500��6*   $	��600��67    � SizeToContent$ WidthAndHeight=�5	   	      MaterialDesignBody#���   5      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5       � Icon$! /Images/cseg_icon_32_32.ico��5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��374* �6   �� 5      $	��23* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $ Vertical=�6   .+�5      �� $ 
Horizontal=�6   $��60��66   .+�5      �� -   5        $��Bottom=�5      $��	20,0,10,0q�67   .��6}   Sprzedawca:6�   �� 5      -   5        $��SprzedawcaComboBox��5   >   $	��150��6    �� 
IsEditable . $��Bottom=�6t    �� Text 6X   �� *
Sprzedawca+6t   �� 5      $��Bottom=�6   $��	20,0,10,0q�67   .��6J   Numer Faktury:6Z   �� 5       -   5        $��NumerFaktury��5    5   	 �� TextAlignment$	 Center=�6   $	��150��6I   $��0,0,15,0q�6�   ��6X   �� *NumerFaktury+
 �� Mode$
 TwoWay=�6�   �� 5"      $��Bottom=�6   $��10,0,0,0q�67    � ToolTip$  Data z Faktury oczywiście��6I   .��6m   Data6s   (  "System.Windows.Controls.DatePicker  5#      -   5        $��dataFaktury��5#   !   $	��100��64   $��Bottom=�6A      SelectedDate 6\   �� *DataWystawienia+5%      �� 5&      -   5        $��StackPanelKontr��5&   B   $ 
Horizontal=�6   $��60��66   y�6Z   �� *KontrahentVisibility+.+�5'      �� $��Bottom=�6   $��	20,0,10,0q�67   .��6J   Kontrahent:6W   �� 5(      -   5        $��KontrahentComboBox��5(   >   $	��150��6    . $��Bottom=�6�    �� SelectedValue 6Y   �� *
Kontrahent+��6~   �� *KontrahenciList+6�   �� 5*      $��Bottom=�6   $��	20,0,10,0q�67   .��6J   
Ilość:6R   �� 5+      -   5        $��IloscKontrahent��5+   5   $	 Center=�6   $��80��6L   $��0q�6�   ��6Z   �� *KontrahentIlosc+$
 TwoWay=�6�   �� 5,      $��Bottom=�6   $��	10,0,10,0q�67   .��6J   Stawka6R   �� 5-      -	   5        $��Stawka��5-   5   $	 Center=�6   $��50��6C   $��0,0,5,0q�6�   ��6Q   �� *KontrahentStawka+$
 TwoWay=� �� StringFormat$ {0:00}��6�   �� 5.      -
   5        $��WalutaKontrahent��5.   9   $��Bottom=�6   $��50��6�   ��6R   �� *KontrahentWaluta+$
 OneWay=�6�   �� 50      -   5        $	��100��50      $��30,0,0,0q�6*    J� RenderTransformOrigin$ 	0.502,0.5&�6Y   .��6{   
Przelicz6�   51      �� 52      $ 
Horizontal=�6   $��60��66   .+�53      �� $��Bottom=�6   $��	20,0,40,0q�67   .��6J   Rodzaj kosztu/opis:6_   �� 54      -   5        $��OpisComboBox��54   A   $	��480��6    . $��Bottom=�6m    6V   �� *Opis+6m   57      �� 59      $ 
Horizontal=�6   $��60��66   .+�5<      �� $��Bottom=�6   $��20,0,0,0q�67   .��6I   Kwota:6Q   �� 5=      -   5        $��Kwota��5=   5   $	 Center=�6   $	��100��6B   $��0,0,15,0q�6�   ��6N   �� *Kwota+$
 TwoWay=�$ {0:0.00}��6�   �� 5>      -   5        $��listaWalutComboBox��5>   :   $��Bottom=�6   $��50��6�    �� SelectedItem 6U   �� *Waluta+$
 TwoWay=�6�   �� 5?      $��Bottom=�6   $��20,0,0,0q�67   .��6I   VAT:6O   �� 5@      -   5        $��listaVatCombobox��5@   W   $��Bottom=�6   $��Center=�6:    �� ItemStringFormat$ {0:0}% ��6p   $��50��6�    6�   �� *	StawkaVAT+$
 TwoWay=�6�   �� 5A      $��Bottom=�6   $��20,0,0,0q�6X   y�67   �� *	WalutaPLN+.��6j   Kurs z dnia:6x   �� 5B      -   5        $��	KursZDnia��5B   5   $	 Center=�6   $	��100��6�   ��6F   �� *	KursZDnia+$ 
{0:0.0000}��y�6}   �� *	WalutaPLN+6�   �� 5E      $��Bottom=�6   $��20,0,0,0q�6X   y�67   �� *	WalutaPLN+.��6j   
Kwota PLN:6v   �� 5F      -   5        $��KwotaPLN��5F   5   $	 Center=�6   $	��100��6�   ��6E   �� *
KwotaPLN+$ {0:0.00}��y�6x   �� *	WalutaPLN+6�   5J      �� 5K      $ 
Horizontal=�6   $��60��66   .+�5L      �� $��Bottom=�6   $��20,0,0,0q�67   $8 2Czy zakup został dokonany z prywatnych środków?��6I   .��6�   
Do zwrotu?6�   �� 5M      -   5        $��DoZwrotu��5M   :   $��Bottom=�6   $��	10,0,10,5q�6v    d� 	IsChecked 6J   �� *
DoZwrotu+$
 TwoWay=�6v   �� 5O      $��Bottom=�6   $) #Dla kogo zakwalifikować ten koszt?��67   .��6d   Czyj koszt:6q   �� 5P      -   5        $��	czyjKoszt��5P   :   $��Bottom=�6   $	��100��6K   $��	20,0,20,0q�6{    6W   �� *	CzyjKoszt+6{   �� 5R      $��Bottom=�6   $? 9Wybierz do której umowy o dzeło przypisać tą fakturę��6X   y�67   �� *	OsobaCSEG+.��6�   Wybierz UD :6�   �� 5S      -   5        $��	listaUmow��5S   :   $��Bottom=�6    .  $	��150��6�    6^   �� *
NumerUmowy+��6�   �� *UmowyListOfOsoba+y�6�   �� *	OsobaCSEG+6�   5V      �� 5W      $ 
Horizontal=�6   $��60��66   .+�5X      �� $��Bottom=�6   $" Czy Faktura zostala zalacona��67   $��Zapłacono?��6^   $��20,0,0,0q�6t   �� 5Y      -   5         ��Name$ 	Zaplacono��5Y   :   $��Bottom=�6   $��10q�6z    6M   �� *	Zaplacono+$
 TwoWay=�6z   �� 5Z      -   5         ��Name$ KontoComboBox��5Z   ?   $	��120��6    .  $1 +Wybierz konto bankowe z którego wypłacono��6V   $��Bottom=�6�    6�   �� *Konto+{�6�   �� *	Zaplacono+$
 OneWay=�5[      �� 5\      $ 
Horizontal=�6   $��Right=�66   $��80��6R   $��0,30,29.6,30q�6^   $	��328��6t   .+�5^      �� -   5        $	��150��5^      $��
100,0,80,0q�6*   .��6T   Zapisz6\   5_      5a      9� 5b       9� ExpandDirection$ Right=�6    9� 
IsExpanded .  ��5c      �� 5d      $��Pliki��6     � LayoutTransform 5e      �� 5f       �� Angle$	 -90Q�6.   5h      .��5j      �� .+�5k      �� .��6   Plik 16$   �� 5l      -   5        $��
Plik1Image��5l       J� 	AllowDrop . $	��100��6n   $	��100��6{   ��6.   �� *	Plik1Icon+ � ContextMenu 5m      �� 5n      .@�5o   "   m� -   5        $��Dodaj��5o   +   $��12(�6V   m� 5p   "   -   5        $��Usuń��5p   +   $��12(�6S   �� 5q   "   m� 5r   "   -   5        $��Pobierz��5r   +   $��12(�6Y   5s      5u      �� 5v      -   5        $��	Plik1Text��5v       $	��100��6M   $
��Wrap=�6]   $��	0,10,0,20q�6q   ��61   �� *	Plik1Name+6q   �� 5x      .��6   Plik 26$   �� 5y      -   5        $��
Plik2Image��5y       . $	��100��6n   $	��100��6{   ��6.   �� *	Plik2Icon+ 5z      �� 5{      .@�5|   "   m� -   5        $��Dodaj��5|   +   $��12(�6V   m� 5}   "   -   5        $��Usuń��5}   +   $��12(�6S   �� 5~   "   m� 5   "   -    5        $��Pobierz��5   +   $��12(�6Y   5�      5�      �� 5�      -!   5        $��	Plik2Text��5�       $	��100��6M   $
��Wrap=�6]   $��	0,10,0,20q�6q   ��61   �� *	Plik2Name+6q   5�      5�      5�      5�      5�      