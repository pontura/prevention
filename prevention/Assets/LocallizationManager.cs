using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocallizationManager : MonoBehaviour
{
    public List<Texts> all;
    public TextAsset textAssetFile;
    public SystemLanguage systemLanguage;

    [Serializable]
    public class Texts
    {
        public SystemLanguage lang;
        public string title;
        public string action;
    }

    void Start()
    {
        this.systemLanguage = Application.systemLanguage;
    }
    public string Translate(string category)
    {
        Texts lineText = all[0];
        foreach (Texts t in all)
        {
            if(t.lang == systemLanguage)
                lineText = t;
        }
        switch (category)
        {
            case "title":       return lineText.title;
            default:            return lineText.action;
        }
    }
}
