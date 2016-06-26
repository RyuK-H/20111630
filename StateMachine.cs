using UnityEngine;
using System.Collections;

public enum STATE { WAIT, GAME, SHOP }

public class StateMachine : MonoBehaviour{

    public static int        GAMESTATE = 0;
    public static bool       TOUCH;
    public  GameObject       obj_DataManager;
    private DataManager      m_srt_DataManager;
    public  GameObject       obj_TouchManager;
    private TouchManager     m_srt_TouchManager;
    public  GameObject       obj_EffectManager;
    private EffectManager    m_srt_EffectManager;
    public  GameObject       obj_GameRule;
    private GameRule         m_srt_GameRule;
    private bool             GAME_INIT;
    private int              Temp_GAMESTATE;
    private int              PastStage;
    
    public void Init()
    {
        PastStage              = 0;
        Temp_GAMESTATE         = GAMESTATE;
        GAME_INIT              = false;
        TOUCH                  = true;
        m_srt_DataManager      = obj_DataManager.GetComponent("DataManager") as DataManager;
        m_srt_TouchManager     = obj_TouchManager.GetComponent("TouchManager") as TouchManager;
        m_srt_EffectManager    = obj_EffectManager.GetComponent("EffectManager") as EffectManager;
        m_srt_GameRule         = obj_GameRule.GetComponent("GameRule") as GameRule;
        m_srt_GameRule.Init();   
        m_srt_DataManager.Init();
        m_srt_TouchManager.Init();
    }

    public void Play()
    {
        if(TOUCH)
            m_srt_TouchManager.Touching(GAMESTATE);

        // Init, If Done Game Mode
        if (Temp_GAMESTATE != GAMESTATE)
        {
            PastStage      = Temp_GAMESTATE;
            Temp_GAMESTATE = GAMESTATE;

            if (PastStage == 1 && GAMESTATE != 1)
            {
                Init_GameMode();
            }
        }

        switch ((STATE)GAMESTATE)
        {
            case STATE.WAIT:
                //print("WAIT");                
                break;

            case STATE.GAME:
                if(!GAME_INIT)
                {
                    State_Game_Init();
                    GAME_INIT = true;
                }
                State_Game_Play();
                break;

            case STATE.SHOP:
                break;
        }
    }

    private void Init_GameMode()
    {
        m_srt_DataManager.Destroy_Character();
        m_srt_DataManager.Destroy_Second_Floor();
        m_srt_DataManager.SETACIVE_FALSE();
        GAME_INIT = false;
    }
    // Game Mode Initialize - Tile, Character, Object
    #region State_Game_Mode
    private void State_Game_Init()
    {
        m_srt_DataManager.TileData();
        m_srt_DataManager.CharacterData();
        m_srt_DataManager.GoalData();
        m_srt_DataManager.INIT_SECOND_FLOOR();
        GAME_INIT = true;
    }

    private void State_Game_Play()
    {
        m_srt_GameRule.Start_Game();
    }
    #endregion

    #region State_Wait_Mode
    private void State_Wait_Play()
    {

    }
    #endregion
}