using UnityEngine;

/// <summary>
/// Chicken animal - demonstrates polymorphism
/// Overrides Feed() to provide chicken-specific feeding behavior
/// </summary>
public class Chicken : Animal
{
    private void Awake()
    {
        hunger = 40;
        level = 1;
        exp = 0;
        expToLevel2 = 30;
        expToLevel3 = 60;
    }

    /// <summary>
    /// Override Feed method for chicken-specific behavior
    /// Chickens get bonus EXP from berries
    /// </summary>
    public override void Feed(Fruit fruit)
    {
        if (fruit == null) return;

        int nutrition = fruit.GetNutrition();
        
        // Chicken gets 1.5x bonus when eating berries
        if (fruit is Berry)
        {
            nutrition = Mathf.RoundToInt(nutrition * 1.5f);
            Debug.Log("Chicken loves berries! Bonus EXP!");
        }

        exp += nutrition;
        hunger = Mathf.Max(0, hunger - nutrition);

        Debug.Log($"Chicken ate {fruit.GetType().Name} and gained {nutrition} EXP! Total EXP: {exp}");

        LevelUp();
    }
}
