using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepData : MonoBehaviour
{
    // Start is called before the first frame update
    private static keepData instanceVar;
    
    public void Awake () {
        

    if (instanceVar == null) {
         instanceVar = this;
         DontDestroyOnLoad(this);
     } else {
        //Destroy(this.gameObject);
     }
    }
    

    // Update is called once per frame
   
}
