using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public static GameManager Instance;
    public GameObject A;
    public GameObject B;
    public int StartCount = 5;
    public float interval = 2;
    public int brokeCount = 0;
    public int Wave = 0;
    public float WaitTime = 3;
    [Header("Exit Panel")]
    public GameObject ExitPanel;
    [Header("Audio")]
    public AudioSource audiosource;

    private void OnEnable()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
        }
        else
        {
            if (GameManager.Instance != this)
            {
                Destroy(GameManager.Instance.gameObject);
                GameManager.Instance = this;
            }
        }

    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        // 防止裝置睡眠
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //初始化方塊
        CreatObstacle();
        FloatingTextController.Initialize();
        ExitPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 按下返回按鍵後, 關閉 APP
        if (Input.GetKeyDown(KeyCode.Escape))
        { ExitPanel.SetActive(true); }

        //生成下一波方塊
        if (brokeCount == StartCount && !player.isjump)
        {
            StartCount++;
            brokeCount = 0;
            Wave++;
            Invoke("CreatObstacle", 1);
        }
    }

    //生成方塊
    void CreatObstacle()
    {
        for (int i = 0; i < StartCount; i++)
        {
            GameObject o;
            if (Random.Range(0, 2) == 0)
            { o = Instantiate(A, transform.position + new Vector3(0, i * interval, 0), transform.rotation); }
            else
            { o = Instantiate(B, transform.position + new Vector3(0, i * interval, 0), transform.rotation); }
            //讓方塊加速
            if (o.GetComponent<Rigidbody>().drag >= 0)
            { o.GetComponent<Rigidbody>().drag -= Wave * 0.5f; }
            // else
            // { o.GetComponent<Rigidbody>().mass +=  Wave *0.5f; }

        }
    }
    //按鈕事件-重新開始
    public void RestartButton()
    { SceneManager.LoadScene(1); }
    public void YesButton()
    { SceneManager.LoadScene(0); }
    public void NoButton()
    { ExitPanel.SetActive(false); }
    //確認本波方塊已全部清除
    public bool IfCubeExist()
    { return brokeCount != StartCount; }
}
