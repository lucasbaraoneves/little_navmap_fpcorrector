   M S B A M L   `   `   `  ����M  ILittleNavMap_Fixer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:LittleNavMap_FixerLittleNavMap_Fixer  5   	   M+clr-namespace:LittleNavMap_Fixer.ViewModelsLittleNavMap_Fixer.ViewModels  5   	   #    LittleNavMap_Fixer.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:LittleNavMap_Fixer     5   	   B
viewmodels+clr-namespace:LittleNavMap_Fixer.ViewModels     5   	     � 
ResizeMode$  NoResize=�5	   	   ���#262626 � WindowStartupLocation$ CenterScreen=�5   	    � Title$! Litte Navmap corrector v1.0��5   	   $	��244��6-   $	��453��6:    � 	Resources 5       �� 
TargetType �� Property �� Value��%X   '��       �� 5   
   " ��6   .�5      ��  � �� �#F2F2F26+   5      5       � DataContext 5      7   1LittleNavMap_Fixer.ViewModels.MainWindowViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $.��(Little Navmap exported FP corrector v1.0��6   $��5q�6D   $��Center=�5      $��Center=�62   $��20(�6M   $
��Bold"�6[   $��0��6m   �� 5    
   $��1��6   $��Center=�6"   $��5q�6=   $��Center=�6H   .+�5!      �� .+�5"      �� $0��*Select MSFS exported flight plan directory��6   �� 5#      	 �� Orientation$	 
Horizontal=�6   $��1��66   .+�5$      �� -   5        
 ��Name$
 txtFilesPath��5$      $	��380��64    �� 
IsReadOnly$
 True��6@   ��6R   �� *
MsfsFolder+�� 5%      -   5         ��Name$ btnSelectDirectory��5%      $	��...��69   $��30��6G   ��6R   �� *FolderSelectCommand+5&      5'      �� 5(      $��10��6   �� 5)      $&�� Or select a msfs single PLN file��6   �� 5*      $	 
Horizontal=�6   $��1��62   .+�5+      �� $	��380��6   $
 True��6&   ��68   �� *MsfsFlightPlanFile+�� 5,      $	��...��6   $��30��6'   ��62   �� *FileSelectCommand+5-      �� 5.      $��5��6   �� 5/      $��Override original files��6   ���#F2F2F2 d� 	IsChecked 69   �� *OverrideOriginals+6a   50      �� 52   
   $��2��6   $	��100��6   $��Apply correction��6*   ��6E   �� *ApplyCorrectionCommand+�� 54   
   $��3��6   $��5q�6"   .+�55      �� ��6   �� *TotalFixedFiles+56      5:      5;      