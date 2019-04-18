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
        public static int totalTime;
        
        public static string warningNotClose = "Do not close this timer. It will interfere with the operation";
        public static string shutdownCommand;
        public static Process process1 = new Process();
        public static StreamWriter streamWriter2 = new StreamWriter("batch1.bat");

        public TimeSpan timeSpan1 = new TimeSpan(0, 0, 0);
        public TimeSpan timeSpanSecond = new TimeSpan(0, 0, 1); //1 second per "tick"
        public MainWindow()
        {
            InitializeComponent();
            progressBar1.Value = 0;
        }

        void dispatcherTimer1_Tick(object sender, EventArgs e)
        {
            //int5 = Int32.Parse(secondInput.Text) + Int32.Parse(minuteInput.Text) * 60 + Int32.Parse(hourInput.Text) * 3600 + 1;   
            //float double1 = (float)100 / int5;            
            progressBar1.Value = progressBar1.Value + 1;
            if (progressBar1.Value == totalTime)
            {
                streamWriter1.WriteLine("@echo off");
                streamWriter1.WriteLine("color 4");
                streamWriter1.WriteLine("echo {0}", warningNotClose);
                streamWriter1.WriteLine(shutdownCommand);
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
            timeSpan1 = timeSpan1 - timeSpanSecond;
            if (timeSpan1 == new TimeSpan(0,0,0))
            {
                timeSpan1 = new TimeSpan(0, 0, 0);
            }

            logOffButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }

        private void shutdownButton_MouseEnter(object sender, MouseEventArgs e)
        {
            shutdownInstructButton.Text = "Set a timer to shutdown your computer.";
            shutdownButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }


        private void shutdownButton_MouseLeave(object sender, MouseEventArgs e)
        {
            shutdownInstructButton.Text = "";
            shutdownButton.Background = (LinearGradientBrush)Resources["BUTTONNOTSELECT"];
        }

        private void restartButton_MouseEnter(object sender, MouseEventArgs e)
        {
            restartInstructButton.Text="Set a timer to restart your computer.";
            restartButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }

        private void restartButton_MouseLeave(object sender, MouseEventArgs e)
        {
            restartInstructButton.Text = null;
            restartButton.Background = (LinearGradientBrush)Resources["BUTTONNOTSELECT"];
        }

        private void logOffButton_MouseEnter(object sender, MouseEventArgs e)
        {
            logOffInstructButton.Text = "Set a timer to log off your computer.";
            logOffButton.Background = (LinearGradientBrush)Resources["BUTTONSELECTCOLOR2"];
        }

        private void logOffButton_MouseLeave(object sender, MouseEventArgs e)
        {
            logOffInstructButton.Text = "";
            logOffButton.Background = (LinearGradientBrush)Resources["BUTTONNOTSELECT"];
        }

        private void TimerLabel1_MouseEnter(object sender, MouseEventArgs e)
        {
            timerInstructLabel.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void TimerLabel1_MouseLeave(object sender, MouseEventArgs e)
        {
            timerInstructLabel.Text = "";
        }

        public void restartButton_Click(object sender, RoutedEventArgs e)
        {
            int hourTime = 0, minuteTime = 0, secondTime = 0;
            CheckTimeInputFormat(ref hourTime, ref minuteTime, ref secondTime);

            totalTime = hourTime + minuteTime + secondTime;
            shutdownCommand = "shutdown /r /t 0";

            DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
            dispatcherTimer1.Tick += dispatcherTimer1_Tick;
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer1.Start();
            progressBar1.Foreground = (LinearGradientBrush)Resources["PROGRESSBARFOREGROUNDGREEN"];
            finalWarning2.Text = "If you wish to cancel and/or perform another operation, close the program and run it again";

            timeSpan1 = new TimeSpan(0,0, totalTime - 1);
            DisableAllButtons();

            
        }

        private void logOffButton_Click(object sender, RoutedEventArgs e)
        {
            int hourTime = 0, minuteTime = 0, secondTime = 0;
            CheckTimeInputFormat(ref hourTime, ref minuteTime, ref secondTime);
            totalTime = hourTime + minuteTime + secondTime;
            shutdownCommand = "shutdown /l";
                
            DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
            dispatcherTimer1.Tick += dispatcherTimer1_Tick;
            dispatcherTimer1.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer1.Start();
            progressBar1.Foreground = (LinearGradientBrush)Resources["PROGRESSBARFOREGROUNDYELLOW"];
            finalWarning2.Text = "If you wish to cancel and/or perform another operation, close the program and run it again";

            timeSpan1 = new TimeSpan(0, 0, totalTime - 1);
            DisableAllButtons();
            
            
        }

        private void shutdownButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int hourTime = 0, minuteTime = 0, secondTime = 0;
                CheckTimeInputFormat(ref hourTime, ref minuteTime, ref secondTime);

                totalTime = hourTime + minuteTime + secondTime;
                shutdownCommand = "shutdown /s /t 0";

                DispatcherTimer dispatcherTimer1 = new DispatcherTimer();
                dispatcherTimer1.Tick += dispatcherTimer1_Tick;
                dispatcherTimer1.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer1.Start();
                progressBar1.Foreground = (LinearGradientBrush)Resources["PROGRESSBARFOREGROUNDRED"];
                timeSpan1 = new TimeSpan(0, 0, totalTime - 1);

                finalWarning2.Text = "If you wish to cancel and/or perform another operation, close the program and run it again";

                DisableAllButtons();
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

        private void hourInput_GotFocus(object sender, RoutedEventArgs e)
        {
            hourInput.Text = "";
        }

        private void hourInput_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int hourLength = Int32.Parse(hourInput.Text);
            }
            catch (FormatException)
            {
                hourInput.Text = "Hours";
            }

            timerInstructLabel.Text = "";
        }

        private void minuteInput_GotFocus(object sender, RoutedEventArgs e)
        {
            minuteInput.Text = "";
        }

        private void minuteInput_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int minuteLength = Int32.Parse(minuteInput.Text);
            }
            catch (FormatException)
            {
                minuteInput.Text = "Minutes";
            }
            
            timerInstructLabel.Text = "";
        }

        private void secondInput_GotFocus(object sender, RoutedEventArgs e)
        {
            secondInput.Text = "";
        }
        private void secondInput_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int secondLength = Int32.Parse(secondInput.Text);
            }
            catch (FormatException)
            {
                secondInput.Text = "Seconds";
            }
            
            timerInstructLabel.Text = "";
        }

        private void hourInput_TouchEnter(object sender, TouchEventArgs e)
        {
            hourInput.SelectAll();
            timerInstructLabel.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void minuteInput_TouchDown(object sender, TouchEventArgs e)
        {
            minuteInput.SelectAll();
            timerInstructLabel.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void secondInput_TouchDown(object sender, TouchEventArgs e)
        {
            secondInput.SelectAll();
            timerInstructLabel.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void hourInput_MouseEnter(object sender, MouseEventArgs e)
        {
            timerInstructLabel.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void hourInput_MouseLeave(object sender, MouseEventArgs e)
        {
            timerInstructLabel.Text = "";
        }

        private void minuteInput_MouseEnter(object sender, MouseEventArgs e)
        {
            timerInstructLabel.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void minuteInput_MouseLeave(object sender, MouseEventArgs e)
        {
            timerInstructLabel.Text = "";
        }

        private void secondInput_MouseEnter(object sender, MouseEventArgs e)
        {
            timerInstructLabel.Text = "Set a countdown for any operation below. If blank, please type 0 in the box";
        }

        private void secondInput_MouseLeave(object sender, MouseEventArgs e)
        {
            timerInstructLabel.Text = "";
        }

        public void DisableAllButtons()
        {
            restartButton.IsEnabled = false;
            shutdownButton.IsEnabled = false;
            logOffButton.IsEnabled = false;
        }

        public void CheckTimeInputFormat(ref int hourTime, ref int minuteTime, ref int secondTime)
        {
            try
            {
                hourTime = Int32.Parse(hourInput.Text) * 3600;
            }
            catch (FormatException)
            {
                hourTime = 0;
            }

            try
            {
                minuteTime = Int32.Parse(minuteInput.Text) * 60;
            }
            catch (FormatException)
            {
                minuteTime = 0;
            }

            try
            {
                secondTime = Int32.Parse(secondInput.Text);
            }
            catch (FormatException)
            {
                secondTime = 0;
                if (hourTime == 0 && minuteTime == 0 && secondTime == 0)
                {
                    secondTime = 10;
                }
                progressBar1.Maximum = hourTime + minuteTime + secondTime;
            }
        }
    }   
}