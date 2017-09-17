using NitroxModel.Logger;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NitroxModel.MonoBehaviours
{
    public class GuidComponent : MonoBehaviour
    {
        private static readonly Dictionary<Guid, GuidComponent> identifiers = new Dictionary<Guid, GuidComponent>();

        private Guid id;

        public Guid Id
        {
            get
            {
                if (IsEmpty())
                {
                    // Call the set function to ensure a valid Guid.
                    //Id = Guid.Empty;
                    EnsureGuid();
                }
                return id;
            }
            set
            {
                Unregister();
                id = EnsureGuid(value);
                var uid = gameObject.GetComponent<UniqueIdentifier>();
                if (uid)
                    uid.Id = id.ToString();
                Register();
            }
        }

        private void Awake()
        {
            EnsureGuid();
        }

        private void OnDestroy()
        {
            Unregister();
        }

        private void Register()
        {
            if (!IsEmpty())
            {
                identifiers[id] = this;
            }
        }

        private void Unregister()
        {
            if (!IsEmpty())
            {
                identifiers.Remove(id);
            }
        }

        private bool IsEmpty()
        {
            return id == Guid.Empty;
        }

        private void EnsureGuid()
        {
            var uid = GetComponent<UniqueIdentifier>();
            if (uid)
            {
                var guid = new Guid(uid.Id);
                if (!IsEmpty() && guid != Id)
                {
                    Log.Warn($"UniqueIdentifier had a different GUID than GuidComponent! {Id} != {guid}");
                    // Also, EnsureGuid shouldn't be called anyway when !IsEmpty().
                }
                id = guid;
                Register();
            }
            else if (IsEmpty())
            {
                id = Guid.NewGuid();
                Register();
            }
        }

        private static Guid EnsureGuid(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                guid = Guid.NewGuid();
            }
            return guid;
        }

        public static bool TryGetIdentifier(Guid guid, out GuidComponent component)
        {
            if (guid == Guid.Empty)
            {
                component = null;
                return false;
            }

            if (identifiers.TryGetValue(guid, out component))
            {
                return true;
            }

            UniqueIdentifier uid;
            if (UniqueIdentifier.TryGetIdentifier(guid.ToString(), out uid))
            {
                // When adding the GuidComponent, it'll automatically be instantiated to match the UniqueIdentifier Id.
                component = uid.gameObject.AddComponent<GuidComponent>();
                return true;
            }
            return false;
        }
    }
}
