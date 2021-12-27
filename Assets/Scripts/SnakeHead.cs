using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SnakeHead : MonoBehaviour
{

    public LineManager lineManager;
    public TailController tailController;
    public Movement SnakeStats;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            lineManager.CollidingCubes.Add(other.GetComponent<Cube>());
            lineManager.gameState = LineManager.GameState.collision;
        }
        if (other.tag == "Food")
        {
            for (int i = 0; i < other.GetComponent<Food>().FoodDurability; i++)
            {
                tailController.AddCircle(true);
                
            }
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Block")
        {
            lineManager.CollidingCubes.Remove(other.GetComponent<Cube>());
        }
    }
}
