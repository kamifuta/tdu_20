using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkPanel : MonoBehaviour
{
    public GameObject exchangePointPanel;
    public GameObject exchangeTrapPanel;
    public GameObject exchangeGoodsPanel;

    private PlayerAction playerAction;

    private void Awake()
    {
        playerAction = FindObjectOfType<PlayerAction>();
    }

    private void Start()
    {
        Debug.Log("aaa");
        exchangePointPanel.SetActive(false);
        exchangeTrapPanel.SetActive(false);
        exchangeGoodsPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void OnExchangePointButtont()
    {
        this.gameObject.SetActive(false);
        exchangePointPanel.SetActive(true);
    }

    public void OnExchangeGoodsButton()
    {
        this.gameObject.SetActive(false);
        exchangeGoodsPanel.SetActive(true);
    }

    public void OnExchangeTrapButton()
    {
        this.gameObject.SetActive(false);
        exchangeTrapPanel.SetActive(true);
    }

    public void StopTalk()
    {
        this.gameObject.SetActive(false);
        playerAction.IsAction = false;
        playerAction.CanOpenMenu = true;
    }

    public void OnExchangePointCloseButtont()
    {
        this.gameObject.SetActive(true);
        exchangePointPanel.SetActive(false);
    }

    public void OnExchangeGoodsCloseButton()
    {
        this.gameObject.SetActive(true);
        exchangeGoodsPanel.SetActive(false);
    }

    public void OnExchangeTrapCloseButton()
    {
        this.gameObject.SetActive(true);
        exchangeTrapPanel.SetActive(false);
    }
}
