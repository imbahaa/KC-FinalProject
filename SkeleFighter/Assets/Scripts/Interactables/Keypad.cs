using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Keypad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    public AudioSource ac;
    public AudioClip doorSound;

    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        ac.PlayOneShot(doorSound);
        StartCoroutine(OpenDoor());
    }

    public IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.8f);
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
