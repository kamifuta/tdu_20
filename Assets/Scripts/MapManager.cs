using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject playerImage;

    private GameObject player;
    private const float zoomValue = 210f / 24f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetPlayerImage()
    {
        playerImage.transform.localPosition = new Vector3(player.transform.position.x * zoomValue, player.transform.position.z * zoomValue, 0);
    }
}
