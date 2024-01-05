------>      ACTIVE Time Reward:
 This one allow you to give you a easy way to reward the player for playing your game.

 First you have to chose if you want to make a sequencial reward or just a time fixed one. 
Then your create the reward by using:

 -> ActiveTimeReward timeR = new ActiveTimeReward(String rewardName, int seconds, int minutes, int hours) 
 -> ActiveTimeReward timeR = new ActiveTimeReward(String rewardName, int seconds, int minutes) 
 -> ActiveTimeReward timeR = new ActiveTimeReward(String rewardName, int seconds) 
For Fixed time.

Or:
 -> ActiveTimeReward timeR = new ActiveTimeReward(String rewardName, int[] seconds, bool reset = false)
For Sequencial time.

Variables:
 String rewardName -> The reward name and where the name file where it will save.
 int seconds/minutes/hours -> Seconds between each reward it will add each one and make the final reward time.
 int[] seconds -> It is the array of seconds between each reward.
 bool reset -> The boolean will control if the reward system will loop or just stay on the last one if reached it.

Then you have to add the callback for it:
 - timeR.AddCallBack()
Example call back void Reward(int index) {} Where the index is the index for the sequencial reward for fixed time it will deafult 0.

To start you just call: 
 - StartCoroutine(timeR.Run());

And its done then you just need to add the logic for you game in the call back.

------>      DAILY Time Reward:
 This allow you to reward your players for daily play your game.
 
First you have to create the reward by using:

 -> DailyReward timeR = new DailyReward(String rewardName, int maxDays);

Variables:
 String rewardName -> The reward name and where the name file where it will save.
 int maxDays -> The number of day that will have your reward.

Then you have to add the callback for it:
 - timeR.AddCallBack()
Example call back void Reward(int day) {} Where the day is the day of reward of the player.

So whenever you want to check if your player has a reward , use :
 -> timeR.Check();

And its done then you just need to add the logic for you game in the call back.

------>      OFFLINE Time Reward:
 This one allow you to give you a easy way to reward the player.

 First you have to chose if you want to make a sequencial reward or just a time fixed one. 
Then your create the reward by using:

 -> TimeReward timeR = new TimeReward(String rewardName, int seconds, int minutes, int hours) 
 -> TimeReward timeR = new TimeReward(String rewardName, int seconds, int minutes) 
 -> TimeReward timeR = new TimeReward(String rewardName, int seconds) 
For Fixed time.

Or:
 -> TimeReward timeR = new TimeReward(String rewardName, int[] seconds, bool reset = false)
For Sequencial time.

Variables:
 String rewardName -> The reward name and where the name file where it will save.
 int seconds/minutes/hours -> Seconds between each reward it will add each one and make the final reward time.
 int[] seconds -> It is the array of seconds between each reward.
 bool reset -> The boolean will control if the reward system will loop or just stay on the last one if reached it.

Then you have to add the callback for it:
 - timeR.AddCallBack()
Example call back void Reward(int index) {} Where the index is the index for the sequencial reward for fixed time it will deafult 0.

So whenever you want to check if your player has a reward , use :
 -> timeR.Check();

And its done then you just need to add the logic for you game in the call back.

-------->     DONE:
 So that is all its simple and very usefull.