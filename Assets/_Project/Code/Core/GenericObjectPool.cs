using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Core
{
    public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
    {
#pragma warning disable 0649 
        [SerializeField] private T _prefab;
#pragma warning restore 0649

        private Queue<T> _objectsQueue = new Queue<T>();

        public static GenericObjectPool<T> Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public T Get()
        {
            if (_objectsQueue.Count == 0)
            {
                AddObjects(1);
            }

            return _objectsQueue.Dequeue();
        }

        public void ReturnToPool(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            _objectsQueue.Enqueue(objectToReturn);
        }

        private void AddObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newObject = GameObject.Instantiate(_prefab, this.transform, true);
                newObject.gameObject.SetActive(false);
                _objectsQueue.Enqueue(newObject);
            }
        }
    }
}