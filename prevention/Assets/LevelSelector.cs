using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public List<LevelButton> buttons;
    public GameObject tropheoDone;
    public Image tropheoBar;

    void Start()
    {
        int id = 0;
        float totalProgress = 0;
        foreach(LevelButton levelButton in buttons)
        {
            LevelData levelData = Data.Instance.userData.levelsData[id];
            levelButton.Init(levelData);
            print(id + " levelData.stars:" + levelData.stars);
            totalProgress += (float)levelData.stars/3;
            id++;
        }
        print("totalProgress:" + totalProgress);
        totalProgress /= 5;
        print("totalProgress by level:" + totalProgress);
        tropheoBar.fillAmount = totalProgress;

        if (totalProgress > 0.95f)
        {
            tropheoDone.SetActive(true);
        }
        else
        {
            tropheoDone.SetActive(false);
        }
    }
    public void OpenLevel(int levelID)
    {
        Data.Instance.userData.levelID = levelID;
        Data.Instance.LoadScene("Game");
    }
}
