using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	public Image tutorialImage;
	public GameObject nextImageButton;
	public GameObject previousImageButton;


	public List<Sprite> tutorialSprites;

	private int currentTutorialSpritesIndex;

	public void OnBeforeOpen()
	{
		currentTutorialSpritesIndex = 0;
		previousImageButton.SetActive(false);
		SetImage();
	}

	public void NextImage()
	{
		currentTutorialSpritesIndex++;
		nextImageButton.SetActive(currentTutorialSpritesIndex + 1 < tutorialSprites.Count);
		previousImageButton.SetActive(true);

		SetImage();

	}

	public void PreviousImage()
	{
		currentTutorialSpritesIndex--;
		previousImageButton.SetActive(currentTutorialSpritesIndex > 0);
		nextImageButton.SetActive(true);

		SetImage();
	}

	private void SetImage()
	{
		tutorialImage.sprite = tutorialSprites[currentTutorialSpritesIndex];
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
