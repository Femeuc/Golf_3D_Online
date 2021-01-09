using Photon.Pun;
using UnityEngine;

namespace Com.Femeuc.Golf3DOnline
{
    public class CameraFollow : MonoBehaviourPun
    {
        #region Class Properties

        public Transform targetOfCamera;
        private Vector3 previousMousePosition;
        public float cameraHeightRelativeToTheTarget = 1.5f;
        [Tooltip("The distance must be a negative number!")]
        public float cameraDistanceOfTheTarget = -4;
        private Camera cam;

        public static GameObject playerObject;

        #endregion

        #region MonoBehaviour Callbacks

        // Start is called before the first frame update
        void Start()
        {
            if (!photonView.IsMine) return;
            cam = Camera.main;
            Debug.Log("this.gameObject is: " + this.gameObject.ToString());
            PhotonNetwork.LocalPlayer.NickName.ToString();
            playerObject = this.gameObject;
            ThrowBall.InitializePlayerRigidBody();
        }

        // LateUpdate is called after all Update functions have been called. 
        //This is useful to order script execution. For example a follow camera should always 
        //be implemented in LateUpdate because it tracks objects that might have moved inside Update.
        void LateUpdate()
        {
            if (!photonView.IsMine) return;
            cam.transform.position = targetOfCamera.position;
            if (Input.GetMouseButtonDown(0))
            {
                // ScreenToViewportPoint(Vector3 position); Transforms position from screen space into viewport space.
                // Screenspace is defined in pixels. The bottom-left of the screen is (0,0); the right-top is (pixelWidth,pixelHeight).
                // Viewport space is relative to the camera. The bottom-left of the camera is (0, 0); the top-right is (1, 1). 
                previousMousePosition = cam.ScreenToViewportPoint(Input.mousePosition); // this line is important for it to feel better.
            }
            if (Input.GetMouseButton(0)) // GetMouseButton(int button) Returns whether the given mouse button is held down.
            {
                Vector3 rotationDirection = previousMousePosition - cam.ScreenToViewportPoint(Input.mousePosition);

                cam.transform.Rotate(new Vector3(1, 0, 0), rotationDirection.y * 180);
                cam.transform.Rotate(new Vector3(0, 1, 0), -rotationDirection.x * 180, Space.World);

                previousMousePosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
            // Translate(Vector3 translation); Moves the transform in the direction and distance of translation.
            cam.transform.Translate(0, cameraHeightRelativeToTheTarget, cameraDistanceOfTheTarget);
        }
        #endregion
    }
}