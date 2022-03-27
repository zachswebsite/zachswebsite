using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle tog1;
    public Toggle tog2;
    public Toggle tog3;
    public void SetText(Object obj){
        Text txtToChange = transform.Find("Text").GetComponent<Text>();
        Text txt = ((GameObject)obj).GetComponent<Text>();
        txtToChange.text = txt.text;
        if(tog1.isOn){
            //red
            GetComponent<Image>().color = Color.red;
        }else{
            if(tog2.isOn){
                //green
                GetComponent<Image>().color = Color.green;
            }
            else{
                if(tog3.isOn){
                    //blue
                    GetComponent<Image>().color = Color.blue;
                }
            }
        }
        
    }
    
}
