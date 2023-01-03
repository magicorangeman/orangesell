namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    public class Player : ICreature
    {
        public CreatureCommand Move()
        {
            var moveUp = new CreatureCommand { DeltaX = 0, DeltaY = -1 };
            var moveDown = new CreatureCommand { DeltaX = 0, DeltaY = 1 };
            var moveLeft = new CreatureCommand { DeltaX = -1, DeltaY = 0 };
            var moveRight = new CreatureCommand { DeltaX = 1, DeltaY = 0 };

            switch (Game.KeyPressed)
            {
                case System.Windows.Forms.Keys.Up:
                    return moveUp;
                case System.Windows.Forms.Keys.Down:
                    return moveDown;
                case System.Windows.Forms.Keys.Left:
                    return moveLeft;
                case System.Windows.Forms.Keys.Right:
                    return moveRight;
            }

            return new CreatureCommand();
        }

        public CreatureCommand Act(int x, int y)
        {
            var command = Move();
            bool isEndMap = (x + command.DeltaX < 0 || x + command.DeltaX >= Game.MapWidth || y + command.DeltaY < 0 ||
                   y + command.DeltaY >= Game.MapHeight);
            if (isEndMap || Game.Map[x + command.DeltaX, y + command.DeltaY] is Sack)
                return new CreatureCommand();
            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Sack || conflictedObject is Monster)
                Game.IsOver = true;
            return conflictedObject is Sack || conflictedObject is Monster;
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }

    public class Sack : ICreature
    {
        public int Counter = 0;
        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                var map = Game.Map[x, y + 1];
                if (map == null || (Counter > 0 && (map is Player || map is Monster)))
                {
                    Counter++;
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                }
            }
            if (Counter > 1)
            {
                Counter = 0;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            Counter = 0;
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return -1;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }

    public class Gold : ICreature
    {
        public void AddScore()
        {
            Game.Scores += 10;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            bool isDeadByPlayer = conflictedObject is Player;
            if (isDeadByPlayer) AddScore();
            return isDeadByPlayer || (conflictedObject is Monster);
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {
        public CreatureCommand MoveToPlayer(int x, int y)
        {
            int playerX = -1;
            int playerY = -1;
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                    if (Game.Map[i, j] is Player)
                    {
                        playerX = i;
                        playerY = j;
                        break;
                    }
            if (playerX < 0 || playerY < 0) return new CreatureCommand();
            var command = CloserToPlayer(x, y, playerX, playerY);
            
            bool isBorder = (x + command.DeltaX < 0 || x + command.DeltaX >= Game.MapWidth || y + command.DeltaY < 0 ||
y + command.DeltaY >= Game.MapHeight);
            if (isBorder)
                return new CreatureCommand();
            var locateToGo = Game.Map[x + command.DeltaX, y + command.DeltaY];
            if (locateToGo is Sack || locateToGo is Monster || locateToGo is Terrain)
                return new CreatureCommand();
            return command;
        }

        public CreatureCommand CloserToPlayer(int x, int y, int playerX, int playerY)
        {
            var command = new CreatureCommand();
            if (playerX < x) command.DeltaX = -1;
            else if (playerX > x) command.DeltaX = 1;
            else
            {
                if (playerY < y) command.DeltaY = -1;
                else if (playerY > y) command.DeltaY = 1;
            }
            return command;
        }

        public CreatureCommand Act(int x, int y)
        {
            return MoveToPlayer(x, y);
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Monster || conflictedObject is Sack;
        }

        public int GetDrawingPriority()
        {
            return -1;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }
}