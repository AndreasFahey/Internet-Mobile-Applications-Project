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
    /// This is My App "My Little Calculator" Ive always wanted to make a calculator
    /// through code. It may be very basic but it is what i wanted to do. 
    /// This is basically a pocket calculator hense the name, you never know when you need
    /// to calculate something. Enjoy!
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
                }//inner if
                result.Text += number;
            }//outer if
            else
            {
                if (number != 0)
                {
                    result.Text += number;
                }//inner if
            }//else
        }
        //math operation expression
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
            }//switch
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
        //used region to expand/collapse block of code 
        #region Equal
        private class Operand
        {
            public Operation operation = Operation.NUMBER;//DEFAULT
            public double value = 0;
            public Operand left = null;
            public Operand right = null;
        }
        //expression from result.Text then Build the Tree
        //Operand for quantity
        private Operand BuildTreeOperand()
        {
            Operand tree = null;
            string expression = result.Text;
            if(!char.IsNumber(expression.Last()))
            {
                expression = expression.Substring(0, expression.Length - 1);
            }
            string numberStr = string.Empty;
            foreach (char c in expression.ToCharArray())
            {
                if (char.IsNumber(c) || c == '.' || numberStr == string.Empty && c == '-')
                {
                    numberStr += c;
                }
                else
                {
                    AddOperandToTree(ref tree, new Operand() { value = double.Parse(numberStr) });
                    numberStr = string.Empty;
                    Operation op = Operation.MINUS;//DEFAULT
                    switch (c)
                    {
                        case '-': op = Operation.MINUS; break;
                        case '+': op = Operation.PLUS; break;
                        case '/': op = Operation.DIV; break;
                        case 'x': op = Operation.TIMES; break;
                    }//switch
                    AddOperandToTree(ref tree, new Operand() { operation = op });
                }
            }
            //Last Number
            AddOperandToTree(ref tree, new Operand { value = double.Parse(numberStr) });

            return tree;
        }
        private void AddOperandToTree(ref Operand tree, Operand elem)
        {
            if (tree == null)
            {
                tree = elem;
            }
            else
            {
                if (elem.operation < tree.operation)
                {
                    Operand auxTree = tree;
                    tree = elem;
                    elem.left = auxTree;
                }
                else
                {
                    AddOperandToTree(ref tree.right, elem);//Recursive
                }
            }
        }

        private double Calc(Operand tree)
        {
            if (tree.left == null && tree.right == null)//number
            {
                return tree.value;
            }
            else //operation (+,-,x,/)
            {
                double subResult = 0;
                switch (tree.operation)
                {
                    case Operation.MINUS: subResult = Calc(tree.left) - Calc(tree.right); break;
                    case Operation.PLUS: subResult = Calc(tree.left) + Calc(tree.right); break;
                    case Operation.DIV: subResult = Calc(tree.left) / Calc(tree.right); break;
                    case Operation.TIMES: subResult = Calc(tree.left) * Calc(tree.right); break;
                }
                return subResult;
            }
        }
        private void buttonEquals_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(result.Text)) return;
            Operand tree = BuildTreeOperand();//result.Text

            double value = Calc(tree);//calc final result
            result.Text = value.ToString();
        }
        #endregion
    }
}//end main xaml cs
