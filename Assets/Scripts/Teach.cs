using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teach : MonoBehaviour
{
    public static Teach Instance;
    [Header("Result Panel")]
    public GameObject ResultPanel;
    public Text bestcombo;
    public Text score;

    [Header("Exit Panel")]
    public GameObject ExitPanel;
    [Header("Teach Panel")]
    public GameObject TeachPanel;
    public Image TeachImg;
    public Text SpriteCountText;
    public int SpriteCount = 0;
    public Sprite[] TeachSprite;
    private SaveData data;
    private void OnEnable()
    {
        if (Teach.Instance == null)
        {
            Teach.Instance = this;
        }
        else
        {
            if (Teach.Instance != this)
            {
                Destroy(Teach.Instance.gameObject);
                Teach.Instance = this;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetRecords();
        ExitPanel.SetActive(false);
        TeachPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 按下返回按鍵後, 關閉 APP
        if (Input.GetKeyDown(KeyCode.Escape))
        { ExitPanel.SetActive(true); }
        //退出界面
        if (ExitPanel.activeSelf)
        { Time.timeScale = 0.01f; }
        else
        { Time.timeScale = 1f; }
        //教學界面
        if (TeachPanel.activeSelf)
        {
            TeachImg.sprite = TeachSprite[SpriteCount];
            SpriteCountText.text = (SpriteCount + 1).ToString();
            if (Input.GetMouseButtonDown(0))
            {
                if (SpriteCount + 1 < TeachSprite.Length)
                { SpriteCount++; }
                else
                {
                    SpriteCount = 0;
                    TeachPanel.SetActive(false);
                }
            }
        }
        //結算界面
        if (ResultPanel.activeSelf)
        { }
    }

    public void GetRecords()
    {
        data = SaveSystem.Load();
        score.text = data.score.ToString();
        bestcombo.text = data.bestCombo.ToString();
    }
    //按鍵事件-確認退出
    public void YesButton()
    { Application.Quit(); }
    //按鍵事件-取消退出
    public void NoButton()
    { ExitPanel.SetActive(false); }
    //按鍵事件-開始/重新開始
    public void StartButton()
    { SceneManager.LoadScene(1); }
     //按鍵事件-回到開始畫面
    public void HomeButton()
    { SceneManager.LoadScene(0); }
    //按鍵事件-打開退出介面
    public void ShowExitPanel()
    { ExitPanel.SetActive(true); }
    //按鍵事件-打開教學介面
    public void ShowTeachPanel()
    { TeachPanel.SetActive(true); }
}
