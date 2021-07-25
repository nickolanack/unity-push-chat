using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStack 
{


    public static void StackVertical(GameObject a, GameObject b, float padY=0){
    	RectTransform ar=a.GetComponent<RectTransform>();
        RectTransform br=b.GetComponent<RectTransform>();

        Vector2 targetPosition=br.anchoredPosition;
        targetPosition.y=ar.anchoredPosition.y-(ar.rect.height+padY);
        br.anchoredPosition=targetPosition;

    }


}
