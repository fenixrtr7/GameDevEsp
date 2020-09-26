using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelDance : ScriptableObject
{
    public float tempo = 1f;
    public List<Key> keys;

    public class Key
    {
        public TypeArrow direction;
        public bool visible = true;
        public bool isRandom;
        public bool isSpecial;
        public int damage = 1;
    }
}
