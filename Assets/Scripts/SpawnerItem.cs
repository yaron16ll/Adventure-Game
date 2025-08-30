using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerItem : MonoBehaviour
{
    public int numItems = 300;
    public GameObject[] GameObjectToSpawn;
    public Item[] items;
    public InventoryManager invManager;
    public Terrain t;
    public Transform MyItems;
    public BoxCollider[] wallColliders;
    public float safeDistanceFromWall = 10f;

    void Start()
    {
        if (t == null) t = Terrain.activeTerrain;

        Bounds innerBounds = CalculateInnerBounds();
        List<Vector3> usedPositions = new List<Vector3>();

        int spawned = 0;
        int maxAttempts = numItems * 10;

        for (int attempts = 0; attempts < maxAttempts && spawned < numItems; attempts++)
        {
            float x = Random.Range(innerBounds.min.x + safeDistanceFromWall, innerBounds.max.x - safeDistanceFromWall);
            float z = Random.Range(innerBounds.min.z + safeDistanceFromWall, innerBounds.max.z - safeDistanceFromWall);
            float y = t.SampleHeight(new Vector3(x, 0, z)) + t.GetPosition().y;

            Vector3 spawnPos = new Vector3(x, y, z);

            if (usedPositions.Any(pos => Vector3.Distance(pos, spawnPos) < 2f))
                continue;

            usedPositions.Add(spawnPos);

            int index = Random.Range(0, items.Length);
            GameObject prefab = GameObjectToSpawn[index];
            GameObject g = Instantiate(prefab, spawnPos, Quaternion.identity, MyItems);

            string lowerName = prefab.name.ToLower();
            Vector3 itemScale = Vector3.one;

            if (lowerName.Contains("garlic"))
                itemScale = new Vector3(4f, 4f, 4f);
            else if (lowerName.Contains("grass"))
                itemScale = new Vector3(2f, 2f, 2f);

            g.transform.localScale = itemScale;

            float manualYOffset = 0f;
            if (lowerName.Contains("mushroom") || lowerName.Contains("cap"))
                manualYOffset = -0.2f;
            else if (lowerName.Contains("leaf") || lowerName.Contains("grass") || lowerName.Contains("plant"))
                manualYOffset = -0.05f;

            Renderer rend = g.GetComponentInChildren<Renderer>();
            if (rend != null)
            {
                float boundsHeight = rend.bounds.size.y;
                g.transform.position = new Vector3(
                    spawnPos.x,
                    spawnPos.y + manualYOffset + boundsHeight / 2f,
                    spawnPos.z
                );
            }

            SphereCollider sc = g.AddComponent<SphereCollider>();
            sc.isTrigger = true;
            sc.radius = 1.5f;

            IngredientsBehaviour pb = g.GetComponent<IngredientsBehaviour>();
            if (pb == null)
                pb = g.AddComponent<IngredientsBehaviour>();

            pb.Item = items[index];
            pb.inventoryManager = invManager;
            pb.folderAudioSource = MyItems.GetComponent<AudioSource>();

            spawned++;
        }
    }

    Bounds CalculateInnerBounds()
    {
        float minX = wallColliders.Max(c => c.bounds.min.x);
        float maxX = wallColliders.Min(c => c.bounds.max.x);
        float minZ = wallColliders.Max(c => c.bounds.min.z);
        float maxZ = wallColliders.Min(c => c.bounds.max.z);
        float minY = t.GetPosition().y;

        Vector3 center = new Vector3((minX + maxX) / 2f, minY, (minZ + maxZ) / 2f);
        Vector3 size = new Vector3(maxX - minX, 1f, maxZ - minZ);

        return new Bounds(center, size);
    }
}
