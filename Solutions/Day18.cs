using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutions
{
    public class Day18 : Day
    {
        public Day18(string input) : base(input)
        {
        }

        public override string Part1()
        {
            var expressions = input.Split(Environment.NewLine);
            var result = 0L;

            bool HasPrecedence(char op1, char op2)
            {
                if (op2 == '(' || op2 == ')')
                {
                    return false;
                }
                if ((op1 == '*' || op1 == '+'))
                {
                    return true;
                }
                return true;
            }

            foreach (var expression in expressions)
            {
                result += Evaluate(expression, HasPrecedence);
            }

            return result.ToString();
        }

        private long Evaluate(string expression, Func<char, char, bool> hasPrecedence)
        {
            long Apply(char op, long arg1, long arg2)
            {
                switch (op)
                {
                    case '+':
                        return arg1 + arg2;
                    case '*':
                        return arg1 * arg2;
                    default:
                        throw new NotSupportedException();
                }

            }

            var tokens = expression.ToArray();
            var opStack = new Stack<char>();
            var values = new Stack<long>();
            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                switch (token)
                {
                    case ' ':
                        break;
                    case '+':
                    case '*':
                        while (opStack.Count > 0 &&
                               hasPrecedence(token,
                                   opStack.Peek()))
                        {
                            values.Push(Apply(opStack.Pop(),
                                values.Pop(),
                                values.Pop()));
                        }

                        opStack.Push(token);
                        break;
                    case '(':
                        opStack.Push(token);
                        break;
                    case ')':
                        while (opStack.Peek() != '(')
                        {
                            values.Push(Apply(opStack.Pop(),
                                values.Pop(),
                                values.Pop()));
                        }
                        opStack.Pop();
                        break;
                    default:
                        StringBuilder builder = new StringBuilder();
                        while (i < tokens.Length &&
                               tokens[i] >= '0' &&
                               tokens[i] <= '9')
                        {
                            builder.Append(tokens[i++]);
                        }
                        values.Push(int.Parse(builder.ToString()));
                        i--;
                        break;
                }
            }

            while (opStack.Count > 0)
            {
                values.Push(Apply(opStack.Pop(),
                    values.Pop(),
                    values.Pop()));
            }

            return  values.Pop();
        }

        public override string Part2()
        {
            var expressions = input.Split(Environment.NewLine);
            var result = 0L;

            bool HasPrecedence(char op1, char op2)
            {
                if (op2 == '(' || op2 == ')')
                {
                    return false;
                }

                if (op2 == '*' && op1 == '+')
                {
                    return false;
                }

                return true;
            }

            foreach (var expression in expressions)
            {
                result += Evaluate(expression, HasPrecedence);
            }

            return result.ToString();
        }
    }
}
