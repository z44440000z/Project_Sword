using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour {
    public float height;
	// Update is called once per frame
	void Update () {
        height=GetGroundDistance();
    }
    public float GetGroundDistance(){//取得玩家與下方地面的距離
		RaycastHit hit;
		if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.down), out hit,LayerMask.GetMask("Ground")))
		{
			return Vector3.Distance(transform.position,hit.point);
		}
		else
		{
			return 0f;
		}
	}
}
