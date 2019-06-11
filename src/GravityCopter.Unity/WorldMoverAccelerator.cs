using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Inputs;

namespace GravityCopter.Unity {

    public class WorldMoverAccelerator : Updatable {

        public StartStopInput AccelerateInput;
        public StartStopInput DecelerateInput;
        public float SpeedDelta = 2f;

        private WorldMover _worldMover;

        protected override void BetterAwake() {
            base.BetterAwake();

            Assert.IsNotNull(AccelerateInput, this.GetAssociationAssertion(nameof(AccelerateInput)));
            Assert.IsNotNull(DecelerateInput, this.GetAssociationAssertion(nameof(DecelerateInput)));

            RegisterUpdatesAutomatically = true;
            BetterUpdate = accelerate;
        }

        public void Inject(WorldMover worldMover) {
            _worldMover = worldMover;
        }

        private void accelerate(float deltaTime) {
            bool accel = AccelerateInput.Started();
            bool decel = DecelerateInput.Started();
            if (accel ^ decel) {
                float speed = _worldMover.Velocity.magnitude;
                Vector2 worldDir = _worldMover.Velocity / speed;
                _worldMover.Velocity = (speed + (accel ? 1f : -1f) * SpeedDelta) * worldDir;
            }
        }

    }

}
