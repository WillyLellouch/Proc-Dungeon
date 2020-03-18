using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    Vector2 dungeonSize = new Vector2(4, 4);

    Room[,] rooms;

    List<Vector2> takenPositions = new List<Vector2>();

    int gridSizeX, gridSizeY, numberOfRooms = 20;

    public GameObject roomWhiteObj;

    // Start is called before the first frame update
    void Start()
    {
        if(numberOfRooms >= (dungeonSize.x * 2) * (dungeonSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((dungeonSize.x * 2) * (dungeonSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(dungeonSize.x);
        gridSizeY = Mathf.RoundToInt(dungeonSize.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateRooms()
    {
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, RoomType.ENTRANCE_ROOM);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;

    }
}
