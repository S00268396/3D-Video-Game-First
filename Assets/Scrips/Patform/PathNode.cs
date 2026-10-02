using Unity.VisualScripting;

using UnityEngine;

public class PathNode : MonoBehaviour
{
    //The next node in the path that the platform will move towards after reaching this node.
    [Header("Path Node Settings:")]   
    [SerializeField]
    private PathNode NextNode;

    //Method to get the current node
    public PathNode GetNextNode()
    {
        return NextNode;
    }

    private void OnDrawGizmos()
    {
        //Draw a cyan wire sphere at the position of the node to visualize it in the editor.
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        //If there is a next node assigned, draw a yellow line from this node to the next node to visualize the path in the editor.
        if (NextNode != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, NextNode.transform.position);
        }
    }
}
