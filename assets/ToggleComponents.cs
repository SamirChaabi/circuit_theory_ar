using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleComponents : MonoBehaviour {

	public GameObject battery;
	public GameObject lamp;
	public GameObject resistor;
	public GameObject onSwitch;
	public GameObject offSwitch;
	public GameObject cable1;
	public GameObject cable2;
	public GameObject cable3;
	public GameObject cable4;

	public Image batterySymbolImage;
	public Image cable1SymbolImage;

	public GameObject[,] componentPosition;

	void Start(){

		componentPosition = new GameObject[3,3];
		componentPosition [1,1] = battery;

	}

	public void moveSymbols(GameObject component, GameObject component2){

		if (component.transform.parent.name == "Battery" || component2.transform.parent.name == "Battery") {
			if(component.name == "Up"){
				if(!component.transform.parent.name.Equals("Battery")){
					changeImage(component.transform.parent.name, "Image1");
					componentPosition[0,0] = component;
				}
				else{
					changeImage(component2.transform.parent.name, "Image1");
					componentPosition[2,0] = component2;
				}
			}
			else if(component.name == "Down"){

			}
		}
	}

	public void changeImage(string imageToChangeTo, string imageToChange){
		Image [] images = gameObject.GetComponentsInChildren<Image>();

		foreach(Image img in images){
			if(img.name == imageToChangeTo){
				if(imageToChange != "Image5"){
					gameObject.transform.FindChild ("CustomizedCircuit").transform.FindChild (imageToChange).GetComponent<Image> ().overrideSprite = img.sprite;
				}
				else{
					gameObject.transform.FindChild ("CustomizedCircuit").transform.FindChild (imageToChange).GetComponent<Image> ().overrideSprite = img.sprite;
					gameObject.transform.FindChild ("CustomizedCircuit").transform.FindChild (imageToChange).GetComponent<Image> ().transform.eulerAngles = new Vector3(0,0,90);
				}
			}
		}
	}

	public void clearImages(){
		Image [] images = gameObject.transform.FindChild("CustomizedCircuit").GetComponentsInChildren<Image> ();

		foreach (Image img in images) {
			if (img.tag == "Icon") {
				gameObject.transform.FindChild ("CustomizedCircuit").transform.FindChild (img.name).GetComponent<Image> ().overrideSprite = null;
			}
		}
	}
		
	public void showComponent(string componentName) {

		if(componentName.Equals("Battery")){
			GameObject.FindGameObjectWithTag("BatterySymbol").GetComponent<CanvasGroup>().alpha = 1;
			//gameObject.transform.FindChild("Battery").GetComponent<CanvasGroup>().alpha = 1;
		}
		else if(componentName.Equals("Lamp")){
			GameObject.FindGameObjectWithTag("LampSymbol").GetComponent<CanvasGroup>().alpha = 1;
			//GameObject.Find("Lamp").GetComponent<CanvasGroup> ().alpha = 1;
		}
		else if(componentName.Equals("Resistor")){
			GameObject.FindGameObjectWithTag("ResistorSymbol").GetComponent<CanvasGroup>().alpha = 1;
			//GameObject.Find("Resistor").GetComponent<CanvasGroup> ().alpha = 1;
		}
		else if(componentName.Equals("OnSwitch")){
			GameObject.FindGameObjectWithTag("OnSwitchSymbol").GetComponent<CanvasGroup>().alpha = 1;
			//GameObject.Find("OnSwitch").GetComponent<CanvasGroup> ().alpha = 1;
		}
		else if(componentName.Equals("OffSwitch")){
			GameObject.FindGameObjectWithTag("OffSwitchSymbol").GetComponent<CanvasGroup>().alpha = 1;
			//GameObject.Find("OffSwitch").GetComponent<CanvasGroup> ().alpha = 1;
		}
		else if(componentName.Equals("Cable1")){
			GameObject.FindGameObjectWithTag("Cable1Symbol").GetComponent<CanvasGroup>().alpha = 1;
			//GameObject.Find("Cable1").GetComponent<CanvasGroup> ().alpha = 1;
		}
		else if(componentName.Equals("Cable2")){
			GameObject.FindGameObjectWithTag("Cable2Symbol").GetComponent<CanvasGroup>().alpha = 1;
			//GameObject.Find("Cable2").GetComponent<CanvasGroup> ().alpha = 1;
		}
		else if(componentName.Equals("Cable3")){
			GameObject.FindGameObjectWithTag("Cable3Symbol").GetComponent<CanvasGroup>().alpha = 1;
			//GameObject.Find("Cable3").GetComponent<CanvasGroup> ().alpha = 1;
		}
		else if(componentName.Equals("Cable4")){
			GameObject.FindGameObjectWithTag("Cable4Symbol").GetComponent<CanvasGroup>().alpha = 1;
			//GameObject.Find("Cable4").GetComponent<CanvasGroup> ().alpha = 1;
		}
	}

	public void hideComponent(string componentName) {

		if(componentName.Equals("Battery")){
			GameObject.FindGameObjectWithTag("BatterySymbol").GetComponent<CanvasGroup>().alpha = 0;
		}
		else if(componentName.Equals("Lamp")){
			GameObject.FindGameObjectWithTag("LampSymbol").GetComponent<CanvasGroup>().alpha = 0;
		}
		else if(componentName.Equals("Resistor")){
			GameObject.FindGameObjectWithTag("ResistorSymbol").GetComponent<CanvasGroup>().alpha = 0;
		}
		else if(componentName.Equals("OnSwitch")){
			GameObject.FindGameObjectWithTag("OnSwitchSymbol").GetComponent<CanvasGroup>().alpha = 0;
		}
		else if(componentName.Equals("OffSwitch")){
			GameObject.FindGameObjectWithTag("OffSwitchSymbol").GetComponent<CanvasGroup>().alpha = 0;
		}
		else if(componentName.Equals("Cable1")){
			GameObject.FindGameObjectWithTag("Cable1Symbol").GetComponent<CanvasGroup>().alpha = 0;
		}
		else if(componentName.Equals("Cable2")){
			GameObject.FindGameObjectWithTag("Cable2Symbol").GetComponent<CanvasGroup>().alpha = 0;
		}
		else if(componentName.Equals("Cable3")){
			GameObject.FindGameObjectWithTag("Cable3Symbol").GetComponent<CanvasGroup>().alpha = 0;;
		}
		else if(componentName.Equals("Cable4")){
			GameObject.FindGameObjectWithTag("Cable4Symbol").GetComponent<CanvasGroup>().alpha = 0;
		}
	}
}