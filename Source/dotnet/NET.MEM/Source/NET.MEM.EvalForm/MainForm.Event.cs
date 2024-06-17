namespace NET.MEM.EvalForm;

using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



partial class MainForm
{
    /// <summary></summary>
    private void OnFormLoad(object source, EventArgs e)
    {
        var asm = Assembly.GetExecutingAssembly();
        var sVersion = asm.GetName().Version?.ToString();

        Text += $" v{sVersion}";
#if DEBUG
        Text += " [DEBUG]";
        debugBorderLabel.BackColor = Color.Red;
#else
        debugBorderLabel.BackColor = SystemColors.Control;
#endif
    }


    /// <summary></summary>
    private void OnFileExitMenuClick(object source, EventArgs e)
    {
        Close();
    }


    /// <summary></summary>
    private void OnInputSelectionModeCheckedChanged(object source, EventArgs e)
    {
        var rbx = source as RadioButton;
        if(rbx == null || ! rbx.Checked)
            return;

        var mode = InputSelectionMode.Any;
        if(rbx == inputCsvModeRadio)
            mode = InputSelectionMode.Csv;
        else if(rbx == inputGenerateModeRadio)
            mode = InputSelectionMode.Frequency;

        switch(mode)
        {
            case InputSelectionMode.Csv:
                inputChooseCsvFileButton.Enabled = true;

                inputFrequenciesText.Enabled       = false;
                inputSamplingFrequencyText.Enabled = false;
                inputGenerateButton.Enabled        = false;
                break;

            case InputSelectionMode.Frequency:
                inputChooseCsvFileButton.Enabled = false;

                inputFrequenciesText.Enabled       = true;
                inputSamplingFrequencyText.Enabled = true;
                inputGenerateButton.Enabled        = true;
                break;

            default:
                throw new NotSupportedException();

        }
    }


    /// <summary></summary>
    private async void OnInputChooseCsvFileClick(object source, EventArgs e)
    {
        using var dlg = new OpenFileDialog();
        dlg.Filter           = "CSV ファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
        dlg.FilterIndex      = 1;
        dlg.RestoreDirectory = true;

        if(DialogResult.OK == dlg.ShowDialog(this))
        {
            try
            {
                var items = await NET.MEM.EvalForm.IO.Csv.ReadAsync<EvalForm.Data.SignalItem>(
                        path:              dlg.FileName,
                        cancellationToken: CancellationToken.None);

                inputCsvFilePathText.Text = dlg.FileName;

                ApplyInputSignal(items);
            }
            catch(Exception ex)
            {
#if DEBUG
                Debug.WriteLine($"[Error] {ex.GetType().FullName} {ex.Message}");
                Debug.WriteLine(ex.StackTrace);
#endif
            }
        }
    }


    /// <summary></summary>
    private async void OnInputGenerateClick(object source, EventArgs e)
    {
        _inputValues = null;
        inputSignalPlot.Plot.Clear();

        try
        {
            var rads = inputFrequenciesText.Text
                .Split(',')
                .Select(f => 2d * Math.PI * Convert.ToDouble(f.Trim()))
                .ToArray();

            var samplingRate = Convert.ToInt32(inputSamplingFrequencyText.Text, 10);
            var dt = 1d / (double)samplingRate;

            var signalItems = await Task.Run(() => Enumerable
                    .Range(0, 1 + samplingRate)
                    .Select(step => new Data.SignalItem
                        {
                            T     = step * dt,
                            Value = rads.Sum(rad => Math.Sin(rad * (step * dt)))
                        })
                    .ToArray());

            ApplyInputSignal(signalItems);
        }
        catch(Exception ex)
        {
#if DEBUG
            Debug.WriteLine($"[Error] {ex.GetType().FullName} {ex.Message}");
            Debug.WriteLine(ex.StackTrace);
#endif
        }
        finally
        {
            inputSignalPlot.Refresh();
        }
    }


    /// <summary></summary>
    private async void OnInputDataSaveMenuClick(object source, EventArgs e)
    {
        using(var dlg = new SaveFileDialog())
        {
            dlg.Filter           = "CSV ファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            dlg.FilterIndex      = 1;
            dlg.RestoreDirectory = true;

            if(DialogResult.OK == dlg.ShowDialog(this))
            {
                var items = inputDataGrid.Rows
                    .Cast<DataGridViewRow>()
                    .Select(row => new Data.SignalItem()
                        {
                            T     = (double)row.Cells[0].Value,
                            Value = (double)row.Cells[1].Value
                        });

                try
                {
                    await IO.Csv.WriteAsync(
                            path:              dlg.FileName,
                            records:           items,
                            encoding:          Encoding.UTF8,
                            cancellationToken: CancellationToken.None);
                }
                catch(Exception ex)
                {
                }
            }
        }
    }


    /// <summary></summary>
    private void OnMEMRetrieveVirtualItem(object source, RetrieveVirtualItemEventArgs e)
    {
        if(_memResultViewItems != null && _memResultViewItems.Length > e.ItemIndex)
        {
            e.Item = _memResultViewItems[e.ItemIndex];
        }
    }


    /// <summary></summary>
    private async void OnMEMSelectedIndexChanged(object source, EventArgs e)
    {
        var myPlot = powerSpectrumDensityPlot.Plot;
        myPlot.Clear();

        if(memResultList.SelectedIndices.Count == 0
            || _memResultViewItems == null
            || memResultList.SelectedIndices[0] >=_memResultViewItems.Length)
        {
            return;
        }

        powerSpectrumDesityDataGrid.DataSource = null;
        powerSpectrumDesityDataGrid.Columns.Clear();

        memDegreeDataGrid.DataSource = null;
        memDegreeDataGrid.Columns.Clear();


        int? samplingRate = null;
        try
        {
            samplingRate = Convert.ToInt32(inputSamplingFrequencyText.Text, 10);
        }
        catch
        {
            // TODO Message
            return;
        }


        try
        {
            var result = _memResultViewItems[memResultList.SelectedIndices[0]].Tag as NET.MEM.ARBurgResult;
            if(result == null)
                return;

            var minFreq = Convert.ToDouble(psdMinFrequensyText.Text);
            var maxFreq = Convert.ToDouble(psdMaxFrequencyText.Text);
            var values  = await Task.Run(() => result.PowerSpectrum(
                        fmin: minFreq,
                        fmax: maxFreq,
                        dt:   1d / (double)samplingRate));

            //
            var dtPowerSpectrumDensity = new DataTable();
            dtPowerSpectrumDensity.Columns.AddRange(new []
                {
                    new DataColumn("周波数[Hz]", typeof(double)),
                    new DataColumn("PSD", typeof(double)),
                    new DataColumn("PSD[dB]", typeof(double)),
                });
            foreach(var value in values)
            {
                var row = dtPowerSpectrumDensity.NewRow();
                row[0] = value.Item1;
                row[1] = value.Item2;
                row[2] = 10d * Math.Log10(value.Item2);

                dtPowerSpectrumDensity.Rows.Add(row);
            }
            powerSpectrumDesityDataGrid.DataSource = dtPowerSpectrumDensity;
            powerSpectrumDesityDataGrid.Columns["周波数[Hz]"].DefaultCellStyle.Format = "0.00";


            // 次数を DataGridView へ反映
            var dtDegree = new DataTable();
            dtDegree.Columns.AddRange(new []
                {
                    new DataColumn("A[m]", typeof(int)),
                    new DataColumn("値", typeof(double)),
                });
            for(var i = 1; i < result.A.Length; ++i)
            {
                var rDegree = dtDegree.NewRow();
                rDegree[0] = i;
                rDegree[1] = result.A[i];

                dtDegree.Rows.Add(rDegree);
            }
            memDegreeDataGrid.DataSource = dtDegree;


            Func<double, string> logTickLabelFormatter = y => $"{Math.Pow(10d, y)}";


            if(psdEnableXLogscaleCheck.Checked)
            {
                var tickGenerator = new ScottPlot.TickGenerators.NumericAutomatic
                {
                    LabelFormatter     = logTickLabelFormatter,
                    MinorTickGenerator = new ScottPlot.TickGenerators.LogMinorTickGenerator(),
                    //IntegerTicksOnly   = true,
                };

                myPlot.Axes.Left.TickGenerator = tickGenerator;
            }
            else
            {
                myPlot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
            }

            // PSD グラフ描画
            var xs = values.Select(v => v.Item1).ToArray();
            var ys = psdEnableXLogscaleCheck.Checked
                ? values.Select(v => 10d * Math.Log10(v.Item2)).ToArray()
                : values.Select(v => v.Item2).ToArray();

            myPlot.Grid.MinorLineWidth = 1f;

            var scatter = myPlot.Add.ScatterLine(xs: xs, ys: ys);
        }
        catch(Exception ex)
        {
#if DEBUG
            Debug.WriteLine($"[Error] {ex.GetType().FullName} {ex.Message}");
            Debug.WriteLine(ex.StackTrace);
#endif
        }
        finally
        {
            myPlot.Axes.AutoScale();
            powerSpectrumDensityPlot.Refresh();
        }
    }


    /// <summary></summary>
    private void OnMEMExecuteClick(object source, EventArgs e)
    {
        int? degree = null;

        try
        {
            degree = Convert.ToInt32(maximumDegreeText.Text, 10);
        }
        catch
        {
            // TODO
            return;
        }

        _memResultViewItems = null;

        // 最大エントロピー法(ARBurg法)
        if(_inputValues != null && _inputValues.Any())
        {
            _memResultViewItems = NET.MEM.ARBurgResult
                .Calc(_inputValues, degree.Value)
                .OrderBy(r => r.Q)
                .Select(r => new ListViewItem(new []
                        {
                            $"{r.A.Length - 1}",
                            $"{r.Q}"
                        }) { Tag = r })
                .ToArray();

            memResultList.VirtualListSize = _memResultViewItems.Length;
            memResultList.Refresh();
        }

    }

}
