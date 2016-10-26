using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class EntryPage : MonoBehaviour {

	public GameObject battery;
	public GameObject lamp;
	public GameObject resistor;
	public GameObject onSwitch;
	public GameObject offSwitch;
	public GameObject cableOne;
	public GameObject cableTwo;
	public GameObject cableThree;
	public GameObject cableFour;

	public GameObject markers;

	public GameObject HUDCanvas;

	public Components[] componentsData;

	public GameObject[][] arrayOfComponents;
	public arrayOfOnlyChildComponens[] arrayOfOnlyChildComponents;

	public ToggleComponents toggleComp;

	public int imageCount;

	private string lampImage;

	public class arrayOfOnlyChildComponens{
		public GameObject component;
		public bool connected;
	}

	public class Components{
		public GameObject component;
		public bool detected;
		public bool connected;

	}

	// Use this for initialization
	void Start () {
		componentsData = new Components[9];
		componentsData[0] = new Components{component = battery};
		componentsData[1] = new Components{component = lamp};
		componentsData[2] = new Components{component = resistor};
		componentsData[3] = new Components{component = onSwitch};
		componentsData[4] = new Components{component = offSwitch};
		componentsData[5] = new Components{component = cableOne};
		componentsData[6] = new Components{component = cableTwo};
		componentsData[7] = new Components{component = cableThree};
		componentsData[8] = new Components{component = cableFour};

		toggleComp = HUDCanvas.transform.FindChild ("RightPane").GetComponent<ToggleComponents> ();

		arrayOfComponents = markers.transform.Cast<Transform> ().Select (a => a.transform.Cast<Transform> ().Where (c => c.gameObject.tag == "Left" || c.gameObject.tag == "Right").Select (c => c.gameObject).ToArray()).ToArray ();
		arrayOfOnlyChildComponents = new arrayOfOnlyChildComponens[36];

		int count = 0;
		for (int i = 0; i < arrayOfComponents.Length; i++) {
			for(int n = 0; n < arrayOfComponents[0].Length; n++){
				arrayOfOnlyChildComponents[count] = new arrayOfOnlyChildComponens{component = arrayOfComponents[i][n], connected = false};
				count++;
			}
		}
	}


	void updateDetectedState(){

		if (HUDCanvas.transform.FindChild ("RightPane").transform.FindChild ("Battery").GetComponent<CanvasGroup> ().alpha == 0) {
			componentsData[0].detected = false;
		} else {
			componentsData[0].detected = true;
		}
		
		if (HUDCanvas.transform.FindChild ("RightPane").transform.FindChild("Lamp").GetComponent<CanvasGroup> ().alpha == 0) {
			componentsData[1].detected = false;
		} else {
			componentsData[1].detected = true;
		}
		
		if (HUDCanvas.transform.FindChild ("RightPane").transform.FindChild("Resistor").GetComponent<CanvasGroup> ().alpha == 0) {
			componentsData[2].detected = false;
		} else {
			componentsData[2].detected = true;
		}
		
		if (HUDCanvas.transform.FindChild ("RightPane").transform.FindChild("OnSwitch").GetComponent<CanvasGroup> ().alpha == 0) {
			componentsData[3].detected = false;
		} else {
			componentsData[3].detected = true;
		}
		
		if (HUDCanvas.transform.FindChild ("RightPane").transform.FindChild("OffSwitch").GetComponent<CanvasGroup> ().alpha == 0) {
			componentsData[4].detected = false;
		} else {
			componentsData[4].detected = true;
		}
		
		if (HUDCanvas.transform.FindChild ("RightPane").transform.FindChild("Cable1").GetComponent<CanvasGroup> ().alpha == 0) {
			componentsData[5].detected = false;
		} else {
			componentsData[5].detected = true;
		}
		
		if (HUDCanvas.transform.FindChild ("RightPane").transform.FindChild("Cable2").GetComponent<CanvasGroup> ().alpha == 0) {
			componentsData[6].detected = false;
		} else {
			componentsData[6].detected = true;
		}
		
		if (HUDCanvas.transform.FindChild ("RightPane").transform.FindChild("Cable3").GetComponent<CanvasGroup> ().alpha == 0) {
			componentsData[7].detected = false;
		} else {
			componentsData[7].detected = true;
		}
		
		if (HUDCanvas.transform.FindChild ("RightPane").transform.FindChild("Cable4").GetComponent<CanvasGroup> ().alpha == 0) {
			componentsData[8].detected = false;
		} else {
			componentsData[8].detected = true;
		}
	}


	// Update is called once per frame
	void Update () {
		//Debug.Log(HUDCanvas.transform.FindChild("RightPane").GetComponent<ToggleComponents>().componentPosition[1,1]);
		updateDetectedState ();

		doConnections ();

		//for(int n = 0; n < arrayOfOnlyChildComponents.Length; n++){
		//	for (int i = 1; i < arrayOfOnlyChildComponents.Length; i++) {

		//		bool detected = componentsData.Where(c => c.component.name == arrayOfOnlyChildComponents[n].transform.parent.name).Select(d => d.detected).FirstOrDefault();
		//		bool detected2 = componentsData.Where(c => c.component.name == arrayOfOnlyChildComponents[i].transform.parent.name).Select(d => d.detected).FirstOrDefault();


		//		if(!arrayOfOnlyChildComponents[n].transform.parent.name.Equals(arrayOfOnlyChildComponents[i].transform.parent.name) && 
		//		   detected && detected2 && Vector3.Distance(arrayOfOnlyChildComponents[n].transform.position, arrayOfOnlyChildComponents[i].transform.position) < 40){

		//			toggleComp.moveSymbols(arrayOfOnlyChildComponents[n], arrayOfOnlyChildComponents[i]);

		//		}
		//	}
		//}


	}

	void clearPrevValues(){
		for (int i = 0; i < arrayOfOnlyChildComponents.Length; i++) {
			arrayOfOnlyChildComponents[i].connected = false;
			if(i <= 8){
				componentsData[i].connected = false;
			}
		}
		toggleComp.clearImages ();
		lamp.transform.FindChild("Bulb").transform.FindChild("Top").GetComponent("Halo").GetType().GetProperty("enabled").SetValue(lamp.transform.FindChild("Bulb").transform.FindChild("Top").GetComponent("Halo"), false, null);
		HUDCanvas.transform.FindChild("RightPane").transform.FindChild("Arrows").GetComponent<CanvasGroup>().alpha = 0;
	}

	void doConnections(){
		clearPrevValues ();
		for (int i = 0; i < arrayOfOnlyChildComponents.Length; i++) {
			if(arrayOfOnlyChildComponents[i].component.name == "Up" && arrayOfOnlyChildComponents[i].component.transform.parent.name.Equals("Battery") ){
				imageCount = 1;
				iterateThroughChildrenUp(arrayOfOnlyChildComponents[i], i);
			}
			else if(arrayOfOnlyChildComponents[i].component.name == "Down" && arrayOfOnlyChildComponents[i].component.transform.parent.name.Equals("Battery")){
				imageCount = 6;
				iterateThroughChildrenDown(arrayOfOnlyChildComponents[i], i);
			}
		}
		for (int n = 0; n < componentsData.Length; n++) {
			if(componentsData[n].connected == true && componentsData[n].component.name.Equals("OffSwitch")){
				break;
			}
			if(componentsData[n].connected == false && !componentsData[n].component.name.Equals("OffSwitch")){
				break;
			}
			else if(n == 8){
				toggleComp.changeImage("LampOn" , lampImage);
				HUDCanvas.transform.FindChild("RightPane").transform.FindChild("Arrows").GetComponent<CanvasGroup>().alpha = 1;
				lamp.transform.FindChild("Bulb").transform.FindChild("Top").GetComponent("Halo").GetType().GetProperty("enabled").SetValue(lamp.transform.FindChild("Bulb").transform.FindChild("Top").GetComponent("Halo"), true, null);
			}
		}
	}

	void iterateThroughChildrenDown(arrayOfOnlyChildComponens orgComponent, int numberInArray){
		arrayOfOnlyChildComponens nextComponent = null;
		int nbr = 0;
		int placeOfCableInArray = 0;
		for(int n = 0; n < arrayOfOnlyChildComponents.Length; n++){
			
			bool detected = componentsData.Where(c => c.component.name == arrayOfOnlyChildComponents[n].component.transform.parent.name).Select(d => d.detected).FirstOrDefault();
			bool detected2 = componentsData.Where(c => c.component.name == orgComponent.component.transform.parent.name).Select(d => d.detected).FirstOrDefault();
			
			if(!arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals(orgComponent.component.transform.parent.name) && 
			   detected && detected2 && !orgComponent.connected && Vector3.Distance(arrayOfOnlyChildComponents[n].component.transform.position, orgComponent.component.transform.position) < 80){
				
				arrayOfOnlyChildComponents[numberInArray].connected = true;
				arrayOfOnlyChildComponents[n].connected = true;

				for(int k = 0; k< componentsData.Length; k++){
					if(arrayOfOnlyChildComponents[numberInArray].component.transform.parent.name.Equals(componentsData[k].component.name)){
						componentsData[k].connected = true;
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals(componentsData[k].component.name)){
						if(!arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1") &&
						   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2") &&
						   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3") &&
						   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
							componentsData[k].connected = true;
						}
						else{
							placeOfCableInArray = k;
						}
					}
				}

				if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Lamp")){
					lampImage = string.Concat("Image", imageCount);
				}

				if(!arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1") &&
				   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2") &&
				   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3") &&
				   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
					toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
				}

				
				if(string.Concat("Image", imageCount).Equals("Image1")){
					if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Up")){
							toggleComp.changeImage("Cable1", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Right")){
							toggleComp.changeImage("Cable1", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Left")){
							toggleComp.changeImage("Cable1", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Down")){
							toggleComp.changeImage("Cable1", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else{
						toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image2")){
					if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Left"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Right"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image3")){
					if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Up")){
							toggleComp.changeImage("Cable2", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Right")){
							toggleComp.changeImage("Cable2", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Left")){
							toggleComp.changeImage("Cable2", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Down")){
							toggleComp.changeImage("Cable2", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image5")){
					if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Left"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Right"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Up"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Down"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image7")){
					
					if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Left"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Right"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image6")){
					if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Up")){
							toggleComp.changeImage("Cable4", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Right")){
							toggleComp.changeImage("Cable4", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Left")){
							toggleComp.changeImage("Cable4", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Down")){
							toggleComp.changeImage("Cable4", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
					}
					else{
						toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image8")){
					if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Up")){
							toggleComp.changeImage("Cable3", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Right")){
							toggleComp.changeImage("Cable3", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Left")){
							toggleComp.changeImage("Cable3", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Down")){
							toggleComp.changeImage("Cable3", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else{
						toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
					}
				}
				
				if(imageCount >= 6 && imageCount < 8){
					imageCount++;
				}
				else if(imageCount == 8){
					imageCount = 5;
				}
				else if(imageCount == 5){
					imageCount = 3;
				}
				else if(imageCount >= 2 && imageCount <=3){
					imageCount--;
				}
				else if(imageCount == 1){
					nextComponent = null;
				}
				//nextComponent = arrayOfOnlyChildComponents[n];
				nbr = n;
				break;
			}
		}
		if(nextComponent !=null){
			iterateThroughChildrenDown(nextComponent, nbr);
		}
	}

	void iterateThroughChildrenUp(arrayOfOnlyChildComponens orgComponent, int numberInArray){
		arrayOfOnlyChildComponens nextComponent = null;
		int nbr = 0;
		int placeOfCableInArray = 0;
		for(int n = 0; n < arrayOfOnlyChildComponents.Length; n++){
			
			bool detected = componentsData.Where(c => c.component.name == arrayOfOnlyChildComponents[n].component.transform.parent.name).Select(d => d.detected).FirstOrDefault();
			bool detected2 = componentsData.Where(c => c.component.name == orgComponent.component.transform.parent.name).Select(d => d.detected).FirstOrDefault();
			
			if(!arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals(orgComponent.component.transform.parent.name) && 
			   detected && detected2 && !orgComponent.connected && Vector3.Distance(arrayOfOnlyChildComponents[n].component.transform.position, orgComponent.component.transform.position) < 80){
				
				arrayOfOnlyChildComponents[numberInArray].connected = true;
				arrayOfOnlyChildComponents[n].connected = true;

				for(int k = 0; k< componentsData.Length; k++){
					if(arrayOfOnlyChildComponents[numberInArray].component.transform.parent.name.Equals(componentsData[k].component.name)){
						componentsData[k].connected = true;
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals(componentsData[k].component.name)){
						if(!arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1") &&
						   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2") &&
						   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3") &&
						   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
							componentsData[k].connected = true;
						}
						else{
							placeOfCableInArray = k;
						}
					}
				}
				
				if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Lamp")){
					lampImage = string.Concat("Image", imageCount);
				}
				
				if(!arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1") &&
				   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2") &&
				   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3") &&
				   !arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
					toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
				}

				if(string.Concat("Image", imageCount).Equals("Image1")){
					if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Right")){
							toggleComp.changeImage("Cable1", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Down")){
							toggleComp.changeImage("Cable1", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Up")){
							toggleComp.changeImage("Cable1", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Left")){
							toggleComp.changeImage("Cable1", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else{
						toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image2")){
					
					if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Left"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Right"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image3")){
					if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Right")){
							toggleComp.changeImage("Cable2", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Down")){
							toggleComp.changeImage("Cable2", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Up")){
							toggleComp.changeImage("Cable2", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Left")){
							toggleComp.changeImage("Cable2", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else{
						toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image5")){
					if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Left"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Right"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Up"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Down"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image7")){
					
					if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Left"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
					}
					else if(arrayOfOnlyChildComponents[n].connected && arrayOfOnlyChildComponents[n].component.name == "Right"){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image6")){
					if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Right")){
							toggleComp.changeImage("Cable4", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Down")){
							toggleComp.changeImage("Cable4", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Up")){
							toggleComp.changeImage("Cable4", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Left")){
							toggleComp.changeImage("Cable4", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else{
						toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
					}
				}
				
				else if(string.Concat("Image", imageCount).Equals("Image8")){
					if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable4")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Up").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Right")){
							toggleComp.changeImage("Cable3", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable1")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Right").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Down")){
							toggleComp.changeImage("Cable2", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable3")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Left").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Up")){
							toggleComp.changeImage("Cable3", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else if(arrayOfOnlyChildComponents[n].component.transform.parent.name.Equals("Cable2")){
						nextComponent = arrayOfOnlyChildComponents.Where(c => c.component.transform.parent.name == arrayOfOnlyChildComponents[n].component.transform.parent.name && c.component.name == "Down").FirstOrDefault();
						if(arrayOfOnlyChildComponents[n].component.name.Equals("Left")){
							toggleComp.changeImage("Cable3", "Image" + imageCount);
							componentsData[placeOfCableInArray].connected = true;
						}
						else{
							toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
						}
					}
					else{
						toggleComp.changeImage(arrayOfOnlyChildComponents[n].component.transform.parent.name, "Image" + imageCount);
					}
				}
				
				imageCount++;
				
				if(imageCount == 4){
					imageCount = 5;
				}
				else if(imageCount == 6){
					imageCount = 8;
				}
				else if(imageCount == 9){
					imageCount = 7;
				}
				else if(imageCount == 8){
					imageCount = 6;
				}
				else if(imageCount == 7){
					nextComponent = null;
				}
				//nextComponent = arrayOfOnlyChildComponents[n];
				nbr = n;
				break;
			}
		}
		if(nextComponent !=null){
			iterateThroughChildrenUp(nextComponent, nbr);
		}
	}



}