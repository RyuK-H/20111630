using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SupervisePosition : MonoBehaviour {
    
    public  GameObject      obj_GameRule;
    public  static bool     Second_Move        = false;
    private GameRule        m_srt_GameRule;
    private int             Character_Direction;
    private float           C_Pos_Y = 0.47f;
    private float           Pos_Y;
    private Character       m_srt_Character;
    private Tile            m_srt_Tile;
    private int[]           Tile_arr           = new  int [81];
    private bool            Cant_Move          = false;
    private bool            Stop_Sign          = false;
    private bool            Active             = false;
    private bool            Character_Climb    = false;
    private int             Ice_Character;

    public void Init()
    {
        m_srt_GameRule = obj_GameRule.GetComponent("GameRule") as GameRule;
        Init_Character_Arr();
    }

    public bool Move_Character(int c_type, int c_direction, int tile_num , bool second_move)
    {
        Stop_Sign = false;

        switch (c_direction)
        {
            case 1:
                Cant_Move = Right_Move(c_type, tile_num, c_direction, second_move);
                break;
            case 2:
                Cant_Move = Down_Move(c_type, tile_num, c_direction, second_move);
                break;
            case 3:
                Cant_Move = Left_Move(c_type, tile_num, c_direction, second_move);
                break;
            case 4:
                Cant_Move = Up_Move(c_type, tile_num, c_direction, second_move);
                break;
            default:
                print("default");
                break;
        }
        return Cant_Move;
    }	

    private bool Right_Move(int c_type, int tile_num, int c_direction, bool second_move)
    {
        // 오른쪽으로 향할때, 더 이상 이동할 곳이 있나, 없나 판별
        if(tile_num % 9 == 8) { return true; }
        else
        {
            int next_tile = tile_num + 1;
            return Move_Function(c_type, tile_num, next_tile, c_direction, second_move);
        }
    }

    private bool Left_Move(int c_type, int tile_num, int c_direction, bool second_move)
    {
        if (tile_num % 9 == 0) { return true; }
        else
        {
            int next_tile = tile_num - 1;
            return Move_Function(c_type, tile_num, next_tile, c_direction, second_move);
        }
    }
        
    private bool Down_Move(int c_type, int tile_num, int c_direction, bool second_move)
    {
        if (tile_num + 9 > 80) { return true; }
        else
        {
            int next_tile = tile_num + 9;
            return Move_Function(c_type, tile_num, next_tile, c_direction, second_move);
        }
    }

    private bool Up_Move(int c_type, int tile_num, int c_direction, bool second_move)
    {
        if (tile_num - 9 < 0) { return true; }
        else
        {
            int next_tile = tile_num - 9;
            return Move_Function(c_type, tile_num, next_tile, c_direction, second_move);
        }
    }

    private bool Move_Function(int c_type, int tile_num, int next_tile, int c_direction, bool second_move)
    {
        m_srt_Tile    = TileManager.Tile_List[next_tile].GetComponent("Tile") as Tile;

        // 만약 다음 타일이 비활성 타일이거나 캐릭
        if (m_srt_Tile.CHECK_DIABLE_TILE()) { return true; }
        else
        {
            if (m_srt_Tile.CHECK_ICE_TILE())
            {
               // 얼음
               Second_Move = true;
               Ice_Character = next_tile;
               next_tile += 1;
            }

            if (Tile_arr[next_tile] != 99)
            {
                m_srt_Character = ObjectManager.Character_List[Tile_arr[next_tile]].GetComponent("Character") as Character;
                // 재귀 함수 진입시에는 direction은 변동없음
                Stop_Sign = Move_Character(m_srt_Character.RETURN_CHARACTER_TYPE(), c_direction,
                    m_srt_Character.RETURN_CHARACTER_TILE_NUM(), Second_Move);
                
                // Setactive false 상태이면.
                if (m_srt_Character.RETURN_CHARACTER_ACTIVE())
                {
                    Stop_Sign = false;
                    Tile_arr[next_tile] = 99;
                }
            }

            if (!Stop_Sign)
            {

                if (TileManager.Check_Second_Floor[next_tile])
                {
                    Pos_Y = C_Pos_Y * 2;
                    Character_Climb = true;
                }
                else
                {
                    Pos_Y = C_Pos_Y;
                    Character_Climb = false;
                }

                m_srt_Tile = TileManager.Tile_List[next_tile].GetComponent("Tile") as Tile;

                /*if(m_srt_Tile.CHECK_ICE_TILE())
                {
                    next_tile += 1;
                }*/

                Vector3 Pos = new Vector3(TileManager.Tile_List[next_tile].transform.position.x,
                Pos_Y, TileManager.Tile_List[next_tile].transform.position.z);
                
                if (Second_Move && Ice_Character == next_tile - 1)
                {
                    print("dqw");
                }

                m_srt_GameRule.Move_Fuction(c_type, Pos, Character_Climb);

                Change_Info(tile_num, next_tile, c_type);
            }

            return Stop_Sign;
        }        
    }

    private void Change_Info(int tile_num, int change_tile_num, int c_type)
    {
        m_srt_Character = ObjectManager.Character_List[c_type].GetComponent("Character") as Character;
        m_srt_Character.CHANGE_CHARACTER_TILE_NUM(change_tile_num);
        Tile_arr[change_tile_num] = Tile_arr[tile_num];
        Tile_arr[tile_num] = 99;
    }        

    public void Init_Character_List()
    {
        for(int i = 0; i <= ObjectManager.Character_List.Count - 1; i++)
        {
            m_srt_Character = ObjectManager.Character_List[i].GetComponent("Character") as Character;
            Tile_arr[m_srt_Character.RETURN_CHARACTER_TILE_NUM()] = i;
        }
    }

    public void Init_Character_Direction(int character_direction)
    {
        Character_Direction = character_direction;
    }

    public void Change_Tile_Arr(int num)
    {
        Tile_arr[num] = 99;
    }

    // If Stage Done, For the next stage, Init List
    public void Init_After()
    {
        m_srt_GameRule.Init_After();
        Init_Character_Arr();
        Second_Move = false;
    }

    // (터치시 바로) 처음 터치한 캐릭터의 넘버, 방향을 받아오고, 그 것을 GameRule에 넘겨준다.
    public void Init_Touch_Character_Num(int num, int dir)
    {
        m_srt_GameRule.Init_Touch_Character_Num(num, dir);
    }

    // 초기 초기화
    private void Init_Character_Arr()
    {
        for (int i = 0; i < 81; i++)
            Tile_arr[i] = 99;
    }
}