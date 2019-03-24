using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (CompositeBehaviour))]

//learning how editor scripting works (not really needed)
public class CompositeBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //column placements
        float behavioursCol = 30f;
        float weightsCol = 60f;

        //initial setup
        CompositeBehaviour cb = (CompositeBehaviour)target;

        //build inspector box f for CompositeBehaviours
        Rect rect = EditorGUILayout.BeginHorizontal();
        rect.height = EditorGUIUtility.singleLineHeight;

        //check for attached behaviours
        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            EditorGUILayout.HelpBox("No behaviours in array", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            rect = EditorGUILayout.BeginHorizontal();
            rect.height = EditorGUIUtility.singleLineHeight;
        }
        else
        {
            //create a margin to left of rect
            rect.x = behavioursCol;
            rect.width = EditorGUIUtility.currentViewWidth - 95f;
            EditorGUI.LabelField(rect, "Behaviours");
            rect.x = EditorGUIUtility.currentViewWidth - 65f;
            rect.width = weightsCol;
            EditorGUI.LabelField(rect, "Weights");

            rect.y += EditorGUIUtility.singleLineHeight * 1.2f;

            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < cb.behaviours.Length; i++)
            {
                //create a field for each behaviour
                rect.x = 5f;
                rect.width = 20f;
                EditorGUI.LabelField(rect, i.ToString());
                rect.x = behavioursCol;
                rect.width = EditorGUIUtility.currentViewWidth - 95f;
                cb.behaviours[i] = (FlockBehaviour)EditorGUI.ObjectField(rect, cb.behaviours[i], typeof(FlockBehaviour), false);

                //create a field for each weight
                rect.x = EditorGUIUtility.currentViewWidth - 65f;
                rect.width = weightsCol;
                cb.weights[i] = EditorGUI.FloatField(rect, cb.weights[i]);
                rect.y += EditorGUIUtility.singleLineHeight * 1.1f;
            }
            //need to make sure that weights are saved
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(cb);//make sure cb is saved
            }
        }

        //reset horizontal
        EditorGUILayout.EndHorizontal();
        rect.x = 5f;
        rect.width = EditorGUIUtility.currentViewWidth - 10f;
        rect.y += EditorGUIUtility.singleLineHeight * 0.5f;

        //add a button to add new behaviour
        if(GUI.Button(rect, "Add Behaviour"))
        {
            AddBehaviour(cb);
            EditorUtility.SetDirty(cb);//make sure that the change is saved
        }

        //add button to remove a behaviour
        rect.y += EditorGUIUtility.singleLineHeight * 1.5f;
        if(cb.behaviours != null && cb.behaviours.Length > 0)
        {
            if(GUI.Button(rect, "Remove Behaviour"))
            {
                RemoveBehaviour(cb);
                EditorUtility.SetDirty(cb);//make sure that the change is saved
            }
        }
    }

    //add a new behaviour
    void AddBehaviour(CompositeBehaviour cb)
    {
        int prevCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
        FlockBehaviour[] addBehaviours = new FlockBehaviour[prevCount + 1];
        float[] addWeights = new float[prevCount + 1];
        for(int i = 0; i < prevCount; i++)
        {
            addBehaviours[i] = cb.behaviours[i];
            addWeights[i] = cb.weights[i];
        }
        addWeights[prevCount] = 1f;
        cb.behaviours = addBehaviours;
        cb.weights = addWeights;
    }

    //remove a behaviour
    void RemoveBehaviour(CompositeBehaviour cb)
    {
        int prevCount = cb.behaviours.Length;
        //null if last behaviour
        if(prevCount == 1)
        {
            cb.behaviours = null;
            cb.weights = null;
            return;
        }
        FlockBehaviour[] removeBehaviours = new FlockBehaviour[prevCount - 1];
        float[] removeWeights = removeWeights = new float[prevCount - 1];
        for (int i = 0; i < prevCount - 1; i++)
        {
            removeBehaviours[i] = cb.behaviours[i];
            removeWeights[i] = cb.weights[i];
        }
        cb.behaviours = removeBehaviours;
        cb.weights = removeWeights;
    }
}
