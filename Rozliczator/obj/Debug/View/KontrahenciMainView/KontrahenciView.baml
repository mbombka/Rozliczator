   M S B A M L   `   `   `  ����F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  5      !    Rozliczator.KontrahenciView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Rozliczator     5      Namewindow     5         MaterialDesignBody#���   5
      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      � $	��Top=�6    � ColumnDefinitions 5   
   �� 5      $	��140 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6%    �� Orientation$ Vertical=�62   $	��450��6I   $	��140��6V   $��2,46,2,0q�6b   .+�5      �� -   5        .��5   ,   Wczytaj z DB6:   �� 5      -   5        .��5   +   Zapisz do DB69   5#      &   System.Windows.Controls.DataGrid  5&   
   -   5        $��DataGridKontrahenci��5&   2   $��1��6   $��0��6$      AutoGenerateColumns .     SelectionUnit$ CellOrRowHeader=�5)         CanUserAddRows .    CanUserDeleteRows . ��. $
��Auto=�5-      $
��Auto=�5.      	   Columns	 5/      0  *System.Windows.Controls.DataGridTextColumn  50      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Id��6%      
Visibility$ Visible=�6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     51      $
 Firma��6%      
IsReadOnly .   64   �� *
NazwaFirmy+6S     52      $

 Opis��6%    .   63   �� *Opis+6L     53      $
 Stawka��6%    .   65   �� *Stawka+ �� StringFormat$ {0:0.00}��6i     54      $
 Waluta��6%    .   65   �� *Waluta+6Q   58      5<      5>      