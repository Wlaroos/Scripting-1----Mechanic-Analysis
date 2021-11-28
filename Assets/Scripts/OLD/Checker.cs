using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    private string _name;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _name = other.name;
    }

    public string FindOverlapping()
    {
        return (_name);
    }
}
