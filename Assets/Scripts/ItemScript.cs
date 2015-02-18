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
		float location = Random.value * 10;
		Vector2 locationValues;
		Mathf.Floor (location);
		switch ((int) location)
		{
			case 0:
				locationValues = new Vector2(-40.6f, 27.8f);
				break;
			case 1:
				locationValues = new Vector2(-11f, 20.8f);
				break;
			case 2:
				locationValues = new Vector2(-9.5f, 11.8f);
				break;
			case 3:
				locationValues = new Vector2(47.3f, -20.9f);
				break;
			case 4:
				locationValues = new Vector2(-28.6f, -21.9f);
				break;
			case 5:
				locationValues = new Vector2(8f, 31.8f);
				break;
			case 6:
				locationValues = new Vector2(30f, -10.4f);
				break;
			case 7:
				locationValues = new Vector2(1.5f, -29.5f);
				break;
			case 8:
				locationValues = new Vector2(-48.7f, -24f);
				break;
			case 9:
				locationValues = new Vector2(44.7f, 19.2f);
				break;
			default:
				locationValues = new Vector2(0f, 0f);
				break;
		}

		Color temp = original;
		temp.a = 1f;
		GetComponent<SpriteRenderer> ().color = temp;
		transform.position = locationValues;
	}
}
