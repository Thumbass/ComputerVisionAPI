   M S B A M L   `   `   `  ����L  HComputerVisionAPI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:ComputerVisionAPI.ViewComputerVisionAPI.View  5      *    $ComputerVisionAPI.View.CelebrityView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:ComputerVisionAPI.View     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��0��6,   .+�5      � $��5q�6    � RowDefinitions 5      �� 5      $��30 �6$   �� 5      $��30 �6$     5      �� 5      $��* �6'   �� 5      $	��130 �6'   .+�5      �� $��
Image URL:��6   $��0��6-   $��0��6:   $
��Bold"�6J   �� 5      $��30��6M   $��0��6Y   $��1��6i   ��6   �� *CelebrityVm.ImageUrl+ �� Mode$ TwoWay=�6i   �� 5      $��Browse and analyze��6   $��1��5      $��1��6-   $	��120��6:   $
��Left=�6F   ��5   6   �� *)'CelebrityVm.LoadAndFindCelebrityCommand+5   F   5       �� 5"      $��Uniform=�6   $	��470��6Q   $	��395��6^   ��6&   �� *CelebrityVm.ImageSource+6^   5#      �� 5%   
   $ Vertical=�6   $��1��6,   .+�5&      �� $��
Celebrity:��6   $
��Bold"�6*   $��15(�6<   $
��5, 5q�6J   $��20��6X   �� 5'      $��
5, 0, 5, 5q�6=   $	��485��6Q   ��6   �� *CelebrityVm.Celebrity+6Q   5(      5)      5*      