namespace NET.MEM.EvalForm.Data;


/// <summary></summary>
internal class SignalItem
{
    /// <summary></summary>
    [CsvHelper.Configuration.Attributes.Name("T")]
    public double T { set; get; } = 0d;


    /// <summary></summary>
    [CsvHelper.Configuration.Attributes.Name("Value")]
    public double Value { set; get; } = 0d;
}
