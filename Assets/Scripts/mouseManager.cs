using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseManager : MonoBehaviour
{
    Camera mainCam;
    public int scrollSpeed = 10;
    public int maxZoom = 10;
    public int minZoom = 4000;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.collider.tag == "systemSquare")
            {
                GameObject hitSystem = hit.collider.gameObject;
                this.GetComponent<GUIManager>().coordText.text = hitSystem.name;

                if (Input.GetMouseButton(1))
                {
                    hitSystem.transform.GetChild(0).gameObject.gameObject.SetActive(false);

                }
                else if (Input.GetMouseButton(0))
                {
                    hitSystem.transform.GetChild(0).gameObject.gameObject.SetActive(true);
                }
            } 
        }
        if (Input.GetAxis("Mouse ScrollWheel")!=0f)
        {
            mainCam.orthographicSize = Mathf.Clamp(mainCam.orthographicSize +
                (Input.GetAxis("Mouse ScrollWheel")*10)* scrollSpeed,maxZoom, minZoom);
        }

    }
}
