   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  F BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator $   Rozliczator.KontaWspolnicyView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6sys&clr-namespace:System;assembly=mscorlib     +localclr-namespace:Rozliczator     $	��400��$	��600��   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � 	Resources  
 Height 	 Width��%.   &	       &	      X�200Q�X�150Q�.��� .+���  �� Orientation$ Vertical=�.+��� .��-+Status majątku wspólników na koncie CSEG�  � ColumnDefinitions �� $��1* ��� $��1* �.+��� $ Vertical=�$��0��.+�9�  9� ExpandDirection$
 Down=� 9� 
IsExpanded .  ���� $ 
Horizontal=�.+��� $��0,0,10,0q�.��Piotr Pawłowski.���� .+��� $ 
Horizontal=�$��0,0,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��Total:����  �� TextAlignment$ Center=�$��80��$��0q����� *KontoPPawlowski.Total+ �� Mode$ OneWay=�	 �� StringFormat$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $ 
Horizontal=�$��0,20,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��
Delegacje:���� $ Center=�$��80��$��0q����� *KontoPPawlowski.SubDelegacje+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $ 
Horizontal=�$��0,20,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��Umowy:���� $ Center=�$��80��$��0q����� *KontoPPawlowski.SubUmowy+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $ 
Horizontal=�$��0,20,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��Zwroty:���� $ Center=�$��80��$��0q����� *KontoPPawlowski.SubZwroty+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $��20��9� $
 Down=� .  ���� $ 
Horizontal=�.+��� $��0,0,0,0q�.��Piotr Ostrowski.���� .+��� $ 
Horizontal=�$��0,0,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��Total:���� $ Center=�$��80��$��0q����� *KontoPOstrowski.Total+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $ 
Horizontal=�$��0,20,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��
Delegacje:���� $ Center=�$��80��$��0q����� *KontoPOstrowski.SubDelegacje+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $ 
Horizontal=�$��0,20,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��Umowy:���� $ Center=�$��80��$��0q����� *KontoPOstrowski.SubUmowy+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $ 
Horizontal=�$��0,20,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��Zwroty:���� $ Center=�$��80��$��0q����� *KontoPOstrowski.SubZwroty+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $��20��9� $
 Down=� .  ���� $ 
Horizontal=�.+��� $��0,0,0,0q�.��Mariusz Bąbka.���� .+��� $ 
Horizontal=�$��0,0,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��Total:���� $ Center=�$��80��$��0q����� *KontoMBabka.Total+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $ 
Horizontal=�$��0,20,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��
Delegacje:���� $ Center=�$��80��$��0q����� *KontoMBabka.SubDelegacje+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $ 
Horizontal=�$��0,20,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��Umowy:���� $ Center=�$��80��$��0q����� *KontoMBabka.SubUmowy+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $ 
Horizontal=�$��0,20,0,0q�.+��� #��[ $��Bottom=�$��20,0,0,0q�$��Zwroty:���� $ Center=�$��80��$��0q����� *KontoMBabka.SubZwroty+$ OneWay=�$	 {0:0.00}���� $ Center=�$��30��$	��PLN��$��0q��� $��20���� $��1��$��30,0,0,0q�.+��� $ Vertical=�.+��� $ 
Horizontal=�.+��� $��Bottom=�$��Center=�$"��Wykonaj operacje na koncie: ��$��	0,10,10,0q��� -   $��OsobaComboBox��$	��100��
 �� 
IsEditable
 .  $��Center=� �� Text �� *Osoba+�� -    ��Name$ OperacjeComboBox��$��Bottom=�$��10,20,114,0q� �� *RodzajOperacji+�� $ 
Horizontal=�.+��� $��Bottom=�$��0,20,0,0q�$��Kwota���� $ Center=�$��80��$��0q����� *KwotaOperacji+$ TwoWay=�$	 {0:0.00}���� $ 
Horizontal=�$��0,20,0,0q�.+��� $��Bottom=�$��0,0,0,0q�$
��Opis��$��37���� $ Center=�$	��200��$��0q����� *OpisOperacji+$ TwoWay=�$	 {0:0.00}���� -   $��90��$��0,20,0,0q�.��	wykonaj