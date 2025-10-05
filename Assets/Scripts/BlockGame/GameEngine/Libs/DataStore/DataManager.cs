using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace BlockGame.GameEngine.Libs.DataStore
{
	public class DataManager
	{
		private static DataManager instance;

		private string dataFilePath = Application.persistentDataPath + "/GameData.dat";

		private DesEncryption desEncrypt = new DesEncryption(GlobalConstants.DesKey);

		private IFormatter form = new BinaryFormatter();

		public static DataManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new DataManager();
				}
				return instance;
			}
		}

		public object Load()
		{
			IFormatter formatter = new BinaryFormatter();
			UnityEngine.Debug.Log("dataFilePath  " + dataFilePath);
			if (File.Exists(dataFilePath))
			{
				UnityEngine.Debug.Log("User Data Exists");
				byte[] bytes = File.ReadAllBytes(dataFilePath);
				byte[] buffer = desEncrypt.Decrypt(bytes);
				MemoryStream serializationStream = new MemoryStream(buffer);
				return formatter.Deserialize(serializationStream);
			}
			return null;
		}

		public void Save(object instance)
		{
			MemoryStream memoryStream = new MemoryStream();
			form.Serialize(memoryStream, instance);
			byte[] array = desEncrypt.Encrypt(memoryStream.ToArray());
			File.WriteAllBytes(dataFilePath, array);
			UnityEngine.Debug.Log("Encrypted: " + array.Length);
		}
	}
}
