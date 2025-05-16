using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    float timePassed = 0;

    public GameObject questionScreen;

    public Text questionText;

    public Text optionOne;
    public Text optionTwo;
    public Text optionThree;
    public Text optionFour;

    public int questionRate;
    private int secondsClone;

    private bool selectedCorrectly = false;
    private bool questionCreated = false;

    private float answer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timePassed / 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

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
