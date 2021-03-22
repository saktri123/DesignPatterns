using System;
using System.Collections.Generic;
using System.Text;

namespace ExressinoCalculator
{
    interface IElement
    {
        public int Value { get; }
    }
    public class Integar : IElement
    {
        private int value;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
    }
    public class BinaryOperation : IElement
    {
        public enum OperatorType { Plus, Minus }
        public int LHS, RHS;
        public OperatorType Operator;

        public int Value
        {
            get
            {
                switch (Operator)
                {
                    case OperatorType.Plus:
                        return LHS + RHS;
                    case OperatorType.Minus:
                        return LHS - RHS;
                    default:
                        return 0;
                }
            }
        }
    }

    public class Token
    {
        public enum TokenType { Integar, Plus, Minus, Variable }

        public readonly TokenType type;
        public readonly string Text;


        public Token(TokenType type, string text)
        {
            this.type = type;
            this.Text = text;
        }
        public override string ToString()
        {
            return $"`{Text}`";
        }
    }
    class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        public int Calculate(string expression)
        {
            List<Token> tokens = Lex(expression);            
            return Parse(tokens, Variables);

        }
        private int Parse(List<Token> tokens, Dictionary<char, int> variables)
        {
            BinaryOperation binaryOperation = new BinaryOperation();
            bool hasLHS = false;
            foreach (Token token in tokens)
            {
                if (token.type == Token.TokenType.Integar && !hasLHS)
                {
                    binaryOperation.LHS = int.Parse(token.Text);
                    hasLHS = true;
                }
                else if (token.type == Token.TokenType.Integar && hasLHS)
                {
                    binaryOperation.RHS = int.Parse(token.Text);
                    binaryOperation.LHS = binaryOperation.Value;
                }
                else if (token.type == Token.TokenType.Minus)
                {
                    binaryOperation.Operator = BinaryOperation.OperatorType.Minus;
                }
                else if (token.type == Token.TokenType.Plus)
                {
                    binaryOperation.Operator = BinaryOperation.OperatorType.Plus;
                }
                else if (token.type == Token.TokenType.Variable)
                {
                    char variable;
                    if (Char.TryParse(token.Text, out variable) && Variables.ContainsKey(variable))
                    {
                        binaryOperation.RHS = variables[variable];
                        binaryOperation.LHS = binaryOperation.Value;
                    }
                    else { return 0; }
                }
                else
                {
                    return 0;
                }
            }
            return binaryOperation.LHS;
        }

        private List<Token> Lex(string expression)
        {
            List<Token> tokens = new List<Token>();
            for (int i = 0; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '+':
                        tokens.Add(new Token(Token.TokenType.Plus, "+"));
                        break;
                    case '-':
                        tokens.Add(new Token(Token.TokenType.Minus, "-"));
                        break;
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        var sb = new StringBuilder(expression[i].ToString());
                        for (int j = i + 1; j < expression.Length; j++)
                        {
                            if (char.IsDigit(expression[j]))
                            {
                                sb.Append(expression[j]);
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        tokens.Add(new Token(Token.TokenType.Integar, sb.ToString()));
                        break;
                    default:
                        var vr = new StringBuilder(expression[i].ToString());
                        for (int j = i + 1; j < expression.Length; j++)
                        {
                            if (char.IsLetter(expression[j]))
                            {
                                vr.Append(expression[j]);
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        tokens.Add(new Token(Token.TokenType.Variable, vr.ToString()));
                        break;
                }
            }
            return tokens;
        }

        static void Main(string[] args)
        {
            ExpressionProcessor processor = new ExpressionProcessor();
            processor.Variables.Add('x', 3);
            processor.Variables.Add('y', 4);
            Console.WriteLine($"'16'={processor.Calculate("16")}");
            Console.WriteLine($"'1+2+3'={ processor.Calculate("1+2+3")}");
            Console.WriteLine($"'1+2+x'={ processor.Calculate("1+2+x")}");
            Console.WriteLine($"'1+2+x-y'={ processor.Calculate("1+2+x-y")}");
            Console.WriteLine($"'10+20+x-y'={ processor.Calculate("10+20+x-y")}");
            Console.WriteLine($"'10+20+xy'={ processor.Calculate("10+20+xy")}");
            Console.ReadKey();
        }
    }
}
