using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour {

	public void SETTING_GOAL_POSITION(Vector3 pos)
    {
        this.gameObject.transform.position = pos;
    }
}
