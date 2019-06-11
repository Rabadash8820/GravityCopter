using UnityEngine;
using UnityEngine.Events;

namespace GravityCopter.Unity {

    public class WorldEnd : MonoBehaviour {

        private WorldBuilder _worldBuilder;

        public UnityEvent WallPassed = new UnityEvent();

        private void Awake() {
            DependencyInjector.ResolveDependenciesOf(this);
        }

        public void Inject(WorldBuilder worldBuilder) {
            _worldBuilder = worldBuilder;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            WorldColumn otherCol = other.attachedRigidbody.GetComponentInChildren<WorldColumn>();
            if (otherCol == null)
                return;

            _worldBuilder.RecycleColumn(otherCol);

            WallPassed.Invoke();
        }

    }

}
