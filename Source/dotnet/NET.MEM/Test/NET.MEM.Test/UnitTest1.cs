namespace NET.MEM.Test;

using NET.MEM;
using System;
using System.IO;
using System.Text;
using Xunit;
using Xunit.Abstractions;


/// <summary></summary>
public class ARBurgResultTest
{
    private readonly ITestOutputHelper _outputHelper;

    /// <summary></summary>
    public ARBurgResultTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }


    /// <summary></summary>
    [Fact]
    public async Task CalcTestAsync()
    {
        if(! Directory.Exists("csvs"))
            Directory.CreateDirectory("csvs");

        var signal = Enumerable.Range(0, 1000)
            .Select(dt => Math.Sin(2f * Math.PI *  32f * ((double)dt / 1000f))
                    +     Math.Sin(2f * Math.PI *  64f * ((double)dt / 1000f))
                    +     Math.Sin(2f * Math.PI * 128f * ((double)dt / 1000f))
                    )
            .ToArray();

        // 波形の保存
        var signalPath = Path.Combine("csvs", "signal.csv");
        using(var streamSignal = File.Open(signalPath, File.Exists(signalPath) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.Write))
        {
            foreach(var o in signal.Select((v, i) => (index: i, value: v)))
            {
                var s = $"{(double)o.index / 1000f}\t{o.value}\n";
                var buffer = Encoding.UTF8.GetBytes(s);

                await streamSignal.WriteAsync(buffer, 0, buffer.Length, CancellationToken.None);
            }
            await streamSignal.FlushAsync(CancellationToken.None);
        }


        var results = ARBurgResult.Calc(signal, 500)
            .OrderBy(result => result.Q)
            .ToArray();

        foreach(var result in results)
        {
            _outputHelper.WriteLine($"[{result.A.Length}] {result.Q} {result.Pm}");
        }



        for(var i = 0; i < 16; ++i)
        {
            var r   = results[i];
            var psd = r.PowerSpectrum(100f, .001);

            var path = Path.Combine("csvs", $"{i:D6}_{r.A.Length}.csv");
            using(var stream = File.Open(path, File.Exists(path) ? FileMode.Truncate : FileMode.CreateNew, FileAccess.Write))
            {
                foreach(var item in psd)
                {
                    var s = $"{item.Item1}\t{item.Item2}\n";
                    var buffer = Encoding.UTF8.GetBytes(s);

                    await stream.WriteAsync(buffer, 0, buffer.Length, CancellationToken.None);
                }
                await stream.FlushAsync(CancellationToken.None);
            }
        }
    }
}
