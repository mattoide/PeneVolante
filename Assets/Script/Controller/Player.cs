using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int accellerometerForceMultiplier = 100;
    public float speed = 0.00f;
    private readonly double inizioInclinazione = -0.1;
    private readonly double fineInclinazione = 0.1;

    private new Rigidbody rigidbody;
    private RigidbodyConstraints rigidbodyOriginalConstrains;

    private Vector3 initialPosition;
    private Vector3 deviceAcceleration;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialPosition = GetComponent<Transform>().position;
        rigidbodyOriginalConstrains = rigidbody.constraints;
    }



    void Update()
    {
        this.handleTouch();
        deviceAcceleration = Input.acceleration;
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(0, 0, speed, ForceMode.VelocityChange);
        this.handleAccellerometer();
    }



    void handleTouch()
    {
        foreach (Touch t in Input.touches)
        {

            Debug.Log("Nuovo tocco: " + t.fingerId + " pos: " + t.position.x + " " + t.position.y);

            switch (t.phase)
            {
                case TouchPhase.Began:
                    this.transform.position = initialPosition;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    break;
            }
        }
    }

    void handleAccellerometer()
    {


        if (isRightAccellerated())
        {
            rigidbody.constraints = rigidbodyOriginalConstrains;

            if (isGoingRight() && canGoRight())
                rigidbody.AddForce(deviceAcceleration.x * accellerometerForceMultiplier, 0, 0, ForceMode.VelocityChange);
            else if (isGoingLeft() && canGoLeft())
                rigidbody.AddForce(deviceAcceleration.x * accellerometerForceMultiplier, 0, 0, ForceMode.VelocityChange);
            else
                rigidbody.constraints = rigidbodyOriginalConstrains | RigidbodyConstraints.FreezePositionX;
        }
    }

    bool isRightAccellerated() {
        return deviceAcceleration.x < inizioInclinazione || deviceAcceleration.x > fineInclinazione;
    }

    bool canGoLeft() {
        return this.transform.position.x > GameManager.leftLimit;
    }

    bool canGoRight() {
        return this.transform.position.x < GameManager.rightLimit;
    }

    bool isGoingLeft() {
        return deviceAcceleration.x < 0;
    }

    bool isGoingRight() {
        return deviceAcceleration.x > 0;
    }


}
