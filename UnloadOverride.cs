using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UnloadOverride : PartModule
{
	[KSPField(isPersistant = true)]
	public bool overrideControl = false;

	[KSPField]
	public float overrideDistance = 300000;

	[KSPField(guiActive = true, guiName = "Unload override is ")]
	public string overrideState = "Disabled";

	public override void OnStart (StartState state)
	{
		print ("default landed unload distance : " + vessel.distanceLandedPackThreshold);
		print ("default unload distance : " + vessel.distancePackThreshold);
	}

	[KSPEvent(guiActive = true, guiName = "Toggle Override", guiActiveUnfocused = true, unfocusedRange = 0)]
	public void toggleUnloadOverride ()
	{
		overrideControl = !overrideControl;
	}

	[KSPAction("Toggle")]
	public void ToggleAction(KSPActionParam param)
	{
		overrideControl = !overrideControl;
	}

	public void FixedUpdate ()
	{
		if (HighLogic.LoadedSceneIsFlight == true) {
			if (overrideControl == true) {
				overrideState = "Enabled";
				this.vessel.distancePackThreshold = overrideDistance;
				this.vessel.distanceLandedPackThreshold = overrideDistance;
				Vessel.unloadDistance = overrideDistance - 250;
				Vessel.loadDistance = overrideDistance;
			} else {
				overrideState = "Disabled";
				this.vessel.distancePackThreshold = 2500;
				this.vessel.distanceLandedPackThreshold = 350;
				Vessel.unloadDistance = 2250;
				Vessel.loadDistance = 2500;
			}
		}
	}
}