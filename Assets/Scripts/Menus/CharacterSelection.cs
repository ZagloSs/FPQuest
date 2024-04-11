using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    //Carlos Stuff
    [Header("Carlos Properties")]
    [SerializeField] private float Carlos_Health = 1f;
    [SerializeField] private float Carlos_Damage = 1f;
    [SerializeField] private float Carlos_Speed = 1f;
    [SerializeField] private float Carlos_AttSpeed = 1f;
    [SerializeField] private Sprite CarlosPortrait;

    //Fer Stuff
    [Header("Fer Properties")]
    [SerializeField] private float Fer_Health = 1f;
    [SerializeField] private float Fer_Damage = 1f;
    [SerializeField] private float Fer_Speed = 1f;
    [SerializeField] private float Fer_AttSpeed = 1f;
    [SerializeField] private Sprite FerPortrait;

    //Marcos Stuff
    [Header("Marcos Properties")]
    [SerializeField] private float Marcos_Health = 1f;
    [SerializeField] private float Marcos_Damage = 1f;
    [SerializeField] private float Marcos_Speed = 1f;
    [SerializeField] private float Marcos_AttSpeed = 1f;
    [SerializeField] private Sprite MarcosPortrait;

    [Header("Portrait")]
    [SerializeField] private GameObject portrait;


    private SpriteRenderer SRPortrait;
    private List<Dictionary<string, float>> characterSelection;
    private List<Sprite> portraits;
    private int characterSelected;



    // Start is called before the first frame update
    void Start()
    {
        
        SRPortrait = portrait.GetComponent<SpriteRenderer>();
        Dictionary<string, float> CProperties = new Dictionary<string, float>()
        {
            { "Health", Carlos_Health },
            { "Damage", Carlos_Damage },
            { "Speed", Carlos_Speed },
            { "AttSpeed", Carlos_AttSpeed }
        };

        Dictionary<string, float> FProperties = new Dictionary<string, float>()
        {
            { "Health", Fer_Health },
            { "Damage", Fer_Damage },
            { "Speed", Fer_Speed },
            { "AttSpeed", Fer_AttSpeed }
        };

        Dictionary<string, float> MProperties = new Dictionary<string, float>()
        {
            { "Health", Marcos_Health },
            { "Damage", Marcos_Damage },
            { "Speed", Marcos_Speed },
            { "AttSpeed", Marcos_AttSpeed }
        };

        characterSelection = new List<Dictionary<string, float>>(){ CProperties, FProperties,MProperties};
        portraits = new List<Sprite>() { CarlosPortrait, FerPortrait, MarcosPortrait };
        characterSelected = 0;
        SRPortrait.sprite = portraits[characterSelected];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if(characterSelected == 2)
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

        if (Input.GetKeyDown(KeyCode.A))
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
    }
}
