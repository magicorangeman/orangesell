namespace Mazes
{
	public static class SnakeMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
			for(int i = 0; !robot.Finished; i++)
            {
				if (i % 2 != 0) MoveDown(robot, 2);
                else
                {
					if (i % 4 == 0) MoveRight(robot, width - 3);
					else MoveLeft(robot, width - 3);
				}
            }
		}

		public static void MoveRight(Robot robot, int stepCount)
		{
			for (int i = 0; i < stepCount; i++)
				robot.MoveTo(Direction.Right);
		}

		public static void MoveDown(Robot robot, int stepCount)
		{
			for (int i = 0; i < stepCount; i++)
				robot.MoveTo(Direction.Down);
		}

		public static void MoveLeft(Robot robot, int stepCount)
		{
			for (int i = 0; i < stepCount; i++)
				robot.MoveTo(Direction.Left);
		}
	}
}