using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour {

	public int xSize, ySize;

	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float waitTime;

	private Vector3[] vertices;
	private Mesh mesh;


	private void Awake () {
		StartCoroutine(Generate());
	}


	private void OnDrawGizmos () {
		Gizmos.color = Color.black;
		if (vertices != null) {
			for (int i = 0; i < vertices.Length; i++) {
				Gizmos.DrawSphere(vertices[i], 0.1f);
			}
		}
	}


	private IEnumerator Generate () {
		WaitForSeconds wait = new WaitForSeconds(waitTime);

		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

		vertices = new Vector3[(xSize + 1) * (ySize + 1)];

		for (int i = 0, y = 0; y <= ySize; y++) {
			for (int x = 0; x <= xSize; x++, i++) {
				vertices[i] = new Vector3(x, y);
				yield return wait;
			}
		}

		mesh.vertices = vertices;

		int[] triangles = new int[6];
		triangles[0] = 0;
		triangles[3] = triangles[2] = 1;
		triangles[4] = triangles[1] = xSize + 1;
		triangles[5] = xSize + 2;
		mesh.triangles = triangles;
	}
}