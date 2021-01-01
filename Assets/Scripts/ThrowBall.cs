using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    #region Properties
    private Rigidbody player;
    private Camera cam;
    #endregion

    #region MonoBehaviour CallBacks
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void Update()
    {
        // Stops the ball when it gets slower than a certain velocity, this way the player doesn't have to 
        // wait until the ball stops.
        if(player.velocity.magnitude < .3)
        {
            player.velocity = Vector3.zero;
        }
    }
    #endregion

    public void Throw()
    {
        // transform.forward = Returns a normalized vector representing the blue axis of the transform in world space.
        // This way the force will be added to the z direction of the camera, which is forward
        player.AddForce(cam.transform.forward * 500 + new Vector3(0, 1000, 0), ForceMode.Force);
    }
}
