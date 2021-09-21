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
        public float Result { get; set; }
    }

    internal class Calculator
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
            } while (!signals.Contains(digit) && expression.Count() + 1 <= index);

            return numberStr.ToString();
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

            for (char i = signals[signals.Count() - 1]; i >= 0; i--)
            {

            }

            return new CalcResult { Success = true, Result = 3 };
        }
    }
}
