   M S B A M L   `   `   `  ����  LaboratoryTest!    LaboratoryTest.ListOfOrders   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � -   5        $��GridListOfOrders��5        � RowDefinitions  5	   
   �� 5
      $��50 �6   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ BtnNewProtocol��5   A   $��1��6   $4��.Новый протокол испытаний��6   6Y   &   System.Windows.Controls.DataGrid  5   
   -   5        $��DgListOfOrders��5         AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .  $��2��5   0   $��15(�5         HorizontalGridLinesBrush �	#FF0B4EA2   VerticalGridLinesBrush �	#FF0B4EA2$��5q�5   [      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 TypeItem��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
TypeItem+6T     5      $	 InfoItem��6%   $
 * 6T    67   �� *
InfoItem+6T     5      $	 TestProcedure��6%   $
 * 6^    6<   �� *TestProcedure+6^     5      $
	 Note��6%   $
 * 6L    63   �� *Note+6L     5      $	 	Capacitor��6%   $
 50 6V    68   �� *	Capacitor+6V     5      $	 FullName��6%   $
 * 6T    67   �� *
FullName+6T     5       $	 FanName��6%   $
 * 6R    66   �� *	FanName+6R   5"      5#      5$      