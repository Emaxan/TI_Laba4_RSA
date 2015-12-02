using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace TI_Laba4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private State _state;

        public MainWindow()
        {
            InitializeComponent();
            Crypting.IsChecked = true;
            Controller.SetEvents(SetPValue, SetQValue, SetRValue, SetKoValue, SetKcValue);
        }

        private void State_OnChecked(object sender, RoutedEventArgs e)
        {
            if (Equals(e.OriginalSource, Hack))
                _state = State.Hack;
            else
                if (Equals(e.OriginalSource, Crypting))
                    _state = State.Coding;
                else
                    if (Equals(e.OriginalSource, Decrypting))
                        _state = State.Decoding;
        }

        private void BWork_Click(object sender, RoutedEventArgs e)
        {
            string result;
            switch (Controller.InitializeModel(TBF.Text, "p", TBP.Text, "q", TBQ.Text, "r", TBR.Text, "kc", TBC.Text, "ko",
                    TBO.Text, "state", ((int)_state).ToString()))
            {
                case 0:
                    result = Controller.ReturnResult();
                    break;
                case 1:
                    result = "Incorrect data for Coding! Enter P, Q, Kc!";
                    break;
                case 2:
                    result = "Multiplication of P and Q lower then 256 or greather then 65537!";
                    break;
                case 3:
                    result = "File path is wrong!";
                    break;
                case 4:
                    result = "Incorrect data for Decrypting! Enter R and Kc!";
                    break;
                case 5:
                    result = "Incorrect data for Hacking! Enter R and Ko!";
                    break;
                case 6:
                    result = "R lower than 256 or greather than 65537!";
                    break;
                case 7:
                    result = "P isn't simple!";
                    break;
                case 8:
                    result = "Q isn't simple!";
                    break;
                case 9:
                    result = "R and Secret Key not relatively simple!";
                    break;
                case 10:
                    result = "R and Opened Key not relatively simple!";
                    break;
                default:
                    result = "Error. Incorrect data!";
                    break;
            }
            TbResult.Text = result;
        }

        private void BExit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void SetRValue(ulong r)
        {
            TBR.Text = r.ToString();
        }

        public void SetQValue(ulong q)
        {
            TBQ.Text = q.ToString();
        }

        public void SetPValue(ulong p)
        {
            TBP.Text = p.ToString();
        }

        public void SetKcValue(ulong kc)
        {
            TBC.Text = kc.ToString();
        }

        public void SetKoValue(ulong ko)
        {
            TBO.Text = ko.ToString();
        }

        private void OpenFile(object sender, MouseButtonEventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Multiselect = false,
                Title = "Open File...",
                InitialDirectory = @"E:\Programs\VS\_TI\TI_Laba4\"
            };
            if (ofd.ShowDialog() == true)
            {
                TBF.Text = ofd.FileNames[0];
            }
        }

        private void BGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            if (_state != State.Coding) return;
            var rand = new Random();
            Processor.IsSimple(4096);
            var p = Processor.GetSimple();
            TBP.Text = p.ToString();
            var q = Processor.GetSimple();
            while (p * q < 256 || p * q > 65536 || p == q)
                q = Processor.GetSimple();
            TBQ.Text = q.ToString();
            var eu = Processor.EulerFunc(p * q);
            var kc = (ulong)rand.Next(4096);
            while (Processor.Gcd(kc, eu) != 1)
                kc = (ulong)rand.Next(4096);
            TBC.Text = kc.ToString();
        }
    }
}
