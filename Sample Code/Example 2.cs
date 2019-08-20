
//This would rotate a camera to face the player and the camera would be dispayed on a wall creating a mirror

	public Transform mirror;
	public Transform player;


	// Update is called once per frame
	void Update ()
    {

		Vector3 dir = (player.position - transform.position).normalized;
		Quaternion rot = Quaternion.LookRotation (dir);

		rot.eulerAngles = transform.eulerAngles - rot.eulerAngles;

		mirror.localRotation = rot;

	}

