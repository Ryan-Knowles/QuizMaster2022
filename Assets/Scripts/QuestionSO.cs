using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(minLines:2,maxLines:6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [Range(0,3)]
    [SerializeField] int correctAnswerIndex = 0;

    public string Question => question;
    public string[] Answers => answers;
    public int CorrectAnswerIndex {
        get => correctAnswerIndex;
        set
        {
            if ((value >= 0) && (value < answers.Length))
            {
                correctAnswerIndex = value;
            }
            else if(value < 0)
            {
                correctAnswerIndex = 0;
            }
            else if (value >= answers.Length)
            {
                correctAnswerIndex = answers.Length - 1;
            }
        }
    }
}

