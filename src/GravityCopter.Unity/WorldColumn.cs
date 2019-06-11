using UnityEngine;

namespace GravityCopter.Unity {
    public class WorldColumn : MonoBehaviour {
        public BoxCollider2D SensorCollider;
        public SpriteRenderer TopWallRenderer;
        public SpriteRenderer BottomWallRenderer;
        public BoxCollider2D TopWallCollider;
        public BoxCollider2D BottomWallCollider;

        public Transform[] Debris;
    }

}
