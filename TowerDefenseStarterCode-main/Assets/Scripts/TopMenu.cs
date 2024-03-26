using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    private Label waveLabel;
    private Label creditsLabel;
    private Label healthLabel;
    private Button startWaveButton;
    private VisualElement root;
    private int currentWave = 1;
    private int playerCredits = 100;
    private int gateHealth = 100;
    private int baseEnemiesPerWave = 10;
    private int enemiesPerWaveIncrease = 5;
    private int totalWaves = 5; // Aantal golven dat je wilt spawnen

    // List to store different levels of enemies
    public List<GameObject> enemyLevels;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // Assign button references
        waveLabel = root.Q<Label>("Wave :");
        creditsLabel = root.Q<Label>("Coins :");
        healthLabel = root.Q<Label>("Health :");
        startWaveButton = root.Q<Button>("Start Wave :");

        // Check if startWaveButton is not null
        if (startWaveButton != null)
        {
            // Subscribe to the click event of startWaveButton
            startWaveButton.clicked += OnStartWaveButtonClicked;
        }
    }

    // Method to handle the click event of the startWaveButton
    void OnStartWaveButtonClicked()
    {
        // Code to execute when the button is clicked
        Debug.Log("Start Wave button clicked!");
        // Call your method to spawn enemies
        SpawnEnemy();
    }

    // Method to spawn enemies
    void SpawnEnemy()
    {
        // Code to spawn enemies
        Debug.Log("Spawning enemies...");
        // You need to implement this logic according to your game requirements
    }
}
