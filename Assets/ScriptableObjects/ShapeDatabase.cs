using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "shapeDatabase", menuName = "AR/shapeDatabase")]
public class ShapeDatabase : ScriptableObject
{
    public List<ShapeData> shapes;

    public ShapeData GetShape(int index)
    {
        if (index < 0 || index >= shapes.Count) return null;
        return shapes[index];
    }
}