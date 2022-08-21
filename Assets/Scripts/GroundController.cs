using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    Player player;
    BoxCollider2D boxcollider;
    public bool debug = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boxcollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var dis = this.transform.position.y +0.38 - player.transform.GetChild(2).transform.position.y;
        if(debug == true)
        {

            Debug.Log(dis + this.name);
            
        }
        if (dis > 0)
        {
            boxcollider.enabled = false;
            //Debug.Log(dis + "Changed");

        }
        else if (dis < 0)
        { 
            boxcollider.enabled = true;
        }
    }
}
