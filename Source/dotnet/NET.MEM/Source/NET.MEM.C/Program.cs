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
            .Select(dt => Math.Sin(2d * Math.PI *  28d   * ((double)dt / 1000d))
                    +     Math.Sin(2d * Math.PI *  31.5d * ((double)dt / 1000d))
                    +     Math.Sin(2d * Math.PI *  32.5d * ((double)dt / 1000d)))
            .ToArray();

        var results = ARBurgResult.Calc(signal, 256);

        var directoryName = DateTime.Now.ToString("yyyyyMMdd_hhmmss");
        var csvPath = TryCreateDirectory(Path.Combine("csvs", directoryName));
        var symLinkPath = Path.Combine("csvs", "latest");
        if(File.Exists(symLinkPath))
            File.Delete(symLinkPath);
        File.CreateSymbolicLink(
                path: symLinkPath,
                pathToTarget: directoryName);

        int index = 1;
        foreach(var r in results
                .OrderBy(r => r.Q)
                .Take(32))
        {
            var csvDataPath = Path.Combine(csvPath, $"{index:D3}_m{r.A.Length:D6}.csv");
            var o = r.PowerSpectrum(250f, .001);

            await PowerSpectrumDensityToCsvAsync(csvDataPath, Encoding.UTF8, o, CancellationToken.None);
            ++index;
        }

#if ENABLE_PSD
        var m35 = results.First(r => r.A.Length == 36);
        var psd = m35.PowerSpectrum(300d, .001d);
        foreach(var item in psd)
        {
            Console.WriteLine($"{item.Item1}\t{item.Item2}\t{10d * Math.Log10(d: item.Item2)}");
        }
#endif

        await Task.Delay(10, CancellationToken.None);
    }


    /**
     */
    static async Task PowerSpectrumDensityToCsvAsync(
            string path,
            Encoding encoding,
            IEnumerable<(double freq, double value)> values,
            CancellationToken cancellationToken = default(CancellationToken))
    {
        using var stream = File.Open(path, File.Exists(path) ? FileMode.Truncate : FileMode.Create, FileAccess.Write);
        foreach(var v in values)
        {
            var s = $"{v.freq}\t{v.value}\t{10d * Math.Log10(v.value)}\n";
            var buffer = encoding.GetBytes(s);

            await stream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
        }
        await stream.FlushAsync(cancellationToken);
    }


    /**
     */
    static string TryCreateDirectory(string path)
    {
        try
        {
            if(! Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        catch
        {
            return Directory.GetCurrentDirectory();
        }

        return path;
    }


    /**
     */
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
