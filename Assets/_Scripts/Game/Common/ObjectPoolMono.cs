using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    public class ObjectPoolMono<T> where T : MonoBehaviour
    {
        public bool AutoExpand { get; set; } = false;
        public int Count { get; }
        public List<T> Pool => _pool;
        private Transform Container { get; }
        private T Prefab { get; }
        private List<T> _pool;
        private List<T> _prefabsList;

        public ObjectPoolMono(T prefab, int count, Transform container = null)
        {
            Prefab = prefab;
            Container = container;
            Count = count;

            CreatePool(count);
        }

        public ObjectPoolMono(List<T> prefabsList, int count, Transform container = null)
        {
            _prefabsList = prefabsList;
            Container = container;
            Count = count;

            CreatePoolList(count);
        }

        public T GetFreeElement(bool isActiveByDefualt = true)
        {
            if (HasFreeElement(out T element, isActiveByDefualt))
            {
                return element;
            }

            if (!AutoExpand)
            {
                Debug.LogWarning($"Pool {nameof(_pool)} is empty. Method {nameof(GetFreeElement)} return null");
                return null;
            }

            if (Prefab != null)
            {
                return CreateObject(true);
            }

            return CreateObjectListPrefabs(true);
        }

        public bool HasFreeElement(out T element, bool isActiveByDefault = true)
        {
            foreach (var mono in _pool)
            {
                if (!mono.gameObject.activeInHierarchy)
                {
                    element = mono;
                    mono.gameObject.SetActive(isActiveByDefault);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public void ReturnObjectsInPool()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                _pool[i].gameObject.SetActive(false);
            }
        }

        public void SetActiveTrueAllElements()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                _pool[i].gameObject.SetActive(true);
            }
        }

        private T CreateObject(bool isActiveByDefualt = false)
        {
            var createdObject = Object.Instantiate(Prefab, Container);
            createdObject.gameObject.SetActive(isActiveByDefualt);
            _pool.Add(createdObject);
            return createdObject;
        }

        private T CreateObjectListPrefabs(bool isActiveByDefualt = false)
        {
            var createdObject =
                Object.Instantiate(_prefabsList[Random.Range(0, _prefabsList.Count)], Container);
            createdObject.gameObject.SetActive(isActiveByDefualt);
            _pool.Add(createdObject);
            return createdObject;
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private void CreatePoolList(int count)
        {
            _pool = new List<T>(count);

            for (int i = 0; i < count; i++)
            {
                CreateObjectListPrefabs();
            }
        }
    }
}