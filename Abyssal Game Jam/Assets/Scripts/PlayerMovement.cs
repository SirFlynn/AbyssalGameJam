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
    [System.NonSerialized] public bool isPossessing = false;

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

        if (Input.GetKey(KeyCode.Space) && possessedObject != null && isPossessing == false)
        {
            spriteRenderer.enabled = false;
            gameObject.transform.position = possessedObject.transform.position;
            isPossessing = true;
        }
        else if (Input.GetKey(KeyCode.Space) && isPossessing == true)
        {
            spriteRenderer.enabled = true;
            isPossessing = false;
        }

        if (isPossessing)
        {
            possessedObject.transform.position = gameObject.transform.position;
            gameObject.transform.position = possessedObject.transform.position;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Possessable")
        {
            possessedObject = collision.gameObject;
        }
        if (collision.gameObject.tag == "Moveable")
        {
            possessedObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        possessedObject = null;
    }
}
