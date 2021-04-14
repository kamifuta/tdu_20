using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    public GameObject haveFossilPanel;
    public GameObject haveTrapPanel;
    public GameObject haveGoodsPanel;

    private PlayerAction playerAction;

    // Start is called before the first frame update
    void Start()
    {
        haveFossilPanel.SetActive(false);
        haveTrapPanel.SetActive(false);
        haveGoodsPanel.SetActive(false);
        playerAction = FindObjectOfType<PlayerAction>();
        this.gameObject.SetActive(false);
    }

    public void OnHavFossilButton()
    {
        this.gameObject.SetActive(false);
        haveFossilPanel.SetActive(true);
        playerAction.CanOpenMenu = false;
    }

    public void OnHaveTrapButton()
    {
        this.gameObject.SetActive(false);
        haveTrapPanel.SetActive(true);
        playerAction.CanOpenMenu = false;
    }

    public void OnHaveGoodsButton()
    {
        this.gameObject.SetActive(false);
        haveGoodsPanel.SetActive(true);
        playerAction.CanOpenMenu = false;
    }

    public void OnMenuCloseButton()
    {
        this.gameObject.SetActive(false);
        playerAction.CanOpenMenu = true;
        playerAction.IsAction = false;
    }

    public void OnHaveFossilCloseButton()
    {
        haveFossilPanel.SetActive(false);
        this.gameObject.SetActive(true);
        playerAction.CanOpenMenu = true;
    }

    public void OnHaveTrapCloseButton()
    {
        haveTrapPanel.SetActive(false);
        this.gameObject.SetActive(true);
        playerAction.CanOpenMenu = true;
    }

    public void OnHaveGoodsCloseButton()
    {
        haveGoodsPanel.SetActive(false);
        this.gameObject.SetActive(true);
        playerAction.CanOpenMenu = true;
    }
}
