   M S B A M L   `   `   `  ����F  BDoitForFree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:DoitForFree.UIDoitForFree.UI  5   	       DoitForFree.UI.NewProject   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:DoitForFree.UI     5   	   -   5        $��NewProjectWindow��5   D     � WindowStyle$
  None=�6   
������� � WindowStartupLocation$ CenterScreen=�6\   $	��300��5   	   $	��630��6    � 
ResizeMode$ NoResize=�6"    � AllowsTransparency .  � Topmost .  � 	Resources 5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $ ResourceDirectory.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��45 �6   �� 5      $	��100 �6   �� 5      $��55 �6   �� 5      $��1 �6   �� 5      .+�5   
   �� $��0��6   	 �� Orientation$	 
Horizontal=�6"   $��Center=�6;   $��Center=�6V   $��
10,15,10,0q�6s   .+�5      �� -   5        $��	tbx标题��5      $��标题��6#      TextBox输入#��[  6-   $	��590��6P   $��30��6\   6�   5      �� 5   
   $��1��6   $	 
Horizontal=�6"   $��Center=�6;   $��Center=�6V   $��
10,10,10,0q�6s   .+�5      �� -   5        $��	tbx描述��5      $��描述��6#   #��[  6-   $	��590��6P   $��90��6\   6�   5      �� 5!   
   $��2��6   $	 
Horizontal=�6"   $��Center=�6;   $��10,10,10,10q�6V   .+�5"        DoitForFree.UI.MenuButton  -   5        
  Name$
 MenuButton截止时间��5"         DoitForFree.UI.MenuButton   Click$" MenuButton截止时间_Click��6�   $��	10,5,10,0q�67      Text$ 截止时间��6J      	ImagePath$ ../Images/日历.png��6V     MenuButtonCalendar#��[ 6s    � ContextMenu 5#      �� 5$      -   5        $��MenuCalendar��5$   "   .@�5%      &   System.Windows.Controls.Calendar  -   5        $��Calendar截止时间��5%   #   6l   5&      5(      5)      �� 5*   
   $��3��6   $��	20,0,20,0q�6!   �� 5,   
   $��4��6   $	 
Horizontal=�6"   $��Center=�6;   $��RightToLeft=�6V   .+�5-      �� -   5        $��	btn确定��5-      $��确定��6"   $��70��6/   $��40��6:     Button确定取消#��[ 6F   $��20,10,10,10q�6m   6�   �� 5.      -   5        $��	btn取消��5.      $��取消��6"   $��70��6/   $��40��6:   #��[ 6F   $��10,10,10,10q�6m   6�   5/      50      51      