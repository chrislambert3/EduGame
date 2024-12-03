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
    public float CurrentPower = 2f; // Need a Default power set so the line will show
    public LineRenderer GolfBallLine;
    public Ball GolfBall;


    private void Start()
    {
        GolfBallLine = GameObject.Find("Golf Ball").GetComponent<LineRenderer>();
        GolfBall = GameObject.Find("Golf Ball").GetComponent<Ball>();
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

    public void IncrementShotCount()
    {
        CurrentShotCount++;
        ShotCountText.text = "Shot Count: " + CurrentShotCount.ToString();
        Debug.Log($"IncrementShotCount called, shot is now{CurrentShotCount}");
    }

    public float GetPower()
    {
        return CurrentPower;
    }

    public void ShootGolfBall()
    {
        if (GolfBall.ShootBall()) { IncrementShotCount(); }
    }
}
