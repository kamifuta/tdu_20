using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DigSceneDirector : MonoBehaviour
{
    public GameObject TileImage;
    public GameObject canvas;
    GameObject InstanceTileImage;
    private int[,] Board = new int[13, 10];
    private Image[,] BoardImage = new Image[13, 10];
    private const int ZERO = 0;
    private const int ONE = 1;
    private const int TWO = 2;
    private const int THREE = 3;
    public Sprite One;
    public Sprite Two;
    public Sprite Three;


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
                Board[i, j] = Random.Range(1,4);
                InstanceTileImage = Instantiate(TileImage, new Vector3(i, j, 0),Quaternion.identity);
                BoardImage[i, j] = InstanceTileImage.GetComponent<Image>();
                switch (Board[i, j])
                {
                    case 1:
                        BoardImage[i, j].sprite = One;
                        break;
                    case 2:
                        BoardImage[i, j].sprite = Two;
                        break;
                    case 3:
                        BoardImage[i, j].sprite = Three;
                        break;
                }
                //aaa.transform.parent = GameObject.Find("Canvas").transform;
                InstanceTileImage.transform.SetParent(canvas.transform);
                //aaa.transform.localPosition = new Vector3(50*i, 50*j, 0);
                InstanceTileImage.GetComponent<RectTransform>().anchoredPosition = new Vector3(40*i+40,40*j+20,0);
                InstanceTileImage.transform.localScale = new Vector3(1, 1, 1);
                //Debug.Log(Tile.transform.position.x);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit raycastHit;

            RaycastHit[] hit = Physics.RaycastAll(ray.origin, ray.direction);
            Debug.Log(hit.Length);
            /*
            if (hit != null)
            {
                Debug.Log("aaa");
            }*/
            /*
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
            {
                Debug.Log("aaa");
                Debug.Log(raycastHit.collider.gameObject.transform.position);
            }
            */
        }
        
    }
    //private void 
    //マウス座標の取得
    //取得した座標を中心にそれぞれのブロックの値をマイナスし、スプライトを変える
    //後ろにある物体（化石）の座標等の取得の仕方（０になったとき～）
    //スクリプト分けると思うけど、ハンマーとピッケルのボタン
    //壁のｈｐのゲージ
}
