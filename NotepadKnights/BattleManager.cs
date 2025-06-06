﻿namespace NotepadKnights
{
    internal class BattleManager
    {
        private int battleLevel = 1;
        private bool checkIntoBattleFirst;
        private string monsterIndexDisplay = "";
        AttackAndDefense atkAndDef = new AttackAndDefense();
        public static event Action<string> OnMonsterKilled;
        private MonsterFactory _monsterFactory;
        private PlayerStatus _playerStatus = Program.playerStatus;
        private BattleRewardManager _battleRewardManager = new BattleRewardManager();

        public void Run()
        {
            _monsterFactory = new MonsterFactory();
            _monsterFactory.InitializeMonstersOnDifficulty(battleLevel);
            IntoBattle();
        }
        
        private void IntoBattle()
        {
            while (!CheckDefeat())
            {
                if (_playerStatus.KilledMonsterCount >= _monsterFactory.createMonsters.Count)
                {
                    Victory();
                    _playerStatus.KilledMonsterCount = 0;
                    return;
                }
                else
                {
                    Monster target;
                    while (true)
                    {
                        DisplayBattleScene();
                        DisplaySelectAction();

                        DisplayBattleScene();
                        DisplayNextSelectAction();
                        int select = InputManager.ReadInt(0, _monsterFactory.createMonsters.Count, "대상을 선택해주세요");
                        if (select == 0)
                        {
                            _playerStatus.IsAttack = false;
                            continue;
                        }
                        
                        target = _monsterFactory.createMonsters[select - 1];
                        if (target.CurrentHp > 0)
                        {
                            Program.playerStatus.Target = target;
                            break;
                        }
                        
                        Console.WriteLine("해당 몬스터는 이미 죽었습니다.");
                        Thread.Sleep(1000);
                        _playerStatus.IsAttack = false;
                    }
                    
                    ExecutePlayerPhase(target);
                    InputManager.ReadInt(0, 0);
                    ExecuteEnemyPhase();
                    _playerStatus.IsAttack = false;
                }
            }
            
            DisplayDefeat();
            Program.playerStatus.KilledMonsterCount = 0;
        }
        
        private bool CheckDefeat()
        {
            return _playerStatus.Hp <= 0;
        }

        private void DisplayDefeat()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.Write($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
            Console.Write($"HP 100 -> {Program.playerStatus.Hp}\n");
            InputManager.ReadInt(0, 0, "0. 나가기");
        }

        private void Victory()
        {
            checkIntoBattleFirst = true;
            battleLevel++;
            VictoryDisplay();
            _battleRewardManager.GetRewards(_playerStatus.KilledMonsterCount);
        }

        private void VictoryDisplay()
        {
            Console.Clear();
            Console.WriteLine("\nBattle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine($"던전에서 몬스터 {Program.playerStatus.KilledMonsterCount}마리를 잡았습니다.\n");
            Console.WriteLine($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
            Console.WriteLine($"현재 체력 : {Program.playerStatus.Hp}\n");
            InputManager.ReadInt(0, 0, "0. 나가기");
        }

        private void DisplayBattleScene()
        {
            if (checkIntoBattleFirst)
            {
                Console.Clear();
                Console.WriteLine($"Level {battleLevel} 던전에 진입합니다.");
                Thread.Sleep(1000);
                checkIntoBattleFirst = false;
            }

            Console.Clear();
            Console.WriteLine("<전투!!>\n");
            Console.WriteLine($"(현재 던전의 레벨 : {battleLevel})");
            Console.WriteLine($"[{Program.playerStatus.Name} 턴]\n");

            for (int i = 0; i < _monsterFactory.createMonsters.Count; i++)
            {
                monsterIndexDisplay = Program.playerStatus.IsAttack ? (i + 1).ToString() : "";

                var monster = _monsterFactory.createMonsters[i];
                string monsterHpTxt = monster.IsDead ? "Dead" : $"HP {monster.CurrentHp}";
                if (monster.IsDead) Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{monsterIndexDisplay} Lv.{monster.Level} {monster.Name} {monsterHpTxt}");
                Console.ResetColor();
            }
        }

        private void DisplaySelectAction()
        {
            bool actionSelected = false;

            while (!actionSelected)
            {
                Console.WriteLine("\n\n\n- 플레이어 행동");
                Console.WriteLine("① 공격  ② 스킬  ③ 아이템  ④ 스테이터스");

                int choice = InputManager.ReadInt(1, 4);

                switch (choice)
                {
                    case 1:
                        _playerStatus.IsAttack = true;
                        _playerStatus.UseSkill = false;
                        actionSelected = true; // 공격 선택 시 루프 탈출
                        break;

                    case 2:
                        _playerStatus.IsAttack = true;
                        _playerStatus.UseSkill = true;
                        actionSelected = true;
                        break;

                    case 3:
                        Program.InventoryManager.Run();
                        DisplayBattleScene();
                        break;

                    case 4:
                        // 스테이터스 출력
                        ShowStatus();
                        DisplayBattleScene();
                        break;
                }
            }
        }

        private void DisplayNextSelectAction()
        {
            Console.WriteLine("0. 취소\n");
        }

        private void ExecutePlayerPhase(Monster targetMonster)
        {
            Console.Clear();
            Console.WriteLine($"[{Program.playerStatus.Name} 턴]\n");
            float playerDamage;
            playerDamage = _playerStatus.UseSkill ? Skill.SkillUse(Program.playerStatus.Mp) : atkAndDef.Attack(Program.playerStatus.Attack + Program.playerStatus.ExtraAttack);
            
            targetMonster.CurrentHp = Math.Max(atkAndDef.EnemyDefense(playerDamage, targetMonster.CurrentHp), 0);
            DisplayAttackResult(playerDamage, Program.player.msg, targetMonster);
        }

        private void DisplayAttackResult(float playerDamage, string msg, Monster targetMonster)
        {
            Monster playerTarget = targetMonster;

            if (!atkAndDef.onDodge) Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name}에게 {playerDamage}만큼의 피해!\n"); Thread.Sleep(1000);

            if (playerTarget.CurrentHp == 0)
            {
                OnMonsterKilled?.Invoke(playerTarget.Name);
                playerTarget.IsDead = true;
                playerTarget = null;
                Console.WriteLine("HP 0 ->Dead\n");
                Program.playerStatus.KilledMonsterCount++;
            }
            else
            {
                Console.WriteLine($"현재 [{playerTarget.Name}]의 HP : {playerTarget.CurrentHp}\n");
            }

            Console.WriteLine(msg + "\n");
            Console.Write("0. 다음\n>>");
        }

        private void ExecuteEnemyPhase()
        {
            foreach (Monster monster in _monsterFactory.createMonsters)
            {
                Console.Clear();
                Console.WriteLine("[적의 턴]\n");
                if (monster.CurrentHp <= 0 || Program.playerStatus.Hp == 0) { continue; }
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                Thread.Sleep(1000);
                
                int monsterAtk = monster.DealDamage();
                int playerHpAfterDamaged = atkAndDef.PlayerDefense(monsterAtk, Program.playerStatus.Hp, Program.playerStatus.Defense + Program.playerStatus.ExtraDefense);

                Console.WriteLine();
                Console.WriteLine($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
                Console.WriteLine($"HP {Program.playerStatus.Hp} -> {playerHpAfterDamaged}");
                
                // 위의 공격 주고 받는 부분 출력 이후 계산
                Program.playerStatus.Hp = playerHpAfterDamaged;
                Console.WriteLine("0. 다음");

                InputManager.ReadInt(0, 0);
            }
            Console.WriteLine("\n몬스터들의 공격 차례가 끝났습니다.");
            Thread.Sleep(1000);
        }

        private void ShowStatus()
        {
            Program.playerStatus.ShowStatus();
        }
    }
}
