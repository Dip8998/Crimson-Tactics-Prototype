using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    // Node class representing a position in the grid for A* pathfinding
    public class Node
    {
        public Vector2Int Position;  // Position of the node in grid coordinates
        public Node Parent;  // Parent node in the path
        public int G;  // Cost from start to current node
        public int H;  // Heuristic cost from current node to target
        public int F => G + H;  // Total cost F = G + H

        // Constructor to initialize a node with position, parent, G and H costs
        public Node(Vector2Int position, Node parent, int g, int h)
        {
            Position = position;
            Parent = parent;
            G = g;
            H = h;
        }
    }

    // A* pathfinding method to find a path from start to target position
    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int target, ObstacleData obstacleData, int gridSize)
    {
        List<Node> openList = new List<Node>();  // List of nodes to be evaluated
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();  // Set of positions already evaluated

        // Create the starting node with initial costs
        Node startNode = new Node(start, null, 0, GetHeuristic(start, target));
        openList.Add(startNode);  // Add the starting node to the open list

        // Loop until all possible nodes have been evaluated
        while (openList.Count > 0)
        {
            Node currentNode = openList[0];  // Get the node with the lowest F cost
            for (int i = 1; i < openList.Count; i++)
            {
                // Find the node with the lowest F cost or lowest H cost if F costs are equal
                if (openList[i].F < currentNode.F || (openList[i].F == currentNode.F && openList[i].H < currentNode.H))
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);  // Remove the current node from the open list
            closedSet.Add(currentNode.Position);  // Add the current node's position to the closed set

            // If the current node is the target, return the path
            if (currentNode.Position == target)
            {
                return RetracePath(startNode, currentNode);
            }

            // Iterate through the neighbors of the current node
            foreach (Vector2Int neighbour in GetNeighbours(currentNode.Position, gridSize))
            {
                // Skip if the neighbor is already evaluated or blocked by an obstacle
                if (closedSet.Contains(neighbour) || ObstacleManager.IsTileBlocked(neighbour))
                {
                    continue;
                }

                int newG = currentNode.G + 1;  // Calculate the new G cost from the current node
                Node neighbourNode = new Node(neighbour, currentNode, newG, GetHeuristic(neighbour, target));

                Node openNode = openList.Find(n => n.Position == neighbour);  // Find the neighbor node in the open list
                if (openNode == null)
                {
                    openList.Add(neighbourNode);  // Add the neighbor node to the open list if not already present
                }
                else if (newG < openNode.G)
                {
                    openNode.Parent = currentNode;  // Update the parent and G cost if a better path is found
                    openNode.G = newG;
                }
            }
        }

        return null;  // Return null if no path found
    }

    // Method to retrace the path from end node to start node
    static List<Vector2Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        // Traverse from end node to start node and add positions to the path list
        while (currentNode != startNode)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }

        path.Reverse();  // Reverse the path to get it from start to end
        return path;
    }

    // Method to calculate the heuristic (Manhattan distance) between two grid positions
    static int GetHeuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    // Method to get valid neighboring positions in a grid based on current position and grid size
    static List<Vector2Int> GetNeighbours(Vector2Int position, int gridSize)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        // Check neighboring positions (left, right, down, up) and add valid positions to the list
        if (position.x - 1 >= 0) neighbours.Add(new Vector2Int(position.x - 1, position.y));
        if (position.x + 1 < gridSize) neighbours.Add(new Vector2Int(position.x + 1, position.y));
        if (position.y - 1 >= 0) neighbours.Add(new Vector2Int(position.x, position.y - 1));
        if (position.y + 1 < gridSize) neighbours.Add(new Vector2Int(position.x, position.y + 1));

        return neighbours;  // Return the list of valid neighboring positions
    }
}
