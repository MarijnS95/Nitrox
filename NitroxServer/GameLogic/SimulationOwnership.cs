using System;
using System.Collections.Generic;

namespace NitroxServer.GameLogic
{
    public class SimulationOwnership
    {
        Dictionary<Guid, Player> guidsByPlayer = new Dictionary<Guid, Player>();

        //TODO: redistribute upon disconnect

        public bool TryToAquireOwnership(Guid guid, Player player)
        {
            lock (guidsByPlayer)
            {
                Player owningPlayer;

                if (guidsByPlayer.TryGetValue(guid, out owningPlayer))
                {
                    if (owningPlayer != player)
                    {
                        return false;
                    }

                    return true;
                }

                guidsByPlayer[guid] = player;
                return true;
            }
        }

    }
}
