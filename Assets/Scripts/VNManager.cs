/*
* ┌──────────────────────────────────┐
* │  描    述: 主控制器               
* │  类    名: VNManager.cs       
* │  创 建 人: 4463fger                 
* └──────────────────────────────────┘
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace VN
{
    public class VNManager : UnityEngine.MonoBehaviour
    {
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI speakingContent;
        public TypeWriterEffect typeWriterEffect;
        public Image avatarImage;
        public AudioSource vocalAudio;
        public Image backgroundImage;
        public AudioSource backgroundMusic;
        public Image characterImage1;
        public Image characterImage2;

        public GameObject choicePanel;
        public Button choiceButton1;
        public Button choiceButton2;
        
        private string storyPath = Constants.STROY_PATH;
        private string defaultStoryFileName = Constants.DEFAULT_STORY_FILE_NAME;
        private string excelFileExtension = Constants.EXCEL_FILE_EXTENSION; // 文件后缀名
        private List<ExcelReader.ExcelData> storyData;
        private int currentLine = Constants.DEFAULT_START_LINE;

        private void Start()
        {
            InitializeAndLoadStory(defaultStoryFileName);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DisplayNextLine();
            }
        }

        private void InitializeAndLoadStory(string fileName)
        {
            Init();
            LoadStoryFromFile(fileName);
            DisplayNextLine();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            currentLine = Constants.DEFAULT_START_LINE; //当前初始化为默认行
            avatarImage.gameObject.SetActive(false);
            backgroundImage.gameObject.SetActive(false);
            characterImage1.gameObject.SetActive(false);
            characterImage2.gameObject.SetActive(false);
            choicePanel.SetActive(false);
        }

        /// <summary>
        /// 从Excel文件中读取文本
        /// </summary>
        /// <param name="path">文件存储路径</param>
        void LoadStoryFromFile(string fileName)
        {
            var path = storyPath + fileName + excelFileExtension;   //存储路径 + 文件名 + 文件后缀名
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
            if (currentLine == storyData.Count - 1)
            {
                if (storyData[currentLine].speakerName == Constants.END_OF_STORY)
                {
                    Debug.Log(Constants.END_OF_STORY);
                    return;
                }
                if (storyData[currentLine].speakerName == Constants.CHOICE)
                {
                    ShowChoices();
                    return;
                }
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
            if (NotNullNorEmpty(data.backgroundImageFileName))
            {
                UpdateBackgroundImage(data.backgroundImageFileName);
            }
            if (NotNullNorEmpty(data.backgroundMusicFileName))
            {
                PlayerBackgroundMusic(data.backgroundMusicFileName);
            }

            if (NotNullNorEmpty(data.character1Action))
            {
                UpdateCharacterImage(data.character1Action, data.character1ImageFileName,characterImage1,data.coordinateX1);
            }

            if (NotNullNorEmpty(data.character2Action))
            {
                UpdateCharacterImage(data.character2Action, data.character2ImageFileName,characterImage2,data.coordinateX2);
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
        /// 显示选择
        /// </summary>
        void ShowChoices()
        {
            var data = storyData[currentLine];
            choiceButton1.onClick.RemoveAllListeners();
            choiceButton2.onClick.RemoveAllListeners();
            choicePanel.SetActive(true);
            choiceButton1.GetComponentInChildren<TextMeshProUGUI>().text = data.speakingContent;
            choiceButton1.onClick.AddListener(() => InitializeAndLoadStory(data.avatarImageFileName));
            choiceButton2.GetComponentInChildren<TextMeshProUGUI>().text = data.vocalAudioFileName;
            choiceButton2.onClick.AddListener(() => InitializeAndLoadStory(data.backgroundImageFileName));

        }
        /// <summary>
        /// 更新头像
        /// </summary>
        /// <param name="imageFileName"></param>
        void UpdateAvatorImage(string imageFileName)
        {
            string imagePath = Constants.AVATAR_PATH + imageFileName;
            UpdateImage(imagePath, avatarImage);
        }
        
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioFileName"></param>
        void PlayerVocalAudio(string audioFileName)
        {
            string audioPath = Constants.VOCAL_PATH + audioFileName;
            PlayAudio(audioPath,vocalAudio,false);
        }


        /// <summary>
        /// 更新背景图片
        /// </summary>
        /// <param name="imageFileName"></param>
        private void UpdateBackgroundImage(string imageFileName)
        {
            string imagePath = Constants.BACKGROUND_PATH + imageFileName;
            UpdateImage(imagePath, backgroundImage);
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="musicFileName"></param>
        void PlayerBackgroundMusic(string musicFileName)
        {
            string musicPath = Constants.MUSIC_PATH + musicFileName;
            PlayAudio(musicPath,backgroundMusic,true);
        }
        
        /// <summary>
        /// 更新角色立绘
        /// </summary>
        /// <param name="action">动作</param>
        /// <param name="imageFileName">图片名</param>
        /// <param name="characterImage">图片</param>
        /// <param name="x">x坐标</param>
        void UpdateCharacterImage(string action, string imageFileName, Image characterImage, string x)
        {
            // 解析action行为
            //角色出现
            if (action.StartsWith(Constants.APPEAR_AT)) 
            {
                string imagePath = Constants.CHARACTER_PATH + imageFileName;
                //检测是否读取到X坐标
                if (NotNullNorEmpty(x))
                {
                    UpdateImage(imagePath,characterImage);
                    var newPosition = new Vector2(float.Parse(x), characterImage.rectTransform.anchoredPosition.y);
                    characterImage.rectTransform.anchoredPosition = newPosition;
                    characterImage.DOFade(1, Constants.DURATION_TIME).From(0);
                }
                else
                {
                    Debug.LogError(Constants.COORDINATE_MISSING);
                }
            }
            else if (action == Constants.DISAPPEAR) // 隐藏角色立绘
            {
                //OnComplete: 动画结束后调用
                characterImage.DOFade(0,Constants.DURATION_TIME).OnComplete(() =>
                {
                    characterImage.gameObject.SetActive(false);
                });
            }
            //将角色移动到指定位置
            else if (action.StartsWith(Constants.MOVE_TO))
            {
                if (NotNullNorEmpty(x))
                {
                    characterImage.rectTransform.DOAnchorPosX(float.Parse(x), Constants.DURATION_TIME);
                }
                else
                {
                    Debug.LogError(Constants.COORDINATE_MISSING);
                }
            }
        }
        
        /// <summary>
        /// 更新图片
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <param name="image">图片</param>
        private void UpdateImage(string imagePath, Image image)
        {
            Sprite sprite = Resources.Load<Sprite>(imagePath);
            if (sprite is not null)
            {
                image.sprite = sprite;
                image.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError(Constants.IMAGE_LOAD_FAILED + imagePath);
            }
        }
        
        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="audioPath">音频路径</param>
        /// <param name="audioSource">音频源</param>
        /// <param name="isLoop">是否循环</param>
        private void PlayAudio(string audioPath, AudioSource audioSource, bool isLoop)
        {
            AudioClip audioClip = Resources.Load<AudioClip>(audioPath);
            if (audioClip is not null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
                audioSource.loop = isLoop;
            }
            else
            {
                Debug.LogError(Constants.AUDIO_LOAD_FAILED + audioPath);
            }
        }
    }
}