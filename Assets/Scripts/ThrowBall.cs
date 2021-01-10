using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

namespace Com.Femeuc.Golf3DOnline
{
    public class ThrowBall : MonoBehaviourPun
    {
        private static Rigidbody playerRigidBody;
        private Camera cam;
        public static Scrollbar forwardForceIntensity, upwardForceIntensity;
        

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
            causeFriction(); // Method responsible for causing the friction between ball and ground.
        }                    // I prefer making my own function than using Rigidbody drag, cause it doesn't look good to me.

        public void Throw()
        {
            Debug.Log("Throwing " + PhotonNetwork.LocalPlayer.NickName);
            int forwardThrowValue = int.Parse((forwardForceIntensity.value * 10).ToString("0"));
            int upwardThrowValue = int.Parse((upwardForceIntensity.value * 10).ToString("0"));
            // Quarternion.Euler is necessary so the ball is thorwn only forward, not upward, even if the camera is pointing upward.
            playerRigidBody.velocity = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * Vector3.forward * forwardThrowValue * forwardThrowValue / 3 + Vector3.up * upwardThrowValue * upwardThrowValue / 3;
            Vector3 v3 = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0) * Vector3.forward;

        }

        public static void InitializePlayerRigidBody()
        {
            playerRigidBody = CameraFollow.playerObject.GetComponent<Rigidbody>();
        }

        // This function causes friction while the ball is rolling on the ground.
        private void causeFriction()
        {
            if(playerRigidBody == null)
            {
                Debug.Log("Rigidbody null: leaving");
                return;
            }
            float baseFramerate = Time.deltaTime * 60;
            float magnitude = playerRigidBody.velocity.magnitude;
            if (magnitude > 0 && playerRigidBody.velocity.y == 0) // only if ball is touching ground
            {
                // Notice the amount of friction is dependant on the velocity.
                if (magnitude > 30)
                    playerRigidBody.AddForce(playerRigidBody.velocity * -0.025f * baseFramerate);
                else if (magnitude > 10)
                    playerRigidBody.AddForce(playerRigidBody.velocity * -0.05f * baseFramerate);
                else if(magnitude > 1)
                    playerRigidBody.AddForce(playerRigidBody.velocity * -0.15f * baseFramerate);
                else if (magnitude > 0.5)
                    playerRigidBody.AddForce(playerRigidBody.velocity * -0.5f * baseFramerate);
                else
                {
                    playerRigidBody.velocity = Vector3.zero;
                    playerRigidBody.Sleep(); // This is necessary to make the ball stop altogether
                }
                Debug.Log(playerRigidBody.velocity.magnitude);
            }
        }
    }
}