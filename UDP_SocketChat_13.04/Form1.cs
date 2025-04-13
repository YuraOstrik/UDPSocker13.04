using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace UDP_SocketChat_13._04
{
    public partial class Form1 : Form
    {
        struct Message
        {
            public string mes { get; set; }
            public string user { get; set; }
            public byte[] image { get; set; }  
        }

        public SynchronizationContext uiContext;
        private Dictionary<string, List<DateTime>> clientRequests = new Dictionary<string, List<DateTime>>();
        private const int MaxRequestsPerHour = 10;

        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;
            WaitClient();
        }

        private async void WaitClient()
        {
            await Task.Run(() =>
            {
                try
                {
                    IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 49152);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    socket.Bind(ipEnd);

                    while (true)
                    {
                        EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
                        byte[] buffer = new byte[1024];
                        int bytesReceived = socket.ReceiveFrom(buffer, ref remote);
                        string json = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                        Message m = JsonSerializer.Deserialize<Message>(json);
                        string clientIp = ((IPEndPoint)remote).Address.ToString();

                        lock (clientRequests)
                        {
                            if (!clientRequests.ContainsKey(clientIp))
                                clientRequests[clientIp] = new List<DateTime>();

                            clientRequests[clientIp].RemoveAll(t => t < DateTime.Now.AddHours(-1));

                            if (clientRequests[clientIp].Count >= MaxRequestsPerHour)
                            {
                                uiContext.Send(d =>
                                {
                                    listBox2.Items.Clear();
                                    if (!listBox2.Items.Contains("Blocked: too many requests from " + clientIp))
                                        listBox2.Items.Add("Blocked: too many requests from " + clientIp);
                                }, null);
                                continue;
                            }

                            clientRequests[clientIp].Add(DateTime.Now);
                        }

                        if (m.mes.ToLower().Contains("ціна"))
                        {

                            string[] products = {
                                "Процесор - 3000 грн",
                                "Відеокарта - 12000 грн",
                                "Оперативна пам’ять - 2000 грн",
                                "Жорсткий диск - 1500 грн"
                            };

                            uiContext.Send(d =>
                            {
                                listBox2.Items.Clear();
                                foreach (var p in products)
                                    listBox2.Items.Add(p);
                            }, null);


                            string productImagePath = @""; 
                            byte[] imageBytes = File.ReadAllBytes(productImagePath);


                            Message responseMessage = new Message
                            {
                                mes = "Процесор - 3000 грн",  
                                user = "Server",
                                image = imageBytes
                            };

                            string responseJson = JsonSerializer.Serialize(responseMessage);
                            byte[] responseData = Encoding.UTF8.GetBytes(responseJson);

                            socket.SendTo(responseData, remote);
                        }

                        uiContext.Send(d => listBox1.Items.Add(m.user), null);
                        uiContext.Send(d => listBox1.Items.Add(m.mes), null);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Receiver error: " + ex.Message);
                }
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ipAd.Text), 49152);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    Message m = new Message
                    {
                        mes = InfoT.Text,
                        user = Environment.UserDomainName
                    };

                    string json = JsonSerializer.Serialize(m);
                    byte[] data = Encoding.UTF8.GetBytes(json);

                    socket.SendTo(data, ipEnd);
                    socket.Shutdown(SocketShutdown.Send);
                    socket.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sender error: " + ex.Message);
                }
            });
        }
    }
}
