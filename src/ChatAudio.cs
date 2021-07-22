using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatAudio : MonoBehaviour
{
    

    public AudioClip onRecievedMessage;
    public AudioClip onRecievedMessageActive;
    public AudioClip onSentMessage;

    public ChatArea chatArea;

    public AudioSource source;

    Queue<AudioClip> queue=new Queue<AudioClip>();

    void Start()
    {
        if(chatArea==null){
            chatArea=gameObject.GetComponent<ChatArea>();
        }

        if(chatArea){
            chatArea.onRecievedMessage.Add(delegate(PushChatChannel chat){
                queue.Enqueue(onRecievedMessage);
            });
            chatArea.onSentMessage.Add(delegate(PushChatChannel chat){
                queue.Enqueue(onSentMessage);
            });

        }

    }

    void Update()
    {
        while(queue.Count>0){

            AudioSource audio = GetComponent<AudioSource>();
            AudioClip clip=queue.Dequeue();
            if(!audio.isPlaying){
                audio.clip=clip;
                audio.Play();
            }
            
        }

    }

}
