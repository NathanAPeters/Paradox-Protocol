using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    Rigidbody pRigidbody;
    [SerializeField, Range(1,100)] float pSpeedWalk;
    [SerializeField, Range(1,100)] float pSpeedRun;
    [SerializeField] float maxPSpeed;
    [SerializeField, Range(0.9f, 1f)] float NotMovingSlowDown;
    [SerializeField, Range(0f, 1f)] float SlideReduction;
    float pSpeed;
    bool IsImobilized;
    void Start()
    {
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
                positionHandler(Mathf.PI*1.5F);
            }
            if (Input.GetKey(KeyCode.D))
            {
                positionHandler(Mathf.PI*0.5F);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                pSpeed = Time.deltaTime * pSpeedRun;
                Vector3 velocity = pRigidbody.velocity;
                velocity.x *= SlideReduction;
            }
            else
            {
                pSpeed = Time.deltaTime * pSpeedWalk;
            }
        }
        else
        {
            pRigidbody.velocity = Vector3.zero;
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {

            Vector3 horizontalVelocity = new Vector3(pRigidbody.velocity.x * NotMovingSlowDown, pRigidbody.velocity.y, pRigidbody.velocity.z * NotMovingSlowDown);
            pRigidbody.velocity = new Vector3(horizontalVelocity.x, pRigidbody.velocity.y, horizontalVelocity.z);
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

        Vector3 velocityToAdd = new Vector3(xPos, yPos, zPos) * pSpeed;
        if(pRigidbody.velocity.magnitude < maxPSpeed)
        {
            pRigidbody.velocity += velocityToAdd;
        }

        //pRigidbody.velocity = new Vector3(xPos, 0.0f, zPos) * pSpeed;
        //Vector3 movementDirection = new Vector3(xPos, 0.0f, zPos);
        //pRigidbody.MovePosition(transform.position + movementDirection * pSpeed);
    }
}
