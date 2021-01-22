using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 4f;
    public float jumpHeight = 3f;
    public float gravity = -9.84f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float distanceToGround = 0.4f;

    public bool shield;
    public GameObject gameOverUI;

    Vector3 velocity;
    bool isGrounded;

    private void Awake()
    {
        characterController = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check Grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, distanceToGround, groundMask);

        // Reset vel when grounded
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Move character forward and horizontally
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move in relation to facing direction
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        // Jump Controls
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply Gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void TakeDamage()
    {
        if (shield)
            shield = false;
        else
            StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        gameOverUI.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(0);
    }
}
