using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Reflection;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif
public class BYDataBase : ScriptableObject
{
    public virtual void CreateBinaryFile(TextAsset tabFile)
    {

    }
}
public class ConfigCompare<T> : IComparer<T> where T : class, new()
{
    private List<FieldInfo> keyInfos = new List<FieldInfo>();
    public ConfigCompare(params string[] keyInfoNames)
    {
        for (int i = 0; i < keyInfoNames.Length; i++)
        {
            FieldInfo keyInfo = typeof(T).GetField(keyInfoNames[i], BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            keyInfos.Add(keyInfo);
        }
    }
    public int Compare(T x, T y)
    {
        int result = 0;
        for (int i = 0; i < keyInfos.Count; i++)
        {
            object val_x = keyInfos[i].GetValue(x);
            object val_y = keyInfos[i].GetValue(y);
            result = ((IComparable)val_x).CompareTo(val_y);
            if (result != 0)
            {
                break;
            }
        }
        return result;
    }
    public T CreateKeySearch(params object[] values)
    {
        T key = new T();
        for (int i = 0; i < values.Length; i++)
        {
            keyInfos[i].SetValue(key, values[i]);
        }
        return key;
    }
}
public abstract class BYDataTable<T> : BYDataBase where T : class, new()
{
    private ConfigCompare<T> configCompare;
    public abstract ConfigCompare<T> DefineCompare();
    [SerializeField]
    protected List<T> records = new List<T>();

    private void OnEnable()
    {
        configCompare = DefineCompare();
    }

    public override void CreateBinaryFile(TextAsset tabFile)
    {

        records.Clear();
        List<List<string>> grids = SplitTabDelimited(tabFile);
        Type recordType = typeof(T);
        FieldInfo[] fieldInfos = recordType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        for (int x = 0; x < fieldInfos.Length; x++)
        {
            Debug.LogError(fieldInfos[x].Name);
        }
        for (int i = 1; i < grids.Count; i++)
        {
            List<string> dataline = grids[i];
            string json = "{";

            for (int x = 0; x < fieldInfos.Length; x++)
            {
                // add ','
                if (x > 0)
                    json += ",";

                if (fieldInfos[x].FieldType != typeof(string))
                {
                    string dataField = "0";
                    if (x < dataline.Count)
                    {
                        if (dataline[x] != string.Empty)
                            dataField = dataline[x];

                    }
                    json += "\"" + fieldInfos[x].Name + "\":" + dataField;
                }
                else
                {
                    string dataField = string.Empty;
                    if (x < dataline.Count)
                    {
                        if (dataline[x] != string.Empty)
                            dataField = dataline[x];

                    }
                    json += "\"" + fieldInfos[x].Name + "\":\"" + dataField + "\"";
                }
            }
            json += "}";

            T r = JsonUtility.FromJson<T>(json);
            records.Add(r);
        }
        records.Sort(configCompare);
    }
    private List<List<string>> SplitTabDelimited(TextAsset tabFile)
    {
        List<List<string>> grids = new List<List<string>>();
        string[] lines = tabFile.text.Split('\n');
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > 0)
                {
                    string[] linedata = lines[i].Split('\t');
                    List<string> ls_line = new List<string>();

                    foreach (string s in linedata)
                    {
                        string newChar = Regex.Replace(s, @"\t|\n|\r", "");
                        //newChar = Regex.Replace(newChar, @"""", "\\" + "\\");
                        ls_line.Add(newChar);
                    }
                    grids.Add(ls_line);
                }

            }
        }
        return grids;
    }
    public List <T> GetAllRecords()
    {
        return records;
    }
    public T GetRecordByKeySearch(params object[] keys_)
    {
        T key = configCompare.CreateKeySearch(keys_);
        int index = records.BinarySearch(key, configCompare);
        if(index>=0&&index<records.Count)
        {
            return records[index];
        }
        else
        {
            return null;
        }
    }
}
