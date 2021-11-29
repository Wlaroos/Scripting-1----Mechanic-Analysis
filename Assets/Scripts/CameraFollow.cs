using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _ref;

    void Update()
    {
        transform.position = new Vector3(((_ref.position.x / 416) * 10), ((_ref.position.y / 208) * 10 ), -1);
    }
}
