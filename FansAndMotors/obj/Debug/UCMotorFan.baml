   M S B A M L   `   `   `  ����P  LDataGridFilterLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\Bclr-namespace:DataGridFilterLibrary;assembly=DataGridFilterLibraryDataGridFilterLibrary  5       FansAndMotors   FansAndMotors.UcMotorFan   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      UfilterBclr-namespace:DataGridFilterLibrary;assembly=DataGridFilterLibrary     5      -   5          >�Name$  UcMotorFanForm��5	       � 	Resources 5      )  #System.Windows.Controls.DataGridRow �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   '        �� 5   
   "  6    5      P� 5       �@J�IsMouseOver . . �5      ��  � ��
 ��� �63   �� 5       � ��
 �����63   5      5      5      .��5      � -   5         � ColumnDefinitions 5    
   �� 5!      $	��300 �6   �� 5"      $��* �6   	 � RowDefinitions	 5$   
   �� 5%      $	��250 �6   �� 5&      $��30 �6   �� 5'      $��* �6   .+�5)   
   �� $��2��6   $��Images\MA-Blauberg.png��6!   $
��Left=�6A   $��50,0,0,0q�6\   � 5+   
   $��0��6   $��1��6   	 5,      �� 5-      $��30 �6    �� 5.      $
��111* �6    .+�51      �� $��0��6   
 �� Orientation$
 
Horizontal=�6&   $
��Left=�6?   .+�52      �� $	����6   $��Webdings)�6+   ���	#FF2B259B$��18(�6X   $��0,3,-5,0q�6f   $��26��6x   �� 53      $��Тип изделия��6   
���   �$��16(�6A   54      �� 55      $��1��6   $
 Vertical=�6&   $��9,0,0,0q�6=   .+�56      	� -   5        $��RbMotor��56      $��
Мотор��6-   $��16(�6=    	� 	GroupName$
 Type��6K   6\   	� 57      -   5        $��RbFan��57      $��Вентилятор��6+   $��16(�6@   $
 Type��6N   6_   58      59      �� 5;   
   $��1��6   $
 
Horizontal=�6"   $��2��6;   $��0��6O   .+�5<      �� $	����6   $��Webdings)�6'   ���	#FF2B259B$��18(�6T   $��0,3,-5,0q�6b   $��26��6t   �� 5=      $9��3Технические характеристики��6   
���   �$��16(�6L   �� 5?      -   5        $��BtnSaveEdit��5?      $��Сохранить��6(   $��76��6<   6G   �� 5A      -   5        $��BtnRefreshFilter��5A      $#��Сбросить фильтр��6-   $	��121��6G   ��5B      ��  �� Path$4 .(filter:DataGridExtensions.ClearFilterCommand)� �� ElementName$ 	DgCatalog��5C      5D      &   System.Windows.Controls.DataGrid  5F   
   -   5          Name$ 	DgCatalog��5F         AutoGenerateColumns .  $��2��5G      $��2��6   $��0��62   $��15(�5H         HorizontalGridLinesBrush �	#FF0B4EA2   VerticalGridLinesBrush �	#FF0B4EA2   CanUserAddRows .  .   (DataGridFilterLibrary.DataGridExtensions&   UseBackgroundWorkerForFiltering .    IsClearButtonVisible .     IsFilterVisible 5M      �� $ 	IsChecked�$ ShowHideFilterCheckBox�� �� Mode$ TwoWay=�   ColumnHeaderStyle 5Q   "   �� *��  �� TypeInTargetAssembly7   1DataGridFilterLibrary.DataGridHeaderFilterControl"   �� 
ResourceId$&  DataGridHeaderFilterControlStyle��+5U      5V      5a      