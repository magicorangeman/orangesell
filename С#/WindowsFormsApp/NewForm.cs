using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    class NewForm : Form
    {
        public NewForm()
        {
            var label = new Label();
            label.Location = new Point(0, 0);
            label.Size = new Size(ClientSize.Width, 30);
            label.Text = "Введите число";
            Controls.Add(label);

            var input = new TextBox();
            input.Location = new Point(0, label.Bottom);
            input.Size = label.Size;
            Controls.Add(input);

            var button = new Button();
            button.Location = new Point(0, input.Bottom);
            button.Size = label.Size;
            button.Text = "Увеличить";
            button.Click += (sender, args) =>
            {
                var number = int.Parse(input.Text);
                number++;
                input.Text = number.ToString();
            };
            Controls.Add(button);
        }

        public static void Main()
        {
            Application.Run(new NewForm());
        }
    }
}
