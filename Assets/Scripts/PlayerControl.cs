using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	private bool flashing;
	private bool fade;
	public GUIText ItemText;
	private Animator animator;
	private Color original;

	void Start()
	{
		fade = true;
		flashing = false;
		animator = GetComponent<Animator>();
		original = GetComponent<SpriteRenderer>().color;
	}

	void FixedUpdate()
	{
		// Get keyboard input
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		// Store information in 2D Vectors
		Vector2 movement = new Vector2(moveHorizontal, moveVertical);
		Vector2 position = new Vector2(rigidbody2D.position.x, rigidbody2D.position.y);

		// Do math
		rigidbody2D.MovePosition(position + movement);

		// For animation
		if (Input.GetAxis("Horizontal") < 0)
		{
			animator.SetTrigger("Left");
		}

		else if(Input.GetAxis("Horizontal") > 0)
		{
			animator.SetTrigger("Right");
		}

		else if(Input.GetAxis("Vertical") > 0)
		{
			animator.SetTrigger("Up");
		}

		else if(Input.GetAxis("Vertical") < 0)
		{
			animator.SetTrigger("Down");
		}

		// If the player is currently on the Ghost layer, make them slightly translucent
		if(this.gameObject.layer == 9 && fade && !flashing)
		{
			StartCoroutine(Fade());
		}
		else if(this.gameObject.layer == 9 && !fade && !flashing)
		{
			StartCoroutine(Unfade());
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		// When player hits an object with the "Item" tag...
		if (other.gameObject.tag == "Item") {
			// ... and the player gets moved to layer 8 (Currently the "Action" layer)
			this.gameObject.layer = 8;
			ItemText.text = "Head to the stash!";

			// ... animation changes as well
			animator.SetTrigger ("ItemGet");
		}

		// When player hits an object with the "Goal" tag...
		else if (other.gameObject.tag == "Goal") {	
			// ... the player gets moved back to layer 9 (Currently the "Ghost" layer)
			this.gameObject.layer = 9;
			ItemText.text = "Good job!";

			// ... player animation changes back
			animator.SetTrigger ("GoalMet");
			
			// ... the animation for the goal changes as well
			other.GetComponent<Animator> ().SetTrigger ("GoalMet");

			// make item reappear
			StartCoroutine(Reset());

		} else if (other.gameObject.tag == "Enemy")
		{
			// Move player back to layer 9
			this.gameObject.layer = 9;
			flashing = true;
			ItemText.text = "Ouch!";

			// Flash red
			StartCoroutine(Flash(.1f));

			// Make item reappear
			GameObject temp = GameObject.FindWithTag("Item");
			Color temporary = temp.GetComponent<SpriteRenderer> ().color;
			temporary.a = 1;
			temp.GetComponent<SpriteRenderer> ().color = temporary;
		}
	}

	IEnumerator Flash(float time)
	{
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(time);
		GetComponent<SpriteRenderer>().color = original;
		yield return new WaitForSeconds(time);
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(time);
		GetComponent<SpriteRenderer>().color = original;
		yield return new WaitForSeconds(time);
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(time);
		GetComponent<SpriteRenderer>().color = original;
		yield return new WaitForSeconds(time);
		ItemText.text = "Try again!";
		flashing = false;
	}

	IEnumerator Fade()
	{
		while(this.GetComponent<SpriteRenderer> ().color.a > .5f)
		{
			Color temporary = this.GetComponent<SpriteRenderer> ().color;
			yield return new WaitForSeconds(.01f);
			temporary.a = temporary.a - .01f;
			GetComponent<SpriteRenderer> ().color = temporary;
		}
		fade = false;
	}

	IEnumerator Unfade()
	{
		while (this.GetComponent<SpriteRenderer> ().color.a != 1f)
		{
			Color temporary = this.GetComponent<SpriteRenderer> ().color;
			yield return new WaitForSeconds (.01f);
			temporary.a = temporary.a + .01f;
			GetComponent<SpriteRenderer> ().color = temporary;
		}
		fade = true;
	}

	IEnumerator Reset()
	{
		yield return new WaitForSeconds (1f);
		ItemText.text = "Get the next booklet!";
		GameObject.FindWithTag("Item").GetComponent<ItemScript>().Reappear();
		GameObject.FindWithTag("Goal").GetComponent<Animator> ().SetTrigger ("GoalReset");
		animator.SetTrigger ("GoalMet");
	}
}