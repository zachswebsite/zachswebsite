using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFailNode : BTNode {
 
     
 
     public BTFailNode(BehaviorTree tree) : base(tree)
     {
         
     }
 
     public override Result Execute ()
     {
         // if we've made it to the destination
         return Result.Failure;
             
     }
 }