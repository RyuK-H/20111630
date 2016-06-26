using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameRule : MonoBehaviour
{

    public GameObject obj_SupervisePosition;
    private SupervisePosition m_srt_SupervisePosition;
    private bool Check_Move;
    private Vector3[] C_Pos = new Vector3[6];
    private bool[] Check_Character_Move = new bool[6];
    private bool[] Check_Character_UpDown = new bool[6];
    private int  Touch_Character_Num;
    private bool Touch_Character_Dir;
    private bool Character_Climb;
    private bool Check_Move_Done;
    private bool[] C_X = new bool[6];
    private bool[] C_Y = new bool[6];
    private bool[] C_Z = new bool[6];
    private bool[] Check_Move_Done1 = new bool[6];
    private float M_X;
    private float M_Y;
    private float M_Z;

    public void Init()
    {
        m_srt_SupervisePosition = obj_SupervisePosition.GetComponent("SupervisePosition") as SupervisePosition;
        M_X = 0.8f;         // 0.575
        M_Y = 0.6f;         // 0.47
        M_Z = 1.0f;         // 0.738
        Check_Move          = false;
        Touch_Character_Dir = false;
        Character_Climb     = false;
        Check_Move_Done     = false;
        Init_Check_List();
    }

    public void Start_Game()
    {
        if (Check_Move)
        {
            StateMachine.TOUCH = false;
            for (int i = 0; i < 6; i++)
            {
                // 모든게 완료 되었다면.
                if (!Check_Character_Move[i]) { Check_Move_Done1[i] = true; }

                if (Check_Move_Done1[i]) { }
                else
                {
                    if (C_X[i])
                    {
                        if (Touch_Character_Dir) { ObjectManager.Character_List[i].transform.position += new Vector3(M_X * Time.deltaTime, .0f, .0f); }
                        else if (!Touch_Character_Dir) { ObjectManager.Character_List[i].transform.position += new Vector3(-M_X * Time.deltaTime, .0f, .0f); }
                    }

                    if (C_Y[i])
                    {
                        if (Check_Character_UpDown[i]) { ObjectManager.Character_List[i].transform.position += new Vector3(.0f, M_Y * Time.deltaTime, .0f); }
                        else if (!Check_Character_UpDown[i]) { ObjectManager.Character_List[i].transform.position += new Vector3(.0f, -M_Y * Time.deltaTime, .0f); }
                    }

                    if (C_Z[i])
                    {
                        if (Touch_Character_Dir) { ObjectManager.Character_List[i].transform.position += new Vector3(.0f, .0f, M_Z * Time.deltaTime); }
                        else if (!Touch_Character_Dir) { ObjectManager.Character_List[i].transform.position += new Vector3(.0f, .0f, -M_Z * Time.deltaTime); }
                    }
                }
            }

            for(int i = 0; i < 6; i ++)
            {
                if (Touch_Character_Dir)
                {
                    // 조건문 만약 이동을 완료했다면
                    if (C_X[i])
                    {
                        if (ObjectManager.Character_List[i].transform.position.x >= C_Pos[i].x) { Stop(i); }
                    }
                    if (C_Y[Touch_Character_Num] && Check_Character_UpDown[Touch_Character_Num])
                    {
                        if (ObjectManager.Character_List[Touch_Character_Num].transform.position.y >= C_Pos[Touch_Character_Num].y) { Stop(i); }
                    }
                    if (C_Z[i])
                    {
                        if (ObjectManager.Character_List[i].transform.position.z >= C_Pos[i].z) { Stop(i); }
                    }
                }

                else if (!Touch_Character_Dir)
                {
                    if (C_X[i])
                    {
                        if (ObjectManager.Character_List[i].transform.position.x <= C_Pos[i].x) { Stop(i); }
                    }
                    if (C_Y[Touch_Character_Num] && !Check_Character_UpDown[Touch_Character_Num])
                    {
                        if (ObjectManager.Character_List[Touch_Character_Num].transform.position.y <= C_Pos[Touch_Character_Num].y) { Done_Move(); }
                    }
                    if (C_Z[i])
                    {
                        if (ObjectManager.Character_List[i].transform.position.z <= C_Pos[i].z) { Stop(i); }
                    }
                }
            }    
            
            for(int i = 0; i < 6; i++)
            {                
                if (!Check_Move_Done1[i])
                {
                    break;
                }
                
                else
                {
                    Character_Line_Up(i);
                    if (i == 5)
                    {
                        Done_Move();
                    }
                }
            }         
        }

        if (!Check_Move && Check_Move_Done)
        {
            // 캐릭터 골인지점 충돌
            for (int i = 0; i < ObjectManager.Character_List.Count - 1; i++)
            {
                Character Temp = ObjectManager.Character_List[i].GetComponent("Character") as Character;

                if (Temp.RETURN_CHARACTER_TYPE() == ObjectManager.GOAL_ARR[Temp.RETURN_CHARACTER_TILE_NUM()])
                {
                    if (Temp.RETURN_CHARACTER_ACTIVE()) { }
                    else
                    {
                        Temp.CHARACTER_DESTROY();
                        ObjectManager.Character_List[i].gameObject.SetActive(false);
                        m_srt_SupervisePosition.Change_Tile_Arr(Temp.RETURN_CHARACTER_TILE_NUM());
                    }
                }
            }
            Check_Move_Done = false;
        }
    }
    private void Stop(int i)
    {
        Check_Move_Done1[i] = true;
    }

    private void Done_Move()
    {
        Check_Move = false;
        StateMachine.TOUCH = true;
        Check_Move_Done = true;
        //Character_Line_Up();
        Init_Check_List();
        SupervisePosition.Second_Move = false;
    }

    public void Move_Fuction(int c_type, Vector3 pos, bool climb)
    {
        Check_Character_Move[c_type] = true;
        C_Pos[c_type] = pos;

        if (ObjectManager.Character_List[c_type].transform.position.x != C_Pos[c_type].x)
        {
            C_X[c_type] = true;
        }

        if (ObjectManager.Character_List[c_type].transform.position.y != C_Pos[c_type].y)
        {
            C_Y[c_type] = true;
            Check_Character_UpDown[c_type] = climb;
        }

        if (ObjectManager.Character_List[c_type].transform.position.z != C_Pos[c_type].z)
        {
            C_Z[c_type] = true;
        }

        // Check_Move = true를 한번 실행하기 위해. 터치한 캐릭터 넘버와 c_type을 비교
        // 터치한 캐릭터는 가장 마지막에 이 함수로 들어오기 때문에.
        if (Touch_Character_Num == c_type)
            Check_Move = true;
    }

    public void Init_Touch_Character_Num(int num, int dir)
    {
        Touch_Character_Num = num;
        if ((dir % 4) == 1 || (dir % 4) == 0) { Touch_Character_Dir = true; }
        else if ((dir % 4) == 2 || (dir % 4) == 3) { Touch_Character_Dir = false; }
    }

    public void Init_After()
    {
        Init_Check_List();
        Check_Move = false;
    }

    // 게임 진행 후 초기화
    #region _INIT_
    private void Init_Check_List()
    {
        for (int i = 0; i < 6; i++)
        {
            Check_Character_Move[i] = false;
            C_X[i] = false;
            C_Y[i] = false;
            C_Z[i] = false;
            Check_Character_UpDown[i] = false;
            Check_Move_Done1[i] = false;
        }
    }

    private void Character_Line_Up(int i)
    {
        /*for (int i = 0; i < 6; i++)
        {*/
            if (!Check_Character_Move[i]) { }
            else
            {
                ObjectManager.Character_List[i].transform.position = C_Pos[i];
            }/*
        }*/
    }
    #endregion
}
