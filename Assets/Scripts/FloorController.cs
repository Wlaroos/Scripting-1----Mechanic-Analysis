using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] public int _maxRooms;
    public int _currentRooms;
    public bool _stopped;

    private bool done;


    

    private void Update()
    {
        if(_stopped == true && done == false)
        {
            Invoke("Event", 5f);
            Debug.Log("Wait");
            done = true;
        }
    }

    private void Event()
    {
        Debug.Log("Event");
        RoomSpawner[] myItems = FindObjectsOfType(typeof(RoomSpawner)) as RoomSpawner[];

        foreach (RoomSpawner item in myItems)
        {
            item.Test();
        }
    }

}
