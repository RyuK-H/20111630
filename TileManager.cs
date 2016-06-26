using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    private Tile              m_srt_Tile;
    public GameObject         Pre_TILE;
    public static List<Tile>  Tile_List;
    public static List<Tile>  Tile_Second_List;
    public static bool[]      Check_Second_Floor   = new bool[81];
    private int               Tile_Width_Num       = 9;
    private int               Tile_height_Num      = 9;
    private float             Tile_Width           = 0.575f;
    private float             Tile_Height          = 0.738f;
    private float             Standard_Tile_Width  = -2.3f;
    private float             Standard_Tile_height = .0f;
    private float             Standard_Tile_Y      = 0.49f;
    private int               Tile_Num             = 0;

    public void Init()
    {
        INGMAE_INIT();

        Tile_List        = new List<Tile>();
        Tile_Second_List = new List<Tile>();
        SETTING_TILE();
        gameObject.SetActive(false);
    }

    public void SETTING_TYPE(int[] int_arr)
    {
        INGMAE_INIT();

        for (int i = 0; i <= Tile_List.Count - 1; i ++)
        {
            int Second_Floor = int_arr[i] / 10;
            if(Second_Floor == 0)
                Tile_List[i].Set_Tile_Type((TILE_TYPE)int_arr[i]);
            else if(Second_Floor != 0)
            { 
                int First_Floor = int_arr[i] % 10;
                Tile_List[i].Set_Tile_Type((TILE_TYPE)First_Floor);
                Check_Second_Floor[i] = true;

                Vector3 pos = new Vector3( Tile_List[i].transform.position.x,
                    Standard_Tile_Y, Tile_List[i].transform.position.z);

                CREATE_SECOND_TILE(pos, Second_Floor);
            }
        }
    }

    public void Destroy_Second_Tile()
    {
        for (int i = 0; i <= Tile_Second_List.Count - 1; i++)
        {
            Destroy(Tile_Second_List[i].gameObject);
        }

        Tile_Second_List.Clear();
    }

    #region SETTING_TILE
    private void SETTING_TILE()
    {
        for (int height = 0; height < Tile_height_Num; ++height)
        {
            for (int width = 0; width < Tile_Width_Num; ++width)
            {
                Tile_Num = (height * Tile_Width_Num) + width;

                Vector3 pos = new Vector3((Standard_Tile_Width + (width * Tile_Width)),
                    .0f, Standard_Tile_height - (Tile_Height * height));

                CREATE_FIRST_TILE(pos, Tile_Num);
            }
        }
    }
    #endregion

    #region CREATE_TILE
    private void CREATE_FIRST_TILE(Vector3 pos, int tile_num)
    {
        GameObject Temp = Instantiate(Pre_TILE) as GameObject;
        m_srt_Tile  = Temp.GetComponent("Tile") as Tile;
        
        m_srt_Tile.Set_Pos(pos);

        m_srt_Tile.name = string.Format("{0}_{1}", "Tile", tile_num);
        m_srt_Tile.transform.parent = transform;

        Tile_List.Add(m_srt_Tile);
    }
    #endregion

    #region CREATE_SECOND_TILE
    private void CREATE_SECOND_TILE(Vector3 pos, int tile_type)
    {
        GameObject Temp = Instantiate(Pre_TILE) as GameObject;
        m_srt_Tile = Temp.GetComponent("Tile") as Tile;

        m_srt_Tile.Set_Pos(pos);
        m_srt_Tile.Set_Tile_Type((TILE_TYPE)tile_type);

        m_srt_Tile.name = string.Format("{0}_{1}", "Second_", "Tile");
        m_srt_Tile.transform.parent = transform;

        Tile_Second_List.Add(m_srt_Tile);
    }
    #endregion

    private void INGMAE_INIT()
    {
        for (int i = 0; i < 81; i++) { Check_Second_Floor[i] = false; }
    }

    public List<Tile> RETURN_LIST(){ return Tile_List; }
    public bool[] RETURN_SECOND_FLOOR() { return Check_Second_Floor; }
}