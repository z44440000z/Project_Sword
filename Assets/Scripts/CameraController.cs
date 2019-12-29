using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public PlayerController player;
    public float groundOffest = 3.46f;
    
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y+groundOffest-player.sensor.height/2 , transform.position.z);
    }
}
