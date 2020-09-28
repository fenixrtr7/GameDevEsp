using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Manager<Spawner>
{
    [SerializeField] GameObject[] arrows;
    float timeToSpawn = 0;
    public ArrowSongDirections duel;
    public GameObject[] arrowObj;
    public GameObject[] arrowObjS;
    public Coroutine spawnArrorDuelCoroutine;

    public IEnumerator SpawnArrow()
    {
        yield return new WaitForSeconds(timeToSpawn);
        Instantiate(arrows[Random.Range(0, arrows.Length)]);

        StartCoroutine(SpawnArrow());
    }

    public void StopCoroutineSpawnDuel()
    {
        StopCoroutine(spawnArrorDuelCoroutine);
        spawnArrorDuelCoroutine = null;
    }
    
    public IEnumerator SpawnArrowDuel()
    {
        int timeStamp = 0;

        for (int i = 0; i < duel.keys.Count; i++)
        {

            if (i == 0)
            {
                if (duel.song == null)
                    Debug.LogError("The duel hasn't a song assigned");
                else
                    StartCoroutine(EPlaySongDelayed(duel.song));
            }
            if (i > 0)
            {
                timeStamp = i - 1;
            }

            timeToSpawn = duel.keys[i].tempo - duel.keys[timeStamp].tempo;
            //Debug.Log( i + " Time spawn " + timeToSpawn);

            yield return new WaitForSeconds(timeToSpawn);

            duel.keys[i].objectPrefab = duel.keys[i].AssignSprite();
            Instantiate(duel.keys[i].objectPrefab, this.transform.position, Quaternion.identity);
        }

        //Debug.Log("End spawn");
        yield return new WaitForSeconds(2.1f);
        
        CombatManager.Instance.EndCombat(false);
    }

    private IEnumerator EPlaySongDelayed(AudioClip song)
    {
        yield return new WaitForSeconds(1.55f);
        AudioManager.Instance.PlayClipInSource("MainAudioSource", song, 1);
    }

    public void OffArrows()
    {
        var arrows = GameObject.FindGameObjectsWithTag("Arrow");

        foreach (var arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }
}
