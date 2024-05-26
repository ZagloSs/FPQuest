using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class ChangeScene : MonoBehaviour
{
    public void changeLvl1()
    {
        transform.DOMoveY(-0.5f, 1f)
       .SetEase(Ease.Linear).SetDelay(1f);
    }

    public void changeLvl2()
    {
        transform.DOMoveY(-3.5f, 1f)
       .SetEase(Ease.Linear).SetDelay(1f);
    }
}
