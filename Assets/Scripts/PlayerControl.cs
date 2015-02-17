using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public GUIText ItemText;
	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
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
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		// When player hits an object with the "Item" tag...
		if(other.gameObject.tag == "Item")
		{
			// ... the object is destroyed
			Destroy(other.gameObject);

			// ... and the player gets moved to layer 8 (Currently the "Action" layer)
			this.gameObject.layer = 8;
			ItemText.text = "Head to the stash!";

			// ... animation changes as well
			animator.SetTrigger("ItemGet");
		}

		// When player hits an object with the "Goal" tag...
		else if(other.gameObject.tag == "Goal")
		{	
			// ... the player gets moved back to layer 9 (Currently the "Ghost" layer)
			this.gameObject.layer = 9;
			ItemText.text = "Good job!";

			// ... player animation changes back
			animator.SetTrigger("GoalMet");
			
			// ... the animation for the goal changes as well
			other.GetComponent<Animator>().SetTrigger("GoalMet");
		}
	}
}