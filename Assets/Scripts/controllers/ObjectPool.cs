using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private GameObject _prefabToSpawn;
        [SerializeField] private Transform _spawnHere;
        [SerializeField] private int _countSpawn;
        [SerializeField] private GameObject _parentToSpawn;

        #endregion Inspector variables

        #region private variables

        private List<GameObject> spawned;

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
            SpawnByCount(_countSpawn);
        }

        private GameObject SpawnByCount(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var spawnObject = Instantiate(_prefabToSpawn, _spawnHere);
                spawnObject.transform.SetParent(_parentToSpawn.transform);
                spawnObject.SetActive(false);
                spawned.Add(spawnObject);
                return spawnObject;
            }

            return null;
        }

        #endregion private functions
    }
}