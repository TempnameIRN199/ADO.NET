   M S B A M L   `   `   `  ����@  <lab 8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:lab_8lab_8  5   	       lab_8.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:lab_8     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � -   5         � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��35 �6   .+�5   
   �� $��0��6   .@�5      �� -   5        $��DepartmentsTabItem��5   +   $��Departments��6   .��5      �� -   5        $��DepartmentsListView��5      ��66   �� *Departments+ �� View 5      �� 5      .P�5      �� $��Id��6-    �� Width$
 Auto��69    �� DisplayMemberBinding 6F   �� *Id+�� 5      $
��Name��6-   $
 Auto��6;    6H   �� *Name+5      5      5      �� 5      -   5        $��FormsTabItem��5   %   $��Forms��6   .��5      �� -   5        $��FormsListView��5      ��60   �� *Forms+ 5      �� 5      .P�5      �� $��Id��6-   $
 Auto��69    6F   �� *Id+�� 5       $
��Name��6-   $
 Auto��6;    6H   �� *Name+5!      5#      5$      �� 5%      -   5        $��GroupsTabItem��5%   &   $��Groups��6   .��5&      �� -   5        $��GroupsListView��5&      ��61   �� *Groups+ 5'      �� 5(      .P�5)      �� $��Id��6-   $
 Auto��69    6F   �� *Id+�� 5*      $
��Name��6-   $
 Auto��6;    6H   �� *Name+�� 5+      $��DepartmentId��6-   $
 Auto��6C    6P   �� *DepartmentId+�� 5,      $��FormId��6-   $
 Auto��6=    6J   �� *FormId+5-      5/      50      �� 51      -   5        $��StudentsTabItem��51   (   $��Students��6   .��52      �� -	   5        $��StudentsListView��52      ��63   �� *
Students+ 53      �� 54      .P�55      �� $��Id��6-   $
 Auto��69    6F   �� *Id+�� 56      $
��Name��6-   $
 Auto��6;    6H   �� *Name+�� 57      $��GroupId��6-   $
 Auto��6>    6K   �� *	GroupId+�� 58      $��Email��6-   $
 Auto��6<    6I   �� *Email+59      5;      5<      5=      �� 5?   
   $��1��6    �� Orientation$ 
Horizontal=�6"   .+�5@      �� -
   5        $	��Add��5@      $	��100��6#   $��5q�6/   6:   �� 5A      -   5        $
��Edit��5A      $	��100��6$   $��5q�60   6;   �� 5B      -   5        $��Delete��5B      $	��100��6&   $��5q�62   6=   5C      5D      5E      