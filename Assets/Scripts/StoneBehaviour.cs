using UnityEngine;

public class StoneBehaviour : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isPlayed = true;
    public GameObject mission1;
    public GameObject mission2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayed)
        {
            if (other.CompareTag("Player"))
            {
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                    mission1.gameObject.SetActive(false);
                    isPlayed = false;
                    mission2.gameObject.SetActive(true);
                }
            }
        }
    }
}
