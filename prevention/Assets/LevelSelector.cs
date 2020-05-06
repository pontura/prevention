using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public List<LevelButton> buttons;

    void Start()
    {
        int id = 0;
        foreach(LevelButton levelButton in buttons)
        {
            levelButton.Init(Data.Instance.userData.levelsData[id]);
            id++;
        }
    }
    public void OpenLevel(int levelID)
    {
        Data.Instance.userData.levelID = levelID;
        Data.Instance.LoadScene("Game");
    }
}
