using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public static class BYDataTableMaker
{
    [MenuItem("Assets/BY/Create Binary file for tab delimited(txt)", false, 1)]
    // Start is called before the first frame update
    private static void CreateBinaryFile()
    {
        foreach (Object obj in Selection.objects)
        {
            TextAsset txtFile = (TextAsset)obj;
            string tableName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(txtFile));
            ScriptableObject scriptable = ScriptableObject.CreateInstance(tableName);
            if (scriptable == null)
                return;
            AssetDatabase.CreateAsset(scriptable, "Assets/Resources/DataTable/" + tableName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            BYDataBase bYDataBase = (BYDataBase)scriptable;
            bYDataBase.CreateBinaryFile(txtFile);
            EditorUtility.SetDirty(bYDataBase);
        }
    }
}
