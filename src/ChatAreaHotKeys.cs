using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatAreaHotKeys : MonoBehaviour
{
    
    public List<KeyCode> openChat=new List<KeyCode>(){KeyCode.LeftControl, KeyCode.T};
    public List<KeyCode> closeChat=new List<KeyCode>(){KeyCode.LeftControl, KeyCode.T};
    public PanelOpener opener;

    void Start(){

        if(opener==null){
            opener=gameObject.GetComponent<PanelOpener>();
        }

    }
    void Update()
    {

        if(opener.open){
            if(CheckKeys(closeChat)){
                opener.Close();
            }
        }else{
            if(CheckKeys(openChat)){
                opener.Open();
            }
        }
        

        

    }

    bool CheckKeys(List<KeyCode> codes){
        foreach(KeyCode code in codes){
             if(!Input.GetKey(code)){
                return false;
             }
        }
        Debug.Log("trigger");
        return true;
    }
}
