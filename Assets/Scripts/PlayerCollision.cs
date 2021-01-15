using UnityEngine;

namespace Com.Femeuc.Golf3DOnline
{
    public class PlayerCollision : MonoBehaviour
    {
        public static bool isTouchingTheGround;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Ground")
                isTouchingTheGround = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.collider.tag == "Ground")
                isTouchingTheGround = false;
        }
    }
}