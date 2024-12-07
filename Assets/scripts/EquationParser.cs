using System;
using UnityEngine;
using TMPro;

public class EquationParser : MonoBehaviour
{
    // Input validator
    private InputValidator inputValidator;
    private TMP_InputField inputField;
    private Ball ball;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.inputValidator = new InputValidator();
        this.ball = GameObject.Find("Golf Ball").GetComponent<Ball>();
        if (inputField == null)
        {
            inputField = GameObject.Find("Input").GetComponent<TMP_InputField>();
        }
        inputField.onValueChanged.AddListener(parseEquation);

    }

    // Update is called once per frame
    void Update()
    {
        // if input changed
        // ingest string
    }

    private void parseEquation(string equation)
    {
        if (!inputValidator.validateSlopeIntercept(equation))
        {
            // Fail
            return;
        }
        // break down string
        string[] tokens = equation.Split(new string[] { "y=", "x" }, StringSplitOptions.RemoveEmptyEntries);
        float m = float.Parse(tokens[0].Trim());

        // compute x and y velocity normalized so that x^2 + y^2 = 1
        Vector2 velocity = new Vector2();
        velocity.y = m;
        float totalVelocity = (float)Math.Sqrt(1 + (velocity.y * velocity.y)) ;
        velocity.x = 1 / totalVelocity;
        velocity.y = velocity.y / totalVelocity;

        Debug.Log($"x={velocity.x} y={velocity.y}");

        ball.setDirection(velocity);
    }

}
