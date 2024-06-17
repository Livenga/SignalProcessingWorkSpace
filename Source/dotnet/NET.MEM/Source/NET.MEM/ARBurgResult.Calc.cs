namespace NET.MEM;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


/// <summary></summary>
partial class ARBurgResult
{
    /// <summary></summary>
    public (double, double)[] PowerSpectrum(
            double fmax,
            double dt)
    {
        return PowerSpectrum(0d, fmax, dt);
    }


    /// <summary></summary>
    public (double, double)[] PowerSpectrum(
            double fmin,
            double fmax,
            double dt)
    {
        var ret = new List<(double, double)>();

        for(double f = fmin; f <= fmax; f += .1f)
        {
            var omega = 2f * Math.PI * f;

            double sumSin = .0f, sumCos = 1f;
            for(var i = 1; i < A.Length; ++i)
            {
                sumCos += A[i] * Math.Cos(omega * i * dt);
                sumSin += A[i] * Math.Sin(omega * i * dt);
            }

            double s;
            if(Pm == 0d)
                s = (.0001d * dt) / (sumCos * sumCos + sumSin * sumSin);
            else
                s = (Pm * dt) / (sumCos * sumCos + sumSin * sumSin);

            ret.Add((f, s));
        }

        return ret.ToArray();
    }



    /// <summary></summary>
    public static IEnumerable<ARBurgResult> Calc(
            double[] x,
            int      orderCount)
    {
        List<ARBurgResult> results = new ();

        int N = x.Length;

        var bx  = new double[N + 1];
        var bdx = new double[N + 1];

        var pm  = new double[orderCount + 1];

        var a = new double[orderCount + 1];
        var prevA = new double[orderCount + 1];

        var aveX = x.Average();
        double sumX = .0f;
        for(var i = 1; i <= N; ++i)
        {
            double adjX = x[i - 1] - aveX;

            //bx[i] = adjX;
            bx[i] = x[i - 1];
            sumX += adjX * adjX;
        }
        a[0]  = 1f;
        pm[0] = sumX / (double)N;

        Array.Copy(
                sourceArray:      bx,  sourceIndex:      2,
                destinationArray: bdx, destinationIndex: 1,
                length: N - 1);

        for(var m = 1; m <= orderCount; ++m)
        {
            var maxCount = N - m;

            double nume = .0f, deno = .0f;
            for(var i = 1; i <= maxCount; ++i)
            {
                var bmi  = bx[i];
                var bdmi = bdx[i];

                nume += bmi * bdmi;
                deno += (bmi * bmi) + (bdmi * bdmi);
            }
            double amm = (-2f * nume) / deno;

            a[m]  = amm;
            pm[m] = pm[m - 1] * (1f - (amm * amm) );

            if(m > 1)
            {
                // 自己回帰モデル次数の更新
                for(var i = 1; i < m; ++i)
                {
                    a[i] = prevA[i] + amm * prevA[m - i];
                }
            }

            Array.Copy(
                    sourceArray:      a,     sourceIndex:      0,
                    destinationArray: prevA, destinationIndex: 0,
                    length:           m + 1);

            double Em2 = .0f;
            for(var k = m + 1; k <= N; ++k)
            {
                double sumAx = .0f;
                for(var i = 1; i <= m; ++i)
                {
                    sumAx += a[i] * x[k - i - 1];
                }

                Em2 += Math.Pow(x[k - 1] - sumAx, 2f);
            }

            var q = Em2 * ((double)(N + m + 1d) / (double)(N - (m + 1f)));
            var cursorAm = new double[m + 1];
            Array.Copy(
                    sourceArray:      a,        sourceIndex:      0,
                    destinationArray: cursorAm, destinationIndex: 0,
                    length: m + 1);

            // 結果要素の追加
            results.Add(new ARBurgResult(pm[m], q, cursorAm));

            // bm, b'm の更新
            for(var i = 1; i <= maxCount; ++i)
            {
                bx[i]  += amm * bdx[i];
                bdx[i]  = bdx[i + 1] + amm * bx[i + 1];
            }
        }

        return results;
    }
}
