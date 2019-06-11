using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravityCopter.Unity {

    public class GameObjectPool : MonoBehaviour, IReadOnlyCollection<ObjectPoolMember> {

        private IList<ObjectPoolMember> _members = new List<ObjectPoolMember>();

        public uint StartCount = 1;
        public GameObject Original;
        public Transform ParentTransform;

        public void Recyle(ObjectPoolMember poolMember, Transform transform, bool reactivate = false) =>
            Recyle(poolMember, transform.position, transform.rotation, reactivate);
        public void Recyle(ObjectPoolMember poolMember, Vector3 position, Quaternion rotation, bool reactivate = false) {
            if (reactivate)
                poolMember.gameObject.SetActive(false);

            poolMember.transform.position = position;
            poolMember.transform.rotation = rotation;

            if (reactivate)
                poolMember.gameObject.SetActive(true);
        }
        public void Return(ObjectPoolMember poolMember) {
            poolMember.gameObject.SetActive(false);
            _members.Add(poolMember);
        }
        public ObjectPoolMember RequestAt(Transform transform) => RequestAt(transform.position, transform.rotation);
        public ObjectPoolMember RequestAt(Vector3 position, Quaternion rotation) {
            ObjectPoolMember poolMember;

            if (_members.Count > 0) {
                poolMember = _members[_members.Count - 1];
                _members.RemoveAt(_members.Count - 1);
                poolMember.transform.position = position;
                poolMember.transform.rotation = rotation;
                poolMember.gameObject.SetActive(true);
            }

            else {
                GameObject obj = Instantiate(Original, position, rotation, ParentTransform);
                poolMember = obj.GetComponent<ObjectPoolMember>() ?? obj.AddComponent<ObjectPoolMember>();
            }

            return poolMember;
        }

        private void Awake() {
            if (StartCount == 0)
                _members = new List<ObjectPoolMember>();
            else {
                _members = new List<ObjectPoolMember>((int)StartCount);
                for (int obj = 0; obj < StartCount; ++obj) {
                    GameObject instance = Instantiate(Original, ParentTransform);
                    instance.SetActive(false);
                    ObjectPoolMember poolMember = instance.GetComponent<ObjectPoolMember>() ?? instance.AddComponent<ObjectPoolMember>();
                    _members.Add(poolMember);
                }
            }
        }

        public int Count => _members.Count;
        public void Clear() => _members.Clear();
        public void CopyTo(ObjectPoolMember[] array, int arrayIndex) => _members.CopyTo(array, arrayIndex);

        public IEnumerator<ObjectPoolMember> GetEnumerator() => _members.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _members.GetEnumerator();

    }

}
