using UnityEngine;

/// <summary>
/// Player controller - handles movement, fruit collection, and animal feeding
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Inventory inventory;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private float horizontalInput;

    [Header("Boundaries")]
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;

    private void Awake()
    {
        // Get or add Inventory component
        if (inventory == null)
        {
            inventory = GetComponent<Inventory>();
            if (inventory == null)
            {
                inventory = gameObject.AddComponent<Inventory>();
            }
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    /// <summary>
    /// Handle player movement (left-right)
    /// </summary>
    private void HandleMovement()
    {
        // Get input
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Move player
        Vector3 movement = new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);
        transform.position += movement;

        // Clamp position within boundaries
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        transform.position = clampedPosition;
    }

    /// <summary>
    /// Collect a fruit (called when fruit touches player)
    /// </summary>
    public void CollectFruit(Fruit fruit)
    {
        if (fruit != null)
        {
            inventory.AddFruit(fruit);
            
            // Hide the fruit GameObject instead of destroying immediately
            fruit.gameObject.SetActive(false);
            
            Debug.Log($"Collected {fruit.GetType().Name}!");
        }
    }

    /// <summary>
    /// Feed an animal with a fruit from inventory
    /// </summary>
    public void FeedAnimal(Animal animal)
    {
        if (animal == null)
        {
            Debug.Log("No animal to feed!");
            return;
        }

        // Get first available fruit from inventory
        var fruits = inventory.GetAllFruits();
        if (fruits.Count > 0)
        {
            Fruit fruitToFeed = fruits[0];
            animal.Feed(fruitToFeed);
            
            // Remove fruit from inventory and destroy it
            inventory.RemoveFruit(fruitToFeed);
            Destroy(fruitToFeed.gameObject);
            
            Debug.Log($"Fed {animal.GetType().Name} with {fruitToFeed.GetType().Name}!");
        }
        else
        {
            Debug.Log("No fruits in inventory to feed!");
        }
    }

    /// <summary>
    /// Get player's inventory
    /// </summary>
    public Inventory GetInventory()
    {
        return inventory;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Auto-collect fruits when they touch player
        Fruit fruit = collision.GetComponent<Fruit>();
        if (fruit != null)
        {
            CollectFruit(fruit);
        }
    }
}
