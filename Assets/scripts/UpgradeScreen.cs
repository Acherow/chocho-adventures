using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeScreen : MonoBehaviour
{
    List<Upgrade> upgrades;

    List<Upgrade> choices;

    public TMP_Text txt1, txt2, txt3;
    public Image img1, img2, img3;

    private void Start()
    {
        upgrades = RunManager.CurrentRun.Unlocks;

        choices = new List<Upgrade>();
        choices.Add(upgrades[Random.Range(0, upgrades.Count)]);

        for (int i = 0; i < 2; i++)
        {
            int c = Random.Range(0, upgrades.Count);
            while (choices.Contains(upgrades[c]))
            {
                c = Random.Range(0, upgrades.Count);
            }
            choices.Add(upgrades[c]);
        }

        txt1.text = choices[0].name + "\n" + choices[0].description;
        txt2.text = choices[1].name + "\n" + choices[1].description;
        txt3.text = choices[2].name + "\n" + choices[2].description;
        img1.sprite = choices[0].image;
        img2.sprite = choices[1].image;
        img3.sprite = choices[2].image;
    }

    public void ChooseUpgrade(int choice)
    {
        RunManager.CurrentRun.Unlocks.Remove(choices[choice]);
        RunManager.CurrentRun += choices[choice].StatChange;
        SceneManager.LoadScene(2);
    }
}
