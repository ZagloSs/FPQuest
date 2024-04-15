using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    //Carlos Stuff
    [Header("Carlos Properties")]
    [SerializeField] private float Carlos_Health;
    [SerializeField] private float Carlos_Damage;
    [SerializeField] private float Carlos_Speed;
    [SerializeField] private float Carlos_AttSpeed;
    [SerializeField] private Sprite CarlosPortrait;
    [SerializeField] private AnimatorController CarlosAnimator;

    //Fer Stuff
    [Header("Fer Properties")]
    [SerializeField] private float Fer_Health;
    [SerializeField] private float Fer_Damage;
    [SerializeField] private float Fer_Speed;
    [SerializeField] private float Fer_AttSpeed;
    [SerializeField] private Sprite FerPortrait;
    [SerializeField] private AnimatorController FerAnimator;

    //Marcos Stuff
    [Header("Marcos Properties")]
    [SerializeField] private float Marcos_Health;
    [SerializeField] private float Marcos_Damage;
    [SerializeField] private float Marcos_Speed;
    [SerializeField] private float Marcos_AttSpeed;
    [SerializeField] private Sprite MarcosPortrait;
    [SerializeField] private AnimatorController MarcosAnimator;

    [Header("Portrait")]
    [SerializeField] private GameObject portrait;

    [Header("Properties")]
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private TextMeshProUGUI Damage;
    [SerializeField] private TextMeshProUGUI Speed;
    [SerializeField] private TextMeshProUGUI AttSpeed;


    private SpriteRenderer SRPortrait;
    private List<Dictionary<string, float>> characterSelection;
    private List<Sprite> portraits;
    private int characterSelected;
    private List<AnimatorController> animations;


    private Dictionary<string, float> CProperties;
    private Dictionary<string, float> FProperties;
    private Dictionary<string, float> MProperties;



    // Start is called before the first frame update
    void Start()
    {
        animations = new List<AnimatorController>() { CarlosAnimator, FerAnimator, MarcosAnimator};
        SRPortrait = portrait.GetComponent<SpriteRenderer>();
        CProperties = new Dictionary<string, float>()
        {
            { "Health", Carlos_Health },
            { "Damage", Carlos_Damage },
            { "Speed", Carlos_Speed },
            { "AttSpeed", Carlos_AttSpeed }
        };

        FProperties = new Dictionary<string, float>()
        {
            { "Health", Fer_Health },
            { "Damage", Fer_Damage },
            { "Speed", Fer_Speed },
            { "AttSpeed", Fer_AttSpeed }
        };

        MProperties = new Dictionary<string, float>()
        {
            { "Health", Marcos_Health },
            { "Damage", Marcos_Damage },
            { "Speed", Marcos_Speed },
            { "AttSpeed", Marcos_AttSpeed }
        };

        characterSelection = new List<Dictionary<string, float>>() { CProperties, FProperties, MProperties };
        portraits = new List<Sprite>() { CarlosPortrait, FerPortrait, MarcosPortrait };
        characterSelected = 0;
        SRPortrait.sprite = portraits[characterSelected];

    }

    // Update is called once per frame
    void Update()
    {
        changePropertiesTxt();

        if (Input.GetKeyDown(KeyCode.Space))
        {
           aceptarSeleccion();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            changeForwards();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            changeBackwards();
        }
    }

    public void changePropertiesTxt()
    {
            Health.text = Convert.ToString(characterSelection[characterSelected]["Health"]);
            Damage.text = Convert.ToString(characterSelection[characterSelected]["Damage"]);
            Speed.text = Convert.ToString(characterSelection[characterSelected]["Speed"]);
            AttSpeed.text = Convert.ToString(characterSelection[characterSelected]["AttSpeed"]);
    }

    public void aceptarSeleccion()
    {
        GameManager.instance.setProperties(characterSelection[characterSelected]);
        GameManager.instance.setAnimator(animations[characterSelected]);
        SceneManager.LoadScene("Escena_Pruebas");
    }

    public void changeBackwards()
    {
        if (characterSelected == 0)
        {
            characterSelected = 2;
        }
        else
        {
            characterSelected--;
        }


        SRPortrait.sprite = portraits[characterSelected];
        UnityEngine.Debug.Log(characterSelected);
    }

    public void changeForwards()
    {
        if (characterSelected == 2)
        {
            characterSelected = 0;
        }
        else
        {
            characterSelected++;
        }

        SRPortrait.sprite = portraits[characterSelected];
        UnityEngine.Debug.Log(characterSelected);
    }
}
