using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    
    
    void Start()
    {
        cam = Camera.main;
        Debug.Log("dev");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(this.transform.position.x,this.transform.position.y,-10);
        cam.transform.position = pos;
    }
    
}
