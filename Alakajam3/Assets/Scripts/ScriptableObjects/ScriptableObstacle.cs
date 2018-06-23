// Date   : #CREATIONDATE#
// Project: #PROJECTNAME#
// Author : #AUTHOR#

using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ScriptableObstacle", menuName = "ScriptableObjects/Obstacles")]
public class ScriptableObstacle : ScriptableObject
{
    [SerializeField]
    private string objectName = "ScriptableObstacle";
    public string Name { get { return objectName; } }

    public float scale = 1f;
    public float requiredScale = 1f;

    public bool collectable = true;

}
