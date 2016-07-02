using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	public Creature leo;
	public Creature enemy;

	// Use this for initialization
	void Start () {
		leo = new Creature("Leo", 5,3,5,100);
		leo.bodyParts.Add(new BodyPart(leo, "Arm", 50,10,3,3,3,0,0));
		enemy = new Creature("Enemy", 1,2,3,150);
		enemy.bodyParts.Add(new BodyPart(enemy, "foot", 80,15,5,2,5,0,0));
	}
	
	// Update is called once per frame
	void Update () {
		leo.Update (enemy);
		enemy.Update (leo);
	}
}
