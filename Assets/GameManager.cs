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
            secondsClone = 0;
        }

        if (selectedCorrectly)
        {
            questionScreen.SetActive(false);
            questionCreated = false;
        }
    }

    void CreateQuestion()
    {
        int numberOne = Random.Range(0, 101);
        int numberTwo = Random.Range(0, 101);

        string[] possibleSigns = { "+", "-", "*", "/" };
        string signToBeUsed = possibleSigns[Random.Range(0,possibleSigns.Length)];

        questionText.text = $"What is {numberOne} {signToBeUsed} {numberTwo} ?";
    }
}
