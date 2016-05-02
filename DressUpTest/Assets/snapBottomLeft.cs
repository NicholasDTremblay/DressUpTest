using UnityEngine;
using System.Collections;

//This class finds the bottom left of our camera and moves the object there
//requires 2d box collider
public class snapBottomLeft : MonoBehaviour {

	//So whe I started writing this, it was just supposed to be for snapping to the edge of the camera view, but as you can see, it's holding a bit more
	public float orthographicSize = 5.0f;
	public float aspectRatio = 1.3333f;
	Vector3 myStart;
	public bool reset = false;
	private const float d2r = 0.01745329251994329576923690768489f;
	private const float r2d = 57.295779513082320876798154814105f;
	public float speed = 0;
	public float angle = 0;
	public float velx = 0;
	public float vely = 0;

	maskController myMaskController;

	// Use this for initialization
	void Start () {
		//this is getting references to our hit box and mask controller which we need to properly position ourselves and change variables repectively
		BoxCollider2D boxColl = gameObject.GetComponent<BoxCollider2D>();
		myMaskController = this.gameObject.GetComponent<maskController>();
		//this is the actual snapping
		myStart = new Vector3((-orthographicSize * aspectRatio)+boxColl.size.x/2,(-orthographicSize * (2.0f-aspectRatio))+boxColl.size.y/2,0);
		this.gameObject.transform.position = myStart;
	}
	
	// Update is called once per frame
	void Update () {
		//so we only care if we're resetting to our start position here
		if(reset == true)
		{
			if(speed == 0)//if we just started
			{
				//fancy directional math that'll point us back to our start position
				speed = 10;

				angle = Mathf.Atan2(myStart.y - gameObject.transform.position.y, myStart.x - gameObject.transform.position.x)*r2d;

				velx = Mathf.Cos(angle*d2r)*speed;
				vely = Mathf.Sin(angle*d2r)*speed;
			}
			if(speed > 0)
			{
				//here we applay that fancy math we did to our position
				Vector3 oldPos = gameObject.transform.position;
				gameObject.transform.position = new Vector3(oldPos.x + (velx*Time.deltaTime),oldPos.y + (vely*Time.deltaTime), 0);

				if(gameObject.transform.position.x < myStart.x || gameObject.transform.position.y < myStart.y)
				{
					gameObject.transform.position = myStart;
					speed = 0;
					reset = false;
					myMaskController.locked = false;
				}
			}
		}
	}
}
