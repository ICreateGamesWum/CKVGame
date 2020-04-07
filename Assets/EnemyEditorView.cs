using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(Enemy))]
//public class EnemyEditorView : Editor
//{
//    private void OnSceneGUI()
//    {
//        Enemy enemy = (Enemy)target;
//        Handles.color = Color.black;
//        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.targetRange);
//        Vector3 viewAngleA = enemy.DirFromAngle(-enemy.viewAngle / 2, false);
//        Vector3 viewAngleB = enemy.DirFromAngle(enemy.viewAngle / 2, false);

//        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleA * enemy.targetRange);
//        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleB * enemy.targetRange);

//        Handles.color = Color.red;
        
        
        
//        //foreach (Transform visibleTarget in enemy.visibleTargets)
//        //{
//        //    Handles.DrawLine(enemy.transform.position, visibleTarget.position);
//        //}
//    }
//}
