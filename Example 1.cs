
// This script would cause a bullet to gain a random rotation offset before shooting off.
// Multiple shots were fired from a shotgun and this script would cause the shotgun pellets to create a spread shot instead of all spawning and flying forward.

	void Start ()
    {

    Vector3 shotSpread, dir;

    shotSpread = new Vector3(Random.Range(transform.forward.x - 0.5f, transform.forward.x + 0.5f), Random.Range(transform.forward.y - 0.5f, transform.forward.y + 0.5f), Random.Range(transform.forward.z - 0.5f, transform.forward.z + 0.5f));

        dir = transform.forward;
        dir.x += shotSpread.x;
        dir.y += shotSpread.y;
        dir.z += shotSpread.z;
        Debug.Log(dir);

        GetComponent<Rigidbody>().velocity = transform.forward + dir * 5;
    }

