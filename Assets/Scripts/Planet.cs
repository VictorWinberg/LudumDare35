﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class Planet : MonoBehaviour {

	public GameObject core;
	GameObject tree, holder;
	Mesh mesh;
	
	// Use this for initialization
	void Start () {
		SphereCollider[] colliders = core.GetComponents<SphereCollider> ();
		SphereCollider collider = null;
		foreach (SphereCollider col in colliders) {
			if (!col.isTrigger)
				collider = col;
		}
		tree = Resources.Load ("Gran") as GameObject;
		holder = new GameObject ("Generated");
		holder.transform.parent = this.transform;
		holder.transform.localScale = Vector3.one;
		holder.transform.localPosition = Vector3.zero;

		mesh = core.GetComponent<MeshFilter> ().mesh;
	}

	public void SpawnTrees(int number, float speed) {
		StartCoroutine (CoSpawnTrees (number, speed));
	}

	IEnumerator CoSpawnTrees(int number, float speed) {
		while(number > 0) {
			yield return new WaitForSeconds(speed);
			Vector3 spawnPos = mesh.vertices [Random.Range(0, mesh.vertices.Length)] * core.transform.localScale.x * transform.localScale.x + core.transform.position;
			GameObject clone = Instantiate (tree, spawnPos, Quaternion.FromToRotation(Vector3.up, spawnPos - transform.position)) as GameObject;
			clone.transform.parent = holder.transform;
			number--;
			yield return null;
		}
	}

	public void orbit() {
		float modifier = Vector3.Distance(Vector3.zero, transform.position) / (core.transform.localScale.x + transform.localScale.x) + 1;
		transform.RotateAround (Vector3.zero, Vector3.up, Time.deltaTime * modifier);
	}
	/*
	IEnumerator orbit(Vector3 pos) {
		transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
	}*/
}
