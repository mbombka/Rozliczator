   M S B A M L   `   `   `  ���� F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  "    Rozliczator.UmowyODzieloView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:Rozliczator     NameUmowyOdzielo        MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  .��� $	��Top=� � ColumnDefinitions �� $	��140 ��� $��* � � RowDefinitions �� $��* �.+��� $��0��$��0�� �� Orientation$ Vertical=�$	��450��$	��140��$	��Top=�.+��� -   .��Dodaj Umowę�� -   .��Wczytaj z DB�� $��0,30,0,0q�.��	Filtruj�� �� .��
Data od:(  "System.Windows.Controls.DatePicker  -   $��
DpFltrujOd��   Text$  ��   SelectedDate �� *FilterDataOd+�� .��
Data do:  -   $��
DpFltrujDo��$  �� �� *FilterDataDo+�� .��Zleceniobiorca:�� -   $��OsobaComboBox�� �� SelectedItem �� *FilterZleceniobiorca+�� .��
Wyplacono:�� -   $��ZaplaconoComboBox�� �� *FilterZaplacono+�� -   $��0,30,0,0q�.��Resetuj Filtry&   System.Windows.Controls.DataGrid  -   $��DataGridUmowy��$��1��$��0��   AutoGenerateColumns .  	   SelectionUnit$	 FullRow=���. $
��Auto=�$
��Auto=�
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   
Visibility$ Visible=�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ Numer Umowy��   
IsReadOnly .  �� *
NumerUmowy+  $ Czyja Umowa�� .  �� *Osoba+  $ Data Od�� .  �� *DataPoczatek+ �� StringFormat$ {0:dd.MM.yyyy}��  $ Data Do�� .  �� *
DataKoniec+$ {0:dd.MM.yyyy}��  $ 	Przychody��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 60  .  �� *SumaPrzychodow+$ {0:0.00}��  $ Wydatki�� .  �� *SumaWydatkow+$ {0:0.00}��  $ Diety�� .  �� *
SumaDiet+$ {0:0.00}��  $ Kwota Netto�� .  �� *
KwotaNetto+$ {0:0.00}��4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Wypł.�� .  �� *	Wyplacono+ � ContextMenu �� .@�m� -	   $��Edytuj��$��12(�m� -
   $��Dodaj��$��12(��� m� -   $��Usuń��$��12(�