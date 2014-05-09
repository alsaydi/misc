//euler 93
using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static int counter = 0;
    static void Main()
    {
        var s = "123456789";
        int n = 4;
        Dictionary<object, int> diHT = new Dictionary<object, int>();
        Powerset(s, "", 0, n, diHT);

        Dictionary<object, int> opsHT = new Dictionary<object, int>();
        s = "++++----****////";
        n = 3;
        Powerset(s, "", 0, n, opsHT);

        /* find all possible combinations of digits and operators and form a postfix notation of each
         * possibility.
         * */
        List<string> possibleOps = new List<string>();
        foreach (var digits in diHT)
        {
            foreach (var ops in opsHT)
            {
                var possible = SpreadOps(digits.Key.ToString(), ops.Key.ToString());
                possibleOps.AddRange(possible);
            }
        }
        
        var evals = new List<Eval>();
        foreach (var expr in possibleOps)
        {
            double value = EvaluatePostfix(expr);
            if (value == -1) continue;
            var eval = new Eval()
            {
                Digits = expr.Substring(0, 4)
            };
            var index = evals.IndexOf(eval);
            if (index > -1)
            {
                evals[index].AddValue(expr.Substring(4), value);
            }
            else
            {
                evals.Add(eval);
                eval.AddValue(expr.Substring(4), value);
            }
        }
        
        var allDigits = new Dictionary<string, List<double>>();
        foreach (var eval in evals)
        {
            var digits = eval.DigitsSorted;
            if (allDigits.ContainsKey(digits))
            {
                var values = allDigits[digits];
                values.AddRange(eval.Values.Where(a => a>0).Distinct().OrderBy(a => a).ToList());
            }
            else
            {
                allDigits.Add(digits, eval.Values.Where(a => a > 0).Distinct().OrderBy(a => a).ToList());
            }
        }
        double prevValue = 0;
        var digitValuePairs = new List<DigitValue>();
        double maxValue = 0;
        string maxValueProducingDigits = string.Empty;
        foreach (var item in allDigits)
        {
            foreach (var value in item.Value.Distinct().OrderBy(a => a))
            {
                if (value > prevValue + 1)
                {
                    if (prevValue > maxValue)
                    {
                        maxValue = prevValue;
                        maxValueProducingDigits = item.Key;
                    }
                    break;
                }
                prevValue = value;             
            }
        }
        Console.WriteLine("These four digits produce the longest sequence: {0} which is of length: {1}.", maxValueProducingDigits, maxValue);
    }
    static void Powerset(string s, string p, int index, int n, Dictionary<object, int> ht)
    {
        if (p.Length == n)
        {
            if (ht.ContainsKey(p))
                return;
            ht.Add(p, 1);            
            counter++;
        }
        for (int i = index; i < s.Length; i++)
        {
            var news = s.Substring(0, i) + s.Substring(i + 1);            
            var newp = p + s[i];
            Powerset(news, newp, i, n, ht);
        }
    }
    /// <summary>
    /// for each permutation of digits (abcd) append every permutation of operators.
    /// return a list of all possibilities.
    /// </summary>
    /// <param name="digits"></param>
    /// <param name="ops"></param>
    /// <returns></returns>
    static List<string> SpreadOps(string digits, string ops)
    {
        var expressions = new List<string>();
        var opsPerms = new List<string>(){ ops};
        if (ops[0] != ops[1] || ops[0] != ops[2])
        {
            opsPerms = perm(ops, "").ToList();
        }
        foreach (var operatorsPermuted in opsPerms)
        {
            foreach (var digitsPermuted in perm(digits, ""))
            {
                string expr = digitsPermuted + operatorsPermuted;
                if (!expressions.Contains(expr))
                {
                    expressions.Add(expr);
                }
            }
        }
        return expressions;        
    }
    /// <summary>
    /// playing with the yield keyword to return permuations
    /// </summary>
    /// <param name="s"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    static IEnumerable<string> perm(string s, string p)
    {
        if (s.Length == 0)
        {
            //Console.WriteLine(p);
            yield return p;
        }
        else
        {
            for (int i = 0; i < s.Length; i++)
            {
                var news = s.Substring(0, i)+s.Substring(i+1);
                var newp = p + s[i];
                foreach (var item in perm(news, newp))
                {
                    yield return item;
                }
            }
            
        }
    }
    static double EvaluatePostfix(string expression)
    {
        Stack<double> stack = new Stack<double>();
        foreach (var c in expression)
        {
            if (char.IsDigit(c))
            {
                stack.Push(double.Parse(c.ToString()));
            }
            else
            {
                var operand1 = stack.Pop();
                var operand2 = stack.Pop();
                double result = 0;
                switch (c)
                {
                    case '+':
                        result = operand2 + operand1;
                        stack.Push(result);
                        break;
                    case '-':
                        result = operand2 - operand1;
                        stack.Push(result);
                        break;
                    case '*':
                        result = (operand2 * operand1);
                        stack.Push(result);
                        break;
                    case '/':
                        result = operand2 / operand1;
                        stack.Push(result);
                        break;
                    default:
                        throw new Exception("Invalid operator : " + c);
                }
            }
        }
        return Math.Abs(Math.Round(stack.Pop()));
    }
}
public class Eval : IEquatable<Eval>
{
    public string Digits { get; set; }
    public List<string> Ops { get; private set; }
    public List<double> Values { get; private set; }
    private int _index = 0;
    public Eval()
    {
        this.Digits = string.Empty;
        this.Ops = new List<string>() { };
        this.Values = new List<double> { };
        _index = 0;
    }
    public void AddValue(string ops, double value)
    {
        this.Ops.Insert(_index, ops);
        this.Values.Insert(_index, value);
        _index++;
    }
    public override int GetHashCode()
    {
        return this.Digits.GetHashCode();
    }
    private bool _Equals(Eval other)
    {
        if (other == null)
        {
            return false;
        }
        return this.Digits == other.Digits;
    }
    public override bool Equals(object other)
    {
        var obj = other as Eval;
        if (obj == null)
        {
            return false;
        }
        return this._Equals(obj);
    }
    public bool Equals(Eval other)
    {
        return this._Equals(other);
    }
    public List<string> GetAll()
    {
        var lines = new List<string>();
        for (int i = 0; i < _index; i++)
        {
            var s = string.Format("{0},{1},{2}", this.Digits, this.Ops[i], this.Values[i]);
            lines.Add(s);
        }
        return lines;
    }
    public string DigitsSorted
    {
        get
        {
            return string.Join("", this.Digits.OrderBy(a => a));
        }
    }
}
class DigitValue
{
    public DigitValue()
    {
        this.Digits = string.Empty;
        this.Value = 0;
    }
    public string Digits { get; set; }
    public int Value { get; set; }
}
