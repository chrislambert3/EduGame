using UnityEngine;

// This class is responsible for generating and updating the linear equation that describes the ideal path of the ball.
public class EquationGenerator : MonoBehaviour
{
    public Rigidbody2D ball;
    public Rigidbody2D goal;
    private LinearEquation linearEquation;
    private bool shouldUpdate = false;

    // Below is for displaying the equation above the ball when it is stationary.
    // This can be removed later.
    private GameObject numberPrefab;
    private Renderer equationRenderer;
    private TextMesh textMesh;


    // Set initial equation on creation
    void Start()
    {
        ball = GameObject.Find("Golf Ball").GetComponent<Rigidbody2D>();
        goal = GameObject.Find("Goal").GetComponent<Rigidbody2D>();
        // Set initial equation
        UpdateEquation();

        // Below is for displaying the equation above the ball when it is stationary.
        // This can be removed later.
        this.numberPrefab = Resources.Load<GameObject>("Prefabs/NumberPrefab");
        GameObject numberObject = Instantiate(numberPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        this.equationRenderer = numberObject.GetComponent<Renderer>();
        if (this.equationRenderer != null)
        {
            this.equationRenderer.sortingLayerName = "Grid Numbers";
            this.equationRenderer.sortingOrder = 1;
        }

        this.textMesh = numberObject.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            this.textMesh.text = "";
            this.textMesh.color = Color.white;
        }

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

            // Below is for displaying the equation above the ball when it is stationary.
            // This can be removed later.
            this.equationRenderer.transform.position = new Vector3(ball.position.x, ball.position.y + 1, 0);
            this.textMesh.text = $"({linearEquation.getSlopeInterceptString()})";
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
