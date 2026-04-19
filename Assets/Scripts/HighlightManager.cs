using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    public static HighlightManager Instance;

    private SnapPoint current;

    private void Awake()
    {
        Instance = this;
    }

    public void SetHighlight(SnapPoint snap)
    {
        Clear();

        current = snap;
        current.SetHighlight(true);
    }

    public void Clear()
    {
        if (current != null)
        {
            current.SetHighlight(false);
            current = null;
        }
    }
}