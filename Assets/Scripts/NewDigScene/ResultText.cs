using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public GameObject _ResultText;
    public Text _resulttext;
    GameObject DigSceneDirector;
    NewDigSceneDirector DigDirectorScript;
    string ObtainFossilName;
    // Start is called before the first frame update
    void Start()
    {
        DigSceneDirector = GameObject.Find("NewDigSceneDirector");
        DigDirectorScript = DigSceneDirector.GetComponent<NewDigSceneDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int i = 0;
            ObtainFossilName = DigDirectorScript.FossilDic[i].Fname;
            _resulttext = _ResultText.GetComponent<Text>();
            _resulttext.text = ObtainFossilName + "　を手に入れたよ！";
        }
    }
}
