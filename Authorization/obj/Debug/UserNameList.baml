   M S B A M L   `   `   `  ����  Authorization     Authorization.UserNameList   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  UserNameList��5   	   $	��463��6   $	��370��6+    � WindowStartupLocation$ CenterScreen=�67    � Topmost . .��5      � -   5        $��GridUsersNameList��5      .+�5   
   �� -   5         ��Name$ ListViewAllUsers��5      $��10,10,13,37q�5      ��5      �� *	UserClass+ �� View 5	      �� 5
      .P�5      ��  �� CellTemplate 5      �� 5      .R�5   "   �� .+�5   &   ��  ��Name$ ChkBoxUsers��6/    d� 	IsChecked 6D   �� *
IsSelected+ �� RelativeSource �� 	 �� AncestorType"	 ��5   #   5      5      �� 5      $��Пользователи��6&    5      �� 5      .R�5   "   �� ��6,   �� *
UserName+5      5      �� 5      $��Полное имя��6&   
 �� Width$

 Auto��6:    5       �� 5!      .R�5"   "   �� ��6,   �� *
FullName+5#      5%      5'      5)      �� 5+   
   -   5        $��
BtnAddUser��5+      $/��)Добавить пользователя��6#   $
��Left=�6C   $��	10,0,0,10q�6^   $	��149��6q   $��22��6�   $��Bottom=�6�   5,      5-      