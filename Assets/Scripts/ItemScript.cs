using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	private Color original;

	// Use this for initialization
	void Start ()
	{
		original = GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// When player hits an object with the "Item" tag...
		if (other.gameObject.tag == "Player")
		{
			Color temp = original;
			temp.a = 0f;
			GetComponent<SpriteRenderer> ().color = temp;
		}
	}
}
