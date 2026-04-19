using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager Instance;

    public Material ghostMaterial;

    private Transform currentAnchor;

    private GameObject currentGhost;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (currentGhost != null)
        {
            currentGhost.transform.position = currentAnchor.position;
        }
    }

    public void ShowGhost(GameObject prefab, Transform anchor)
    {
        ClearGhost();

        currentAnchor = anchor;
        currentGhost = Instantiate(prefab, currentAnchor.position, currentAnchor.rotation, currentAnchor);

        ApplyGhostMaterial(currentGhost);
    }

    public void ClearGhost()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
            currentAnchor = null;
        }
    }

    void ApplyGhostMaterial(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (var r in renderers)
        {
            foreach (var mat in r.materials)
            {
                mat.CopyPropertiesFromMaterial(ghostMaterial);
            }
        }
    }
}