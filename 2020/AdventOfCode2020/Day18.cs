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
			return Run(inputs, true);
		}

		public long Run(List<string> inputs, bool A)
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

					var num = A ? Evaluate(insideBrackets) : EvaluateB(insideBrackets);

					expression = $"{expression.Substring(0, openingIndex)}{num}{expression.Substring(closingIndex + 1)}";
				}
				sum += A ? Evaluate(expression) : EvaluateB(expression);
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
			return Run(inputs, false);
		}

		public long EvaluateB(string input)
		{
			var parts = input.Split(' ');

			var digits = parts.Where(x => int.TryParse(x, out int _)).Select(long.Parse).ToList();
			var symbols = parts.Where(x => !int.TryParse(x, out int _)).ToList();

			while(symbols.Contains("+"))
			{
				var index = symbols.IndexOf("+");
				digits[index] = digits[index] + digits[index + 1];
				digits.RemoveAt(index + 1);
				symbols.RemoveAt(index);
			}

			return digits.Aggregate((curr, next) => curr * next);
		}
	}
}
