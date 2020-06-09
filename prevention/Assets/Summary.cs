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
    public Image progress;
    bool isOn;
    public Image fullImage;
    float totalProgress;

    void Start()
    {
        panel.SetActive(false);
        fullImage.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!isOn)
            return;

        
        progress.fillAmount = Mathf.Lerp(progress.fillAmount, totalProgress, 0.025f);
        
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

        isOn = true;

        totalProgress = Data.Instance.userData.GetTrophyProgress();
        progress.fillAmount = Data.Instance.userData.totalProgress;
        Data.Instance.userData.SetNewTotalProgress(totalProgress);

        if (totalProgress > 0.95f)
            fullImage.gameObject.SetActive(true);
        else
            fullImage.gameObject.SetActive(false);
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
