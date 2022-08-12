using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ConsultaCuentasUNIENS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Ingresa los apellidos del estudiante");
            tbLastName.Select();
            tbLastName.Focus();
        }

        private void tbLastName_KeyDown(object sender, KeyEventArgs e) 
        { 
            if (e.KeyCode == Keys.Enter)
            {
                tbLastName.Text= tbLastName.Text.Trim().ToUpper();
                MessageBox.Show("Ingresa los nombres del estudiante");
                tbName.Select();
                tbName.Focus();
            } 
        }
        private string ClaveCarrera(string clave) 
        {
            string Carrera = "";
            if(clave == "LAITE") 
            {
                Carrera = "LICENCIADO EN ADMINISTRACIÓN DE EMPRESAS Y LOGÍSTICA";
            }
            else if (clave == "LCEITE") 
            {
                Carrera = "LICENCIADO EN CIENCIAS DE LA EDUCACION";
            }
            else if (clave == "IDITE") 
            {
                Carrera = "INGENIERIA INDUSTRIAL";
            }
            else if (clave == "LDITE") 
            {
                Carrera = "LICENCIADO EN DERECHO";
            }
            else if (clave == "LFITE") 
            {
                Carrera = "LICENCIADO EN FILOSOFÍA";
            }
            else if (clave == "LMAITE") 
            {
                Carrera = "LICENCIADO EN MEDIOS AUDIOVISUALES";
            }
            else if (clave == "LNITE") 
            {
                Carrera = "LICENCIADO EN NEGOCIOS INTERNACIONALES";
            }
            else if (clave == "LCRITE") 
            {
                Carrera = "LICENCIADO EN CRIMINOLOGÍA";
            }
            return Carrera;
        }
        private void tbName_KeyDown(object sender, KeyEventArgs e) 
        { 
            if (e.KeyCode == Keys.Enter) 
            {
                tbName.Text = tbName.Text.Trim().ToUpper();
                string wName = tbLastName.Text + " " + tbName.Text;
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://uniens:uniens22@cluster0.ex5nn.mongodb.net/?retryWrites=true&w=majority?connect=replicaSet");
                var client = new MongoClient(settings);
                var database = client.GetDatabase("studENS");
                var collection = database.GetCollection<BsonDocument>("infoMails");
                var filter = Builders<BsonDocument>.Filter.Eq("wholeName", wName);
                var BsonDoc = collection.Find(filter).FirstOrDefault();
                try
                {
                    if (BsonDoc != null) 
                    {
                        string email = BsonDoc["mail"].ToString();
                        string pass = BsonDoc["password"].ToString();
                        string matr = BsonDoc["matInt"].ToString();
                        string turno = BsonDoc["turno"].ToString();
                        string cuatr = BsonDoc["cuatri"].ToString();
                        string clave = BsonDoc["carrera"].ToString();
                        tbEmail.Text = email;
                        tbPassword.Text = pass;
                        tbMatr.Text = matr;
                        tbTurno.Text = turno;
                        tbCuatri.Text = cuatr;
                        tbClave.Text = clave;
                        tbCarrera.Text = ClaveCarrera(clave);
                    }
                    else 
                    {
                        MessageBox.Show("El usuario no existe, favor de verificar");
                    }
                }
                catch 
                {
                    MessageBox.Show("Error con la conexión a la base de datos");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbName.Text = "";
            tbLastName.Text = "";
            tbEmail.Text = "";
            tbPassword.Text = "";
            tbMatr.Text = "";
            tbTurno.Text = "";
            tbCuatri.Text = "";
            tbClave.Text = "";
            tbCarrera.Text = "";
        }
    }
}
