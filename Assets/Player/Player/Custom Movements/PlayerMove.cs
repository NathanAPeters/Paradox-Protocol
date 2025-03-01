using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    Rigidbody pRigidbody;
    float pSpeed;
    [SerializeField, Range(1, 100)] float pSpeedWalk;
    [SerializeField, Range(1, 100)] float pSpeedRun;
    bool IsImobilized;

    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        pRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!IsImobilized)
        {
            if (Input.GetKey(KeyCode.W))
            {
                positionHandler(0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                positionHandler(Mathf.PI);
            }
            if (Input.GetKey(KeyCode.A))
            {
                positionHandler(Mathf.PI * 1.5F);
            }
            if (Input.GetKey(KeyCode.D))
            {
                positionHandler(Mathf.PI * 0.5F);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                pSpeed = Time.deltaTime * pSpeedRun;
            }
            else
            {
                pSpeed = Time.deltaTime * pSpeedWalk;
            }
        }
    }

    internal void DisableMovement()
    {
        IsImobilized = true;
    }
    internal void EnableMovement()
    {
        IsImobilized = false;
    }
    void positionHandler(float direction)
    {
        float xPos = Mathf.Sin(transform.eulerAngles.y * Mathf.Deg2Rad + direction);
        float yPos = 0;
        float zPos = Mathf.Cos(transform.eulerAngles.y * Mathf.Deg2Rad + direction);
        gameObject.transform.position += new Vector3(xPos*pSpeed,yPos*pSpeed,zPos*pSpeed);
    }
}
