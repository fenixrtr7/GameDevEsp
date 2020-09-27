using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Manager<Spawner>
{
    [SerializeField] GameObject[] arrows;
    [SerializeField]float timeToSpawn = 3;
    public ArrowSongDirections duel;
    public GameObject[] arrowObj;
    public GameObject[] arrowObjS;
    
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
            if (i == 0)
            {
                if (duel.song == null)
                    Debug.LogError("The duel hasn't a song assigned");
                else
                    StartCoroutine(EPlaySongDelayed(duel.song));
            }
            yield return new WaitForSeconds(timeToSpawn);

            duel.keys[i].objectPrefab = duel.keys[i].AssignSprite();
            Instantiate(duel.keys[i].objectPrefab, this.transform.position, Quaternion.identity);

            timeToSpawn = duel.keys[i].tempo;
        }

        //Debug.Log("End spawn");
        yield return new WaitForSeconds(5);
        
        CombatManager.Instance.EndCombat();
    }

    private IEnumerator EPlaySongDelayed(AudioClip song)
    {
        yield return new WaitForSeconds(.5f);
        AudioManager.Instance.PlayClipInSource("MainAudioSource", song);
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
