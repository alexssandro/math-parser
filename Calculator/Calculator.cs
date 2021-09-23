using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class CalcResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public double Result { get; set; }
    }

    public class Calculator
    {
        public static char[] signals = { '+', '-', '*', '/' };

        public static string GetNumber(string expression)
        {
            char digit;
            int index = 0;
            StringBuilder numberStr = new StringBuilder();

            do
            {
                digit = expression[index];
                index++;
                if (!signals.Contains(digit))
                    numberStr.Append(digit);
            } while (!signals.Contains(digit) && index <= expression.Count() - 1);

            return numberStr.ToString();
        }

        public static bool CheckParenthesisOpening(string expressionWithParentheses)
        {
            int amountOpening = 0;

            foreach (char item in expressionWithParentheses)
            {
                if (item == '(')
                    amountOpening++;
                if (item == ')')
                    amountOpening--;

                if (amountOpening < 0)
                    return false;
            }

            return true;
        }

        public static CalcResult CalcWithParentheses(string expressionWithParentheses)
        {
            if (!expressionWithParentheses.Contains('(') && expressionWithParentheses.Contains(')'))
                return Calc(expressionWithParentheses);

            if (expressionWithParentheses.Count(c => c == '(') != expressionWithParentheses.Count(c => c == ')'))
                return new CalcResult { Success = false, Error = "the number of parentheses opening must be the same as parentheses closing" };

            if (!CheckParenthesisOpening(expressionWithParentheses))
                return new CalcResult { Success = false, Error = "for every parentheses opening must be an after parentheses closing" };

            int lastOpeningIndex = expressionWithParentheses.LastIndexOf('(');
            int firstClosingAfterLastOpeningIndex = expressionWithParentheses[lastOpeningIndex..]
                                                                        .IndexOf(')') + lastOpeningIndex;

            string expressionWithoutParenthesis = expressionWithParentheses.Substring(lastOpeningIndex, firstClosingAfterLastOpeningIndex);
            expressionWithoutParenthesis = expressionWithoutParenthesis.Replace(")", "").Replace("(", "");

            return null;
        }

        public static CalcResult Calc(string expression)
        {
            if (!expression.Any(e => signals.Contains(e)))
                return new CalcResult { Success = false, Error = "There are no signals in expression" };

            string expressionTMP = expression;
            bool numberTurn = true;
            string numberTMP;
            List<object> items = new List<object>();

            do
            {
                if (numberTurn)
                {
                    numberTMP = GetNumber(expressionTMP);

                    if (!Regex.IsMatch(numberTMP, @"\d"))
                        return new CalcResult { Success = false, Error = "syntax error, between signal, must have a number" };

                    items.Add(Convert.ToInt32(numberTMP));

                    expressionTMP = expressionTMP.Remove(0, numberTMP.Length);
                    numberTurn = false;
                }
                else
                {
                    char signalTMP = expressionTMP[0];
                    if (!signals.Contains(signalTMP))
                        return new CalcResult { Success = false, Error = "syntax error, between number, must have a signal" };

                    items.Add(signalTMP);
                    expressionTMP = expressionTMP.Remove(0, 1);
                    numberTurn = true;
                }
            } while (!string.IsNullOrEmpty(expressionTMP));

            if (signals.Contains(Convert.ToChar(items.Last())))
                return new CalcResult { Success = false, Error = "syntax error, the last digit must be a number" };

            double value = 0;

            try
            {
                value = CalcRecursively(items);
            }
            catch (ArgumentException ex)
            {
                if (ex.ParamName == "dividebyzero")
                    return new CalcResult { Success = false, Error = ex.Message.Replace(" (Parameter 'dividebyzero')", "") };
            }

            return new CalcResult { Success = true, Result = value };
        }

        public static double CalcRecursively(List<object> items)
        {
            if (items.Count == 1)
                return Convert.ToDouble(items[0]);

            double value = 0;

            for (int i = signals.Count() - 1; i >= 0; i--)
            {
                for (int j = 0; j < items.Count(); j++)
                {
                    if (items[j].GetType() != typeof(double) && Convert.ToChar(items[j]) == signals[i])
                    {
                        if (signals[i] == '/' && items[j + 1].Equals(0))
                            throw new ArgumentException("Syntax error, division by zero is not possible", "dividebyzero");

                        double numberBefore = Convert.ToDouble(items[j - 1]);
                        double numberAfter = Convert.ToDouble(items[j + 1]);

                        switch (signals[i])
                        {
                            case '/':
                                value = numberBefore / numberAfter;
                                break;
                            case '*':
                                value = numberBefore * numberAfter;
                                break;
                            case '+':
                                value = numberBefore + numberAfter;
                                break;
                            case '-':
                                value = numberBefore - numberAfter;
                                break;
                        }

                        items[j - 1] = value;
                        items.RemoveRange(j, 2);
                        return CalcRecursively(items);
                    }
                }
            }
            return value;
        }
    }
}
