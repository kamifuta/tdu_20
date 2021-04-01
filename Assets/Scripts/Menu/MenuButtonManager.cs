using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    public GameObject havingPanel;
    public GameObject havingTrapPanel;
    public bool canOpenMenu = true;

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
        canOpenMenu = false;
    }

    public void OnHavingTrapButton()
    {
        this.gameObject.SetActive(false);
        havingTrapPanel.SetActive(true);
        canOpenMenu = false;
    }

    public void OnMenuCloseButton()
    {
        this.gameObject.SetActive(false);
        canOpenMenu = true;
    }

    public void OnItemCloseButton()
    {
        havingPanel.SetActive(false);
        this.gameObject.SetActive(true);
        canOpenMenu = true;
    }

    public void OnTrapCloseButton()
    {
        havingTrapPanel.SetActive(false);
        this.gameObject.SetActive(true);
        canOpenMenu = true;
    }
}
