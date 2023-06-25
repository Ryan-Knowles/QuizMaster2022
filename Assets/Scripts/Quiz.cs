using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    List<QuestionSO> quizQuestions;
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] GameObject timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    int questionsAnsweredCorrectly = 0;
    int questionsAsked = 0;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    [Header("FinishedScreen")]
    [SerializeField] GameObject finishedCanvas;


    public bool isComplete;

    void Start()
    {
        timer = timerImage.GetComponent<Timer>();
        Reset();
    }

    public void Reset()
    {
        isComplete = false;
        finishedCanvas.SetActive(false);
        gameObject.SetActive(true);
        quizQuestions = new List<QuestionSO>(questions);
        timer.Reset();
        timer.InitTimer(StartQuestionPhase, StartAnswerPhase);
        questionsAnsweredCorrectly = 0;
        questionsAsked = 0;
        progressBar.maxValue = quizQuestions.Count;
        progressBar.value = 0;
        UpdateScore();
        GetNextQuestion();
    }

    public void OnAnswerSelected(int index)
    {
        Debug.Log($"Clicked button {index}");
        // Disable the buttons
        SetButtonState(false);

        // Get button references
        TextMeshProUGUI buttonText = answerButtons[correctAnswerIndex].GetComponentInChildren<TextMeshProUGUI>();
        string correctAnswer = buttonText.text;
        Image buttonImage;

        // Update the question box to display answer and success/failure
        if (index == correctAnswerIndex)
        {
            questionText.text = $"Good job! The correct answer is {correctAnswer}";
            questionsAnsweredCorrectly += 1;
        }
        else if (index <= -1)
        {
            questionText.text = $"Sorry, you ran out of time! The correct answer is {correctAnswer}";
        }
        else
        {
            questionText.text = $"Try again! The correct answer is {correctAnswer}";

        }

        // Highlight correct answer
        buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;

        // Cancel the timer if player clicked a button
        if (index >= 0)
        {
            timer.CancelTimer();
        }

        UpdateScore();
    }

    public void StartQuestionPhase()
    {
        GetNextQuestion();
    }

    public void StartAnswerPhase()
    {
        OnAnswerSelected(-1);
    }

    void QuizFinished()
    {
        progressBar.value = progressBar.maxValue;
        isComplete = true;
        timer.running = false;
        gameObject.SetActive(false);
        TextMeshProUGUI finishedText = finishedCanvas.GetComponentInChildren<TextMeshProUGUI>();
        double score = (double)questionsAnsweredCorrectly / (double)questionsAsked * 100;
        finishedText.text = $"Congratulations!\nYou scored {score:f1}%";
        finishedCanvas.SetActive(true);
    }

    void GetNextQuestion()
    {
        // Check if has next question
        if (quizQuestions.Count == 0)
        {
            QuizFinished();
            return;
        }

        SetButtonState(true);
        SetDefaultButtonSprites();
        GetRandomQuestion();
        DisplayQuestion();
        questionsAsked += 1;
        progressBar.value = questionsAsked;
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.Question;
        for (int i = 0; i < currentQuestion.Answers.Length; i++)
        {
            TMP_Text text = answerButtons[i].GetComponentInChildren<TMP_Text>();
            text.text = currentQuestion.Answers[i];
        }
        correctAnswerIndex = currentQuestion.CorrectAnswerIndex;
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button b = answerButtons[i].GetComponent<Button>();
            b.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void UpdateScore()
    {
        double score = 0; ;
        if (questionsAsked > 0)
        {
            score = (double)questionsAnsweredCorrectly / (double)questionsAsked * 100;
        }
        scoreText.text = $"Score: {score:f1}%";
        Debug.Log($"score: {score} - asked: {questionsAsked} - correct: {questionsAnsweredCorrectly}");
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, quizQuestions.Count);
        currentQuestion = quizQuestions[index];

        if (quizQuestions.Contains(currentQuestion))
        {
            quizQuestions.Remove(currentQuestion);
        }
    }
}
