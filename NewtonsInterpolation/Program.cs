using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

internal class Program
{
    private static void Main(string[] args)
    {
        List<(int, int)> points = new(); //string do podejścia #1

        points.Add((1, 2));
        points.Add((2, -3));
        points.Add((3, 4));
        points.Add((-1, 2));

        List<double> xValues = new();
        List<double> yValues = new();
        foreach (var point in points)
        {
            // do podejścia #1 xValues.Add(int.Parse(point.Item1));
            xValues.Add(point.Item1);
            yValues.Add(point.Item2);
        }

        // do podejścia #1 List<double> functionValues = new();
        // do podejścia #1 functionValues.Add(points[0].Item2);

        List<double> coefficients = new();

        //do podejścia #1 NewtonInterpolation(points, functionValues, xValues);
        var result = NewtonInterpolation(xValues, yValues, coefficients);

        //Wypisz wszystkie współczynniki
        for (int i = 0; i < result.Count; i++)
        {
            Console.WriteLine($"a{i} = {result[i]}");
        }
        //Zbuduj wielomian
        StringBuilder polynomial = new();
        foreach(var coeff in result.Select((value, index) => new { value, index }))
        {
            if (coeff.index == 0)
            {
                polynomial.Append($"{coeff.value}");
            }
            else
            {
                polynomial.Append($" + {coeff.value}");
                for (int j = 0; j < coeff.index; j++)
                {
                    polynomial.Append($"(x - {xValues[j]})");
                }
            }
        }
        Console.WriteLine($"Wielomian interpolacyjny Newtona: P(x) = {polynomial}");   

    }

    public static List<double> NewtonInterpolation(List<double>xValues, List<double> yValues, List<double> coefficient, int iteration = 1)//iteration w sumie nie jest wymagane, można użyć coefficient.Count
    {
        //Wyniki
        coefficient.Add(yValues[0]);
        //Obecne yValues, zmniejsza się o 1 element przy kazdej rekurencji
        List<double> current = new();
        for(int i = 0; i < yValues.Count -1; i++)
        {
            double product = (yValues[i + 1] - yValues[i]) / (xValues[i + iteration] - xValues[i]);
            current.Add(product);
        }
        if (yValues.Count == 1)
        {
            
            return coefficient;
        }
        else
        {
            iteration++;
            return NewtonInterpolation(xValues, current, coefficient, iteration);
        }
    }













    /* =================Podejście #1====================== */
    public static void NewtonInterpolation(List<(string, double)> points, List<double> functionValues, List<double> xValues)
    {

        List<(string, double)> differentialQuotients = new();
        int iteration = functionValues.Count;

        for(int i = 0; i < points.Count-1; i++)
        {
            //budowa klucza
            StringBuilder key = new();
            int dlugoscKlucza = xValues.Count - points.Count + 2;
            int indeksPoczatkowy = i;
            int indeksKoncowy = i + dlugoscKlucza - 1;
            key.Append("f[");
            for (int j = indeksPoczatkowy; j <= indeksKoncowy; j++)
            {
                key.Append($"x{j}");
                if (j < indeksKoncowy)
                {
                    key.Append(", ");
                }
            }
            key.Append("]");

         
            double product = (points[i + 1].Item2 - points[i].Item2) / (xValues[i + iteration] - xValues[i]);
            //Console.WriteLine($"{xValues[iteration]} - {xValues[i]} = {xValues[i + iteration] - xValues[i]}");
            Console.WriteLine($"{key} = {product}");
            if (i == 0)
            {
                functionValues.Add(product);
                differentialQuotients.Add((key.ToString(), product));
            }
            else
            {

                differentialQuotients.Add((key.ToString(), product));
            }
        }
        if(points.Count == 1)
        {
            foreach(var dq in differentialQuotients)
            {
                Console.WriteLine($"{dq.Item1} = {dq.Item2}");
            }
            return;
        }
        else
        {
            NewtonInterpolation(differentialQuotients, functionValues, xValues);
        }
    }
}
