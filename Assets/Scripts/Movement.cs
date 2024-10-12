using UnityEngine;

public class Movement : MonoBehaviour
{
    // Parameters
    [SerializeField] float upThrustSpeed = 2000f;
    [SerializeField] float rotationThrustSpeed = 200f;
    [SerializeField] float drag = 1.0f;
    [SerializeField] AudioClip rocketBoostAudioClip;

    // Cache
    Rigidbody rocketRigidbody;
    AudioSource rocketAudioSource;

    // State
    bool isAlive;

    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
        rocketRigidbody.drag = drag;
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
            if(!rocketAudioSource.isPlaying){
                rocketAudioSource.PlayOneShot(rocketBoostAudioClip);
            }
        }
        else{
            rocketAudioSource.Stop();
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
