   M S B A M L   `   `   `  ����  LaboratoryTest$    LaboratoryTest.NewProtocolForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  Новый протокол��5   	   $	��221��6    $	��434��6-    � Topmost .  � WindowStartupLocation$ CenterScreen=�6H    � 
ResizeMode$ NoResize=�6m   .��5      � -   5        $��GridNewProtocol��5       � ColumnDefinitions 5   
   �� 5      $	��95* �6   �� 5      $
��298* �6    � RowDefinitions 5   
   �� 5      $
��132* �6   �� 5      $��30 �6   .+�5   
   �� $��0��6   $��0��6%    �� Orientation$ Vertical=�62   .+�5      �� $��	Дата:��6   $
��Left=�6$   �� 5      $��Протокол №��6   $
��Left=�6)   �� 5      $��Наименование:��6   $
��Left=�6,   �� 5      $��Крыльчатка:��6   $
��Left=�6*   �� 5      $!��Производитель:��6   $
��Left=�6-   �� 5      $��Напряжение:��6   $
��Left=�6*   5      �� 5   
   $��1��6   $��0��6&   $ Vertical=�63   .+�5      (  "System.Windows.Controls.DatePicker  -   5        $��DateP��5      $
��Left=�6&   $	��Top=�6A   $��
0, 3, 0, 0q�5      �� 5      -   5         ��Name$ TxtNewProtocolNo��5      $��
0, 3, 5, 0q�60   �� 5      -   5        $ TxtNewDescription��5      $��
0, 3, 5, 0q�61   �� 5      -   5         ��Name$ CboNewImpeller��5      $��
0, 3, 5, 0q�6/   �� 5       -   5        $ TxtNewDeveloper��5       $��
0, 3, 5, 0q�6/   �� 5!      -   5        $ 
CboNewVolt��5!      $��
0, 3, 5, 0q�6+   5"      �� 5$   
   $��1��6   $��1��6&   $ Vertical=�63   .+�5%      �� -   5        	 ��Name$	 BtnCreatNewProtocol��5%      
 �� 	IsDefault
 . $��Создать��5&      $��5q�6   $��22��6(   $��Right=�5'      $��75��6(   5(      5)      5+      5,      