using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GravityCopter.Unity {

    public class ScoreManager : MonoBehaviour {

        private WorldBuilder _worldBuilder;

        public int PointsPerWall = 1;
        public Text TxtScore;

        private void Awake() {
            DependencyInjector.ResolveDependenciesOf(this);

            Assert.IsNotNull(TxtScore, this.GetAssociationAssertion(nameof(TxtScore)));
        }

        public void Inject(WorldBuilder worldBuilder) {
            _worldBuilder = worldBuilder;
        }

        public void UpdateScore() {
            int score = PointsPerWall * (_worldBuilder.NumWallsPassed - _worldBuilder.NumColumns);
            TxtScore.text = $"Distance: {score}";
        }

    }

}
