using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

namespace Com.Femeuc.Golf3DOnline
{
    public class ThrowBall : MonoBehaviourPun
    {
        private static Rigidbody playerRigidBody;
        private Camera cam;
        public static Scrollbar forwardForceIntensity, upwardForceIntensity;
        private float previousBallVelocity;

        // Start is called before the first frame update
        void Start()    
        {
            cam = Camera.main;
            forwardForceIntensity = GameObject.Find("Forward Force Intensity").GetComponent<Scrollbar>();
            upwardForceIntensity = GameObject.Find("Upward Force Intensity").GetComponent<Scrollbar>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
           manageFriction(); 
        }                    

        public void Throw()
        {
            Debug.Log("Throwing " + PhotonNetwork.LocalPlayer.NickName);
            int forwardThrowValue = int.Parse((forwardForceIntensity.value * 10).ToString("0"));
            int upwardThrowValue = int.Parse((upwardForceIntensity.value * 10).ToString("0"));
            // Quarternion.Euler is necessary so the ball is thorwn only forward, not upward, even if the camera is pointing upward.
            playerRigidBody.velocity = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * Vector3.forward * forwardThrowValue * forwardThrowValue / 3 + Vector3.up * upwardThrowValue * upwardThrowValue / 3;
        }

        public static void InitializePlayerRigidBody()
        {
            playerRigidBody = CameraFollow.playerObject.GetComponent<Rigidbody>();
        }

        private void manageFriction()
        {
            if (playerRigidBody == null) // If the playerRigidBody hasn't been instantiated yet
                return;

            float currentVelocity = playerRigidBody.velocity.magnitude;

            if (PlayerCollision.isTouchingTheGround)
            {
                playerRigidBody.drag = 1 / playerRigidBody.velocity.magnitude; // As ball slows down, drag increases up to infinity
            }
            else
                playerRigidBody.drag = 0.01f;
           
            Debug.Log("Magnitude: " + playerRigidBody.velocity.magnitude);

            previousBallVelocity = currentVelocity;

        }
    }
}