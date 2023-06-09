using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Level
{
    public GameObject map;
    [SerializeField]
    public List<Vector2> spawnPoints;
    public List<Vector2> laserSpawnPoints;
    public List<Vector2> mcgSpawnPoints;
}

public class EnemyWaves : MonoBehaviour
{
    public List<Level> levels;
    public GameObject macguffinprefab;
    public int level;
    public int loop;

    public GameObject chaser, spitter, gazer, swarm;

    public float MaxEnemyTimer;
    float EnemyTimer;

    float mgTimer;

    public bool begun;

    public List<GameObject> AliveEnemies;

    public static int macguffincount;
    int maxmgc = 10;
    public TMP_Text mgtxt;

    Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        level = (RunManager.currentlevel-1) % levels.Count;
        loop = (RunManager.currentlevel-1) / levels.Count;
        Instantiate(levels[level].map, transform.position, Quaternion.identity);

        MaxEnemyTimer *= (Mathf.Pow(0.5f, loop));

        SceneManager.sceneLoaded -= RestartScene;
        SceneManager.sceneLoaded += RestartScene;
    }

    private void Update()
    {
        mgtxt.text = $"{macguffincount}/{maxmgc}";
        if(macguffincount >= maxmgc)
            player.gameObject.GetComponent<PlayerEndLevel>().ENDLEVEL();

        AliveEnemies.RemoveAll(obj => obj == null);

        if (begun)
        {
            if (EnemyTimer < 0 || AliveEnemies.Count <= 0)
                NextWave();
            else
                EnemyTimer -= Time.deltaTime;

            if (levels[level].mcgSpawnPoints.Count > 0)
            {
                if (mgTimer > 0)
                    mgTimer -= Time.deltaTime;
                else
                {
                    Instantiate(macguffinprefab, levels[level].mcgSpawnPoints[Random.Range(0, levels[level].mcgSpawnPoints.Count)] + new Vector2(Random.Range(-1,1f), Random.Range(-1, 1f)), Quaternion.identity);
                    mgTimer = 5;
                }
            }
        }

        if (!begun && player.position.y >= -2)
            begun = true;        
    }

    bool lasered;
    void NextWave()
    {
        if (levels.Count == 0)
            return;

        if (levels[level].spawnPoints.Count > 0)
        {
            foreach (var item in levels[level].spawnPoints)
            {
                int rng = Random.Range(1,101);
                GameObject g;
                if (rng < 10)
                    g = swarm;
                else if (rng < 50)
                    g = spitter;
                else
                    g = chaser;

                EnemyPortal s = Instantiate(g, item, Quaternion.identity).GetComponent<EnemyPortal>();
                s.loop = loop + 1;
                AliveEnemies.Add(s.gameObject);
            }
        }

        if (levels[level].laserSpawnPoints.Count > 0 && !lasered)
        {
            EnemyPortal s = Instantiate(gazer, levels[level].laserSpawnPoints[Random.Range(0, levels[level].laserSpawnPoints.Count)], Quaternion.identity).GetComponent<EnemyPortal>();
            s.loop = loop + 1;
            AliveEnemies.Add(s.gameObject);
            lasered = true;
        }
        else
            lasered = false;

        EnemyTimer = MaxEnemyTimer;
    }

    void RestartScene(Scene s, LoadSceneMode m)
    {
        macguffincount = 0;
    }
}