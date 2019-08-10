using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputManager;

public abstract class Unit : MonoBehaviour
{
    public Int2 pos;
    protected Health hp;



    public void MoveAlongDirection(Direction direction)
    {
        Debug.Log("Move: " + direction);
        Vector3Int toAdd = Vector3Int.zero;
        switch (direction)
        {
            case Direction.Up:
                toAdd += new Vector3Int(0, 0, 1);
                break;
            case Direction.Down:
                toAdd += new Vector3Int(0, 0, -1);
                break;
            case Direction.Left:
                toAdd += new Vector3Int(-1, 0, 0);
                break;
            case Direction.Right:
                toAdd += new Vector3Int(1, 0, 0);
                break;
            default:
                break;
        }

        if (GridHolder.IsWalkable(pos.x + toAdd.x, pos.y + toAdd.z) == false)
        {
            return;
        }
        pos.x += toAdd.x;
        pos.y += toAdd.z;
        if (GetComponent<iTween>())
        {
            Destroy(GetComponent<iTween>());
        }
        iTween.MoveAdd(this.gameObject, iTween.Hash(
            "amount", toAdd.GetAsVector3(),
            "time", Rules.ruleSet.PLAYER_MOVEMENT_TICK / 2,
            "easeType", iTween.EaseType.easeInOutSine
            ));
        //iTween.MoveAdd(this.gameObject, toAdd, Rules.ruleSet.MOVEMENT_TICK / 2);
    }


}
