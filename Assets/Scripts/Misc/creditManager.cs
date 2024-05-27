using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class creditManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject credits;
    private bool isFaded = false;
    void Start()
    {
        StartCoroutine(Credits());
        
    }

    private void Update()
    {
        if (isFaded)
        {
            GameObject.Find("TXTFade").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, (GameObject.Find("TXTFade").GetComponent<TextMeshProUGUI>().color.a - (100f * Time.deltaTime) / 255));
            GameObject.Find("IMGFade").GetComponent<Image>().color = new Color(1, 1, 1, (GameObject.Find("IMGFade").GetComponent<Image>().color.a - (100f * Time.deltaTime) / 255));

        }
    }

    public IEnumerator Credits()
    {
        credits.transform.DOMoveY(21f, 15f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(18f);
        isFaded = true;
        yield return new WaitForSeconds(22f);
        GameManager.instance.backToMenu();
    }
     
    

}
