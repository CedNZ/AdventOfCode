﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCodeCore;

namespace AoC_2021
{
    public class Day12 : IDay<string>
    {
        public long A(List<string> inputs)
        {
            Dictionary<string, Cave> caves = new ();

            foreach (var line in inputs)
            {
                var parts = line.Split('-');
                if (!caves.TryGetValue(parts[0], out var cave1))
                {
                    cave1 = new Cave(parts[0]);
                    caves.Add(cave1.Name, cave1);
                }
                if (!caves.TryGetValue(parts[1], out var cave2))
                {
                    cave2 = new Cave(parts[1]);
                    caves.Add(cave2.Name, cave2);
                }

                cave1.AddConnection(cave2);
                cave2.AddConnection(cave1);
            }

            var start = caves["start"];
            var end = caves["end"];

            List<Cave> visited = new();


            return FindPath(start, visited);
        }

        public long FindPath(Cave from, List<Cave> visited)
        {
            if (!visited.Contains(from))
            {
                visited.Add(from);
            }

            var toVisit = from.Connections.Except(visited.Where(x => !x.BigCave));
            if (!toVisit.Any())
            {
                return 0;
            }

            return toVisit.Sum(cave =>
            {
                if (cave.Name == "end")
                {
                    return 1;
                }
                return FindPath(cave, visited.ToList());
            });
        }

        public long B(List<string> inputs)
        {
            return default;
        }

        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }
    }

    public class Cave
    {
        public string Name;
        public bool BigCave;
        public List<Cave> Connections;

        public Cave(string name)
        {
            Connections = new();
            Name = name;
            if (char.IsUpper(Name[0]))
            {
                BigCave = true;
            }
        }

        public void AddConnection(Cave other)
        {
            Connections.Add(other);
        }
    }
}
