using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MatchingUIController : MonoBehaviour
{
    [SerializeField] Text nicknameText=null;
    [SerializeField] GameObject titleIMage=null;
    [SerializeField] GameObject networkManagerObj=null;
    [System.NonSerialized] public string nickname;
    /*private void Awake()
    {
        photonview = GetComponent<PhotonView>();
    }*/

    public void NameInput(string name)
    {
        nickname = name;
        nicknameText.text = name;
        titleIMage.SetActive(false);
        networkManagerObj.SetActive(true);
    }
   
    public void GameStartButton()
    {
        SceneManager.LoadScene("RoomScene");
    }

    /*[PunRPC]
    public void SceneChange()
    {
        
    }*/

}
