using UnityEngine;

namespace FIMSpace.Basics.Demo
{
    /// <summary>
    /// Class with methods to change player controller pawn
    /// </summary>
    public class FBasic_Demo_CharacterChoosePanel : MonoBehaviour
    {
        public FBasic_CharacterInputKeys TargetController; // Target character controller
        public GameObject ToActivate; // UI text
        public int ChooseCamera = -1; // Hard coded camera component switcher

        private static FBasic_CharacterInputKeys mainController;
        private static FBasic_CharacterInputKeys currentController;
        private static GameObject mainText;

        private FBasic_TPPCameraBehaviour tppCam;
        private FBasic_FreeCameraBehaviour freeCam;

        private void Start()
        {
            if (!tppCam) tppCam = Camera.main.GetComponent<FBasic_TPPCameraBehaviour>();
            if (!freeCam) freeCam = Camera.main.GetComponent<FBasic_FreeCameraBehaviour>();
            if (!mainText) mainText = FindObjectOfType<FBasic_Demo_HideText>().gameObject;
        }

        public void Switch()
        {
            if (!mainController) mainController = FindObjectOfType<FBasic_FheelekController>().GetComponent<FBasic_CharacterInputKeys>();

            if (TargetController != null)
            {
                if (mainController.enabled && currentController != TargetController)
                {
                    mainController.enabled = false;
                    TargetController.enabled = true;
                    tppCam.ToFollow = TargetController.transform;
                    SwitchActiveText();
                }
                else
                {
                    mainController.enabled = true;
                    TargetController.enabled = false;
                    tppCam.ToFollow = mainController.transform;
                    SwitchActiveText(true);
                }

                if (TargetController.enabled) currentController = TargetController; else currentController = mainController;
            }
            else
            {
                if (!tppCam.enabled)
                {
                    tppCam.enabled = true;
                    freeCam.enabled = false;
                    mainController.enabled = true;
                    SwitchActiveText(true);
                }
                else
                {
                    if (ChooseCamera == 0)
                    {
                        tppCam.enabled = false;
                        freeCam.enabled = true;
                        mainController.enabled = false;
                        freeCam.SpeedMultiplier = 10f;
                        freeCam.AccelerationSmothnessValue = 10f;
                        freeCam.RotationSmothnessValue = 10f;
                        freeCam.MouseSensitivity = 5f;

                        SwitchActiveText();
                    }
                    else
                    {
                        tppCam.enabled = false;
                        freeCam.enabled = true;
                        mainController.enabled = false;
                        freeCam.SpeedMultiplier = 4f;
                        freeCam.AccelerationSmothnessValue = 5f;
                        freeCam.RotationSmothnessValue = 3f;
                        freeCam.MouseSensitivity = 5f;
                        SwitchActiveText();
                    }
                }
            }

        }

        private void SwitchActiveText(bool restoreMain = false)
        {
            if (!restoreMain)
            {
                if (ToActivate)
                {
                    ToActivate.SetActive(true);
                    mainText.SetActive(false);
                }
            }
            else
            {
                ToActivate.SetActive(false);
                mainText.SetActive(true);
            }
        }
    }
}