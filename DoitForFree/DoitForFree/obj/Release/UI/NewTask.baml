   M S B A M L   `   `   `  ���� F  BDoitForFree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:DoitForFree.UIDoitForFree.UI      DoitForFree.UI.NewTask   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:DoitForFree.UI     -   $��NewTaskWindow��
�������  � Topmost  .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=�$	��450��$	��630�� � 
ResizeMode$ NoResize=� � AllowsTransparency .  � 	Resources  �� MergedDictionaries �� Source��  �� $ ResourceDirectory.xaml?�%    .���  � RowDefinitions �� $��45 ��� $	��100 ��� $��55 ��� $��1 ��� $	��155 ��� $��1 ��� .+��� $��0��	 �� Orientation$	 
Horizontal=�$��Center=�$��Center=�$��
10,15,10,0q�.+��� -   $��	tbx标题��$��标题��   TextBox输入#��[  $	��590��$��30���� $��1��$	 
Horizontal=�$��Center=�$��Center=�$��
10,10,10,0q�.+��� -   $��	tbx描述��$��描述��#��[  $	��590��$��90���� $��2��$	 
Horizontal=�$��Center=�$��10,10,10,10q�.+�  DoitForFree.UI.MenuButton  -   
  Name$
 MenuButton类型��   DoitForFree.UI.MenuButton   Click$ MenuButton菜单_Click��$��	20,5,10,0q�   Text$ 类型��   	ImagePath$ ../Images/类型.png��  MenuButtonCalendar#��[  � ContextMenu �� -   $��
Menu类型��.@�m� -   $��今日待办��  downMenu#��[ m� -   $��正在行动��#��[ m� -   $��	收集箱��#��[   -	   $
 MenuButton截止时间��$ MenuButton菜单_Click��$��	10,5,10,0q�$ 截止时间��$ ../Images/日历.png��#��[  �� -
   $��MenuCalendar��.@�&   System.Windows.Controls.Calendar  -   $��Calendar截止时间���� $��3��$��	20,0,20,0q��� $��4��$	 Vertical=�$��10,10,10,10q�.+�  -   $
 MenuButton项目��$ MenuButton菜单_Click��$��	20,5,10,5q�$ 项目��$ ../Images/项目.png��#��[  �� -   $��
Menu项目��  -   $
 MenuButton目标��$ MenuButton菜单_Click��$��	20,5,10,5q�$ 目标��$  ../Images/今日待办.png��#��[  �� -   $��
Menu目标��  -   $
 MenuButton情境��$ MenuButton菜单_Click��$��	20,5,10,5q�$ 情境��$ ../Images/situation.png��#��[  �� -   $��
Menu情境���� $��5��$��	20,0,20,0q��� $��6��$	 
Horizontal=�$��Center=�$��RightToLeft=�.+��� -   $��	btn确定��$��确定��$��70��$��40��  Button确定取消#��[ $��20,10,10,10q��� -   $��	btn取消��$��取消��$��70��$��40��#��[ $��10,10,10,10q�