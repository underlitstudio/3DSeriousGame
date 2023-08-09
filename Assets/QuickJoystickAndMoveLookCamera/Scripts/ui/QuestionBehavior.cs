using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class QuestionBehavior : MonoBehaviour
{
    //public TMP_Text questionTextPanel;
    public TMP_Text[] questionTextPanels;
    public TMP_Text responseTextPanel;
    public Button[] questionButtons;
    
    public QnAScriptableObject qnaCollection; // Assign in the Inspector

    private void Start()
    {
        // Attach button click events
        for (int i = 0; i < questionButtons.Length; i++)
        {
            int questionIndex = i; // To capture the correct index in the lambda
            questionButtons[i].GetComponentInChildren<TMP_Text>().text = qnaCollection.qnaPairs[questionIndex].question;
            questionButtons[i].onClick.AddListener(() => OnQuestionButtonClicked(questionIndex));
        }
    }

    private void OnQuestionButtonClicked(int questionIndex)
    {
        if (questionIndex < qnaCollection.qnaPairs.Count)
        {
            QnAScriptableObject.QnAPair selectedQnA = qnaCollection.qnaPairs[questionIndex];

            // Display question and response
           // questionTextPanel.text = selectedQnA.question;
            responseTextPanel.text = selectedQnA.response;
        }
    }
}
