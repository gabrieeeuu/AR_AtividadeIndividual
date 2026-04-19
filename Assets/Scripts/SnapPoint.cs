using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public bool isOccupied;

    //public Renderer rend;

    private void Awake()
    {
        //rend = GetComponent<Renderer>();
        SetHighlight(false);
    }

    public void SetHighlight(bool active)
    {
        //if (rend == null) return;

        //rend.enabled = active;
    }

    public void Occupy()
    {
        isOccupied = true;
        SetHighlight(false);
    }
}