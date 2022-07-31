using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net;
using System.Net.Mail;

namespace HareketSensorApp
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();     
        }
        private string data;
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = 9600;
                    serialPort1.Parity = Parity.None;
                    serialPort1.DataBits = 8;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Open(); 

                    label2.Text = "Bağlantı Sağlandı.";
                    label2.ForeColor = Color.Green;
                    button1.Text = "KES";                 
                }
                else
                {
                    label2.Text = "Bağlantı Kesildi.";
                    label2.ForeColor = Color.Red;
                    button1.Text = "BAĞLAN";              
                    serialPort1.Close();                

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Port Hatası");     
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();  
            foreach (string port in ports)
                comboBox1.Items.Add(port);               

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived); 
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data = serialPort1.ReadLine();
            this.Invoke(new EventHandler(displaydata));
            panel1.BackColor = Color.LightGreen;      
            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Credentials = new NetworkCredential("***Gönderici Mail Adresi***", "***Gönderici Mail Adresi Şifresi***");  
            SmtpServer.Port = 587;                              
            SmtpServer.Host = "smtp.gmail.com";                 
            SmtpServer.EnableSsl = true;                       
            MailMessage mail = new MailMessage();
            mail.To.Add("***Alıcı Mail Adresi***");            
            mail.From = new MailAddress("***Gönderici Mail Adresi***", "***Gönderen İsim***");  
            mail.Subject = "***Mail Konusu***";     
            mail.Body = "***Mail İçeriği***";
            SmtpServer.Send(mail);  
            System.Threading.Thread.Sleep(2000);  
            panel1.BackColor = Color.Silver;
            this.Invoke(new EventHandler(displaydata2));
        }

        private void displaydata2(object sender, EventArgs e)
        {
            label3.Text = "HAREKET YOK";
            label3.ForeColor = Color.Green;
            
        }

        private void displaydata(object sender, EventArgs e)
        {
            label3.Text = "HAREKET ALGILANDI";
            label3.ForeColor = Color.Red;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();  
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
