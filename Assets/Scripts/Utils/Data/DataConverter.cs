using ClosedXML.Excel;
using GameCore.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataConverter : MonoBehaviour
{

#if UNITY_EDITOR
    public static List<T> ReadDataFromXlsx<T>(string sheetName, XLWorkbook workbook) where T : class, new()
    {
        var dataType = typeof(T);
        List<T> ret = new List<T>();

        IXLWorksheet sheet;
        if (workbook.TryGetWorksheet(sheetName, out sheet) == false)
        {
            throw new System.Exception("Table doen't exist");
        }

        int count = 0;
        int row = 1;
        bool isEmpty = false;

        Dictionary<string, int> dic = new Dictionary<string, int>();
        for (int fieldColumn = 1; fieldColumn <= dataType.GetFields().Length; fieldColumn++)
        {
            string val = sheet.Cell(row, fieldColumn).GetValue<String>();
            if (string.IsNullOrWhiteSpace(val))
                break;
            dic.Add(val, fieldColumn);
        }
        while (count++ < 1000)
        {
            row++;
            T data = new T();
            foreach (var fi in dataType.GetFields())
            {
                int col;
                if (!dic.TryGetValue(fi.Name, out col))
                    continue;
                var val = sheet.Cell(row, col).GetValue<String>();
                if (string.IsNullOrWhiteSpace(val))
                {
                    isEmpty = true;
                    break;
                }

                Type type = fi.FieldType;



                if (type.IsEnum)
                    fi.SetValue(data, Enum.Parse(type, val));
                else if (type == typeof(string))
                    fi.SetValue(data, val);
                else if (type.IsPrimitive)
                    fi.SetValue(data, Convert.ChangeType(val, type));
                else if (type.GetInterface("IParsable") != null)
                {
                    var parsableObj = System.Activator.CreateInstance(type) as IParsable;
                    parsableObj.FillFromStr(val);
                    fi.SetValue(data, parsableObj);
                }
            }
            if (isEmpty)
                break;
            ret.Add(data);
        }
        return ret;
    }


    [MenuItem("xlsx/ReadExcel")]
    private static void ReadExcel()
    {
        Debug.Log("ReadExcel");

        //파일이 있는지 확인
        var xlsxPath = "Assets/Resources/Data/xlsx/data.xlsx";
        var isExist = File.Exists(xlsxPath);
        Debug.Log("IsExist " + isExist);


        //파일을 읽은 후 파일스트림 닫기 및 dispose
        XLWorkbook workbook;
        using (var stream = File.Open(xlsxPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            workbook = new XLWorkbook(stream);
            stream.Close();
        }

        //리플렉션으로 자동
        var assetPath = "Assets/Resources/Prefabs/DataScriptable/data.asset";
        var gameDataType = typeof(GameData);
        var loadedAsset = AssetDatabase.LoadAssetAtPath<GameData>(assetPath);

        foreach (var fi in gameDataType.GetFields())
        {
            //List<Item> data = ReadDataFromXslx<Item>(fi.Name, workbook);

            //제네릭타입 알아내기
            Type type = fi.FieldType.GetGenericArguments()[0];
            //함수 리플렉션 호출
            var method = typeof(DataConverter).GetMethod("ReadDataFromXlsx").MakeGenericMethod(type);
            var data = method.Invoke(null, new object[] { fi.Name, workbook });

            if (loadedAsset == null)
            {
                loadedAsset = ScriptableObject.CreateInstance<GameData>();

                fi.SetValue(loadedAsset, data);
                AssetDatabase.CreateAsset(loadedAsset, assetPath);
            }
            else
            {
                fi.SetValue(loadedAsset, data);
            }
        }
        EditorUtility.SetDirty(loadedAsset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif

}
