   M S B A M L   `   `   `  ���� F  BDoitForFree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DoitForFreeDoitForFree  /clr-namespace:DoitForFree.UIDoitForFree.UI      DoitForFree.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:DoitForFree     +UIclr-namespace:DoitForFree.UI     -     � Icon$  Images/icon.ico�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�$	��660��$	��660��$
��1100��$
��1100�� � Title$ 
MainWindow��$	��660��$
��1100�� � AllowsTransparency .  � WindowStyle$
 None=� � 	Resources  �� MergedDictionaries �� Source��  �� $ UI/ResourceDirectory.xaml?�%    .��� 	 � RowDefinitions	 �� $��55 ��� $��* ��� $��60 �
 � ColumnDefinitions
 �� $	��245 ��� $��* �.+�� $��0��$��0��$��2�����#1162A0.+���  �� Orientation$ 
Horizontal=�$��RightToLeft=�$��5,5,15,5q�.+�   DoitForFree.UI.ImageButton  -     Name$ btnClose��    DoitForFree.UI.ImageButton   Click$ btnClose_Click��$��27��$��26��   	ImagePath$ Images/关闭.png��   imageButton#��[    -   $ btnMin��$ btnMin_Click��$��27��$��26��$ Images/最小化.png��#��[    -   $ btnMenu��$ btnMenu_Click��$��27��$��26��$ Images/设置菜单.png��#��[   � ContextMenu �� -   $��downMenu��$	��100�� �� 	Placement$ Mouse=�
�������$��1q�.@�m� -   $��退出账户��  downMenu#��[ � $��1��$��0��$��2��.+��� $
��Auto=�  scrollviewer#��[ .���� -   $��spMenu��$ Vertical=����#F2F3F5.+�  DoitForFree.UI.MenuButton  -     Name$ 
menu收集��   MenuButton左侧菜单按钮#��[    Text$ 收集��  MenuButtonTitle#��[   -	   $ menu收集箱��   DoitForFree.UI.MenuButton   Click$ menu收集箱_Click��#��[ $ 	收集箱��   	ImagePath$ Images/收集箱.png��  
MenuButton#��[ ��   -
   $ 
menu聚焦��#��[ $ 聚焦��#��[   -   $ menu今日待办��$ menu今日待办_Click��#��[ $ 今日待办��$ Images/今日待办.png��#��[   -   $ menu正在行动��$ menu正在行动_Click��#��[ $ 正在行动��$ Images/正在行动.png��#��[   -   $ 
menu过期��$ menu过期_Click��#��[ $ 过期��$ Images/过期.png��#��[ ��   -   $ menu所有项目��$ menu所有项目_Click��   
MouseEnter$ mouseChange��   
MouseLeave$ mouseChange��#��[ $ 所有项目��$ Images/右箭头.png��#��[ � -   $��wp所有项目����   -   $ menu所有目标��$ menu所有目标_Click��$ mouseChange��$ mouseChange��#��[ $ 所有目标��$ Images/右箭头.png��#��[ � -   $��wp所有目标����   -   $ menu所有情境��$ menu所有情境_Click��$ mouseChange��$ mouseChange��#��[ $ 所有情境��$ Images/右箭头.png��#��[ � -   $��wp所有情境����   -   $ menu已完成��$ menu已完成_Click��#��[ $ 	已完成��$ Images/已完成.png��#��[ ��   -   $ menu垃圾箱��$ menu垃圾箱_Click��#��[ $ 	垃圾箱��$ Images/垃圾箱.png��#��[ �� � $��1��$��1�����#FFFFFF.+��� $
��Auto=�.��� -   $��wp右侧显示�� � Orientation$ Vertical=�� $��2��$��2�����#FFFFFF.+��� $��0,0,0,59q��� -   $��spn添加栏��$ 
Horizontal=�$��Center=�.+��� -   $��	spn添加��$ 
Horizontal=�$��Center=��� -   $��	spn通用��$ 
Horizontal=�$��Center=�