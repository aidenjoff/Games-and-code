using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour {

	[SerializeField] GameObject enemyPos;
	Vector3 lookDir;

	public GameObject shooting; //audio source

	public GameObject bullet; //whats being shot
	public GameObject cannon; //the point at where it is being shot from
	// Use this for initialization
	void Start () 
	{
        //shooting is finding an audio point to play shooting sound effects
		shooting = GameObject.Find ("TurretShotAudio");

        //the player who created the turret has the turrets tag set to the same as theirs so that it could figure out who to target. a turret could still hurt both players however
		if (gameObject.tag == "TurretP1")
		{
            //finds the player 2 tank
			enemyPos = GameObject.Find ("P2Tank");
		}
		else if (gameObject.tag == "TurretP2")
		{
            //finds teh player 1 tag
			enemyPos = GameObject.Find ("P1Tank");
		}
		else
		{
            //simple error check
			Debug.Log ("Enemy detect Error");
		}

        
		StartCoroutine ("shotFreq");
		StartCoroutine ("timeAlive");
	}
	
	
	void Update ()
	{
	    //this would take the enemies position and set the Y value to 0. this is incase the enemy was super close it would cause the cannon to look upwards at it.
		lookDir = enemyPos.transform.position;

		lookDir.y = 0;
		transform.LookAt (lookDir);
	}

	void OnCollisionEnter (Collision other) //allows players to destroy turrets by shooting them
	{
		if (other.gameObject.tag == "Bullet") {
			Destroy (gameObject);
		}
	}

	IEnumerator shotFreq ()
	{
		while (true)
		{
			yield return new WaitForSeconds (0.5f); //wait half a second so that it didn't instantly shoot when created as it had to begin aiming first
			GameObject newMissle = Instantiate (bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
			newMissle.GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * 10);
			shooting.GetComponent<AudioSource>().Play ();
			yield return new WaitForSeconds (2.5f);
		}
	}

	IEnumerator timeAlive()
	{
		yield return new WaitForSeconds (10);
		Destroy (gameObject);
	}
}
