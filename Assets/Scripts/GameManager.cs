using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject A;
    public GameObject B;

    public int StartCount = 5;
    public int brokeCount = 0;
    public int Wave = 0;
    public float WaitTime = 3;

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

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        // 防止裝置睡眠
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        CreatObstacle();
        FloatingTextController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (brokeCount == StartCount)
        {
            StartCount++;
            brokeCount = 0;
            Wave++;
            Invoke("CreatObstacle",1);
        }
    }

    void CreatObstacle()
    {
        for (int i = 0; i < StartCount; i++)
        {
            GameObject o;
            if (Random.Range(0, 2) == 0)
            { o = Instantiate(A, transform.position + new Vector3(0, i * 3, 0), transform.rotation); }
            else
            { o = Instantiate(B, transform.position + new Vector3(0, i * 3, 0), transform.rotation); }
            if (o.GetComponent<Rigidbody>().drag >= 0)
            { o.GetComponent<Rigidbody>().drag -= Wave * 0.5f; }
            // else
            // { o.GetComponent<Rigidbody>().mass +=  Wave *0.5f; }

        }
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
}
