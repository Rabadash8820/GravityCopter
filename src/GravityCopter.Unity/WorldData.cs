using UnityEngine;

namespace GravityCopter.Unity {

    [CreateAssetMenu(menuName = "GravityCopter.Unity/" + nameof(WorldData), fileName = "world-data")]
    public class WorldData : ScriptableObject {
        [Space]
        public float ColumnHeight = 20f;
        public float ColumnWidth = 1f;
        [Space]
        public int NumInitialLowWalls = 20;
        public int NumColumnsBeforeDebris = 35;
        [Space]
        public float MinWallHeight = 1f;
        public float MinGapHeight = 3f;
        public float MaxGapHeight = 18f;
        public float MaxHeightDeltaBetweenColumns = 5f;
        [Space]
        public float MinDebrisGravityScale = 0.5f;
        public float MaxDebrisGravityScale = 2f;
    }

}
