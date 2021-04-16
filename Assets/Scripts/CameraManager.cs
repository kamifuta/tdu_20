using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject digSceneCamera;
    public GameManager gameManager;

    private void Update()
    {
        if (gameManager.isDigScene == true)
        {
            playerCamera.SetActive(false);
            digSceneCamera.SetActive(true);
        }
        else
        {
            playerCamera.SetActive(true);
            digSceneCamera.SetActive(false);
        }
    }
}
