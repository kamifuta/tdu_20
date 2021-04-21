using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private Slider redSlider = null;
    [SerializeField] private Slider greenSlider = null;
    [SerializeField] private Slider blueSlider = null;

    [SerializeField] private GameObject hat = null;
    Material hatmat;

    Color hatcolor;

    void Awake()
    {
        hatmat = hat.GetComponent<Renderer>().material;
        Observable.CombineLatest(redSlider.OnValueChangedAsObservable(), greenSlider.OnValueChangedAsObservable(), blueSlider.OnValueChangedAsObservable())
            .Select(colorList => hatcolor =  new Color(colorList[0], colorList[1], colorList[2])).Subscribe(color => hatmat.color = color);
    }

    private void OnDestroy()
    {
        Destroy(hatmat);
    }
}
