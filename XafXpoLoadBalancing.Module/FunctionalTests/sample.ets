;#DropDB XafXpoLoadBalancingEasyTest

#Application XafXpoLoadBalancingWin
#Application XafXpoLoadBalancingWeb
#IfDef XafXpoLoadBalancingWin

*Action About...
 
*Action OK

#EndIf

#IfDef XafXpoLoadBalancingWeb
!Action Navigation(My Details)
#EndIf
