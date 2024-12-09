using UnityEngine;

// This class is responsible for generating and updating the linear equation that describes the ideal path of the ball.
public class EquationGenerator : MonoBehaviour
{
    public Rigidbody2D ball;
    public Rigidbody2D goal;
    private LinearEquation linearEquation;
    private bool shouldUpdate = false;

    // Set initial equation on creation
    void Start()
    {
        ball = GameObject.Find("Golf Ball").GetComponent<Rigidbody2D>();
        goal = GameObject.Find("Goal").GetComponent<Rigidbody2D>();
        // Set initial equation
        UpdateEquation();

        Debug.Log($"Equation: {linearEquation.getSlopeInterceptString()}");
    }

    // Maintain the equation
    void Update()
    {
        // Update the equation once each time the ball comes to a stop.
        if (ball.linearVelocity.magnitude == 0 && 
            this.shouldUpdate) {
            this.shouldUpdate = false;
            UpdateEquation();

        }
        // If the ball is moving and the equation is not up to date, set the flag to update the equation.
        else if (ball.linearVelocity.magnitude != 0 &&
                   !this.shouldUpdate) {
            this.shouldUpdate = true;
        }
    }

    void UpdateEquation()
    {
        this.linearEquation = new LinearEquation(ball.position.x, ball.position.y, 
                                                 goal.position.x, goal.position.y);
    }
}
