using System;
using System.Collections.Generic;
using System.Linq;

public class Table
{
    public string Name { get; set; }
    public List<string> Parents { get; set; }

    public Table(string name, List<string> parents)
    {
        Name = name;
        Parents = parents;
    }
}

public class TableSorter
{
    public static List<string> SortTables(List<Table> tables)
    {
        Dictionary<string, List<string>> graph = new();
        Dictionary<string, int> indegree = new();
        HashSet<string> allTables = new();

        foreach (var table in tables)
        {
            string tableName = table.Name;
            allTables.Add(tableName);

            if (!graph.ContainsKey(tableName))
                graph[tableName] = new List<string>();

            foreach (var parent in table.Parents)
            {
                if (!graph.ContainsKey(parent))
                    graph[parent] = new List<string>();

                graph[parent].Add(tableName);

                indegree[tableName] = indegree.ContainsKey(tableName) ? indegree[tableName] + 1 : 1; 
                allTables.Add(parent);
            }
        }

        // Initialize queue with nodes having indegree 0
        Queue<string> queue = new(
            allTables.Where(t => !indegree.ContainsKey(t))
        );

        List<string> sortedList = new();

        // Process the queue
        while (queue.Count > 0)
        {
            string current = queue.Dequeue();
            sortedList.Add(current);

            if (!graph.ContainsKey(current))
                continue;

            foreach (var child in graph[current])
            {
                indegree[child]--;

                if (indegree[child] == 0)
                    queue.Enqueue(child); 
            }
        }

        // Check for cycles
        if (sortedList.Count != allTables.Count)
            throw new InvalidOperationException("Cycle Detected");

        return sortedList;
    }
}
