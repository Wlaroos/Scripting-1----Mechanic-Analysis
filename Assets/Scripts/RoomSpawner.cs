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
    private int rand;
    private bool spawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.5f);
    }

    private void Spawn()
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

            spawned = true;
        }
    }

}
