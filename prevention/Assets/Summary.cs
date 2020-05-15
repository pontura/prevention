using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summary : MonoBehaviour
{
    public GameObject panel;
    public Stars stars;
    public Text scoreField;
    public Button buttonNext;

    void Start()
    {
        panel.SetActive(false);
    }
    
    public void Init()
    {
        int s = Data.Instance.userData.GetStars(Data.Instance.userData.newScore, Data.Instance.userData.levelID);
        stars.Init(s);
        scoreField.text = Mathf.Round(Data.Instance.userData.newScore * 100).ToString();
        panel.SetActive(true);
        if (Data.Instance.userData.levelID == 5)
            buttonNext.interactable = false;
        else
            buttonNext.interactable = true;
    }
    public void LevelSelector()
    {
        Events.PlayUISfx("click");
        Data.Instance.LoadScene("LevelSelector");
    }
    public void Replay()
    {
        Events.PlayUISfx("click");
        Game.Instance.Replay();
    }
    public void Next()
    {
        Events.PlayUISfx("click");
        Data.Instance.userData.Next();
        Events.PlayMusic("ingame");
        Data.Instance.LoadScene("Game");        
    }
}
