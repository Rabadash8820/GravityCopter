using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GravityCopter.Unity {

    public class WorldBuilder : MonoBehaviour {

        private GameObjectPool _worldColPool;

        private IList<WorldColumn> _cols;
        private float _prevTopHeight;

        public int NumColumns = 30;
        public WorldData Data;
        public Transform ColumnsRoot;

        public int NumWallsPassed { get; private set; } = -1;

        public void Inject(GameObjectPool worldColumnPool, RandomNumberGenerator rand) {
            _worldColPool = worldColumnPool;
        }

        private void Awake() {
            DependencyInjector.ResolveDependenciesOf(this);

            Assert.IsNotNull(Data, this.GetAssociationAssertion(nameof(Data)));
            Assert.IsNotNull(ColumnsRoot, this.GetAssociationAssertion(nameof(ColumnsRoot)));

            _prevTopHeight = Data.ColumnHeight / 2f;

            _cols = new List<WorldColumn>(NumColumns);
            for (int c = 0; c < NumColumns; ++c) {
                WorldColumn column = requestColumn();
                randomizeColumn(column);
            }
        }

        public void RecycleColumn(WorldColumn column) {
            var colPos = new Vector2((NumColumns / 2) * Data.ColumnWidth, 0f);
            column.transform.position = colPos;

            randomizeColumn(column);
        }

        private WorldColumn requestColumn() {
            var colPos = new Vector2((_cols.Count - NumColumns / 2) * Data.ColumnWidth, 0f);
            ObjectPoolMember colPoolMember = _worldColPool.RequestAt(colPos, Quaternion.identity);
            WorldColumn col = colPoolMember.GetComponent<WorldColumn>();

            _cols.Add(col);
            return col;
        }
        private void randomizeColumn(WorldColumn column) {
            ++NumWallsPassed;
            if (NumWallsPassed <= Data.NumInitialLowWalls)
                return;

            float maxWallHeight = Data.ColumnHeight - Data.MinGapHeight - Data.MinWallHeight;
            float unclampedTopHeight = _prevTopHeight + Random.Range(-1f, 1f) * Data.MaxHeightDeltaBetweenColumns;
            float topHeight = Mathf.Clamp(unclampedTopHeight, Data.MinWallHeight, maxWallHeight);
            float maxGapHeight = Mathf.Min(Data.MaxGapHeight, Data.ColumnHeight - topHeight - Data.MinWallHeight);
            float heightBw = Random.Range(Data.MinGapHeight, maxGapHeight);

            // Randomize top wall
            Vector2 topSize = column.TopWallRenderer.size;
            topSize.y = topHeight;
            column.TopWallRenderer.size = topSize;
            column.TopWallCollider.size = topSize;
            Vector2 topCollOffset = column.TopWallCollider.offset;
            topCollOffset.y = -topSize.y / 2f;
            column.TopWallCollider.offset = topCollOffset;

            // Randomize bottom wall
            Vector2 bottomSize = column.BottomWallRenderer.size;
            bottomSize.y = Data.ColumnHeight - topHeight - heightBw;
            column.BottomWallRenderer.size = bottomSize;
            column.BottomWallCollider.size = bottomSize;
            Vector2 bottomCollOffset = column.BottomWallCollider.offset;
            bottomCollOffset.y = bottomSize.y / 2f;
            column.BottomWallCollider.offset = bottomCollOffset;

            // Adjust debris
            if (NumWallsPassed > Data.NumColumnsBeforeDebris) {
                int debrisIndex = Random.Range(0, column.Debris.Length);
                Transform debrisTrans = column.Debris[debrisIndex];
                debrisTrans.gameObject.SetActive(true);
                float debrisX = debrisTrans.position.x - Data.ColumnWidth / 2f;
                float debrisY = ColumnsRoot.transform.position.y - topHeight - heightBw / 2f;
                debrisTrans.position = new Vector2(debrisX, debrisY);
                Rigidbody2D debrisRb = debrisTrans.GetComponentInChildren<Rigidbody2D>();
                debrisRb.gravityScale = Random.Range(Data.MinDebrisGravityScale, Data.MaxDebrisGravityScale);
            }

            _prevTopHeight = topHeight;
        }

    }

}
