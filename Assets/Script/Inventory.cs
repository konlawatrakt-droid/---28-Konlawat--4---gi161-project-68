using System.Collections.Generic;
using UnityEngine;

/// Inventory system for storing collected fruits
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Fruit> fruits = new List<Fruit>();

    /// Add a fruit to the inventory
    public void AddFruit(Fruit fruit)
    {
        if (fruit != null)
        {
            fruits.Add(fruit);
            Debug.Log($"Added {fruit.GetType().Name} to inventory. Total fruits: {fruits.Count}");
        }
    }
    /// Remove a specific fruit from inventory
    public void RemoveFruit(Fruit fruit)
    {
        if (fruits.Contains(fruit))
        {
            fruits.Remove(fruit);
            Debug.Log($"Removed {fruit.GetType().Name} from inventory. Remaining fruits: {fruits.Count}");
        }
    }

    /// Get the first fruit of a specific type
    /// Returns null if not found
    public Fruit GetFruitByType<T>() where T : Fruit
    {
        foreach (Fruit fruit in fruits)
        {
            if (fruit is T)
            {
                return fruit;
            }
        }
        return null;
    }

    /// Count fruits of a specific type
    public int CountFruitType<T>() where T : Fruit
    {
        int count = 0;
        foreach (Fruit fruit in fruits)
        {
            if (fruit is T)
            {
                count++;
            }
        }
        return count;
    }

    /// Get all fruits in inventory
    public List<Fruit> GetAllFruits()
    {
        return new List<Fruit>(fruits);
    }

    /// Clear all fruits from inventory
    public void ClearInventory()
    {
        fruits.Clear();
        Debug.Log("Inventory cleared!");
    }

    /// Get total number of fruits
    public int GetFruitCount()
    {
        return fruits.Count;
    }
}
