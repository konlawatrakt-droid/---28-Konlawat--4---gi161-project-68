using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main game manager - controls game flow, turns, spawning, and win/lose conditions
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    [SerializeField] private int currentTurn = 0;
    [SerializeField] private int maxTurn = 10;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float fruitSpawnRate = 2f;

    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private List<Animal> animals = new List<Animal>();

    [Header("Fruit Prefabs")]
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private GameObject bananaPrefab;
    [SerializeField] private GameObject berryPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnHeight = 6f;
    [SerializeField] private float minSpawnX = -7f;
    [SerializeField] private float maxSpawnX = 7f;
    [SerializeField] private float fruitFallSpeed = 2f;
    [SerializeField] private float spawnSpeedIncrease = 0.1f; // Speed increases each turn

    [Header("UI References (Optional)")]
    [SerializeField] private Text turnText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text gameStatusText;

    private bool gameActive = false;
    private float spawnTimer = 0f;
    private int totalAnimalsAtLevel3 = 0;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (!gameActive) return;

        // Update timer
        timer += Time.deltaTime;
        UpdateUI();

        // Spawn fruits
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= fruitSpawnRate)
        {
            SpawnFruit();
            spawnTimer = 0f;
        }
    }

    /// <summary>
    /// Start the game
    /// </summary>
    public void StartGame()
    {
        gameActive = true;
        currentTurn = 1;
        timer = 0f;
        totalAnimalsAtLevel3 = 0;

        Debug.Log("Game Started! Feed the animals to Level 3 within " + maxTurn + " turns!");
        
        if (gameStatusText != null)
        {
            gameStatusText.text = "Game Started!";
        }

        StartCoroutine(TurnSystem());
    }

    /// <summary>
    /// Turn-based system coroutine
    /// </summary>
    private IEnumerator TurnSystem()
    {
        while (currentTurn <= maxTurn && gameActive)
        {
            Debug.Log($"--- Turn {currentTurn}/{maxTurn} ---");
            
            // Wait for turn duration (e.g., 30 seconds per turn)
            yield return new WaitForSeconds(30f);

            // End turn
            EndTurn();
            currentTurn++;

            // Increase difficulty (fruits spawn faster)
            fruitSpawnRate = Mathf.Max(0.5f, fruitSpawnRate - spawnSpeedIncrease);
            fruitFallSpeed += 0.5f;
        }

        // Check final win condition
        CheckWinCondition();
    }

    /// <summary>
    /// End current turn
    /// </summary>
    private void EndTurn()
    {
        Debug.Log($"Turn {currentTurn} ended!");
        
        // Check if any animal reached Level 3
        CheckWinCondition();
    }

    /// <summary>
    /// Spawn a random fruit at random position
    /// </summary>
    private void SpawnFruit()
    {
        // Randomly select fruit type
        int randomFruit = Random.Range(0, 3);
        GameObject fruitPrefab = null;

        switch (randomFruit)
        {
            case 0:
                fruitPrefab = applePrefab;
                break;
            case 1:
                fruitPrefab = bananaPrefab;
                break;
            case 2:
                fruitPrefab = berryPrefab;
                break;
        }

        if (fruitPrefab == null)
        {
            Debug.LogWarning("Fruit prefab not assigned!");
            return;
        }

        // Random spawn position
        float spawnX = Random.Range(minSpawnX, maxSpawnX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnHeight, 0);

        // Spawn fruit
        GameObject fruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
        
        // Add falling behavior
        Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = fruit.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = fruitFallSpeed;

        // Add collider if not present
        if (fruit.GetComponent<Collider2D>() == null)
        {
            CircleCollider2D collider = fruit.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
        }

        // Destroy fruit after 10 seconds if not collected
        Destroy(fruit, 10f);
    }

    /// <summary>
    /// Check win/lose condition
    /// </summary>
    public void CheckWinCondition()
    {
        totalAnimalsAtLevel3 = 0;

        foreach (Animal animal in animals)
        {
            if (animal.Level >= 3)
            {
                totalAnimalsAtLevel3++;
            }
        }

        // Win condition: At least one animal reaches Level 3 within max turns
        if (totalAnimalsAtLevel3 > 0)
        {
            WinGame();
        }
        else if (currentTurn > maxTurn)
        {
            LoseGame();
        }
    }

    /// <summary>
    /// Win game
    /// </summary>
    private void WinGame()
    {
        gameActive = false;
        Debug.Log($"🎉 YOU WIN! {totalAnimalsAtLevel3} animal(s) reached Level 3!");
        
        if (gameStatusText != null)
        {
            gameStatusText.text = $"YOU WIN! {totalAnimalsAtLevel3} animal(s) reached Level 3!";
            gameStatusText.color = Color.green;
        }
    }

    /// <summary>
    /// Lose game
    /// </summary>
    private void LoseGame()
    {
        gameActive = false;
        Debug.Log("😢 YOU LOSE! No animals reached Level 3 in time!");
        
        if (gameStatusText != null)
        {
            gameStatusText.text = "YOU LOSE! Try again!";
            gameStatusText.color = Color.red;
        }

        // Call fail behavior for all animals
        foreach (Animal animal in animals)
        {
            animal.FailBehavior();
        }
    }

    /// <summary>
    /// Update UI elements
    /// </summary>
    private void UpdateUI()
    {
        if (turnText != null)
        {
            turnText.text = $"Turn: {currentTurn}/{maxTurn}";
        }

        if (timerText != null)
        {
            timerText.text = $"Time: {timer:F1}s";
        }
    }

    /// <summary>
    /// Add an animal to be tracked
    /// </summary>
    public void RegisterAnimal(Animal animal)
    {
        if (!animals.Contains(animal))
        {
            animals.Add(animal);
            Debug.Log($"Registered {animal.GetType().Name} to game manager");
        }
    }

    /// <summary>
    /// Feed specific animal (can be called by UI buttons)
    /// </summary>
    public void FeedAnimalByIndex(int animalIndex)
    {
        if (animalIndex >= 0 && animalIndex < animals.Count)
        {
            player.FeedAnimal(animals[animalIndex]);
        }
    }

    // Public getters
    public int CurrentTurn { get { return currentTurn; } }
    public int MaxTurn { get { return maxTurn; } }
    public float Timer { get { return timer; } }
    public bool IsGameActive { get { return gameActive; } }
}
