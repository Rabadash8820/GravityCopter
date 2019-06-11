using UnityEngine;
using UnityEngine.Assertions;

namespace GravityCopter.Unity {

    public class WorldMover : Updatable {

        public Vector2 Velocity;
        public Transform WorldRoot;

        protected override void BetterAwake() {
            base.BetterAwake();

            Assert.IsNotNull(WorldRoot, this.GetAssociationAssertion(nameof(WorldRoot)));

            RegisterUpdatesAutomatically = true;
            BetterUpdate = translate;
        }

        private void translate(float deltaTime) {
            WorldRoot.position = (Vector2)WorldRoot.position + Velocity * deltaTime;
        }

    }

}
