namespace NET.MEM.C;

using NET.MEM;
using System;


static class Program
{
    static void Main(string[] args)
    {
        var signal = Enumerable.Range(0, 1000)
            .Select(dt => Math.Sin(2f * Math.PI *  32f * ((double)dt / 1000f))
                    +     Math.Sin(2f * Math.PI *  64f * ((double)dt / 1000f))
                    +     Math.Sin(2f * Math.PI * 128f * ((double)dt / 1000f)))
            .ToArray();

        var results = ARBurgResult.Calc(signal, 500)
            .OrderBy(r => r.Q)
            .Take(32)
            .ToArray();

        foreach(var r in results)
        {
            Console.WriteLine($"[{r.A.Length}]\t{r.Q}\t{r.Pm}");
            var o = r.PowerSpectrum(150f, .001);
        }
    }
}
