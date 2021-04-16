using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrashTrap : Trap
{
    [SerializeField] GameObject trashBlind = null;
    private Vector3 playertf;
    private GameObject trashInstance;
    private float countup = 0.0f;

    protected override void OnAwake()
    {
        countup = 0.0f;
        playertf = gameObject.transform.position;
        playertf = new Vector3(playertf.x, playertf.y+1.0f, playertf.z);
        trashInstance=PhotonNetwork.Instantiate("trashObj", playertf, gameObject.transform.rotation);
        trashBlind.SetActive(true);
    }


    private void Update()
    {
        if (countup>10.0f)
        {
            PhotonNetwork.Destroy(trashInstance);//時間があったら「どっかに飛んでって落ちて消える」とかやってみたいなー
            trashBlind.SetActive(false);
            gameObject.SetActive(false);
            return;
        }
        countup += Time.deltaTime;
    }
}
