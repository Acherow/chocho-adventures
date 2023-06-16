using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager man;

    private void Start()
    {
        if (man == null)
            man = this;
        else if (man != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded -= SceneLoad;
        SceneManager.sceneLoaded += SceneLoad;
    }

    void SceneLoad(Scene sc, LoadSceneMode mode)
    {
        if (sc.buildIndex < 2)
            Destroy(gameObject);
    }
}