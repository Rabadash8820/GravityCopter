using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Inputs;

namespace GravityCopter.Unity {

    public class GravityScaler : Updatable {

        public StartStopInput SwitchGInput;
        public StartStopInput IncreaseGInput;
        public StartStopInput DecreaseGInput;
        public float ForceDelta = 5f;
        [Space]
        public AudioSource SwitchGAudio;

        private WorldMover _worldMover;

        protected override void BetterAwake() {
            base.BetterAwake();

            Assert.IsNotNull(SwitchGInput, this.GetAssociationAssertion(nameof(SwitchGInput)));
            Assert.IsNotNull(IncreaseGInput, this.GetAssociationAssertion(nameof(IncreaseGInput)));
            Assert.IsNotNull(DecreaseGInput, this.GetAssociationAssertion(nameof(DecreaseGInput)));

            RegisterUpdatesAutomatically = true;
            BetterUpdate = accelerate;
        }

        public void Inject(WorldMover worldMover) {
            _worldMover = worldMover;
        }

        private void accelerate(float deltaTime) {
            if (SwitchGInput.Started()) {
                SwitchGAudio?.Play();
                Physics2D.gravity = -Physics2D.gravity;
            }

            bool inc = IncreaseGInput.Started();
            bool dec = DecreaseGInput.Started();
            if (!(inc ^ dec))
                return;

            Vector2 gravityDir = Physics2D.gravity.normalized;
            _worldMover.Velocity += (inc ? 1f : -1f) * ForceDelta * gravityDir;
        }

    }

}
