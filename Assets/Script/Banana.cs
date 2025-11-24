using UnityEngine;

/// <summary>
/// Banana fruit - provides specific nutrition value
/// Demonstrates polymorphism by overriding GetNutrition()
/// </summary>
public class Banana : Fruit
{
    private void Awake()
    {
        nutritionValue = 15; // Banana gives 15 EXP
    }

    public override int GetNutrition()
    {
        Debug.Log($"Banana provides {nutritionValue} nutrition!");
        return nutritionValue;
    }
}
