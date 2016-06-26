using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataManager : MonoBehaviour {

    public  GameObject      obj_TileManager;
    private TileManager     m_srt_TileManager;
    public  GameObject      obj_ObjectManager;
    private ObjectManager   m_srt_ObjectManager;
    private List<Tile>      Tile_List;
    private bool[] Check_Second_Floor = new bool[81];

    public void Init()
    {
        m_srt_TileManager   = obj_TileManager.GetComponent("TileManager") as TileManager;
        m_srt_ObjectManager = obj_ObjectManager.GetComponent("ObjectManager") as ObjectManager;
        m_srt_TileManager.Init();        
        Tile_List           = m_srt_TileManager.RETURN_LIST();
        Check_Second_Floor  = m_srt_TileManager.RETURN_SECOND_FLOOR();
        m_srt_ObjectManager.Init(Tile_List, Check_Second_Floor);
    }

    // Read Tile Data & Change Tile Materials
    public void TileData(/*string file_name*/)
    {
        StreamReader sr = new StreamReader("StageFile/Stage1_1.txt");

        string str_map = sr.ReadToEnd();
        string[] tile_arr = str_map.Split(',');
        int[] int_tile_arr = new int[tile_arr.Length - 1];

        for (int i = 0; i < (int)int_tile_arr.Length; i++)
        {
            string str = tile_arr[i];
            int_tile_arr[i] = int.Parse(str);
        }
        sr.Close();

        obj_TileManager.SetActive(true);
        m_srt_TileManager.SETTING_TYPE(int_tile_arr);
    }

    //Read Character Data & Create Character
    public void CharacterData()
    {
        StreamReader sr = new StreamReader("StageFile/Character1_1.txt");

        string str_map = sr.ReadToEnd();
        string[] character_arr = str_map.Split(',');
        int[] int_character_arr = new int[character_arr.Length - 1];

        for (int i = 0; i < (int)int_character_arr.Length; i++)
        {
            string str = character_arr[i];
            int_character_arr[i] = int.Parse(str);
        }
        sr.Close();

        m_srt_ObjectManager.SETTING_CHARACTER(int_character_arr);
    }

    // Read Goal Data & Create Goal
    public void GoalData()
    {
        StreamReader sr = new StreamReader("StageFile/Goal1_1.txt");

        string str_map = sr.ReadToEnd();
        string[] goal_arr = str_map.Split(',');
        int[] int_goal_arr = new int[goal_arr.Length - 1];

        for (int i = 0; i < (int)int_goal_arr.Length; i++)
        {
            string str = goal_arr[i];
            int_goal_arr[i] = int.Parse(str);
        }
        sr.Close();

        m_srt_ObjectManager.SETTING_GOAL(int_goal_arr);
    }

    // Second floor tile List Destroy
    public void Destroy_Second_Floor()
    {
        m_srt_TileManager.Destroy_Second_Tile();
    }

    // Chracter List Destroy
    public void Destroy_Character()
    {
        m_srt_ObjectManager.Destroy_Character();
    }

    // tile setactive false
    public void SETACIVE_FALSE()
    {
        m_srt_TileManager.gameObject.SetActive(false);
    }

    public List<Tile> RETURN_LIST() { return Tile_List; }
    public bool[] RETURN_SECOND_FLOOR() { return Check_Second_Floor; }
    public void INIT_SECOND_FLOOR()
    {
        Check_Second_Floor = m_srt_TileManager.RETURN_SECOND_FLOOR();
        m_srt_ObjectManager.INIT_SECONDE_FLOOR(Check_Second_Floor);
    }
}
