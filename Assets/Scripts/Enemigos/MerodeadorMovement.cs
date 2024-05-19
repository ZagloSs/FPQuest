using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerodeadorMovement : MonoBehaviour
{

    [SerializeField] private float speed;

    private GameObject player; // Bucar al player
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        animator.SetBool("moverAbajo", true);
    }

    // Update is called once per frame
    void Update()
    {


        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
