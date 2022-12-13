using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce;
    float score;

    [SerializeField]
    bool isGrounded = false;
    bool isAlive = true;

    Rigidbody2D RB;

    public Text scoreText;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {
                RB.AddForce(Vector2.up * jumpForce);
                isGrounded = false;
            }
        }

        if(isAlive)
        {
            score += Time.deltaTime * 4;
            scoreText.text = "Score" + score.ToString("F");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("spike"))
        {
            isAlive = false;
            Time.timeScale = 0;
        }
    }
}
