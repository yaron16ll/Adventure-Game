using UnityEngine;
using UnityEngine.UI;

public class BattleSound : MonoBehaviour
{
    AudioSource audio;
    public GameObject mission;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audio.Play();
            mission.SetActive(false);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audio.Stop();
            mission.SetActive(true);
        }
    }
}
