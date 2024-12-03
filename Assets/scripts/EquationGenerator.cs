using UnityEngine;

// This class is responsible for generating and updating the linear equation that describes the ideal path of the ball.
public class EquationGenerator : MonoBehaviour
{
    public Rigidbody2D ball;
    public Rigidbody2D goal;
    private LinearEquation linearEquation;
    private bool shouldUpdate = false;

    private GameObject numberPrefab; // delete this line
    private Renderer renderer;
    private TextMesh textMesh;


    // Set initial equation on creation
    void Start()
    {
        UpdateEquation();

        // delete below
        this.numberPrefab = Resources.Load<GameObject>("Prefabs/NumberPrefab");
        GameObject numberObject = Instantiate(numberPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        this.renderer = numberObject.GetComponent<Renderer>();
        if (this.renderer != null)
        {
            this.renderer.sortingLayerName = "Grid Numbers";
            this.renderer.sortingOrder = 1;
        }

        this.textMesh = numberObject.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            this.textMesh.text = "";
            this.textMesh.color = Color.white;
        }
    }

    // Maintain the equation
    void Update()
    {
        // Update the equation once each time the ball comes to a stop.
        if (ball.linearVelocity.magnitude == 0 && 
            this.shouldUpdate) {
            this.shouldUpdate = false;
            UpdateEquation();
            this.renderer.transform.position = new Vector3(ball.position.x, ball.position.y + 1, 0);
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
