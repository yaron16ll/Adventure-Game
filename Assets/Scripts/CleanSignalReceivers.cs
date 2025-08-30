using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public static class CleanSignalReceivers
{
    [MenuItem("Tools/Timeline/Remove All SignalReceivers (TEMP FIX)")]
    static void RemoveAllReceivers()
    {
        var receivers = Object.FindObjectsOfType<SignalReceiver>(true);
        int n = 0;
        foreach (var r in receivers)
        {
            Undo.RecordObject(r.gameObject, "Remove SignalReceiver");
            Object.DestroyImmediate(r, true);
            n++;
        }
        Debug.Log($"Removed {n} SignalReceiver components. Re-add them manually as needed.");
    }
}
