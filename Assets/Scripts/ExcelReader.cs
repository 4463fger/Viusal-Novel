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
            public string speaker;  //说话者名称
            public string content;  //说话内容
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
                            data.speaker = reader.GetString(0);
                            data.content = reader.GetString(1);
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