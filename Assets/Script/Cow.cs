using UnityEngine;
public class Cow : Animal
{
    private void Awake()
    {
        hunger = 50;
        level = 1;
        exp = 0;
        expToLevel2 = 40;
        expToLevel3 = 80;
    }
    /// Cows get bonus EXP from green fruits
    public override void Feed(Fruit fruit)
    {
        if (fruit == null) return;

        int nutrition = fruit.GetNutrition();
        
        // Cow gets 1.2x bonus when eating apples (green fruit)
        if (fruit is Apple)
        {
            nutrition = Mathf.RoundToInt(nutrition * 1.2f);
            Debug.Log("Cow loves apples! Bonus EXP!");
        }

        exp += nutrition;
        hunger = Mathf.Max(0, hunger - nutrition);

        Debug.Log($"Cow ate {fruit.GetType().Name} and gained {nutrition} EXP! Total EXP: {exp}");

        LevelUp();
    }
}
