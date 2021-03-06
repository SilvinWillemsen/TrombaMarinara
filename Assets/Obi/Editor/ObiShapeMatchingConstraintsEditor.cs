using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Obi{
	
	/**
	 * Custom inspector for ObiShapeMatchingConstraints component. 
	 */
	
	[CustomEditor(typeof(ObiShapeMatchingConstraints)), CanEditMultipleObjects] 
	public class ObiShapeMatchingConstraintsEditor : Editor
	{
		
		ObiShapeMatchingConstraints constraints;
		
		public void OnEnable(){
			constraints = (ObiShapeMatchingConstraints)target;
		}
		
		public override void OnInspectorGUI() {
			
			serializedObject.UpdateIfRequiredOrScript();
			
			Editor.DrawPropertiesExcluding(serializedObject,"m_Script");
			
			// Apply changes to the serializedProperty
			if (GUI.changed){
				
				serializedObject.ApplyModifiedProperties();
				
				constraints.PushDataToSolver();
				
			}
			
		}
		
	}
}
