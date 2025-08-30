using System.Reflection;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GunBehaviour : MonoBehaviour
{
    public GameObject gunOnWall;
    public GameObject gunInHand;
    public GameObject gunText;
    public GameObject newMission;
    public GameObject crosshair;

    private AudioSource sound;
    private bool isTaken = false;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(sound.clip, transform.position);
        gunOnWall.SetActive(false);
        gunInHand.SetActive(true);
        gunText.SetActive(false);
        newMission.SetActive(true);
        isTaken = true;
        crosshair.SetActive(true);
    }

    void OnMouseOver()
    {
        if (!isTaken)
            gunText.gameObject.SetActive(true);

    }
    void OnMouseExit()
    {
        gunText.gameObject.SetActive(false);

    }
}
