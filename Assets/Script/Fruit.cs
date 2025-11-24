using UnityEngine;

/// <summary>
/// Abstract base class for all fruits
/// Implements polymorphism for GetNutrition()
/// </summary>
public abstract class Fruit : MonoBehaviour
{
    [SerializeField] protected int nutritionValue;

    /// <summary>
    /// Virtual method that can be overridden by child classes
    /// Returns the nutrition value of the fruit
    /// </summary>
    public virtual int GetNutrition()
    {
        return nutritionValue;
    }

    public int NutritionValue
    {
        get { return nutritionValue; }
        set { nutritionValue = value; }
    }
}
