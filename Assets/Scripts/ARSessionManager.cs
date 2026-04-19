using UnityEngine;

public class ARSessionManager : MonoBehaviour
{
    public static ARSessionManager Instance;

    public bool HasPlacedInitialObject { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SetInitialPlaced()
    {
        HasPlacedInitialObject = true;
    }
}