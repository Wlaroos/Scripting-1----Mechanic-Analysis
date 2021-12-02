using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFog : MonoBehaviour
{
    MapSpriteSelector _mpsRef;
    SpriteRenderer _spRef;

    Color initColor;

    bool isFull = false;

    //Initialization for references and Hide method
    private void Start()
    {
        _mpsRef = GetComponent<MapSpriteSelector>();
        _spRef = GetComponent<SpriteRenderer>();
        DelayHelper.DelayAction(this, DelayedHide, .01f);
    }

    //Tests if the player icon is touching or near a room
    private void OnTriggerStay(Collider other)
    {
        //Trigger on top of player icon
        if (other.gameObject.name == "Main")
        {
            Full();
            isFull = true;
        }
        //Only run once (boolean) -- Trigger around the player icon
        else if (isFull == false && other.gameObject.name == "Collider")
        {
            Black();
        }
    }

    //Map icon is transparent if player has not visited
    void DelayedHide()
    {
        initColor = _spRef.color;
        _spRef.color = new Color(initColor.r, initColor.g, initColor.b, 0);
    }

    //Map icon is blacked out if player is near but has not visited
    void Black()
    {
        if (_mpsRef.type != 5)
        {
            _spRef.color = new Color(0, 0, 0, 1);
        }
    }

    //Map icon is full color once player has visited the room
    void Full()
    {
        _spRef.color = new Color(initColor.r, initColor.g, initColor.b, 1);

        if (_mpsRef.type == 5)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
