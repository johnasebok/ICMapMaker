using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class makeUniverse : MonoBehaviour
{
    public TextAsset universeTextAsset;
    public GameObject planet;
    
    // Start is called before the first frame update
    void Start()
    {
        makeFromEmpty();

        
    }

    void makeFromJson()
    {
        Star_System d = JsonUtility.FromJson<Star_System>(universeTextAsset.text);

        for (int i = 0; i < d.starSystem.Length; i++)
        {
            StarSystem s = d.starSystem[i];
            Vector3 worldPosition = new Vector3(int.Parse(s.x), int.Parse(s.y));
            Instantiate(planet, worldPosition, new Quaternion()).name = s.x + ":" + s.y;
        }
    }

    void makeFromEmpty()
    {
        this.transform.GetComponent<GUIManager>().makeMap(100);
    }


}
