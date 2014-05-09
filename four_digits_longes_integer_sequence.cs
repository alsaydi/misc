//euler 93
using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static int counter = 0;
    static void Main()
    {
        /* 
        Console.Read();
        Console.WriteLine(EvaluatePostfix("8512/-*"));
        Environment.Exit(0); 
         * */
        //var s = "123456789";
        var s = "1258";
        int n = 4;
        Dictionary<object, int> diHT = new Dictionary<object, int>();
        Powerset(s, "", 0, n, diHT);
        //        Console.WriteLine("Sets: {0}", counter);

        Dictionary<object, int> opsHT = new Dictionary<object, int>();
        s = "++++----****////";
        n = 3;
        Powerset(s, "", 0, n, opsHT);
        //Console.WriteLine("Ops Sets: {0}", counter);
        List<string> possibleOps = new List<string>();
        foreach (var digits in diHT)
        {
            foreach (var ops in opsHT)
            {
                var possible = SpreadOps(digits.Key.ToString(), ops.Key.ToString());
                possibleOps.AddRange(possible);
            }
        }
        //foreach (var item in possibleOps)
        //{
        //    Console.Write(item);
        //    var value = EvaluatePostfix(item);
        //    Console.WriteLine(" {0}", value);
        //}
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
        /*foreach (var eval in evals)
        {
            var lines = eval.GetAll();
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }*/
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
                    Console.WriteLine("{0},{1}", item.Key, prevValue);
                    if (prevValue > maxValue)
                    {
                        maxValue = prevValue;
                        maxValueProducingDigits = item.Key;
                    }
                    break;
                }
                prevValue = value;
                //Console.WriteLine("{0},{1}", item.Key, value);                
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
            //Console.WriteLine(p);
            counter++;
        }
        for (int i = index; i < s.Length; i++)
        {
            var news = s.Substring(0, i) + s.Substring(i + 1);
            //if(p.Length>0 && p[p.Length-1]>s[i]) return;
            //if(p.Length>0 && p[p.Length-1] == s[i]) break;
            var newp = p + s[i];
            Powerset(news, newp, i, n, ht);
        }
    }
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
        //Console.WriteLine(expressions.Count);
        return expressions;
        //return s;	
    }
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
                        //if (result < 0) return -1;
                        stack.Push(result);
                        break;
                    case '*':
                        result = (operand2 * operand1);
                        stack.Push(result);
                        break;
                    case '/':
                        //if (operand2 % operand1  != 0) return 9999;
                        //Console.WriteLine("{0} {1}", operand1, operand2);
                        result = operand2 / operand1;
                        stack.Push(result);
                        break;
                    default:
                        throw new Exception("Invalid operator : " + c);
                }
            }
        }
        return Math.Abs((stack.Pop()));
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
