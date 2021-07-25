using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{


    public Button openBtn;
    public Button closeBtn;

    public RectTransform panel;


    public float smoothTime = 0.3F;
    private Vector2 velocity = Vector2.zero;


    public bool open=true;
    public bool preventToggle=false;

    public bool hideCursorOnClose=false;

    public  List<MonoBehaviour> disableMouseControls;

    float pad=2;


    // Start is called before the first frame update
    void Start()
    {
        if(panel==null){
            panel=gameObject.GetComponent<RectTransform>();
        }

        if(openBtn!=null){
            openBtn.onClick.AddListener(delegate(){
                Open();
            });
        }

        if(closeBtn!=null){
            closeBtn.onClick.AddListener(delegate(){
                Close();             
            });
        }


        if(!open){
            Vector2 targetPosition = panel.anchoredPosition;
            targetPosition.x=(panel.rect.width+pad)*(panel.pivot.x<0.5?-1:1);
            panel.anchoredPosition=targetPosition;
        }
    }

    void Update()
    {

        Vector2 targetPosition = panel.anchoredPosition;
        targetPosition.x=(panel.rect.width+pad)*(panel.pivot.x<0.5?-1:1);

        if(open){
            targetPosition.x=0;
            
            if(openBtn!=null){
                openBtn.gameObject.SetActive(false);
            }
            if(closeBtn!=null){
                closeBtn.gameObject.SetActive(true);
            }
        }else{
            if(openBtn!=null){
                openBtn.gameObject.SetActive(true);
            }
            if(closeBtn!=null){
                closeBtn.gameObject.SetActive(false);
            }
        }

        if(Mathf.Abs(panel.anchoredPosition.x-targetPosition.x)<0.02){
            panel.anchoredPosition=targetPosition;
            return;
        }

        Vector2 p=Vector2.SmoothDamp(panel.anchoredPosition, targetPosition, ref velocity, smoothTime);
        targetPosition.x=p.x;
        panel.anchoredPosition=targetPosition;


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
