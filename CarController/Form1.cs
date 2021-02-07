using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.Threading;

namespace CarController
{
    public partial class Form1 : Form
    {
        string msg;
        int command = 0;
        

        ClientWebSocket ws = new ClientWebSocket();

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.KeyPreview = true;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            msg = "{ \"message\":\"sendmessage\", \"data\":\"" + command.ToString() + "\"}";
            

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            command = 0;
        }

        public async void Form1_Load(object sender, EventArgs e)
        {
            await ws.ConnectAsync(new Uri("wss://twclevz8t5.execute-api.us-east-1.amazonaws.com/prod"), CancellationToken.None);
            timer1.Start();
        }

        //On KeyDown, determine which Keys it is and perform a movement on the car
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Q)
            {
                command = 0;
            }
            if(e.KeyCode == Keys.W)
            {
                command = 1;
            }
            if (e.KeyCode == Keys.S)
            {
                command = 2;
            }
            if (e.KeyCode == Keys.A)
            {
                command = 3;
            }
            if (e.KeyCode == Keys.D)
            {
                command = 4;
            }
        }

        public async Task<string> Connection(string input)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(input);

            await ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            //await Task.Delay(500);
            //await ws.ReceiveAsync(buffer, CancellationToken.None);

            //var response = System.Text.Encoding.UTF8.GetString(buffer);
            //var splitResponse = response.Split(":");
            //response = splitResponse[splitResponse.Length - 1];
            //response = response.Substring(1, response.Length - 3);

            var response = "1";

            return response;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            msg = "{ \"message\":\"sendmessage\", \"data\":\"" + command.ToString() + "\"}";
            Task<string> task = Connection(msg);
        }
    }
}
