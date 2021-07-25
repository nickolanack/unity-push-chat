using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatArea : MonoBehaviour
{


    public PushChatChannel activeChat;
    public List<PushChatChannel> rooms=new List<PushChatChannel>();


    public InputField inputField;

    public Button submitBtn;


    public  List<MonoBehaviour> disableMouseControls;


    public delegate void MessageEvent(PushChatChannel chat, MessageChannel.Message[] messages);
    public List<MessageEvent> onRecievedMessage=new List<MessageEvent>();
    public List<MessageEvent> onSentMessage=new List<MessageEvent>();


    // Start is called before the first frame update
    void Start()
    {
      


        if(inputField==null){
           InputField[] inputs=gameObject.GetComponentsInChildren<InputField>();

            if(inputs.Length==1){
                Debug.Log("Using the first input field!");
                inputField=inputs[0];
            }
        }

        if(inputField!=null){

        }

        if(submitBtn!=null){
            submitBtn.onClick.AddListener(delegate(){
                activeChat.SendText(inputField.text);
                inputField.text="";
            });
        }



        /*
         * bubble events with chat
         */
        foreach(PushChatChannel chat in rooms){
            chat.onRecievedMessage.Add(delegate (MessageChannel.Message[] messages){
                foreach(MessageEvent listener in onRecievedMessage){
                    listener(chat, messages);
                }
            });

            chat.onSentMessage.Add(delegate (MessageChannel.Message[] messages){
                foreach(MessageEvent listener in onSentMessage){
                    listener(chat, messages);
                }
            });
        }


    }


}
