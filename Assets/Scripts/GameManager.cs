﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MenuePanel;

    // Start is called before the first frame update
    void Start()
    {
        MenuePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MenuePanel.SetActive(true);
        }
    }
}
