   M S B A M L   `   `   `  ����J  FOnlineBookShelf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:OnlineBookShelfOnlineBookShelf  5   	        OnlineBookShelf.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:OnlineBookShelf     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      �    GradientBackground#��[  5      .+�5   
   �� -   5         ��Name$ lbItems��5   
    �� DisplayMemberPath$
 Name��5   
   $	��250��5   
   $��50,92,470,92q�6   �� 5   
   $��Choose directory:��6   $
��Left=�6,   $��	50,43,0,0q�6G   $	��Top=�6Z    J� RenderTransformOrigin$ -1.842,0.579&�6r   �� 5   
   -   5         ��Name$ cbDirectories��5   
    �� 
IsEditable$
 True��5   
   $
��Left=�5   
   $��
180,45,0,0q�6%   $	��Top=�69   $	��150��6Q   �� 5   
   -   5         ��Name$ 	btnUpload��5   
   $��Upload��5   
   ���#4D8A7C$
��Left=�5   
   $��
50,364,0,0q�6%   $	��Top=�69   $��70��6Q   �� 5   
   -   5        $ btnDownload��5   
   $��Download��5    
   ���#6C8D53$
��Left=�5#   
   $��155,364,0,0q�6%   $	��Top=�6:   $��70��6R   �� 5$   
   -   5        $ btnDownloadAll��5%   	   $��Download All��5&   	   ���#5A71D1$
��Left=�5)   	   $��365,364,0,0q�6$   $	��Top=�69   $	��100��6Q   �� 5*   
   -   5        $ 	btnDelete��5+   
   $��Delete��5,   
   ���#8F1D2C$
��Left=�5/   
   $��260,364,0,0q�6%   $	��Top=�6:   $��70��6R   �� 50   
   $��Content type:��51   
   $
��Left=�52   
   $��
385,92,0,0q�6%   $	��Top=�69   �� 53   
    �� 
IsReadOnly$
 True��55   
   $
��Left=�56   
   $��
525,96,0,0q�6%   	 �� TextWrapping$
	 Wrap=�69   $	��Top=�6M   $	��210��6e   ��54   
   �� *Properties.ContentType+
 �� Mode$
 OneWay=�56   e   �� 57   
   $��Source size:��58   
   $
��Left=�59   
   $��385,147,0,0q�6%   $	��Top=�6:   �� 5:   
   $
 True��5<   
   $
��Left=�5=   
   $��525,151,0,0q�6%   $
	 Wrap=�6:   $	��Top=�6N   $	��210��6f   ��5;   
   �� *Properties.ContentLength+$
 OneWay=�5=   f   �� 5>   
   $��Date Created:��5?   
   $
��Left=�5@   
   $��385,201,0,0q�6%   $	��Top=�6:   �� 5A   
   $
 True��5C   
   $
��Left=�5D   
   $��525,205,0,0q�6%   $
	 Wrap=�6:   $	��Top=�6N   $	��210��6f   ��5B   
   �� *Properties.CreatedOn+$
 OneWay=�5D   f   �� 5E   
   $��Last modified:��5F   
   $
��Left=�5G   
   $��385,256,0,0q�6%   $	��Top=�6:   �� 5H   
   $
 True��5J   
   $
��Left=�5K   
   $��525,260,0,0q�6%   $
	 Wrap=�6:   $	��Top=�6N   $	��210��6f   ��5I   
   �� *Properties.LastModified+$
 OneWay=�5K   f   �� 5L   
   $��Server encrypted:��5M   
   $
��Left=�5N   
   $��385,312,0,0q�6%   $	��Top=�6:   �� 5O   
   $
 True��5Q   
   $
��Left=�5R   
   $��525,316,0,0q�6%   $
	 Wrap=�6:   $	��Top=�6N   $��55��6f   ��5P   
   �� *Properties.ServerEncrypted+$
 OneWay=�5R   f   5U      5V      