using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float sequenceDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        // should turn tags into enum
        var otherTag = other.gameObject.tag;
        switch(otherTag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fatal":
                StartCrashSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        StartCoroutine(StartSuccessSequenceCoroutine());
    }

    void StartCrashSequence()
    {
        StartCoroutine(StartCrashSequenceCoroutine());
    }

    IEnumerator StartSuccessSequenceCoroutine()
    {
        while (audioSource.isPlaying == true)
        {
            yield return null;
        }
        audioSource.PlayOneShot(success);
        //GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", sequenceDelay);
    }
    
    IEnumerator StartCrashSequenceCoroutine()
    {
        while (audioSource.isPlaying == true)
        {
            yield return null;
        }
        audioSource.PlayOneShot(crash);
        //GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", sequenceDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }
}

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Rocket");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}