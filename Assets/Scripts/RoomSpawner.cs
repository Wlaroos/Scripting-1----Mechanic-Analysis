using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] int _openingDir;
    // 1 = Up Room (Down Door)
    // 2 = Down Roomn (Up Door)
    // 3 = Right Room (Left Door)
    // 4 = Left Room (Right Door)

    private RoomTemplates templates;
    private FloorController controller;
    private int rand;
    private bool spawned = false;

    private bool _ss;

    private Collider2D _test;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<FloorController>();
        if (controller._stopped == false)
        { 
             Invoke("Spawn", 1f);
        }
    }

    private void Update()
    {

    }

    private void Spawn()
    {
        if (controller._currentRooms < controller._maxRooms)
        {
            if (spawned == false)
            {
                if (_openingDir == 1)
                {
                    // Spawn a room UP
                    rand = Random.Range(0, templates._upRooms_D.Length);
                    Instantiate(templates._upRooms_D[rand], transform.position, Quaternion.identity);
                }
                else if (_openingDir == 2)
                {
                    // Spawn a room DOWN
                    rand = Random.Range(0, templates._downRooms_U.Length);
                    Instantiate(templates._downRooms_U[rand], transform.position, Quaternion.identity);
                }
                else if (_openingDir == 3)
                {
                    // Spawn a room to the LEFT
                    rand = Random.Range(0, templates._leftRooms_R.Length);
                    Instantiate(templates._leftRooms_R[rand], transform.position, Quaternion.identity);
                }
                else if (_openingDir == 4)
                {
                    // Spawn a room to the RIGHT
                    rand = Random.Range(0, templates._rightRooms_L.Length);
                    Instantiate(templates._rightRooms_L[rand], transform.position, Quaternion.identity);
                }

                controller._currentRooms++;
                spawned = true;
            }
        }
        else
        {
            controller._stopped = true;
        }
    }

    public void Test()
    {

        if (_test == null && spawned == false)
        {
            if (_openingDir == 1)
            {
                // Spawn a room UP
                controller._currentRooms++;
                Instantiate(templates._endRooms[0], transform.position, Quaternion.identity);
            }
            else if (_openingDir == 2)
            {
                // Spawn a room DOWN
                controller._currentRooms++;
                Instantiate(templates._endRooms[1], transform.position, Quaternion.identity);
            }
            else if (_openingDir == 3)
            {
                // Spawn a room to the LEFT
                controller._currentRooms++;
                Instantiate(templates._endRooms[2], transform.position, Quaternion.identity);
            }
            else if (_openingDir == 4)
            {
                // Spawn a room to the RIGHT
                controller._currentRooms++;
                Instantiate(templates._endRooms[3], transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _test = other;

        
        
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>() != null)
            {
                if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                {
                    // Spawn walls blocking off any openings 
                    Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                    controller._currentRooms++;
                    //Destroy(gameObject);
                }
            }

            spawned = true;
        }

        
    }

}
