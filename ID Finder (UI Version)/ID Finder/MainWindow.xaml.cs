using MongoDB.Bson;
using MongoDB.Driver;
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
        private ProximaAula proximaAula = new ProximaAula(0, "");
        private string linkPresenca = "";

        public MainWindow()
        {
            if (!File.Exists("horarios.db"))
            {
                Inicializacao();
            }
            if (!(bool) Properties.Settings.Default.updated)
            {
                File.Delete("horarios.db");
                Inicializacao();
                Properties.Settings.Default.updated = true;
                Properties.Settings.Default.Save();
            }
            InitializeComponent();
            comboBox.SelectedIndex = (int)Properties.Settings.Default.turma;
            theme(!(bool) Properties.Settings.Default.mode);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 10000;
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
            timer.Start();
            SearchUpdate();
            
        }

        private void Inicializacao()
        {
            Loading_Screen ls = new Loading_Screen();
            ls.Show();
            Controller.inicializarDados();
            ls.Close();
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
            proximaAula = new ProximaAula(0, "");

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
                if (!model.Presenca.Equals("-"))
                    labelLista.Visibility = Visibility.Visible;
                else
                    labelLista.Visibility = Visibility.Hidden;
                linkPresenca = model.Presenca;
                stackPanel2.Visibility = Visibility.Visible;
                labelNotificacao.Visibility = Visibility.Hidden;
            }
            else
            {
                stackPanel2.Visibility = Visibility.Hidden;
                labelNotificacao.Visibility = Visibility.Visible;
            }

            if (model?.Fim/100 == DateTime.Now.Hour || model == null)
            {
                now = DateTime.Now.Hour * 100 + DateTime.Now.Minute + 30;
                model = Controller.getAula(
                ((ComboBoxItem)comboBox.Items.GetItemAt(comboBox.SelectedIndex))
                .Content.ToString(),
                weekday.IndexOf(DateTime.Now.DayOfWeek), now);
                if(model != null)
                    proximaAula = new ProximaAula(model.ID, model.Aula);
            }
            else
            {
                now = (DateTime.Now.Hour + 1) * 100 + DateTime.Now.Minute;
                Model m = Controller.getAula(
                ((ComboBoxItem)comboBox.Items.GetItemAt(comboBox.SelectedIndex))
                .Content.ToString(),
                weekday.IndexOf(DateTime.Now.DayOfWeek), now);
                if (m != null && !model.Aula.Equals(m.Aula))
                    proximaAula = new ProximaAula(m.ID, m.Aula);
                else
                {
                    now = (DateTime.Now.Hour + 2) * 100 + DateTime.Now.Minute;
                    model = Controller.getAula(
                    ((ComboBoxItem)comboBox.Items.GetItemAt(comboBox.SelectedIndex))
                    .Content.ToString(),
                    weekday.IndexOf(DateTime.Now.DayOfWeek), now);
                    if (model != null)
                        proximaAula = new ProximaAula(model.ID, model.Aula);
                }
            }
            if (proximaAula.ID == 0)
                buttonSeguir.Visibility = Visibility.Hidden;
            else
            {
                buttonSeguir.Visibility = Visibility.Visible;
                buttonSeguir.ToolTip = proximaAula.Cadeira;
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
            System.Diagnostics.Process.Start(linkPresenca);
        }

        private void labelLista_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkPresenca);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/dersonmutemba");
        }

        private void buttonSeguir_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://zoom.us/j/" + proximaAula.ID);
        }

        private void SearchUpdate()
        {
            try
            {
                var client = new MongoClient(
                    "mongodb+srv://client:BU2cK8OB58mdk6QJ@clusteridfinder.6ocgc.mongodb.net/ClusterIDFinder?retryWrites=true&w=majority"
                );
                var database = client.GetDatabase("horarios");
                var collection = database.GetCollection<BsonDocument>("update");
                var dados = collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();

                foreach(var dado in dados)
                {
                    Version version = new Version(dado["version"].ToString(),
                        DateTime.FromFileTime(long.Parse(dado["lancamento"].ToString())), dado["mudancas"].ToString(),
                        dado["link"].ToString());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public struct ProximaAula
    {
        public ProximaAula(long id, string cadeira)
        {
            ID = id;
            Cadeira = cadeira;
        }

        public long ID { get; set; }
        public string Cadeira { get; set; }

    }

    public struct Version
    {
        public Version(string versao, DateTime lancamento, string mudancas, string link)
        {
            Versao = versao;
            Lancamento = lancamento;
            Mudancas = mudancas;
            Link = link;
            if(!_antigaVersao.Equals(Versao))
                Notify();
        }

        void Notify()
        {
            System.Windows.Forms.NotifyIcon notify = new System.Windows.Forms.NotifyIcon();
            notify.Icon = new System.Drawing.Icon("ID.ico");
            notify.BalloonTipTitle = "Update! " + Versao;
            notify.BalloonTipText = Mudancas + "\nClique pra actualizar.";
            notify.Text = Mudancas + "\nClique pra actualizar.";
            notify.Click += Notify_Click;
            notify.DoubleClick += Notify_DoubleClick;
            notify.BalloonTipClicked += Notify_BalloonTipClicked;
            notify.MouseClick += Notify_MouseClick;
            notify.Visible = true;
            notify.ShowBalloonTip(50000);
        }

        private void Notify_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Accao();
        }

        private void Notify_BalloonTipClicked(object sender, EventArgs e)
        {
            Accao();
        }

        private void Notify_DoubleClick(object sender, EventArgs e)
        {
            Accao();
        }

        private void Notify_Click(object sender, EventArgs e)
        {
            Accao();
        }

        private void Accao()
        {
            System.Diagnostics.Process.Start(Link);
        }

        private const string _antigaVersao = "1.0.0.5";
        public string Versao { get; set; }
        public DateTime Lancamento { get; set; }
        public string Mudancas { get; set; }
        public string Link { get; set; }

    }
}
