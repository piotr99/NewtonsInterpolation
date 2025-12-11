/*
SPRAWOZDANIE – Interpolacja wielomianowa metodą Newtona

1. Cel programu
   Program realizuje interpolację wielomianową metodą Newtona dla zadanych punktów (x_i, y_i).
   Wynikiem działania programu są:
   - współczynniki wielomianu interpolacyjnego Newtona,
   - tekstowa postać wielomianu P(x) wypisana w konsoli.

2. Podstawa teoretyczna
   Dla punktów (x_0, y_0), (x_1, y_1), ..., (x_n, y_n) szukamy wielomianu P(x) stopnia n takiego, że:
      P(x_i) = y_i  dla każdego i.
   Wielomian Newtona ma postać:
      P(x) = a0 
           + a1 (x - x0)
           + a2 (x - x0)(x - x1)
           + ...
           + an (x - x0)(x - x1)…(x - x_{n-1})

   Współczynniki a_k wyznaczane są za pomocą różnic dzielonych:
      a0 = f[x0] = y0
      a1 = f[x0, x1]
      a2 = f[x0, x1, x2]
      ...
   Różnice dzielone wyższego rzędu oblicza się rekurencyjnie z wykorzystaniem wzoru:
      f[x_i, x_{i+1}] = (f(x_{i+1}) - f(x_i)) / (x_{i+1} - x_i)

3. Opis działania programu
   - W funkcji Main tworzona jest lista punktów typu (int, int):
        (1, 2), (2, -3), (3, 4), (-1, 2).
   - Następnie punkty są rozdzielane na dwie listy:
        xValues – lista argumentów x,
        yValues – lista wartości y.
   - Tworzona jest lista coefficients, która będzie przechowywać kolejne współczynniki a_k.
   - Wywołanie:
        var result = NewtonInterpolation(xValues, yValues, coefficients);
     zwraca listę współczynników wielomianu Newtona.

   Funkcja NewtonInterpolation:
   - Parametry:
       xValues     – stała lista argumentów x,
       yValues     – aktualne wartości (kolejne rzędy różnic dzielonych),
       coefficient – lista dotychczas obliczonych współczynników a_k,
       iteration   – numer aktualnego rzędu różnic dzielonych (domyślnie 1).
   - Dodaje do coefficient pierwszy element yValues[0] jako kolejny współczynnik a_k.
   - Oblicza nową listę current, zawierającą różnice dzielone wyższego rzędu:
       current[i] = (yValues[i + 1] - yValues[i]) / (xValues[i + iteration] - xValues[i]).
   - Jeżeli yValues.Count == 1, to znaczy, że obliczono wszystkie współczynniki i funkcja zwraca coefficient.
   - W przeciwnym wypadku iteration jest zwiększane i funkcja wywołuje się rekurencyjnie
     z aktualnymi wartościami current.

   Po otrzymaniu listy result:
   - Program wypisuje na ekranie współczynniki:
        a0, a1, a2, ...
   - Następnie buduje tekstową postać wielomianu P(x) z użyciem StringBuilder:
       P(x) = a0 + a1(x - x0) + a2(x - x0)(x - x1) + ...

4. Przykładowe dane
   W programie użyto punktów:
      (1, 2), (2, -3), (3, 4), (-1, 2).
   Na ich podstawie wyznaczane są współczynniki a0, a1, a2, a3
   oraz budowany jest odpowiadający im wielomian interpolacyjny Newtona.

5. Wnioski
   - Program poprawnie implementuje interpolację Newtona z wykorzystaniem różnic dzielonych.
   - Zastosowanie rekursji w funkcji NewtonInterpolation upraszcza zapis algorytmu,
     ale wymaga ostrożności przy doborze argumentów (xValues, yValues, iteration).
   - Oddzielenie przygotowania danych (Main) od funkcji obliczeniowej (NewtonInterpolation)
     ułatwia dalszą rozbudowę programu, np. o wczytywanie punktów z pliku lub z klawiatury.
   - Tekstowa postać wielomianu P(x) umożliwia jego łatwą interpretację oraz późniejsze
     podstawianie konkretnych wartości x do obliczeń numerycznych.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

internal class Program
{ //testosdfssdfsdfsd 13132131231212312312
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
