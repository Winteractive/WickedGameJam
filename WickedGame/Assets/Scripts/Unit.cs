﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputManager;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    public Int2 pos;
    protected Health hp;
    public Direction facingDirection;
    public GameObject visualRepresentation;

    protected virtual void RefreshForNewWorld()
    {
        if (GetComponent<iTween>())
        {
            ServiceLocator.GetDebugProvider().Log("remove itween");
            Destroy(GetComponent<iTween>());
        }
        Cell newCell = GridHolder.GetRandomWalkableCell();
        newCell.walkable = false;
        pos.x = newCell.GetX();
        pos.y = newCell.GetY();
        this.transform.position = new Vector3(pos.x, 0, pos.y);
    }

    public virtual void MoveAlongDirection(Direction direction, float _speed)
    {
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
        if (GetComponent<iTween>())
        {
            Destroy(GetComponent<iTween>());
            this.transform.position = pos.GetAsBoardAlignedVector3Int();
        }
        pos.x += toAdd.x;
        pos.y += toAdd.z;

        iTween.MoveTo(this.gameObject, iTween.Hash(
            "position", new Vector3(pos.x, this.transform.position.y, pos.y),
            "time", _speed,
            "easeType", iTween.EaseType.linear
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
            return;
        }
        float turnTime = 0.3f;
        switch (prevDirection)
        {
            case Direction.Up:
                switch (facingDirection)
                {
                    case Direction.Down:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, 180, 0), turnTime);
                        break;
                    case Direction.Right:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, 90, 0), turnTime);
                        break;
                    case Direction.Left:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, -90, 0), turnTime);
                        break;
                }
                break;
            case Direction.Down:
                switch (facingDirection)
                {
                    case Direction.Up:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, 180, 0), turnTime);
                        break;
                    case Direction.Right:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, -90, 0), turnTime);
                        break;
                    case Direction.Left:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, 90, 0), turnTime);
                        break;
                }
                break;
            case Direction.Right:
                switch (facingDirection)
                {
                    case Direction.Up:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, -90, 0), turnTime);
                        break;
                    case Direction.Down:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, 90, 0), turnTime);
                        break;
                    case Direction.Left:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, 180, 0), turnTime);
                        break;
                }
                break;
            case Direction.Left:
                switch (facingDirection)
                {
                    case Direction.Up:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, 90, 0), turnTime);
                        break;
                    case Direction.Down:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, -90, 0), turnTime);
                        break;
                    case Direction.Right:
                        iTween.RotateAdd(visualRepresentation, new Vector3(0, 180, 0), turnTime);
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
