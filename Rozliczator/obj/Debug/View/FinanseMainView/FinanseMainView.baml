   M S B A M L   `   `   `  ����F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  5      !    Rozliczator.FinanseMainView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Rozliczator     5         MaterialDesignBody#���   5
      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5       � 	Resources 5       �� 
TargetType  TabItemTemplate ��Name �� Padding �� CornerRadius �� Triggers P� Property	 P� Value
 �� 
TargetName �� Property �� Value��%h  &	       �� 5   
   " ��6   .R�5      �� $ ContentPresenterBorder��6   $ 9q�65   
�������$��1,1,1,0q�6Y   $	��2,0q�6s   $ 3,3,0,0��6�   ._�5      �� $��Header��6#   5       5      P� 5       �@��
IsSelected	 . . �5      �� $
 ContentPresenterBorder��6O    � ��$ 2,2,2,0q�6?   6O   5      5      5      .��5      � .+�5!   
   �� $��0,0,0,0q�6   .@�5#   
   �� $��
Konta CSEG��6   #��[ 6&   .��5$         Rozliczator.KontaCSEGView  $��0,5,0,0q�6#   5%      �� 5&   
   $��Konta Wspolnicy��6   #��[ 6+   .��5'      $   Rozliczator.KontaWspolnicyView  $��0,5,0,0q�6(   5(      5)      5*      5+      