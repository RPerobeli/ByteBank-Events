using ByteBank.Agencias.DAL;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ByteBank.Agencias
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ByteBankEntities _contextoBancoDeDados = new ByteBankEntities();
        private readonly ListBox lstAgencias;

        public MainWindow()
        {
            InitializeComponent();

            lstAgencias = new ListBox();
            AtualizarControles();
        }

        private void AtualizarControles()
        {
            lstAgencias.Width = 270;
            lstAgencias.Height = 290;

            Canvas.SetTop(lstAgencias, 15);
            Canvas.SetLeft(lstAgencias, 15);

            lstAgencias.SelectionChanged += new SelectionChangedEventHandler(lstAgencias_SelectionChanged);

            container.Children.Add(lstAgencias);

            btnEditar.Click += new RoutedEventHandler(btnEditar_Click);

            AtualizarListaAgencias();
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Agencia agenciaAtual = lstAgencias.SelectedItem as Agencia;
            var janelaEdicao = new EdicaoAgencias(agenciaAtual);
            bool resultado = janelaEdicao.ShowDialog().Value;
            if(resultado)
            {

            }else
            {

            }
        }
        private void AtualizarListaAgencias()
        {
            lstAgencias.Items.Clear();
            var agencias = _contextoBancoDeDados.Agencias.ToList();
            foreach (var agencia in agencias)
                lstAgencias.Items.Add(agencia);
        }

        private void lstAgencias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var agenciaSelecionada = lstAgencias.SelectedItem as Agencia;
            txtNumero.Text = agenciaSelecionada.Nome;
            txtNome.Text = agenciaSelecionada.Nome;
            txtTelefone.Text = agenciaSelecionada.Telefone;
            txtEndereco.Text = agenciaSelecionada.Endereco;
            txtDescricao.Text = agenciaSelecionada.Descricao;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var confirmacao = 
                MessageBox.Show(
                    "Você deseja realmente excluir este item?",
                    "Confirmação",
                    MessageBoxButton.YesNo);
            if (confirmacao == MessageBoxResult.Yes)
            {
                //Excluir
            }
            else
            {
                //Não faz nada
            }
        }
    }
}
