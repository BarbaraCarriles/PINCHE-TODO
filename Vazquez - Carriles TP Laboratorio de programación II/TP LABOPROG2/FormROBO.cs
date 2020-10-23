using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_LABOPROG2
{ 
    public partial class BBENEFICIODINAMICO : Form
    {
       
        List<cQueso> ListaQuesos = new List<cQueso>();
        List<cQueso> ListaMochila = new List<cQueso>();

        public BBENEFICIODINAMICO()
        {
            InitializeComponent();
        }

        private void cQuesoDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormROBO_Load(object sender, EventArgs e)
        {
            cQuesoBindingSource.DataSource = ListaQuesos;
            //for(int i=0;i<ListaMochila.Count;i++)
            //{
            //    listBox1.Items.Add("Queso: "+ListaMochila[i].Queso +ListaMochila[i].fraccion);
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ALGORITMO 
            textBoxCapacidad.Enabled = false;
            labelEligiendo.Visible = true;
            buttonelegirrobo.Enabled = false;
            ROBARVORAZBUTTON.Enabled = false;
            int cont = 0;
            
            progressBarvoraz.Visible = true;
           
            while(cont<= 1000000)
            {
                progressBarvoraz.Value = cont;
                cont++; 
               
            }

   
            MOSTRARMOCHILAVORAZ.Visible = true;
            
            float Capacidad = Convert.ToInt32(textBoxCapacidad.Text);//lee la capacidad que ingresa el usuario en eldel textbox
            int contcambios;
            float acum = 0;
            cQueso aux;
            //recorremos la lista de quesos para generar una lista de beneficios
            for (int i = 0; i < ListaQuesos.Count; i++)
            {
                ListaQuesos[i].beneficio = ((float)ListaQuesos[i].precio / ListaQuesos[i].peso);
            }

            for (int i = 0; i < ListaQuesos.Count - 1; i++)
            {
                contcambios = 0;
                for (int j = 0; j < ListaQuesos.Count - 1; j++)
                {
                    if (ListaQuesos[i].beneficio < ListaQuesos[i + 1].beneficio)
                    {
                        aux = ListaQuesos[i];
                        ListaQuesos[i] = ListaQuesos[i + 1];
                        ListaQuesos[i + 1] = aux;
                        contcambios++;
                    }
                    if (contcambios == 0)
                        break;
                }
            }
            for (int i = 0; i < ListaQuesos.Count; i++)
            {
                if (acum+ListaQuesos[i].peso <= Capacidad)
                {
                    ListaQuesos[i].fraccion = 1;//se lleva el queso entero
                    ListaMochila.Add(ListaQuesos[i]);
                    acum = acum + ListaQuesos[i].peso; //peso acumulado  
                }
                else
                {
                    //fracciona el queso y lo mete en la mochila 
                    ListaQuesos[i].fraccion = (Capacidad - acum) / ListaQuesos[i].peso;
                    ListaMochila.Add(ListaQuesos[i]);
                    acum = Capacidad;
                }
                if (acum == Capacidad)
                    break;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            iniciorobodinamico.Visible = false;
            iniciorobovoraz.Visible = false;
            textladron.Visible = false;
            robardinamicamente.Visible = false;
            textboxcapacidad2.Visible = false;
            textBoxCapacidad.Visible = false;
            labelvoraz.Visible = false;
            ROBARVORAZBUTTON.Enabled = true;
            ROBARDINAMICOBUTTON.Enabled = true;
            pictureBox3.Visible = true;
        }

        private void ROBARDINAMICOBUTTON_Click(object sender, EventArgs e)
        {
            
            ROBARVORAZBUTTON.Enabled = false;
            robardinamicamente.Visible = true;
            textladron.Visible = true;
            textboxcapacidad2.Visible = true;
            iniciorobodinamico.Visible = true;
        }

        private void iniciorobodinamico_Click(object sender, EventArgs e)
        {
            
           labelEligiendo.Visible = true;
            textboxcapacidad2.Enabled = false;
            int cont = 0;

            progressBarvoraz.Visible = true;

            while (cont <= 1000000)
            {
                progressBarvoraz.Value = cont;
                cont++;          
            }



            buttonelegirrobo.Enabled = false;
            ROBARDINAMICOBUTTON.Enabled = false;
            //pictureBox5.Visible = true;
            MOSTRARMOCHILAVORAZ.Visible = true;
            //ALGORITMO DINAMICO
            int n = ListaQuesos.Count();//filas        
            int c = Convert.ToInt32(textboxcapacidad2.Text);//columnas
            int[,] Beneficio = new int[(n + 1), (c + 1)];//nos creamos matriz 
            for (int i = 0; i <= n; i++)
            {
                Beneficio[i, 0] = 0; //ponemos nuestra primera fila y columna en 0
            }
            for (int j = 0; j <= c; j++)
            {
                Beneficio[0, j] = 0;//representa el estado incial de la mochila
            }
            //llenamos la matriz
            for (int i = 1; i <= n; i++) //me paro en la primera fila y me voy moviendo columna x columna
            {
                for (int j = 1; j <= c; j++)
                {
                    if (i == 1)
                    {
                        if (j >= ListaQuesos[i - 1].peso)
                            Beneficio[i, j] = ListaQuesos[i - 1].precio;//entra el queso en la mochila y se guarda el valor de este
                    }
                    else //si no es 1 va a ser mayor
                    {
                        if (j < ListaQuesos[i - 1].peso)
                        {
                            Beneficio[i, j] = Beneficio[i - 1, j];//si no entra el queso, me guardo el beneficio del anterior

                        }
                        else if (j >= ListaQuesos[i - 1].peso)//entra el quesito
                        {
                            Beneficio[i, j] = Math.Max(Beneficio[i - 1, j], ListaQuesos[i - 1].precio + Beneficio[i - 1, j - ListaQuesos[i - 1].peso]);
                        }
                    }
                }
            }
            funcionrecursiva(n, c);
            void funcionrecursiva(int i, int j)
            {
                int beneficiototal = Beneficio[n, c];

                for (int k = n; k > 0; k--)
                {

                    if (Beneficio[k, j] != Beneficio[k - 1, j])
                    {
                        ListaQuesos[k - 1].fraccion = 1;
                        ListaMochila.Add(ListaQuesos[k - 1]);//esta el ultimo queso dentro de la mochila
          
                        j = j - ListaQuesos[k - 1].peso;
                    }
                }
            }   



            //Intentamos hacer la función de forma recursiva, pero una vez que se completaba el ciclo while, 
            //los parametros volvian a incrementar sus valores, de tal forma que volvían a entrar al ciclo, y nunca salía de la función. Lo resolvimos con un ciclo for (como muestra el código de arriba) 

                //if (i == 0)
                //{
                //    return;
                //}
                //else
                //{
                //    while (i > 0 && j > 0)
                //    {
                //        if (Beneficio[i, j] != Beneficio[i - 1, j])
                //        {
                //            ListaMochila.Add(ListaQuesos[i - 1]);//esta el ultimo queso dentro de la mochila
                //            funcionrecursiva(i - 1, j - ListaQuesos[i - 1].peso);
                //        }
                //        else
                //        {
                //            funcionrecursiva((i - 1), j);
                //        }

                //    }

                //imprimir los datos en la listbox2 de beneficio
            //}
            //}
        }
        private void ROBARVORAZBUTTON_Click(object sender, EventArgs e)
        {
           
            labelvoraz.Visible = true;
            textladron.Visible = true;
            textBoxCapacidad.Visible = true;
            iniciorobovoraz.Visible = true;
            ROBARDINAMICOBUTTON.Enabled = false;
        }

        private void ESCAPE_Click(object sender, EventArgs e)
        {

        }

        private void textboxcapacidad2_TextChanged(object sender, EventArgs e)
        {
            iniciorobodinamico.Enabled = true;
        }

        private void textBoxCapacidad_TextChanged(object sender, EventArgs e)
        {
            iniciorobovoraz.Enabled = true;
        }

        private void labelmuestra_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelEligiendo.Visible = false;
            iniciorobodinamico.Enabled = false;
            iniciorobovoraz.Enabled = false;
            progressBarvoraz.Visible = false;
            ESCAPE.Visible = true;
            labelescape.Visible = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox4.Visible = true;

            listBox1.Items.Add("USTED HA LOGRADO ESCAPAR CON:");
            for (int i = 0; i < ListaMochila.Count; i++)
            {
                listBox1.Items.Add("\n "+Math.Round(ListaMochila[i].fraccion,2)+ " " +ListaMochila[i].Queso);
                listBox1.Visible = true;
            }
        }

        private void MOSTRARMOCHILADINAMICA_Click(object sender, EventArgs e)
        {
            labelEligiendo.Visible = false;
            ESCAPE.Visible = true;
            labelescape.Visible = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox4.Visible = true;

            listbox2.Items.Add("USTED HA LOGRADO ESCAPAR CON:");
            for (int i = 0; i < ListaMochila.Count; i++)
            {
                listbox2.Items.Add("\n" + ListaMochila[i].fraccion + " " + ListaMochila[i].Queso);
                listbox2.Visible = true;
            }
        }

        private void labelbeneficiodinamico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ESCAPE_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void progressBarvoraz_Click(object sender, EventArgs e)
        {

        }
    }
}

