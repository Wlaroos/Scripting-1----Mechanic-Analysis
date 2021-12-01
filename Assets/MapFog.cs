using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFog : MonoBehaviour
{
    [SerializeField] GameObject _ref;
    Color def;
    bool ful = false;

    private void Start()
    {
        _ref = GameObject.Find("Map Icon");
        DelayHelper.DelayAction(this, DelayedColor, .02f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Main")
        {
            Full();
            ful = true;
        }
        else if (ful == false)
        {
            Black();
        }
    }

    void DelayedColor()
    {
        def = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(def.r, def.g, def.b, 0);
    }

    void Black()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        if (GetComponent<MapSpriteSelector>().type == 5)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void Full()
    {
        GetComponent<SpriteRenderer>().color = new Color(def.r, def.g, def.b, 1);
        if (GetComponent<MapSpriteSelector>().type == 5)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
