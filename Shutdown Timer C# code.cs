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
using System.IO;
using System.Windows.Threading;

namespace ShutdownTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static StreamWriter streamWriter1 = new StreamWriter("1.txt");
        public static string string1 = "";
        public static int int1;
        public static int int2;
        public static int int3;
        public static float int5;
        public static int int6;
        public static string string2 = "Do not close this timer. It will interfere with the operation";
        public static string string3;
        public static Process process1 = new Process();
        public static StreamWriter streamWriter2 = new StreamWriter("batch1.bat");

        public TimeSpan timeSpan1 = new TimeSpan(0, 0, 0);
        public TimeSpan timeSpan2 = new TimeSpan(0, 0, 1);
        public MainWindow()
        {
            InitializeComponent();
            progressBar1.Value = 0;
            
            
        }

        void dispatcherTimer1_Tick(object sender, EventArgs e)
        {

            int5 = Int32.Parse(timerInput1.Text) + Int32.Parse(timerInput2.Text) * 60 + Int32.Parse(timerInput3.Text) * 3600 + 1;
            
            float double1 = (float)100 / int5;            
            progressBar1.Value = progressBar1.Value + double1;
                if (progressBar1.Value == 100)
                {
                    streamWriter1.WriteLine("@echo off");
                    streamWriter1.WriteLine("color 4");
                    streamWriter1.WriteLine("echo {0}", string2);
                    streamWriter1.WriteLine(string3);
                    streamWriter1.Close();
                    StreamReader streamReader1 = new StreamReader("1.txt");
                    while (string1 != null)
                    {
                        if (string1 != null)
                        {
                            string1 = streamReader1.ReadLine();
                            streamWriter2.WriteLine(string1);
                        }
                    }
                    streamWriter2.Close();
                    streamReader1.Close();
                    process1.StartInfo.FileName = "batch1.bat";
                    process1.Start(); 
                }

            label1.Content = timeSpan1;
            timeSpan1 = timeSpan1 - timeSpan2;
            if (timeSpan1 == new TimeSpan(0,0,0))
            {
                timeSpan1 = new TimeSpan(0, 0, 0);
            }

            logOffButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
            
        }

        private void shutdownButton_MouseEnter(object sender, MouseEventArgs e)
        {
            message1.Text = "Set a timer to shutdown your computer.";
            shutdownButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }

        private void shutdownButton_MouseLeave(object sender, MouseEventArgs e)
        {
            message1.Text = "";
            shutdownButton.Background = (LinearGradientBrush)Resources["BUTTONNOTSELECT"];
        }

        private void restartButton_MouseEnter(object sender, MouseEventArgs e)
        {
            message2.Text="Set a timer to restart your computer.";
            restartButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }

        private void restartButton_MouseLeave(object sender, MouseEventArgs e)
        {
            message2.Text = null;
            restartButton.Background = (LinearGradientBrush)Resources["BUTTONNOTSELECT"];
        }

        private void logOffButton_MouseEnter(object sender, MouseEventArgs e)
        {
            message3.Text = "Set a timer to log off your computer.";
            logOffButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }

        private void logOffButton_MouseLeave(object sender, MouseEventArgs e)
        {
            message3.Text = "";
            logOffButton.Background = (LinearGradientBrush)Resources["BUTTONNOTSELECT"];
        }

        private void TimerLabel1_MouseEnter(object sender, MouseEventArgs e)
        {
            message4.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void TimerLabel1_MouseLeave(object sender, MouseEventArgs e)
        {
            message4.Text = "";
        }

        public void restartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                int1 = Int32.Parse(timerInput1.Text) + Int32.Parse(timerInput2.Text) * 60 + Int32.Parse(timerInput3.Text) * 3600;
                string3 = "shutdown /r /t 0";

                
                DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
                dispatcherTimer1.Tick += dispatcherTimer1_Tick;
                dispatcherTimer1.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer1.Start();
                progressBar1.Foreground = (LinearGradientBrush)Resources["PROGRESSBARFOREGROUNDGREEN"];
                finalWarning2.Text = "If you wish to cancel and/or perform another operation, close the program and run it again";

                timeSpan1 = new TimeSpan(0,0, int1 - 1);

                restartButton.IsEnabled = false;
                shutdownButton.IsEnabled = false;
                logOffButton.IsEnabled = false;

                shutdownButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
                restartButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
                logOffButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
            }
            catch (FormatException)
            {
                finalWarning2.Text = "The timing is not in correct format. If blank, type 0 instead.";
            }
            
        }

        private void logOffButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int2 = Int32.Parse(timerInput1.Text) + Int32.Parse(timerInput2.Text) * 60 + Int32.Parse(timerInput3.Text) * 3600;
                string3 = "shutdown /l";
                
                DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
                dispatcherTimer1.Tick += dispatcherTimer1_Tick;
                dispatcherTimer1.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer1.Start();
                progressBar1.Foreground = (LinearGradientBrush)Resources["PROGRESSBARFOREGROUNDYELLOW"];
                finalWarning2.Text = "If you wish to cancel and/or perform another operation, close the program and run it again";

                timeSpan1 = new TimeSpan(0, 0, int2 - 1);

                restartButton.IsEnabled = false;
                shutdownButton.IsEnabled = false;
                logOffButton.IsEnabled = false;
            }
            catch (FormatException)
            {
                finalWarning2.Text = String.Format("The timing is not in correct format. If blank, type 0 instead.");
            }
            
        }

        private void shutdownButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int1 = Int32.Parse(timerInput1.Text) + Int32.Parse(timerInput2.Text) * 60 + Int32.Parse(timerInput3.Text) * 3600;
                string3 = "shutdown /s /t 0";

                DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
                dispatcherTimer1.Tick += dispatcherTimer1_Tick;
                dispatcherTimer1.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer1.Start();
                progressBar1.Foreground = (LinearGradientBrush)Resources["PROGRESSBARFOREGROUNDRED"];
                timeSpan1 = new TimeSpan(0, 0, int1 - 1);

                finalWarning2.Text = "If you wish to cancel and/or perform another operation, close the program and run it again";

                restartButton.IsEnabled = false;
                shutdownButton.IsEnabled = false;
                logOffButton.IsEnabled = false;
            }
            catch (FormatException)
            {
                finalWarning2.Text = "The timing is not in correct format. If blank, type 0 instead.";
            }
            catch (FileNotFoundException)
            {
                finalWarning2.Text = "Some files are missing. Make sure 1.txt and batch1.bat with Shutdown Timer.";
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Exit();
        }

        private void restartButton_GotFocus(object sender, RoutedEventArgs e)
        {
            restartButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }

        private void logOffButton_GotFocus(object sender, RoutedEventArgs e)
        {
            logOffButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }

        private void shutdownButton_GotFocus(object sender, RoutedEventArgs e)
        {
            shutdownButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }

        private void shutdownButton_LostFocus(object sender, RoutedEventArgs e)
        {
            shutdownButton.Background = (LinearGradientBrush)Resources["BUTTONNOTSELECT"];
        }

        private void logOffButton_LostFocus(object sender, RoutedEventArgs e)
        {
            logOffButton.Background = (LinearGradientBrush)Resources["BUTTONNOTSELECT"];
        }

        private void restartButton_LostFocus(object sender, RoutedEventArgs e)
        {
            restartButton.Background = (LinearGradientBrush)Resources["BUTTONNOTSELECT"];
        }

        private void timerInput3_GotFocus(object sender, RoutedEventArgs e)
        {
            timerInput3.SelectAll();
        }

        private void timerInput3_LostFocus(object sender, RoutedEventArgs e)
        {
            message4.Text = "";
        }

        private void timerInput2_GotFocus(object sender, RoutedEventArgs e)
        {
            timerInput2.SelectAll();
        }

        private void timerInput2_LostFocus(object sender, RoutedEventArgs e)
        {
            message4.Text = "";
        }

        private void timerInput1_GotFocus(object sender, RoutedEventArgs e)
        {
            timerInput1.SelectAll();
        }
        private void timerInput1_LostFocus(object sender, RoutedEventArgs e)
        {
            message4.Text = "";
        }

        private void timerInput3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timerInput3.SelectAll();
        }

        private void timerInput2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timerInput2.SelectAll();
        }

        private void timerInput1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timerInput1.SelectAll();
        }

        private void timerInput1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            timerInput1.SelectAll();
        }

        private void timerInput3_TouchEnter(object sender, TouchEventArgs e)
        {
            timerInput3.SelectAll();
            message4.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void timerInput2_TouchDown(object sender, TouchEventArgs e)
        {
            timerInput2.SelectAll();
            message4.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void timerInput1_TouchDown(object sender, TouchEventArgs e)
        {
            timerInput1.SelectAll();
            message4.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void timerInput3_MouseEnter(object sender, MouseEventArgs e)
        {
            message4.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void timerInput3_MouseLeave(object sender, MouseEventArgs e)
        {
            message4.Text = "";
        }

        private void timerInput2_MouseEnter(object sender, MouseEventArgs e)
        {
            message4.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void timerInput2_MouseLeave(object sender, MouseEventArgs e)
        {
            message4.Text = "";
        }

        private void timerInput1_MouseEnter(object sender, MouseEventArgs e)
        {
            message4.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void timerInput1_MouseLeave(object sender, MouseEventArgs e)
        {
            message4.Text = "";
        }

        private void timerInput1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            timerInput1.SelectAll();
        }

        private void timerInput3_GotMouseCapture(object sender, MouseEventArgs e)
        {
            timerInput3.SelectAll();
        }

        


        



    }   
}