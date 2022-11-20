using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlMenu1 : MonoBehaviour
{
    public void Control()
    {
        Debug.Log("pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -3);
    }
}
