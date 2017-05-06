using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Honours
{
    public class CommonButtonAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent OnPressedEnd;

        private Action playAnimationEnd;
        public Action OnPlayEnd
        {
            get
            {
                return playAnimationEnd;
            }

            set
            {
                playAnimationEnd = value;
            }
        }
        private Animator buttonAnimator;
        private Button playButton;

        //states
        private const string PLAY = "playButtonPlay";
        public void PlayPressed ()
        {
            buttonAnimator.Play(PLAY);
        }

        public void PlayAnimationEnd ()
        {
            if (playAnimationEnd != null)
            {
                playAnimationEnd(); 
            }
            OnPressedEnd.Invoke();
            Debug.Log("PLAY ANIMATION END");
        }

        private void Start()
        {
            buttonAnimator = GetComponent<Animator>();
            playButton = GetComponent<Button>();

            playButton.onClick.AddListener(() => PlayPressed());
        }

    } 
}
