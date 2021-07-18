using UnityEngine;
using System.IO;
using System.Security.Cryptography;

public class SaveManager : MonoBehaviour
{
    private static readonly string privateKey = "1718hy9dsf0jsdlfjds0pa9ids78ahgf81h32re";

    private static readonly string dataPath = Application.streamingAssetsPath;
    private static readonly string directoryName = "SaveData";

    public static void SaveToJson<T>(T t, string fileName)
    {
        string jsonString = DataToJson(t);
        File.WriteAllText(dataPath + "/" + directoryName + "/" + fileName + ".json", jsonString);
        string encryptString = Encrypt(jsonString);
        SaveFile(encryptString, fileName);
    }

    public static T LoadFromJson<T>(string fileName)
    {
        if (!File.Exists(GetPath(fileName))) return default;

        string encryptData = LoadFile(GetPath(fileName));
        string decryptData = Decrypt(encryptData);

        return JsonToData<T>(decryptData);
    }

    static string DataToJson<T>(T t)
    {
        string jsonData = JsonUtility.ToJson(t, true);
        return jsonData;
    }

    static T JsonToData<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    static void SaveFile(string jsonData, string path)
    {
        using (FileStream fs = new FileStream(GetPath(path), FileMode.Create, FileAccess.Write))
        {
            //파일로 저장할 수 있게 바이트화
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            //bytes의 내용물을 0 ~ max 길이까지 fs에 복사
            fs.Write(bytes, 0, bytes.Length);
        }
    }

    //파일 불러오기
    static string LoadFile(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            //파일을 바이트화 했을 때 담을 변수를 제작
            byte[] bytes = new byte[(int)fs.Length];

            //파일스트림으로 부터 바이트 추출
            fs.Read(bytes, 0, (int)fs.Length);

            //추출한 바이트를 json string으로 인코딩
            string jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            return jsonString;
        }
    }

    private static string Encrypt(string data)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateEncryptor();
        byte[] results = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Convert.ToBase64String(results, 0, results.Length);
    }

    private static string Decrypt(string data)
    {
        byte[] bytes = System.Convert.FromBase64String(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateDecryptor();
        byte[] resultArray = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Text.Encoding.UTF8.GetString(resultArray);
    }


    private static RijndaelManaged CreateRijndaelManaged()
    {
        byte[] keyArray = System.Text.Encoding.UTF8.GetBytes(privateKey);
        RijndaelManaged result = new RijndaelManaged();

        byte[] newKeysArray = new byte[16];
        System.Array.Copy(keyArray, 0, newKeysArray, 0, 16);

        result.Key = newKeysArray;
        result.Mode = CipherMode.ECB;
        result.Padding = PaddingMode.PKCS7;
        return result;
    }

    static string GetPath(string fileName)
    {
        return Path.Combine(dataPath + "/" + directoryName + "/" + fileName + ".bin");
    }
}