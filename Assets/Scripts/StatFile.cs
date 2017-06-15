using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StatFile {

 	private List<Stat> statList;
	private bool valid = true; //Is the stat file validly defined?
	public string fileName;

	
	///Abilities are just lots of Stats put together. Max 3 abilities outlined per StatFile
	private Stat[,] abilityList = new Stat[3,2]; 

	///Creates a new StatFile instance based on just the name of the file (no path included). Example: "FireTower"
	public StatFile (string file) {
		fileName = file;
		if(!File.Exists("../Stats/" + fileName + ".stats")) {
			 Debug.LogWarning("Could not find Stat file \'" + fileName + "\'.");
			 valid = false;
		}
	}

	public string GetStat (string identifier) {
		 // Open the file to read from.
		 if(valid) {
			using (StreamReader sr = File.OpenText("../Stats/" + fileName + ".stats")) {
				string s = "";
				while (!s.Contains(identifier) || s.Contains("#")) {
					s = sr.ReadLine();
					if(s == null) {
						Debug.Log("Identifier " + identifier + " could not be found in stat file " + fileName);
						return "";
					}
				}

				//If the while loop finished without returning, then the identifier has been found.
				try {
					return s.Substring(s.IndexOf(':'), s.Length).Trim();
				} catch {
					valid = false;
					Debug.LogWarning("Error parsing " + fileName);
				}
			}
		 }

		 Debug.LogWarning("Stat file " + fileName + " is missing or invalid!");
		 return "";
	}

	///Overload of GetStat that returns an int instead of a string.
	public int GetStatInt (string identifier) {
		try {
			return Convert.ToInt32(GetStat(identifier));
		} catch {
			Debug.LogWarning("Identifier " + identifier + " in file " + fileName + " could not be parsed as an int.");
			return 0;
		}
	}
}

class Stat {

	public string identifier;
	public string value;

	///Constructor for standard Stats (one Identifier and one Value)
	public Stat (string id, string val) {
		identifier = id;
		value = val;
	}
}