   M S B A M L   `   `   `  ????J  FWpfPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfPresentationWpfPresentation  5   	   *    $WpfPresentation.UpdatePasswordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfPresentation     5   	   -   5          ? Title$  Update Password??5   	   $	??400??6!   $	??460??6.    ? WindowStartupLocation$ CenterOwner=?6:    ? 
ResizeMode$ NoResize=?6^   .??5	      ?  ? RowDefinitions 5   
   ?? 5      ?? 5      ?? 5      ?? 5      ?? 5      ?? 5       ? ColumnDefinitions 5   
   ?? 5      $	??160 ?6   ?? 5      .+?5   
   ?? -   5         ??Name$ txtInstructions??5      $??0??6-   $??2??6:   $
??Wrap=?6N    ?? TextAlignment$ Center=?6b   $
??50 5q?6y   $
??Bold"?6?   $??Yu Gothic UI)?5      B@This text is in a column span. It stretches across columns. WOW.5      ?? 5   
   -   5         ??Name$ lblEmail??5      $??1??6"   $??	10 5 20 5q?6/   $??Center=?6B   $??Right=?6d   .??6?   Email:6?   ?? 5   
   -   5        $ 
lblOldPass??5      $??2??6$   $??	10 5 20 5q?61   $??Center=?6D   $??Right=?6f   .??6?   Old Password:6?   ?? 5    
   -   5        $ 
lblNewPass??5       $??3??6$   $??	10 5 20 5q?61   $??Center=?6D   $??Right=?6f   .??6?   New Password:6?   ?? 5!   
   -   5        $ lblConfirmPass??5!      $??4??6(   $??	10 5 20 5q?65   $??Center=?6H   $??Right=?6j   .??6?   Confirm Password:6?   ?? 5#   
   -   5         ??Name$ txtEmail??5#      $??1??6$   $??1??61   $??	10 5 20 5q?6A   $??Center=?6T   $??Center=?6v   G? 5$   
   -   5        	 G?Name$	 
txtOldPass??5$      $??2??6*   $??1??67   $??	10 5 20 5q?6G   $??Center=?6Z   $??Center=?6|   G? 5%   
   -	   5        $	 
txtNewPass??5%      $??3??6*   $??1??67   $??	10 5 20 5q?6G   $??Center=?6Z   $??Center=?6|   G? 5&   
   -
   5        $	 txtConfirmPass??5&      $??4??6.   $??1??6;   $??	10 5 20 5q?6K   $??Center=?6^   $??Center=?6?   ?? 5(   
   -   5        
 ??Name$
 	btnSubmit??5(      $??5??6$   $??0??61   $??
10 5 10 10q?6A   $
??Save??6U   $??Right=?6d   $	??100??6?   6?   ?? 5)   
   -   5        $
 	btnCancel??5)      $??5??6$   $??1??61   $??
10 5 10 10q?6A   $??Cancel??6U   $	??100??6f   $
??Left=?6r   6?   5*      5+      