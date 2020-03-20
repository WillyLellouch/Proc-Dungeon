using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBehaviour : MonoBehaviour
{
    public GameObject northDoor, southDoor, eastDoor, westDoor, northWall, westWall, eastWall, southWall;
    public bool north, south, east, west;
    public RoomType type;
    // Start is called before the first frame update
    void Start()
    {
        northDoor.SetActive(north);
        southDoor.SetActive(south);
        eastDoor.SetActive(east);
        westDoor.SetActive(west);
        northWall.SetActive(!north);
        westWall.SetActive(!west);
        southWall.SetActive(!south);
        eastWall.SetActive(!east);
    }

    // Update is called once per frame
    void Update()
    {
        northDoor.SetActive(north);
        southDoor.SetActive(south);
        eastDoor.SetActive(east);
        westDoor.SetActive(west);
        northWall.SetActive(!north);
        westWall.SetActive(!west);
        southWall.SetActive(!south);
        eastWall.SetActive(!east);
    }
}
