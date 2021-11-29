using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _ref;
    [SerializeField] Transform _quadRef;
    bool toggle;

    void Update()
    {
        transform.position = new Vector3(((_ref.position.x / 416) * 10), ((_ref.position.y / 208) * 10 ), -1);

        if (toggle == true)
        {
            _quadRef.localScale = new Vector3(132f, 120f, 1f);
            _quadRef.localPosition = new Vector3(130, 60, -1);
            //GetComponent<Camera>().backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.15f);
            _quadRef.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1f, 1f, 1f, 0.5f));
        }
        else if (toggle == false)
        {
            _quadRef.localScale = new Vector3(66f, 60f, 1f);
            _quadRef.localPosition = new Vector3(150, 75, -1);
            //GetComponent<Camera>().backgroundColor = new Color(0.1f, 0.1f, 0.1f, 1f);
            _quadRef.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            toggle = !toggle;
        }
    }
}
