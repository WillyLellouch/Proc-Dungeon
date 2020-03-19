using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGeneration : MonoBehaviour
{
    Vector2 dungeonSize = new Vector2(4, 4);

    Room[,] rooms;

    List<Vector2> takenPositions = new List<Vector2>();

    int gridSizeX, gridSizeY, numberOfRooms = 20;

    public GameObject roomFloor;

    // Start is called before the first frame update
    void Start()
    {
        if(numberOfRooms >= (dungeonSize.x * 2) * (dungeonSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((dungeonSize.x * 2) * (dungeonSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(dungeonSize.x);
        gridSizeY = Mathf.RoundToInt(dungeonSize.y);
        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            SceneManager.LoadScene("Test Generate");
        }
    }

    void CreateRooms()
    {
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, RoomType.ENTRANCE_ROOM);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            float randomPerc = ((float)i) / ((float)numberOfRooms - 1);
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            checkPos = NewPosition();
            if (NumberOfNeighbours(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iter = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iter++;
                } while (NumberOfNeighbours(checkPos, takenPositions) > 1 && iter < 100);
                if (iter >= 50)
                {
                    print("error: could not create with fewer neighbours than : " + NumberOfNeighbours(checkPos, takenPositions));
                }
            }
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, RoomType.EMPTY_ROOM);
            takenPositions.Insert(0, checkPos);
        }
    }

    void SetRoomDoors()
    {
        for (int x = 0; x < (gridSizeX * 2); x++)
        {
            for (int y = 0; y < (gridSizeY * 2); y++)
            {
                if (rooms[x, y] == null)
                {
                    continue;
                }
                Vector2 gridPos = new Vector2(x, y);
                if (y - 1 < 0)
                {
                    rooms[x, y].south = false;
                }
                else
                {
                    rooms[x, y].south = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2)
                {
                    rooms[x, y].north = false;
                }
                else
                {
                    rooms[x, y].north = (rooms[x, y + 1]) != null;
                }
                if (x - 1 < 0)
                {
                    rooms[x, y].west = false;
                }
                else
                {
                    rooms[x, y].west = (rooms[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2)
                {
                    rooms[x, y].east = false;
                }
                else
                {
                    rooms[x, y].east = (rooms[x + 1, y] != null);
                }
            }
        }
    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 newPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool longitude = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (longitude)
            {
                if (positive)
                {
                    y++;
                }
                else
                {
                    y--;
                }
            }
            else
            {
                if (positive)
                {
                    x++;
                }
                else
                {
                    x--;
                }
            }
            newPos = new Vector2(x, y);
        } while (takenPositions.Contains(newPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        return newPos;
    }

    int NumberOfNeighbours(Vector2 checkPos, List<Vector2> usedPositions)
    {
        int result = 0;
        if (usedPositions.Contains(checkPos + Vector2.right))
        {
            result++;
        }
        if (usedPositions.Contains(checkPos + Vector2.left))
        {
            result++;
        }
        if (usedPositions.Contains(checkPos + Vector2.up))
        {
            result++;
        }
        if (usedPositions.Contains(checkPos + Vector2.down))
        {
            result++;
        }
        return result;
    }

    Vector2 SelectiveNewPosition()
    {
        int index = 0, iter = 0;
        int x = 0, y = 0;
        Vector2 newPos = Vector2.zero;
        do
        {
            iter = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                iter++;
            } while (NumberOfNeighbours(takenPositions[index], takenPositions) > 1 && iter < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool longitude = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (longitude)
            {
                if (positive)
                {
                    y++;
                }
                else
                {
                    y--;
                }
            }
            else
            {
                if (positive)
                {
                    x++;
                }
                else
                {
                    x--;
                }
            }
            newPos = new Vector2(x, y);
        } while (takenPositions.Contains(newPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (iter >= 100)
        {
            print("error : could not find position with only one neighbour");
        }
        return newPos;
    }

    void DrawMap()
    {
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue;
            }
            Vector3 realPos = new Vector3(room.gridPos.x * 60, 0, room.gridPos.y * 40);
            GameObject instance = Instantiate(roomFloor, realPos, Quaternion.identity);
            FloorBehaviour floorBehaviour = instance.GetComponent<FloorBehaviour>();
            floorBehaviour.north = room.north;
            floorBehaviour.south = room.south;
            floorBehaviour.east = room.east;
            floorBehaviour.west = room.west;
        }
    }
}
