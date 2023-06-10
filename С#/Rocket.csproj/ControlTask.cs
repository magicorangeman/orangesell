using System;

namespace func_rocket;

public class ControlTask
{
    public static Turn ControlRocket(Rocket rocket, Vector target)
    {
        var direction = target - rocket.Location;
        if (direction.Angle > (0.33 * rocket.Direction + 0.66 * rocket.Velocity.Angle))
            return Turn.Right;
        else if (direction.Angle < (0.33 * rocket.Direction + 0.66 * rocket.Velocity.Angle)) return Turn.Left;
        else return Turn.None;
    }
}