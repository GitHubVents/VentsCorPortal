   M S B A M L   `   `   `  ����  AirVentsOrderManager*    $AirVentsOrderManager.ManagerEditorUc   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 
ResizeMode$  NoResize=�5   $    � WindowStartupLocation$ CenterScreen=�6:    � ShowInTaskbar .   � Topmost . $	��300��5   	   $	��500��6   {�.  � WindowStyle$ 
ToolWindow=�5	      .��5
      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��30 �6   .+�5   
   �� -   5         ��Name$ GroupBox��5      $��Менеджеры��6%   $��0��68   .��5      &   System.Windows.Controls.DataGrid  -   5          Name$( "ТаблицаМенеджеров��5   J      
IsReadOnly .  $��15(�5   *   	   HorizontalGridLinesBrush	 �	#FF0B4EA2
   VerticalGridLinesBrush
 �	#FF0B4EA2   CanUserAddRows .    CanUserReorderColumns . $��0,0,4,4q�5   e   $
��Auto��6v      AutoGenerateColumns .  ��6�   �� *	Manager+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Фамилия��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6:   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *
LastName+  5      $ Имя��6)      MinWidth$	 100Q�66   $ * 6E    6O   �� *	FirstName+ � ContextMenu 5      �� 5      .@�5      m� -   5        $��Удалить..��5   #   66   5      5      5      �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5       �� -   5        $
��Ок��5       $��70��6"   $��25��6-   $��0,0,1,0q�69   ��6_   Z� 5!      5"      5$      