using System;
public class LinearEquation
{
    // distance from ball to goal
    float distance;

    // internal storage of the equation in y=mx+b form
    float m;
    float b;

    // constructor
    public LinearEquation(float x1, float y1, float x2, float y2)
    {
        this.m = (y2 - y1) / (x2 - x1);
        this.b = y1 - this.m * x1;
        this.distance = CalculateDistance(x1, y1, x2, y2);
    }

    float CalculateDistance(float x1, float y1, float x2, float y2)
    {
        return (float)Math.Sqrt(Math.Pow(x2 - x1, 2) + 
                                Math.Pow(y2 - y1, 2));
    }

    public string getSlopeInterceptString()
    {
        return "y = " + Math.Round(this.m, 2) + "x + " + Math.Round(this.b, 2);
    }

    public string getPointSlopeString(float x, float y)
    {
        return "y - " + y + " = " + Math.Round(this.m, 2) + "(x - " + x + ")";
    }

    public string getStandardFormString()
    {
        return Math.Round(this.m, 2) + "x - y + " + Math.Round(this.b, 2) + " = 0";
    }
}