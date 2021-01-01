using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    private Rigidbody player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    public void Throw()
    {
        player.AddForce(new Vector3(0, 2, 2), ForceMode.VelocityChange);
    }
}
