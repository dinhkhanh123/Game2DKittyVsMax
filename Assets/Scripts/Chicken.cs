using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    private Kitty _kitty;
    public bool follow = false;
    void Start()
    {
      //  _kitty = player.GetComponent<Kitty>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!follow)
            return;


        if (!player.GetComponent<SpriteRenderer>().flipX)
        {
            this.gameObject.transform.position = Vector2.MoveTowards
                      (
                      new Vector3(transform.position.x, transform.position.y, transform.position.z),
                      new Vector3(player.transform.position.x - 1, player.transform.position.y, player.transform.position.z),
                      6f * Time.deltaTime
                      );
    }
        else
        {
            this.gameObject.transform.position = Vector2.MoveTowards
                       (
                       new Vector3(transform.position.x, transform.position.y, transform.position.z),
                       new Vector3(player.transform.position.x + 1, player.transform.position.y, player.transform.position.z),
                       10f * Time.deltaTime
                       );
        }
      

        
    }


     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            int children = player.transform.childCount;
            if (children == 2)
                return;


            follow = true;
            transform.SetParent(player.transform);
        }
        if (collision.tag == "Max")
        {
            follow = false;
            transform.parent = null;
        }
    }
}
