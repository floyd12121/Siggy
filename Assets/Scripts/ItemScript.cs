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
	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// When player hits an object with the "Item" tag...
		if (other.gameObject.tag == "Player")
		{
			// Item disappears
			Color temp = original;
			temp.a = 0f;
			GetComponent<SpriteRenderer> ().color = temp;
		}
	}

	public void Reappear()
	{
		Color temp = original;
		temp.a = 1f;
		GetComponent<SpriteRenderer> ().color = temp;
		transform.position = new Vector2(0f, 0f);
	}
}
