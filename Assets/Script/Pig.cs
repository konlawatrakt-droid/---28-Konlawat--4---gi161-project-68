using UnityEngine;

/// <summary>
/// Pig animal - demonstrates polymorphism
/// Overrides Feed() to provide pig-specific feeding behavior
/// </summary>
public class Pig : Animal
{
    private void Awake()
    {
        hunger = 60;
        level = 1;
        exp = 0;
        expToLevel2 = 50;
        expToLevel3 = 100;
    }

    /// <summary>
    /// Override Feed method for pig-specific behavior
    /// Pigs eat everything and get standard EXP
    /// </summary>
    public override void Feed(Fruit fruit)
    {
        if (fruit == null) return;

        int nutrition = fruit.GetNutrition();
        
        // Pigs eat everything equally well
        exp += nutrition;
        hunger = Mathf.Max(0, hunger - nutrition);

        Debug.Log($"Pig ate {fruit.GetType().Name} and gained {nutrition} EXP! Total EXP: {exp}");

        LevelUp();
    }
}
