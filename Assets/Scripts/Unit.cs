using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;

    private void Update()
    {
        float stoppingDistance = 0.1f;
        if(Vector3.Distance(targetPosition, transform.position) >= stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetMouseWorldPosition());
        }
    }

    public void Move(Vector3 targetposition)
    {
        this.targetPosition = targetposition;
    }
}
