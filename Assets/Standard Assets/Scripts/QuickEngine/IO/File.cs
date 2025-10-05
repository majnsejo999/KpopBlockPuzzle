using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuickEngine.IO
{
	public static class File
	{
		private static List<string> listOfStrings;

		private static FileInfo[] fileInfoArray;

		private static DirectoryInfo[] directoryInfoArray;

		public static bool Exists(string path)
		{
			return System.IO.File.Exists(path);
		}

		public static void CreateDirectory(string path)
		{
			FileInfo fileInfo = new FileInfo(path);
			fileInfo.Directory.Create();
		}

		public static string GetAbsoluteDirectoryPath(string directoryName, bool debug = false)
		{
			string[] directories = Directory.GetDirectories(Application.dataPath, directoryName, SearchOption.AllDirectories);
			if (directories == null)
			{
				if (debug)
				{
					UnityEngine.Debug.LogError("[QuickEngine.IO] You searched for the [" + directoryName + "] folder, but no folder with that name exists in the current project.");
				}
				return "ERROR";
			}
			if (directories.Length > 1 && debug)
			{
				UnityEngine.Debug.LogWarning("[QuickEngine.IO] You searched for the [" + directoryName + "] folder. There are " + directories.Length + " folders with that name. Returned the folder location for the first one, but it might not be the one you're looking for. Give the folder you are looking for an unique name to avoid any issues.");
			}
			return directories[0];
		}

		public static string GetRelativeDirectoryPath(string directoryName)
		{
			string absoluteDirectoryPath = GetAbsoluteDirectoryPath(directoryName);
			return absoluteDirectoryPath.Replace(Application.dataPath, "Assets");
		}

		public static void WriteFile<T>(string filePath, T obj, Action<FileStream, T> serializeMethod)
		{
			CreateDirectory(filePath);
			FileStream fileStream = new FileStream(filePath, FileMode.Create);
			serializeMethod(fileStream, obj);
			fileStream.Close();
		}

		public static void Delete(string path)
		{
			System.IO.File.Delete(path);
		}

		public static void Move(string sourceFileName, string destFileName)
		{
			System.IO.File.Move(sourceFileName, destFileName);
		}

		public static void Rename(string sourceFileName, string destFileName)
		{
			System.IO.File.Move(sourceFileName, destFileName);
		}

		public static FileInfo[] GetFiles(string directoryPath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
			return directoryInfo.GetFiles("*.*", SearchOption.AllDirectories);
		}

		public static FileInfo[] GetFiles(string directoryPath, string fileExtension)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
			return directoryInfo.GetFiles("*." + fileExtension, SearchOption.AllDirectories);
		}

		public static string[] GetFilesNames(string directoryPath)
		{
			listOfStrings = new List<string>();
			fileInfoArray = GetFiles(directoryPath);
			if (fileInfoArray != null)
			{
				for (int i = 0; i < fileInfoArray.Length; i++)
				{
					listOfStrings.Add(fileInfoArray[i].Name.Replace(fileInfoArray[i].Extension, string.Empty));
				}
				listOfStrings.Sort();
			}
			return listOfStrings.ToArray();
		}

		public static string[] GetFilesNames(string directoryPath, string fileExtension)
		{
			listOfStrings = new List<string>();
			fileInfoArray = GetFiles(directoryPath, fileExtension);
			if (fileInfoArray != null)
			{
				for (int i = 0; i < fileInfoArray.Length; i++)
				{
					listOfStrings.Add(fileInfoArray[i].Name.Replace(fileInfoArray[i].Extension, string.Empty));
				}
				listOfStrings.Sort();
			}
			return listOfStrings.ToArray();
		}

		public static DirectoryInfo[] GetDirectories(string directoryPath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
			return directoryInfo.GetDirectories();
		}

		public static string[] GetDirectoriesNames(string directoryPath)
		{
			listOfStrings = new List<string>();
			directoryInfoArray = GetDirectories(directoryPath);
			if (directoryInfoArray != null)
			{
				for (int i = 0; i < directoryInfoArray.Length; i++)
				{
					listOfStrings.Add(directoryInfoArray[i].Name);
				}
				listOfStrings.Sort();
			}
			return listOfStrings.ToArray();
		}
	}
}
