using UnityEngine;
using Vuforia;

public class ImageTargetHandler : MonoBehaviour
{
    public GameObject virtualObjectRoot;

    private ObserverBehaviour observer;

    private void Awake()
    {
        observer = GetComponent<ObserverBehaviour>();
        observer.OnTargetStatusChanged += OnStatusChanged;
    }

    void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        bool visible = status.Status == Status.TRACKED ||
                       status.Status == Status.EXTENDED_TRACKED;

        virtualObjectRoot.SetActive(visible);
    }
}