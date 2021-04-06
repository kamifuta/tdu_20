using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NodeController_point : MonoBehaviour
{
    public GameObject countUpButtonObj;
    public GameObject countDownButtonObj;
    public Button countUpButton;
    public Button countDownButton;
    public Text itemCountText;
    private Having having;
    private FossilInfo.FossilSize sizeKey;
    private ItemInfo.pointType colorKey;
    private KeyValuePair<int,FossilInfo.Fossil> pair;

    private int itemCount=0;

    // Start is called before the first frame update
    void Start()
    {
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();

        countDownButton.interactable = false;

        string itemName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        pair = new FossilInfo().FossilInfoDic.FirstOrDefault(c => c.Value.itemName == itemName);
        sizeKey= (FossilInfo.FossilSize)Enum.ToObject(typeof(FossilInfo.FossilSize), pair.Key % 3);
        colorKey = (ItemInfo.pointType)Enum.ToObject(typeof(ItemInfo.pointType), pair.Key/3);

        itemCountText.text = "x" + 0;
    }

    public void OnClickCountUpButton()
    {
        itemCount++;
        having.GetPoint(having.HaveFossil[pair.Key].fossilColor, having.HaveFossil[pair.Key].point);
        itemCountText.text = "x" + itemCount;
        if(itemCount== having.HaveFossil[pair.Key].itemCount)
        {
            countUpButton.interactable = false;
        }
        if (countDownButton.interactable == false)
        {
            countDownButton.interactable = true;
        }
    }

    public void OnClickCountDownButton()
    {
        itemCount--;
        having.LosePoint(having.HaveFossil[pair.Key].fossilColor, having.HaveItem[pair.Key].point);
        itemCountText.text = "x" + itemCount;
        if (itemCount == 0)
        {
            countDownButton.interactable = false;
        }
        if (countUpButton.interactable == false)
        {
            countUpButton.interactable = true;
        }
    }

    public void Exchange()
    {
        for(int i = 0; i < itemCount; i++)
        {
            having.ThrowFossil(sizeKey,colorKey);
        }
        itemCount = 0;
        itemCountText.text = "x" + 0;
    }
}
