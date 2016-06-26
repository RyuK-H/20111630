using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    public  GameObject obj_Touch_Game;
    private Touch_Game m_srt_Touch_Game;
    public  GameObject obj_Touch_Wait;
    private Touch_Wait m_srt_Touch_Wait;
    public  GameObject obj_Touch_Shop;
    private Touch_Shop m_srt_Touch_Shop;

    public void Init()
    {
        m_srt_Touch_Game = obj_Touch_Game.GetComponent("Touch_Game") as Touch_Game;        
        m_srt_Touch_Wait = obj_Touch_Wait.GetComponent("Touch_Wait") as Touch_Wait;
        m_srt_Touch_Shop = obj_Touch_Shop.GetComponent("Touch_Shop") as Touch_Shop;

        m_srt_Touch_Game.Init();
    }

    public void Touching(int GameMode)
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj_hit = GetRayCastHit_Obj();

            if (obj_hit != null)
            {
                switch((STATE)GameMode)
                {
                    case STATE.WAIT:
                        m_srt_Touch_Wait.Wait_Touching(obj_hit);
                        break;

                    case STATE.GAME:
                        m_srt_Touch_Game.Wait_Touching(obj_hit);
                        break;

                    case STATE.SHOP:
                        m_srt_Touch_Shop.Wait_Touching(obj_hit);
                        break;                       
                }             
            }
        }    
    }

    private GameObject GetRayCastHit_Obj()
    {        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                       
        Physics.Raycast(ray, out hit, Mathf.Infinity);
        
        if (hit.transform == null)
            return null;
        return hit.transform.gameObject;
    }
}
