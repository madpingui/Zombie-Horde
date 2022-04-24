using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawn : MonoBehaviour {

    [SerializeField]
    private GameObject spawnee;

    private int contador;

    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private float spawnDelay;

    void Start ()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
	}

    public void SpawnObject()
    {
        GameObject enemmigo = Instantiate(spawnee, transform.position, transform.rotation);
        enemmigo.transform.parent = transform;

        contador++;

        if(contador >= 5)
        {
            spawnDelay -= 0.2f;
            if(spawnDelay < 0.5f)
            {
                spawnDelay = 0.5f;
            }
        }
    }
}
