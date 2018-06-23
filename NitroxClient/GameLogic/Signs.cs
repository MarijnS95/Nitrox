using NitroxClient.Communication.Abstract;
using NitroxModel.Packets;

namespace NitroxClient.GameLogic
{
    public class Signs
    {
        private readonly IPacketSender packetSender;

        public Signs(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public void Changed(uGUI_SignInput sign)
        {
            string guid = sign.gameObject.FindAncestor<PrefabIdentifier>().Id;

            SignChanged signChanged = new SignChanged(guid, sign.text, sign.colorIndex, sign.scaleIndex, sign.elementsState, sign.IsBackground());
            packetSender.Send(signChanged);
        }
    }
}
