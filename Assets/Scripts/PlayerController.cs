using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Transform coinsParent;
    public Transform groundPoint;
    public LayerMask ground;
    public AudioSource audioSource;
    public AudioClip Jump;
    public AudioClip Coin;
    public AudioClip Win;
    public Animator worldAnimator;
    public int coins;

    public TextMesh score;

    private bool isGrounded;
    private float radius = 0.7f;
    private float force = 300;
    private Rigidbody2D rigidBody;
    private Animator anim;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, ground);
        
        if(isGrounded )
        {
            anim.SetTrigger("landed");
            if (Input.GetMouseButtonDown(0))
            {
                rigidBody.AddForce(Vector2.up * force);
                audioSource.clip = Jump;
                audioSource.Play();
            }
        }
        else
        {
            anim.SetTrigger("jump");
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Debug.Log(other.tag);
            coins++;
            UpdateScoreText();
            Destroy(other.gameObject);
            audioSource.clip = Coin;
            audioSource.Play();
        }
        else if (other.tag == "DeadZone")
        { 
            Application.LoadLevel("Level_01");
            Debug.Log(other.tag);
        }
        else if (other.tag == "Finish")
        {
            Debug.Log(other.tag);
            rigidBody.isKinematic = true;
            coins = coins + 10;
            UpdateScoreText();
            audioSource.clip = Win;
            audioSource.Play();
            worldAnimator.Stop();
        }
    }

    private void UpdateScoreText()
    {
        score.text = "Score: " + coins.ToString();
    }

    void OnBecameInvisible()
    {
        Application.LoadLevel("Level_01");
    }
}
