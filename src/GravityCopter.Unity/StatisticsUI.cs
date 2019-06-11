using UnityEngine;
using UnityEngine.UI;

namespace GravityCopter.Unity {

    public class StatisticsUI : Updatable {

        public RectTransform Root;

        [Header("UI Objects")]
        public Text TxtWorldVelocity;
        public Text TxtWorldSpeed;

        [Space]
        public Text TxtGravity;
        public Text TxtGravityMagnitude;

        [Space]
        public Text TxtNumColumns;
        public Text TxtWorldColumnPoolCount;

        private WorldMover _worldMover;
        private WorldBuilder _worldBuilder;
        private GameObjectPool _worldColumnPool;

        protected override void BetterAwake() {
            base.BetterAwake();

            RegisterUpdatesAutomatically = true;
            BetterUpdate = doUpdate;
        }

        public void Inject(WorldMover worldMover, WorldBuilder worldBuilder, GameObjectPool worldColumnPool) {
            _worldMover = worldMover;
            _worldBuilder = worldBuilder;
            _worldColumnPool = worldColumnPool;
        }

        private void doUpdate(float deltaTime) {
            if (TxtWorldVelocity != null)
                TxtWorldVelocity.text = $"World velocity: {_worldMover.Velocity}";
            if (TxtWorldSpeed != null)
                TxtWorldSpeed.text = $"World speed: {_worldMover.Velocity.magnitude}";

            if (TxtGravity != null)
                TxtGravity.text = $"Gravity: {Physics2D.gravity}";
            if (TxtGravityMagnitude != null)
                TxtGravityMagnitude.text = $"Gravity magnitude: {Physics2D.gravity.magnitude}";

            if (TxtNumColumns  != null)
                TxtNumColumns.text = $"Num columns: {_worldBuilder.NumColumns}";
            if (TxtWorldColumnPoolCount != null)
                TxtWorldColumnPoolCount.text = $"World column pool count: {_worldColumnPool.Count}";
        }
    }

}
