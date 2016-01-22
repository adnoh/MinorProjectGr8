using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {

	public static int level = 1;
	public static int experience = 0;
	public static int pointsToUpgrade = 0;

	private static int experienceNeededToLevelUp = 5;
    
	public static int attackPoints = 0;
	public static int speedPoints = 0;
	public static int maxHealthPoints = 0;
	public static int maxEnergyPoints = 0;

	private static float attackMultiplier = 1f;
	private static float walkingSpeed = 5f;
	private static float maxWalkingSpeed = 5f;
	private static float runningSpeed = walkingSpeed * 2f;
	private static int maxHealth = 100;
	private static int health = 100;
	private static int maxEnergy = 500;
	private static int energy = 500;

	private static bool running = false;
	private static float speed = 5f;

	public static int maxFatique = 10000;
	public static int fatique = 10000;

	public GameObject upgradePanel;
	public Text pointsToAssignText;
	public Text attackPointsText;
	public Text speedPointsText;
	public Text healthPointsText;
	public Text energyPointsText;

	public static int amountOfHealthBeds;

	public static bool capped;

	/// <summary>
	/// Sets all the UI elements right.
	/// </summary>
	void Start() {
		pointsToAssignText.text = "Points to assign " + pointsToUpgrade;
		attackPointsText.text = "Attack Points: " + attackPoints;
		speedPointsText.text = "Speed Points: " + speedPoints;
		healthPointsText.text = "Max Health Points: " + maxHealthPoints;
		energyPointsText.text = "Max Energy Points: " + maxEnergyPoints;
		upgradePanel.SetActive (false);
		speed = walkingSpeed;
	}

	/// <summary>
	/// Checks if the player. If its capped the upgrade button won't blink.
	/// </summary>
	void Update(){
		if (attackPoints == 25 && speedPoints == 25 && maxHealthPoints == 25 && maxEnergyPoints == 25) {
			capped = true;
		}
	}

	/// <summary>
	/// When a new game is started this is called. sets all fields to default.
	/// </summary>
    public void firstLoad()
    {
        attackMultiplier = 1f + (float)attackPoints * 0.05f;
        maxWalkingSpeed = 5f + (float)speedPoints * 0.5f;
        runningSpeed = 2f * maxWalkingSpeed;
        maxHealth = 100 + 5 * maxHealthPoints;
        maxEnergy = 100 + 5 * maxEnergyPoints;
    }

	/// <summary>
	/// When the upgrade button is clicked, this method is used to show the upgrade panel;
	/// </summary>
    public void openUpgradePanel()
    {
        upgradePanel.SetActive(true);
        pointsToAssignText.text = "Points to assign " + pointsToUpgrade;
        attackPointsText.text = "Attack Points: " + attackPoints;
        speedPointsText.text = "Speed Points: " + speedPoints;
        healthPointsText.text = "Max Health Points: " + maxHealthPoints;
        energyPointsText.text = "Max Energy Points: " + maxEnergyPoints;
    }

	/// <summary>
	/// Closes the upgrade panel.
	/// </summary>
	public void closeUpgradePanel(){
		upgradePanel.SetActive (false);
	}

	/// <summary>
	/// Sets the level one higher and gives points to upgrade.
	/// </summary>
	private static void levelUp(){
		level ++;
		pointsToUpgrade += 3;
        Analytics.setPlayerLevel();
		setAmountOfExperienceToLevelUp ();
	}

	/// <summary>
	/// Gets the level. Used for saving the game.
	/// </summary>
	/// <returns>The level.</returns>
	public static int getLevel(){
		return level;
	}

	/// <summary>
	/// Gets the experience. Used for saving the game.
	/// </summary>
	/// <param name="xp">Xp.</param>
	public static void getExperience(int xp){
		experience += xp;
		if (experience >= level * experienceNeededToLevelUp) {
			levelUp ();
		}
	}

	/// <summary>
	/// Upgrades the attack.
	/// </summary>
	public void upgradeAttack(){
		if (pointsToUpgrade > 0 && attackPoints < 25) {
			attackPoints++;
			attackMultiplier = 1f + (float)attackPoints * 0.05f;
			attackPointsText.text = "Attack Points: " + attackPoints;
			pointsToUpgrade --;
			pointsToAssignText.text = "Points to assign " + pointsToUpgrade;
            Analytics.setPlayerUpgrades(1);
		}
	}

	/// <summary>
	/// Upgrades the speed.
	/// </summary>
	public void upgradeSpeed(){
		if (pointsToUpgrade > 0 && speedPoints < 25) {
			speedPoints++;
			maxWalkingSpeed = 5f + (float)speedPoints * 0.1f;
            runningSpeed = maxWalkingSpeed * 2;
			speedPointsText.text = "Speed Points: " + speedPoints;
			pointsToUpgrade --;
			pointsToAssignText.text = "Points to assign " + pointsToUpgrade;
            Analytics.setPlayerUpgrades(2);
        }
	}

	/// <summary>
	/// Upgrades the max health.
	/// </summary>
	public void upgradeMaxHealth(){
		if (pointsToUpgrade > 0 && maxHealthPoints < 25) {
			maxHealthPoints++;
			maxHealth = 100 + 5 * maxHealthPoints + 50 * amountOfHealthBeds;
			health += 5;
			healthPointsText.text = "Max Health Points: " + maxHealthPoints;
			pointsToUpgrade --;
			pointsToAssignText.text = "Points to assign " + pointsToUpgrade;
            Analytics.setPlayerUpgrades(3);
        }
	}

	/// <summary>
	/// Upgrades the max energy.
	/// </summary>
	public void upgradeMaxEnergy(){
		if (pointsToUpgrade > 0 && maxEnergyPoints < 25) {
			maxEnergyPoints++;
			maxEnergy = 500 + 20 * maxEnergyPoints;
			energyPointsText.text = "Max Energy Points: " + maxEnergyPoints;
			pointsToUpgrade --;
			pointsToAssignText.text = "Points to assign " + pointsToUpgrade;
            Analytics.setPlayerUpgrades(4);
        }
	}

	/// <summary>
	/// Gets the attack multiplier.
	/// </summary>
	/// <returns>The attack multiplier.</returns>
	public static float getAttackMultiplier(){
		return attackMultiplier;
	}

	/// <summary>
	/// Gets the speed.
	/// </summary>
	/// <returns>The speed.</returns>
	public static float getSpeed(){
		return speed;
	}

	/// <summary>
	/// Gets the max health.
	/// </summary>
	/// <returns>The max health.</returns>
	public static int getMaxHealth(){
		return maxHealth;
	}

	/// <summary>
	/// Sets the max health.
	/// </summary>
	/// <param name="res">Res.</param>
	public static void setMaxHealth(int res){
		maxHealth += res;
		health += res;
	}

	/// <summary>
	/// Gets the health.
	/// </summary>
	/// <returns>The health.</returns>
	public static int getHealth(){
		return health;
	}

	/// <summary>
	/// Sets the health.
	/// </summary>
	/// <param name="res">Res.</param>
	public static void setHealth(int res){
		health = res;
	}

	/// <summary>
	/// Used when the player is attacked by an enemy.
	/// </summary>
	/// <param name="damage">Damage.</param>
	public static void takeDamage(int damage){
		health -= damage;
        GameObject.Find("player").GetComponent<SoundsPlayer>().PlayHit();           // Sound
	}

	/// <summary>
	/// Gain 3 health. Is called in PlayerController every x seconds.
	/// </summary>
	public static void regenerate(){
		health += 3;
	}

	/// <summary>
	/// Gets the max energy.
	/// </summary>
	/// <returns>The max energy.</returns>
	public static int getMaxEnergy(){
		return maxEnergy;
	}

	/// <summary>
	/// Sets the max energy.
	/// </summary>
	/// <param name="res">Res.</param>
	public static void setMaxEnergy(int res){
		maxEnergy += res;
		energy += res;
	}

	/// <summary>
	/// Gets the energy.
	/// </summary>
	/// <returns>The energy.</returns>
	public static int getEnergy(){
		return energy;
	}

	/// <summary>
	/// Sets the energy.
	/// </summary>
	/// <param name="res">Res.</param>
    public static void setEnergy(int res){
        energy = res;
    }

	/// <summary>
	/// Used when the player is running. Every frame it loweres one.
	/// </summary>
	public static void setEnergyDown(){
		energy --;
	}

	/// <summary>
	/// Gain 20 energy. Called by PlayerController every x seconds.
	/// </summary>
	public static void replenish(){
        energy += 20;
	}

	/// <summary>
	/// Set the speed to running speed.
	/// </summary>
	public static void run(){
		speed = runningSpeed;
		running = true;
	}

	/// <summary>
	/// Set the speed to walking speed.
	/// </summary>
	public static void dontRun(){
		speed = walkingSpeed;
		running = false;
	}

	/// <summary>
	/// Returns if the player is running.
	/// </summary>
	/// <returns><c>true</c>, if running was ised, <c>false</c> otherwise.</returns>
	public static bool isRunning(){
		return running;
	}

	/// <summary>
	/// Gets the max fatique.
	/// </summary>
	/// <returns>The max fatique.</returns>
    public static int getMaxFatique(){
        return maxFatique;
    }

	/// <summary>
	/// Gets the fatique.
	/// </summary>
	/// <returns>The fatique.</returns>
	public static int getFatique(){
		return fatique;
	}

	/// <summary>
	/// Sets the fatique.
	/// </summary>
	/// <param name="res">Res.</param>
    public static void setFatique(int res){
        fatique = res;
    }

	/// <summary>
	/// Sets fatique one lower, called every frame. And lower the walkingSpeed according to the fatique.
	/// </summary>
    public static void getTired(){
		fatique--;
		walkingSpeed = maxWalkingSpeed * Mathf.Pow (0.8f, (10000f - (float)fatique) / 10000f);
	}

	/// <summary>
	/// Sets fatique to maximum. Called when a player enters the base.
	/// </summary>
	public static void resetFatique(){
		fatique = maxFatique;
		walkingSpeed = maxWalkingSpeed;
	}

	/// <summary>
	/// Resets the points.
	/// </summary>
    private static void resetPoints(){
        attackPoints = 0;
        speedPoints = 0;
        maxHealthPoints = 0;
        maxEnergyPoints = 0;
    }

	/// <summary>
	/// Reset this instance.
	/// </summary>
    public static void reset(){
        level = 1;
        experience = 0;
        pointsToUpgrade = 0;

        resetPoints();

        attackMultiplier = 1f;
        walkingSpeed = 5f;
        maxWalkingSpeed = 5f;
        runningSpeed = walkingSpeed * 2f;
        maxHealth = 100;
        health = 100;
        maxEnergy = 500;
        energy = 500;

        running = false;
        speed = 5f;

        maxFatique = 10000;
        fatique = 10000;
	}

	/// <summary>
	/// Sets the amount of experience to level up.
	/// </summary>
	public static void setAmountOfExperienceToLevelUp(){
		experienceNeededToLevelUp = 5 + (int)(0.5 * level * level);
	}
}