using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    /// Headers that will show up in the script inspector field
    [Header("References")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private LineRenderer line;
    [Header("Attributes")]
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    //[SerializeField] private float maxGoalSpeed = 4f;

    private bool isDragging;
    private bool inHole;

    private void Update()
    {
        PlayerInput();
    }
    // this returns if the ball is moving "slow enough" to hit again
    private bool isReady()
    {
        return body.linearVelocity.magnitude <= 0.2f; 
    }

    private void PlayerInput()
    {   // if the ball is still rolling, return to ignore
        if (!isReady()) return;
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
        Vector2 direction = (Vector2)transform.position - pos;

        // matches the line to the trajectory of the shot
        // the parameters divided by 2 to make the line shorter than the actual shot so it wont project far
        // *** Im thinking we make this a difficulty mode where the projected trajectory is longer when easier/ shorter when harder
        line.SetPosition(0, transform.position);
        line.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((direction * power) / 2, maxPower / 2) );
    }
    private void DragRelease(Vector2 pos) {
        // get the distance from mouse click to ball again
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        // hide the line on release
        line.positionCount = 0;
        // if you click and drag for a shot but release the click next the ball, return to cancel the shot
        if (distance <= 1f) { return; }

        // set the power and direction of the ball based on how far you pulled back teh mouse on release
        Vector2 direction = (Vector2)transform.position - pos;
        body.linearVelocity = Vector2.ClampMagnitude(direction * power, maxPower);

    }

}
