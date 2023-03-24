using System;
using System.Collections.Generic;

namespace func_rocket;

public class LevelsTask
{
	static readonly Physics standardPhysics = new();
    static Vector GetGravityValue(Vector v, double multiplier) => v.Normalize() * multiplier * (v.Length / (Math.Pow(v.Length, 2) + 1));

    public static IEnumerable<Level> CreateLevels()
	{
        var start = new Vector(200, 500);
        var rocket = new Rocket(start, Vector.Zero, -0.5 * Math.PI);
        var target = new Vector(600, 200);
        yield return new Level("Zero", rocket, target, (size, v) => Vector.Zero, standardPhysics);
        yield return new Level("Heavy", rocket, target, (size, v) => new Vector(0, 0.9), standardPhysics);
        yield return new Level("Up", rocket, new Vector(700, 500), (size, v) => new Vector(0, -300/(size.Y - v.Y + 300)), standardPhysics);
        yield return new Level("WhiteHole", rocket, target, (size, v) => GetGravityValue(v - target, 140), standardPhysics);
        yield return new Level("BlackHole", rocket, target, (size, v) => GetGravityValue((start + target)/2 - v, 300), standardPhysics);
        yield return new Level("BlackAndWhite", rocket, target, (size, v) => 
            (GetGravityValue(v - target, 140) + GetGravityValue((start + target) / 2 - v, 300))/2, standardPhysics);
    }
}