using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public GameObject _ResultText;
    string [] _resulttext;
    GameObject DigSceneDirector;
    NewDigSceneDirector DigDirectorScript;
    string ObtainFossilName;
    Text ggg;
    int j;
    // Start is called before the first frame update
    void Awake()
    {
        ggg = _ResultText.GetComponent<Text>();
        DigSceneDirector = GameObject.Find("NewDigSceneDirector");
        DigDirectorScript = DigSceneDirector.GetComponent<NewDigSceneDirector>();
    }
    private void OnEnable()
    {
        j = DigDirectorScript.ExcavationCompletedhs.Count;
        _resulttext = new string[j];
        int aaa = 0;
        foreach (int i in DigDirectorScript.ExcavationCompletedhs)
        {

            ObtainFossilName =new FossilInfo().FossilInfoDic[i].itemName;
            _resulttext[aaa] = ObtainFossilName;
            aaa++;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            //DigDirectorScript.MemorizeKey[];
            for(int k = 0; k < j; k++)
            {
                ggg.text += _resulttext[k];
                ggg.text += " ";
            }
            ggg.text += "　を手に入れたよ！";
        }
    }
}
