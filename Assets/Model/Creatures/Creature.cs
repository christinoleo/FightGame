using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creature {
	public List<BodyPart> bodyParts = new List<BodyPart>();
	public string name;
	public short luck;
	public float speed;
	public float initiative;
	public bool alive = true;
	public float hp;

	public float initiative_tick;

	public Creature (string name, short luck, float speed, float initiative, float hp)
	{
		this.name = name;
		this.bodyParts = bodyParts;
		this.luck = luck;
		this.speed = speed;
		this.initiative = initiative;
		this.hp = hp;
		this.initiative_tick = Time.time + initiative;
	}
	
	public void Update(Creature other){
		if (Time.time > initiative_tick) {
			Debug.Log (name + " is going to attack");
			initiative_tick += initiative;
			bodyParts [0].Attack (other.bodyParts [0]);
		}
	}

	public void Hit (float value)
	{
		hp -= value;
		if (hp <= 0) {
			alive = false;
			Debug.Log (name + " is dead");
		}
	}
}
