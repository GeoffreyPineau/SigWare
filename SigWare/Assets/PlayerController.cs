using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion playerRot;
    float rotSpeed = 5;
    float speed = 10;
    bool moving = false;
    private bool overlap = false;


    void Update()
    {   
        
            SetTargetPosition();
       
        if (moving)
            Move();
    }

    private void OnMouseOver()
    {
        Debug.Log("MoseOver");
        overlap = true;
    }
    void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000) && overlap == false)
        {
            targetPosition = hit.point;
            this.transform.LookAt(targetPosition);

            lookAtTarget = new Vector3(targetPosition.x - transform.position.x,
                transform.position.y, 
                targetPosition.z - transform.position.z);
            playerRot = Quaternion.LookRotation(lookAtTarget);
            moving = true;
            overlap = true;
        }
    }

    void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition * 0.85f, speed * Time.deltaTime);
        if (transform.position == targetPosition)
            moving = false;
        overlap = false;
    }
}
