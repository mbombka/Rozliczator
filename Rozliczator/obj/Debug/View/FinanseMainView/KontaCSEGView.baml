   M S B A M L   `   `   `  ����F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  5          Rozliczator.KontaCSEGView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Rozliczator     5         MaterialDesignBody#���   5
      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5"      �� $��0��6   $	��300��6$   .+�5$      �� $
�� 300��6#   $
�� 400��61   .��5%          Rozliczator.WykresSubKonta  5&      �� 5(       �� Orientation$ Vertical=�6    �� Left$	 700��65   $��Right=�6G   .+�5)      �� $ 
Horizontal=�6!   $��0,20,0,0q�6:   .+�5*      �� $��Bottom=�6    $��Center=�6;   $"��Wykonaj operacje na koncie: ��6X   $��	0,10,10,0q�6   5,      �� 5.      -   5         ��Name$ OperacjeComboBox��5.   :   $��Bottom=�6   $��3,20,0,0q�6t    �� Text 6T   �� *RodzajOperacji+6t   �� 5/      $ 
Horizontal=�6!   $��0,20,0,0q�6;   .+�50      �� $��Bottom=�6    $��0,20,0,0q�6;   $��Kwota��6M   �� 51       �� TextAlignment$ Center=�6"   $��80��6:   $��0q�6�   ��6E   �� *KwotaOperacji+ �� Mode$ TwoWay=�	 �� StringFormat$	 {0:0.00}��6�   52      �� 53      $ 
Horizontal=�6!   $��0,30,0,0q�6;   .+�54      �� $��Bottom=�6    $��0,0,0,0q�6;   $
��Opis��6L   $��37��6[   �� 55      $ Center=�6"   $	��200��6:   $��0q�6�   ��6F   �� *OpisOperacji+$ TwoWay=�$	 {0:0.00}��6�   56      �� 57      -   5        $��90��57      $��0,30,0,0q�6(   .��6O   	wykonaj6X   59      5:      �� 5<      $��1��6   $��50��6'   $ 
Horizontal=�63   .+�5=      �� $��Bottom=�6   $��20,0,0,0q�63   $��
Konto PLN:��6E   �� 5>      $ Center=�6   $��80��61   $��0q�6�   ��6<   �� *KontoCSEGHandle.KontoPLN+$ OneWay=�$	 {0:0.00}��6�   �� 5?      $ Center=�6   $��30��61   $	��PLN��6<   $��0q�6G   �� 5A      $��Bottom=�6   $��50,0,0,0q�63   $��
Konto EUR:��6E   �� 5B      $ Center=�6   $��80��61   $��0q�6�   ��6<   �� *KontoCSEGHandle.KontoEUR+$ OneWay=�$	 {0:0.00}��6�   �� 5C      $ Center=�6   $��30��61   $	��EUR��6<   $��0q�6G   �� 5E      $��Bottom=�6   $��50,0,0,0q�63   $��
Konto GBP:��6E   �� 5F      $ Center=�6   $��80��62   $��0q�6�   ��6=   �� *KontoCSEGHandle.KontoGBP+$ OneWay=�$	 {0:0.00}��6�   �� 5G      $ Center=�6   $��30��61   $	��GBP��6<   $��0q�6G   5I      5J      �� 5K   
   $��0,5,0,0q�6   $��1��6(   $��Stretch=�66   $��Stretch=�5L      
 �� ResizeBehavior$
 BasedOnAlignment=�5M      $��10��5N      ���	#FFBCBCBC6   � 5P   
   $��2��6    5Q      �� 5R      $
��Auto �6    �� 5S      $��* �6    �� 5T      $
��Auto �6    .+�5W      �� $��0��6   $��10,5,0,0q�6!   .��63   
Historia6=   &   System.Windows.Controls.DataGrid  5Y      -   5        $��DataGridKontaCSEG��5Y   $   $��1��6   $��Stretch=�5[         AutoGenerateColumns .     SelectionUnit$ FullRow=�5]         CanUserAddRows .     CanUserDeleteRows .  ��. $
��Auto=�5a      $
��Auto=�5b         HeadersVisibility$	 All=�5c   #      Columns 5e      0  *System.Windows.Controls.DataGridTextColumn  5f   "   ,  &System.Windows.Controls.DataGridColumn   Header$ Id��65      
Visibility$ 	Collapsed=�6X      SortDirection$ 
DescendingX�6o   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *Id+6o     5g   "   $ 	Konto PLN��65      
IsReadOnly .  6H   �� *
KontoPLN+$	 F2��6v     5h   "   $ 	Konto EUR��65    .  6H   �� *
KontoEUR+$	 F2��6v     5i   "   $ 	Konto GBP��65    .  6H   �� *
KontoEUR+$	 F2��6w     5j   "   $ 	SK Spolka��65    .  6H   �� *SubKontoSpolka+$	 F2��6}     5k   "   $ SK Wspolnicy��65    .  6K   �� *SubKontoWspolnicy+$	 F2��6�     5l   "   $ SK VAT��65    .  6E   �� *SubKontoVAT+$	 F2��6w     5m   "   $ SK CIT��65    .  6E   �� *SubKontoCIT+$	 F2��6w     5n   "   $ SK PIT��65    .  6E   �� *SubKontoPIT+$	 F2��6w     5p   "   $
 Opis��65      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6\    .  6C   �� *Opis+6i     5q   "   $ Kwota��65    .  6D   �� *Kwota+$	 F2��6p    � ContextMenu 5t      �� 5u   "   5w   #   5y      5|   
   5�      5�      