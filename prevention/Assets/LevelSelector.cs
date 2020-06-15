using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public List<LevelButton> buttons;
    public GameObject tropheoDone;
    public Image tropheoBar;
    public GameObject Logo;
    public GameObject panel;
    public Text titleField;

    void Start()
    {
        titleField.text = Data.Instance.locallizationManager.Translate("title");
        if (Data.Instance.timesScenesChanged > 1)
        {
            Intro();
            panel.SetActive(false);
            Logo.SetActive(true);
            Invoke("Init", 4);
        }
        else
            Init();
    }
    public void Intro()
    {
        Events.PlayUISfx("click");
        Invoke("Init", 4);
        Events.PlayUISfx("splashOutro");
    }
    private void Init()
    {
        Logo.SetActive(false);
        panel.SetActive(true);
        int id = 0;
        
        foreach (LevelButton levelButton in buttons)
        {
            LevelData levelData = Data.Instance.userData.levelsData[id];
            levelButton.Init(levelData);
            id++;
        }
        float totalProgress = Data.Instance.userData.GetTrophyProgress();
        tropheoBar.fillAmount = totalProgress;

        if (totalProgress > 0.8f)
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
