using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour
{
    public Sprite spU, spD, spL, spR,
            spUD, spLR, spUL, spUR, spDL, spDR,
            spUDL, spULR, spUDR, spDLR, spUDLR;

    public bool up, down, left, right;

    public int type;

    public Vector2 loc;

    public Color normalColor, startColor, bossColor, lootColor, shopColor, secretColor, genericEnd;

    Color mainColor;

    SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        mainColor = normalColor;
        transform.GetChild(0).gameObject.SetActive(false);
        PickSprite();
        PickColor();
    }

    void PickSprite()
    {
        if(up)
        {
            if(down)
            {
                if(left)
                {
                    if(right)
                    {
                        rend.sprite = spUDLR;
                    }
                    else
                    {
                        rend.sprite = spUDL;
                    }
                }
                else if (right)
                {
                    rend.sprite = spUDR;
                }
                else
                {
                    rend.sprite = spUD;
                }
            }
            else
            {
                if (left)
                {
                    if(right)
                    {
                        rend.sprite = spULR;
                    }
                    else
                    {
                        rend.sprite = spUL;
                    }
                }
                else if (right)
                {
                    rend.sprite = spUR;
                }
                else
                {
                    rend.sprite = spU;
                }
            }
            return;
        }

        if(down)
        {
            if(left)
            {
                if(right)
                {
                    rend.sprite = spDLR;
                }
                else
                {
                    rend.sprite = spDL;
                }
            }
            else if(right)
            {
                rend.sprite = spDR;
            }
            else
            {
                rend.sprite = spD;
            }
            return;
        }

        if (left)
        {
            if(right)
            {
                rend.sprite = spLR;
            }
            else
            {
                rend.sprite = spL;
            }
        }
        else
        {
            rend.sprite = spR;
        }
    }

    public void PickColor()
    {
        if (type == 0)
        {
            mainColor = normalColor;
        }
        else if (type == 1)
        {
            mainColor = startColor;
        }
        else if (type == 2)
        {
            mainColor = lootColor;
        }
        else if (type == 3)
        {
            mainColor = shopColor;
        }
        else if (type == 4)
        {
            mainColor = bossColor;
        }
        else if (type == 5)
        {
            mainColor = secretColor;
        }
        else if (type == 99)
        {
            mainColor = genericEnd;
        }
        rend.color = mainColor;
    }
}
