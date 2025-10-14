using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;
    private Animator animator;
    private Vector3 lastPosition;
    private Vector2 moveInput;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }
    
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.Translate(move * speed * Time.deltaTime, Space.World);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (move.x > 0.1f)
            spriteRenderer.flipX = false;
        else if (move.x < -0.1f)
            spriteRenderer.flipX = true;

        float velocity = (transform.position - lastPosition).magnitude / Time.deltaTime;
        animator.SetFloat("Speed", velocity);
        lastPosition = transform.position;
    }

}