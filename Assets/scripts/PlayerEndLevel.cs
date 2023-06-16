using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEndLevel : MonoBehaviour
{
    public GameObject endscreen;

    public void ENDLEVEL()
    {
        GetComponent<Animator>().SetTrigger("END");
    }

    public void DEATH()
    {
        RunManager.CurrentRun = null;
        endscreen.SetActive(true);
    }

    public void Go()
    {
        SceneManager.LoadScene(3);
    }
}
