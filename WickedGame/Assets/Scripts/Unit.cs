using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputManager;

public abstract class Unit : MonoBehaviour
{
    public Int2 pos;
    protected Health hp;
    public Direction facingDirection;
    public GameObject visualRepresentation;

    protected virtual void RefreshForNewWorld()
    {
        Cell newCell = GridHolder.GetRandomWalkableCell();
        newCell.walkable = false;
        pos.x = newCell.GetX();
        pos.y = newCell.GetY();
        this.transform.position = new Vector3(pos.x, 0, pos.y);
    }

    public virtual void MoveAlongDirection(Direction direction)
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

        MakeCurrentCellWalkable();

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


        RotateToFaceDirection(direction);
        MakeCurrentCellNotWalkable();
    }

    private void MakeCurrentCellNotWalkable()
    {
        GridHolder.SetWalkableCell(pos.x, pos.y, false);
    }

    private void MakeCurrentCellWalkable()
    {
        GridHolder.SetWalkableCell(pos.x, pos.y, true);
    }

    private void RotateToFaceDirection(Direction direction)
    {
        Direction prevDirection = facingDirection;
        facingDirection = direction;

        if (prevDirection == facingDirection)
        {
            ServiceLocator.GetDebugProvider().Log("Same direction", LogType.Log);
            return;
        }

        switch (prevDirection)
        {
            case Direction.Up:
                switch (facingDirection)
                {
                    case Direction.Down:
                        visualRepresentation.transform.Rotate(new Vector3(0, 180, 0));
                        break;
                    case Direction.Right:
                        visualRepresentation.transform.Rotate(new Vector3(0, 90, 0));
                        break;
                    case Direction.Left:
                        visualRepresentation.transform.Rotate(new Vector3(0, -90, 0));
                        break;
                }
                break;
            case Direction.Down:
                switch (facingDirection)
                {
                    case Direction.Up:
                        visualRepresentation.transform.Rotate(new Vector3(0, 180, 0));
                        break;
                    case Direction.Right:
                        visualRepresentation.transform.Rotate(new Vector3(0, -90, 0));
                        break;
                    case Direction.Left:
                        visualRepresentation.transform.Rotate(new Vector3(0, 90, 0));
                        break;
                }
                break;
            case Direction.Right:
                switch (facingDirection)
                {
                    case Direction.Up:
                        visualRepresentation.transform.Rotate(new Vector3(0, -90, 0));
                        break;
                    case Direction.Down:
                        visualRepresentation.transform.Rotate(new Vector3(0, 90, 0));
                        break;
                    case Direction.Left:
                        visualRepresentation.transform.Rotate(new Vector3(0, 180, 0));
                        break;
                }
                break;
            case Direction.Left:
                switch (facingDirection)
                {
                    case Direction.Up:
                        visualRepresentation.transform.Rotate(new Vector3(0, 90, 0));
                        break;
                    case Direction.Down:
                        visualRepresentation.transform.Rotate(new Vector3(0, -90, 0));
                        break;
                    case Direction.Right:
                        visualRepresentation.transform.Rotate(new Vector3(0, 180, 0));
                        break;

                }
                break;
            case Direction.NONE:
                ServiceLocator.GetDebugProvider().Log("this should never happen", LogType.Error);
                break;
            default:
                break;
        }
    }
}
