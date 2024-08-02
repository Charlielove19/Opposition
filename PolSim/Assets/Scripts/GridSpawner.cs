using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this script generates the hex grid based off of a height and width of said grid. Due to the nature of hexagons, x position offsets are required for odd values of y
    /// </summary>
    [SerializeField] private GameObject[] hexPrefabs;
    [SerializeField] private GameObject hexParent;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject uIManager;
    public int width;
    public int height;
    [SerializeField] private float hexRadLong = 1;
    private float hexRadShort;
    private float hexGapWide;
    private float hexGapHeight;
    private float oddOffset;
    private Vector3 pos;
    // C.Love - this is called at the start of the game and instantiates each hex object at the correct 3D space co-ordinates and gives each hex an XY co-ordinate value also
    void Start()
    {
        uIManager = GameObject.Find("UI Manager");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hexRadLong = hexPrefabs[0].transform.localScale.x/100;
        hexRadShort = (1.73205f * hexRadLong)/2f;
        hexGapWide = hexRadShort*2f;
        hexGapHeight = hexRadLong*(3f/2f);
        for (int i = 0; i < width; i++)
        {
            for (int p = 0; p < height; p++)
            {
                if (p%2 != 0)
                {
                    oddOffset = hexRadShort;
                }
                else
                {
                    oddOffset = 0;
                }
                pos = new Vector3(i*hexGapWide+oddOffset, 0f, p*hexGapHeight);
                int randHex = Random.Range(0,hexPrefabs.Length);
                var hexObject = Instantiate(hexPrefabs[randHex],pos, hexPrefabs[randHex].transform.rotation, hexParent.transform);
                hexObject.GetComponent<HexProperties>().xCoord = i;
                hexObject.GetComponent<HexProperties>().yCoord = p;
                hexObject.name = "X:" + i +" Y:" + p;
                uIManager.GetComponent<UI_Controller>().SetCurrentHex(hexObject);
                gameManager.hexList.Add(hexObject);
            }
        }
    }
}
