using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    public GameObject havingPanel;
    public GameObject havingTrapPanel;

    // Start is called before the first frame update
    void Start()
    {
        havingPanel.SetActive(false);
        havingTrapPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHavingButton()
    {
        this.gameObject.SetActive(false);
        havingPanel.SetActive(true);
    }

    public void OnHavingTrapButton()
    {
        this.gameObject.SetActive(false);
        havingTrapPanel.SetActive(true);
    }

    public void OnMenuCloseButton()
    {
        this.gameObject.SetActive(false);
    }

    public void OnItemCloseButton()
    {
        havingPanel.SetActive(false);
        this.gameObject.SetActive(true);
    }
}
