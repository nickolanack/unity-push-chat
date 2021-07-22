using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatArea : MonoBehaviour
{


    public Button openChat;
    public Button closeChat;
    public RectTransform chatArea;

    public PushChatChannel activeChat;
    public List<PushChatChannel> rooms=new List<PushChatChannel>();

    public bool open=true;
    public bool preventToggle=false;


    public float smoothTime = 0.3F;
    private Vector2 velocity = Vector2.zero;

    public InputField inputField;
    public bool hideCursorOnClose=false;

    public Button submitBtn;


    public  List<MonoBehaviour> disableMouseControls;

    // Start is called before the first frame update
    void Start()
    {
        if(chatArea==null){
            chatArea=gameObject.GetComponent<RectTransform>();
        }

        if(openChat!=null){
            openChat.onClick.AddListener(delegate(){
                Open();
            });
        }

        if(closeChat!=null){
            closeChat.onClick.AddListener(delegate(){
                Close();             
            });
        }


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

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 targetPosition = chatArea.anchoredPosition;
        targetPosition.x=chatArea.rect.width;

        if(open){
            targetPosition.x=0;
            
            if(openChat!=null){
                openChat.gameObject.SetActive(false);
            }
            if(closeChat!=null){
                closeChat.gameObject.SetActive(true);
            }
        }else{
            if(openChat!=null){
                openChat.gameObject.SetActive(true);
            }
            if(closeChat!=null){
                closeChat.gameObject.SetActive(false);
            }
        }

        if(chatArea.anchoredPosition.x==targetPosition.x){
            return;
        }

        Vector2 p=Vector2.SmoothDamp(chatArea.anchoredPosition, targetPosition, ref velocity, smoothTime);

        targetPosition.x=p.x;
        chatArea.anchoredPosition=targetPosition;
    }


    public void Open(){

        if(!Cursor.visible){
            Cursor.visible=true;
            hideCursorOnClose=true;
        }

        if(preventToggle){
            return;
        }

        foreach(MonoBehaviour script in disableMouseControls){
            script.enabled=false;
        }

        open=true;
        Debounce(0.5f);

        if(inputField!=null){
            inputField.ActivateInputField();
        }

    }
    public void Close(){
        if(preventToggle){
            return;
        }

        if(hideCursorOnClose){
            Cursor.visible=false;
            hideCursorOnClose=false;
        }

        foreach(MonoBehaviour script in disableMouseControls){
            script.enabled=true;
        }

        open=false;
        Debounce(0.5f);
    }
    void Reenable(){
        preventToggle=false;
    }
    void Debounce(float t){
        preventToggle=true;
        Invoke("Reenable", t);
    }

}
