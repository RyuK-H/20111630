using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour {

    public GameObject[]                CharacterList;
    public GameObject[]                ObjectList;
    public GameObject[]                GoalObject;
    public GameObject                  obj_SupervisePosition;
    public static int[]                GOAL_ARR = new int[81];               
    private SupervisePosition          m_srt_SupervisePosition;
    private Character                  m_srt_Character;
    private Object                     m_srt_Object;
    private List<Tile>                 Tile_List;
    public static List<Character>      Character_List;
    private List<Object>               Goal_List;
    private bool[]                     Check_Second_Floor = new bool[81];
    private Vector3                    C_Pos;
    private Vector3                    G_Pos;
    private float                      C_Pos_Y = 0.47f;

    public void Init(List<Tile> tile_list, bool[] check_second_floor)
    {
        for (int i = 0; i < 81; i++)
            GOAL_ARR[i] = 99;
        Character_List          = new List<Character>();
        Goal_List               = new List<Object>();
        Tile_List               = tile_list;
        Check_Second_Floor      = check_second_floor;
        m_srt_SupervisePosition = obj_SupervisePosition.GetComponent("SupervisePosition") as SupervisePosition;
        m_srt_SupervisePosition.Init();
    }

    public void SETTING_CHARACTER(int[] character_arr)
    {
        int i = 0;
        int CPos, CNum, CDir; 

        do
        {
            CPos = character_arr[i];
            CNum = character_arr[i + 1];
            CDir = character_arr[i + 2];

            Create_Character(CPos, CNum, CDir);

            i += 3;
        } while (i <= character_arr.Length - 1);

        // Init. Character_List & Tile_List to SupervisePosition Script
        m_srt_SupervisePosition.Init_Character_List();
    }

    public void SETTING_GOAL(int[] goal_arr)
    {
        int i = 0;
        int Gpos, GNum;

        do
        {
            Gpos = goal_arr[i];
            GNum = goal_arr[i + 1];

            GOAL_ARR[Gpos] = GNum; 

            Create_Goal(Gpos, GNum);

            i += 2;
        } while (i <= goal_arr.Length - 1);
    }

    private void Create_Goal(int gpos, int gnum)
    {
        GameObject Temp = Instantiate(GoalObject[gnum]) as GameObject;
        m_srt_Object    = Temp.GetComponent("Object") as Object;

        Set_Goal_Pos(gpos);
        m_srt_Object.SETTING_GOAL_POSITION(G_Pos);

        Goal_List.Add(m_srt_Object);
    }

    private void Create_Character(int cpos, int cnum, int cdir)
    {
        GameObject Temp = Instantiate(CharacterList[cnum]) as GameObject;
        m_srt_Character = Temp.GetComponent("Character") as Character;

        m_srt_Character.SETTING_CHARACTER_TYPE(cnum);
        m_srt_Character.SETTING_CHARACTER_DIRECTION(cdir);
        m_srt_Character.INIT_CHARACTER_TILE_NUM(cpos);
        Set_Pos(cpos);
        m_srt_Character.SETTING_CHARACTER_POSITION(C_Pos);

        Character_List.Add(m_srt_Character);
    }

    private void Set_Goal_Pos(int gpos)
    {
        int floor;
        if (!(Check_Second_Floor[gpos])) { floor = 1; }
        else { floor = 2; }

        G_Pos = new Vector3(Tile_List[gpos].transform.position.x,
                (C_Pos_Y * floor) + 0.31f, Tile_List[gpos].transform.position.z);
    }

    private void Set_Pos(int cpos)
    {
        int floor;
        if (!(Check_Second_Floor[cpos])) { floor = 1; }
        else { floor = 2; }

        C_Pos = new Vector3(Tile_List[cpos].transform.position.x,
                C_Pos_Y * floor, Tile_List[cpos].transform.position.z);
    }

    public void Create_Object()
    {

    }

    public void Destroy_Object()
    {
        for (int i = 0; i < 81; i++)
            GOAL_ARR[i] = 99;
    }

    // Destroy Character List & SupervisePosition Character List Clear
    public void Destroy_Character()
    {
        for (int i = 0; i <= Character_List.Count - 1; i++)
            Destroy(Character_List[i].gameObject);         

        for (int i = 0; i <= Goal_List.Count - 1; i++)
            Destroy(Goal_List[i].gameObject);

        Character_List.Clear();
        Goal_List.Clear();
        m_srt_SupervisePosition.Init_After();
    }

    public void INIT_SECONDE_FLOOR(bool[] check_second_floor)
    {
        Check_Second_Floor = check_second_floor;
    }
}
