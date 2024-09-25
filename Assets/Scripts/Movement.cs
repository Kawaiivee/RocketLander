using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float upThrustSpeed = 600f;
    [SerializeField] float rotationThrustSpeed = 200f;
    Rigidbody rocketRigidbody;
    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        rocketRigidbody.drag = 0.5f;
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space)){

            rocketRigidbody.AddRelativeForce(Vector3.up * upThrustSpeed * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrustSpeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrustSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rocketRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRigidbody.freezeRotation = false;
    }

}
