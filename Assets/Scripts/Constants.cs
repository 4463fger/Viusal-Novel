/*
* ┌──────────────────────────────────┐
* │  描    述: Excel文件的存储路径              
* │  类    名: Constants.cs       
* │  创 建 人: 4463fger                     
* └──────────────────────────────────┘
*/

using UnityEngine;
// ReSharper disable InconsistentNaming

namespace VN
{
    public class Constants
    {
        public static string STROY_PATH = "Assets/Resources/story/";
        public static string DEFAULT_STORY_FILE_NAME = "1";
        public static string EXCEL_FILE_EXTENSION = ".xlsx";
        public static int DEFAULT_START_LINE = 1;
        
        public static string AVATAR_PATH = "image/avatar/"; 
        public static string BACKGROUND_PATH = "image/background/";
        public static string BUTTON_PATH = "image/button/";
        public static string CHARACTER_PATH = "image/character/";
        public static string IMAGE_LOAD_FAILED = "Failed to load image: ";

        public static string AUTO_ON = "autoplayon";
        public static string AUTO_OFF = "autoplayoff";
        
        public static string VOCAL_PATH = "audio/vocal/";
        public static string MUSIC_PATH = "audio/music/";
        public static string AUDIO_LOAD_FAILED = "Failed to load audio: ";
        
        public static string NO_DATA_FOUND = "No data found";
        public static string END_OF_STORY = "End of story";
        public static string CHOICE = "choice";
        public static float DEFAULT_WAITING_SECONDS = 0.1f;    //默认打字机速度

        public static string APPEAR_AT = "appearAt"; 
        public static string DISAPPEAR = "disappear";
        public static string MOVE_TO = "moveTo";
        public static int DURATION_TIME = 1;    //动画的持续效果
        public static string COORDINATE_MISSING = "Coordinate missing"; //X坐标缺失报错
    }
}