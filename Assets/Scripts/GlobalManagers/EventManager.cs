using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Gère le système d'events
 * Est un singleton donc est accessible depuis tout le projet
 * Garde une liste de listener, des fonction qui doivent être appelées quand un event est levé
 */ 

public class EventManager
{
	//déclaration de la signature des fonction delegates acceptées comme listener
	//specifique
	public delegate void EventDelegate<T> (T e) where T : GameEvent;
	//général (super type)
	private delegate void EventDelegate (GameEvent e);

	//Dictionnaire de stockage des listener
	//couple (type d'event, listener)
	private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
	private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();


	//instencie le singleton
	static EventManager instanceInternal = null;
	public static EventManager instance
	{
		get
		{
			if (instanceInternal == null)
			{
				instanceInternal = new EventManager();
			}

			return instanceInternal;
		}
	}


	//Ajoute un listener au dictionnaire 
	//Si un listener existe déja pour le type d'event concerné, combine le nouveau listener avec le précédent
	public void AddListener<T> (EventDelegate<T> del) where T : GameEvent
	{	
		// Early-out if we've already registered this delegate
		if (delegateLookup.ContainsKey(del))
			return;

		// Create a new non-generic delegate which calls our generic one.
		// This is the delegate we actually invoke.
		EventDelegate internalDelegate = (e) => del((T)e);
		delegateLookup[del] = internalDelegate;

		EventDelegate tempDel;
		if (delegates.TryGetValue(typeof(T), out tempDel))
		{
			delegates[typeof(T)] = tempDel += internalDelegate; 
		}
		else
		{
			delegates[typeof(T)] = internalDelegate;
		}
	}

	//Enlève un listener au 
	public void RemoveListener<T> (EventDelegate<T> del) where T : GameEvent
	{
		EventDelegate internalDelegate;
		if (delegateLookup.TryGetValue(del, out internalDelegate))
		{
			EventDelegate tempDel;
			if (delegates.TryGetValue(typeof(T), out tempDel))
			{
				tempDel -= internalDelegate;
				if (tempDel == null)
				{
					delegates.Remove(typeof(T));
				}
				else
				{
					delegates[typeof(T)] = tempDel;
				}
			}

			delegateLookup.Remove(del);
		}
	}

	//Lève un évent
	//execute les listener qui lui sont associé
	public void Raise (GameEvent e)
	{
		EventDelegate del;
		if (delegates.TryGetValue(e.GetType(), out del))
		{
			del.Invoke(e);
		}
	}
}