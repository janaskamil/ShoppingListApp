   M S B A M L   `   `   `  ����F  BShoppingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ShoppingAppShoppingApp  5      &     ShoppingApp.CheckAndGenerateList   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:ShoppingApp     5      .��5      �   � ColumnDefinitions  5	   
   �� 5
      $��* �6   6+   �� 5      $��3* �6   6,   �� 5      $��* �6   6+   �� 5      $��3* �6   6,   �� 5      $��* �6   6+    � RowDefinitions 5   
   �� 5      $��5* �6   6*   �� 5      $��* �6   6)   .+�5   
   (   "ShoppingApp.ListOfIngredientsToBuy  $��0��6'   $��1��64   $	��800��6D   $	��480��6Q   5      �� 5   
   $��1��6   $��1��6#    �� Orientation$ 
Horizontal=�63   .+�5      �� $��80��6   $	��100��6!   
��� � �$��Center=�6@   $��Center=�6[   ��6x   �� *	CreateXls+6�   �� 5      $��80��6   $	��100��6!   
���  ��$��Center=�6>   $��Center=�6Y   ��6v   �� **(DeleteShoppingListWithIngredientsCommand+6�   5      %   ShoppingApp.ItemsToAddAndModify  5   
   $��0��6$   $��3��61   5      �� 5   
   $��1��6   $��3��6#   $ 
Horizontal=�63   .+�5      �� $��50��6   $��50��6!   
���"�"���6E   �� *
AddItems+6d   �� 5      $��50��6   $��50��6!   
����  ���6>   �� *	SaveItems+6^   �� 5      $��50��6   $��50��6!   
���\\����6C   �� *DeleteItems+6e   5       5!      5"      