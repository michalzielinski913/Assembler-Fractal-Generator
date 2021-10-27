using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace aplProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Measurement> measurements;
        private int iterations;
        private EngineType engine;
        private Bitmap map;
        private System.ComponentModel.BackgroundWorker fractalGenerationWorker;

        private int Mwidth = 1920;
        private int Mheight = 1080;
        private void initializeMeasurementTable()
        {
            measurements = new List<Measurement>();
            measurements.Add(new Measurement() { measureType="Average", cSharp=0.0, cpp=0.0, masm=0.0 });
            measurements.Add(new Measurement() { measureType = "Minimum", cSharp = 0.0, cpp = 0.0, masm = 0.0 });
            measurements.Add(new Measurement() { measureType = "Maximum", cSharp = 0.0, cpp = 0.0, masm = 0.0 });
            measurement.ItemsSource = measurements;
            measurement.IsReadOnly =true;
            measurement.CanUserReorderColumns = false;
            measurement.CanUserSortColumns = false;
            progress.Visibility = Visibility.Hidden;
        }

        public MainWindow()
        {
            InitializeComponent();
            initializeMeasurementTable();
            iterations = 80;
            engine = EngineType.CSHARP;
            SelectionEngine.SelectedIndex = 0;

            fractalGenerationWorker = new BackgroundWorker();
            fractalGenerationWorker.DoWork += generateFractalWorker;
            fractalGenerationWorker.RunWorkerCompleted += generateFractalComplete;
            //fractalGenerationWorker.WorkerReportsProgress = true;
            //fractalGenerationWorker.ProgressChanged += new ProgressChangedEventHandler(generateFractalReportProgress);


        }

        private void generateFractalWorker(object sender, DoWorkEventArgs e)
        {
            Mandelbrot man = new Mandelbrot(iterations);
            map = new Bitmap(Mwidth, Mheight);
            int test;
            for (int x = 0; x < Mwidth; x++)
            {
                test = (x ) * 100;
                
                for (int y = 0; y < Mheight; y++)
                {
                    double a = (double)(x - (Mwidth / 2)) / (double)(Mwidth / 4);
                    double b = (double)(y - (Mheight / 2)) / (double)(Mheight / 4);
                    Complex c = new Complex(a, b);
                    Complex z = new Complex(0, 0);
                    int it = man.countIterations(c, z);
                    map.SetPixel(x, y, man.getColor(it));
                  //  (sender as BackgroundWorker).ReportProgress(test);
                }
            }
          
        }

        private void generateFractalReportProgress(object sender, ProgressChangedEventArgs e)
        {

         //   progress.Value = e.ProgressPercentage;
        }

        private void generateFractalComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                map.Save("mandelbrot.png");
            }
            catch (Exception ed)
            {
                MessageBox.Show("Image mandelbrot.png could not be saved!");
            }

            try
            {
                mandelbrot.Source = ImageSourceFromBitmap(map);
            }
            catch (OutOfMemoryException oome)
            {
                MessageBox.Show("Image was to big to display!");
            }
            progress.Visibility = Visibility.Hidden;
            iter.IsEnabled = true;
            width.IsEnabled = true;
            height.IsEnabled = true;
            runButton.IsEnabled = true;
            SelectionEngine.IsEnabled = true;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int myNumber = Int32.Parse(iter.Text);
                int myWidth = Int32.Parse(width.Text);
                int myHeight = Int32.Parse(height.Text);
                Mwidth = myWidth;
                Mheight = myHeight;
                iterations = myNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Iterations amount must be valid Int!");
                return;
            }
            iter.IsEnabled = false;
            width.IsEnabled = false;
            height.IsEnabled = false;
            runButton.IsEnabled = false;
            SelectionEngine.IsEnabled = false;
            progress.Visibility = Visibility.Visible;
            fractalGenerationWorker.RunWorkerAsync();
            
           
           
        }
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
        private void SelectionEngine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionEngine.Text == "C#")
            {
                engine = EngineType.CSHARP;
            }else if(SelectionEngine.Text == "C++")
            {
                engine = EngineType.CPP;
            }
            else
            {
                engine = EngineType.MASM;
            }
        }
    }
}
