using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace QuickEngine.Utils
{
	public static class QReflection
	{
		public static List<string> AssemblyNames
		{
			get;
			private set;
		}

		public static Dictionary<string, Type> TypeCache
		{
			get;
			private set;
		}

		public static Dictionary<Assembly, List<string>> NameSpaceCache
		{
			get;
			private set;
		}

		public static Assembly[] Assemblies
		{
			get;
			private set;
		}

		static QReflection()
		{
			AssemblyNames = new List<string>();
			TypeCache = new Dictionary<string, Type>();
			NameSpaceCache = new Dictionary<Assembly, List<string>>();
			Assemblies = GetAllAssemblies();
		}

		public static void PrintManifestResources()
		{
			string[] manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
			string[] array = manifestResourceNames;
			foreach (string message in array)
			{
				UnityEngine.Debug.Log(message);
			}
		}

		public static Type GetTypeByQualifiedName(string name)
		{
			try
			{
				TypeCache.TryGetValue(name, out Type value);
				if (object.ReferenceEquals(value, null))
				{
					if (Assemblies == null || Assemblies.Any())
					{
						Assemblies = GetAllAssemblies();
					}
					foreach (Assembly item in from assembly in Assemblies
						where !AssemblyNames.Contains(assembly.FullName)
						select assembly)
					{
						AssemblyNames.Add(item.FullName);
					}
					foreach (string assemblyName in AssemblyNames)
					{
						value = Type.GetType(name + "," + assemblyName);
						if (!object.ReferenceEquals(value, null))
						{
							break;
						}
					}
					if (object.ReferenceEquals(value, null))
					{
						foreach (string assemblyName2 in AssemblyNames)
						{
							string text = assemblyName2.Substring(0, assemblyName2.IndexOf(",", StringComparison.Ordinal));
							value = Type.GetType(text + "." + name + "," + assemblyName2);
							if (!object.ReferenceEquals(value, null))
							{
								break;
							}
						}
					}
					if (object.ReferenceEquals(value, null))
					{
						return null;
					}
					TypeCache.Add(name, value);
				}
				return value;
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.LogError(string.Format("QReflection - Get Type by Qualified Name : Can't find the type - {0} - with the exception." + arg, name));
				return null;
			}
		}

		public static string GetQualifiedName(string name, string @namespace = "")
		{
			string empty = string.Empty;
			if (Assemblies == null || !Assemblies.Any())
			{
				Assemblies = GetAllAssemblies();
			}
			List<Type> list = new List<Type>();
			Assembly[] assemblies = Assemblies;
			foreach (Assembly assembly in assemblies)
			{
				list.AddRange(assembly.GetTypes());
			}
			foreach (Type item in list)
			{
				if (!string.IsNullOrEmpty(item.AssemblyQualifiedName) && item.AssemblyQualifiedName.Contains(name) && item.AssemblyQualifiedName.Contains(@namespace))
				{
					return item.AssemblyQualifiedName;
				}
			}
			return empty;
		}

		public static Type GetType(string name, string @namespace = "")
		{
			if (Assemblies == null || !Assemblies.Any())
			{
				Assemblies = GetAllAssemblies();
			}
			List<Type> list = new List<Type>();
			Assembly[] assemblies = Assemblies;
			foreach (Assembly assembly in assemblies)
			{
				list.AddRange(assembly.GetTypes());
			}
			Type type2 = list.FirstOrDefault((Type type) => type.AssemblyQualifiedName != null && type.AssemblyQualifiedName.Contains(name) && type.AssemblyQualifiedName.Contains(@namespace));
			if (type2 != null && !string.IsNullOrEmpty(type2.AssemblyQualifiedName) && !TypeCache.ContainsKey(type2.AssemblyQualifiedName))
			{
				TypeCache.Add(type2.AssemblyQualifiedName, type2);
			}
			return type2;
		}

		public static List<string> GetNameSpaces(Assembly assembly)
		{
			if (NameSpaceCache.TryGetValue(assembly, out List<string> value))
			{
				return value;
			}
			value = (from t in assembly.GetTypes()
				select t.Namespace).Distinct().ToList();
			if (!NameSpaceCache.ContainsKey(assembly))
			{
				NameSpaceCache.Add(assembly, value);
			}
			return value;
		}

		public static Assembly[] GetAllAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}

		public static object GetSingletonInstance(Type type, string singletonName, bool singletonIsProperty, BindingFlags flags = BindingFlags.Static | BindingFlags.Public)
		{
			if (type == null)
			{
				return null;
			}
			if (singletonIsProperty)
			{
				return type.GetProperty(singletonName, flags)?.GetValue(null, null);
			}
			return type.GetField(singletonName, flags)?.GetValue(null);
		}

		public static object GetSingletonProperty(Type type, string singletonName, string propertyName, bool singletonIsProperty = true, BindingFlags singletonFlags = BindingFlags.Static | BindingFlags.Public)
		{
			object singletonInstance = GetSingletonInstance(type, singletonName, singletonIsProperty, singletonFlags);
			if (singletonInstance == null)
			{
				return null;
			}
			return type.GetProperty(propertyName)?.GetValue(singletonInstance, null);
		}

		public static object GetSingletonField(Type type, string singletonName, string fieldName, bool singletonIsProperty = true, BindingFlags singletonFlags = BindingFlags.Static | BindingFlags.Public)
		{
			object singletonInstance = GetSingletonInstance(type, singletonName, singletonIsProperty, singletonFlags);
			if (singletonInstance == null)
			{
				return null;
			}
			return type.GetField(fieldName)?.GetValue(singletonInstance);
		}

		public static bool SetSingletonProperty(Type type, string singletonName, string propertyName, object value, bool singletonIsProperty = true, BindingFlags singletonFlags = BindingFlags.Static | BindingFlags.Public)
		{
			object singletonInstance = GetSingletonInstance(type, singletonName, singletonIsProperty, singletonFlags);
			if (singletonInstance == null)
			{
				return false;
			}
			PropertyInfo property = type.GetProperty(propertyName);
			if (property == null)
			{
				return false;
			}
			property.SetValue(singletonInstance, value, null);
			return true;
		}

		public static bool SetSingletonField(Type type, string singletonName, string fieldName, object value, bool singletonIsProperty = true, BindingFlags singletonFlags = BindingFlags.Static | BindingFlags.Public)
		{
			object singletonInstance = GetSingletonInstance(type, singletonName, singletonIsProperty, singletonFlags);
			if (singletonInstance == null)
			{
				return false;
			}
			FieldInfo field = type.GetField(fieldName);
			if (field == null)
			{
				return false;
			}
			field.SetValue(singletonInstance, value);
			return true;
		}
	}
}
