using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Agenda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string connectionString = @"Data Source=DESKTOP-39L9QCH;Initial Catalog=Agenda;Integrated Security=True;Pooling=False";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM  AGENDA WHERE ID=" + IdTextBox.Text;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            try
            {

                con.Open();
                SqlDataReader leitor = cmd.ExecuteReader();

                if (leitor.Read())
                {
                    NomeTextBox.Text = leitor["Nome"].ToString();
                    EnderecoTextBox.Text = leitor["Endereco"].ToString();
                    TelefoneTextBox.Text = leitor["Telefone"].ToString();
                }
                else
                {
                    NomeTextBox.Clear();
                    EnderecoTextBox.Clear();
                    TelefoneTextBox.Clear();
                    throw new Exception("Código não localizado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }

        }
        private void InserirButton_Click(object sender, RoutedEventArgs e)
        {

            string sql = "INSERT INTO AGENDA(NOME , ENDERECO , TELEFONE)" + "VALUES ('" + NomeTextBox.Text + "','" + EnderecoTextBox.Text + "','" + TelefoneTextBox.Text + "')";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();


                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    NomeTextBox.Clear();
                    EnderecoTextBox.Clear();
                    TelefoneTextBox.Clear();
                    IdTextBox.Clear();
                    MessageBox.Show("Cadastrado com Sucesso");

                }


            }
            catch (Exception ex)
            {
                IdTextBox.Clear();
                MessageBox.Show(ex.Message);

            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }

        }

        private void AlterarButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "UPDATE AGENDA SET NOME='" + NomeTextBox.Text + "',ENDERECO='" + EnderecoTextBox.Text + "',TELEFONE='" + TelefoneTextBox.Text + "'WHERE ID='" + IdTextBox.Text + "'";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    NomeTextBox.Clear();
                    EnderecoTextBox.Clear();
                    TelefoneTextBox.Clear();
                    IdTextBox.Clear();
                    MessageBox.Show("Alterado com Sucesso!");
                }
                else
                {
                    throw new Exception("Código não localizado!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        private void ExcluirButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "DELETE FROM AGENDA WHERE ID=" + IdTextBox.Text;

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;

            try
            {
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    NomeTextBox.Clear();
                    EnderecoTextBox.Clear();
                    TelefoneTextBox.Clear();
                    IdTextBox.Clear();
                    MessageBox.Show("Excluido com Sucesso!");
                }
                else
                {
                    throw new Exception("Código não localizado!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            
        }

        private void NomeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}