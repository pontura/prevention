using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public int levelID;
    public List<LevelData> levelsData;
    public List<float> thisLevelscores;
    public int levelUnlocked;

    private void Start()
    {
        Events.OnGameDone += OnGameDone;
        LoadScores();
    }
    private void OnDestroy()
    {
        Events.OnGameDone -= OnGameDone;
    }
    void LoadScores()
    {
        levelUnlocked = 1;
        for (int a = 1; a < 6; a++)
        {
            levelsData[a - 1].score = PlayerPrefs.GetFloat("level_" + a);
            if (levelsData[a - 1].score > 0)
                levelUnlocked = a+1;
        }
        SetStars();
    }
    void SetStars()
    {
        for (int a = 1; a < 6; a++)
        {
            if(levelsData[a - 1].score == 0)
                levelsData[a - 1].stars = 0;
            else if (levelsData[a - 1].score < levelsData[a - 1].scoreForStar_1)
                levelsData[a - 1].stars = 1;
            else if (levelsData[a - 1].score < levelsData[a - 1].scoreForStar_2)
                levelsData[a - 1].stars = 2;
            else
                levelsData[a - 1].stars = 3;
        }
        
    }
    void OnGameDone()
    {
        float scoreTime = Game.Instance.timeline.GetValue();
        thisLevelscores.Add(scoreTime);
    }
    public void AllLevelComplete()
    {
        float newScore = GetAverage();
        if (levelsData[levelID - 1].score < newScore)
            SaveNewHiscore(newScore);
    }
    void SaveNewHiscore(float newScore)
    {
        levelsData[levelID - 1].score = newScore;
        PlayerPrefs.SetFloat("level_" + levelID, newScore);
        SetStars();
    }
    float GetAverage()
    {
        float total = 0;
        foreach (float f in thisLevelscores)
            total += f;
        return total / thisLevelscores.Count;
    }
}
