using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkenemy : MonoBehaviour
{
    //使用Debug.DrawLine函数显示的射线只会出现在编辑窗口

    public float viewRadius = 8.0f;     //视野最远距离
    public float viewAngleStep = 30;    //射线数量，越打越密

    void DrawFieldOfView()
    {
        //获得最左边那条射线的响亮，相对正前方，角度是-45
        Vector3 forward_left = Quaternion.Euler(0, -45, 0) * transform.forward * viewRadius;

        //依次处理每条线

        for (int i = 0; i <= viewAngleStep; i++)
        {
            Vector3 v = Quaternion.Euler(0, (90.0f / viewAngleStep) * i, 0) * forward_left;     //涉及欧拉角

            //创建射线
            Ray ray = new Ray(transform.position, v);
            RaycastHit hitt = new RaycastHit();
            //射线只有两种层碰撞，注意名字一致

            int mask = LayerMask.GetMask("Obstacle", "Enemy");      //实际是一种位操作，unity 一共32层lay， 记录每一层的2进制传给int

            Physics.Raycast(ray, out hitt, viewRadius, mask);


            //Player 位置加v，就是射线终点 pos
            Vector3 pos = transform.position + v;

            if (hitt.transform != null)
            {
                //射线终点变成碰撞的点
                pos = hitt.point;
            }


            //Debug.DrawLine 画线段
            Debug.DrawLine(transform.position, pos, Color.red);


            //如果发现敌人，显示敌人
            if (hitt.transform != null && hitt.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                OnEnemySpotted(hitt.transform.gameObject);
            }
        }
    }



    void OnEnemySpotted(GameObject enemy)
    {
        //当前帧数给spottedFrame ，离开视线10帧以后消失
        enemy.GetComponent<Enemy>().spottedFrame = Time.frameCount;
    }




    void Update()
    {
        DrawFieldOfView();
        
    }

}

