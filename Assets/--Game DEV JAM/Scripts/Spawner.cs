using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Manager<Spawner>
{
    [SerializeField] GameObject[] arrows;
    float timeToSpawn = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnArrow()
    {
        yield return new WaitForSeconds(timeToSpawn);
        Instantiate(arrows[Random.Range(0, arrows.Length)]);

        StartCoroutine(SpawnArrow());
    }
}
