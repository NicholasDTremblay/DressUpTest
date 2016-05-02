using UnityEngine;
using System.Collections;

//this will control the mask movement, sounds, and most of everything
public class maskController : MonoBehaviour {

	bool touched = false;
	public bool locked = false;
	Touch myTouch;
	//bool targetFound = false;
	Vector2 offset;
	AudioSource positiveSound;
	AudioSource negativeSound;
	public ParticleSystem myParticles;

	snapBottomLeft mySnap;

	public GameObject myDaisy = null;
	/*
	void onTriggerStay2D(Collider2D other)
	{
		gameObject.audio.Play();
		if(other.tag == "daizy")                      //Not Using These triggers at the moment
		{											  //Currently using some point precision colision instead since it was just easier for this
			targetFound = true;						  //small project
		}
	}
	void onTriggerExit2D(Collider2D other)
	{
		if(other.tag == "daizy")
		{
			targetFound = false;
		}
	}*/
	// Use this for initialization
	void Start () {
		//here we're just setting up sound and script variables that we'll use later
		AudioSource[] audios = GetComponents<AudioSource>();
		positiveSound = audios[0];
		negativeSound = audios[1];
		mySnap = gameObject.GetComponent<snapBottomLeft>();
	}
	
	// Update is called once per frame
	void Update () {
		//this is much less complicated than it looks
		if(locked == false)//so we only care to check our update if we aren't locked from either already being on target, or being moved back to the start position
		{
			if(touched == true)
			{
				if(Input.touches.Length > 0)//So here we only care about a single touch so we only check the first
				{
					myTouch = Input.touches[0];
					if(myTouch.phase == TouchPhase.Moved)//if we're dragging, we move our object
					{
						Vector2 deltaMovement = Camera.main.ScreenToWorldPoint(myTouch.position);
						gameObject.transform.position = deltaMovement + offset;
					}
					else if(myTouch.phase == TouchPhase.Ended)//we have to check hitboxes and stuff once a touch ends
					{
						touched = false;
						BoxCollider2D daisyCollider = myDaisy.GetComponents<BoxCollider2D>()[0];//daizy requires a box collider for this to work
						Vector2 targetPoint = new Vector2(myDaisy.transform.position.x + 0.05f,myDaisy.transform.position.y + 0.225f); //Here is the aformentioned point colision, the point is hard coded since we know the character is always centered
						Vector2 daisyColliderTopLeft = new Vector2(daisyCollider.center.x - (daisyCollider.size.x/2), daisyCollider.center.y + (daisyCollider.size.y/2));
						Vector2 daisyColliderBottomRight = new Vector2(daisyCollider.center.x + (daisyCollider.size.x/2), daisyCollider.center.y - (daisyCollider.size.y/2));
						if(Physics2D.OverlapArea(daisyColliderTopLeft,daisyColliderBottomRight).tag == "mask")//here is if we hit the target
						{
							gameObject.transform.position = new Vector3(targetPoint.x,targetPoint.y,0);
							positiveSound.Play();
							myParticles.Play();
							locked = true;
						}
						else//this is if we don't hit the target
						{
							negativeSound.Play();
							mySnap.reset = true;
							locked = true;
						}
						/*
						if(targetFound)
						{
							gameObject.transform.position = new Vector3(0,0,0);
							gameObject.audio.Play();
						}*/
					}
				}
			}
			else if(touched == false)//this checks for initial touches
			{
				Debug.Log("Not Touched");
				Touch curTouch;
				if(Input.touches.Length > 0)
				{
					curTouch = Input.touches[0];
					if(curTouch.phase == TouchPhase.Began)
					{
						Vector2 ray = Camera.main.ScreenToWorldPoint(curTouch.position);
						RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
						if(hit.collider.tag == "mask")
						{
							touched = true;
							offset = new Vector2(gameObject.transform.position.x - ray.x,gameObject.transform.position.y - ray.y);
						}
					}
				}
			}
		}
	}
}
