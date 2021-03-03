using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;

using FrameControl;

namespace FrameControl
{
    public class SingleChoice4 : MonoBehaviour
    {
        public Toggle answer;
        public bool comfirm = false;
        public Button button;
        public GameObject correctShow;
        public GameObject errorShow;
        // Start is called before the first frame update
        void Start()
        {
            comfirm = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (answer.isOn && comfirm)
            {
                Debug.Log("you are right!");
                correctShow.SetActive(true);
                button.interactable = false;
            }
            else if (answer.isOn == false && comfirm)
            {
                Debug.Log("wrong");
                comfirm = false;
                errorShow.SetActive(true);
                button.interactable = false;
            }
        }

        public void clickcomfirm()
        {
            comfirm = true;
        }

        public void BackQuest()
        {
            errorShow.SetActive(false);
            button.interactable = true;
        }

        public void EndQuest()
        {
            correctShow.SetActive(false);
            button.interactable = true;
            gameObject.SetActive(false);
            AnimationControl.play = true;
        }
    }
}
