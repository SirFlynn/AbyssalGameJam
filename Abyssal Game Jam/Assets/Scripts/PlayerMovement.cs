using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    PlayerData playerData;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    SpriteRenderer spriteRenderer;
    Vector2 movement;

    [System.NonSerialized] public GameObject possessedObject;
    [System.NonSerialized] public BoxCollider2D possessedCollider;
    [System.NonSerialized] public bool isPossessing = false;
    [System.NonSerialized] public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerData = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPossessing || (possessedObject != null && possessedObject.tag == "Moveable"))
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (isPossessing && possessedCollider.IsTouchingLayers())
        {
            rb.velocity = -movement;
        }

        if (isPossessing)
        {
            possessedObject.transform.position = gameObject.transform.position;
            gameObject.transform.position = possessedObject.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Space) && possessedObject != null && !isPossessing)
        {
            AudioManager.Instance.PlayPossessing();
            spriteRenderer.enabled = false;
            gameObject.transform.position = possessedObject.transform.position;
            isPossessing = true;
            rb.velocity = Vector2.zero;
            animator = possessedObject.GetComponent<Animator>();

        }
        else if (Input.GetKeyDown(KeyCode.Space) && isPossessing == true)
        {
            spriteRenderer.enabled = true;
            isPossessing = false;
            animator = null;
        }

        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Possessable" || collision.gameObject.tag == "Moveable") && !isPossessing)
        {
            possessedObject = collision.gameObject;
            possessedCollider = possessedObject.GetComponent<BoxCollider2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isPossessing)
        {
            possessedObject = null;
            possessedCollider = null;
        }
        
    }
}
