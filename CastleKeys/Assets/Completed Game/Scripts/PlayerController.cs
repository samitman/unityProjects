using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {
	
	
	public float speed = 10f;
	public Text countText;
	public Text winText;

    //added losing text
    public Text loseText;

    //instructions
    public Text instructions;

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

    //lose sound
    public AudioClip loseSound;
    public bool isGameOver = false;
   

    //time variables
    public Text timeText;
    public float seconds = 300f;

    //mute toggle
    public bool toggleMute = false;

    //door objects
    public GameObject door;
    public GameObject jailDoor;

    //key
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    public GameObject key4;
    public GameObject key5;
	
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

        //makes the lose text empty
        loseText.text = "";

        instructions.text = "Use Arrows/WASD to move Hold shift to run            Press Space to jump    Right Click to paint        Click to clear instructions";

    }

	
	void FixedUpdate ()
	{
		
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);

	}

    void Update()
    {
        //removes instructions
        if (Input.GetMouseButtonDown(0))
        {
            instructions.text = "";
        }
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

        //displays lose text and plays lose sound if time runs out
        if (seconds > 0 && count!=5)
        {
            seconds -= Time.deltaTime;
            timeText.text = seconds.ToString("#");
        }
        if(seconds<=0 && isGameOver == false && count!=5)
        {
            loseText.text = "Game Over";
            isGameOver = true;
            gameObject.GetComponent<AudioSource>().clip = loseSound;
            gameObject.GetComponent<AudioSource>().Play();

            timeText.text = "";

            //prevent's player from unlocking castle
            Destroy(key1);
            Destroy(key2);
            Destroy(key3);
            Destroy(key4);
            Destroy(key5);
            count = 0;
            countText.text = "";

        }
    }

	
	void OnTriggerEnter(Collider other) 
	{
		
		if (other.gameObject.CompareTag ("Pick Up"))
		{
    
            other.gameObject.SetActive (false);

            //modified the function to play the coin pickup sound when colliding with a pickup
            AudioSource.PlayClipAtPoint(coinSound, transform.position);


            count = count + 1;
			SetCountText ();

            //logic that unlocks the door to the jail, and the last key for the castle
            if (count == 4)
            {
                Destroy(jailDoor);
            }
            if (count == 5)
            {
                Destroy(door); 
            }

		}

	}

	
	void SetCountText()
	{
		countText.text = "Keys: " + count.ToString () + "/5";
		if (count >= 5) 
		{
			
			winText.text = "You Win!";

            //plays victory sound
            gameObject.GetComponent<AudioSource>().clip = victorySound;
            gameObject.GetComponent<AudioSource>().Play();

            timeText.text = "Time: " + Time.realtimeSinceStartup.ToString().Substring(0,5) + "s";
		}
	}
}