using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatAreaHotKeys : MonoBehaviour
{
    
    public List<KeyCode> openChat=new List<KeyCode>(){KeyCode.LeftControl, KeyCode.T};
    public List<KeyCode> closeChat=new List<KeyCode>(){KeyCode.LeftControl, KeyCode.T};
    public PanelOpener chatArea;

    void Start(){

        if(chatArea==null){
            chatArea=gameObject.GetComponent<PanelOpener>();
        }

    }
    void Update()
    {

        if(chatArea.open){
            if(CheckKeys(closeChat)){
                chatArea.Close();
            }
        }else{
            if(CheckKeys(openChat)){
                chatArea.Open();
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
