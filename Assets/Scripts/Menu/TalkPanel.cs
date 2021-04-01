using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkPanel : MonoBehaviour
{
    public GameObject talkPanel;
    public GameObject exchangePointPanel;

    private PlayerAction playerAction;

    private void Awake()
    {
        playerAction = FindObjectOfType<PlayerAction>();
    }

    private void Start()
    {
        exchangePointPanel.SetActive(false);
        talkPanel.SetActive(false);
    }

    public void CheckHavePoint()
    {

    }

    public void ExchacgePoint()
    {
        talkPanel.SetActive(false);
        exchangePointPanel.SetActive(true);
    }

    public void ExchacgeItem()
    {

    }

    public void ExchangeTrap()
    {

    }

    public void StopTalk()
    {
        talkPanel.SetActive(false);
        playerAction.IsAction = false;
    }

    public void CloseExchangePoint()
    {
        talkPanel.SetActive(true);
        exchangePointPanel.SetActive(false);
    }
}
