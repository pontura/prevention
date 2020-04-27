using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Settings : MonoBehaviour
{
    public TextAsset configFile;

    public GameData gameData;

    [Serializable]
    public class GameData
    {
        public int[] soap_duration;
        public int[] soap_score;
        public int[] vertical;
        public int[] vertical_score;
        public int[] circles;
        public int[] circles_score;
        public int[] pulgar;
        public int[] pulgar_score;
        public int[] nails;
        public int[] nails_score;
    }
    private void Start()
    {
        gameData = JsonUtility.FromJson<GameData>(configFile.text);
    }
}
