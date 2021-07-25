using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageChannel : MonoBehaviour
{


    public GameObject messageArea;
    public GameObject messageTemplate;

    public GameObject lastMessage;
    public float padY=10;

    public delegate void MessageEvent(Message[] messages);
    public List<MessageEvent> onRecievedMessage=new List<MessageEvent>();
    public List<MessageEvent> onSentMessage=new List<MessageEvent>();

    public delegate bool MessageFilter(Message message);
    public List<MessageFilter> recieveFilter=new List<MessageFilter>();
    public List<MessageFilter> sendFilter=new List<MessageFilter>();

    protected Queue<Message> recievedQueue=new Queue<Message>();
    protected Queue<Message> sendQueue=new Queue<Message>();

    public enum Type{ Plain, System };

    public class Message{
        public string text;
        public Type type=Type.Plain;
    }


    // Start is called before the first frame update
    void Start()
    {
        if(messageArea==null){
            messageArea=gameObject;
        }
        messageTemplate.SetActive(false);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(recievedQueue.Count>0){
            NotifyRecieve(recievedQueue.ToArray());
        }


        if(sendQueue.Count>0){
            NotifySend(sendQueue.ToArray());
        }
        

        while(recievedQueue.Count>0){
            AddMessage(recievedQueue.Dequeue());
        }

    }

    protected void NotifySend(Message[] messages){
        foreach(MessageEvent listener in onSentMessage){
            listener(messages);
        }
    }

    protected void NotifyRecieve(Message[] messages){
        foreach(MessageEvent listener in onRecievedMessage){
            listener(messages);
        }
    }

    protected void AddMessage(string text){
        Message m = new Message();
        m.text=text;
        AddMessage(m);
    }



    protected void AddMessage(Message text){
        DisplayMessage(text);
    }

    /*
     * push message into the queue and handles it like a received message
     * ie: plays audio and other new message notifications
     */
    public void QueueRecievedMessage(Message message){

        foreach(MessageFilter filter in recieveFilter){
            if(!filter(message)){
                Debug.Log("Filter out receive");
                return;
            }
        }

        recievedQueue.Enqueue(message);
    }


    public void SendText(string text){

        MessageChannel.Message message=new MessageChannel.Message();
        message.text=text;

        foreach(MessageFilter filter in sendFilter){
            if(!filter(message)){
                Debug.Log("Filter out send");
                return;
            }
        }


        sendQueue.Enqueue(message);
        // if(client!=null&&client.authenticated){
        //     client.Send(channel, eventType, EncodeMessage(text));
        //     NotifySend(new MessageChannel.Message[]{m});
        // }
    }


    /*
     * display a message
     */
    public void DisplayMessage(Message text){


        GameObject messageObject=(GameObject)Instantiate(messageTemplate, gameObject.transform);

        ChatMessage message=messageObject.GetComponent<ChatMessage>();
        if(message==null){
            message=messageObject.AddComponent<ChatMessage>();
        }
        message.SetText(text);


        if(lastMessage!=null){

            SimpleStack.StackVertical(lastMessage, messageObject, padY);

            // RectTransform rect=messageObject.GetComponent<RectTransform>();
            // RectTransform lastRect=lastMessage.GetComponent<RectTransform>();

            // Vector2 targetPosition=rect.anchoredPosition;
            // targetPosition.y=lastRect.anchoredPosition.y-(lastRect.rect.height+padY);

            // rect.anchoredPosition=targetPosition;

        }

        lastMessage=messageObject;
        messageObject.SetActive(true);

    }

}
