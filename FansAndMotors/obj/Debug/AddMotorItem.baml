   M S B A M L   `   `   `  ����  FansAndMotors     FansAndMotors.AddMotorItem   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  AddMotorItem��5   	   $	��578��6    � WindowStartupLocation$ CenterScreen=�6*   $	��260��6O    � 
ResizeMode$ NoResize=�6\   .��5      � -   5        $��GridHome��5       � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $
��281* �6   �� 5	      $
��189* �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $	��120 �6   �� 5      $��50 �6   .+�5   
   �� $��0��6   $��0��6"    �� Orientation$ Vertical=�62   $��Right=�6I   .+�5      �� $��Мотор:��6   5      �� 5   
   $��0��6   $ 
Horizontal=�6"   $��1��6;   $��2��6K   .+�5      �� -   5        $��LblGenerateName��5      $	��174��6+   67   5      �� 5   
   $��1��6   $��0��6"   $ Vertical=�62   .+�5      �� $��Тип мотора:��6   �� 5      $��Кол. полюсов:��6   �� 5      $��Напряжение:��6   �� 5      $��Корпус:��6   5      �� 5    
   $��1��6   $ Vertical=�6"   $��1��69   $
��Left=�5!      .+�5"      �� -   5        $��CboMotorType��5"      $	��120��6,   �� 5#      -   5        $��CboPole��5#      $	��120��6'   �� 5$      -   5        $��CboVolt��5$      $	��120��6'   �� 5&      -   5        $��CboMotorParam��5&      $	��120��6,   5'      �� 5)   
   $��2��6   $��2��6"   .+�5*      �� -   5         ��Name$ BtnSave��5*      $��Сохранить��6&   6:   5+      5-      5.      