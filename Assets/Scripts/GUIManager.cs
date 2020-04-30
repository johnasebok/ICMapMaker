using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public static GUIManager guiManager;
    public Slider numOfSystemsSlider;
    public TextMeshProUGUI numOfSystemsText;
    public TextMeshProUGUI coordText;
    public GameObject boardMap;
    public GameObject boardSquare;
    public int maxsize;
    public GameObject[,] boardSquareArray;
    public int currentMapSize;

    private void Start()
    {
        guiManager = this;
    }

    public void updateNumOfSystemsText()
    {
        numOfSystemsText.text = numOfSystemsSlider.value.ToString();
        updateMapSize(int.Parse(numOfSystemsSlider.value.ToString()));
    }


    //makes new map
    public void makeMap(int size)
    {
        maxsize = size;
        boardSquareArray = new GameObject[size,size];
       
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                GameObject square=Instantiate(boardSquare, boardMap.transform);
                square.transform.localPosition = new Vector3(x, y, 0);
                boardSquareArray[x, y] = square;
                square.name = x.ToString() + "," + y.ToString();
                square.transform.Find("Planet").gameObject.SetActive(false);
                if (x > numOfSystemsSlider.value-1 || y > numOfSystemsSlider.value-1)
                {
                    square.SetActive(false);
                }
            }
        }
        currentMapSize = int.Parse(numOfSystemsSlider.value.ToString());
    }

    public void updateMapSize(int newSize)
    {
  
        for (int x = 0; x < maxsize; x++)
        {
            for (int y = 0; y < maxsize; y++)
            {
                boardSquareArray[x, y].SetActive(x+1<=newSize&&y+1<=newSize);
            }
        }
        currentMapSize = newSize;
    }

    public void generateRandomGalaxy()
    {
        for (int x = 0; x < maxsize; x++)
        {
            for (int y = 0; y < maxsize; y++)
            {
     
                boardSquareArray[x, y].transform.Find("Planet").gameObject.SetActive(Random.Range(0, 100)<21);
            }
        }
    }

    public void resetGalaxy()
    {
        for (int x = 0; x < maxsize; x++)
        {
            for (int y = 0; y < maxsize; y++)
            {

                boardSquareArray[x, y].transform.Find("Planet").gameObject.SetActive(false);
            }
        }
    }

    public void makeMapFromImage(Texture2D imageImported)
    {
        //when the image isn't the current map size we need to alter which
        //pixel we choose by using the modifiers below
        int width = imageImported.width;
        int height = imageImported.height;
        float widthModifier = imageImported.width / currentMapSize;
        float heightModifier = imageImported.height / currentMapSize;

        Color32[] importedPixels = imageImported.GetPixels32();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                int pixelOn = (y * width) + x;
                Debug.Log(pixelOn);
                Color32 pixelColor=importedPixels[pixelOn];
                GameObject planetSystem = boardSquareArray[x, 99-y].transform.Find("Planet").gameObject;
                int systemType = getClosestColorMatch(pixelColor);
                if (systemType == 0)
                {
                    planetSystem.SetActive(false);
                }
                else
                {
                    planetSystem.SetActive(true);
                    Material m = planetSystem.GetComponent<Renderer>().material;
                    m.EnableKeyword("_EMISSION");
                    m.color = Image_Import.planetColorMap[systemType];
                    m.SetColor("_EmissionColor", Image_Import.planetColorMap[systemType]);


                }
                
            }
        }
    }

    public int getClosestColorMatch(Color32 pixelColor)
    {
        int closest = 0;
        float nearestDifference = 900f;
        for (int i = 0; i < Image_Import.planetColorMap.Length; i++)
        {
            float difference = Mathf.Abs(Image_Import.planetColorMap[i].b - pixelColor.b) +
                                Mathf.Abs(Image_Import.planetColorMap[i].r - pixelColor.r) +
                                Mathf.Abs(Image_Import.planetColorMap[i].g - pixelColor.g);
            if (difference < nearestDifference)
            {
                nearestDifference = difference;
                closest = i;
            }
        }
        //we agree the color isn't close enough to the defined colors so we set to empty
        if (nearestDifference > 100)
        {
            closest = 0;
        }
        return closest;

        
    }
}
