using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Newtonsoft.Json.Linq;

public class PushChatChannel : MessageChannel
{


    public string channel="levelChat";
    public string eventType="message";

    PushSocketIOClient client=null;



    protected override void Update()
    {

        if(client==null){
            client=PushSocketIOClient.Client;
            if(client!=null){
                PushSocketIOClient.Client.Subscribe(channel, eventType, delegate(JToken[] data){

                    MessageChannel.Message m=new MessageChannel.Message();
                    m.text=DecodeMessage(data);
                    QueueRecievedMessage(m);

                });
            }
        }

        
        
        base.Update();

        while(sendQueue.Count>0){
            if(client!=null&&client.authenticated){
                client.Send(channel, eventType, EncodeMessage(sendQueue.Dequeue().text));
               // NotifySend(new MessageChannel.Message[]{m});
            }
        }

    }



    


    JObject EncodeMessage(string message){
        return new JObject(
            new JProperty("message", message)
        );
    }

    string DecodeMessage(JToken[] data){
        return data[0]["message"].ToObject<string>();
    }


}
