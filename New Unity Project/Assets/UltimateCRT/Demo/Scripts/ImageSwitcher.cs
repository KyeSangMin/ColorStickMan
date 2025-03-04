﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(SpriteRenderer))]
public class ImageSwitcher : MonoBehaviour {

	//public static ImageSwitcher instance;

	private int textureIndex;
	private Texture2D[] textures;

	private BaseCRTEffect.Preset[] presets;
	private int presetIndex;

	private SpriteRenderer spriteRenderer;
	private BaseCRTEffect effect;
	private Text text;

	private float textVisibleDuration = 0.0f;
	private int y;

	public GameObject[] SpriteTag;
	public GameObject[] OnLeverTag;
	public GameObject[] TileMapTag;
	public GameObject RedObjects;
	public GameObject BlueObjects;
	public GameObject GreenObjects;
	public GameObject NoneObjects;


	private SpriteRenderer render;
	private Tilemap map;


	private bool ColorRed;
	private bool ColorGreen;
	private bool ColorBlue;
	private bool ColorWhite;


	void Awake() {

		

		BaseCRTEffect.Preset[] allPresets = (BaseCRTEffect.Preset[]) System.Enum.GetValues(typeof(BaseCRTEffect.Preset));
		presets = new BaseCRTEffect.Preset[allPresets.Length];

		var size = 0;
		foreach(var p in allPresets)
			if(p != BaseCRTEffect.Preset.ColorTV)
				presets[size++] = p;
		//프리셋 배열에 저장
		textures = Resources.LoadAll<Texture2D>("");

		spriteRenderer 	= GetComponent<SpriteRenderer>();
		//render = GameObject.Find("Sprite").GetComponent<SpriteRenderer>();
		SpriteTag = GameObject.FindGameObjectsWithTag("sprite");
		OnLeverTag = GameObject.FindGameObjectsWithTag("OnLever");
		//render = SpriteTag[SpriteTag.Length].GetComponent<SpriteRenderer>();
		TileMapTag = GameObject.FindGameObjectsWithTag("tilemap");
		RedObjects = GameObject.FindGameObjectWithTag("RedObjects");
		GreenObjects = GameObject.FindGameObjectWithTag("GreenObjects");
		BlueObjects = GameObject.FindGameObjectWithTag("BlueObjects");
		NoneObjects = GameObject.FindGameObjectWithTag("sprite");
		NoneObjects.SetActive(false);
		ColorRed = false;
		ColorBlue = false;
		ColorGreen = false;
		ColorWhite = true;




		foreach (Camera camera in Camera.allCameras) {
			effect = camera.GetComponentInChildren<BaseCRTEffect>();

			if(effect != null)
				break;
		}
			/*
		var texts = FindObjectsOfType<Text>();
		foreach(var t in texts) {
			if(t.name != "Preset Name")
				continue;

			text = t;
			break;
		}
			*/
		//text.enabled = false;

		//textureIndex = System.Array.IndexOf(textures, spriteRenderer.sprite.texture);
		presetIndex = System.Array.IndexOf(presets, effect.predefinedModel);

		if(presetIndex < 0) {
			effect.predefinedModel = presets[0];
			presetIndex = 0;
		}

		//ShowPresetName(presets[presetIndex]);
	
	}
	
	void Update () {
		if(textVisibleDuration != 0.0f) {
			textVisibleDuration -= Time.deltaTime;

			if(textVisibleDuration <= 0.0f) {
				textVisibleDuration = 0.0f;
				//text.enabled = false;
			}
		}

		//var x = Input.GetKeyDown("a") ? -1 : Input.GetKeyDown("d") ? 1 : 0;
		//var y = Input.GetKeyDown("w") ? -1 : Input.GetKeyDown("s") ? 1 : 0;

		if (Input.GetKeyDown("a")) // 빨강
		{
			SetColorRed();
		}
		else if (Input.GetKeyDown("s")) // 초록 
		{
			SetColorGreen();
		}
		else if (Input.GetKeyDown("d")) // 파랑 
		{
			SetColorBlue();
		}
		else if (Input.GetKeyDown("q")) // 초기화 
		{
			SetColorReset();
		}

		var onOff = Input.GetKeyDown("p");

		

		//var texInd = (textureIndex + x + textures.Length) % textures.Length;
		//var preInd = (presetIndex + y + presets.Length) % presets.Length;
		
		var preInd = y;


/*
		if(textureIndex != texInd) {
			spriteRenderer.sprite = Sprite.Create(textures[texInd], new Rect(0.0f, 0.0f, textures[texInd].width, textures[texInd].height), new Vector2(0.5f, 0.5f));
			textureIndex = texInd;
		}
*/

		if(presetIndex != preInd) {
			presetIndex = preInd;
			effect.predefinedModel = presets[preInd];


			//ShowPresetName(presets[preInd]);
		}

		if(onOff) {
			effect.enabled = ! effect.enabled;

			ShowOnOff(effect.enabled);
		}
	}

	/*
	public void OnUpClicked() {
		var preInd = (presetIndex - 1 + presets.Length) % presets.Length;

		presetIndex = preInd;
		effect.predefinedModel = presets[preInd];

		//ShowPresetName(presets[preInd]);
	}

	public void OnDownClicked() {
		var preInd = (presetIndex + 1 + presets.Length) % presets.Length;

		presetIndex = preInd;
		effect.predefinedModel = presets[preInd];

		//ShowPresetName(presets[preInd]);
	}

	
	 public void OnLeftClicked() {
		var texInd = (textureIndex - 1 + textures.Length) % textures.Length;

		spriteRenderer.sprite = Sprite.Create(textures[texInd], new Rect(0.0f, 0.0f, textures[texInd].width, textures[texInd].height), new Vector2(0.5f, 0.5f));
		textureIndex = texInd;
	}
	
	public void OnRightClicked() {
		var texInd = (textureIndex + 1 + textures.Length) % textures.Length;

		spriteRenderer.sprite = Sprite.Create(textures[texInd], new Rect(0.0f, 0.0f, textures[texInd].width, textures[texInd].height), new Vector2(0.5f, 0.5f));
		textureIndex = texInd;
	}

	public void OnCenterClicked() {
		effect.enabled = ! effect.enabled;

		ShowOnOff(effect.enabled);
	}

	/*
	private void ShowPresetName(BaseCRTEffect.Preset preset) {
		text.text = preset.ToString();
		text.enabled = true;

		textVisibleDuration = 2.0f;
	}
	*/
	private void ShowOnOff(bool on) {
		//text.text = on ? "[postprocess: on]" : "[postprocess: off]";
		//text.enabled = true;

		textVisibleDuration = 2.0f;
	}


	public void SetColorRed()
    {
		y = 1;
		//render.color = new Color(255.0f / 255.0f, 5.0f / 255.0f, 0.0f / 255.0f);
		for (int i = 0; i < SpriteTag.Length; i++)
		{
			SpriteTag[i].GetComponent<SpriteRenderer>().color = new Color(255.0f / 255.0f, 5.0f / 255.0f, 0.0f / 255.0f);
		}
		for (int i = 0; i < TileMapTag.Length; i++)
		{
			TileMapTag[i].GetComponent<Tilemap>().color = new Color(255.0f / 255.0f, 5.0f / 255.0f, 0.0f / 255.0f);
		}
		for (int i = 0; i < OnLeverTag.Length; i++)
		{
			OnLeverTag[i].GetComponent<SpriteRenderer>().color = new Color(255.0f / 255.0f, 5.0f / 255.0f, 0.0f / 255.0f);
		}
		RedObjects.SetActive(false);
		ColorRed = true;
		ColorBlue = false;
		ColorGreen = false;
		ColorWhite = false;
		//NoneObjects.SetActive(true);

	}

	public void SetColorGreen()
	{
		y = 2;
		//render.color = new Color(80.0f/ 255.0f, 255.0f / 255.0f, 0.0f/ 255.0f);
		for (int i = 0; i < SpriteTag.Length; i++)
		{
			SpriteTag[i].GetComponent<SpriteRenderer>().color = new Color(80.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f);
		}
		for (int i = 0; i < TileMapTag.Length; i++)
		{
			TileMapTag[i].GetComponent<Tilemap>().color = new Color(80.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f);
		}
		for (int i = 0; i < OnLeverTag.Length; i++)
		{
			OnLeverTag[i].GetComponent<SpriteRenderer>().color = new Color(80.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f);
		}
		GreenObjects.SetActive(false);
		ColorRed = false;
		ColorBlue = false;
		ColorGreen = true;
		ColorWhite = false;
		//NoneObjects.SetActive(true);
	}
	public void SetColorBlue()
	{
		y = 3;
		//render.color = new Color(0.0f / 255.0f, 155.0f / 255.0f, 255.0f / 255.0f);
		for (int i = 0; i < SpriteTag.Length; i++)
		{
			SpriteTag[i].GetComponent<SpriteRenderer>().color = new Color(0.0f / 255.0f, 155.0f / 255.0f, 255.0f / 255.0f);
		}
		for (int i = 0; i < TileMapTag.Length; i++)
		{
			TileMapTag[i].GetComponent<Tilemap>().color = new Color(0.0f / 255.0f, 155.0f / 255.0f, 255.0f / 255.0f);
		}
		for (int i = 0; i < OnLeverTag.Length; i++)
		{
			OnLeverTag[i].GetComponent<SpriteRenderer>().color = new Color(0.0f / 255.0f, 155.0f / 255.0f, 255.0f / 255.0f);
		}
		BlueObjects.SetActive(false);
		ColorRed = false;
		ColorBlue = true;
		ColorGreen = false;
		ColorWhite = false;
		//NoneObjects.SetActive(true);
	}

	public void SetColorReset()
	{
		y = 0;
		//render.color = new Color(255.0f, 255.0f, 255.0f);
		for (int i = 0; i < SpriteTag.Length; i++)
		{
			SpriteTag[i].GetComponent<SpriteRenderer>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
		}
		for (int i = 0; i < TileMapTag.Length; i++)
		{
			TileMapTag[i].GetComponent<Tilemap>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
		}
		for (int i = 0; i < OnLeverTag.Length; i++)
		{
			OnLeverTag[i].GetComponent<SpriteRenderer>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
		}
		RedObjects.SetActive(true);
		BlueObjects.SetActive(true);
		GreenObjects.SetActive(true);
		ColorRed = false;
		ColorBlue = false;
		ColorGreen = false;
		ColorWhite = true;
		//NoneObjects.SetActive(false);
	}


	public int GetColorSet()
    {
		if (ColorRed == true)
		{
			return 1;
		}

		else if (ColorGreen == true)
		{
			return 2;
		}
		else if (ColorBlue == true)
		{
			return 3;
		}
		else if (ColorWhite == true)
		{
			return 4;
		}
		else
			return 0;
	}

}
