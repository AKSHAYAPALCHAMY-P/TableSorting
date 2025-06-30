namespace Sorting_without_Algorithm
{
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

    public class TableSorting
    {
        public static List<string> SortingTable(List<Table> tables)
        {
            var Result = new List<string>();
            int[] Indegree = new int[tables.Count];

            for (int i = 0; i<tables.Count; i++)
            {
                Indegree[i] = tables[i].Parents.Count;
            }

            while (true)
            {
                bool Process = false;
                for (int i = 0; i < tables.Count; i++)
                {
                    if (Indegree[i] == 0)
                    {
                        Result.Add(tables[i].Name);
                        Indegree[i] = -1;
                        Process = true;
                    }

                    for (int j = 0; j<tables.Count; i++)
                    {
                        if (tables[j].Parents.Contains(tables[i].Name))
                        {
                            Indegree[j]--;
                        }
                    }
                }
                if (!Process) break;
            }

            if (Result.Count != tables.Count)
            {
                throw new InvalidOperationException("Cycle detected");
            }

            return Result;
        }
    }
}
