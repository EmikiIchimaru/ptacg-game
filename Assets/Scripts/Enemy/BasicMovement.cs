using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
 // Public variable to control movement speed
    public Buff currentBuff { get; set; }
    [SerializeField] private bool isHostile = true;
    [SerializeField] private bool shouldChasePlayer = false;
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float reflectVariance = 0.2f;
    [SerializeField] private float speedVariance = 0.1f;
    [SerializeField] private Transform rotatePart;

    private Rigidbody2D rb;
    private DetectPlayer detectPlayer;
    // Private variable to store the movement direction
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (isHostile)
        {
            detectPlayer = GetComponent<DetectPlayer>();
        }

        if (!shouldChasePlayer)
        {
            float moveX = Random.Range(-1f, 1f);
            float moveY = Random.Range(-1f, 1f);

            // Create a Vector3 for movement direction
            moveDirection = new Vector2(moveX, moveY);

            // Normalize the direction vector to ensure consistent movement speed
            if (moveDirection.magnitude > 1)
            {
                moveDirection.Normalize();
            }
        }      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveUnit();
        if (rotatePart != null) { MakeUnitFacePlayer(); }
    }

    private void MoveUnit()
    {
        //if (positionIndexX != LevelManager.Instance.currentX || positionIndexY != LevelManager.Instance.currentY) { return; }
        // Move the sprite by translating its position
        if (shouldChasePlayer) 
        { 
            if (detectPlayer.isPlayerInRange)
            {
                moveDirection = GameManager.Instance.playerCharacter.transform.position - transform.position;

                // Normalize the direction vector to ensure consistent movement speed
                if (moveDirection.magnitude > 1)
                {
                    moveDirection.Normalize();
                }
            }
        }
        float buffMultiplier = 1f;
        if (currentBuff != null)
        {
            buffMultiplier = currentBuff.speedMultiplier;
        }
        rb.AddForce(100f * moveDirection * moveSpeed * buffMultiplier * Time.fixedDeltaTime);
        //transform.Translate(moveDirection * moveSpeed * buffMultiplier * Time.deltaTime);
    }

    private void MakeUnitFacePlayer()
    {
        if (isHostile) 
        {
            Vector3 direction = GameManager.Instance.playerCharacter.transform.position - rotatePart.position;
            // Calculate the angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            rotatePart.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90f));
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Invisible") )
        {
            // Get the contact point and normal
            ContactPoint2D contact = collision.contacts[0];
            Vector2 normal = contact.normal;

            // Reflect the direction based on the collision normal
            moveDirection = Vector2.Reflect(moveDirection, normal);

            //Add randomness
            moveDirection += new Vector2(Random.Range(-reflectVariance, reflectVariance), Random.Range(-reflectVariance, reflectVariance));
            moveSpeed *= Random.Range(1-speedVariance, 1+speedVariance);
            rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Impulse);
        }
        
    }
}
