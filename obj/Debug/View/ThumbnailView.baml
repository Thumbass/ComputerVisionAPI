   M S B A M L   `   `   `  ����L  HComputerVisionAPI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:ComputerVisionAPI.ViewComputerVisionAPI.View  5      *    $ComputerVisionAPI.View.ThumbnailView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:ComputerVisionAPI.View     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��0��6,   .+�5      �� $#��Browse and generate thumbnail��6   $��5, 10, 5, 10q�5      $��20��6+   $	��180��67   $
��left=�6C   ��5   =   �� */-ThumbnailVm.BrowseAndGenerateThumbnailCommand+5   C   �� 5      $��Uniform=�6   $	��475��6Q   $	��395��6^   ��6&   �� *ThumbnailVm.ImageSource+6^   5      �� 5   
   $ Vertical=�6   $��1��6,   .+�5      �� $��
Thumbnail:��6   $
��Bold"�6*   $��15(�6<   $
��5, 5q�6J   $��20��6X   �� 5      $
��None=�6   $��
5, 0, 5, 5q�6L   $	��250��6`   $	��250��6m   ��6#   �� *ThumbnailVm.Thumbnail+6m   5      5      5      