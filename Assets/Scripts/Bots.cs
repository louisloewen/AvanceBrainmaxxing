using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot
{
    public string Name;
    public int Score;

    public Bot(string name)
    {
        Name = name;
        Score = 0;
    }

    public int AnswerQuestion(QnA question)
    {
        // Simulate bot's answer (you can implement your own logic here)
        return Random.Range(0, 4); // Randomly choose an answer index
    }
}

