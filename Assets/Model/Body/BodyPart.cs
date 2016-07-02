using UnityEngine;
using System.Collections;

public class BodyPart {
	public static float MAX_HP;
	public static float MAX_STRENGTH;
	public enum AttackCompletion
	{
		miss,
		scratch,
		bruise,
		hit,
		critical,
		tired,
		no_damage,
		dead
	};

	public Creature creature;
	public bool alive = true;
	public string name;
	public float stamina;
	public float strength;
	public float dexterity;
	public float armor;
	public float weapon;
	public float hp;
	public float speed;

	public BodyPart (Creature creature, string name, float hp, float stamina, float strength, float dexterity, float speed, float armor, float weapon)
	{
		this.creature = creature;
		this.name = name;
		this.hp = hp;
		this.stamina = stamina;
		this.strength = strength;
		this.dexterity = dexterity;
		this.speed = speed;
		this.armor = armor;
		this.weapon = weapon;
	}

	public void Hit(float value) { 
		this.hp -= value;
		creature.Hit (value);
		Debug.Log (creature.name + "'s " + name + " was hit by "+ value + "|hp: " + (this.hp) + " left");
		if (this.hp <= 0) {
			alive = false;
			Debug.Log (creature.name + "'s " + name + " is incapacitaded");
		}

	}

	public AttackCompletion Attack(BodyPart other, float distance=1){
		Debug.Log (creature.name + "'s " + name + " is going to attack " + other.creature.name + "'s " + other.name);

		if (stamina - strength - armor / 2 < 0) {
			Debug.Log (creature.name + "'s " + name + " is tired");
			return AttackCompletion.tired;
		} else if (!alive) {
			Debug.Log (creature.name + "'s " + name + " is incapacitaded");
			return AttackCompletion.dead;
		}

		int d20 = Random.Range(((int)(1 - other.creature.luck * Random.value)),((int)(20 + creature.luck * Random.value)));
		if (d20 + dexterity / distance - (other.speed + other.creature.speed) * distance < 0) {
			Debug.Log (creature.name + "'s " + name + " missed");
			return AttackCompletion.miss;
		} else if (d20 + strength + weapon - other.armor < 0) {
			Debug.Log (creature.name + "'s " + name + " did no damage");
			return AttackCompletion.no_damage;
		}
		else /*hit*/{
			stamina -= (strength + armor/2);
			hp -= strength / 5;
			other.hp -= (d20 + strength + weapon - other.armor);

			if(d20 < 20){
				other.Hit(d20 + strength + weapon - other.armor);
				return AttackCompletion.hit;
			}
			else{
				other.Hit(2*(d20 + strength + weapon - other.armor));
				return AttackCompletion.critical;
			}
		}
	}
}
