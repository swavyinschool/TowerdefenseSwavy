using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Voeg deze regel toe om LINQ-functionaliteit te gebruiken
using System.IO; // Voeg deze regel toe om bestandsbewerkingen uit te voeren

public class HighScoreManager : MonoBehaviour
{
    // Definieer een klasse voor highscores
    [System.Serializable]
    public class HighScore
    {
        public string playerName;
        public int score;

        public HighScore(string name, int score)
        {
            playerName = name;
            this.score = score;
        }
    }

    private List<HighScore> highScores = new List<HighScore>();
    private string filePath; // Het pad naar het bestand waar de highscores worden opgeslagen

    private void Start()
    {
        // Bepaal het pad naar het bestand
        filePath = Application.persistentDataPath + "/highscores.json";

        // Als het bestand bestaat, laad dan de highscores vanuit het bestand
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            highScores = JsonUtility.FromJson<List<HighScore>>(json);
        }
    }

    public void AddHighScore(string playerName, int score)
    {
        // Kijk of de score hoger is dan minstens 1 score in de lijst
        if (highScores.Count < 5 || highScores.Any(hs => score > hs.score))
        {
            // Voeg een nieuwe highscore toe
            highScores.Add(new HighScore(playerName, score));

            // Sorteer de lijst volgens score, van hoog naar laag
            highScores = highScores.OrderByDescending(hs => hs.score).ToList();

            // Als de lijst meer dan 5 elementen bevat, verwijder dan het laatste element
            if (highScores.Count > 5)
            {
                highScores.RemoveAt(highScores.Count - 1);
            }

            // Serializeer de lijst naar JSON
            string json = JsonUtility.ToJson(highScores);

            // Schrijf de JSON-string naar het bestand
            File.WriteAllText(filePath, json);
        }
    }
}
