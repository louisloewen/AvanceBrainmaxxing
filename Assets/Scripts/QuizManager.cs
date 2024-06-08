using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class QnAContainer
{
    public List<QnA> questions;
}

public class QuizManager : MonoBehaviour
{
    public GameObject[] options;
    public GameObject Panel;
    public GameObject GoPanel;
    public Text QuestionText;
    public Text ScoreText;
    public Text PlayerScoreText;
    public Text[] BotScoreTexts;
    public string playerName = "Player";
    public int score = 0;
    public int TotalQ = 0;
    private int currentQuestionIndex = 0;
    private List<QnA> QandA = new List<QnA>();
    private List<Bot> Bots;
    public RankingManager rankingManager;

    private void Start()
    {
        GoPanel.SetActive(false);
        PlayerScoreText.text = $"{playerName}: {score}/{TotalQ}";
        Bots = new List<Bot>
        {
            new Bot("Bot1"),
            new Bot("Bot2")
        };
        StartCoroutine(FetchQuestions());
    }

    private IEnumerator FetchQuestions()
    {
        string url = "http://your-api-url/api/users/4/questionsets/math";
        Debug.Log("Fetching questions from: " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch questions: " + request.error);
            }
            else
            {
                Debug.Log("Successfully fetched questions");
                string json = request.downloadHandler.text;
                QnAContainer container = JsonUtility.FromJson<QnAContainer>(json);

                if (container != null && container.questions != null)
                {
                    QandA = container.questions;
                    TotalQ = QandA.Count;
                    genereateQuestion();
                }
                else
                {
                    Debug.LogError("Failed to parse questions.");
                }
            }
        }
    }

    private void genereateQuestion()
    {
        if (currentQuestionIndex < TotalQ)
        {
            QnA currentQnA = QandA[currentQuestionIndex];
            QuestionText.text = currentQnA.QuestionText;
            SetAnswers(currentQnA);
            BotAnswerQuestion(currentQnA);
            currentQuestionIndex++;
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }

    private void SetAnswers(QnA currentQnA)
    {
        string[] choices = currentQnA.GetChoices();
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerS>().isCorrect = (currentQnA.GetCorrectAnswerIndex() == i);
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = choices[i];
        }
    }

    private void BotAnswerQuestion(QnA question)
    {
        foreach (var bot in Bots)
        {
            int botAnswer = bot.AnswerQuestion(question);
            if (botAnswer == question.GetCorrectAnswerIndex())
            {
                bot.Score += 1;
            }
        }
        UpdateBotScores();
    }

    private void UpdateBotScores()
    {
        for (int i = 0; i < Bots.Count; i++)
        {
            BotScoreTexts[i].text = Bots[i].Name + ": " + Bots[i].Score + "/" + TotalQ;
        }
    }

    public void correct()
    {
        score++;
        if (currentQuestionIndex < QandA.Count)
        {
            QandA.RemoveAt(currentQuestionIndex);
        }
        ActualScore();
        genereateQuestion();
    }

    public void wrong()
    {
        if (currentQuestionIndex < QandA.Count)
        {
            QandA.RemoveAt(currentQuestionIndex);
        }
        ActualScore();
        genereateQuestion();
    }

    private void ActualScore()
    {
        PlayerScoreText.text = $"{playerName}: {score}/{TotalQ}";
        UpdateBotScores();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameOver()
    {
        Panel.SetActive(false);
        GoPanel.SetActive(true);
        rankingManager.ShowRanking(playerName, score, Bots);
    }
}
