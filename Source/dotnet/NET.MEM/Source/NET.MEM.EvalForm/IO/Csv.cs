namespace NET.MEM.EvalForm.IO;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



/// <summary></summary>
internal static class Csv
{
    private static readonly CsvHelper.Configuration.CsvConfiguration _config;

    static Csv()
    {
        _config = new (CultureInfo.InvariantCulture)
        {
            Encoding        = Encoding.UTF8,
            HasHeaderRecord = false,
        };
    }



    /// <summary></summary>
    public static async Task<IEnumerable<T>> ReadAsync<T>(
            string            path,
            Encoding?         encoding = null,
            CancellationToken cancellationToken = default(CancellationToken))
    {
        using var stream = File.Open(path, FileMode.Open, FileAccess.Read);
        return await ReadAsync<T>(stream, encoding, cancellationToken);
    }


    /// <summary></summary>
    public static async Task<IEnumerable<T>> ReadAsync<T>(
            Stream            stream,
            Encoding?         encoding = null,
            CancellationToken cancellationToken = default(CancellationToken))
    {
        using var reader = new StreamReader(stream, encoding ?? Encoding.UTF8);
        using var csv = new CsvHelper.CsvReader(reader, _config, false);

        return await csv
            .GetRecordsAsync<T>(cancellationToken)
            .ToArrayAsync(cancellationToken);
    }


    /// <summary></summary>
    public static async Task WriteAsync<T>(
            Stream            stream,
            IEnumerable<T>    records,
            Encoding?         encoding          = null,
            CancellationToken cancellationToken = default(CancellationToken))
    {
        using var writer = new StreamWriter(stream, encoding ?? Encoding.UTF8);
        using var csv = new CsvHelper.CsvWriter(writer, _config, false);

        await csv.WriteRecordsAsync(records, cancellationToken);
        await stream.FlushAsync(cancellationToken);
    }


    /// <summary></summary>
    public static async Task WriteAsync<T>(
            string            path,
            IEnumerable<T>    records,
            Encoding?         encoding          = null,
            CancellationToken cancellationToken = default(CancellationToken))
    {
        using var stream = File.Open(
                path:   path,
                mode:   File.Exists(path) ? FileMode.Truncate : FileMode.Create,
                access: FileAccess.Write);

        await WriteAsync<T>(stream, records, encoding, cancellationToken);
    }
}
