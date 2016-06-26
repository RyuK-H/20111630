using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public  GameObject   obj_StateMachine;
    private StateMachine m_srt_StateMachine;

    void Start ()
    {
        m_srt_StateMachine = obj_StateMachine.GetComponent ("StateMachine") as StateMachine;
        m_srt_StateMachine.Init();
	}
	
	void Update ()
    {
        m_srt_StateMachine.Play();	
	}
}
