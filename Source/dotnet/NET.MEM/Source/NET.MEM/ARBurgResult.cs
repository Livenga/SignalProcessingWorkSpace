namespace NET.MEM;


/// <summary></summary>
public sealed partial class ARBurgResult
{
    /// <summary></summary>
    public double Pm { get; }


    /// <summary></summary>
    public double Q { get; }

    /// <summary></summary>
    public double[] A { get; }


    /// <summary></summary>
    private ARBurgResult(
            double   pm,
            double   q,
            double[] a)
    {
        Pm = pm;
        Q  = q;
        A  = a;
    }
}
