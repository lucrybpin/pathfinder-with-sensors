using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CryptoGame {

    [ExecuteInEditMode()]
    public class Tooltip : MonoBehaviour {

        [SerializeField] TextMeshProUGUI headerField;
        [SerializeField] TextMeshProUGUI contentField;
        [SerializeField] LayoutElement layoutElement;
        [SerializeField] int characterWrapLimit;

        [SerializeField] RectTransform rectTransform;

        private void Awake ()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetText(string content, string header = "")
        {
            if (string.IsNullOrEmpty(header))
            {
                headerField.gameObject.SetActive( false );
            }
            else
            {
                headerField.gameObject.SetActive( true );
                headerField.text = header;
            }

            contentField.text = content;

            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            layoutElement.enabled = ( headerLength > characterWrapLimit || contentLength > characterWrapLimit ) ? true : false;
        }

        private void Update ()
        {
            if (Application.isEditor)
            {
                int headerLength = headerField.text.Length;
                int contentLength = contentField.text.Length;

                layoutElement.enabled = ( headerLength > characterWrapLimit || contentLength > characterWrapLimit ) ? true : false; 
            }

            Vector2 position = Input.mousePosition; //TODO use new input system

            float pivotX = position.x / Screen.width;
            float pivotY = position.y / Screen.height;

            rectTransform.pivot = new Vector2( pivotX, pivotY );

            transform.position = position;
        }
    }


}