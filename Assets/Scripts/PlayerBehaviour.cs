using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject playerCamera; // must be connected to a camera in Unity
    CharacterController controller;
    float speed = 10f;
    public GameObject playerPosition;
    float angularSpeed = 100.0f;
    AudioSource sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sound = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame

    void Update()
    {
        float dx, dz;
        // Input.GetAxis("Mouse X") is the deviation in pixels from the center to x direction
        float RotationAboutY = angularSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        float RotationAboutX = -angularSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

        playerCamera.transform.Rotate(new Vector3(RotationAboutX, 0, 0));

        transform.Rotate(new Vector3(0, RotationAboutY, 0));

        dx = speed * Input.GetAxis("Horizontal") * Time.deltaTime; // can be -1, 0 or 1
        dz = speed * Input.GetAxis("Vertical") * Time.deltaTime; // can be -1, 0 or 1
                                                                 // adaptive motion
        Vector3 motion = new Vector3(dx, -1, dz);
        // trsforms motion to local coordinates
        motion = transform.TransformDirection(motion);
        controller.Move(motion);
        if (dx != 0 || dz != 0)
        {
            if (!sound.isPlaying)
            {
                sound.Play();
            }
        }

    }
}
