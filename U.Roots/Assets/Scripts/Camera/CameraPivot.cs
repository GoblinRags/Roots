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

    [SerializeField] private GameObject fastParallax;
    [SerializeField] private GameObject midParallax;
    [SerializeField] private GameObject slowParallax;
    [SerializeField] private GameObject cloud;
    [SerializeField] private GameObject fastParallaxNight;
    [SerializeField] private GameObject midParallaxNight;
    [SerializeField] private GameObject slowParallaxNight;
    [SerializeField] private GameObject cloudNight;

    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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
                fastParallax.transform.rotation = Quaternion.Euler(fastParallax.transform.eulerAngles - Vector3.forward * (activeSpeed / 15) * Time.deltaTime);
                midParallax.transform.rotation = Quaternion.Euler(midParallax.transform.eulerAngles - Vector3.forward * (activeSpeed / 28) * Time.deltaTime);
                slowParallax.transform.rotation = Quaternion.Euler(slowParallax.transform.eulerAngles - Vector3.forward * (activeSpeed / 40) * Time.deltaTime);
                fastParallaxNight.transform.rotation = Quaternion.Euler(fastParallaxNight.transform.eulerAngles - Vector3.forward * (activeSpeed / 15) * Time.deltaTime);
                midParallaxNight.transform.rotation = Quaternion.Euler(midParallaxNight.transform.eulerAngles - Vector3.forward * (activeSpeed / 28) * Time.deltaTime);
                slowParallaxNight.transform.rotation = Quaternion.Euler(slowParallaxNight.transform.eulerAngles - Vector3.forward * (activeSpeed / 40) * Time.deltaTime);
                break;
            case "Left":
                transform.rotation = Quaternion.Euler(transform.eulerAngles + Vector3.forward * activeSpeed * Time.deltaTime);
                fastParallax.transform.rotation = Quaternion.Euler(fastParallax.transform.eulerAngles - Vector3.forward * (activeSpeed / 15) * Time.deltaTime);
                midParallax.transform.rotation = Quaternion.Euler(midParallax.transform.eulerAngles - Vector3.forward * (activeSpeed / 28) * Time.deltaTime);
                slowParallax.transform.rotation = Quaternion.Euler(slowParallax.transform.eulerAngles - Vector3.forward * (activeSpeed / 40) * Time.deltaTime);
                fastParallaxNight.transform.rotation = Quaternion.Euler(fastParallaxNight.transform.eulerAngles - Vector3.forward * (activeSpeed / 15) * Time.deltaTime);
                midParallaxNight.transform.rotation = Quaternion.Euler(midParallaxNight.transform.eulerAngles - Vector3.forward * (activeSpeed / 28) * Time.deltaTime);
                slowParallaxNight.transform.rotation = Quaternion.Euler(slowParallaxNight.transform.eulerAngles - Vector3.forward * (activeSpeed / 40) * Time.deltaTime);
                break;
        }
    }

    void Update()
    {
        if (decel)
        {
            RotatePivot(enterDirection, "Decelerate");
        }
        
        cloud.transform.rotation = Quaternion.Euler(cloud.transform.eulerAngles - Vector3.forward * 2 * Time.deltaTime);
        cloudNight.transform.rotation = Quaternion.Euler(cloud.transform.eulerAngles - Vector3.forward * 2 * Time.deltaTime);
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
