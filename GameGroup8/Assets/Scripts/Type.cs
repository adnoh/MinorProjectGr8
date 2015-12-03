using UnityEngine;
using System.Collections;

public class Type {

	private int type;

	public Type(int i){
		if (i > 0 && i <= 3) {
			this.type = i;
		} else {
			this.type = 1;
		}
	}

	public string toString(){
		if (type == 1) {
			return "Wind";
		} else if (type == 2) {
			return "Earth";
		} else if (type == 3) {
			return "Water";
		} else {
			return "";
		}
	}

	public void nextType(){
		if(type < 3){
			type ++;
		}
		else if(type == 3){
			type = 1;
		}
	}

	public int getType(){
		return type;
	}

	public void setType(int i){
		if (i >= 0 && i <= 3) {
			this.type = i;
		} else {
			this.type = 1;
		}
	}

	public double damageMultiplierToType(Type t){
		int i = t.getType ();
		if (type == 1) {
			if (i == 2) {
				return 1.5;
			} else if (i == 3) {
				return 0.5;
			} else {
				return 1d;
			}
		} else if (type == 2) {
			if (i == 3) {
				return 1.5;
			} else if (i == 1) {
				return 0.5;
			} else {
				return 1d;
			}
		} else if (type == 3) {
			if (i == 1){
				return 1.5;
			}
			else if(i == 2){
				return 0.5;
			} else{
				return 1d;
			}
		}
		else{
			return 1d;
		}
	}




}
