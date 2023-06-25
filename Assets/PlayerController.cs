using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D body;
    Animator animator;
    public float MAX_SPEED = 8f;
    public int pickups = 0;

    Vector2 inputs;
    Vector2 velocity;
    Vector2 direction;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        velocity = Vector2.ClampMagnitude((inputs * MAX_SPEED), MAX_SPEED);

        // Animation
        if (inputs.magnitude > 0) direction = inputs.normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", velocity.magnitude);

        if (direction.x < 0) GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;
        }

    private void FixedUpdate()
    {
        body.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gObject = collision.gameObject;
        if (gObject.tag == "Pickup")
        {
            Destroy(gObject);
            pickups++;
            if (pickups == 5) quit();
        }
    }

    private void quit()
    {
        if (UnityEditor.EditorApplication.isPlaying) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }

}
