using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 15f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    public bool hasAnsweredQuestion = false;
    float timerValue;
    public bool running = false;

    Action questionCB;
    Action answerCB;

    [SerializeField] bool DEBUG = false;

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            UpdateTimer();
        }
    }

    void UpdateTimer(bool notify = true)
    {
        timerValue -= Time.deltaTime;

        if (timerValue < 0)
        {
            if (notify)
            {
                // Send a signal to Quiz
                Action cb = !hasAnsweredQuestion ? answerCB : questionCB;
                cb();
            }
            hasAnsweredQuestion = !hasAnsweredQuestion;
            ResetTimer();

        }
        else
        {
            Image ti = gameObject.GetComponent<Image>();
            float timerBase = hasAnsweredQuestion ? timeToShowCorrectAnswer : timeToCompleteQuestion;
            ti.fillAmount = (timerValue / timerBase);
        }
        if (DEBUG)
        {
            Debug.Log($"Has Answered? {hasAnsweredQuestion} - Timer: {timerValue:f2}s");
        }
    }

    void ResetTimer()
    {
        if (hasAnsweredQuestion)
        {
            timerValue = timeToShowCorrectAnswer;
        }
        else
        {
            timerValue = timeToCompleteQuestion;
        }
    }

    public void CancelTimer()
    {
        timerValue = -10f;
        UpdateTimer(false);
    }

    public void InitTimer(Action startQuestionCB, Action startAnswerCB)
    {
        questionCB = startQuestionCB;
        answerCB = startAnswerCB;

        Reset();
        running = true;
    }

    public void Reset()
    {
        running = false;
        timerValue = timeToCompleteQuestion;
    }
}


