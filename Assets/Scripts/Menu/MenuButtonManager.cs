using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    public GameObject havingPanel;
    public GameObject havingTrapPanel;

    private PlayerAction playerAction;

    // Start is called before the first frame update
    void Start()
    {
        havingPanel.SetActive(false);
        havingTrapPanel.SetActive(false);
        playerAction = FindObjectOfType<PlayerAction>();
    }

    public void OnHavingButton()
    {
        this.gameObject.SetActive(false);
        havingPanel.SetActive(true);
        playerAction.CanOpenMenu = false;
    }

    public void OnHavingTrapButton()
    {
        this.gameObject.SetActive(false);
        havingTrapPanel.SetActive(true);
        playerAction.CanOpenMenu = false;
    }

    public void OnMenuCloseButton()
    {
        this.gameObject.SetActive(false);
        playerAction.CanOpenMenu = true;
        playerAction.IsAction = false;
    }

    public void OnItemCloseButton()
    {
        havingPanel.SetActive(false);
        this.gameObject.SetActive(true);
        playerAction.CanOpenMenu = true;
    }

    public void OnTrapCloseButton()
    {
        havingTrapPanel.SetActive(false);
        this.gameObject.SetActive(true);
        playerAction.CanOpenMenu = true;
    }
}
