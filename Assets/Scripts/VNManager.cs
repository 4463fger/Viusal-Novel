/*
* ┌──────────────────────────────────┐
* │  描    述: 主控制器               
* │  类    名: VNManager.cs       
* │  创 建 人: 4463fger                 
* └──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VN
{
    public class VNManager : UnityEngine.MonoBehaviour
    {
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI speakingContent;
        public TypeWriterEffect typeWriterEffect;
        public Image avatarImage;
        public AudioSource vocalAudio;

        private string storyPath = Constants.STROY_PATH;
        private string defaultStoryFileName = Constants.DEFAULT_STORY_FILE_NAME;
        private List<ExcelReader.ExcelData> storyData;
        private int currentLine = 0;

        private void Start()
        {
            LoadStoryFromFile(storyPath + defaultStoryFileName);
            DisplayNextLine();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DisplayNextLine();
            }
        }

        /// <summary>
        /// 从Excel文件中读取文本
        /// </summary>
        /// <param name="path">文件存储路径</param>
        void LoadStoryFromFile(string path)
        {
            storyData = ExcelReader.ReadExcel(path);
            if (storyData == null || storyData.Count == 0)
            {
                Debug.LogError(Constants.NO_DATA_FOUND);
            }
        }

        /// <summary>
        /// 一行一行显示文本
        /// </summary>
        void DisplayNextLine()
        {
            if (currentLine >= storyData.Count)
            {
                Debug.Log(Constants.END_OF_STORY);
                return;
            }

            // 如果正在打字，则直接打字完成
            if (typeWriterEffect.IsTyping())
            {
                typeWriterEffect.CompleteLine();
            }
            else
            {
                DisplayThisLine();
            }
        }
        
        void DisplayThisLine()
        {
            var data = storyData[currentLine];
            speakerName.text = data.speakerName;
            speakingContent.text = data.speakingContent;
            typeWriterEffect.StartTyping(speakingContent.text);
            //头像名不为空,更新
            if (NotNullNorEmpty(data.avatarImageFileName))
            {
                UpdateAvatorImage(data.avatarImageFileName);
            }
            else
            {
                avatarImage.gameObject.SetActive(false);
            }

            if (NotNullNorEmpty(data.vocalAudioFileName))
            {
                PlayerVocalAudio(data.vocalAudioFileName);
            }
            currentLine++;
        }
        
        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="str">需要判断的字符名</param>
        /// <returns></returns>
        bool NotNullNorEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }
        
        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="imageFileName"></param>
        void UpdateAvatorImage(string imageFileName)
        {
            string imagePath = Constants.AVATAR_PATH + imageFileName;
            Sprite sprite = Resources.Load<Sprite>(imagePath);
            if (sprite is not null)
            {
                avatarImage.sprite = sprite;
                avatarImage.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError(Constants.IMAGE_LOAD_FAILED);
            }
        }
        
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="audioFileName"></param>
        void PlayerVocalAudio(string audioFileName)
        {
            string audioPath = Constants.VOCAL_PATH + audioFileName;
            AudioClip audioClip = Resources.Load<AudioClip>(audioPath);
            if (audioClip is not null)
            {
                vocalAudio.clip = audioClip;
                vocalAudio.Play();
            }
            else
            {
                Debug.LogError(Constants.AUDIO_LOAD_FAILED);
            }
        }
    }
}