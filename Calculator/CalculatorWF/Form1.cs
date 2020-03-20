using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CalculatorWF
{
    public partial class Calculator : Form
    {
        private const string INVALID_EXPRESSION = "Invalid expression";
        private readonly List<Button> expressionButtons;
        private string expression;
        public Calculator()
        {
            InitializeComponent();
            expressionButtons = new List<Button> { buttonPlus, buttonMinus, buttonDivision, buttonMultiplication };
            AddEventToAllButtons();
            AddEventToInput();
        }

        private void AddEventToInput()
        {
            input.TextChanged += TextChangedEvent;
            input.KeyPress += Input_KeyPressEvent;
        }
        private void Input_KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar);
        }
        private void TextChangedEvent(object sender, EventArgs e)
        {
            expressionLabel.Text = expression;
        }

        private void AddEventToAllButtons()
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.MouseClick += Click_Button;
            }
        }
        private void Click_Button(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            if (currentButton == buttonEqual)
            {
                expression += input.Text;
                input.Text = Eval(expression);
                expression = "";
            }
            else if (currentButton == buttonClear)
            {
                expression = "";
                input.Text = "";
            }
            else if (currentButton == buttonDelete)
                input.Text = input.Text.Remove(input.Text.Length - 1);
            else if (currentButton == buttonNegate) {
                try
                {
                    char firstCharacter = input.Text[0];
                    if (firstCharacter == '-')
                        input.Text = input.Text.Remove(0, 1);
                    else
                        input.Text = input.Text.Insert(0, "-");
                }
                catch
                {
                    input.Text += "-";
                }
            }
            else if (expressionButtons.Contains(currentButton)) {
                expression += input.Text + currentButton.Text;
                input.Text = "";
            }
            else if (input.Text != INVALID_EXPRESSION)
                input.Text += currentButton.Text;
            else
                input.Text = currentButton.Text;
        }
        private string Eval(string strinForEval)
        {
            try
            {
                return new DataTable().Compute(strinForEval, "").ToString();
            }
            catch
            {
                return INVALID_EXPRESSION;
            }
        }
    }
}
