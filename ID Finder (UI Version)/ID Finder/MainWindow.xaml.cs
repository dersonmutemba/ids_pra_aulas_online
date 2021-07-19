using System.IO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections;

namespace ID_Finder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool dark = (bool) Properties.Settings.Default.mode;

        public MainWindow()
        {
            if (!File.Exists("horarios.db"))
            {
                Loading_Screen ls = new Loading_Screen();
                ls.Show();
                Controller.inicializarDados();
                ls.Close();
            }
            InitializeComponent();
            comboBox.SelectedIndex = (int)Properties.Settings.Default.turma;
            theme(!(bool) Properties.Settings.Default.mode);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 10000;
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            getAula();
        }

        private void theme(bool opt)
        {
            if (opt)
            {
                stackPanel.Background = null;
                labelAula.Foreground = Brushes.Black;
                labelDia.Foreground = Brushes.Black;
                labelID.Foreground = Brushes.Black;
                labelLista.Foreground = Brushes.Black;
                labelNotificacao.Foreground = Brushes.Black;
                labelPeriodo.Foreground = Brushes.Black;
                stackPanel2.Background = new SolidColorBrush(
                            Color.FromArgb(25, 0, 0, 0));
            }
            else
            {
                stackPanel.Background = new SolidColorBrush(Color.FromRgb(20, 20, 20));
                labelAula.Foreground = Brushes.White;
                labelDia.Foreground = Brushes.White;
                labelID.Foreground = Brushes.White;
                labelLista.Foreground = Brushes.White;
                labelNotificacao.Foreground = Brushes.White;
                labelPeriodo.Foreground = Brushes.White;
                stackPanel2.Background = new SolidColorBrush(
                            Color.FromArgb(25, 255, 255, 255));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            theme(dark);
            if (dark)
            {
                Properties.Settings.Default.mode = false;
                Properties.Settings.Default.Save();
                dark = false;
            }
            else
            {
                Properties.Settings.Default.mode = true;
                Properties.Settings.Default.Save();
                dark = true;
            }
        }

        private void getAula()
        {
            ArrayList weekday = new ArrayList
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            };

            string[] diasDaSemana = { "Segunda-feira", "Terça-feira",
                    "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado"};

            long now = DateTime.Now.Hour * 100 + DateTime.Now.Minute;
            Model model = Controller.getAula(
                ((ComboBoxItem)comboBox.Items.GetItemAt(comboBox.SelectedIndex))
                .Content.ToString(),
                weekday.IndexOf(DateTime.Now.DayOfWeek), now);
            if (model != null)
            {
                labelAula.Content = model.Aula;
                labelDia.Content = diasDaSemana[weekday.IndexOf(DateTime.Now.DayOfWeek)];
                labelID.Content = model.ID;
                labelPeriodo.Content = model.Inicio / 100 + ":" +
                                model.Inicio % 100 + " - " + model.Fim / 100 +
                                ":" + model.Fim % 100;
                progressBar.Value = (now - model.Inicio) * 100 / (model.Fim - model.Inicio);
                Console.WriteLine("Info: UI Updated at {0}", DateTime.Now);
                if (!model.Presenca.Equals("-"))
                    labelLista.Visibility = Visibility.Visible;
                else
                    labelLista.Visibility = Visibility.Hidden;
                labelLista.Content = model.Presenca;
                stackPanel2.Visibility = Visibility.Visible;
                labelNotificacao.Visibility = Visibility.Hidden;
            }
            else
            {
                stackPanel2.Visibility = Visibility.Hidden;
                labelNotificacao.Visibility = Visibility.Visible;
            }
        }

        private void buttonAbrir_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://zoom.us/j/" + labelID.Content);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsActive)
            {
                getAula();
                Properties.Settings.Default.turma = comboBox.SelectedIndex;
                Properties.Settings.Default.Save();
            }
        }

        private void window_ContentRendered(object sender, EventArgs e)
        {
            getAula();
        }

        private void labelLista_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(labelLista.Content.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/dersonmutemba");
        }
    }
}
