using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Json;

//存檔系統
public static class SaveSystem
{
    public static void Save(SaveData data)
    {
        PlayerPrefs.SetFloat("Score", data.score);
        PlayerPrefs.SetInt("BestCombo", data.bestCombo);
    }

    public static SaveData Load()
    {
        SaveData data = new SaveData(PlayerPrefs.GetFloat("Score"), PlayerPrefs.GetInt("BestCombo"));
        return data;
    }

    public static SaveData Compare(SaveData data)
    {
        SaveData newData = new SaveData(0, 0);
        //用較高的分數替代
        if (data.score > PlayerPrefs.GetFloat("Score"))
        { newData.score = data.score; }
        else
        { newData.score = PlayerPrefs.GetFloat("Score"); }
        //用較高的Combo數替代
        if (data.bestCombo > PlayerPrefs.GetInt("BestCombo"))
        { newData.bestCombo = data.bestCombo; }
        else
        { newData.bestCombo = PlayerPrefs.GetInt("BestCombo"); }
        return newData;
    }

}
[System.Serializable]
public class SaveData
{
    public float score;
    public int bestCombo;

    public SaveData(float score, int combo)
    {
        this.score = score;
        this.bestCombo = combo;
    }
}
