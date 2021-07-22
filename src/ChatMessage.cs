using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour
{


    public Sprite plain;
    public Sprite plainAlt;

    public Sprite system;

    public Text text;
    public Image icon;

    public void SetText(string str){
        text.text=str;
    }

    public void SetText(MessageChannel.Message message){
        text.text=message.text;
        if(message.type==MessageChannel.Type.System){
            icon.sprite=system;
        }


    }

}
