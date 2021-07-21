using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json.Linq;

public class PushChatChannel : MonoBehaviour
{


    public string channel="levelChat";
    public string eventType="message";

    PushSocketIOClient client=null;


    void Update()
    {

        if(client==null){
            client=PushSocketIOClient.Client;
            if(client!=null){
                PushSocketIOClient.Client.Subscribe(channel, eventType, delegate(JToken[] data){



                });

            }
        }

    }



    public void SendText(string text){
        if(client!=null&&client.authenticated){
            client.Send(channel, eventType, Message(text));
        }
    }


    JObject Message(string message){
        return new JObject(
            new JProperty("message", message)
        );
    }


}
