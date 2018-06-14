using System;


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
}


