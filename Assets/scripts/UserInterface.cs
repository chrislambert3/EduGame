using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public GameObject NextLevScreen;
    public GameObject GameUI;
    public TextMeshProUGUI ShotCountText;
    public TMP_Text PowerText;
    public Slider Slider;
    public int CurrentShotCount = 0;
    public float MaxPower = 20f;
    public float CurrentPower = 0f; // Need a Default power set so the line will show
    public LineRenderer GolfBallLine;
    public Ball GolfBall;
    private Question Question;
    private TMP_Text Prompt;


    private void Start()
    {
        GolfBall = GameObject.Find("Golf Ball").GetComponent<Ball>();
        GolfBallLine = GameObject.Find("Golf Ball").GetComponent<LineRenderer>();
        Slider = GameObject.Find("Slider").GetComponent<Slider>();
        
        // configure slider max/min
        Slider.maxValue = MaxPower;
        Slider.minValue = -1 * MaxPower;

        this.Question = new Question();
        this.Prompt = GameObject.Find("Prompt").GetComponent<TMP_Text>();
        UpdatePrompt();
    }

    public void GoToScene(string sceneName)
    {
        Debug.Log($"going to: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game has been slain(quit game)");
    }
    [ContextMenu("Activate Level Finish")]
    public void ActivateLevelFinish()
    {
        Debug.Log("ActivateLevelFinish has been called");
        GameUI.SetActive(false);
        NextLevScreen.SetActive(true);
    }
    // gets call onValueChange of power slider
    public void SetTextBox()
    {   // update the current power to the variable and on screen
        PowerText.text = "POWER SLIDER: " + Slider.value.ToString("F2");
        CurrentPower = float.Parse(Slider.value.ToString("F2"));
        // if the line set to active redraw the line as you change up the power
        if (GolfBallLine.positionCount == 2)
        {
            GolfBall.RedrawLine();
        }

    }
    //increments shot count for shot counter
    public void IncrementShotCount()
    {
        CurrentShotCount++;
        ShotCountText.text = "Shot Count: " + CurrentShotCount.ToString();
        //Debug.Log($"IncrementShotCount called, shot is now{CurrentShotCount}");
    }

    public float GetPower()
    {
        return CurrentPower;
    }

    public void ShootGolfBall()
    {
        if (GolfBall.ShootBall()) { IncrementShotCount(); }
    }

    public void UpdatePrompt()
    {
        this.Prompt.text = this.Question.GetQuestion();
        this.Prompt.fontStyle = FontStyles.Bold;
        this.Prompt.fontSize = 24;
    }
}
