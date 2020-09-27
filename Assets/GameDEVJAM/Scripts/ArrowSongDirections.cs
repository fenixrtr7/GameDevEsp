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
public class ArrowSongDirections : ScriptableObject
{
    public List<Key> keys;
    [HideInInspector] public int damage = 10;

    [System.Serializable]
    public class Key
    {
        public TypeArrow direction;
        public float tempo = 1f;
        [HideInInspector] public bool visible = true;
        public bool isRandom;
        public bool isSpecial;
        [HideInInspector] public int damage = 1;
        [HideInInspector] public GameObject objectPrefab;

        public Key(TypeArrow _direction, float _tempo, bool _visible, bool _isRandom, bool _isSpecial, int _damage = 1)
        {
            this.direction = _direction;
            this.tempo = _tempo;
            this.visible = _visible;
            this.isRandom = _isRandom;
            this.isSpecial = _isSpecial;
            this.damage = _damage;
            //this.objectPrefab = AssignSprite();
        }

        public GameObject AssignSprite()
        {
            GameObject gObj = null;
            if (isRandom)
            {
                direction = (TypeArrow)Random.Range(1, 5);
            }

            if (!isSpecial)
            {
                switch (direction)
                {
                    case TypeArrow.UP:
                        gObj = Spawner.Instance.arrowObj[0];
                        break;

                    case TypeArrow.DOWN:
                        gObj = Spawner.Instance.arrowObj[1];
                        break;

                    case TypeArrow.RIGHT:
                        gObj = Spawner.Instance.arrowObj[2];
                        break;

                    case TypeArrow.LEFT:
                        gObj = Spawner.Instance.arrowObj[3];
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case TypeArrow.UP:
                        gObj = Spawner.Instance.arrowObjS[0];
                        break;

                    case TypeArrow.DOWN:
                        gObj = Spawner.Instance.arrowObjS[1];
                        break;

                    case TypeArrow.RIGHT:
                        gObj = Spawner.Instance.arrowObjS[2];
                        break;

                    case TypeArrow.LEFT:
                        gObj = Spawner.Instance.arrowObjS[3];
                        break;
                }
            }


            return gObj;
        }
    }
}
