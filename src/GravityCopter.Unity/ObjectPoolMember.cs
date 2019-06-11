using UnityEngine;

namespace GravityCopter.Unity {

    public class ObjectPoolMember : MonoBehaviour {

        public GameObjectPool ObjectPool { get; private set; }

        private void Awake() => DependencyInjector.ResolveDependenciesOf(this);

        public void Inject(GameObjectPool objectPool) => ObjectPool = objectPool;

        public void Return() => ObjectPool.Return(this);

    }

}
