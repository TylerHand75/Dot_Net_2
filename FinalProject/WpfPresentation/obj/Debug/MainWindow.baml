   M S B A M L   `   `   `  ????J  FWpfPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfPresentationWpfPresentation  5   	        WpfPresentation.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfPresentation     5   	   -   5          ?Name$  frmMain??5   	    ? Title$ Mncs Valorant ??5   	   $	??800??6    $
??1200??6-    ? WindowStartupLocation$ CenterOwner=?6:   .??5
      ?  ? RowDefinitions 5   
   ?? 5      $??25 ?6   ?? 5      $??70 ?6   ?? 5      $??10 ?6   ?? 5      ?? 5      $??50 ?6   ?? 5      $??35 ?6   .+?5   
   o? $??0??6   $??20, 3q?6   .@?5      m? -   5         m?Name$ mnuFile??5      $
??File??6(   .@?5      m? -   5        $ mnuExit??5      $
??Exit??6,   6<   5      m? 5      -   5        $ mnuEdit??5      $
??Edit??6(   .@?5      m? -   5        $ mnuChangePassword??5      $??Update/Change Passoword??66   6y   m? 5       -   5        $ mnuPreferences??5       $??Preferences??6/   6F   5!      m? 5#      -   5        $ mnuHelp??5#      $
??Help??6(   .@?5$      m? -   5        $ mnuAbout??5$      $??About??6-   6>   5%      5&      ? 5(   
   $??1??6    5)      ?? 5*      ?? 5+       ? ColumnDefinitions 5-      ?? 5.      ?? 5/      $	??250 ?6#   ?? 50      $	??250 ?6#   ?? 51      $	??120 ?6#   .+?53      ?? -	   5         ??Name$ lblGreeting??53      $??You are not logged in.??6)   $??20 5 0 0q?6J   $??5q?6]   ?? 54      -
   5         ??Name$ txtEmail??54      $??5 5 5 0q?6(   $??1??69   $??Center=?6I   $??Center=?6m   G? 55      -   5         G?Name$ txtPassword??55      $??5 5 5 0q?6/   $??2??6@   $??Center=?6P   $??Center=?6t   ?? 56      -   5        	 ??Name$	 btnLogin??56      $??5 5 20 5q?6'   $??3??69   $??Login??6I   6Y   ?? 57      -   5        $ lblEmail??57      $??1??6&   $??1??63   $??Email Address??6C   $??Center=?6[   ?? 58      -   5        $ lblPassword??58      $??1??6)   $??2??66   $??Password??6F   $??Center=?6Y   5;      ?? 5?   
   -   5        
 ??Name$
 pnlTabs??5?      $??3??6&   .+?5@      ? $
??1000??6   .+?5A      ?? -   5         ??Name$ 
tabsetMain??5A      .@?5B      ?? -   5         ??Name$ 
tabMatches??5B      $??Matches Played??62   .??5D      &   System.Windows.Controls.DataGrid  -   5          Name$ 
datMatches??5D   #      
IsReadOnly . $??18(?6I   $??Tahoma)?6W   $??10q?6k   
??? ??6?   5F      ?? 5G      -   5        $ 
tabPlayers??5G      $??Players in League??62   .??5H        -   5        $ 
datPlayers??5H   #    . $??18(?6I   $??Tahoma)?6W   $??10q?6k   
??? ??6w   5I      ?? 5J      -   5        $ tabTeams??5J      $??Teams??60   .??5K        -   5        $ datTeams??5K   #    . $??18(?6G   $??Tahoma)?6U   $??10q?6i   
??? ??6u   5L      ?? 5M      -   5        $ 	tabAgents??5M      $??Agents??61   .??5N        -   5        $ 	datAgents??5N   #    . $??18(?6H   $??Tahoma)?6V   $??10q?6j   
??? ??6v   5O      ?? 5P      -   5        $ tabStats??5P      $??Stats of Players??60   .??5R        -   5        $ datStats??5R   #    . $??18(?6G   $??Tahoma)?6U   $??10q?6i   
??? ??6?   5T      5U      5V      5W      ?? 5[   
   $??5??6   .@?5\      ?? -   5         ??Name$ statMessage??5\      $
??20 0q?61   .??6@   '%Welcome. Please log in to continue...5^      5_      5a      5b      