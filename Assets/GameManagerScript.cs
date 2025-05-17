using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public Text timerText;
    float timePassed = 0;
    public bool timerPaused = false;

    private int seconds;
    private int minutes;

    public GameObject questionScreen;

    public Text questionText;

    public Text optionOne;
    public Text optionTwo;
    public Text optionThree;
    public Text optionFour;

    public int userQuestionRate;
    public static int questionRate;
    public static bool questionRateInitialised = false;

    private int secondsClone;

    private bool selectedCorrectly = false;
    private bool questionCreated = false;

    private float answer;

    public GameObject winScreen;
    public Text timerMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!questionRateInitialised)
        {
            questionRate = userQuestionRate;
            questionRateInitialised = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(questionRate);

        if (!timerPaused)
        {
            timePassed += Time.deltaTime;
            minutes = Mathf.FloorToInt(timePassed / 60);
            seconds = Mathf.FloorToInt(timePassed % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        secondsClone = seconds;
        if (secondsClone != 0 && secondsClone % questionRate == 0 && !questionCreated)
        {
            questionCreated = true;
            CreateQuestion();
            questionScreen.SetActive(true);
        }

        if (selectedCorrectly)
        {
            questionScreen.SetActive(false);
            questionCreated = false;
            secondsClone = 0;
            selectedCorrectly = false;
        }
    }

    public void win()
    {
        if (questionRate != 1)
        {
            questionRate--;
        }
        winScreen.SetActive(true);
        timerPaused = true;
        timerMessage.text = string.Format("Your time was {0:00}:{1:00}", minutes, seconds);
    }

    void CreateQuestion()
    {
        int numberOne = Random.Range(1, 101);
        int numberTwo = Random.Range(1, 101);

        string[] possibleSigns = { "+", "-", "*", "/" };
        string signToBeUsed = possibleSigns[Random.Range(0,possibleSigns.Length)];

        questionText.text = $"What is {numberOne} {signToBeUsed} {numberTwo} ?";

        if (signToBeUsed == "+")
        {
            answer = numberOne + numberTwo;
        }
        else if (signToBeUsed == "-")
        {
            answer = numberOne - numberTwo;
        }
        else if (signToBeUsed == "*")
        {
            answer = numberOne * numberTwo;
        }
        else
        {
            answer = numberOne / numberTwo;  
        }

        float[] possibleAnswers = { answer - 1, answer + 1, answer + 2, answer};
        float[] shuffledAnswers = ShuffleArray(possibleAnswers);

        optionOne.text = shuffledAnswers[0].ToString();
        optionTwo.text = shuffledAnswers[1].ToString();
        optionThree.text = shuffledAnswers[2].ToString();
        optionFour.text = shuffledAnswers[3].ToString();
    }

    float[] ShuffleArray(float[] array)
    {
        float[] shuffledArray = (float[])array.Clone();
        for (int i = 0; i < shuffledArray.Length; i++)
        {
            int rnd = Random.Range(i, shuffledArray.Length);
            float temp = shuffledArray[rnd];
            shuffledArray[rnd] = shuffledArray[i];
            shuffledArray[i] = temp;
        }
        return shuffledArray;
    }

    public void CheckAnswer(GameObject buttonObject)
    {
        Text buttonText = buttonObject.GetComponentInChildren<Text>();

        if (buttonText.text == answer.ToString())
        {
            selectedCorrectly = true;
        }
        else
        {
            CreateQuestion();
        }
    }
}
