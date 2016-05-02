using UnityEngine;
using System.Collections;

public class cameraScaler : MonoBehaviour {

	public float orthographicSize = 5.0f;
	public float aspectRatio = 1.3333f;

	//This class basically just scales our camera at start up so that the app looks good on any device
	void Start()
	{
		Camera.main.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspectRatio, orthographicSize * aspectRatio,
			-orthographicSize * (2.0f-aspectRatio), orthographicSize * (2.0f-aspectRatio),
			camera.nearClipPlane, camera.farClipPlane);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
