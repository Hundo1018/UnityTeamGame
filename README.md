# UnityTeamGame
===

版本更改內容 :
/PlayerController.cs
	ability和ultimateAbility從2維bool陣列降成1維int陣列
	新增int abilityClears_Num //以數字表示清除的位置
	bool CheckAbility()和bool CheckUltimateAbility()的判斷方式
	合併void GetInput()和void action()的功能為void action()
	//GetInput 原判斷按鍵按下
	//action 原執行動作
/StageManager.cs
	bool stageStatus從2維陣列降成1維陣列
	bool SetStatus新增(int x, int y, bool s)的輸入
	新增bool Status(int x, int y) //取得stageStatus的元素 
	新增bool[] GetAllStatus() //取得stageStatus
	新增void ChangeAllStatus(int n) //傳入25bits數字做一次性清除(遇1清除)


===

目前做了Prototype

##### Let's do it
### Prototype 現已開發 (在develop分支中)

以下簡單描述一下各分支名稱在幹嘛
| master | develop | feature |  
| ------ | ------ | ------ |
| 可釋出的版本| 開發中的版本 | 開發中的功能 |
====
目前做了Prototype




