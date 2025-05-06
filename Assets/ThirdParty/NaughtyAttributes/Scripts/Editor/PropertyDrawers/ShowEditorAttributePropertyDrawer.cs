using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NaughtyAttributes;
using NaughtyAttributes.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using PropertyDrawer = NaughtyAttributes.Editor.PropertyDrawer;

namespace NaughtyAttributes.Editor
{
	public class DerivedTypesPopupDrawer
	{
		/// <summary>
		/// (newType, oldObject):newObject
		/// </summary>
		public static Func<Type, Object, Object> CreateNewAsset;
		
		Type baseType;

		public DerivedTypesPopupDrawer(Type baseType)
		{
			this.baseType = baseType;
		}

		const bool UseCash = true;
		
		Type[]   types;
		string[] names;
     
		public Type[] Types {
			get
			{
				if (types == null || !UseCash)
					types = GetDerivedTypes(baseType);

				return types;
			}
		}

		public string[] Names
		{
			get
			{
				if (names == null)
					names = Types.Select(t => t.Name).ToArray();

				return names;
			}
		}

		public Object DrawPopup(Object current)
		{
			Type type = null;
			
			if (current)
				type = current.GetType();

			int currentIndex = -1;
			for (int i = 0; i < Types.Length; i++)
				if (Types[i] == type)
					currentIndex = i;

			GUI.changed = false;
			int index = EditorGUILayout.Popup(currentIndex, Names); 
			if (GUI.changed)
				return CreateNewAsset(Types[index], current);


			return current;
		}

		static Type[] GetDerivedTypes(Type baseType)
		{
			List<Type> types      = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				try
				{
					var typesToAdd = assembly.GetTypes()
						.Where(t => !t.IsAbstract && baseType.IsAssignableFrom(t))
						.ToArray();
                    
					types.AddRange(typesToAdd);
				}
				catch (ReflectionTypeLoadException)
				{
				}
			}

			return types.ToArray();
		}
	}
	
	[PropertyDrawer(typeof(ShowEditorAttribute))]
	public class ShowEditorAttributePropertyDrawer : PropertyDrawer
	{
		static Dictionary<Type, DerivedTypesPopupDrawer> derivedTypesDrawers = new Dictionary<Type, DerivedTypesPopupDrawer>();

		DerivedTypesPopupDrawer GetDerivedTypesDrawer(Type t)
		{
			DerivedTypesPopupDrawer value;
			
			if (derivedTypesDrawers.TryGetValue(t, out value))
				return value;
			else
			{
				value = new DerivedTypesPopupDrawer(t);
				derivedTypesDrawers.Add(t,value);
				return value;
			}
		}

		public override void DrawProperty(SerializedProperty property)
		{
			var f         = property.serializedObject.targetObject.GetType().GetField(property.propertyPath, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			var fieldType = f.FieldType;

			var drawer = GetDerivedTypesDrawer(fieldType);
			//EditorGUILayout.PropertyField(property);

			GUILayout.BeginVertical("button");
			
			GUILayout.BeginHorizontal();
			GUILayout.Label(property.displayName );
			property.objectReferenceValue = drawer.DrawPopup(property.objectReferenceValue);
			GUILayout.EndHorizontal();

			if (property.objectReferenceValue)
			{
				var e = UnityEditor.Editor.CreateEditor(property.objectReferenceValue);
				e.OnInspectorGUI();
			}

			GUILayout.EndVertical();
		}
	}
}