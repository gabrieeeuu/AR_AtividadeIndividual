using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject settingsPanel;

    public int selectedIndex { get; private set; }

    public ShapeSelectorUI selectorUI;

    public TextMeshProUGUI log;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectShape(int index)
    {
        selectedIndex = index;

        selectorUI.UpdateSelection(index);

        var shape = BuildManager.Instance.database.GetShape(index);

        if (BuildManager.Instance.HasAnchor())
        {
            GhostManager.Instance.ShowGhost(shape.prefab,
                BuildManager.Instance.currentAnchor);
        }

        Log($"Shape {shape.id} selecionado");
    }

    public void OnClickSpawn()
    {
        BuildManager.Instance.SpawnSelectedShape(selectedIndex);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void Log(string message) 
    {
        Debug.Log(message);
        log.text = message;
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}