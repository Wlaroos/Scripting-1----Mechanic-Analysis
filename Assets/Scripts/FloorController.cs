using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] public int _maxRooms;
    public int _currentRooms;
    public bool _stopped;

    private RoomTemplates templates;
    private int rand;
    private bool done;

    [SerializeField] public float _spawnSpeed;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        rand = Random.Range(0, templates._startRooms.Length);
        Instantiate(templates._startRooms[rand], transform.position, Quaternion.identity);
        _currentRooms++;
    }

    private void Update()
    {
        if(_stopped == true && done == false)
        {
            Invoke("Event", _spawnSpeed + 1f);
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
