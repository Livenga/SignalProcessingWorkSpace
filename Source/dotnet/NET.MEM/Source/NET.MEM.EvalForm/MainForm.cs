namespace NET.MEM.EvalForm;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
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


    /// <summary>
    /// </summary>
    private void ApplyInputSignal(IEnumerable<Data.SignalItem> items)
    {
        _inputValues = null;
        inputSignalPlot.Plot.Clear();

        _inputValues = items 
            .Select(o => o.Value)
            .ToArray();

        var xs = items 
            .Select(o => o.T)
            .ToArray();

        var dataTable = new DataTable();
        dataTable.Columns.Add("時間[ms]", typeof(double));
        dataTable.Columns.Add("値", typeof(double));
        foreach(var item in items)
        {
            var row = dataTable.NewRow();
            row[0] = item.T;
            row[1] = item.Value;
            dataTable.Rows.Add(row);
        }

        inputDataGrid.DataSource = dataTable;

        inputDataGrid.Columns[0].DefaultCellStyle.Format = "0.0000";
        inputDataGrid.Columns[1].DefaultCellStyle.Format = "E";


        // グラフ描画
        var myPlot  = inputSignalPlot.Plot;
        var scatter = myPlot.Add.ScatterLine(
                xs: xs,
                ys: _inputValues);
        scatter.LineStyle.AntiAlias = true;
        scatter.LineStyle.Width     = .5f;

        myPlot.Axes.Left.Label.FontName = "Times New Roman";
        myPlot.Axes.Left.Label.FontSize = 4f;

        myPlot.Axes.Bottom.Label.FontName = "Times New Roman";
        myPlot.Axes.Bottom.Label.FontSize = 4f;

        myPlot.Axes.AutoScale();

        inputSignalPlot.Refresh();
    }
}
