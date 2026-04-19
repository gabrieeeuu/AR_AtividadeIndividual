using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [Header("References")]
    public Transform virtualObjectRoot;
    public GameObject initialBase;
    public ShapeDatabase database;

    public Transform currentAnchor;

    public int blocksPlaced = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetAnchor(transform);
    }

    public bool HasAnchor()
    {
        return currentAnchor != null;
    }

    public void SetAnchor(Transform anchor)
    {
        currentAnchor = anchor;

        var shape = database.GetShape(UIManager.Instance.selectedIndex);

        GhostManager.Instance.ShowGhost(shape.prefab, anchor);
    }

    public void SpawnSelectedShape(int index)
    {
        if (currentAnchor == null) return;

        ShapeData shape = database.GetShape(index);
        if (shape == null) return;

        if (blocksPlaced == 0)
        {
            initialBase.SetActive(false);
        }

        GameObject newBlock = Instantiate(shape.prefab,
            currentAnchor.position,
            currentAnchor.rotation,
            virtualObjectRoot);

        newBlock.GetComponentInChildren<Renderer>().material.color = Random.ColorHSV();

        blocksPlaced++;

        GhostManager.Instance.ClearGhost();

        currentAnchor = null;
    }
}