using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeArrow
{
    NONE,
    UP,
    DOWN,
    RIGHT,
    LEFT
}

[CreateAssetMenu]
public class DuelDance : ScriptableObject
{
    public List<Key> keys;
    public int damage = 1;

    [System.Serializable]

    public class Key
    {
        public TypeArrow direction;
        public float tempo = 1f;
        public bool visible = true;
        public bool isRandom;
        public bool isSpecial;
        public int damage = 1;
        public GameObject objectPrefab;
    }
}
