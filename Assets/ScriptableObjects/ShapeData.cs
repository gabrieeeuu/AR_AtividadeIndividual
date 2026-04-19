using UnityEngine;

[CreateAssetMenu(fileName = "shapeData", menuName = "AR/shapeData")]
public class ShapeData : ScriptableObject
{
    public string id;
    public string name;
    public GameObject prefab;
    public Sprite icon;
}