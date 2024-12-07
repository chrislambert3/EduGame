using UnityEngine;
using UnityEngine.Tilemaps;

public class Question
{
    private CoordinateMapper coordinateMapper;
    private Rigidbody2D ball;
    private Rigidbody2D goal;

    public Question()
    {
        // access tilemap and determine lower left corner
        this.coordinateMapper = new CoordinateMapper(GameObject.Find("Tilemap").GetComponent<Tilemap>());
        this.ball = GameObject.Find("Golf Ball").GetComponent<Rigidbody2D>();
        this.goal = GameObject.Find("Goal").GetComponent<Rigidbody2D>();
    }

    public string GetQuestion()
    {
        return "The ball is at " + (Vector2)this.coordinateMapper.worldPosToCoordinates(this.ball.position) + ".\n" +
               "the goal is at " + (Vector2)this.coordinateMapper.worldPosToCoordinates(this.goal.position) + ".\n" +
               "Enter the formula for a line that goes through both points.\n" +
               "Then, set the slider to a negative number to hit the ball to the left,\n" +
               "or a positive number to hit the ball to the right.";
    }
}
