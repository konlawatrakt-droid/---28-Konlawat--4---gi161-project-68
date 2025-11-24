using UnityEngine;

public abstract class Animal : MonoBehaviour, IFailable
{
    [SerializeField] protected int hunger;
    [SerializeField] protected int level = 1;
    [SerializeField] protected int exp = 0;

    [Header("Level Requirements")]
    [SerializeField] protected int expToLevel2 = 50;
    [SerializeField] protected int expToLevel3 = 100;

    // Properties
    public int Hunger { get { return hunger; } set { hunger = value; } }
    public int Level { get { return level; } }
    public int Exp { get { return exp; } }

    public virtual void Feed(Fruit fruit)
    {
        if (fruit == null) return;

        int nutrition = fruit.GetNutrition();
        exp += nutrition;
        hunger = Mathf.Max(0, hunger - nutrition);

        Debug.Log($"{this.GetType().Name} ate {fruit.GetType().Name} and gained {nutrition} EXP! Total EXP: {exp}");

        LevelUp();
    }

    /// Check and  level up
    protected virtual void LevelUp()
    {
        if (level == 1 && exp >= expToLevel2)
        {
            level = 2;
            Debug.Log($"{this.GetType().Name} leveled up to Level 2!");
        }
        else if (level == 2 && exp >= expToLevel3)
        {
            level = 3;
            Debug.Log($"{this.GetType().Name} leveled up to Level 3!");
        }
    }
    /// Called when animal fails/dies
    public virtual void FailBehavior()
    {
        Debug.Log($"{this.GetType().Name} has failed!");
    }

    private void Update()
    {
        // Increase hunger over time
        hunger += (int)(Time.deltaTime * 2);
    }
}
