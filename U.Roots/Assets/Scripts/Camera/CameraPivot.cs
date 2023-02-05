using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] private Transform center;
    private PlayerController _playerController;
    private string enterDirection;
    
    private float speed;
    private float activeSpeed;
    private bool decel;

    void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        speed = _playerController.moveSpeed * 15; // DO NOT CHANGE
        transform.position = center.position; // set pivot position the same as center of world
    }

    void RotatePivot(string direction, string lerpMode)
    {
        switch (lerpMode)
        {
            case "Accelerate":
                activeSpeed = Mathf.Lerp(activeSpeed, speed, 5f * Time.deltaTime);
                break;
            case "Decelerate":
                activeSpeed = Mathf.Lerp(activeSpeed, 0, 2f * Time.deltaTime);
                break;
        }
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

    void Update()
    {
        if (decel)
        {
            RotatePivot(enterDirection, "Decelerate");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (_playerController.movementInput != 0) enterDirection = _playerController.movementInput > 0 ? "Right" : "Left";
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && _playerController.isMoving)
        {
            decel = false;
            var movingDirection = _playerController.movementInput > 0 ? "Right" : "Left";
            if (movingDirection == enterDirection) RotatePivot(movingDirection, "Accelerate");
            else decel = true;
        }
        else if (other.gameObject.name == "Player" && !_playerController.isMoving)
        {
            decel = true;
        }
    }
}
