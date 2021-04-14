using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Vector3> trapPos = new List<Vector3>();
    public GameObject textPanel;
    public Text checkText;

    private bool YES;
    private bool NO;

    private void Start()
    {
        textPanel.SetActive(false);
    }

    public async UniTask<bool> SetTextPanelAsync(CancellationToken token = default, string text = default)
    {
        checkText.text = text;
        textPanel.SetActive(true);

        var result=await UniTask.WhenAny(
            UniTask.WaitUntil(() => YES, cancellationToken: token),
            UniTask.WaitUntil(() => NO, cancellationToken: token)
            );

        YES = false;
        NO = false;
        textPanel.SetActive(false);

        if (result == 0) return true;

        return false;
    }

    public void OnYesButton()
    {
        YES = true;
    }

    public void OnNoButton()
    {
        NO = true;
    }

}
