using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgain : MonoBehaviour
{
    [SerializeField] GameObject quizCanvas;


    public void RestartQuiz()
    {
        Quiz quiz = quizCanvas.GetComponent<Quiz>();
        quiz.Reset();
    }
}
