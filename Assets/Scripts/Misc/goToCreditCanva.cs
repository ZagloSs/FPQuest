using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class goToCreditCanva : MonoBehaviour
{
    [SerializeField] private Image fade;
    void Start()
    {
        fade.color = new Color(0, 0, 0, 0);
        StartCoroutine(goToCreditScene());
    }

    // Update is called once per frame
    void Update()
    {
      fade.color = new Color(0, 0, 0, (fade.color.a) + ((100 * Time.deltaTime) / 255));
    }

    public IEnumerator goToCreditScene()
    {
        yield return new WaitForSeconds(3f);
        AudioManager.instance.playCredits();
        SceneManager.LoadScene("credits");
    }
}
