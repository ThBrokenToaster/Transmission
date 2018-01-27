using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public float smoothingEffect;
    Vector3 offset;
    float yLowerBound;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.position;
        yLowerBound = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothingEffect * Time.deltaTime);

        if (transform.position.y < yLowerBound)
        {
            transform.position = new Vector3(transform.position.x, yLowerBound, transform.position.z);
        }
    }
}
