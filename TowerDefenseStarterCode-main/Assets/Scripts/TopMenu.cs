using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TopMenu : MonoBehaviour
{
    public Text waveLabel;
    public Text creditsLabel;
    public Text healthLabel;
    public Button startWaveButton;

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
        // Koppel de startWaveButton aan de StartWave functie
        startWaveButton.onClick.AddListener(StartWave);

        // Update de labels met de startwaarden
        UpdateLabels();
    }

    void OnDestroy()
    {
        // Verwijder de koppeling van de StartWave functie van de startWaveButton
        startWaveButton.onClick.RemoveListener(StartWave);
    }

    void UpdateLabels()
    {
        // Pas de tekst van de labels aan met de huidige waarden
        waveLabel.text = "Wave: " + currentWave;
        creditsLabel.text = "Credits: " + playerCredits;
        healthLabel.text = "Gate Health: " + gateHealth;
    }

    void StartWave()
    {
        if (currentWave <= totalWaves) // Controleer of het totale aantal golven is bereikt
        {
            // Bepaal het aantal vijanden voor deze golf
            int enemiesThisWave = baseEnemiesPerWave + (currentWave - 1) * enemiesPerWaveIncrease;

            // Bepaal het niveau van de vijand voor deze golf
            int enemyLevelIndex = (currentWave - 1) % enemyLevels.Count;
            GameObject enemyLevel = enemyLevels[enemyLevelIndex];

            // Spawn vijanden
            for (int i = 0; i < enemiesThisWave; i++)
            {
                SpawnEnemy(enemyLevel);
            }

            // Incrementeer de huidige golf
            currentWave++;

            // Update de UI-labels
            UpdateLabels();
        }
        else
        {
            Debug.Log("Alle golven zijn gespawnd.");
        }
    }

    void SpawnEnemy(GameObject enemyLevel)
    {
        // Bepaal het type vijand op basis van het GameObject-niveau
        int enemyType = DetermineEnemyType(enemyLevel);

        // Bepaal het pad voor de vijand
        GameObject pathObject = GetRandomPath(); // Veronderstel dat GetRandomPath een pad-object retourneert
        Path path = pathObject.GetComponent<Path>(); // Veronderstel dat Path een script is dat is gekoppeld aan het pad-object

        // Roep de SpawnEnemy-methode aan van de EnemySpawner om de vijand te spawnen
        EnemySpawner.Instance.SpawnEnemy(enemyType, path);
    }

    int DetermineEnemyType(GameObject enemyLevel)
    {
        // Implementeer je eigen logica om het type vijand te bepalen op basis van het niveau van de vijand
        // Dit kan bijvoorbeeld worden gedaan door te controleren welk GameObject wordt doorgegeven en het bijbehorende type te retourneren
        // Voor nu retourneren we gewoon een statische waarde
        return 0; // Stel dat het eerste type vijand wordt geretourneerd
    }

    GameObject GetRandomPath()
    {
        // Implementeer je eigen logica om een willekeurig pad te kiezen
        // Dit kan bijvoorbeeld worden gedaan door een van de beschikbare paden willekeurig te kiezen
        // Voor nu retourneren we gewoon een van de standaardpaden
        return EnemySpawner.Instance.Path1[0]; // Stel dat we het eerste pad kiezen
    }
}
