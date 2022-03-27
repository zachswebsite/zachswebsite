using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelectorNode : BTCompositeNode
{
    // variable to keep track of current node
     private int currentNode = 0; 
 
     public BTSelectorNode(BehaviorTree tree, BTNode [] children) 
         : base(tree, children)
     {
     }
 
     public override Result Execute ()
     {
         if (currentNode < Children.Count) {
             // store the result for the following tests
             Result result = Children [currentNode].Execute ();
             if (result == Result.Running)
                 return Result.Running;
             else if (result == Result.Success) {
                 //RETURN SUCCESS IMMEDIATELY
                 currentNode = 0;
                 return Result.Success;
             } else {
                 // result is Result.Failure
                 //only return if ALL are failures, so pass unless at end
                 currentNode++;
                 if (currentNode < Children.Count)
                     return Result.Running;
                 else {
                     //this will only hit if only failures have hit the selector.
                     currentNode = 0;
                     return Result.Failure;
                 }
             }
         }
         
         return Result.Failure;
     }
 }