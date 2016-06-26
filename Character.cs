using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private int  Character_Direction;
    private int  Character_Roatation;
    private int  Character_Type;
    private int  Character_Tile_Num;
    private bool Character_Active;

    public void SETTING_CHARACTER_TYPE(int ctype)
    {
        Character_Active = false;
        Character_Type   = ctype;
    }

    public void SETTING_CHARACTER_DIRECTION(int cdir)
    {
        Character_Direction = cdir;
        Change_Character_Direction();
    }

    public void SETTING_CHARACTER_POSITION(Vector3 pos)
    {
        this.gameObject.transform.position = pos;
    }

    private void Change_Character_Direction()
    {
        Character_Roatation = Character_Direction * 90;
        Vector3 Pos = new Vector3(.0f, Character_Roatation, .0f);
        
        this.gameObject.transform.Rotate(Pos);
    }

    public int RETURN_CHARACTER_TYPE() { return Character_Type; }
    public int RETURN_CHARACTER_DIRECTION() { return Character_Direction; }
    public int RETURN_CHARACTER_TILE_NUM() { return Character_Tile_Num; }
    public void INIT_CHARACTER_TILE_NUM(int tile_num) { Character_Tile_Num = tile_num; }
    public void CHANGE_CHARACTER_TILE_NUM(int tile_num) { Character_Tile_Num = tile_num; }
    public void CHARACTER_DESTROY() { Character_Active = true; }
    public bool RETURN_CHARACTER_ACTIVE() { return Character_Active; }
}