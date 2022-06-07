using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CryptoGame {

    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [SerializeField] string header;

        [TextArea( 3, 10 )]
        [SerializeField] string content;

        Sequence sequence;

        public void OnPointerEnter (PointerEventData eventData) => Showtooltip();

        public void OnPointerExit (PointerEventData eventData) => HideTooltip();

        private void OnMouseEnter () => Showtooltip();

        private void OnMouseExit() => HideTooltip();

        private void Showtooltip ()
        {
            sequence = DOTween.Sequence();
            sequence.AppendCallback( () => { TooltipSystem.Show( content, header ); } );
            sequence.PrependInterval( 1f );
        }

        private void HideTooltip ()
        {
            sequence.Kill();
            TooltipSystem.Hide();
        }
    }

}