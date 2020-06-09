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
        
        foreach (LevelButton levelButton in buttons)
        {
            LevelData levelData = Data.Instance.userData.levelsData[id];
            levelButton.Init(levelData);
            id++;
        }
        float totalProgress = Data.Instance.userData.GetTrophyProgress();
        tropheoBar.fillAmount = totalProgress;

        if (totalProgress > 0.95f)
            tropheoDone.SetActive(true);
        else
            tropheoDone.SetActive(false);
    }
    public void OpenLevel(int levelID)
    {
        Data.Instance.userData.levelID = levelID;
        Data.Instance.LoadScene("Game");
    }
}
