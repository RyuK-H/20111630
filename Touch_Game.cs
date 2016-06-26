using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Touch_Game : MonoBehaviour {

    public  GameObject        obj_TileManager;
    public  GameObject        obj_SupervisePosition;
    private SupervisePosition m_srt_SupervisePosition;
    private TileManager       m_srt_TileManager;
    private Character         m_srt_Charater;
    private List<Tile>        List_Tile;

    public void Init()
    {
        m_srt_TileManager       = obj_TileManager.GetComponent("TileManager") as TileManager;
        m_srt_SupervisePosition = obj_SupervisePosition.GetComponent("SupervisePosition") as SupervisePosition;
        List_Tile               = m_srt_TileManager.RETURN_LIST();
    }

    public void Wait_Touching(GameObject obj_hit)
    {

        switch (obj_hit.tag)
        {
            case "Home":
                print("Home");
                StateMachine.GAMESTATE = 0;
                break;

            case "GameMode":
                print("GameMode");
                StateMachine.GAMESTATE = 1;
                break;

            case "Shop":
                print("Shop");
                StateMachine.GAMESTATE = 2;
                break;

            case "Character":
                m_srt_Charater = obj_hit.GetComponent("Character") as Character;
                m_srt_SupervisePosition.Init_Touch_Character_Num(m_srt_Charater.RETURN_CHARACTER_TYPE(), m_srt_Charater.RETURN_CHARACTER_DIRECTION());
                m_srt_SupervisePosition.Move_Character(m_srt_Charater.RETURN_CHARACTER_TYPE(), 
                    m_srt_Charater.RETURN_CHARACTER_DIRECTION(), m_srt_Charater.RETURN_CHARACTER_TILE_NUM(), false);
                break;
        }
    }
}
