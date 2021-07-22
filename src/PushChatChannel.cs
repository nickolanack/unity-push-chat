using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using Newtonsoft.Json.Linq;

public class PushChatChannel : MonoBehaviour
{


    public string channel="levelChat";
    public string eventType="message";

    PushSocketIOClient client=null;


    public GameObject messageArea;
    public GameObject messageTemplate;

    public GameObject lastMessage;
    public float padY=10;


    public Queue<JToken[]> messageQueue=new Queue<JToken[]>();

    void Start(){
        if(messageArea==null){
            messageArea=gameObject;

        }

        messageTemplate.SetActive(false);
    }


    void Update()
    {

        if(client==null){
            client=PushSocketIOClient.Client;
            if(client!=null){
                PushSocketIOClient.Client.Subscribe(channel, eventType, delegate(JToken[] data){

                    messageQueue.Enqueue(data);

                });

            }
        }



        while(messageQueue.Count>0){

                    JToken[] data=messageQueue.Dequeue();


                    GameObject messageObject=(GameObject)Instantiate(messageTemplate, gameObject.transform);

                    ChatMessage message=messageObject.GetComponent<ChatMessage>();
                    if(message==null){
                        message=messageObject.AddComponent<ChatMessage>();
                    }
                    message.SetText(data[0]["message"].ToObject<string>());

                    

                    if(lastMessage!=null){

                        RectTransform rect=messageObject.GetComponent<RectTransform>();
                        RectTransform lastRect=lastMessage.GetComponent<RectTransform>();

                        Vector2 targetPosition=rect.anchoredPosition;
                        targetPosition.y=lastRect.anchoredPosition.y-(lastRect.rect.height+padY);

                        rect.anchoredPosition=targetPosition;

                    }

                    lastMessage=messageObject;
                    messageObject.SetActive(true);

                   
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
