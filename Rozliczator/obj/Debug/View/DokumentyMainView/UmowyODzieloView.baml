   M S B A M L   `   `   `  ����F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  5      "    Rozliczator.UmowyODzieloView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Rozliczator     5      NameUmowyOdzielo     5         MaterialDesignBody#���   5	      $��Regular"�5
      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      � $	��Top=�6    � ColumnDefinitions 5   
   �� 5      $	��140 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6%    �� Orientation$ Vertical=�5      $	��450��6,   $	��140��69   $	��Top=�5      .+�5      �� -   5        .��5   3   Dodaj Umowę6@   �� 5      -   5        .��5   0   Wczytaj z DB6>   �� 5!      $��0,30,0,0q�6   .��6+   	Filtruj64   �� 5"      �� 5#      .��6   
Data od:6"   (  "System.Windows.Controls.DatePicker  5$      -   5        $��
DpFltrujOd��5$         Text$  ��60      SelectedDate 68   �� *FilterDataOd+5&      �� 5'      .��6   
Data do:6"     5(      -   5        $��
DpFltrujDo��5(      $  ��6/    67   �� *FilterDataDo+5)      �� 5*      .��6   Zleceniobiorca:6)   �� 5+      -   5        $��OsobaComboBox��5+       �� SelectedItem 61   �� *FilterZleceniobiorca+5,      �� 5-      .��6   
Wyplacono:6$   �� 5.      -   5        $��ZaplaconoComboBox��5.       65   �� *FilterZaplacono+5/      �� 50      -   5        $��0,30,0,0q�50   .   .��6@   Resetuj Filtry6P   52      &   System.Windows.Controls.DataGrid  54   
   -   5        $��DataGridUmowy��55      $��1��54      $��0��6#      AutoGenerateColumns .  	   SelectionUnit$	 FullRow=�57      ��. $
��Auto=�5:      $
��Auto=�5;      
   Columns
 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6-      
Visibility$ Visible=�6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Id+6P     5>      $ Numer Umowy��6-      
IsReadOnly .  6B   �� *
NumerUmowy+6a     5?      $ Czyja Umowa��6-    .  6B   �� *Osoba+6\     5@      $ Data Od��6-    .  6>   �� *DataPoczatek+ �� StringFormat$ {0:dd.MM.yyyy}��6~     5A      $ Data Do��6-    .  6>   �� *
DataKoniec+$ {0:dd.MM.yyyy}��6|     5B      $ 	Przychody��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6|    .  6@   �� *SumaPrzychodow+$ {0:0.00}��6�     5C      $ Wydatki��6-    .  6>   �� *SumaWydatkow+$ {0:0.00}��6x     5D      $ Diety��6-    .  6<   �� *
SumaDiet+$ {0:0.00}��6r     5E      $ Kwota Netto��6-    .  6B   �� *
KwotaNetto+$ {0:0.00}��6z   4  .System.Windows.Controls.DataGridCheckBoxColumn  5F      $ Wypł.��61    .  6@   �� *	Wyplacono+6^    � ContextMenu 5H      �� 5I      .@�5J      m� -	   5        $��Edytuj��5J   '   $��12(�67   6E   m� 5K      -
   5        $��Dodaj��5K   '   $��12(�66   6D   �� 5L      m� 5M      -   5        $��Usuń��5M   '   $��12(�65   6D   5N      5P      5R      5S      