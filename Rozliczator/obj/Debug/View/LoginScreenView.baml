   M S B A M L   `   `   `  ����F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  5   	   !    Rozliczator.LoginScreenView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Rozliczator     5   	     � Title$  LoginScreenView��5   	   $	��300��6!   $	��600��6.    � WindowStartupLocation$ CenterScreen=�5	   	      MaterialDesignFont#���   5       � SizeToContent$ WidthAndHeight=�5   	    � AllowsTransparency .  � WindowStyle$
 None=�5   #    � Icon$! /Images/cseg_icon_32_32.ico��5   	    +� FocusedElement 5   	   ��  �� ElementName$ txtUserName����5      �� 5   
    �� Opacity$	 0.9Q�6   	 �� Color$	 	#FFFAFAFF��6(   
 � 	Resources
 5      3  -System.Windows.Media.Effects.DropShadowEffect  shadowWindow   Color   Opacity   ShadowDepth �� 
TargetType  WindowButtons �� Property �� Value��%�   &	       &	 F       5   
   $ Black��5      $ 1Q�5      $ 12Q�5      �� 5   
   " ��6   .�5      ��  �@J�Opacity$ 1Q�6(   5      5      .��5      ��  �� CornerRadius$ 10��6   $��4q�5    	   $��4q�5"   	    �� Padding$ 24q�5#   	   ._�5'   
   � .+�5(      �� $
��Left=�6   $	��201��6/   $��/Images/cseg_logo.png��6=   $��	10,10,0,0q�6]   $	��Top=�6p   $	��186��6�   �� 5)      -   5         ��Name$ txtUsername��5)      $
��Left=�6+   $��36��6F   $��
271,35,0,0q�6R    �� TextWrapping$
 Wrap=�6f   $�� ��6z   $	��Top=�6�   $	��254��6�   ���	#FF233737$��18(�6�   �� 5*      $��Login��6   $
��Left=�6$   $��
201,35,0,0q�6?   $	��Top=�6S   $��65��6k   $��36��6v   ���	#FF233737$��18(�6�   G� 5+      -   5         G�Name$ txtPassword��5+      $
��Left=�6/   $��37��6J   $��
271,76,0,0q�6V   $	��Top=�6k   $	��254��6�   ���	#FF233737$��18(�6�   �� 5,      $��Hasło��6   $
��Left=�6$   $��
201,77,0,0q�6?   $	��Top=�6S   $��65��6k   $��36��6v   ���	#FF233737$��18(�6�   �� 5-      -   5        $��Cancel��5-   %   $
��Auto��66   $��201,176,192,10q�6D    �� IsCancel . 6\   �� 5.      -   5         �� 	IsDefault . $��OK��5.   5   $
��Auto��6B   $��378,176,11,10q�6P   �� 5/      -   5         ��Name$ checkboxPamietaj��5/      $��Pamietaj��61   $
��Left=�6D   $��201,132,0,0q�6_   $	��Top=�6t   $	��241��6�   $��25��6�   ���	#FF233737���	#FFFAFAFA
�������6�   �� 50      -   5         ��Name$ InfoTextBlock��50   D    �� TextAlignment$ Center=�6   $��0,245,0,-25q�6/   $	��Top=�6\   $	��536��6t   ���	#FF2337376�   54      56      57      