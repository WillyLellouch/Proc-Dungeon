using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    EMPTY_ROOM,
    TREASURE_ROOM,
    CORRIDOR,
    BOSS_ROOM,
    ENTRANCE_ROOM
}

public class Room
{
    public Vector2 gridPos;

    public RoomType type;

    public bool north, east, south, west;

    public Room(Vector2 _gridPos, RoomType _type)
    {
        gridPos = _gridPos;
        type = _type;
    }
}
