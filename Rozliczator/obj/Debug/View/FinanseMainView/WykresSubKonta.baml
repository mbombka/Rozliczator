   M S B A M L   `   `   `  ����F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  5           Rozliczator.WykresSubKonta   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Rozliczator     5      V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
      $	��400��5	      $	��300��6      MaterialDesignBody#���   5      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      �� .+�5   
   F� -   5        $��ArcSubKontoSpolka��5      $��30��6(   ���	#FF55962C��5      B� 5      ��5      E�  E� 
StartPoint 6%   �� *	Arc1Start+��5   "   ��  �� RotationAngle$ 0Q�5   )    �� SweepDirection$ Counterclockwise=�5   )    �� Size 5   -   �� *	ArcRadius+ �� 
IsLargeArc 5   )   �� *	Arc1Big+ �� Point 5   )   �� *	Arc2Start+5       5"      5$      F� 5%   
   -   5        $��ArcSubKontoWspolnicy��5%      
����  �$��30��69   ��5&      B� 5'      ��5)      E�  6%   �� *	Arc2Start+��5+   "   �� $ 0Q�5,   )   $ Counterclockwise=�5.   )    5+   -   �� *	ArcRadius+ 5-   (   �� *	Arc2Big+ 5/   )   �� *	Arc3Start+51      53      55      F� 56   
   -   5        $��ArcSubKontoVAT��56      ���	#FFD9D951$��30��68   ��57      B� 58      ��5:      E�  6%   �� *	Arc3Start+��5<   "   �� $ 0Q�5=   )   $ Counterclockwise=�5?   )    5<   -   �� *	ArcRadius+ 5>   (   �� *	Arc3Big+ 5@   )   �� *	Arc4Start+5B      5D      5F      F� 5G   
   -   5        $��ArcSubKontoCIT��5G      ���	#FFD07676$��30��68   ��5H      B� 5I      ��5K      E�  6%   �� *	Arc4Start+��5M   "   �� $ 0Q�5N   )   $ Counterclockwise=�5P   )    5M   -   �� *	ArcRadius+ 5O   )   �� *	Arc4Big+ 5Q   )   �� *	Arc5Start+5S      5U      5W      F� 5X   
   -   5        $��ArcSubKontoPITT��5X      ���	#FF7F7A7A$��30��69   ��5Y      B� 5Z      ��5\      E�  6%   �� *	Arc5Start+��5^   "   �� $ 0Q�5_   )   $ Counterclockwise=�5a   )    5^   -   �� *	ArcRadius+ 5`   (   �� *	Arc5Big+ 5b   )   �� *	Arc1Start+5d      5f      5h      F� 5j   
   
�������$��2��6"   ��5k      B� 5l      ��5n      E�  6%   �� *
Arc1Middle+��5p   "   ��  �� Point 6/   �� *Arc1LineStart+�� 5q   "    6/   �� *Arc1LineEnd+5s      5u      5w      �� 5x   
   	 �� Orientation$	 
Horizontal=�6   

 �� Top
 60   �� *Arc1Label.Y+ �� Left 6T   �� *Arc1Label.X+.+�5y      �� .��6   Rachunek bieżący:6(   �� 5z      $��Center=�6H   ��6   �� * KontoCSEGHandle.SubKontoSpolka+6H   �� 5{      .��6   PLN6   5|      F� 5~   
   
�������$��2��6"   ��5      B� 5�      ��5�      E�  6%   �� *
Arc2Middle+��5�   "   ��  6/   �� *Arc2LineStart+�� 5�   "    6/   �� *Arc2LineEnd+5�      5�      5�      �� 5�   
   $	 
Horizontal=�6   
 60   �� *Arc2Label.Y+ 6T   �� *Arc2Label.X+.+�5�      �� .��6   SubKontoWspolnicy:6)   �� 5�      $��Center=�6K   ��6   �� *#!KontoCSEGHandle.SubKontoWspolnicy+6K   �� 5�      .��6   PLN6   5�      F� 5�   
   
�������$��2��6"   ��5�      B� 5�      ��5�      E�  6%   �� *
Arc3Middle+��5�   "   ��  6/   �� *Arc3LineStart+�� 5�   "    6/   �� *Arc3LineEnd+5�      5�      5�      �� 5�   
   $	 
Horizontal=�6   
 60   �� *Arc3Label.Y+ 6T   �� *Arc3Label.X+.+�5�      �� .��6   SubKontoVAT:6#   �� 5�      $��Center=�6E   ��6   �� *KontoCSEGHandle.SubKontoVAT+6E   �� 5�      .��6   PLN6   5�      F� 5�   
   
�������$��2��6"   ��5�      B� 5�      ��5�      E�  6%   �� *
Arc4Middle+��5�   "   ��  6/   �� *Arc4LineStart+�� 5�   "    6/   �� *Arc4LineEnd+5�      5�      5�      �� 5�   
   $	 
Horizontal=�6   
 60   �� *Arc4Label.Y+ 6T   �� *Arc4Label.X+.+�5�      �� .��6   SubKontoCIT:6#   �� 5�      $��Center=�6E   ��6   �� *KontoCSEGHandle.SubKontoCIT+6E   �� 5�      .��6   PLN6   5�      F� 5�   
   
�������$��2��6"   ��5�      B� 5�      ��5�      E�  6%   �� *
Arc5Middle+��5�   "   ��  6/   �� *Arc5LineStart+�� 5�   "    6/   �� *Arc5LineEnd+5�      5�      5�      �� 5�   
   $	 
Horizontal=�6   
 60   �� *Arc5Label.Y+ 6T   �� *Arc5Label.X+.+�5�      �� .��6   SubKontoPIT:6#   �� 5�      $��Center=�6E   ��6   �� *KontoCSEGHandle.SubKontoPIT+6E   �� 5�      .��6   PLN6   5�      �� 5�   
   $	 
Horizontal=�6   
 60   �� *MiddleLabel.Y+ 6V   �� *MiddleLabel.X+.+�5�      �� $
��Bold"�6:   $��14(�6L   $��Center=�6[   ��6   �� *MajatekSpolkiPLN+6[   �� 5�      $
��Bold"�6   .��6'   PLN6,   5�      5�      5�      