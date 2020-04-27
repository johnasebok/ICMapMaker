using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class moveManager : MonoBehaviour
{
    public float speed=10f;
    public Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        Vector3 endPosition = Vector3.zero;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            endPosition.x -= speed;
           
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            endPosition.x += speed;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            endPosition.y += speed;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            endPosition.y -= speed;
        }

        cam.transform.position = Vector3.Lerp(cam.transform.position, endPosition+ cam.transform.position, step);
    }
}
