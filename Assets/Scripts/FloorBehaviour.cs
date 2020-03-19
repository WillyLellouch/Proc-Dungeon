using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBehaviour : MonoBehaviour
{
    public GameObject northDoor, southDoor, eastDoor, westDoor;
    public bool north, south, east, west;
    // Start is called before the first frame update
    void Start()
    {
        northDoor.SetActive(north);
        southDoor.SetActive(south);
        eastDoor.SetActive(east);
        westDoor.SetActive(west);
    }

    // Update is called once per frame
    void Update()
    {
        northDoor.SetActive(north);
        southDoor.SetActive(south);
        eastDoor.SetActive(east);
        westDoor.SetActive(west);
    }
}
