using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private GameObject _prefabToSpawn;
        [SerializeField] private Transform _spawnHere;
        [SerializeField] private int _countSpawn;
        [SerializeField] private GameObject _parentToSpawn;
        [SerializeField] private float _timerToSpawn;

        #endregion Inspector variables

        #region private variables

        private List<GameObject> spawned = new List<GameObject>();
        private bool isReady;

        #endregion private variables

        #region Unity functions

        private void Start()
        {
            Init();
        }

        #endregion Unity functions

        #region public functions

        public GameObject GetObject()
        {
            var find = spawned.FirstOrDefault(x => !x.activeSelf);
            if (find == null)
            {
                var newObject = SpawnByCount(1);
                return newObject;
            }

            return find;
        }

        #endregion public functions

        #region private functions

        private void Init()
        {
            isReady = true;
            SpawnByCount(_countSpawn);
            StartCoroutine(OpenByTimer(_timerToSpawn));
        }
        
        private IEnumerator OpenByTimer(float time)
        {
            while (isReady)
            {
                var objNew = GetObject();
                objNew.SetActive(true);
                objNew.transform.position = NewPosition();
                objNew.GetComponent<MeteorElement>().InitMeteor();
                yield return new WaitForSeconds(time);
            }
        }

        private GameObject SpawnByCount(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var spawnObject = Instantiate(_prefabToSpawn, NewPosition(), Quaternion.identity);
                spawnObject.transform.SetParent(_parentToSpawn.transform);
                spawned.Add(spawnObject);
                spawnObject.SetActive(false);
                return spawnObject;
            }

            return null;
        }

        private Vector2 NewPosition()
        {
            var box = _spawnHere.GetComponent<BoxCollider2D>();
            Vector2 colliderSize = box.size;
            Vector2 colliderOffset = box.offset;
            Vector2 colliderPosition = box.transform.position;
            
            float randomX = Random.Range(-colliderSize.x / 2, colliderSize.x / 2);
            float randomY = Random.Range(-colliderSize.y / 2, colliderSize.y / 2);
            Vector2 randomPosition = colliderPosition + colliderOffset + new Vector2(randomX, randomY);
            return randomPosition;
        }

        #endregion private functions
    }
}