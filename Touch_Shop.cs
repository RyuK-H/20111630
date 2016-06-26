using UnityEngine;
using System.Collections;

public class Touch_Shop : MonoBehaviour
{
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
        }
    }
}
