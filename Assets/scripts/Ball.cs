using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    /// Headers that will show up in the script inspector field
    [Header("References")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private LineRenderer line;
    [Header("Attributes")]
    [SerializeField] private float maxPower = 20f;
    //[SerializeField] private float power = 2f;    /// I may call GetPower() directly into the parameter
    [SerializeField] private float maxGoalSpeed = 4f;
    [SerializeField] private UserInterface user;
    [SerializeField] private AudioSource goalSound;
    [SerializeField] private AudioSource wallSound;

    private bool isDragging;
    private bool inHole;
    private float BallDistance;
    private Vector2 BallDirection;
    private Vector2 LineDirection;

    private void Start()
    {
        user = GameObject.Find("Canvas").GetComponent<UserInterface>();
        body = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
        goalSound = GameObject.Find("Victory Sound").GetComponent<AudioSource>();
        wallSound = GameObject.Find("Wall Sound").GetComponent<AudioSource>();
    }
    private void Update()
    {
        PlayerInput();
    }
    // this returns if the ball is moving "slow enough" to hit again
    private bool isReady()
    {
        return body.linearVelocity.magnitude <= 0.2f;
    }
    public void setDirection(Vector2 direction)
    {
        BallDirection = direction;
        LineDirection = direction;
    }

    private void PlayerInput()
    {   // if the ball is still rolling, return to ignore
        if (!isReady()) return;
        user.UpdatePrompt();
        // gets mouse position from screen to "game world"
        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // gets relative position from mouse too ball
        float distance = Vector2.Distance(transform.position, inputPos);

        // if where you click the mouse is close enough to the ball
        if (Input.GetMouseButtonDown(0) && distance <= 0.5f) { DragStart(); }
        // this is for if the mouse is still clicked on the following frames
        if (Input.GetMouseButton(0) && isDragging) { DragChagne(inputPos); }

        // if you relese the mouse
        if (Input.GetMouseButtonUp(0) && isDragging) { DragRelease(inputPos); }
    }
    private void DragStart()
    {
        isDragging = true;
        // create 2 points for the line (the clicked ball and direction to go)
        line.positionCount = 2;
    }
    private void DragChagne(Vector2 pos)
    {
        LineDirection = (Vector2)transform.position - pos;

        // matches the line to the trajectory of the shot
        // the parameters divided by 2 to make the line shorter than the actual shot so it wont project far
        // *** Im thinking we make this a difficulty mode where the projected trajectory is longer when easier/ shorter when harder
        line.SetPosition(0, transform.position);
        line.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((LineDirection * user.GetPower()) / 2, maxPower / 2));
    }

    public void RedrawLine()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((LineDirection * user.GetPower()) / 2, maxPower / 2));
    }
    private void DragRelease(Vector2 pos)
    {
        // get the distance from mouse click to ball again
        BallDistance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        // hide the line on release
        //line.positionCount = 0;  ** commented out to see if the line will persist after dragging
        // if you click and drag for a shot but release the click next the ball, return to cancel the shot
        if (BallDistance <= 1f) { return; }

        // set the power and direction of the ball based on how far you pulled back the mouse on release
        BallDirection = (Vector2)transform.position - pos;
    }

    public bool ShootBall()
    {
        // if the ball is still rolling, or the ball trajectory is cleared, dont shoot
        if (!isReady() || BallDirection == Vector2.zero) return false;
        // shoot the ball, hide the trajectory line, erase the ball trajectory (so it the user needs to specify the shot)
        body.linearVelocity = Vector2.ClampMagnitude(BallDirection * user.GetPower(), maxPower);
        line.positionCount = 0;
        //BallDirection = Vector2.zero; // clear the ball trajectory vector ( so user wont press shoot again with the previous trajectory, forcing them to enter another of they shoot) 
        return true;
    }
    private void CheckWinState()
    {
        if (inHole) return;

        if (body.linearVelocity.magnitude <= maxGoalSpeed)
        {
            inHole = true;
            body.linearVelocity = Vector2.zero;
            goalSound.time = 0.35f; // short silence at the start so i skipped it
            goalSound.Play();
            //gameObject.SetActive(false);
            // Level complete function goes here
            user.ActivateLevelFinish();
        }
    }

    // conditionals for when the ball makes contact with the hole upon the first frame and subsequent frames 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Goal") CheckWinState();
    }
    // ontriggerStay is for subsequent frames*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Goal") CheckWinState();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Wall") {
            wallSound.time = 0.2f;
            wallSound.Play(); 
        }
    }

}
