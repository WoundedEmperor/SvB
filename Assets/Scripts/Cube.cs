using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public TMPro.TextMeshPro CubeDurabilityText;
    public int CubeDurability;

    public void InitPrefab(int cubeDurability)
    {
        CubeDurability = cubeDurability;
        CubeDurabilityText.text = CubeDurability.ToString();
    }

}
