namespace mynah.FFT;

using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


static class Program
{
    /// <summary>
    /// メイン
    /// </summary>
    static async Task Main(string[] args)
    {
        if(! args.Any())
        {
            return;
        }

        var signal = await LoadSignalFromFileAsync(path: args[0]);

        // ハミング窓
        var window = Window.Hamming(signal.Length);
        var dataset = signal
            .Select((value, idx) => new Complex(value * window[idx], .0f))
            .ToArray();

        Fourier.Forward(dataset, FourierOptions.Matlab);

        for(var i = 0; i < dataset.Length / 2; ++i)
        {
            var m = dataset[i].Magnitude;
            Console.WriteLine($"{i}\t{m}\t{10f * Math.Log10(m)}");
        }
    }



    /// <summary>
    /// 信号データをファイルから読み込む
    /// </summary>
    /// <param name="path">パス</param>
    private static async Task<double[]> LoadSignalFromFileAsync(string path)
    {
        using var stream = File.Open(path, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream, Encoding.UTF8);

        string? line = null;
        var values = new List<double>();

        while((line = await reader.ReadLineAsync()) != null)
        {
            values.Add(Convert.ToDouble(value: line));
        }

        return values.ToArray();
    }
}
