using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyLittleCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            result.Text = 0.ToString();
        }

        private void AddNumberToResult(double number)
        {
            if(char.IsNumber(result.Text.Last()))
            {
                if(result.Text.Length == 1 && result.Text == "0")
                {
                    result.Text = string.Empty;
                }
                result.Text += number;
            }
            else
            {
                if (number != 0)
                {
                    result.Text += number;
                }
            }
        }

        enum Operation { MINUS=1, PLUS=2, DIV=3, TIMES=4, NUMBER=5 }
        private void AddOperationToResult(Operation operation)
        {
            if (result.Text.Length == 1 && result.Text == "0") return;

            if (!char.IsNumber(result.Text.Last()))
            {
                result.Text = result.Text.Substring(0, result.Text.Length-1);
            }

            switch (operation)
            {
                case Operation.MINUS: result.Text += "-"; break;
                case Operation.PLUS: result.Text += "+"; break;
                case Operation.DIV: result.Text += "/"; break;
                case Operation.TIMES: result.Text += "x"; break;
            }
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            AddOperationToResult(Operation.PLUS);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(7);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(8);
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(9);
        }

        private void buttonMultiply_Click(object sender, RoutedEventArgs e)
        {
            AddOperationToResult(Operation.TIMES);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(4);
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(5);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(6);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(1);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(2);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(3);
        }

        private void buttonMinus_Click(object sender, RoutedEventArgs e)
        {
            AddOperationToResult(Operation.MINUS);
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            result.Text = 0.ToString();
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            AddNumberToResult(0);
        }

        private void buttonDivide_Click(object sender, RoutedEventArgs e)
        {
            AddOperationToResult(Operation.DIV);
        }

        private void buttonEquals_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
