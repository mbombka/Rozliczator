   M S B A M L   `   `   `  ���� F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator      Rozliczator.KontaCSEGView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:Rozliczator        MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$	��300��.+��� $
�� 300��$
�� 400��.��    Rozliczator.WykresSubKonta  ��  �� Orientation$ Vertical=� �� Left$	 700��$��Right=�.+��� $ 
Horizontal=�$��0,20,0,0q�.+��� $��Bottom=�$��Center=�$"��Wykonaj operacje na koncie: ��$��	0,10,10,0q��� -    ��Name$ OperacjeComboBox��$��Bottom=�$��3,20,0,0q� �� Text �� *RodzajOperacji+�� $ 
Horizontal=�$��0,20,0,0q�.+��� $��Bottom=�$��0,20,0,0q�$��Kwota����  �� TextAlignment$ Center=�$��80��$��0q����� *KwotaOperacji+ �� Mode$ TwoWay=�	 �� StringFormat$	 {0:0.00}���� $ 
Horizontal=�$��0,30,0,0q�.+��� $��Bottom=�$��0,0,0,0q�$
��Opis��$��37���� $ Center=�$	��200��$��0q����� *OpisOperacji+$ TwoWay=�$	 {0:0.00}���� -   $��90��$��0,30,0,0q�.��	wykonaj�� $��1��$��50��$ 
Horizontal=�.+��� $��Bottom=�$��20,0,0,0q�$��
Konto PLN:���� $ Center=�$��80��$��0q����� *KontoCSEGHandle.KontoPLN+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $��Bottom=�$��50,0,0,0q�$��
Konto EUR:���� $ Center=�$��80��$��0q����� *KontoCSEGHandle.KontoEUR+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��EUR��$��0q��� $��Bottom=�$��50,0,0,0q�$��
Konto GBP:���� $ Center=�$��80��$��0q����� *KontoCSEGHandle.KontoGBP+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��GBP��$��0q��� $��0,5,0,0q�$��1��$��Stretch=�$��Stretch=�
 �� ResizeBehavior$
 BasedOnAlignment=�$��10�����	#FFBCBCBC� $��2�� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��0��$��10,5,0,0q�.��
Historia&   System.Windows.Controls.DataGrid  -   $��DataGridKontaCSEG��$��1��$��Stretch=�   AutoGenerateColumns .     SelectionUnit$ FullRow=�   CanUserAddRows .     CanUserDeleteRows .  ��. $
��Auto=�$
��Auto=�   HeadersVisibility$	 All=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   
Visibility$ 	Collapsed=�   SortDirection$ 
DescendingX�1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ 	Konto PLN��   
IsReadOnly .  �� *
KontoPLN+$	 F2��  $ 	Konto EUR�� .  �� *
KontoEUR+$	 F2��  $ 	Konto GBP�� .  �� *
KontoEUR+$	 F2��  $ 	SK Spolka�� .  �� *SubKontoSpolka+$	 F2��  $ SK Wspolnicy�� .  �� *SubKontoWspolnicy+$	 F2��  $ SK VAT�� .  �� *SubKontoVAT+$	 F2��  $ SK CIT�� .  �� *SubKontoCIT+$	 F2��  $ SK PIT�� .  �� *SubKontoPIT+$	 F2��  $
 Opis��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50  .  �� *Opis+  $ Kwota�� .  �� *Kwota+$	 F2�� � ContextMenu �� 