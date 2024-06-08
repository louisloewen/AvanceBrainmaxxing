using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    public Text playerScoreText;
    public Text[] botScoreTexts;
    public GameObject rankingPanel;

    public void ShowRanking(string playerName, int playerScore, List<Bot> bots)
    {
        rankingPanel.SetActive(true);

        // Crear una lista de puntuaciones
        List<ScoreEntry> scoreEntries = new List<ScoreEntry>
        {
            new ScoreEntry(playerName, playerScore)
        };

        // Añadir las puntuaciones de los bots
        foreach (var bot in bots)
        {
            scoreEntries.Add(new ScoreEntry(bot.Name, bot.Score));
        }

        // Ordenar por puntuación, de mayor a menor
        scoreEntries = scoreEntries.OrderByDescending(entry => entry.Score).ToList();

        // Mostrar las puntuaciones ordenadas
        for (int i = 0; i < scoreEntries.Count; i++)
        {
            if (i == 0)
            {
                playerScoreText.text = $"{scoreEntries[i].Name}: {scoreEntries[i].Score}";
            }
            else if (i - 1 < botScoreTexts.Length)
            {
                botScoreTexts[i - 1].text = $"{scoreEntries[i].Name}: {scoreEntries[i].Score}";
            }
        }
    }
}

public class ScoreEntry
{
    public string Name;
    public int Score;

    public ScoreEntry(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
