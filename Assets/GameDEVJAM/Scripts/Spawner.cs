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
            int index = CombatManager.Instance.enemyIndex;

            if (index == 0)
            {
                if (duel.keys[i].direction == TypeArrow.DOWN)
                {
                    EventManager.Instance.dic_dynamicObjects["Charlie"].objectRelated.GetComponent<CharacterAnimaController>().Dance_01();
                } else if (duel.keys[i].direction == TypeArrow.LEFT)
                {
                    EventManager.Instance.dic_dynamicObjects["Charlie"].objectRelated.GetComponent<CharacterAnimaController>().Dance_02();
                }
                else if (duel.keys[i].direction == TypeArrow.UP)
                {
                    EventManager.Instance.dic_dynamicObjects["Charlie"].objectRelated.GetComponent<CharacterAnimaController>().Dance_03();
                } else if (duel.keys[i].direction == TypeArrow.RIGHT)
                {
                    EventManager.Instance.dic_dynamicObjects["Charlie"].objectRelated.GetComponent<CharacterAnimaController>().Dance_04();
                }
            }
            else if (index == 1)
            {
                if (duel.keys[i].direction == TypeArrow.DOWN)
                {
                    EventManager.Instance.dic_dynamicObjects["Brad"].objectRelated.GetComponent<CharacterAnimaController>().Dance_01();
                } else if (duel.keys[i].direction == TypeArrow.UP)
                {
                    EventManager.Instance.dic_dynamicObjects["Brad"].objectRelated.GetComponent<CharacterAnimaController>().Dance_02();
                }
                else if (duel.keys[i].direction == TypeArrow.RIGHT)
                {
                    EventManager.Instance.dic_dynamicObjects["Brad"].objectRelated.GetComponent<CharacterAnimaController>().Dance_03();
                } else if (duel.keys[i].direction == TypeArrow.LEFT)
                {
                    EventManager.Instance.dic_dynamicObjects["Brad"].objectRelated.GetComponent<CharacterAnimaController>().Dance_04();
                }
            }
            else if (index == 2)
            {
                if (duel.keys[i].direction == TypeArrow.DOWN)
                {
                    EventManager.Instance.dic_dynamicObjects["Caico"].objectRelated.GetComponent<CharacterAnimaController>().Dance_01();
                } else if (duel.keys[i].direction == TypeArrow.UP)
                {
                    EventManager.Instance.dic_dynamicObjects["Caico"].objectRelated.GetComponent<CharacterAnimaController>().Dance_02();
                }
                else if (duel.keys[i].direction == TypeArrow.RIGHT)
                {
                    EventManager.Instance.dic_dynamicObjects["Caico"].objectRelated.GetComponent<CharacterAnimaController>().Dance_03();
                } else if (duel.keys[i].direction == TypeArrow.LEFT)
                {
                    EventManager.Instance.dic_dynamicObjects["Caico"].objectRelated.GetComponent<CharacterAnimaController>().Dance_04();
                }
            }

            duel.keys[i].objectPrefab = duel.keys[i].AssignSprite();
            //Debug.Log("I " + i);
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
