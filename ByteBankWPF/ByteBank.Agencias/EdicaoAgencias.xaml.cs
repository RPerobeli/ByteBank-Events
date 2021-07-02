using ByteBank.Agencias.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ByteBank.Agencias
{
    /// <summary>
    /// Lógica interna para EdicaoAgencias.xaml
    /// </summary>
    public partial class EdicaoAgencias : Window
    {
        private readonly Agencia _agencia;
        public EdicaoAgencias(Agencia agencia)
        {
            InitializeComponent();
            _agencia = agencia ?? throw new ArgumentNullException(nameof(agencia));
            AtualizarCamposTexto();
            AtualizarControles();
        }

        private void AtualizarCamposTexto()
        {
            txtNumero.Text = _agencia.Numero;
            txtNome.Text = _agencia.Nome;
            txtTelefone.Text = _agencia.Telefone;
            txtEndereco.Text = _agencia.Endereco;
            txtDescricao.Text = _agencia.Descricao;
        }
        private void AtualizarControles()
        {
            //De forma mais simples ainda, é possivel tornar o delegate mais sucinto com expressões lambda
            RoutedEventHandler dialogResultTrue = (o, e) => DialogResult = true;
            RoutedEventHandler dialogResultFalse = (o, e) => DialogResult = false;

            //Açucar sintático: e possível usar funções anonimas com a palavra reservada delegate, evitando criar um novo método
            //RoutedEventHandler dialogResultTrue = delegate (object o, RoutedEventArgs e)
            //{
            //    DialogResult = true;
            //};
            //RoutedEventHandler dialogResultFalse = delegate (object o, RoutedEventArgs e)
            //{
            //    DialogResult = false;
            //};
            //Elimina a necessidade de criar metodos privados

            var cancelarEventHandler = dialogResultFalse + Fechar;
            var okEventHandler = dialogResultTrue + Fechar;
            //var okEventHandler = (RoutedEventHandler)btnOk_Click + Fechar;
            //var cancelarEventHandler = (RoutedEventHandler)Delegate.Combine(
            //                                                                (RoutedEventHandler)btnCancelar_Click,
            //                                                                (RoutedEventHandler)Fechar);
            btnOK.Click += okEventHandler;
            btnCancelar.Click += cancelarEventHandler;
            ValidarNuloEDigito();
        }

        private void ValidarNuloEDigito()
        {
            txtNumero.TextChanged += ValidarCampoNulo;
            txtNumero.TextChanged += ValidarSeDigito;
            txtNome.TextChanged += ValidarCampoNulo;
            txtTelefone.TextChanged += ValidarCampoNulo;
            txtEndereco.TextChanged += ValidarCampoNulo;
            txtDescricao.TextChanged += ValidarCampoNulo;
        }

        private void ValidarSeDigito(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;

            //Func<char, bool> verificaSeDigito = caractere =>
            //{
            //    return Char.IsDigit(caractere);
            //};
            var tudoDigito = txt.Text.All(Char.IsDigit);

            if (!tudoDigito)
                txt.Background = new SolidColorBrush(Colors.IndianRed);
            //else
            //    txt.Background = new SolidColorBrush(Colors.White);
        }

        private TextChangedEventHandler ConstruirDelegateValidacaoCampoNulo(TextBox txt)
        {
            //Não é necessário construir um método construtor de delegates, basta criar um método que trata o object sender, dessa forma
            //1 método é capaz de tratar todas as mudanças
            return (o, e) =>
            {
                var textoEstaVazio = String.IsNullOrEmpty(txt.Text);

                if (textoEstaVazio)
                    txt.Background = new SolidColorBrush(Colors.OrangeRed);
                else
                    txt.Background = new SolidColorBrush(Colors.White);

            };
        }
        private void ValidarCampoNulo(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            txt.Text = txt.Text.Trim();
            var textoEstaVazio = string.IsNullOrEmpty(txt.Text);
            if (textoEstaVazio)
                txt.Background = new SolidColorBrush(Colors.OrangeRed);
            else
                txt.Background = new SolidColorBrush(Colors.White);
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = true;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = false;
        }
        private void Fechar(object sender, EventArgs e)
        {
            Close();
        }
    }
}
