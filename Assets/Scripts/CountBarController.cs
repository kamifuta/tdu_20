using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBarController : MonoBehaviour
{
    Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GameObject.Find("CountBar").GetComponent<Slider>();
    }


    float _hp = 0;
    void Update()
    {
        // HP上昇
        _hp += 0.01f;
        if (_hp > 1)
        {
            // 最大を超えたら0に戻す
            _hp = 0;
        }

        // HPゲージに値を設定
        _slider.value = _hp;
    }

    // Update is called once per frame

}
