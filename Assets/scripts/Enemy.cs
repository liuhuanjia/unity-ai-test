using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float viewRadius = 8.0f;
    public float viewAngleStep = 40;


    Vector3 basePosition;
    Quaternion baseDirection;


	// Use this for initialization
	void Start () {
        basePosition = transform.position;
        baseDirection = transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {
        

		
	}


    void DrawFieldOfView()
    {
        Vector3 forward_left = Quaternion.Euler(0, -45, 0) * transform.forward * viewRadius;

        for(int i=0;i<=viewAngleStep;i++)
        {
            Vector3 v = Quaternion.Euler(0, 90.0f / viewAngleStep * i, 0) * transform.forward * viewRadius;
            Ray ray = new Ray(transform.position, v);
            RaycastHit hitt = new RaycastHit();

            int mask = LayerMask.GetMask("Obstacle", "Player");
            Physics.Raycast(ray, out hitt, viewRadius, mask);

            Vector3 pos = transform.position + v;
            if (hitt.transform != null)
            {
                pos = hitt.point;
            }
            Debug.DrawLine(transform.position, pos, Color.red);

            if(hitt.transform!=null&&hitt.transform.gameObject.layer==LayerMask.GetMask("Player"))
            {
                
            }

        }

       
    }


    void OnEnemySpotter(GameObject enemy)
    {
        Debug.Log("Plyer");
    }
}
