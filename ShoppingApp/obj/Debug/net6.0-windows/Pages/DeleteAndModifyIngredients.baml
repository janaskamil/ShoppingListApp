   M S B A M L   `   `   `  ����F  BShoppingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ShoppingAppShoppingApp  5      )    #ShoppingApp.DeleteUnusedIngredients   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:ShoppingApp     5      .��5	      �   � RowDefinitions  5
   
   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      $	��500 �6   6-   �� 5      $��1 �6   6+   �� 5      $	��500 �6   6-   .+�5   
   %   ShoppingApp.IngredientsToDelete  $��0��6$   $��0��61   5      �� 5   
   $��1��6   $��0��6   $	��100��6.   $	��100��6;   
���  ����6X   �� *DeleteIngredients+5      %   ShoppingApp.IngredientsToModify  5   
   $��0��6$   $��2��61   5      �� 5   
   $��1��6   $��2��6   $	��100��6.   $	��100��6;   
��� � ���6Z   �� *UpdateIngredient+5      5      5      