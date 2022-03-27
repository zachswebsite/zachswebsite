 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 
 public class BTNode {
 
     public enum Result { Running, Failure, Success };
 
     public BehaviorTree Tree { get; set; }
     // create a BTNode with a behavior tree attached to it
     public BTNode(BehaviorTree t)
     {
         Tree = t;
     }
 
     public virtual Result Execute()
     {
         return Result.Failure;
     }
 } 
