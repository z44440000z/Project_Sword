using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public PlayerController player;
    public float groundOffest = 3.46f;
    public float heightDecrease = 0.3f;
    public float Smooth = 2.0f;

    private GameObject ScreenCenter;
    private float ScreenHeight = 4.3f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            ScreenCenter = new GameObject();
            ScreenCenter.transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        }
    }

    void Update()
    {
        Vector3 p;
        if (CheckMargin())
        { p = new Vector3(transform.position.x, player.transform.position.y, transform.position.z); }
        else
        { p = new Vector3(transform.position.x, player.transform.position.y + groundOffest - player.sensor.height * heightDecrease, transform.position.z); }
        transform.position = Vector3.Lerp(transform.position, p, Smooth * Time.deltaTime);
    }

    bool CheckMargin()//確認玩家位置
    {
        return Vector3.Distance(player.transform.position, ScreenCenter.transform.position) > ScreenHeight;
    }
}