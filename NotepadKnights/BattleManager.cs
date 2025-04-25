using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotepadKnights
{
    internal class BattleManager
    {
        private string monsterIndexDisplay = "";
        AttackAndDefense atkAndDef = new AttackAndDefense();

        private MonsterFactory _monsterFactory;
        private PlayerStatus _playerStatus = Program.playerStatus;
        private BattleRewardManager _battleRewardManager = new BattleRewardManager();


        public void Run()
        {
            _monsterFactory = new MonsterFactory();
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
            Console.Clear();
            Console.WriteLine("<전투!!>\n");
            Console.WriteLine($"[{Program.playerStatus.Name} 턴]\n");

            for (int i = 0; i < _monsterFactory.createMonsters.Count; i++)
            {
                monsterIndexDisplay = Program.playerStatus.IsAttack ? (i + 1).ToString() : "";

                var monster = _monsterFactory.createMonsters[i];
                string monsterHpTxt = monster.IsDead ? "Dead" : $"HP {monster.CurrentHp}";
                Console.WriteLine($"{monsterIndexDisplay} Lv.{monster.Level} {monster.Name} {monsterHpTxt}");
            }

            Console.WriteLine("\n\n[내정보]");
            Console.Write($"Lv.{Program.playerStatus.Level}\n이름 : {Program.playerStatus.Name}\n직업 :{Program.playerStatus.Job}\nMp : {Program.playerStatus.Mp}\n");
            Console.WriteLine($"HP {Program.playerStatus.Hp}/{Program.playerStatus.MaxHp}\n");
        }

        private void DisplaySelectAction()
        {
            Console.WriteLine("1. 공격\n");
            InputManager.ReadInt(1, 1);
            _playerStatus.IsAttack = true;
        }
        private void DisplayNextSelectAction()
        {
            Console.WriteLine("0. 취소\n");
            //Console.WriteLine(Program.player.msg + "????????\n");
        }

        private void ExecutePlayerPhase(Monster targetMonster)
        {
            float playerDamage = Skill.SkillUse(Program.playerStatus.Mp);
            _playerStatus.Attack = playerDamage;
            
            targetMonster.CurrentHp = Math.Max(atkAndDef.EnemyDefense(playerDamage, targetMonster.CurrentHp), 0);
            DisplayAttackResult(playerDamage, Program.player.msg, targetMonster);
        }
        
        private void DisplayAttackResult(float playerDamage, string msg, Monster targetMonster)
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            Console.WriteLine($"{Program.playerStatus.Name} 의 공격!");

            Monster playerTarget = targetMonster;

            Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name} 을(를) 맞췄습니다. [데미지 : {playerDamage}]\n");
            Console.WriteLine($"Lv.{playerTarget.Level} {playerTarget.Name}");

            // 이 부분 IsDead로 관리해야 할 것 같은데 실시간 반영이 안 되어서 CurrentHP로 관리하겠습니다.
            if (playerTarget.CurrentHp == 0)
            {
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
                int playerHpAfterDamaged = atkAndDef.PlayerDefense(monsterAtk, Program.playerStatus.Hp, Program.playerStatus.Defense);
                
                // Console.WriteLine($"{Program.playerStatus.Name} 을(를) 맞췄습니다.   [데미지 : {monster.Atk - Program.playerStatus.Defense}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{Program.playerStatus.Level} {Program.playerStatus.Name}");
                Console.WriteLine($"HP {Program.playerStatus.Hp} -> {playerHpAfterDamaged}");
                
                // 위의 공격 주고 받는 부분 출력 이후 계산
                Program.playerStatus.Hp = playerHpAfterDamaged;
                Console.WriteLine("0. 다음");

                InputManager.ReadInt(0, 0);
            }
            Console.Clear();
            Console.WriteLine("몬스터들의 공격 차례가 끝났습니다.");
            Thread.Sleep(1000);
        }
    }
}
