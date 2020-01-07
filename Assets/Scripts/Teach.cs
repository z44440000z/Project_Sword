using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teach : MonoBehaviour
{
    public Text bestcombo;
    public Text score;
    
    [Header("Exit Panel")]
    public GameObject ExitPanel;

    public Image[] Teach_img;
    private SaveData data;
    // Start is called before the first frame update
    void Start()
    {
        data = SaveSystem.Load();
        score.text = data.score.ToString();
        bestcombo.text = data.bestCombo.ToString();
        ExitPanel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        // 按下返回按鍵後, 關閉 APP
        if (Input.GetKeyDown(KeyCode.Escape))
        { ExitPanel.SetActive(true); }
    }
    public void YesButton()
    { Application.Quit(); }
    public void NoButton()
    { ExitPanel.SetActive(false); }
    public void StartButton()
    { SceneManager.LoadScene(1); }

    public void ShowExitPanel()
    {ExitPanel.SetActive(true);}
}
