﻿using System;


public class Skill
{
	public int id { get; private set;}
	public string name { get; private set; }

	public Skill ()
	{
		id = 0;
		name = "";
	}

	public Skill ( int _id, string _name){
		id = _id;
		name = _name;
	}

	public override bool Equals(Object obj)
	{
		Skill skill = obj as Skill; 
		if (skill == null)
			return false;
		else
			return id.Equals(skill.id) && name.Equals(skill.name);
	}

	public override int GetHashCode()
	{
		return this.id.GetHashCode(); 
	}
}


