using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public Stars stars;
    public GameObject locked;
    public LevelSelector levelSelector;
    LevelData levelData;

    public void Init(LevelData levelData)
    {
        this.levelData = levelData;
        //if (levelData.id>Data.Instance.userData.levelUnlocked)
        //{
        //    locked.SetActive(true);
        //    GetComponent<Button>().interactable = false;
        //} 
        //else
            locked.SetActive(false);
        stars.Init(levelData.stars);
    }
    public void OnClicked()
    {
        levelSelector.OpenLevel(levelData.id);
    }
}
