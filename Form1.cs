using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testes_Delegate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double n1 = 100;
            double n2 = 200;

            Action<double, double> dividir = Dividir;
            dividir(n1, n2);
        }
        void Dividir(double num1, double num2)
        {
            double resultado = num1 / num2;
            txtConsole.Text += string.Format(" Divisão : {0} ", resultado) + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var lista = new[] { "Santos", "Corinthians", "São Paulo", "Palmeiras" };

            Action<string> minhaAction = new Action<string>(EscreveLinha);

            Array.ForEach(lista, minhaAction);
        }

        void EscreveLinha(string Texto)
        {
            txtConsole.Text += string.Format(Texto + Environment.NewLine);
            txtConsole.SelectionStart = txtConsole.TextLength;
            txtConsole.ScrollToCaret();
            Application.DoEvents();
        }
        void EscreveLinha2(string Texto)
        {
            txtConsole2.Text += string.Format(Texto + Environment.NewLine);
            txtConsole2.SelectionStart = txtConsole2.TextLength;
            txtConsole2.ScrollToCaret();
            Application.DoEvents();
        }
        void CalculaIdade(Funcionario funci)
        {
            funci.Idade = DateTime.Now.Year - funci.Nascimento.Year;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Funcionario> funcionarios = Funcionario.GetFuncionarios();

            Action<Funcionario> actionFunc = new Action<Funcionario>(CalculaIdade);
            funcionarios.ForEach(actionFunc);

            foreach (var funci in funcionarios)
            {
                EscreveLinha(funci.Nome + " " + funci.Idade.ToString());
            }

        }

        void Atualiza(string Texto, int ValorAtual, int ValorMaximo)
        {
            lblDescricao.Text = Texto;
            pbBar.Maximum = ValorMaximo;
            pbBar.Value = ValorAtual;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(thread1);
            t.Start();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(thread2);
            t.Start();
        }

        private void thread1()
        {
            Action<string, int, int> actionProgresBar = new Action<string, int, int>(Atualiza);
            Action<string> escreveAction = new Action<string>(EscreveLinha);
            for (int i = 0; i < 1000; i++)
            {
                this.Invoke(escreveAction, new object[] { i.ToString() });
                this.Invoke(actionProgresBar, new object[] { "linha Adicionada " + i, i, 1000  });
                Thread.Sleep(100);
            }
        }

        private void thread2()
        {
            Action<string> escreveAction = new Action<string>(EscreveLinha2);
            for (int i = 0; i < 1000; i++)
            {
                this.Invoke(escreveAction, new object[] { i.ToString() });

                Thread.Sleep(100);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var httpClient = new HttpClient())
            {

                var content = new StringContent("", Encoding.UTF8, "application/json");
                content.Headers.Clear();
                content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                //HttpResponseMessage response = httpClient.GetAsync("http://localhost:54931/api/uf", content).Result;

                HttpResponseMessage response = httpClient.GetAsync("http://localhost:54931/api/uf").Result;

                if (response.IsSuccessStatusCode)
                {
                    EscreveLinha("ok");
                    var retornoApi = response.Content.ReadAsStringAsync();
                    DataResult dados = JsonConvert.DeserializeObject<DataResult>(retornoApi.Result);
                    dgDados.DataSource = dados.data;
                }
                else
                {
                    EscreveLinha("erro");
                }

            }

            //
            //var response = Call().Result;
        }

        private async Task<string> Call()
        {
            var parametros = new Dictionary<string, string>();
            parametros.Add("username", "004143");
            parametros.Add("password", "fpf@1212");
            parametros.Add("client_id", "selador");
            parametros.Add("grant_type", "password");

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:54931/api");
                var content = new FormUrlEncodedContent(parametros);
                //var request = await cliente.PostAsync("/auth", content);
                var request2 = await cliente.GetAsync("/uf");
                return await request2.Content.ReadAsStringAsync();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {


            Task task = new Task();
            task.EscreveLinha += EscreveLinha;

            EscreveLinha("Inicio:" + DateTime.Now.ToString());

            Coffee cup = task.PourCoffee();
            EscreveLinha("Café Preto está pronto");

            Egg eggs = task.FryEggs(2);
            EscreveLinha("Ovos estão prontos");

            Bacon bacon = task.FryBacon(3);
            EscreveLinha("Bacon estão prontos");

            Toast toast = task.ToastBread(2);
            task.ApplyButter(toast);
            task.ApplyJam(toast);
            EscreveLinha("Torradas estão prontos");

            Juice oj = task.PourOJ();
            EscreveLinha("Suco de Laranja estão prontos");
            EscreveLinha("Cafe da manha concluido!");

            EscreveLinha("Fim:" + DateTime.Now.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChamadaAssincronaAsync();
        }

        async System.Threading.Tasks.Task ChamadaAssincronaPlusAsync()
        {
            Task task = new Task();
            task.EscreveLinha += EscreveLinha;

            EscreveLinha("Inicio:" + DateTime.Now.ToString());

            Coffee cup = task.PourCoffee();
            EscreveLinha("Café preto está pronto");

            var eggsTask = task.FryEggsAsync(2);
            var baconTask = task.FryBaconAsync(3);
            var toastTask = task.MakeToastWithButterAndJamAsync(2);

            var breakfastTasks = new List<System.Threading.Tasks.Task> { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                System.Threading.Tasks.Task finishedTask = await System.Threading.Tasks.Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    EscreveLinha("ovos estão prontos");
                }
                else if (finishedTask == baconTask)
                {
                    EscreveLinha("Bacon estão prontos");
                }
                else if (finishedTask == toastTask)
                {
                    EscreveLinha("torrada Estão prontos");
                }
                breakfastTasks.Remove(finishedTask);
            }

            Juice oj = task.PourOJ();
            EscreveLinha("Suco de Laranja está pronto");
            EscreveLinha("Café da manha completo está pronto!");

            EscreveLinha("Fim:" + DateTime.Now.ToString());
        }

        async System.Threading.Tasks.Task ChamadaAssincronaAsync()
        {
            Task task = new Task();
            task.EscreveLinha += EscreveLinha;

            EscreveLinha("Inicio:" + DateTime.Now.ToString());

            Coffee cup = task.PourCoffee();
            EscreveLinha("Café Preto está pronto");

            Task<Egg> eggsTask = task.FryEggsAsync(2);
            Task<Bacon> baconTask = task.FryBaconAsync(3);
            Task<Toast> toastTask = task.ToastBreadAsync(2);

            Toast toast = await toastTask;
            task.ApplyButter(toast);
            task.ApplyJam(toast);
            EscreveLinha("Torradas estão prontos");
            Juice oj = task.PourOJ();
            EscreveLinha("Suco de Laranja estão prontos");

            Egg eggs = await eggsTask;
            EscreveLinha("Ovos estão prontos");
            Bacon bacon = await baconTask;
            EscreveLinha("Bacon estão prontos");

            EscreveLinha("Cafe da manha concluido!");

            EscreveLinha("Fim:" + DateTime.Now.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChamadaAssincronaPlusAsync();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtConsole.Text = "";
            txtConsole2.Text = "";
            pbBar.Value = 0;
            lblDescricao.Text = "";
            dgDados.DataSource = null;
        }
    }
}
