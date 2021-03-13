using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewDigSceneDirector : MonoBehaviour
{
    public GameObject[] BoardSprite = new GameObject[3];
    private int[,] Board = new int[13, 10];
    private GameObject[,] BoardImage = new GameObject[13, 10];
    private int Strength = 0;
    private Vector3 mousePos;
    private Vector3 objPos;
    private int objPosX;
    private int objPosY;
    public Button HammerButton;
    public Button PickelButton;
    //public bool HammerCheck = true;
    //public bool PickelCheck = true;

    private void IntializeArray()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                
                Strength = Random.Range(0, 3);
                Board[i, j] = Strength;
                BoardImage[i,j] = Instantiate(BoardSprite[Strength], new Vector3(i, j, 0), Quaternion.identity);//マスの生成

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IntializeArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SearchMousePosition();

            if (objPos.y > -0.5 && objPos.y < 9.5 && objPos.x > -0.5 && objPos.x < 12.5)
            {
                

                objPosX = (int)Mathf.Round(objPos.x);
                objPosY = (int)Mathf.Round(objPos.y);

                if (HammerButton.interactable == false)
                {
                    HammerMode();
                }
                else if (PickelButton.interactable == false)
                {
                    PickelMode();                    
                }

            }
        }
    }
    private void SearchMousePosition()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10.0f;                             //役割何？
        objPos = Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.Log(objPos);
    }
    public void HammerORPickelButton()
    {
        if (HammerButton.interactable == false)
        {
            HammerButton.interactable = true;
            PickelButton.interactable = false;
        }
        else if (PickelButton.interactable == false)
        {
            
            PickelButton.interactable = true;
            HammerButton.interactable = false;
        }
    }
    
    public void HammerMode()
    {
        for(int i = -1; i <= 1; i++)
        {
            if (objPosX + i == -1 || objPosX + i == 13)
            {
                continue;
            }
            for(int j=-1;j<= 1; j++)
            {
                if(objPosY + j == -1 || objPosY + j == 10)
                {
                    continue;
                }
                Destroy(BoardImage[objPosX + i, objPosY + j]);
                Board[objPosX + i, objPosY + j] -= 2;
                if (Board[objPosX+i, objPosY+j] > -1)
                {
                    BoardImage[objPosX + i, objPosY +j] = Instantiate(BoardSprite[Board[objPosX + i, objPosY + j]], new Vector3(objPosX + i, objPosY + j, 0), Quaternion.identity);
                }
            }
        }
    }
    public void PickelMode()
    {
        for (int j = -1; j <= 1; j++)
        {
            if (objPosY + j == -1 || objPosY + j == 10)
            {
                continue;
            }
            Destroy(BoardImage[objPosX, objPosY + j]);
            Board[objPosX, objPosY + j]--;
            if (Board[objPosX , objPosY + j] > -1)
            {
                BoardImage[objPosX, objPosY + j] = Instantiate(BoardSprite[Board[objPosX, objPosY + j]], new Vector3(objPosX, objPosY + j, 0), Quaternion.identity);
            }
        }
        for (int j = -1; j <= 1; j++)
        {
            if (objPosX + j == -1 || objPosX + j == 13)
            {
                continue;
            }
            Destroy(BoardImage[objPosX + j, objPosY]);
            Board[objPosX + j, objPosY]--;
            if (Board[objPosX + j, objPosY ] > -1)
            {
                BoardImage[objPosX + j, objPosY] = Instantiate(BoardSprite[Board[objPosX + j, objPosY ]], new Vector3(objPosX + j, objPosY, 0), Quaternion.identity);
            }
        }
    }
}
