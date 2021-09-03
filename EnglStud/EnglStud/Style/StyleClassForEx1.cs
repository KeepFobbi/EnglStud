using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglStud.Style
{
    class StyleClassForEx1
    {
        private void Studing_word_TxtBlock_0_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var temp = sender as TextBlock;
            if (Studing_word_TxtBlock_Main.Tag.ToString() == temp.Text)
            {
                MessageBox.Show("+");
            }
        }

        private void Studing_word_TxtBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            var temp = sender as TextBlock;
            temp.Foreground = Brushes.Red;
        }

        private void Studing_word_TxtBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            var temp = sender as TextBlock;
            temp.Foreground = Brushes.Black;
        }
    }
}
