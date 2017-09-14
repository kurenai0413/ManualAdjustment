using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using HUX;
using HUX.Focus;
using HUX.Interaction;
using HUX.Receivers;

namespace HUX
{
    public class kuButtonReceiver : InteractionReceiver
    {
        protected enum ActionType
        {
            Translation,
            Rotation
        }

        protected enum ActionAxis
        {
            X,
            Y,
            Z
        }

        public GameObject TargetObj;
        public GameObject ControlPanelObj;
        public TextMesh   IndicatorText;

        public float TranslationStep = 0.05f;
        public float RotationStep = 45.0f;

        private bool ControlPanelStatus = true;
        private ActionType ActionStatus;

        // Use this for initialization
        void Start()
        {
            ActionStatus = ActionType.Translation;
            ToggleIndicatorText(ActionStatus);
        }

        protected override void OnTapped(GameObject obj, InteractionManager.InteractionEventArgs eventArgs)
        {
            //IndicatorText.text = obj.name + " OnTapped.";

            switch (obj.name)
            {
                case "TranslationBtn":
                    ActionStatus = ActionType.Translation;
                    ToggleIndicatorText(ActionStatus);
                    break;

                case "RotationBtn":
                    ActionStatus = ActionType.Rotation;
                    ToggleIndicatorText(ActionStatus);
                    break;

                case "ToggleBtn":
                    ControlPanelStatus = !ControlPanelStatus;
                    ToggleControlPanel(ControlPanelStatus);
                    break;

                case "BtnXUp":
                    ApplyAction(ActionStatus, ActionAxis.X, TargetObj, 1);
                    break;

                case "BtnXDown":
                    ApplyAction(ActionStatus, ActionAxis.X, TargetObj, -1);
                    break;

                case "BtnYUp":
                    ApplyAction(ActionStatus, ActionAxis.Y, TargetObj, 1);
                    break;

                case "BtnYDown":
                    ApplyAction(ActionStatus, ActionAxis.Y, TargetObj, -1);
                    break;

                case "BtnZUp":
                    ApplyAction(ActionStatus, ActionAxis.Z, TargetObj, 1);
                    break;

                case "BtnZDown":
                    ApplyAction(ActionStatus, ActionAxis.Z, TargetObj, -1);
                    break;      
            }

            base.OnTapped(obj, eventArgs);
        }

        protected override void OnHoldStarted(GameObject obj, InteractionManager.InteractionEventArgs eventArgs)
        {
            base.OnHoldStarted(obj, eventArgs);
        }

        protected override void OnFocusEnter(GameObject obj, FocusArgs args)
        {
            base.OnFocusEnter(obj, args);
        }

        protected override void OnFocusExit(GameObject obj, FocusArgs args)
        {
            base.OnFocusExit(obj, args);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        protected void ToggleControlPanel(bool toggleFlag)
        {
            ControlPanelObj.SetActive(toggleFlag);
        }

        protected void ToggleIndicatorText(ActionType Act)
        {
            switch (Act)
            {
                case ActionType.Translation:
                    IndicatorText.text = "Translation.";
                    break;

                case ActionType.Rotation:
                    IndicatorText.text = "Rotation.";
                    break;
            }
        }

        protected void ApplyAction(ActionType Act, ActionAxis ActAxis, GameObject Target, float Direction)
        {
            switch (Act)
            {
                case ActionType.Translation:
                    ApplyTranslation(ActAxis, Target, TranslationStep, Direction);
                    break;

                case ActionType.Rotation:
                    ApplyRotation(ActAxis, Target, RotationStep, Direction);
                    break;
            }
        }

        protected void ApplyTranslation(ActionAxis ActAxis, GameObject Target, float Step, float Direction)
        {
            float TanslationVal = Direction * Step;

            switch (ActAxis)
            {
                case ActionAxis.X:
                    Target.transform.Translate(new Vector3(TanslationVal, 0, 0), Space.World);
                    break;

                case ActionAxis.Y:
                    Target.transform.Translate(new Vector3(0, TanslationVal, 0), Space.World);
                    break;

                case ActionAxis.Z:
                    Target.transform.Translate(new Vector3(0, 0, TanslationVal), Space.World);
                    break;
            }
        }

        protected void ApplyRotation(ActionAxis ActAxis, GameObject Target, float Step, float Direction)
        {
            float RotationVal = Direction * Step;

            switch (ActAxis)
            {
                case ActionAxis.X:
                    Target.transform.Rotate(RotationVal, 0, 0, Space.Self);
                    break;

                case ActionAxis.Y:
                    Target.transform.Rotate(0, RotationVal, 0, Space.Self);
                    break;

                case ActionAxis.Z:
                    Target.transform.Rotate(0, 0, RotationVal, Space.Self);
                    break;
            }
        }
    }

}

