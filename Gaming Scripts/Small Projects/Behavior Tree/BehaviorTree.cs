 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 
 public class BehaviorTree : MonoBehaviour {
 
     private BTNode mRoot; 
     private bool startedBehavior;

     // BTNode is the root node, while our Dictionary acts as 
     // the blackboard so that nodes can communicate with each other
     public BTNode Root { get { return mRoot; }}
     public Dictionary<string, object> Blackboard { get; set; }
 
     // Use this for initialization
     void Start () {
         Blackboard = new Dictionary<string, object> ();
         Blackboard.Add ("WorldBounds", new Rect (0, 0, 5, 5));
 
         startedBehavior = false;
         // create a behavior tree manually
         if(this.tag == "player1"){
             //This is the REPEAT_UNTIL_FAILURE_NODE example.
             mRoot = new BTRepeatUntilFailureNode(this, new BTSequencerNode(this, 
             new BTNode[] { new BTRandomWalkNode(this), new BTRandomWalkNode(this), new BTFailNode(this) }));
         }
         else if(this.tag == "player2"){
             //This is the SELECTOR_NODE example
            mRoot = new BTRepeatUntilFailureNode(this, new BTSelectorNode(this, 
            new BTNode[] { new BTRandomWalkNode(this), new BTFailNode(this), new BTRandomWalkNode(this), new BTRandomWalkNode(this), new BTFailNode(this) }));
         }
     }
     
     // Update is called once per frame
     void Update () {
         if (!startedBehavior) {
             StartCoroutine (RunBehavior ());
             startedBehavior = true;
         }
     }
 
     IEnumerator RunBehavior() 
     { 
         BTNode.Result result = Root.Execute ();
         while (result == BTNode.Result.Running) {
             Debug.Log ("Root result: " + result);
             yield return null;
             result = Root.Execute ();
         }
         Debug.Log ("Behavior has finished: " + result);
     }
 } 