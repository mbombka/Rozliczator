   M S B A M L   `   `   `  ���� F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator       Rozliczator.WykresSubKonta   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:Rozliczator     V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  $	��400��$	��300��   MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  .���� .+�F� -   $��ArcSubKontoSpolka��$��30�����	#FF55962C��B� ��E�  E� 
StartPoint �� *	Arc1Start+����  �� RotationAngle$ 0Q� �� SweepDirection$ Counterclockwise=� �� Size �� *	ArcRadius+ �� 
IsLargeArc �� *	Arc1Big+ �� Point �� *	Arc2Start+F� -   $��ArcSubKontoWspolnicy��
����  �$��30����B� ��E�  �� *	Arc2Start+���� $ 0Q�$ Counterclockwise=� �� *	ArcRadius+ �� *	Arc2Big+ �� *	Arc3Start+F� -   $��ArcSubKontoVAT�����	#FFD9D951$��30����B� ��E�  �� *	Arc3Start+���� $ 0Q�$ Counterclockwise=� �� *	ArcRadius+ �� *	Arc3Big+ �� *	Arc4Start+F� -   $��ArcSubKontoCIT�����	#FFD07676$��30����B� ��E�  �� *	Arc4Start+���� $ 0Q�$ Counterclockwise=� �� *	ArcRadius+ �� *	Arc4Big+ �� *	Arc5Start+F� -   $��ArcSubKontoPITT�����	#FF7F7A7A$��30����B� ��E�  �� *	Arc5Start+���� $ 0Q�$ Counterclockwise=� �� *	ArcRadius+ �� *	Arc5Big+ �� *	Arc1Start+F� 
�������$��2����B� ��E�  �� *
Arc1Middle+����  �� Point �� *Arc1LineStart+��  �� *Arc1LineEnd+�� 	 �� Orientation$	 
Horizontal=�

 �� Top
 �� *Arc1Label.Y+ �� Left �� *Arc1Label.X+.+��� .��Rachunek bieżący:�� $��Center=����� * KontoCSEGHandle.SubKontoSpolka+�� .��PLNF� 
�������$��2����B� ��E�  �� *
Arc2Middle+����  �� *Arc2LineStart+��  �� *Arc2LineEnd+�� $	 
Horizontal=�
 �� *Arc2Label.Y+ �� *Arc2Label.X+.+��� .��SubKontoWspolnicy:�� $��Center=����� *#!KontoCSEGHandle.SubKontoWspolnicy+�� .��PLNF� 
�������$��2����B� ��E�  �� *
Arc3Middle+����  �� *Arc3LineStart+��  �� *Arc3LineEnd+�� $	 
Horizontal=�
 �� *Arc3Label.Y+ �� *Arc3Label.X+.+��� .��SubKontoVAT:�� $��Center=����� *KontoCSEGHandle.SubKontoVAT+�� .��PLNF� 
�������$��2����B� ��E�  �� *
Arc4Middle+����  �� *Arc4LineStart+��  �� *Arc4LineEnd+�� $	 
Horizontal=�
 �� *Arc4Label.Y+ �� *Arc4Label.X+.+��� .��SubKontoCIT:�� $��Center=����� *KontoCSEGHandle.SubKontoCIT+�� .��PLNF� 
�������$��2����B� ��E�  �� *
Arc5Middle+����  �� *Arc5LineStart+��  �� *Arc5LineEnd+�� $	 
Horizontal=�
 �� *Arc5Label.Y+ �� *Arc5Label.X+.+��� .��SubKontoPIT:�� $��Center=����� *KontoCSEGHandle.SubKontoPIT+�� .��PLN�� $	 
Horizontal=�
 �� *MiddleLabel.Y+ �� *MiddleLabel.X+.+��� $
��Bold"�$��14(�$��Center=����� *MajatekSpolkiPLN+�� $
��Bold"�.��PLN