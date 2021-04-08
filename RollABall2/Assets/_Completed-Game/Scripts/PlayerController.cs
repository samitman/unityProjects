using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {
	
	
	public float speed = 10f;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

    //for jumping
    public LayerMask groundLayers;
    public float jumpForce = 5;
    public SphereCollider col;

    //pickup sound
    public AudioClip coinSound;

    //victory sound
    public AudioClip victorySound;

    //jump sound
    public AudioClip jumpSound;

    //pickup scale change
    private Vector3 scaleChange = new Vector3(-0.05f, -0.05f, -0.05f);

    //time variables
    public Text timeText;

    //speed boost timer
    public float boostWait = 2.0f;
    public float boostTimer;

    //mute toggle
    public bool toggleMute = false;
	
	void Start ()
	{
		
		rb = GetComponent<Rigidbody>();
        //grab a reference to sphere collider
        col = GetComponent<SphereCollider>();
		
		count = 0;
		SetCountText ();
		winText.text = "";

        //makes the time text empty
        timeText.text = "";

        //sets boost timer
        boostTimer = Time.time;

	}

	
	void FixedUpdate ()
	{
		
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);

        //when spacebar is pressed, vertical force gets applied to make player jump
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.velocity = new Vector3(moveHorizontal,0.5f,moveVertical);

            //plays jump sound
            AudioSource.PlayClipAtPoint(jumpSound,transform.position);
        }

        //speed boost if x is pressed
        boostTimer += Time.deltaTime;
        if (boostTimer >= boostWait)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                rb.velocity *= 2;
                boostTimer = 0;
            }
        }

	}

    void Update()
    {
        //mute command (m key)
        if (Input.GetKeyDown(KeyCode.M))
        {
            toggleMute = !toggleMute;

            if (toggleMute)
            {
                AudioListener.volume = 0f; //mutes sounds
            }
            else
            {
                AudioListener.volume = 1f; //unmutes
            }
        }
    }

    //checks if player is on the ground
    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, 
            col.bounds.min.y, col.bounds.center.z),col.radius * .9f, groundLayers);
    }

	
	void OnTriggerEnter(Collider other) 
	{
		
		if (other.gameObject.CompareTag ("Pick Up"))
		{
    
            other.gameObject.SetActive (false);

            //modified the function to play the coin pickup sound when colliding with a pickup
            AudioSource.PlayClipAtPoint(coinSound, transform.position);

            //when a pickup is collected, the rest shrink to make the game harder!
            gameObject.transform.localScale += scaleChange;

            count = count + 1;
			SetCountText ();

		}

        //Lowers the speed of the player if they collide with a wall
        if (other.gameObject.CompareTag ("Wall"))
        {
            rb.velocity = Vector3.zero;
            speed *=0.9f;
            
        }
	}

	
	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) 
		{
			
			winText.text = "You Win!";

            //plays victory sound
            gameObject.GetComponent<AudioSource>().clip = victorySound;
            gameObject.GetComponent<AudioSource>().Play();

            timeText.text = "Time: " + Time.realtimeSinceStartup.ToString().Substring(0,5) + "s";
		}
	}
}