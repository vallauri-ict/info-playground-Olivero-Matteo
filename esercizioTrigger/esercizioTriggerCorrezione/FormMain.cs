using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace esercizioTriggerCorrezione
{
    public partial class FormMain : Form
    {
        private static string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFileName=" + Directory.GetCurrentDirectory() + @"\DBTrigger.mdf;Integrated Security=True";

        private BindingSource bsDriver = new BindingSource();
        private BindingSource bsStoricoCancellazioni = new BindingSource();
        private BindingSource bsStoricoAggiornamenti = new BindingSource();
        private DataTable dtCliente, dtStoricoCancellazioni, dtStoricoAggiornamenti;
        private SqlDataAdapter daCliente, daStoricoCancellazioni, daStoricoAggiornamenti;

        public FormMain()
        {
            InitializeComponent();
        }

        private void btnAggDB_Click(object sender, EventArgs e)
        {
            daCliente.Update(dtCliente);
            PopulateDgv(0);
        }

        private void OnRowsDeletedDriver(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            daCliente.Update(dtCliente);
            PopulateDgv(2);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            PopulateDgv(0);
        }

        private void Query(string selectCommand, out SqlDataAdapter da)
        {
            da = null;
            try
            {
                //Creo un nuovo data adapter basato su selectCommand
                da = new SqlDataAdapter(selectCommand, CONNECTION_STRING);

                //Creo un command builder per generare un comando SQL update, insert, delete
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateDgv(int tables)
        {
            switch (tables)
            {
                case 0:
                    {
                        //Driver
                        dgvDriver.DataSource = bsDriver;
                        Query("SELECT * FROM Cliente", out daCliente);
                        if (daCliente != null)
                        {
                            dtCliente = new DataTable();
                            daCliente.Fill(dtCliente);
                            bsDriver.DataSource = dtCliente;
                        }
                        dgvDriver.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                        //Cancellazioni
                        dgvStoricoCancellazioni.DataSource = bsStoricoCancellazioni;
                        Query("SELECT * FROM StoricoCancellazioni", out daStoricoCancellazioni);
                        if (daStoricoCancellazioni != null)
                        {
                            dtStoricoCancellazioni = new DataTable();
                            daStoricoCancellazioni.Fill(dtStoricoCancellazioni);
                            bsStoricoCancellazioni.DataSource = dtStoricoCancellazioni;
                        }
                        dgvStoricoCancellazioni.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                        //Aggiornamenti
                        dgvStoricoAggiornamenti.DataSource = bsStoricoAggiornamenti;
                        Query("SELECT * FROM StoricoAggiornamenti", out daStoricoAggiornamenti);
                        if (daStoricoAggiornamenti != null)
                        {
                            dtStoricoAggiornamenti = new DataTable();
                            daStoricoAggiornamenti.Fill(dtStoricoAggiornamenti);
                            bsStoricoAggiornamenti.DataSource = dtStoricoAggiornamenti;
                        }
                        dgvStoricoAggiornamenti.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                        break;
                    }
                case 1:
                    {
                        //Driver
                        dgvDriver.DataSource = bsDriver;
                        Query("SELECT * FROM Cliente", out daCliente);
                        if (daCliente != null)
                        {
                            dtCliente = new DataTable();
                            daCliente.Fill(dtCliente);
                            bsDriver.DataSource = dtCliente;
                        }
                        dgvDriver.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                        break;
                    }
                case 2:
                    {
                        //Cancellazioni
                        dgvStoricoCancellazioni.DataSource = bsStoricoCancellazioni;
                        Query("SELECT * FROM StoricoCancellazioni", out daStoricoCancellazioni);
                        if (daStoricoCancellazioni != null)
                        {
                            dtStoricoCancellazioni = new DataTable();
                            daStoricoCancellazioni.Fill(dtStoricoCancellazioni);
                            bsStoricoCancellazioni.DataSource = dtStoricoCancellazioni;
                        }
                        dgvStoricoCancellazioni.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                        break;
                    }
                case 3:
                    {
                        //Aggiornamenti
                        dgvStoricoAggiornamenti.DataSource = bsStoricoAggiornamenti;
                        Query("SELECT * FROM StoricoAggiornamenti", out daStoricoAggiornamenti);
                        if (daStoricoAggiornamenti != null)
                        {
                            dtStoricoAggiornamenti = new DataTable();
                            daStoricoAggiornamenti.Fill(dtStoricoAggiornamenti);
                            bsStoricoAggiornamenti.DataSource = dtStoricoAggiornamenti;
                        }
                        dgvStoricoAggiornamenti.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                        break;
                    }
            }
        }
    }
}
