using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public Slider numOfSystemsSlider;
    public TextMeshProUGUI numOfSystemsText;
    public TextMeshProUGUI coordText;
    public GameObject boardMap;
    public GameObject boardSquare;
    public int maxsize;
    public GameObject[,] boardSquareArray;
    public int currentMapSize;

    public void updateNumOfSystemsText()
    {
        numOfSystemsText.text = numOfSystemsSlider.value.ToString();
        updateMapSize(int.Parse(numOfSystemsSlider.value.ToString()));
    }

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
}
