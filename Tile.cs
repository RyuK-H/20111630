using UnityEngine;
using System.Collections;

public enum TILE_TYPE {DISABLE = 0, NORMAL = 1, SECOND = 2, ICE = 3}

public class Tile : MonoBehaviour {

    public  Material[]  Tile_Materials;
    private Renderer    Rend;
    private TILE_TYPE   tile_type;

    public void Init(Vector3 _pos, TILE_TYPE _type)
    {
        Rend = this.gameObject.GetComponent<Renderer>();
        
        Set_Tile_Type(_type);
        Set_Tile_Pos(_pos);
    }

    public void Set_Pos(Vector3 _pos)
    {
        Rend = this.gameObject.GetComponent<Renderer>();
        Set_Tile_Pos(_pos);
    }
    
    public void Set_Tile_Type (TILE_TYPE _type)
    {
        tile_type = _type;
        
        switch(tile_type)
        {
            case TILE_TYPE.DISABLE:
                Rend.material = Tile_Materials[0];
                break;

            case TILE_TYPE.NORMAL:
                Rend.material = Tile_Materials[1];
                break;

            case TILE_TYPE.SECOND:
                Rend.material = Tile_Materials[2];
                break;

            case TILE_TYPE.ICE:
                Rend.material = Tile_Materials[3];
                break;
        }
    }

    private void Set_Tile_Pos(Vector3 _pos)
    {
        this.gameObject.transform.position = _pos;
    }

    public bool CHECK_DIABLE_TILE()
    {
        if (tile_type == TILE_TYPE.DISABLE) return true;
        else return false;
    }

    public bool CHECK_ICE_TILE()
    {
        if (tile_type == TILE_TYPE.ICE) return true;
        else return false;
    }

    public bool CHECK_NORMAL_TILE()
    {
        if (tile_type == TILE_TYPE.NORMAL) return true;
        else return false;
    }

    public TILE_TYPE Temp()
    {
        return tile_type;
    }
}
