   M S B A M L   `   `   `  ���� F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  !    Rozliczator.KontrahenciView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:Rozliczator     Namewindow        MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  .��� $	��Top=� � ColumnDefinitions �� $	��140 ��� $��* � � RowDefinitions �� $��* �.+��� $��0��$��0�� �� Orientation$ Vertical=�$	��450��$	��140��.+��� -   .��Wczytaj z DB�� -   .��Zapisz do DB&   System.Windows.Controls.DataGrid  -   $��DataGridKontrahenci��$��1��$��0��   AutoGenerateColumns .     SelectionUnit$ CellOrRowHeader=�   CanUserAddRows .    CanUserDeleteRows . ��. $
��Auto=�$
��Auto=�	   Columns	 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn
   Header$
 Id��   
Visibility$ Visible=�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $
 Firma��   
IsReadOnly .   �� *
NazwaFirmy+  $

 Opis�� .   �� *Opis+  $
 Stawka�� .   �� *Stawka+ �� StringFormat$ {0:0.00}��  $
 Waluta�� .   �� *Waluta+