﻿using System.Collections;
using System.Collections.Generic;
using NitroxClient.Communication;
using NitroxClient.Communication.Abstract;
using NitroxClient.Map;
using NitroxModel.DataStructures;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.GameLogic
{
    public class Terrain
    {
        private readonly IMultiplayerSession multiplayerSession;
        private readonly IPacketSender packetSender;
        private readonly VisibleCells visibleCells;
        private readonly DeferringPacketReceiver packetReceiver;

        private bool cellsPendingSync;
        private float timeWhenCellsBecameOutOfSync;

        private List<VisibleCell> added = new List<VisibleCell>();
        private List<VisibleCell> removed = new List<VisibleCell>();

        public Terrain(IMultiplayerSession multiplayerSession, IPacketSender packetSender, VisibleCells visibleCells, DeferringPacketReceiver packetReceiver)
        {
            this.multiplayerSession = multiplayerSession;
            this.packetSender = packetSender;
            this.visibleCells = visibleCells;
            this.packetReceiver = packetReceiver;
        }

        public void CellLoaded(Int3 batchId, Int3 cellId, int level)
        {
            LargeWorldStreamer.main.StartCoroutine(WaitAndAddCell(batchId, cellId, level));
            markCellsReadyForSync(0.5f);
        }

        private IEnumerator WaitAndAddCell(Int3 batchId, Int3 cellId, int level)
        {
            yield return new WaitForSeconds(0.5f);

            VisibleCell cell = new VisibleCell(batchId, cellId, level);

            if (!visibleCells.Contains(cell))
            {
                visibleCells.Add(cell);
                added.Add(cell);
                packetReceiver.CellLoaded(cell);
            }
        }

        public void CellUnloaded(Int3 batchId, Int3 cellId, int level)
        {
            VisibleCell cell = new VisibleCell(batchId, cellId, level);

            if (visibleCells.Contains(cell))
            {
                visibleCells.Remove(cell);
                removed.Add(cell);
                markCellsReadyForSync(0);
            }
        }

        private void markCellsReadyForSync(float delay)
        {
            if (cellsPendingSync == false)
            {
                timeWhenCellsBecameOutOfSync = Time.time;
                LargeWorldStreamer.main.StartCoroutine(WaitAndSyncCells(delay));
                cellsPendingSync = true;
            }
        }

        private IEnumerator WaitAndSyncCells(float delay)
        {
            yield return new WaitForSeconds(delay);

            while (cellsPendingSync)
            {
                float currentTime = Time.time;
                float elapsed = currentTime - timeWhenCellsBecameOutOfSync;

                if (elapsed >= 0.1)
                {
                    CellVisibilityChanged cellsChanged = new CellVisibilityChanged(multiplayerSession.Reservation.PlayerId, added.ToArray(), removed.ToArray());
                    packetSender.Send(cellsChanged);

                    added.Clear();
                    removed.Clear();

                    cellsPendingSync = false;
                    yield break;
                }

                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
