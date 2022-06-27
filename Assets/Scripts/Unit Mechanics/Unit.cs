using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;    
    }

    private void Update()
    {
        float stoppingDistance = 0.1f;
        if(Vector3.Distance(targetPosition, transform.position) >= stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime); //rotate player to move direction

            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking",false);
        }
    }

    public void Move(Vector3 targetposition)
    {
        this.targetPosition = targetposition;
    }
}
