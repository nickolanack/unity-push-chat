using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemCommandFilter : MonoBehaviour
{


    public string commandPrefix="/";

    void Start(){



        gameObject.GetComponent<MessageChannel>().sendFilter.Add(delegate(MessageChannel.Message message){


            //foreach(MessageChannel.Message message in messages){
                string text=message.text;
                if(message.type==MessageChannel.Type.Plain&&text.StartsWith(commandPrefix)){
                    gameObject.GetComponent<MessageChannel>().QueueRecievedMessage(message);
                    StartCoroutine(HandleCommand(text.Substring(1)));
                    return false;
                }
                return true;
                

           // }

        });

    }

    GameObject GetPlayer(){
        return Camera.main.gameObject;
    }

    void QueueResponse(string text){

        MessageChannel.Message response=new MessageChannel.Message(); 
        response.type=MessageChannel.Type.System;
        response.text=text;
        gameObject.GetComponent<MessageChannel>().QueueRecievedMessage(response);


    }


    IEnumerator HandleCommand(string text){

        yield return new WaitForSeconds(0.3f);

        QueueResponse(ProcessCommand(text));
        
    }

    string ProcessCommand(string text){


        
        if(text.Equals("hello")){
            return "Hello "+GetPlayer().name;
            
        }

        if(text.Equals("position")){
            return GetPlayer().transform.position.ToString();
        }


        if(text.Equals("rotation")){
            return GetPlayer().transform.eulerAngles.ToString();
            
        }

        if(text.Equals("uid")){
            return SystemInfo.deviceUniqueIdentifier+"-"+Application.platform;
            
        }
        
        return "Unknown: "+text.Substring(1);        

    }

}
