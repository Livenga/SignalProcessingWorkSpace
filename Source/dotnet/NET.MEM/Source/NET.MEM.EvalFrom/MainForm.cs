namespace NET.MEM.EvalFrom;

using System.Drawing;
using System.Windows.Forms;


/// <summary>
/// メインフォーム
/// </summary>
public partial class MainForm : Form
{
    /// <summary></summary>
    internal enum InputSelectionMode : byte
    {
        /// <summary></summary>
        Any = 0,

        /// <summary></summary>
        Csv,

        /// <summary></summary>
        Frequency
    }


    private double[]? _inputValues = null;
    private ListViewItem[]? _memResultViewItems = null;

    private readonly ScottPlot.WinForms.FormsPlot inputSignalPlot;
    private readonly ScottPlot.WinForms.FormsPlot powerSpectrumDensityPlot;


    /// <summary>
    /// ctor
    /// </summary>
    public MainForm()
    {
        InitializeComponent();

        // NOTE プロットのサイズに関しては個別に調整が必要
        inputSignalPlot = new ScottPlot.WinForms.FormsPlot
        {
            Name     = "inputSignalPlot",
            BackColor = Color.White,
            Dock     = DockStyle.Fill,
            TabIndex = 1,
            TabStop  = true,
        };

        powerSpectrumDensityPlot = new ScottPlot.WinForms.FormsPlot
        {
            Name      = "powerSpectrumDensityPlot",
            BackColor = Color.White,
            Dock      = DockStyle.Fill,
            TabIndex  = 1,
            TabStop   = true,
        };


        inputSignalPlotPanel.Controls.Add(inputSignalPlot);
        memPSDPlotPanel.Controls.Add(powerSpectrumDensityPlot);
    }
}
