﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [SerializeField] public GameObject[] _upRooms_D;
    [SerializeField] public GameObject[] _downRooms_U;
    [SerializeField] public GameObject[] _leftRooms_R;
    [SerializeField] public GameObject[] _rightRooms_L;

    [SerializeField] public GameObject[] _endRooms;

    [SerializeField] public GameObject[] _startRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;
}
