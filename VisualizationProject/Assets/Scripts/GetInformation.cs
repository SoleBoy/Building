using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInformation : MonoBehaviour
{
    float lastTime;
    bool isShowTip;

    void Start()
    {
       
    }

    private void OnMouseOver()
    {
        lastTime += Time.deltaTime;
        if(!isShowTip && lastTime >= 0.5f)
        {
            isShowTip = true;
            UIManager.Instance.SetInfo(transform.position);
        }
    }

    void OnMouseExit()
    {
        lastTime = 0;
        isShowTip = false;
        //if(lastTime >= 0.2f)
        //{
        //    Debug.Log("停留时间满足");
        //}
    }
}
