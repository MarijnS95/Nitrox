using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using NitroxModel.MonoBehaviours;
using System;
using UnityEngine;

namespace NitroxClient.GameLogic.Helper
{
    public static class GuidHelper
    {
        public static GameObject RequireObjectFrom(Guid guid)
        {
            Optional<GameObject> gameObject = GetObjectFrom(guid);
            Validate.IsPresent(gameObject, "Game object required from guid: " + guid);
            return gameObject.Get();
        }

        // Feature parity of UniqueIdentifierHelper.GetByName() except does not do the verbose logging
        public static Optional<GameObject> GetObjectFrom(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                return Optional<GameObject>.Empty();
            }

            GuidComponent uid;

            if (!GuidComponent.TryGetIdentifier(guid, out uid))
            {
                return Optional<GameObject>.Empty();
            }

            if (uid == null)
            {
                return Optional<GameObject>.Empty();
            }

            return Optional<GameObject>.Of(uid.gameObject);
        }

        public static Guid GetGuid(GameObject gameObject)
        {
            return GetUniqueIdentifier(gameObject).Id;
        }

        public static void SetNewGuid(GameObject gameObject, Guid guid)
        {
            GetUniqueIdentifier(gameObject).Id = guid;
        }

        private static GuidComponent GetUniqueIdentifier(GameObject gameObject)
        {
            return gameObject.EnsureComponent<GuidComponent>();
        }
    }
}
