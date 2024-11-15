/*
* ┌──────────────────────────────────┐
* │  描    述: Excel表格阅读器                      
* │  类    名: ExcelReader.cs       
* │  创 建 人: 4463fger                      
* └──────────────────────────────────┘
*/

using System.Collections.Generic;
using System.IO;
using System.Text;
using ExcelDataReader;
using UnityEngine;

namespace VN
{
    public class ExcelReader : MonoBehaviour
    {
        /// <summary>
        /// Excel表格数据
        /// </summary>
        public struct ExcelData
        {
            public string speakerName;  //说话者名称
            public string speakingContent;  //说话内容
            public string avatarImageFileName;  //头像文件名
            public string vocalAudioFileName;   //音频文件名
            public string backgroundImageFileName; //背景图片名字
            public string backgroundMusicFileName; //背景音乐名字
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回ExcelData</returns>
        public static List<ExcelData> ReadExcel(string filePath)
        {
            List<ExcelData> excelData = new List<ExcelData>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath,FileMode.Open,FileAccess.Read))
            {
                //创建ExcelDataReader用于读取Excel
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        //循环条件: 下一行有数据
                        while (reader.Read())
                        {
                            ExcelData data = new ExcelData();
                            data.speakerName = reader.IsDBNull(0) ? string.Empty : reader.GetValue(0)?.ToString();
                            data.speakingContent = reader.IsDBNull(1) ? string.Empty : reader.GetValue(1)?.ToString();
                            data.avatarImageFileName = reader.IsDBNull(2) ? string.Empty : reader.GetValue(2)?.ToString();
                            data.vocalAudioFileName = reader.IsDBNull(3) ? string.Empty : reader.GetValue(3)?.ToString();
                            data.backgroundImageFileName = reader.IsDBNull(4) ? string.Empty : reader.GetValue(4)?.ToString();
                            data.backgroundMusicFileName = reader.IsDBNull(5) ? string.Empty : reader.GetValue(5)?.ToString();
                            excelData.Add(data);
                        }
                    } 
                    //循环条件: 有下一张表
                    while (reader.NextResult());
                }
            }
            return excelData;
        }
    }
}