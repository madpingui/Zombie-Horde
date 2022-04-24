using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour {

    public GameObject[] bosses;

    public float r = 5f;

    // Update is called once per frame
    void Update ()
    {
		if(Time.time > 20)
        {
            float angle = Random.Range(0, Mathf.PI * 2);
            Vector3 pos3d = new Vector3(Mathf.Sin(angle) * r, 0, Mathf.Cos(angle) * r);

            GameObject enemmigo = Instantiate(bosses[0], pos3d, transform.rotation);
            enemmigo.transform.parent = transform.parent;
        }
	}
}
