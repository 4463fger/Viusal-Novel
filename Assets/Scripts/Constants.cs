/*
* ┌──────────────────────────────────┐
* │  描    述: Excel文件的存储路径              
* │  类    名: Constants.cs       
* │  创 建 人: 4463fger                     
* └──────────────────────────────────┘
*/

using UnityEngine;

namespace VN
{
    public class Constants
    {
        public static string STROY_PATH = "Assets/Resources/story/";
        public static string DEFAULT_STORY_FILE_NAME = "1.xlsx";
        
        public static string AVATAR_PATH = "image/avatar/"; 
        public static string VOCAL_PATH = "audio/vocal/";
        public static string AUDIO_LOAD_FAILED = "Failed to load audio: ";
        public static string IMAGE_LOAD_FAILED = "Failed to load image: ";
        
        public static string NO_DATA_FOUND = "No data found";
        public static string END_OF_STORY = "End of story";
        public static float DEFAULT_WAITING_SECONDS = 0.05f;    //默认打字机速度
    }
}