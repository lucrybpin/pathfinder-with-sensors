using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CryptoGame {

    public class TooltipSystem : MonoBehaviour {

        public static TooltipSystem Instance;

        [SerializeField] Tooltip tooltip;
        [SerializeField] CanvasGroup canvasGroup;

        private void Awake ()
        {
            Instance = this;
        }

        public static void Show (string content, string header = "")
        {
            Instance.tooltip.SetText( content, header );
            //Instance.tooltip.gameObject.SetActive( true );
            Instance.canvasGroup.DOFade( .84f, .25f );
        }

        public static void Hide ()
        {
            Instance.canvasGroup.DOFade( 0f, .25f );
            //Instance.tooltip.gameObject.SetActive( false );

        }

    }

}