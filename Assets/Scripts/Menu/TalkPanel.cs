using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkPanel : MonoBehaviour
{
    public GameObject talkPanel;
    public GameObject exchangePanel;

    private void Start()
    {
        talkPanel.SetActive(false);
        exchangePanel.SetActive(false);
    }

    public void CheckHavePoint()
    {

    }

    public void ExchacgePoint()
    {
        talkPanel.SetActive(false);
        exchangePanel.SetActive(true);
    }

    public void ExchacgeItem()
    {

    }

    public void ExchangeTrap()
    {

    }

    public void StopTalk()
    {

    }
}
