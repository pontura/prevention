using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summary : MonoBehaviour
{
    public GameObject panel;
    public Stars stars;
    public Text scoreField;

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
    }
    public void LevelSelector()
    {
        Data.Instance.LoadScene("LevelSelector");
    }
    public void Replay()
    {
        Game.Instance.Replay();
    }
    public void Next()
    {        
        if (Data.Instance.userData.levelID == 5)
            Data.Instance.LoadScene("LevelSelector");
        else
        {
            Data.Instance.userData.Next();
            Data.Instance.LoadScene("Game");
        }
            
    }
}
