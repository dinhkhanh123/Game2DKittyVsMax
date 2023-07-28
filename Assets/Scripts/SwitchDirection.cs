using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchDirection : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Max")
        {
            col.gameObject.GetComponent<Max>().SwitchDirection();
        }
    }
}
