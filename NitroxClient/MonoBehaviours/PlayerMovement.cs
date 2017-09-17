using NitroxClient.GameLogic.Helper;
using NitroxModel.DataStructures.ServerModel;
using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using UnityEngine;

namespace NitroxClient.MonoBehaviours
{
    public class PlayerMovement : MonoBehaviour
    {
        public const float BROADCAST_INTERVAL = 0.05f;

        private float time = 0.0f;

        public void Update()
        {
            time += Time.deltaTime;

            // Only do on a specific cadence to avoid hammering server
            if (time >= BROADCAST_INTERVAL)
            {
                time = 0;

                Optional<VehicleModel> vehicle = GetVehicleModel();

                Optional<string> opSubGuid = Optional<string>.Empty();

                SubRoot currentSub = Player.main.GetCurrentSub();

                if (currentSub != null)
                {
                    opSubGuid = Optional<string>.OfNullable(GuidHelper.GetGuid(currentSub.gameObject));
                }

                Vector3 playerVelocity = Player.main.playerController.velocity;
                Vector3 currentPosition;
                Quaternion bodyRotation;
                Quaternion aimingRotation;

                if (vehicle.IsEmpty() && currentSub)
                {
                    // We're sending the local position relative to the subroot, because the player is attached to the SubRoot on remotes.
                    // This solves all kinds of stuttering, position and position-correction issues (because the player isn't necessarlily
                    //  walking /moving when the cyclops is moving).

                    //currentPosition = currentSub.transform.InverseTransformPoint(Player.main.transform.position);
                    //var inverseRotation = currentSub.transform.rotation.GetInverse();
                    //bodyRotation = inverseRotation * MainCameraControl.main.viewModel.transform.rotation;
                    //aimingRotation = inverseRotation * Player.main.camRoot.GetAimingTransform().rotation;

                    //currentPosition = Player.main.transform.position - currentSub.transform.position;
                    //var inverseRotation = currentSub.transform.rotation.GetInverse();
                    //bodyRotation = MainCameraControl.main.viewModel.transform.rotation;
                    //aimingRotation = Player.main.camRoot.GetAimingTransform().rotation;

                    // Subtract vehicle velocity to get the pure local velocity.
                    //playerVelocity -= currentSub.GetComponent<Rigidbody>().velocity;

                    Player.main.transform.parent = currentSub.transform;
                    currentPosition = Player.main.transform.position;
                    bodyRotation = MainCameraControl.main.viewModel.transform.rotation;
                    aimingRotation = Player.main.camRoot.GetAimingTransform().rotation;
                    Player.main.transform.parent = null;
                }
                else
                {
                    currentPosition = Player.main.transform.position;
                    bodyRotation = MainCameraControl.main.viewModel.transform.rotation;
                    aimingRotation = Player.main.camRoot.GetAimingTransform().rotation;
                }
                Multiplayer.Main.GetComponent<DebugGui>()["player"] = () =>
                {
                    GUILayout.BeginVertical("box");
                    DebugGui.FormatLabel("Local player");
                    DebugGui.FormatLabel("body: Position: {0}, Rotation: {1}", Player.main.transform.position, Player.main.transform.rotation);
                    DebugGui.FormatLabel("body_local: Position: {0}, Rotation: {1}", Player.main.transform.localPosition, Player.main.transform.localRotation);
                    DebugGui.FormatLabel("playerVelocity: {0}", playerVelocity);
                    if (Player.main.transform.parent)
                    {
                        DebugGui.FormatLabel("parent: Position: {0}, Rotation: {1}", Player.main.transform.parent.localPosition, Player.main.transform.parent.localRotation);
                        DebugGui.FormatLabel("parent_diff: Position: {0}, Rotation: {1}", Player.main.transform.position - Player.main.transform.parent.position,
                            Player.main.transform.rotation * Player.main.transform.parent.rotation.GetInverse());
                    }
                    GUILayout.EndVertical();
                };

                Multiplayer.Logic.Player.UpdateLocation(currentPosition, playerVelocity, bodyRotation, aimingRotation, vehicle, opSubGuid);
            }
        }

        private Optional<VehicleModel> GetVehicleModel()
        {
            Vehicle vehicle = Player.main.GetVehicle();
            SubRoot sub = Player.main.GetCurrentSub();

            string guid;
            Vector3 position;
            Quaternion rotation;
            Vector3 velocity;
            Vector3 angularVelocity;
            TechType techType;
            float steeringWheelYaw = 0f, steeringWheelPitch = 0f;
            bool appliedThrottle = false;

            if (vehicle != null)
            {
                guid = GuidHelper.GetGuid(vehicle.gameObject);
                position = vehicle.gameObject.transform.position;
                rotation = vehicle.gameObject.transform.rotation;
                techType = CraftData.GetTechType(vehicle.gameObject);

                Rigidbody rigidbody = vehicle.gameObject.GetComponent<Rigidbody>();

                velocity = rigidbody.velocity;
                angularVelocity = rigidbody.angularVelocity;

                // Required because vehicle is either a SeaMoth or an Exosuit, both types which can't see the fields either.
                steeringWheelYaw = (float)vehicle.ReflectionGet<Vehicle, Vehicle>("steeringWheelYaw");
                steeringWheelPitch = (float)vehicle.ReflectionGet<Vehicle, Vehicle>("steeringWheelPitch");

                // Vehicles (or the SeaMoth at least) do not have special throttle animations. Instead, these animations are always playing because the player can't even see them (unlike the cyclops which has cameras).
                // So, we need to hack in and try to figure out when thrust needs to be applied.
                if (vehicle && AvatarInputHandler.main.IsEnabled())
                {
                    if (techType == TechType.Seamoth)
                    {
                        bool flag = vehicle.transform.position.y < Ocean.main.GetOceanLevel() && vehicle.transform.position.y < vehicle.worldForces.waterDepth && !vehicle.precursorOutOfWater;
                        appliedThrottle = flag && GameInput.GetMoveDirection().sqrMagnitude > .1f;
                    }
                    else if (techType == TechType.Exosuit)
                    {
                        Exosuit exosuit = vehicle as Exosuit;
                        if (exosuit)
                        {
                            appliedThrottle = (bool)exosuit.ReflectionGet("_jetsActive") && (float)exosuit.ReflectionGet("thrustPower") > 0f;
                        }
                    }
                }
            }
            else if (sub != null && Player.main.isPiloting)
            {
                guid = GuidHelper.GetGuid(sub.gameObject);
                position = sub.gameObject.transform.position;
                rotation = sub.gameObject.transform.rotation;
                Rigidbody rigidbody = sub.GetComponent<Rigidbody>();
                velocity = rigidbody.velocity;
                angularVelocity = rigidbody.angularVelocity;
                techType = TechType.Cyclops;

                SubControl subControl = sub.GetComponent<SubControl>();
                steeringWheelYaw = (float)subControl.ReflectionGet("steeringWheelYaw");
                steeringWheelPitch = (float)subControl.ReflectionGet("steeringWheelPitch");
                appliedThrottle = subControl.appliedThrottle && (bool)subControl.ReflectionGet("canAccel");
            }
            else
            {
                return Optional<VehicleModel>.Empty();
            }

            VehicleModel model = new VehicleModel(techType,
                                                  guid,
                                                  position,
                                                  rotation,
                                                  velocity,
                                                  angularVelocity,
                                                  steeringWheelYaw,
                                                  steeringWheelPitch,
                                                  appliedThrottle);

            return Optional<VehicleModel>.Of(model);
        }
    }
}
