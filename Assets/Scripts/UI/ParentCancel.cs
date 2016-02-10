using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ParentCancel : JComponent {
	protected override void OnStart() {
		var trigger = transform.parent.GetComponent<EventTrigger>();
		if (trigger) {
			var myTrigger = gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Cancel;
			entry.callback.AddListener((eventData) => { trigger.OnCancel(eventData); });
			myTrigger.triggers.Add(entry);
		}
	}
}
