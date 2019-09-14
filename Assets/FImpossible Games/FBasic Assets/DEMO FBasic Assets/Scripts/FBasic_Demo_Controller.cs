using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FIMSpace.Basics.Demo
{
    /// <summary>
    /// FM: Class to controll demo scene for FBasicAssets
    /// </summary>
    public class FBasic_Demo_Controller : MonoBehaviour
    {
        public Text descriptionText;
        public Text numberText;
        public Text nameText;

        public List<GameObject> dontDestroyOnLoad;

        int currentScene;
        List<string> scenes;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if ( dontDestroyOnLoad != null )
            {
                foreach (GameObject o in dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(o);
                }
            }
        }

        private void Start()
        {
            scenes = new List<string>();
            scenes.Add("S_FBasic_Empty");
            scenes.Add("S_FBasic_CharacterMovement");
            scenes.Add("S_FBasic_CharacterMovementRigidbody");
            scenes.Add("S_FBasic_CharacterMovementCharacterController");
            scenes.Add("S_FBasic_FreeCamera");
            scenes.Add("S_FBasic_FreeCameraCinematicMove");

             currentScene = 0;

            ResetTexts();
        }

        public void NextScene()
        {
            currentScene++;

            if (currentScene > scenes.Count - 1) currentScene = 0;

            SceneManager.LoadScene(scenes[currentScene]);

            ResetTexts();
        }

        public void PreviousScene()
        {
            currentScene--;

            if (currentScene < 0) currentScene = scenes.Count - 1;

            SceneManager.LoadScene(scenes[currentScene]);

            ResetTexts();
        }

        void ResetTexts()
        {
            numberText.text = currentScene + " / " + (scenes.Count - 1);
            nameText.text = scenes[currentScene];
            descriptionText.text = GetDescription(currentScene);
        }

        string GetDescription(int scene)
        {
            switch (scene)
            {
                case 0: return "This are demo scenes of simple but handy assets for your project.\nYou can use it just for testing things, or extend them for your project.";
                case 1: return "Here is example of character movement using rigidbody.\n" + ClassColor("FBasic_FheelekController") + " is derived from " + ClassColor("FBasic_RigidbodyMovement") + " and is overriding few methods to add some features to main concept.\n\nCamera in scene have attached basic third person camera controller " + ClassColor("FBasic_TPPCameraBehaviour") + ".\n\nUse " + KeyColor("Main Input Axes") + " to move, I changed parameters in InputSettings for values of input axes.\nPress " + KeyColor("Space")+" to jump." + "\nPress " + KeyColor("TAB") + " to switch cursor lock.";
                case 2: return "Here is base exmaple for character movement using rigidbody.\nPrevious example was inheriting from " + ClassColor("FBasic_RigidbodyMovement") + " component presented here.\nIf you want to use it not just for debugging you can do something similar like in " + ClassColor("FBasic_FheelekController") + "\nPress " + KeyColor("TAB") + " to switch cursor lock.";
                case 3: return "Here is exmaple for character movement using " + ClassColor("CharacterController") + " component.\nI don't recommend to use it because it's expensive when comes to about 50 objects using " + ClassColor("CharacterController") + " " + MethodColor("Move()") + " method.\nBut you have some more controll over it's behaviour than on rigidbody and it don't act with objects with not kinematic rigidbody." + "\nPress " + KeyColor("TAB") + " to switch cursor lock.";
                case 4: return "Example of handy component for camera fly.\nIt have smoothing parameters so you can customize it a little.\nHold " + KeyColor("Right Mouse Button") + " to rotate camera and " + KeyColor("Main Input Axes") + " to move, also " + KeyColor("Left Control") + " and " + KeyColor("Space") + " to move up or down and " + KeyColor("Left Shift") + " for turbo.";
                case 5: return "Same component but with different smoothness settings, you can find it useful when you need to record some shots from game, this movement sometimes can give you cool shots.";
            }

            return "";
        }

        string KeyColor(string input)
        {
            return "<color=#E77A0AFF>[" + input + "]</color>";
        }

        string ClassColor(string className)
        {
            return "<color=#206A56FF>" + className + "</color>";
        }

        string MethodColor(string className)
        {
            return "<color=#275A78FF>" + className + "</color>";
        }
    }
}
