using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public float moveSpeed = 6;
    Rigidbody myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }




    void Update()
    {
        // 说明： 鼠标在屏幕上位置 Input.mousePosition
        //通过摄像机 创造一条射线，将position与游戏空间联系在一起
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //创建一个射线碰撞物体
        RaycastHit hitt = new RaycastHit();
        //打出ray,碰撞结果保存在hitt中
        if(Physics.Raycast(ray, out hitt, 100, LayerMask.GetMask("Ground")))
        //打印是否碰撞
        Debug.Log("hitt", hitt.transform);      //值得试一试
        if (hitt.transform != null)
        {
            transform.LookAt(new Vector3(hitt.point.x, transform.position.y, hitt.point.z));
        }

        //wasd移动，Rigidbody.velocity刚体移动速度  Input.GetAxisRaw()输出的是一个-1~+1间的浮点数，所以需要把其标准化变成那个1（normalized）
        myRigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * moveSpeed;

    }
}