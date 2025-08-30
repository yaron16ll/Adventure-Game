using DialogueEditor;
using UnityEngine;

public class VillagerBehaviour : MonoBehaviour
{
    public NPCConversation firstConversation;
    public NPCConversation finalConversation;

    public GameObject playerObject;
    public GameObject InventorBar;
    public GameObject missionBox;
    public GameObject missionBox1;
    public GameObject missionBox2;

    private bool isFirstConversation = false;
    private Animator animator;
    private PlayerBehaviour playerBehaviour;

    private bool conversationStarted = false;
    private bool firstDialogueDone = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (playerObject != null)
            playerBehaviour = playerObject.GetComponent<PlayerBehaviour>();

        ConversationManager.OnConversationEnded += OnConversationEnded;
    }

    private void OnDestroy()
    {
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || conversationStarted) return;

        conversationStarted = true;

        if (!firstDialogueDone)
        {
            firstDialogueDone = true;
            isFirstConversation = true;
            StartDialogue(firstConversation);
            missionBox2.SetActive(false);

            return;
        }

        if (BridgeParts.areAllPartsFixed)
        {
            StartDialogue(finalConversation);
            InventorBar.SetActive(false);
            isFirstConversation  = false;
            missionBox.SetActive(false);
            missionBox1.SetActive(false);

        }
        else
        {
            Debug.Log("You still don't have all 80 parts fixed.");
            conversationStarted = false;
        }
    }

    private void StartDialogue(NPCConversation convo)
    {
        if (convo == null) { conversationStarted = false; return; }

        if (animator != null)
            animator.SetInteger("State", 1);

        if (playerBehaviour != null)
            playerBehaviour.enabled = false;

        ConversationManager.Instance.StartConversation(convo);
    }

    private void OnConversationEnded()
    {
        if (isFirstConversation)
        {
            if (playerBehaviour != null)
                playerBehaviour.enabled = true;

            if (InventorBar != null)
                InventorBar.SetActive(true);

            if (animator != null)
                animator.SetInteger("State", 0);

             conversationStarted = false;

            if (missionBox != null)
                missionBox.SetActive(true);
        }
        else
        {
            playerBehaviour.enabled = true;
        }
    }
}
