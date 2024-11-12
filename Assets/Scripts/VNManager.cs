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

namespace VN
{
    public class VNManager : UnityEngine.MonoBehaviour
    {
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI speakingContent;
        public TypeWriterEffect typeWriterEffect;

        private string filePath = Constants.STORY_PATH;
        private List<ExcelReader.ExcelData> storyData;
        private int currentLine = 0;

        private void Start()
        {
            LoadStoryFromFile(filePath);
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
                Debug.LogError("No data found in the file");
            }
        }

        /// <summary>
        /// 一行一行显示文本
        /// </summary>
        void DisplayNextLine()
        {
            if (currentLine >= storyData.Count)
            {
                Debug.Log("End of Story");
                return;
            }

            // 如果正在打字，则直接打字完成
            if (typeWriterEffect.IsTyping())
            {
                typeWriterEffect.CompleteLine();
            }
            else
            {
                var data = storyData[currentLine];
                speakerName.text = data.speaker;
                typeWriterEffect.StartTyping(data.content);
                currentLine++;
            }
        }
    }
}