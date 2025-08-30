using UnityEngine;
using System;
using DialogueEditor;

public class BridgeParts : MonoBehaviour
{
    // ===== Events =====
    public static event Action OnAllPartsFixed;
    public static event Action<int, int> OnProgressChanged;

    // ===== Counters (global) =====
    public static int FixedCount = 0;
    public static int TotalNeeded = 0;
    public static bool AllFixed = false;
    public Transform myItems;
    // ניתן לאלץ מספר חלקים ידני (למשל 52)
    [Header("Override total (optional)")]
    public bool overrideTotal = false;
    public int overrideTotalValue = 52;
    private static bool s_totalOverridden = false;

    // ===== Per-part =====
    private Renderer myRenderer;
    private Color tmpColor;
    private bool Fixed = false;

    public InventoryManagerRiver inventoryManager;
    public GameObject InventorBar;
    public Item item;
    public AudioClip clickSound;
    public GameObject statue;
    public Material fixedMaterial;
    public GameObject missionBox2;
    public GameObject missionBox1;
    public NPCConversation firstConversation;
    public static bool areAllPartsFixed = false;
    public AudioClip soundMission;
    private BoxCollider boxCol;

     private static bool s_bootstrapped = false;

    private void Awake()
    {
        if (!s_bootstrapped)
        {
            FixedCount = 0;
            TotalNeeded = 0;
            AllFixed = false;
            s_totalOverridden = false;
            s_bootstrapped = true;
        }
    }

    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        boxCol = GetComponent<BoxCollider>();

        if (myRenderer != null)
        {
            var c = myRenderer.material.color;
            myRenderer.material.color = new Color(c.r, c.g, c.b, 0.5f);
        }
        if (boxCol != null)
        {
            boxCol.enabled = true;
            boxCol.isTrigger = true;
        }

         if (overrideTotal && !s_totalOverridden)
        {
            TotalNeeded = Mathf.Max(1, overrideTotalValue);
            s_totalOverridden = true;
        }
        else if (TotalNeeded == 0)
        {
            TotalNeeded = FindObjectsOfType<BridgeParts>(true).Length;
        }
    }

    private void OnMouseEnter()
    {
        if (!Fixed && myRenderer != null)
        {
            tmpColor = myRenderer.material.color;
            myRenderer.material.color = new Color(1f, 0f, 0f, 0.6f);
        }
    }

    private void OnMouseExit()
    {
        if (!Fixed && myRenderer != null)
            myRenderer.material.color = tmpColor;
    }

    private void OnMouseDown()
    {
        if (Fixed) return;

        var sound = statue != null ? statue.GetComponent<AudioSource>() : null;

        if (inventoryManager != null && inventoryManager.RemoveSelectedItem(item))
        {
            Fixed = true;

             if (fixedMaterial != null && myRenderer != null)
                myRenderer.material = fixedMaterial;
            else if (myRenderer != null)
                myRenderer.material.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 1f);

             if (boxCol != null) boxCol.isTrigger = false;

            if (sound != null && clickSound != null)
                sound.PlayOneShot(clickSound);

             FixedCount = Mathf.Min(FixedCount + 1, Mathf.Max(1, TotalNeeded));
            OnProgressChanged?.Invoke(FixedCount, TotalNeeded);

            if (!AllFixed && FixedCount == 57)
            {
                AllFixed = true;
                missionBox1.gameObject.SetActive(false);
                missionBox2.gameObject.SetActive(true);
                areAllPartsFixed = true;
                InventorBar.SetActive(false);
                foreach (Transform child in myItems)
                        Destroy(child.gameObject);
                sound.PlayOneShot(soundMission);
            }
        }
        else
        {
            if (myRenderer != null)
                myRenderer.material.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 0.5f);
        }
    }
}
