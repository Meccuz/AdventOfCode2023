using System.Text.RegularExpressions;

namespace AdventOfCode2023.App
{
    internal static class Day19
    {
        private static string[] input = File.ReadAllLines("Inputs/19.txt");
        private static List<Workflow> workflows = new();
        private static List<Part> parts = new();

        public static void Run()
        {
            ParseWorkflowsAndParts();
            var res1 = ProcessParts();

            Console.WriteLine($"Part 1: {res1}");

            Console.WriteLine($"Part 2: {""}");
            Console.ReadLine();
        }

        private static int ProcessParts()
        {
            var tot = 0;
            foreach (var part in parts)
            {
                var workflowName = "in";
                var workflow = workflows.First(x => x.Name == workflowName);
                while (true)
                {
                    if (workflowName == "A")
                    {
                        tot += (part.Categories['x'] + part.Categories['m'] + part.Categories['a'] + part.Categories['s']);
                        break;
                    }
                    if (workflowName == "R")
                    {
                        break;
                    }
                    workflow = workflows.First(x => x.Name == workflowName);
                    foreach (var step in workflow.Steps)
                    {
                        if (!step.Category.HasValue)
                        {
                            workflowName = step.DestinationWorkflow;
                            break;
                        }
                        else
                        {
                            var partVal = part.Categories[step.Category.Value];
                            if ((step.Operator == '>' && partVal > step.Value) || (step.Operator == '<' && partVal < step.Value))
                            {
                                workflowName = step.DestinationWorkflow;
                                break;
                            }
                        }
                    }
                }
            }
            return tot;
        }

        private static void ParseWorkflowsAndParts()
        {
            var parseWorkflows = true;
            foreach (string line in input)
            {
                if (line == string.Empty)
                {
                    parseWorkflows = false;
                    continue;
                }
                if (parseWorkflows)
                {
                    var match = Regex.Match(line, @"(\w+)\{(.*)\}");
                    var wf = new Workflow { Name = match.Groups[1].Value };
                    foreach (var instruction in match.Groups[2].Value.Split(','))
                    {
                        var match2 = Regex.Match(instruction, @"(\w)([<>])(\d+):(\w+)");
                        if (match2.Success)
                        {
                            wf.Steps.Add(new WorkflowStep
                            {
                                Category = char.Parse(match2.Groups[1].Value),
                                Operator = char.Parse(match2.Groups[2].Value),
                                Value = int.Parse(match2.Groups[3].Value),
                                DestinationWorkflow = match2.Groups[4].Value
                            });
                        }
                        else
                        {
                            wf.Steps.Add(new WorkflowStep
                            {
                                DestinationWorkflow = instruction
                            });
                        }
                    }
                    workflows.Add(wf);
                }
                else
                {
                    var categories = line.Replace("{", string.Empty).Replace("}", string.Empty).Split(",");
                    var part = new Part();
                    foreach (var category in categories)
                    {
                        var match = Regex.Match(category, @"(\w)=(\d+)");
                        part.Categories.Add(char.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                    }
                    parts.Add(part);
                }
            }
        }

        public class Workflow
        {
            public string Name { get; set; }
            public List<WorkflowStep> Steps { get; } = new();
        }

        public class WorkflowStep
        {
            public char? Category { get; set; }
            public char? Operator { get; set; }
            public int? Value { get; set; }
            public string DestinationWorkflow { get; set; }
        }

        public class Part
        {
            public Dictionary<char, int> Categories { get; } = new();
        }
    }
}
