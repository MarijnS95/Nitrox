using NitroxClient.MonoBehaviours;
using NitroxModel.Logger;
using NitroxModel.Packets;
using NitroxModel.Tcp;
using System;
using System.Net;
using System.Net.Sockets;

namespace NitroxClient.Communication
{
    public class TcpClient
    {
        private const int port = 11000;

        private ChunkAwarePacketReceiver packetReceiver;
        private Connection connection;

        public TcpClient(ChunkAwarePacketReceiver packetManager)
        {
            this.packetReceiver = packetManager;
        }

        public void Start(String ip)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                connection = new Connection(socket);
                connection.Connect(remoteEP);

                if (connection.Open)
                {
                    connection.BeginReceive(DataReceived);
                }
            }
            catch (Exception e)
            {
                Log.Debug("Unforeseen error when connecting: " + e.GetBaseException());
            }
        }

        public void Stop()
        {
            connection.Close(); // Server will clean up pretty quickly
            Multiplayer.PacketSender.Active = false;
            Multiplayer.RemoveAllOtherPlayers();
            Log.InGame("Disconnected from server.");
        }

        private void DataReceived(IAsyncResult ar)
        {
            Connection connection = (Connection)ar.AsyncState;

            foreach (Packet packet in connection.GetPacketsFromRecievedData(ar))
            {
                packetReceiver.PacketReceived(packet);
            }

            if (connection.Open)
            {
                connection.BeginReceive(DataReceived);
            }
            else
            {
                Log.Debug("Error reading data from server");
                Stop();
            }
        }

        public void Send(Packet packet)
        {
            connection.SendPacket(packet, PacketSentSuccessful);
        }

        public void PacketSentSuccessful(IAsyncResult ar)
        {
        }

        public bool IsConnected()
        {
            return connection != null && connection.Open;
        }
    }
}
