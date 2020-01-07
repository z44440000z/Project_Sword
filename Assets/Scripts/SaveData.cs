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
