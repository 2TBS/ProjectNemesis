#Project Rush: The 2D Super Tower Defense
###Brought to you by 2TBS Studios

#NOTICE TO ALL DEVS: DO NOT COMMIT TO MASTER! COMMIT TO DEVELOP INSTEAD.

##Platforms
 - PC
 - Mac OS
 - Linux
 - Android (maybe)
 - IOS (maybe)

##Alpha 1.0.0 Features
1. Main menu (i'll copy paste this from GameJam2016: just a username input + play button for now)
2. Networking ( @Vikram P. halp plx )
3. One basic minion
4. One basic map. Just one map with a track, entry point for minions and Gov't base
5. One basic tower and mechanics to place it on map
6. Game ends when minion reaches gov't


##Introduction

Project Rush is a casual, multiplayer 1v1 matchup game with mechanics similar to a tower defense game. Each player has a different role, as outlined below:

**Player 1: The Defender:**  Player 1 plays a traditional tower defense battle, gaining the ability to place towers in order to attempt to stop Player 2.

**Player 2: The Attacker:** Player 2 controls a massive army set to destroy the Defender. This player can customize the type and quantity of minions, as well as their priority targets.

##Objective of the Game

Each game will be timed, which is determined by the players’ choice of 3,5, or 10 minutes before the match begins. If the time runs out and the Defender is not destroyed, the Defender wins. If the Defender is destroyed, the Attacker wins. The effectiveness of the Attacker is varied based on the time selected.

##Before Matches
 - Player is able to upgrade minions/towers with starting Foobar amount of 500.
 - Create separate armies/tower selections to bring into matches (like selecting a deck of cards in Clash Royale)
 - Options to choose different commanders that give boosts to the entire army/all towers (e.g. a strict dictator-like commander that 	improves attack dmg for attackers or a priest that improves SOLS for defenders)
 - Perhaps only one commander per player so they have to make the choice between having a commander for defending or a commander for attacking
 - Can be bought or won through achievements, cannot be upgraded
 - Some are joke commanders, like only changing aesthetics
 - Upgrade armies with monies?
 - Maybe spectate other battles?
 - Justice rains from above?

##Match Start
 - Each player begins with 500 Foobars
 - Time selection period: 10 seconds to choose.  The lowest time selected by either player is chosen (so that people who are crunched for time can easily play).
 - The matches will be balanced for 5 minutes then tweaked to fit the other times.

##The Map
For now, we will have one set map, with this design:
	
![Map 1](https://cdn.discordapp.com/attachments/261198631762264065/263850699476041728/ConceptPicPR1.png "Map 1")
	
Eventually we will plan to allow for players to dynamically shape the map during the match. That will come later, though. Don’t worry about it
	
##Match Mechanics
 - Def. starts by placing towers around the map (maybe timed), then def.’s turn ends
 - Att. cannot do anything in this turn
 - Att.’s turn begins (also timed) in which att. places minions strategically
 - During the attack, maybe def. has potions/spells to actively use, with limited effects


####The Life Point System: SOLS
*How about Stand Unit for Health, Punk(SUHP)?*
SOLS stands for whatever you want it to.
Rough estimates: 100 SOLS for the weakest of the weak; 100,000 for rekt noobs

####The Currency System: Foobars (the best we can come up with)
Rough conversion factor: 1 Foobar = a small loan of a million USD omg
Weak tower: 250 Foobars
Average tower: 1000 Foobars
Buffy tower: 2750 Foobars
Minions: 50 Foobars(weak) to 1000 Foobars (strong)

Defending Player

##List of Towers

####Tower Template
 - **DESCRIPTION** This is the template you should follow if you want to design your own tower. Put a detailed description here.
 - **SOLS** OVER NINE THOUSAND!!!!!!!!!!!
 - **Price** OVER NINE THOUSAND!!!!!!!!!!!!
 - **Damage** Over 9000 SOLS
 - **Upgrade 1** Generic Upgrade: Increases damage by 20%. Costs 10 Foobars
 - **Upgrade 2** Another Generic Upgrade: Increases uselessness by 100%. Costs 100 Foobars

####Tower 2: Something to do with fire?
####Tower 3: Healers?
 - **SOLS** 1000
 - **Price** 400
 - **Healing**  10/second to small radius
 - **Upgrade 1** Increases healing by 20. Costs 500 Foobars
 - **Upgrade 2** Increases AOE by 100%. Costs 750 Foobars
####Tower 4: something to do with AOE?
####Tower 5: generic single target attacker
####Tower 6: tower which can slow down minions (freeze tower?)
####Tower 7: repulsion (push back minions?)
####Tower 8: G3t R3kt m8 (the overpriced but op tower)
 - **DESCRIPTION** Really really really expensive. For endgame. Shoots a massive pink laser.
 - **SOLS** 2500
 - **Price** 8000
 - **Damage** 50/second to a decently sized radius
 - **Upgrade 1** Increases radius by 20%. (Turns from pink to bright green) Price: 1000 Foobars 
 - **Upgrade 2** Also sets everything on fire. (Turns from bright green to dark red) Price: 1500 Foobars
####Tower9 lIGHTING (KILL MINIONS) takes long time to re load
##Splash Potions
###Potion Template
 - **DESCRIPTION** This is the template you should follow if you want to design your own tower. Put a detailed description here.
 - **Range** BLAAAAAAAAAA big
 - **Duration** blablablalblabal seconds
 - **Price** OVER NINE THOUSAND!!!!!!!!!!!!
 - **Damage** Over 9000 SOLS
 - **Upgrade 1** Generic Upgrade: Increases how much Sols you get. Costs 10 DOODs
 - **Upgrade 2** Another Generic Upgrade: Increases How much sols you get. Costs 100 dolloreydoodas

####Damage (Overtime)
 - **DESCRIPTION**Deals Damage over time in an area of space
 - **Range**(small at first rank)
 - **Duration** 5
 - **Price** 100
 - **Damage** 20 per second
 - **Upgrade 1 ** Dmg: 25    Dur: 10
 - **Upgrade 2 ** Dmg: 35    Dur: 15 

##Minion Idea

Instead of having set minions, players will be able to create their own, with customized health, defense, and damage. 

![Minion Idea](https://cdn.discordapp.com/attachments/261198631762264065/263499041294254082/unknown.png "Minion Idea")
 - Health: x Foobars per SOLS
 - Defense: x Foobars per percentage grade
 - Damage: x Foobars per SOLS
 - Based on the stats, the minions will appear differently
