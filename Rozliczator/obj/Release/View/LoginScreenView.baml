   M S B A M L   `   `   `  ���� F  BRozliczator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RozliczatorRozliczator  !    Rozliczator.LoginScreenView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Rozliczator       � Title$  LoginScreenView��$	��300��$	��600�� � WindowStartupLocation$ CenterScreen=�   MaterialDesignFont#���    � SizeToContent$ WidthAndHeight=� � AllowsTransparency .  � WindowStyle$
 None=� � Icon$! /Images/cseg_icon_32_32.ico�� +� FocusedElement ��  �� ElementName$ txtUserName������  �� Opacity$	 0.9Q�	 �� Color$	 	#FFFAFAFF��
 � 	Resources
 3  -System.Windows.Media.Effects.DropShadowEffect  shadowWindow   Color   Opacity   ShadowDepth �� 
TargetType  WindowButtons �� Property �� Value��%`   &	       &	 "       $ Black��$ 1Q�$ 12Q��� " ��.���  �@J�Opacity$ 1Q�.����  �� CornerRadius$ 10��$��4q�$��4q� �� Padding$ 24q�._�� .+��� $
��Left=�$	��201��$��/Images/cseg_logo.png��$��	10,10,0,0q�$	��Top=�$	��186���� -    ��Name$ txtUsername��$
��Left=�$��36��$��
271,35,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��254�����	#FF233737$��18(��� $��Login��$
��Left=�$��
201,35,0,0q�$	��Top=�$��65��$��36�����	#FF233737$��18(�G� -    G�Name$ txtPassword��$
��Left=�$��37��$��
271,76,0,0q�$	��Top=�$	��254�����	#FF233737$��18(��� $��Hasło��$
��Left=�$��
201,77,0,0q�$	��Top=�$��65��$��36�����	#FF233737$��18(��� -   $��Cancel��$
��Auto��$��201,176,192,10q� �� IsCancel . �� -    �� 	IsDefault . $��OK��$
��Auto��$��378,176,11,10q��� -    ��Name$ checkboxPamietaj��$��Pamietaj��$
��Left=�$��201,132,0,0q�$	��Top=�$	��241��$��25�����	#FF233737���	#FFFAFAFA
��������� -    ��Name$ InfoTextBlock�� �� TextAlignment$ Center=�$��0,245,0,-25q�$	��Top=�$	��536�����	#FF233737