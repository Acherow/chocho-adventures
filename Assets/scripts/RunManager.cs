using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    public List<Upgrade> DefaultUpgrades;
    public static RunManager manager;
    public static int currentlevel;

    private void Awake()
    {
        if (manager == null)
            manager = this;
        else if(manager != this)
            Destroy(gameObject);

        CurrentRun ??= new PlayerStats(unlocks: DefaultUpgrades);
        if(CurrentRun.Unlocks == null || CurrentRun.Unlocks.Count == 0)
        {
            CurrentRun.Unlocks = DefaultUpgrades;
        }

        SceneManager.sceneLoaded -= NextLevel;
        SceneManager.sceneLoaded += NextLevel;
    }

    public static PlayerStats CurrentRun;

    public void NextLevel(Scene sc, LoadSceneMode mode)
    {
        if(sc.buildIndex == 1)
            currentlevel++;
        SceneManager.sceneLoaded -= NextLevel;
    }
}
