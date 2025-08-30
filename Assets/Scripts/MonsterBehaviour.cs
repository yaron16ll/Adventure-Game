using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class MonsterBehaviour : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform player;     
    [SerializeField] private GameObject lives;                      
    [SerializeField] private AudioClip clip;

    [Header("Tuning")]
    [SerializeField] private float rotateSpeed = 6f;   

    private Animator animator;
    private AudioSource audioSrc;
    public bool isPlayerIn;

    private void Reset()
    {
         var col = GetComponent<SphereCollider>();
        if (col) col.isTrigger = true;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
         if (audioSrc) audioSrc.spatialBlend = 0f;

         if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) player = p.transform;
        }

        if (lives) lives.SetActive(false);
        animator.SetInteger("State", 0);
    }

    private void Update()
    {
        if (!isPlayerIn || player == null) return;

         Vector3 dir = player.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerIn = true;
        animator.SetInteger("State", 1);

        if (lives) lives.SetActive(true);

        if (clip && audioSrc)
        {
            if (audioSrc.clip != clip) audioSrc.clip = clip;
            if (!audioSrc.isPlaying) audioSrc.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        isPlayerIn = false;
        animator.SetInteger("State", 0);

        if (lives) lives.SetActive(false);

        if (audioSrc && audioSrc.isPlaying && audioSrc.clip == clip)
            audioSrc.Stop();
    }
}
