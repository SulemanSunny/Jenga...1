using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_InputField answerInput;
    public GameObject resultPanel;
    public TMP_Text resultText;

    private string[] equations; // Array of equations for the quiz
    private int currentEquationIndex = 0; // Index of the current equation
    private int correctAnswers = 0;
    private bool isQuizActive = false;
    public Scene  active;
    

    public int totalQuestionsToWin = 5;
    private bool show = false;
    private QuizManager quiz;

    private void Start()
    {
        InitializeEquations();
        ShowNextQuestion();
        quiz = FindObjectOfType<QuizManager>();
    }

    private void Update()
    {
        if (isQuizActive && Input.GetKeyDown(KeyCode.N))
        {
            ShowNextQuestion();
        }
        if (show)
        {
            InitializeEquations();
            ShowNextQuestion();
            
        }
    }

    public void CheckAnswer()
    {
        if (!isQuizActive) return;

        string playerAnswer = answerInput.text;
        string correctAnswer = equations[currentEquationIndex - 1].Split('=')[1].Trim();

        if (playerAnswer == correctAnswer)
        {
            correctAnswers++;
        }

        if (correctAnswers >= totalQuestionsToWin)
        {
            WinGame();
        }
        else
        {
            ShowNextQuestion();
        }
    }

    private void ShowNextQuestion()
    {
        if (currentEquationIndex < equations.Length)
        {
            isQuizActive = true;
            questionText.text = equations[currentEquationIndex];
            answerInput.text = "";
            currentEquationIndex++;
        }
        else
        {
            isQuizActive = false;
            resultText.text = "You've answered all questions. You win the game!";
            resultPanel.SetActive(true);
        }
    }

    private void WinGame()
    {
        isQuizActive = false;
        resultText.text = "You win the game!";
        resultPanel.SetActive(true);
        SceneManager.LoadScene("3");
    }

    private void InitializeEquations()
    {
        // Define the equations for the quiz
        equations = new string[]
        {
            "2 + 2 = ",
            "6 * 8 = ",
            "15 / 3 = ",
            "9 - 4 = ",
            "7 * 3 = ",
            "12 / 4 = ",
        };
    }
}
