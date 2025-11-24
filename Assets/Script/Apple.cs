using UnityEngine;

///Apple fruit
///polymorphism by overriding GetNutrition()
public class Apple : Fruit
{
    private void Awake()
    {
        nutritionValue = 10; // Apple gives 10 EXP
    }

    public override int GetNutrition()
    {
        Debug.Log($"Apple provides {nutritionValue} nutrition!");
        return nutritionValue;
    }
}
