using UnityEngine;

/// <summary>
/// Berry fruit - provides specific nutrition value
/// Demonstrates polymorphism by overriding GetNutrition()
/// </summary>
public class Berry : Fruit
{
    private void Awake()
    {
        nutritionValue = 20; // Berry gives 20 EXP
    }

    public override int GetNutrition()
    {
        Debug.Log($"Berry provides {nutritionValue} nutrition!");
        return nutritionValue;
    }
}
