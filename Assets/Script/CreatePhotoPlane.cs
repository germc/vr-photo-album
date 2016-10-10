﻿using UnityEngine;
using System.Collections.Generic;

public class CreatePhotoPlane : MonoBehaviour {

	// Use this for initialization
	public float planescale;
	private static System.Random rng = new System.Random();
	void Start () {
		//var slash = System.IO.Path.DirectorySeparatorChar;
		//byte[] image = System.IO.File.ReadAllBytes(System.IO.Directory.GetCurrentDirectory() + string.Format(@"{0}Assets{1}Starsinthesky.jpg",slash,slash));
		//Texture2D t2 = new Texture2D(3877, 2842);
		//t2.LoadImage(image);
		var ds = this.GetComponentInParent<DataScan>();
		var albums = ds.rModel.albumList;
		var curAlbum = albums[1];
		var photoList = ShufflePhotos(curAlbum.photoList);
		List<GameObject> photos = new List<GameObject>();
		foreach (Photo p in photoList)
		{
			GameObject photo = GameObject.CreatePrimitive(PrimitiveType.Quad);
			photo.transform.parent = this.transform;
			photo.transform.localScale = new Vector3(p.width / p.height, 1, p.height / p.width);
			float px = (float)rng.NextDouble() * planescale*planescale - planescale*planescale/2;
			float pz = (float)rng.NextDouble() * planescale*planescale - planescale*planescale/2;
			photo.transform.localPosition = new Vector3(px, 0, pz);
			photo.transform.eulerAngles = new Vector3(-90, 0, 0);

			Material mat = new Material(Shader.Find("Unlit/Texture"));
			mat.SetTexture("_MainTex", p.texture);
			Renderer rend = photo.GetComponent<Renderer>();
			rend.material = mat;
			photos.Add(photo);
		}
		
	}
	
	public Photo[] ShufflePhotos(Photo[] list)
	{
		int n = list.Length;
		while(n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			Photo t = list[k];
			list[k] = list[n];
			list[n] = t;
		}
		return list;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
