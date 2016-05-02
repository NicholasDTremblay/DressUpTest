using UnityEngine;
using System.Collections;

public class snapBottomRight : MonoBehaviour {
	//again, this was originally meant just for snapping, but other stuff snuck in here
	public float orthographicSize = 5.0f;
	public float aspectRatio = 1.3333f;
	public GameObject myMask;
	// Use this for initialization
	void Start () {
		
		BoxCollider2D boxColl = gameObject.GetComponent<BoxCollider2D>();//this is getting a reference to our hitbox
		//This is the snapping
		Vector3 myStart = new Vector3((orthographicSize * aspectRatio)-boxColl.size.x/2,(-orthographicSize * (2.0f-aspectRatio))+boxColl.size.y/2,0);
		this.gameObject.transform.position = myStart;
	}
	
	// Update is called once per frame
	void Update () {
		Touch curTouch;
		if(Input.touches.Length > 0)//so here we check our first touch
		{
			curTouch = Input.touches[0];
			if(curTouch.phase == TouchPhase.Began)//if it's the beginning
			{
				Vector2 ray = Camera.main.ScreenToWorldPoint(curTouch.position);
				RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
				if(hit.collider.tag == "restart")//and it hit our hitbox
				{
					//here we reset our mask, which will float nicely back home
					maskController tempCont = myMask.GetComponent<maskController>();
					tempCont.locked = true;
					snapBottomLeft tempSnap = myMask.GetComponent<snapBottomLeft>();
					tempSnap.reset = true;
				}
			}
		}
	}
}
