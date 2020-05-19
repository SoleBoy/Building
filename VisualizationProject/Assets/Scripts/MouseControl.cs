using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MouseControl : MonoBehaviour
{
    public float speed = 5;
    public float angle = 60;

    private Vector3 vect;
    private float xcream;
    private float ycream;
    private Transform follow_obj;

    private void Awake()
    {

    }
    private void Update()
    {
        Get_Model();
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            CreamView();
        }
    }
    private void CreamView()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if (x != 0 || y != 0)
        {
            LimitAngle();
            this.transform.Rotate(-y * speed, 0, 0);
            this.transform.Rotate(0, x * speed, 0, Space.World);
        }
    }
    //相机的旋转
    private void LimitAngle()
    {
        vect = this.transform.eulerAngles;
        //限制相机左右视角的角度
        xcream = IsPosNum(vect.x);
        if (xcream > angle)
            this.transform.rotation = Quaternion.Euler(angle, vect.y, 0);
        else if (xcream < -angle)
            this.transform.rotation = Quaternion.Euler(-angle, vect.y, 0);

        // 限制相机上下视角的角度
        vect = this.transform.eulerAngles;
        ycream = IsPosNum(vect.y);
        if (ycream > angle)
            this.transform.rotation = Quaternion.Euler(vect.x, angle, 0);
        else if (ycream < -angle)
            this.transform.rotation = Quaternion.Euler(vect.x, -angle, 0);
    }
    /// <summary>
    /// 将角度转换为-180~180的角度
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private float IsPosNum(float x)
    {
        x -= 180;
        if (x < 0)
            return x + 180;
        else return x - 180;
    }

    public void Get_Model()
    {
        if (Input.GetMouseButtonDown(1) && follow_obj)
        {
            if (follow_obj.gameObject.CompareTag("Floor"))
            {
                transform.DORotate(TotalData.startAngle,1);
                transform.DOLocalMove(TotalData.startPoint, 1);
                follow_obj.GetComponent<Collider>().enabled = true;
                UIManager.Instance.PageMoveCome();
                follow_obj = null;
                return;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Floor"))
                {
                    follow_obj = hit.transform;
                    follow_obj.GetComponent<Collider>().enabled = false;
                    transform.DORotate(TotalData.buildingAngle1, 1);
                    transform.DOLocalMove(TotalData.buildingPoint1, 1);
                    UIManager.Instance.PageMoveEnter();
                }
            }
        }
    }
}
