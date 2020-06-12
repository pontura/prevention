using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    public GameObject enter;
    public GameObject logo;
    public Text titleField;

    void Start()
    {
        titleField.text = Data.Instance.locallizationManager.Translate("title");
        logo.SetActive(false);
        Events.PlayMusic("splash");
    }

    public void OnClicked()
    {
        Events.PlayUISfx("click");
        logo.SetActive(true);
        enter.SetActive(false);
        Invoke("GotoGame", 4);
        Events.StopMusic();
        Events.PlayUISfx("splashOutro");
    }
    void GotoGame()
    {
        Data.Instance.LoadScene("LevelSelector");
    }
}
