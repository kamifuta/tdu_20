using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigSceneDirector : MonoBehaviour
{
    public GameObject TileImage;
    public GameObject canvas;
    GameObject aaa;
    private int[,] Board = new int[13, 10];
    private const int EMPTY = 0;
    

    // Start is called before the first frame update
    void Start()
    {

        //GameObject TileImage = new GameObject("TileImage");
        //TileImage.transform.parent = GameObject.Find("Canvas").transform;
        //TileImage.AddComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        //TileImage.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        //Tile.transform.SetParent(this.transform);

        for (int i=0; i < 13; i ++) 
        {
            for (int j=0;j < 10;j++)
            {
                Board[i, j] = EMPTY;
                aaa = Instantiate(TileImage, new Vector3(i, j, 0),Quaternion.identity);
                //aaa.transform.parent = GameObject.Find("Canvas").transform;
                aaa.transform.SetParent(canvas.transform);
                //aaa.transform.localPosition = new Vector3(50*i, 50*j, 0);
                aaa.GetComponent<RectTransform>().anchoredPosition = new Vector3(40*i+40,40*j+20,0);
                aaa.transform.localScale = new Vector3(1, 1, 1);
                //Debug.Log(Tile.transform.position.x);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
