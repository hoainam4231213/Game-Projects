using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BYDataTableMaker 
{
    [MenuItem("Assets/Create Binary file for tab delimited(txt)",false,1)]
    // Start is called before the first frame update
    private static void CreateBinaryFile()
    {
        foreach(UnityEngine.Object obj in Selection.objects)
        {
            TextAsset txtFile = (TextAsset)obj;
            string tableName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(txtFile));
            ScriptableObject scriptableObject = ScriptableObject.CreateInstance(tableName);
            if (scriptableObject == null)
                return;
            AssetDatabase.CreateAsset(scriptableObject, "Assets/Resources/DataTable/" + tableName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            BYDataBase bYDataBase = (BYDataBase)scriptableObject;
            bYDataBase.CreateBinaryFile(txtFile);
            EditorUtility.SetDirty(bYDataBase);
        }
    }
}
