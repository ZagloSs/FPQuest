using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadFIleBtn : MonoBehaviour
{
    private Button btnControls;
    private Image image;
    private void Start()
    {
        btnControls = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SaveLoadManager.Instance.SaveFileExists())
        {
            btnControls.enabled = false;
            image.color = new Color32(142, 142, 142, 255);
        }
        else
        {
            btnControls.enabled = true;
            image.color = Color.white;
        }
    }
}
