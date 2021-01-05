using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.Femeuc.Golf3DOnline
{
    public class ThrowBall : MonoBehaviourPun
    {
        private static Rigidbody playerRigidBody;
        private Camera cam;
        bool isBeingThrown;

        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            stopWhenGoingTooSlowAfterBeingThrown();
        }

        public void Throw()
        {
            Debug.Log("Throwing " + PhotonNetwork.LocalPlayer.NickName);
            playerRigidBody.velocity = cam.transform.forward * 15 + Vector3.up * 5;
            isBeingThrown = true;
        }

        public static void InitializePlayerRigidBody()
        {
            playerRigidBody = CameraFollow.playerObject.GetComponent<Rigidbody>();
        }

        // This method stops the ball if it going too slow,
        // this way you don't have to wait until the ball stops
        private void stopWhenGoingTooSlowAfterBeingThrown()
        {
            if (!isBeingThrown) return;
            if (!(playerRigidBody.velocity.magnitude < 1)) return;
            Debug.Log("Stoping the ball");
            playerRigidBody.velocity = Vector3.zero;
            playerRigidBody.Sleep(); // This is necessary to make the ball stop altogether
            isBeingThrown = false;
        }
    }
}