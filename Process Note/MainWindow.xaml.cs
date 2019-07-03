using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;

namespace Process_Note
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process[] processes;
        private Process selectedProcess;
        private List<PerformanceCounter> counters = new List<PerformanceCounter>();

        public MainWindow()
        {
            InitializeComponent();
            showAllProcesses();
            processBox.MouseLeftButtonUp += new MouseButtonEventHandler(processBox_LeftClick);
            processBox.MouseDoubleClick += new MouseButtonEventHandler(processBox_DoubleClick);
        }

        void showAllProcesses()
        {
            processes = Process.GetProcesses();
            processBox.Items.Clear();
            
            foreach (Process proc in processes)
            {
                processBox.Items.Add("" + proc.Id + " " + proc.ProcessName);
            }
        }
        private void processBox_DoubleClick(object sender, RoutedEventArgs e)
        {
            int processId = Convert.ToInt32(processBox.SelectedItem.ToString().Split(' ')[0]);
            selectedProcess = Process.GetProcessById(processId);
            processDetails.Items.Clear();
            processDetails.Items.Add("Id: " + selectedProcess.Id);
            processDetails.Items.Add("Name: " + selectedProcess.ProcessName);

            PerformanceCounter counter = new PerformanceCounter("Process", "% Processor Time", selectedProcess.ProcessName, true);
            counter.NextValue();
            Thread.Sleep(1);
            processDetails.Items.Add("CPU usage: " + Math.Round(counter.NextValue(), 2) + "%");
            processes = Process.GetProcesses();
            int countProcess = 0;
            foreach (Process proc in processes)
            {
                if (proc.ProcessName == selectedProcess.ProcessName)
                {
                    countProcess += 1;
                }
            }
            processDetails.Items.Add("Process memory: " + BytesToString(selectedProcess.VirtualMemorySize64));
            processDetails.Items.Add("Running time: " + selectedProcess.TotalProcessorTime);
            processDetails.Items.Add("Start time: " + selectedProcess.StartTime);
            processDetails.Items.Add("Threads count: " + countProcess);
        }

        private void processBox_LeftClick(object sender, RoutedEventArgs e)
        {
            if (processBox.SelectedItem != null)
            {

                int processId = Convert.ToInt32(processBox.SelectedItem.ToString().Split(' ')[0]);
                selectedProcess = Process.GetProcessById(processId);
                processDetails.Items.Clear();
                processDetails.Items.Add("Id: " + selectedProcess.Id);
                processDetails.Items.Add("Name: " + selectedProcess.ProcessName);

                PerformanceCounter counter = new PerformanceCounter("Process", "% Processor Time", selectedProcess.ProcessName, true);
                counter.NextValue();
                Thread.Sleep(1);
                processDetails.Items.Add("CPU usage: " + Math.Round(counter.NextValue(),2) + "%");
                processes = Process.GetProcesses();
                int countProcess = 0;
                foreach (Process proc in processes)
                {
                    if (proc.ProcessName == selectedProcess.ProcessName)
                    {
                        countProcess += 1;
                    }
                }
                processDetails.Items.Add("Process memory: " + BytesToString(selectedProcess.VirtualMemorySize64));
                processDetails.Items.Add("Running time: " + selectedProcess.UserProcessorTime);
                processDetails.Items.Add("Start time: " + selectedProcess.StartTime);
                processDetails.Items.Add("Threads count: " + countProcess);


            }
        }


        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

    }
}
