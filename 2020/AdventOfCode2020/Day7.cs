using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day7 : IDay<Bag>
    {
        public List<Bag> SetupInputs(string[] inputs)
        {
            List<Bag> AllBags = new List<Bag>();

            foreach(var input in inputs)
            {
                var parts = input.Split(" contain ");
                var parentName = parts[0].Replace(" bags", "");
                var children = parts[1].Split(',');

                var parentBag = AllBags.FirstOrDefault(b => b.Name == parentName) ?? new Bag(parentName);

                foreach(var child in children)
                {
                    if (child.Contains("no other"))
                    {
                        break;
                    }

                    var match = Regex.Match(child, @"(\d+) (\w+ \w+).+");

                    var childCount = int.Parse(match.Groups[1].Value);
                    var childName = match.Groups[2].Value;

                    var childBag = AllBags.FirstOrDefault(b => b.Name == childName) ?? new Bag(childName);

                    parentBag.AddChild(childBag, childCount);

                    if (!AllBags.Contains(childBag))
                    {
                        AllBags.Add(childBag);
                    }
                }

                if(!AllBags.Contains(parentBag))
                {
                    AllBags.Add(parentBag);
                }
            }

            return AllBags;
        }

        public long A(List<Bag> inputs)
        {
            var shinyGold = inputs.First(x => x.Name == "shiny gold");

            return inputs.Count(x => RecursiveBagSearch(x.Children.Keys.ToList(), shinyGold));
        }

        public bool RecursiveBagSearch(IEnumerable<Bag> bags, Bag toFind)
        {
            if (bags.Contains(toFind))
            {
                return true;
            }
            return bags.Any(b => RecursiveBagSearch(b.Children.Keys.ToList(), toFind));
        }

        public long B(List<Bag> inputs)
        {
            var shinyGold = inputs.First(x => x.Name == "shiny gold");

            return RecursiveBagCount(shinyGold) - 1;
        }

        public int RecursiveBagCount(Bag bag)
        {
            return 1 + (bag.Children.Sum(c => RecursiveBagCount(c.Key) * c.Value));
        }

    }

    public class Bag
    {
        public string Name;
        public Dictionary<Bag, int> Children;

        public Bag(string name)
        {
            Name = name;
            Children = new Dictionary<Bag, int>();
        }

        public void AddChild(Bag child, int count)
        {
            Children.Add(child, count);
        }

        public override string ToString()
        {
            return $"{Name}, C:{Children.Count()}";
        }
    }
}
