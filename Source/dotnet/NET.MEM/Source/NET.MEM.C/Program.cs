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
            .Select(dt => Math.Sin(2d * Math.PI *  50d   * ((double)dt / 1000d))
                    +     Math.Sin(2d * Math.PI *  100d * ((double)dt / 1000d))
                    +     Math.Sin(2d * Math.PI *  62d * ((double)dt / 1000d))
                    +     Math.Sin(2d * Math.PI *  250d * ((double)dt / 1000d)))
            .ToArray();

        var results = ARBurgResult.Calc(signal, 256);

        var directoryName = DateTime.Now.ToString("yyyyyMMdd_hhmmss");
        var csvPath = TryCreateDirectory(Path.Combine("csvs", directoryName));

#if DEBUG
        var signalPath = Path.Combine(csvPath, "signal.csv");
        using(var signalStream = File.Open(signalPath, File.Exists(signalPath) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.Write))
        {
            for(var i = 0; i < 1000; ++i)
            {
                var signalBuf = Encoding.UTF8.GetBytes($"{(double)i/1000d}\t{signal[i]}\n");
                await signalStream.WriteAsync(signalBuf, 0, signalBuf.Length, CancellationToken.None);
            }
            await signalStream.FlushAsync(CancellationToken.None);
        }
#endif
        var symLinkPath = Path.Combine("csvs", "latest");
        if(Directory.Exists(symLinkPath))
            File.Delete(symLinkPath);
        File.CreateSymbolicLink(
                path: symLinkPath,
                pathToTarget: directoryName);

        int index = 1;
        foreach(var r in results
                .OrderBy(r => r.Q)
                .Take(16))
        {
            var csvDataPath = Path.Combine(csvPath, $"{index:D3}_m{r.A.Length:D6}.csv");

            Console.Error.WriteLine($"[{r.A.Length}]\tQm = {r.Q}");

            var o = r.PowerSpectrum(250f, .001);

            await PowerSpectrumDensityToCsvAsync(csvDataPath, Encoding.UTF8, o, CancellationToken.None);

            var aPath = Path.Combine("a", $"{r.A.Length:D6}.txt");
            using(var stream = File.Open(aPath, File.Exists(aPath) ? FileMode.Truncate : FileMode.Create, FileAccess.Write))
            {
                string s = $"Pm = {r.Pm}\nQ = {r.Q}\n\n";
                var buffer = Encoding.UTF8.GetBytes(s);
                await stream.WriteAsync(buffer, 0, buffer.Length, CancellationToken.None);

                int _index = 1;
                foreach(var a in r.A)
                {
                    buffer = Encoding.UTF8.GetBytes($"{_index}\t{a}\n");
                    await stream.WriteAsync(buffer, 0, buffer.Length, CancellationToken.None);
                    ++_index;
                }

            }

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
