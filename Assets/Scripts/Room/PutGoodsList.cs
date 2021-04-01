using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutGoodsList : MonoBehaviour
{
    [SerializeField] GameObject goodsButtonPrefab=null;

    private GameObject goodsButton;
   public void CreateButton()
    {
        goodsButton= Instantiate(goodsButtonPrefab);
        //goodsButton.GetComponent<GoodsInfo>().変数=何か;

    }
}
