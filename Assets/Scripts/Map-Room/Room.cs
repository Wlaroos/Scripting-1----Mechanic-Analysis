using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    public Vector2 gridPos;

    public int type;

    public bool doorTop;
    public bool doorBot;
    public bool doorLeft;
    public bool doorRight;

    public Room(Vector2 _gridpos, int _type)
    {
        gridPos = _gridpos;
        type = _type;
    }
}
