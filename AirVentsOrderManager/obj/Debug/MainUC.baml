   M S B A M L   `   `   `  ����  AirVentsOrderManager!    AirVentsOrderManager.MainUc   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5      )  #System.Windows.Controls.DataGridRow �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   '        �� 5   
   "  6    5	      P� 5
       �@J�IsMouseOver . . �5      ��  � ��
 ��� �63   �� 5       � ��
 �����63   5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��ForDataTable��5      $��0��6'   $	��200��64   6C     5   
   -   5          Name$ 	OrderGrid��5      	   SelectionMode$	 Single=�6A   
   
IsReadOnly
 .    AutoGenerateColumns .  $
��Auto��5   1   $��15(�5         HorizontalGridLinesBrush �	#FF0B4EA2   VerticalGridLinesBrush �	#FF0B4EA2$��1��6g    � ContextMenu 5      �� 5      .@�5      m� -   5        $"��Редактировать..��5      68   m� 5      -   5        $��Удалить..��5      62   5         Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$ Подбор��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 135 65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *ProjectNumber+  5#      $ Менеджер��6%   $ * 67    6A   �� *Expr1+  5$      $ 
Заказ��6%   $ * 64    6>   �� *InternalNumberFull+ �� StringFormat$ d��  5%      $ Тип каркаса��6%   $ * 6:    6D   �� *Description+$ d��  5&      $ Конструктор��6%   $ * 6:    6D   �� *Expr2+  5'      $ Дата��6%   $ * 63    6=   �� *Date+$ d�� �� ConverterCulture$ ru-RU��  5(      $ Поступление��6%   $ * 6:    6D   �� *RequiredDate+$ ru-RU��$ d��  5)      $ Сдача КД (П)��6%   $ * 6;    6E   �� *CompletionDate+$ ru-RU��$ d��  5*      $ Сдача КД (Ф)��6%   $ * 6;    6E   �� *FinishCompletionDate+$ ru-RU��$ d��  5+      $ Отгрузка��6%   $ * 67    6A   �� *ShippedDate+$ ru-RU��$ d��54      �� 55   
   -   5        $��	CounLabel��55      $��2��6!   60   56      57      