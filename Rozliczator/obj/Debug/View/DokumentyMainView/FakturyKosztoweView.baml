   M S B A M L   `   `   `  ����F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  5      %    Rozliczator.FakturyKosztoweView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Rozliczator     5      V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	         MaterialDesignBody#���   5
      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      � $	��Top=�6    � ColumnDefinitions 5   
   �� 5      $	��140 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6%    �� Orientation$ Vertical=�62   $	��140��6J   $	��Top=�6V   .+�5      �� -   5        .��5   5   Dodaj Fakturę6D   �� 5      -   5        .��5   0   Wczytaj z DB6>   �� 5       $��0,30,0,0q�6   .��6+   	Filtruj64   �� 5!      �� 5"      .��6   
Data od:6"   (  "System.Windows.Controls.DatePicker  5#      -   5        $��
DpFltrujOd��5#         Text$  ��60      SelectedDate 68   �� *FilterDataOd+5%      �� 5&      .��6   
Data do:6"     5'      -   5        $��
DpFltrujDo��5'      $  ��6/    67   �� *FilterDataDo+5(      �� 5)      .��6   Czyj koszt:6%   �� 5*      -   5        $��CzyjKosztComboBox��5*       �� SelectedItem 65   �� *FilterCzyjKoszt+5+      �� 5-      .��6   Sprzedawca:6%   �� 5.      -   5        $��SprzedawcaComboBox��5.       66   �� *FilterSprzedawca+5/      �� 50      .��6   
Zaplacono:6$   �� 51      -   5        $��ZaplaconoComboBox��51       65   �� *FilterZaplacono+52      �� 53      -   5        $��0,30,0,0q�53   .   .��6@   Resetuj Filtry6P   54      &   System.Windows.Controls.DataGrid  56   
   -	   5        $��DataGridKosztowe��57      $��1��56      $��0��6#      AutoGenerateColumns .  	   SelectionUnit$	 FullRow=�59      
   CanUserAddRows
 .     CanUserDeleteRows .  ��. $
��Auto=�5>      $
��Auto=�5?         Columns 5A      0  *System.Windows.Controls.DataGridTextColumn  5B      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6-      
Visibility$ 	Collapsed=�6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Id+6P     5C      $ 
Sprzedawca��6-      
IsReadOnly .  6A   �� *
Sprzedawca+6`     5D      $ Numer Faktury��6-    .  6D   �� *NumerFaktury+6e     5E      $ Data Sprzedazy ��6-    .  6F   �� *DataWystawienia+ �� StringFormat$ {0:dd.MM.yyyy}��6�     5F      $
 Opis��6-    .  6;   �� *Opis+6V     5G      $ Kwota��6-    .  6<   �� *Kwota+$ F2��6h     5H      $ Waluta��6-    .  6=   �� *Waluta+6Z   4  .System.Windows.Controls.DataGridCheckBoxColumn  5I      $ Zapł��61    .  6?   �� *	Zaplacono+6^     5J      $ 
Czyj Koszt��6-    .  6A   �� *	CzyjKoszt+6`    � ContextMenu 5L      �� 5M      .@�5N      m� -
   5        $��Edytuj��5N   '   $��12(�67   6E   m� 5O      -   5        $��Dodaj��5O   '   $��12(�66   6D   �� 5P      m� 5Q      -   5        $��Usuń��5Q   '   $��12(�65   6D   5R      5T      5X      5Y      