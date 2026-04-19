using UnityEngine;

public class ShapeSelectorUI : MonoBehaviour
{
    public Transform content;
    public GameObject itemPrefab;

    public ShapeDatabase database;

    private ShapeItemUI[] items;

    private void Start()
    {
        Populate();

        UpdateSelection(0);
    }

    void Populate()
    {
        items = new ShapeItemUI[database.shapes.Count];

        for (int i = 0; i < database.shapes.Count; i++)
        {
            var shape = database.shapes[i];

            GameObject obj = Instantiate(itemPrefab, content);

            ShapeItemUI item = obj.GetComponent<ShapeItemUI>();

            item.Setup(i, shape.icon);

            items[i] = item;
        }
    }

    public void UpdateSelection(int selectedIndex)
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetSelected(i == selectedIndex);
        }
    }

    Sprite GetPlaceholderSprite()
    {
        return null; // temporário
    }
}