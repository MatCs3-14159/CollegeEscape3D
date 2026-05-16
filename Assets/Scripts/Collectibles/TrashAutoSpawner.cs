using UnityEngine;

public class TrashAutoSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Material trashMaterial;
    public Vector3 trashScale = new Vector3(0.5f, 0.18f, 0.5f);
    public int coinsOnPickup = 1;

    [Header("Extra Trash Positions")]
    public Vector3[] trashPositions =
    {
        new Vector3(-5f, 0.2f, 45f),
        new Vector3(22f, 0.2f, 44f),
        new Vector3(58f, 0.2f, 43f),
        new Vector3(92f, 0.2f, 37f),
        new Vector3(138f, 0.2f, 28f),
        new Vector3(175f, 0.2f, 10f),
        new Vector3(160f, 0.2f, -18f),
        new Vector3(126f, 0.2f, -48f),
        new Vector3(92f, 0.2f, -73f),
        new Vector3(52f, 0.2f, -88f),
        new Vector3(10f, 0.2f, -91f),
        new Vector3(-25f, 0.2f, -76f),
        new Vector3(-63f, 0.2f, -59f),
        new Vector3(-72f, 0.2f, -22f),
        new Vector3(-66f, 0.2f, 18f),
        new Vector3(-38f, 0.2f, 38f),
        new Vector3(18f, 0.2f, -18f),
        new Vector3(36f, 0.2f, -18f),
        new Vector3(64f, 0.2f, -18f),
        new Vector3(82f, 0.2f, 5f),
        new Vector3(24f, 0.2f, -52f),
        new Vector3(42f, 0.2f, -52f),
        new Vector3(68f, 0.2f, -52f),
        new Vector3(118f, 0.2f, 52f),
        new Vector3(148f, 0.2f, 52f),
        new Vector3(178f, 0.2f, 52f),
        new Vector3(-48f, 0.2f, -38f),
        new Vector3(-60f, 0.2f, -8f),
        new Vector3(6f, 0.2f, 64f),
        new Vector3(72f, 0.2f, 64f)
    };

    private void Start()
    {
        SpawnTrashItems();
    }

    private void SpawnTrashItems()
    {
        for (int i = 0; i < trashPositions.Length; i++)
        {
            GameObject trash = GameObject.CreatePrimitive(PrimitiveType.Cube);
            trash.name = "Trash_Auto_" + (i + 11).ToString("00");
            trash.transform.SetParent(transform);
            trash.transform.position = trashPositions[i];
            trash.transform.localScale = trashScale;
            trash.transform.rotation = Quaternion.Euler(0f, i * 23f, 0f);

            if (trashMaterial != null)
            {
                Renderer trashRenderer = trash.GetComponent<Renderer>();
                trashRenderer.material = trashMaterial;
            }

            TrashItem trashItem = trash.AddComponent<TrashItem>();
            trashItem.coinsOnPickup = coinsOnPickup;
        }
    }
}
