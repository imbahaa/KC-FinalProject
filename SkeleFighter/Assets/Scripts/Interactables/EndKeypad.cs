using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class EndKeypad : Interactable
{
    public TextMeshProUGUI score;
    public long scoreAmount = 0;
    public AudioSource ac;
    public AudioClip buzzer;

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
        Int64.TryParse(score.text, out scoreAmount);
        if (scoreAmount >= 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            ac.PlayOneShot(buzzer);
        }
    }
}
