using BlockGame.GameEngine.Libs.Log;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BlockGame.GameEngine.Libs.DataStore
{
	public class FileIOHelper
	{
		private DesEncryption desEncrypt;

		private static FileIOHelper instance;

		public static FileIOHelper Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new FileIOHelper();
				}
				return instance;
			}
		}

		public void InitDesEnc(string key)
		{
			desEncrypt = new DesEncryption(key);
		}

		public void SaveFile(string filePath, object userData)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(memoryStream, userData);
				byte[] array = desEncrypt.Encrypt(memoryStream.ToArray());
				File.WriteAllBytes(filePath, array);
			}
		}

		public object ReadFile(string filePath)
		{
			byte[] bytes = File.ReadAllBytes(filePath);
			byte[] buffer = desEncrypt.Decrypt(bytes);
			using (MemoryStream serializationStream = new MemoryStream(buffer))
			{
				IFormatter formatter = new BinaryFormatter();
				return formatter.Deserialize(serializationStream);
			}
		}
	}
}
