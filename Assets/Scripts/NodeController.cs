using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    private GameObject itemPanel;
    public GameObject throwButtonObj;
    private Having having;
    //private string itemName;
    //public Button throwButton;
    // Start is called before the first frame update
    void Start()
    {
        itemPanel = GameObject.Find("HaveItemPanel");
        having = GameObject.FindGameObjectWithTag("Player").GetComponent<Having>();
        throwButtonObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickItemNode()
    {
        throwButtonObj.SetActive(true);
    }

    public void OnClickThrowButton()
    {
        string itemName = this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        throwButtonObj.SetActive(false);
        having.ThrowItem(itemName);
        itemPanel.SetActive(false);
    }
}
