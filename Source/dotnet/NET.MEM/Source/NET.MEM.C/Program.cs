namespace NET.MEM.C;

using NET.MEM;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


static class Program
{
    static async Task Main(string[] args)
    {
        var signal = Enumerable.Range(0, 1000)
            .Select(dt => Math.Sin(2f * Math.PI *  32f * ((double)dt / 1000f))
                    +     Math.Sin(2f * Math.PI *  64f * ((double)dt / 1000f))
                    +     Math.Sin(2f * Math.PI * 128f * ((double)dt / 1000f)))
            .ToArray();

        var results = ARBurgResult.Calc(signal, 256);

        await DumpAsync("a.csv", Encoding.UTF8, results);


        foreach(var r in results
                .OrderBy(r => r.Q)
                .Take(32))
        {
            Console.WriteLine($"[{r.A.Length}]\t{r.Q}\t{r.Pm}");
            var o = r.PowerSpectrum(150f, .001);
        }
    }


    static async Task DumpAsync(
            Stream stream,
            Encoding encoding,
            IEnumerable<ARBurgResult> results)
    {
        for(var i = 0; i < results.Count(); ++i)
        {
            var r = results.ElementAt(i);
            for(var j = 0; j < r.A.Length; ++j)
            {
                var s = $"{i}\t{j}\t{r.A[j]}\n";
                var buffer = encoding.GetBytes(s);

                await stream.WriteAsync(buffer, 0, buffer.Length, CancellationToken.None);
            }
        }
        await stream.FlushAsync(CancellationToken.None);
    }

    static async Task DumpAsync(
            string path,
            Encoding encoding,
            IEnumerable<ARBurgResult> results)
    {
        using var stream = File.Open(path, File.Exists(path) ? FileMode.Truncate : FileMode.CreateNew);
        await DumpAsync(stream, encoding, results); 
    }
}
