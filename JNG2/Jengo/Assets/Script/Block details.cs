using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BlockDetails : MonoBehaviour
{
    public TMP_Text detailsText;
    public string gradeLevel;
    public string domain;
    public string cluster;
    public string standardID;
    public string standardDescription;
    public string blockType;
    public GameObject Table;

    public bool isDetailsVisible = false;
    private bool isQuizVisible = false;
    private bool isGameWon = false;

    public GameObject stack;
    public float vibrationStrength = 0.1f;
    public float cameraMoveSpeed = 1.0f;
    public TMP_Text questionText; 
    public TMP_InputField answerInput;
    private int correctAnswers = 0;


    private string[] equations;
    private int currentEquationIndex = 0;
   

    private void Start()
    {
        HideDetails();
        HideQuiz();
        InitializeEquations(); 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ToggleDetails();
                }
            }
        }

        if (isDetailsVisible && Input.GetKeyDown(KeyCode.Q) && !isGameWon)
        {
            ShowNextEquation();
        }

       

        if (Input.GetKeyDown(KeyCode.U))
        {
            MoveCameraUp();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            MoveCameraDown();
        }

        if (isQuizVisible)
        {
           
                if (answerInput.isFocused && Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    CheckAndHandleAnswer();
                }

                if (Input.GetKeyDown(KeyCode.N) && !isGameWon)
                {
                    ShowNextEquation();
                }
            }
        
    }

    private void ToggleDetails()
    {
        isDetailsVisible = !isDetailsVisible;
        if (isDetailsVisible)
        {
            ShowDetails();

        }
        else
        {
            HideDetails();
        }
    }

    private void ShowDetails()
    {
        string details = $"Grade level: {gradeLevel}\nDomain: {domain}\nCluster: {cluster}\nStandard ID: {standardID}\nStandard Description: {standardDescription}";
        detailsText.text = details;
        detailsText.gameObject.SetActive(true);
        if (Input.GetKey(KeyCode.Q))
        {
            HideDetails();
            ShowQuiz();
        }
    }

    private void HideDetails()
    {
        detailsText.gameObject.SetActive(false);
    }

    private void ShowNextEquation()
    {
        if (currentEquationIndex < equations.Length)
        {
          
            string equation = equations[currentEquationIndex];
            questionText.text = "Math Question: " + equation;
            answerInput.gameObject.SetActive(true); 
            answerInput.text = "";
            currentEquationIndex++;
        }
        else
        {
            WinGame();
        }
    }




    private void CorrectAnswer()
    {
        correctAnswers++;
        if (correctAnswers == 5)
        {
            WinGame();
        }
        else
        {
            ShowNextEquation();
        }
    }

    private void IncorrectAnswer()
    {
        VibrateTable();
            
        HideDetails();
        HideQuiz();
        currentEquationIndex = 0; 
        correctAnswers = 0; 
        ShowNextEquation();
        if (stack != null)
        {
            Destroy(stack);
        }
    }



    private bool CheckAnswer(string answer)
    {
       
        string correctAnswer = equations[currentEquationIndex - 1].Split('=')[1].Trim();
        return answer == correctAnswer;
    }

    private void WinGame()
    {
        Debug.Log(" You win the game!");
        isGameWon = true;
        SceneManager.LoadScene(3);
        currentEquationIndex = 0; 
        correctAnswers = 0;
    }

    

   

    private void VibrateTable()
    {
        if (Table != null)
        {
            float xOffset = Random.Range(-vibrationStrength, vibrationStrength);
            float zOffset = Random.Range(-vibrationStrength, vibrationStrength);
            transform.Translate(new Vector3(xOffset, 0, zOffset));
        }
    }

    private void MoveCameraUp()
    {
        Camera.main.transform.Translate(Vector3.up * cameraMoveSpeed * Time.deltaTime);
    }
    private void MoveCameraDown()
    {
        Camera.main.transform.Translate(Vector3.down * cameraMoveSpeed * Time.deltaTime);
    }
    private void ShowQuiz()
    {
        InitializeEquations();
        currentEquationIndex = 0;
        correctAnswers = 0;
        ShowNextEquation();
        HideDetails();
        isQuizVisible = true;
        answerInput.gameObject.SetActive(true);
    }
    private void HideQuiz()
    {
        questionText.text = ""; 
        answerInput.text = ""; 
        isQuizVisible = false;
        answerInput.gameObject.SetActive(false);
    }
    private void CheckAndHandleAnswer()
    {
        string playerAnswer = answerInput.text;
        string correctAnswer = equations[currentEquationIndex - 1].Split('=')[1].Trim();

        if (playerAnswer == correctAnswer)
        {
            CorrectAnswer();
        }
        else
        {
            IncorrectAnswer();
        }
    }



    private void InitializeEquations()
    {
        
        if (blockType == "Glass")
        {
            equations = new string[]
            {
                "7 * 3 =",
                "12 / 4 =",
                "8 - 2 =",
                "9 * 5 =",
                "14 / 7 ="
            };
        }
        else if (blockType == "Wood")
        {
            equations = new string[]
            {
                "4 + 2 =",
                "5 * 8 =",
                "15 / 3 =",
                "7 - 2 =",
                "10 * 4 ="
            };
        }
        else if (blockType == "Stone")
        {
            equations = new string[]
            {
                "9 + 7 =",
                "14 * 6 =",
                "18 / 2 =",
                "5 - 1 =",
                "20 * 3 ="
            };
        }
    }
}




