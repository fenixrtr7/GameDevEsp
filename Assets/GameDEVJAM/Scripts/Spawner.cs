using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Manager<Spawner>
{
    [SerializeField] GameObject[] arrows;
    [SerializeField]float timeToSpawn = 3;
    public DuelDance duel;
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

    public IEnumerator SpawnArrowDuel()
    {
        for (int i = 0; i < duel.keys.Count; i++)
        {
            yield return new WaitForSeconds(timeToSpawn);
            Instantiate(duel.keys[i].objectPrefab);

            timeToSpawn = duel.keys[i].tempo;
        }

        Debug.Log("End");
    }
}
