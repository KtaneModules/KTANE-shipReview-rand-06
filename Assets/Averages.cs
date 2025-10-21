using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class Averages
{

    public static decimal Average(List<sbyte> values, int num)
    {
        switch (num)
        {
            case 1: return MedianAverage(values);
            case 2: return ArithmeticMean(values);
            case 3: return TrimmedMean(values);
            case 4: return SignedRMS(values);
            case 5: return HuberMeanOneIter(values);
            default: return 0;
        }
    }
    
    public static decimal MedianAverage(List<sbyte> values)
    {
        values.Sort();
        return values[values.Count / 2];
    }
    
    public static decimal ArithmeticMean(List<sbyte> values)
    {
        decimal sum = 0;
        foreach (var v in values) sum += v;
        return sum / values.Count;
    }
    
    public static decimal TrimmedMean(List<sbyte> values)
    {
        var sorted = values.OrderBy(v => v).ToList();
        sorted.RemoveAt(0);
        sorted.RemoveAt(sorted.Count - 1);
        decimal sum = 0;
        foreach (var v in sorted) sum += v;
        return sum / sorted.Count;
    }
    
    public static decimal SignedRMS(List<sbyte> values)
    {
        decimal sumSq = 0;
        decimal sum = 0;
        foreach (var v in values)
        {
            sumSq += v * v;
            sum += v;
        }
        double meanSq = (double)sumSq / values.Count;
        decimal rms = (decimal)Math.Sqrt(meanSq);
        if (sum > 0) return rms;
        if (sum < 0) return -rms;
        return 0; // ровно ноль
    }
    
    public static decimal HuberMeanOneIter(List<sbyte> values)
    {
        sbyte m = values.OrderBy(v => v).ToArray()[values.Count / 2]; // начальная медиана
        decimal num = 0, den = 0;
        foreach (var v in values)
        {
            float w = m == v ? 1f : Math.Min(1f, 1.5f / Math.Abs(v - m));
            num += (decimal)w * v;
            den += (decimal)w;
        }
        return num / den;
    }
}