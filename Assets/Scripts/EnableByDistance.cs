using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableByDistance : MonoBehaviour
{
    public Transform player;
    public float enableDistance = 35f;    // כמה לראות
    public float checkInterval = 0.25f;   // בדיקה כל רבע שנייה
    private readonly List<Renderer> _renders = new List<Renderer>();
    private readonly List<Collider> _colliders = new List<Collider>();

    void Start()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player")?.transform;
        GetComponentsInChildren(true, _renders);
        GetComponentsInChildren(true, _colliders);
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        var wait = new WaitForSeconds(checkInterval);
        while (true)
        {
            if (player)
            {
                Vector3 p = player.position;
                float d2 = enableDistance * enableDistance;

                foreach (var r in _renders)
                    if (r) r.enabled = (r.transform.position - p).sqrMagnitude <= d2;

                foreach (var c in _colliders)
                    if (c) c.enabled = (c.transform.position - p).sqrMagnitude <= d2;
            }
            yield return wait;
        }
    }

    void GetComponentsInChildren<T>(bool includeInactive, List<T> list) where T : Component
    {
        list.Clear();
        GetComponentsInChildren(includeInactive, list);
    }
}
