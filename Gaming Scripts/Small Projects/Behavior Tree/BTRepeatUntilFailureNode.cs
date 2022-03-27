 using UnityEngine;
 using System.Collections;
 
 public class BTRepeatUntilFailureNode : BTDecoratorNode {
 
     public BTRepeatUntilFailureNode(BehaviorTree t, BTNode child) : base(t, child)
     {
     }
 
     public override Result Execute()
     {
         Result result = Child.Execute();
         Debug.Log("Child returned: " + result);
         if(result == Result.Failure){
             return Result.Failure;
         }
         else{
             return Result.Running;
         }
     }
 }