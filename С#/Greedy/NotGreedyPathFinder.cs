using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy;

public record struct Route(Point Start, Point End);

public class NotGreedyPathFinder : IPathFinder
{
    public List<Point> FindPathToCompleteGoal(State state)
    {
        var paths = new Dictionary<Route, PathWithCost>();
        var bestOrder = new List<int>();
        var orders = GetRoutes(state.Chests.Count, new List<List<int>>());

        while (true)
        {
            var failedOrders = new List<List<int>>();
            orders = GetRoutes(state.Chests.Count, orders);

            foreach (var order in orders)
            {
                ExtendPathDictionary(state, paths, order);
                if (IsOrderSuccess(state, paths, order)) bestOrder = order;
                else failedOrders.Add(order);
            }

            if (orders.Any(x => x.Count > bestOrder.Count) || bestOrder.Count == state.Chests.Count) break;
            orders.RemoveAll(x => failedOrders.Contains(x));
        }
        return CollectFullPath(paths, bestOrder, state);
    }

    public static List<List<int>> GetRoutes(int destinations, List<List<int>> routes)
    {
        var routesList = new List<List<int>>();
        if (routes.Count == 0)
            for (int i = 0; i < destinations; i++)
                routesList.Add(new List<int> { i });
        else
            foreach (var route in routes)
                for (int i = 0; i < destinations; i++)
                {
                    if (route.Contains(i)) continue;
                    var newRoute = new List<int>(route) { i };
                    routesList.Add(newRoute);
                }
        return routesList;
    }

    public static void ExtendPathDictionary(State state, Dictionary<Route, PathWithCost> paths, List<int> order)
    {
        var location = state.Position;
        var chests = new List<Point>(state.Chests);

        foreach (var numberChest in order)
        {
            var route = new Route(location, chests[numberChest]);
            if (!paths.ContainsKey(route))
                paths[route] = GetPathByRoute(state, route);
            location = route.End;
        }
    }

    public static bool IsOrderSuccess(State state, Dictionary<Route, PathWithCost> paths, List<int> order)
    {
        var location = state.Position;
        var chests = new List<Point>(state.Chests);
        var currentCost = 0;
        foreach (var numberChest in order)
        {
            var route = new Route(location, chests[numberChest]);
            currentCost += paths[route].Cost;
            location = route.End;
            if (currentCost > state.Energy) return false;
        }

        return true;
    }

    public static PathWithCost GetPathByRoute(State state, Route path)
    {
        var pathFinder = new DijkstraPathFinder();
        return pathFinder.GetPathsByDijkstra(state, path.Start, new List<Point> { path.End }).FirstOrDefault();
    }

    public static List<Point> CollectFullPath(Dictionary<Route, PathWithCost> paths, List<int> order, State state)
    {
        var chests = new List<Point>(state.Chests);
        var location = state.Position;
        var result = new List<Point>();

        foreach (var number in order)
        {
            var route = new Route(location, chests[number]);
            result.AddRange(paths[route].Path.Skip(1));
            location = chests[number];
        }
        return result;
    }
}