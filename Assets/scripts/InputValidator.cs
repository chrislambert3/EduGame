﻿using System;
using System.Text.RegularExpressions;

public class InputValidator
{
    private string slopeInterceptPattern = @"^y\s*=\s*[+-]?\s*\d+\.*\d*\s*x\s*[+-]?\s*\d+\.*\d*$";
    private string pointSlopePattern = @"^y\s*-\s*\d+\.*\d*\s*=\s*[+-]?\s*\d+\s*\(\s*x\s*-\s*\d+\.*\d*\s*\)$";
    private string standardFormPattern = @"^\d+\.*\d*\s*x\s*\+\s*\d+\.*\d*\s*y\s*=\s*\d+\.*\d*$";

    // implied constructor

    public bool validateSlopeIntercept(string equation)
    {
        return Regex.IsMatch(equation, slopeInterceptPattern);
    }

    public bool validatePointSlope(string equation)
    {
        return Regex.IsMatch(equation, pointSlopePattern);
    }

    public bool validateStandardForm(string equation)
    {
        return Regex.IsMatch(equation, standardFormPattern);
    }
}
