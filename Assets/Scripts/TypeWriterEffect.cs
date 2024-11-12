/*
* ┌──────────────────────────────────┐
* │  描    述: 打字机效果                      
* │  类    名: TypeWriterEffect.cs       
* │  创 建 人: 4463fger                      
* └──────────────────────────────────┘
*/

using System.Collections;
using UnityEngine;
using TMPro;

namespace VN
{
    public class TypeWriterEffect : MonoBehaviour
    {
        public TextMeshProUGUI textDisPlay; //展示的文本
        public float waitingSeconds = Constants.DEFAULT_WAITING_SECONDS;

        private Coroutine typingCoroutine;  //打字协程
        private bool isTyping;

        /// <summary>
        /// 开始打字机
        /// </summary>
        /// <param name="text">文本</param>
        public void StartTyping(string text)
        {
            // 停止之前的打字
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            typingCoroutine = StartCoroutine(TypeLine(text));
        }
        
        private IEnumerator TypeLine(string text)
        {
            isTyping = true;
            textDisPlay.text = text;
            textDisPlay.maxVisibleCharacters = 0; //可见字符的数量

            for (int i = 0; i < text.Length; i++)
            {
                textDisPlay.maxVisibleCharacters = i;
                yield return new WaitForSeconds(waitingSeconds);
            }

            isTyping = false;
        }

        public void CompleteLine()
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            //显示全部
            textDisPlay.maxVisibleCharacters = textDisPlay.text.Length;
            isTyping = false;
        }
        
        /// <summary>
        /// 是否正在打字
        /// </summary>
        /// <returns></returns>
        public bool IsTyping()
        {
            return isTyping;
        }
    }
}