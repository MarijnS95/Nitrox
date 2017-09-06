using System;
using UnityEngine;

namespace NitroxClient.MonoBehaviours
{
    class MultiplayerSeaMoth : MultiplayerVehicleControl<Vehicle>
    {
        private bool lastThrottle = false;
        private const float turnSpeedMax = 19f;
        private float turnDampening = 5f;
        private float turnSpeed;
        private SeaMoth seamoth;
        private Transform propeller;

        protected override void Awake()
        {
            steeringControl = seamoth = GetComponent<SeaMoth>();
            base.Awake();

            propeller = transform.Find("Model/Submersible_SeaMoth/Master_jnt/propeller_shell_jnt/propeller_jnt");
        }

        [NitroxReloader.ReloadableMethod]
        private void Update()
        {
            turnSpeed = Mathf.MoveTowards(turnSpeed, 0f, Time.deltaTime * turnDampening);
            var prop = transform.Find("Model/Submersible_SeaMoth_extras/Master_jnt/propeller_shell_jnt/propeller_jnt");
            prop.eulerAngles = new Vector3(0f, -turnSpeed, 0f);
            prop.localEulerAngles = new Vector3(0f, -turnSpeed, 0f);
            //prop.Rotate(new Vector3(0f, -turnSpeed, 0f), Space.Self);
            transform.Find("Model/Submersible_SeaMoth/Submersible_seaMoth_geo/Submersible_SeaMoth_propeller_geo").eulerAngles = new Vector3(0f, -turnSpeed, 0f);
            transform.Find("Model/Submersible_SeaMoth/Master_jnt/propeller_shell_jnt/propeller_jnt").eulerAngles = new Vector3(0f, -turnSpeed, 0f);
            transform.Find("Model/Submersible_SeaMoth/Submersible_seaMoth_geo/Submersible_SeaMoth_propeller_geo").localEulerAngles = new Vector3(0f, -turnSpeed, 0f);
            transform.Find("Model/Submersible_SeaMoth/Master_jnt/propeller_shell_jnt/propeller_jnt").localEulerAngles = new Vector3(0f, -turnSpeed, 0f);
            //transform.Find("Model/Submersible_SeaMoth/Submersible_seaMoth_geo/Submersible_SeaMoth_propeller_geo").Rotate(new Vector3(0f, -turnSpeed, 0f), Space.Self);
            //transform.Find("Model/Submersible_SeaMoth/Master_jnt/propeller_shell_jnt/propeller_jnt").Rotate(new Vector3(0f, -turnSpeed, 0f), Space.Self);
        }

        internal override void Exit()
        {
            seamoth.bubbles.Stop();
            base.Exit();
        }

        internal override void SetThrottle(bool isOn)
        {
            if (isOn != lastThrottle)
            {
                if (isOn)
                {
                    turnSpeed = turnSpeedMax;
                    seamoth.bubbles.Play();
                }
                else
                {
                    seamoth.bubbles.Stop();
                }

                lastThrottle = isOn;
            }
        }
    }
}
