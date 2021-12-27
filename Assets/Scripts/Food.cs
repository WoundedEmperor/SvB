using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public TMPro.TextMeshPro FoodDurabilityText;
    public int FoodDurability;

    public void InitPrefab(int foodDurability)
    {
        FoodDurability = foodDurability;
        FoodDurabilityText.text = FoodDurability.ToString();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
