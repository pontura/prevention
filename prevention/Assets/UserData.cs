using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public int levelID;
    public List<LevelData> levelsData;
    public List<float> thisLevelscores;
    public int levelUnlocked;
    public float newScore;
    public float totalProgress;

    private void Start()
    {        
        Events.OnGameDone += OnGameDone;
        LoadScores();
        totalProgress = GetTrophyProgress();
    }
    private void OnDestroy()
    {
        Events.OnGameDone -= OnGameDone;
    }
    public void Next()
    {
        levelID++;
        if (levelID > 5)
            levelID = 5;
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
            levelsData[a - 1].stars = GetStars(levelsData[a - 1].score, a);
        
    }
    public int GetStars(float score, int levelID)
    {
        if (score == 0)
            return 0;
        else if (score < levelsData[levelID - 1].scoreForStar_1)
            return 1;
        else if (score < levelsData[levelID - 1].scoreForStar_2)
            return 2;
        else
            return 3;
    }
    void OnGameDone()
    {
        print("OnGameDone");
        float scoreTime = Game.Instance.timeline.GetValue();
        thisLevelscores.Add(scoreTime);
    }
    public void AllLevelComplete()
    {
        print("AllLevelComplete");
        OnGameDone();
        newScore = GetAverage();
        if (levelsData[levelID - 1].score < newScore)
            SaveNewHiscore(newScore);

        LoadScores();
        thisLevelscores.Clear();
    }
    void SaveNewHiscore(float newScore)
    {
        levelsData[levelID - 1].score = newScore;
        PlayerPrefs.SetFloat("level_" + levelID, newScore);
        SetStars();
    }
    float GetAverage()
    {
        print("Avergao");
        float total = 0;
        foreach (float f in thisLevelscores)
        {
            print("Avergao : " + f + " total: " + total);
            total += f;
        }
        return total / thisLevelscores.Count;
    }
    public float GetTrophyProgress()
    {
        float totalProgress = 0;
        foreach (LevelData levelData in Data.Instance.userData.levelsData)
        {
            totalProgress += (float)levelData.stars / 3;
        }
        print("totalProgress:" + totalProgress);
        totalProgress /= 5;
        print("totalProgress by level:" + totalProgress);
        return totalProgress;
    }
    public void SetNewTotalProgress(float totalProgress)
    {
        this.totalProgress = totalProgress;
    }
}
