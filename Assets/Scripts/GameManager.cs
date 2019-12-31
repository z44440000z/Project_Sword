using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject A;
    public GameObject B;

    public int StartCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < StartCount; i++)
        {
            if (Random.Range(0, 2) == 0)
            { Instantiate(A, transform.position + new Vector3(0, i * 3, 0), transform.rotation); }
            else
            { Instantiate(B, transform.position + new Vector3(0, i * 3, 0), transform.rotation); }

        }
        FloatingTextController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
