using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] private Transform center;
    private PlayerController _playerController;
    private string enterDirection;
    
    [SerializeField] private float speed;
    private float activeSpeed;

    void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    
    void Update()
    {
        transform.position = center.position; // set pivot position the same as center of world
        
        Debug.Log(activeSpeed);
    }

    void RotatePivot(string direction)
    {
        activeSpeed = Mathf.Lerp(activeSpeed, 58f, 2f * Time.deltaTime);
        switch (direction)
        {
            case "Right":
                transform.rotation = Quaternion.Euler(transform.eulerAngles - Vector3.forward * activeSpeed * Time.deltaTime);
                break;
            case "Left":
                transform.rotation = Quaternion.Euler(transform.eulerAngles + Vector3.forward * activeSpeed * Time.deltaTime);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enterDirection = _playerController.movementInput > 0 ? "Right" : "Left";
            activeSpeed = speed;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _playerController.isMoving)
        {
            var movingDirection = _playerController.movementInput > 0 ? "Right" : "Left";
            if (movingDirection == enterDirection)
            {
                RotatePivot(movingDirection);
            }
        }
        else if (other.CompareTag("Player") && !_playerController.isMoving)
        {
            RotatePivot(enterDirection);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enterDirection = "";
            activeSpeed = speed;
        }
    }
}
