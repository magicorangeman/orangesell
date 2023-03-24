using System;

namespace func_rocket;

public class ControlTask
{
	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
		var directionTarget = target - rocket.Location;
		var flyingDirection = 0.33 * rocket.Direction + 0.66 * rocket.Velocity.Angle;
		if (directionTarget.Angle < flyingDirection)
			return Turn.Right;
		else if (directionTarget.Angle > flyingDirection)
			return Turn.Left;
		return Turn.None;
	}
}