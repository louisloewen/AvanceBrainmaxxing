
[System.Serializable]
public class QnA
{
    public int QuestionSetID;
    public int QuestionID;
    public string QuestionText;
    public string CorrectAnswer;
    public string Choice1;
    public string Choice2;
    public string Choice3;
    public string Choice4;

    public string[] GetChoices()
    {
        return new string[] { Choice1, Choice2, Choice3, Choice4 };
    }

    public int GetCorrectAnswerIndex()
    {
        string[] choices = GetChoices();
        for (int i = 0; i < choices.Length; i++)
        {
            if (choices[i] == CorrectAnswer)
            {
                return i;
            }
        }
        return -1; // Handle appropriately if not found
    }
}