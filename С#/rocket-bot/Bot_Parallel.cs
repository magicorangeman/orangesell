using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot;

public partial class Bot
{
	public Rocket GetNextMove(Rocket rocket)
	{
		var (moveOne, scoreOne) = GetNextMoveFromThread(rocket).Result;
        var (moveTwo, scoreTwo) = GetNextMoveFromThread(rocket).Result;
		if (scoreOne > scoreTwo) return rocket.Move(moveOne, level);
        else return rocket.Move(moveTwo, level);
	}

	async public Task<(Turn Turn, double Score)> GetNextMoveFromThread(Rocket rocket) =>
		await CreateTasksAsync(rocket).First();

    public List<Task<(Turn Turn, double Score)>> CreateTasksAsync(Rocket rocket) =>
		new() { Task.Run(() => SearchBestMove(rocket, new Random(random.Next()), iterationsCount/threadsCount)) };

    public List<Task<(Turn Turn, double Score)>> CreateTasks(Rocket rocket) =>
		new() { Task.Run(() => SearchBestMove(rocket, new Random(random.Next()), iterationsCount)) };
}