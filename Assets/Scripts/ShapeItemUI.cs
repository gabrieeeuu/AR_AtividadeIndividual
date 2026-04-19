using UnityEngine;
using UnityEngine.UI;

public class ShapeItemUI : MonoBehaviour
{
    public Image icon;
    public GameObject highlight;

    private int index;

    public void Setup(int i, Sprite sprite)
    {
        index = i;
        icon.sprite = sprite;
        SetSelected(false);
    }

    public void OnClick()
    {
        UIManager.Instance.SelectShape(index);
    }

    public void SetSelected(bool selected)
    {
        highlight.SetActive(selected);
    }
}