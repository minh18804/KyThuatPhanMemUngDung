   M S B A M L   `   `   `  ����?  ;Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TestTest  5   	       Test.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Test     5   	     � Title$  Đăng nhập��5   	   $	��450��6   $	��800��6(   
�������.��5	      � ��5
   
   �� 5   
    �� ImageSource$ images/background.jpg��6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5       $��* �6   �� 5!      $��* �6   �� 5"      $��* �6   �� 5#      $��* �6   �� 5$      $��* �6   �� 5%      $��* �6   �� 5&      $��* �6   �� 5'      $��* �6   �� 5(      $��* �6   �� 5)      $��* �6   �� 5*      $��* �6   .+�5-      �� $)��#Hệ thống quản lý chăn nuôi��6   $��28(�5.      
�������$
��Bold"�50      $��5��51      $��2��52      $��0��53      $��8��54      $��Center=�55      $��10q�56      �� 58      $��1��6   $��7��59      $��7��5:      $��3��5;      $	��Top=�5<      $��Right=�5=      $��10q�5>      .+�5?      �� $��Quyền truy cập��6   $
��Bold"�5@      $��0,0,0,10q�5A      	� 5B      -   5         	�Name$ admin��5B      $��Admin��5C       	� 	GroupName$ Roles��5D       d� 	IsChecked$
 TrueY�5E      $��	0,10,0,10q�5F      	� 5G      -   5        $ capHuyen��5G      $��Cấp huyện��5H      $ Roles��5I      $��	0,10,0,10q�5J      	� 5K      -   5        $ capXa��5K      $��	Cấp xã��5L      $ Roles��5M      $��	0,10,0,10q�5N      	� 5O      -   5        $ congTy��5O      $��Công ty��5P      $ Roles��5Q      $��	0,10,0,10q�5R      5S      �� 5U   
   $��1��6   $��10��5V      $��10��5W      $��3��5X      $	��300��5Y      $	��500��5Z      $��30��5[      $	��300��5\      $	��Top=�5]      $��Stretch=�5^      .+�5_      �� $��Tên đăng nhập:��6   $��14(�5`      �� 5a      -   5         ��Name$ Username��5a      
�������$��16(�5d      $��24��5e      $	��300��5f      $��30��5g      $	��150��5h      $	��100��5i      �� 5j      $��Mật khẩu:��6   $��14(�5k      G� 5l      -   5         G�Name$ Password��5l      
�������$��24��5n      $��16(�5o      $	��300��5q      $	��100��5r      $��30��5s      $	��150��5t      �� 5u      -   5        	 ��Name$	 login��5u      $��Đăng nhập��5v      $��14(�5x      $
��Bold"�5y      
�������$��0q�5{      $��32��5|      $��30��5}      $	��150��5~      $��10q�5      $	��150��5�      $��80��5�      ��5�      �� 5�      
 �� 
TargetType"
 ��6&   .R�5�      �� #��z��6!   #��z��6M    �� CornerRadius$ 15��6�   ��5�      �� 5�   "    �� Color$ Green��62    �� Opacity$	 0.7Q�6@   ._�5�      �� $��Center=�6/   $��Center=�6L   #��z��6g   #��z��6�   5�      5�      5�      �� 5�      -   5        $	 register��5�      $��
Đăng ký��5�      $��14(�5�      $
��Bold"�5�      
�������$��0q�5�      $��32��5�       J� Opacity$ 1Q�5�      $��30��5�      $	��150��5�      $	��150��5�      $��80��5�      ��5�      �� 5�      "
 ��6&   .R�5�      �� #��z��6!   #��z��6M   $ 15��6�   ��5�      �� 5�      $ Orange��6.   $	 0.7Q�6=   ._�5�      �� $��Center=�6/   $��Center=�6L   #��z��6g   #��z��6�   5�      5�      5�      �� 5�      -	   5        $	 forgotPassword��5�      $��Quên mật khẩu?��5�      $��12(�5�      
����  �
�������$��0q�5�      $��0,10,0,0q�5�      ��5�      �� 5�      "
 ��6&   .R�5�      �� #��z��6!   #��z��6M   $ 15��6�   ._�5�      �� $��Center=�6/   $��Center=�6L   5�      5�      5�      5�      �� 5�      $��Trang đăng nhập��6   $
��Bold"�5�      $��21(�5�      $��0��5�      $��8��5�      $��4��5�      $	��Top=�5�      $��Right=�5�      $��10q�5�      5�      5�      