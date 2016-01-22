using UnityEngine;
using System.Collections;

public class Type {

	private int type;

	/// <summary>
	/// Constructor for the type object. 1 is wind, 2 is earth and 3 is water. 0 is no type.
	/// </summary>
	/// <param name="i">The index.</param>
	public Type(int i){
		if (i >= 0 && i <= 3) {
			this.type = i;
		} else {
			this.type = 0;
		}
	}

	/// <summary>
	/// Converts the type to string and returns it.
	/// </summary>
	/// <returns>The string.</returns>
	public string toString(){
		if (type == 0) {
			return "No type";
		}else if (type == 1) {
			return "Wind";
		} else if (type == 2) {
			return "Earth";
		} else if (type == 3) {
			return "Water";
		} else {
			return "";
		}
	}

	/// <summary>
	/// Gets the type.
	/// </summary>
	/// <returns>The type.</returns>
	public int getType(){
		return type;
	}

	/// <summary>
	/// Sets the type.
	/// </summary>
	/// <param name="i">The index.</param>
	public void setType(int i){
		if (i >= 0 && i <= 3) {
			this.type = i;
		} else {
			this.type = 1;
		}
	}

	/// <summary>
	/// Returns the damage multiplier to Type t.
	/// </summary>
	/// <returns>The multiplier to type.</returns>
	/// <param name="t">T.</param>
	public double damageMultiplierToType(Type t){
		int i = t.getType ();
		if (type == 1) {
			if (i == 2) {
				return 0.5;
			} else if (i == 3) {
				return 1.5;
			} else {
				return 1d;
			}
		} else if (type == 2) {
			if (i == 3) {
				return 0.5;
			} else if (i == 1) {
				return 1.5;
			} else {
				return 1d;
			}
		} else if (type == 3) {
			if (i == 1){
				return 0.5;
			}else if(i == 2){
				return 1.5;
			} else{
				return 1d;
			}
		}else{
			return 1d;
		}
	}
}