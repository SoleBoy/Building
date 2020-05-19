using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get => instance;}

    RectTransform leftPatent;
    RectTransform rightParent;
    GameObject infoPanel;

    private void Awake()
    {
        instance = this;
        leftPatent = transform.Find("LeftPatent").GetComponent<RectTransform>();
        rightParent = transform.Find("RightParent").GetComponent<RectTransform>();
        infoPanel = transform.Find("Image").gameObject;
    }

    void Start()
    {
        
    }

    //UI进入
    public void PageMoveEnter()
    {
        leftPatent.DOLocalMoveX((leftPatent.localPosition.x + leftPatent.rect.width), 1);
        rightParent.DOLocalMoveX((rightParent.localPosition.x - rightParent.rect.width), 1);
    }
    //UI出去
    public void PageMoveCome()
    {
        leftPatent.DOLocalMoveX((leftPatent.localPosition.x - leftPatent.rect.width), 1);
        rightParent.DOLocalMoveX((rightParent.localPosition.x + rightParent.rect.width), 1);
        
    }
    // 信息显示
    public void SetInfo(Vector3 point)
    {
        if(!infoPanel.activeInHierarchy)
        {
            point = Camera.main.WorldToScreenPoint(point);
            infoPanel.transform.position = point;
            infoPanel.SetActive(true);
            StartCoroutine(HidePanel());
        }
    }
    IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(2);
        infoPanel.SetActive(false);
    }
}
