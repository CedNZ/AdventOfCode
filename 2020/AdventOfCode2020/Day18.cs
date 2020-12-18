using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day18 : IDay<string>
    {
        public List<string> SetupInputs(string[] inputs)
        {
            return inputs.ToList();
        }

        public long A(List<string> inputs)
        {
			long sum = 0;
			foreach(var input in inputs)
			{
				var expression = input;
				while(expression.Contains("("))
				{
					var closingIndex = expression.IndexOf(')');
					var openingIndex = expression.LastIndexOf('(', closingIndex);

					var insideBrackets = expression.Substring(openingIndex + 1, (closingIndex - openingIndex - 1));

					var num = Evaluate(insideBrackets);

					expression = $"{expression.Substring(0, openingIndex)}{num}{expression.Substring(closingIndex + 1)}";
				}
				sum += Evaluate(expression);
			}
			return sum;
		}

		public long Evaluate(string input)
		{
			long? a = null;
			long? b = null;
			string op = "";
			foreach(var part in input.Split(' '))
			{
				if(long.TryParse(part, out var num))
				{
					if(a == null)
					{
						a = num;
					}
					else if(b == null)
					{
						b = num;

						a = op switch
						{
							"+" => a + b,
							"*" => a * b,
							_ => a,
						};

						b = null;
					}
				}
				else
				{
					op = part;
				}
			}
			return a.Value;
		}

		public long B(List<string> inputs)
        {
			return -1;
        }
    }
}
