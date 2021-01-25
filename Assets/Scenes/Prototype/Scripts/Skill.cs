using System;
using UnityEngine;

namespace SkillConstructer
{
    public class Skill
    {
        private int _w, _h;  //觸發圖形大小
        private int _shape; //觸發圖形 或 攻擊範圍

        private int _type;  //攻擊類型 0 玩家技能判定 1 以絕對座標攻擊 2 以隨機座標攻擊 3 以相對座標攻擊

        private int _data;  //攻擊數據(隨Tpye變化) 0 無數據 1 攻擊數量 2 基準位置
        private int _nextRange;//下次攻擊範圍

        //隨機生成亂數用
        private double rand
        {
            get
            {
                return UnityEngine.Random.value;
            }
        }

        //攻擊時要做的動作
        public Action<int, int> DoAttackForEachCube = (x, y) => { };
        public Action DoBeforeAttack = () => { };
        public Action DoAfterAttack = () => { };

        //初始化變數用
        private void Init(int w, int h, int shape, int type, int data)
        {
            _w = w; _h = h;
            _shape = shape;
            _type = type;
            _data = data;
            CreateNextRange();
        }

        //初始化變數用
        private void Init(string shape, int type, int data)
        {
            string[] str = shape.Split(';');
            int h = str.Length, w = 0, tmp_shape = 0;
            if (h > 5) h = 5;

            for (int i = 0; i < h; i++)
            {
                if (w < str[i].Length)
                {
                    w = str[i].Length;
                    if (w > 5) w = 5;
                }
                str[i] += "00000";
                for (int j = 0; j < 5; j++)
                {
                    tmp_shape <<= 1;
                    if (str[i][j] != '0') tmp_shape += 1;
                }
            }
            tmp_shape <<= (5 - h) * 5;

            _w = w; _h = h;
            _shape = tmp_shape;
            _type = type;
            _data = data;
            CreateNextRange();
        }

        private void CreateNextRange()
        {
            //攻擊範圍(固定式)
            if (_type == 1) _nextRange = _shape;
            //攻擊範圍(隨機式)
            else if (_type == 2)
            {
                int max = _data / 2, cou = 0, i = 0;
                if (_data % 2 == 0)
                {
                    _nextRange = 0;
                    //隨機生成
                    while (i < 25)
                    {
                        _nextRange <<= 1;
                        i++;
                        if (rand * 25 < max)
                        {
                            _nextRange += 1;
                            cou++;
                        }
                        //確認是否達到上限
                        if (cou == max) break;
                    }
                    _nextRange <<= 25 - i;
                    //過濾範圍外位置
                    _nextRange &= _shape;
                }
                else
                {
                    int tempRange = 1;
                    _nextRange = _shape;
                    //計算場上可釋放的格子數量
                    while (i < 25)
                    {
                        tempRange <<= 1;
                        if (_nextRange % 2 == 1)
                        {
                            tempRange += 1;
                            cou++;
                        }
                        _nextRange >>= 1;
                        i++;
                    }
                    //判斷上限是否超過格子數
                    if (max > cou) max = cou;
                    //隨機生成
                    i = 0;
                    while (i < max)
                    {
                        _nextRange <<= 1;
                        if (tempRange % 2 == 1)
                        {
                            //判斷是否剩餘格子數不夠
                            if (cou == max - i)
                            {
                                _nextRange += 1;
                                i++;
                            }
                            else if (rand * 25 < max)
                            {
                                _nextRange += 1;
                                i++;
                            }
                            cou--;
                        }
                        tempRange >>= 1;
                    }
                    //填滿25格
                    while (tempRange > 1)
                    {
                        _nextRange <<= 1;
                        tempRange >>= 1;
                    }
                }
            }
            //攻擊範圍(追蹤式)
            else if (_type == 3)
            {
                int[] temp = new int[25];
                _nextRange = _shape;
                int dy = (_data / 5)+3, dx = (_data % 5)+3;
                for (int i = 4; i >= 0; i--)
                {
                    for (int j = 4; j >= 0; j--)
                    {
                        temp[((i + dy) * 5 + ((j + dx) % 5)) % 25] = _nextRange % 2;
                        _nextRange >>= 1;
                    }
                }
                for (int i = 0; i < 25; i++)
                {
                    _nextRange <<= 1;
                    _nextRange += temp[i];
                }
            }
            //例外狀況
            else _nextRange = 0;
        }

        //空技能
        public Skill()
        {
            Init(0, 0, 0, -1, 0);
        }

        //建構玩家技能 或 固定式敵人攻擊
        //shape是以每一Row以;分隔
        //isPlayer True為玩家技能 False為固定式技能
        public Skill(string shape, bool isPlayer)
        {
            if (isPlayer)
                Init(shape, 0, 0);
            else Init(shape, 1, 0);
        }

        //建構隨機式敵人攻擊
        //range是以每一Row以;分隔
        //type 0是小於等於numberOfCube 1是等於numberOfCube
        //numberOfCube是召喚的上限
        public Skill(string range, int type, int numberOfCube)
        {
            Init(range, 2, (numberOfCube << 1) + type);
        }

        //建構追蹤式敵人攻擊
        //range是以每一Row以;分隔
        //position是玩家的位置
        public Skill(string range, Vector2Int position)
        {
            Init(range, 3, position.y * 5 + position.x);
        }

        /*//建構固定式敵人攻擊
        //range是以25bits來表示的攻擊範圍
        public Skill(int range)
        {
            Init(0, 0, range, 1, 0);
        }
  
        //建構隨機式敵人攻擊
        //range是以25bits來表示的攻擊範圍
        //type 0是小於等於numberOfCube 1是等於numberOfCube
        //numberOfCube是召喚的上限
        public Skill(int range, int type, int numberOfCube)
        {
            Init(0, 0, range, 2, (numberOfCube << 1) + type);
        }
  
        //建構追蹤式敵人攻擊
        //range是以25bits來表示的相對攻擊範圍
        //position是玩家的位置
        public Skill(int range, Vector2Int position)
        {
            Init(0, 0, range, 3, position.y * 5 + position.x);
        }//*/

        //複製Skill
        public Skill(Skill skill)
        {
            Init(skill._w, skill._h, skill._shape, skill._type, skill._data);
        }

        //更改隨機式攻擊的數據
        //type 0是小於等於numberOfCube 1是等於numberOfCube
        //numberOfCube是召喚的上限
        public void ChangeData(int type, int numberOfCube)
        {
            Init(0, 0, _shape, 2, (numberOfCube << 2) + type);
        }

        //更改追蹤式攻擊的數據
        //position是玩家的位置
        public void ChangeData(Vector2Int position)
        {
            Init(0, 0, _shape, 3, position.y * 5 + position.x);
        }

        //判斷技能是否觸發
        public bool isTrigger(bool[] mapStatus)
        {
            int status = 0;
            _nextRange = _shape;
            for (int i = 0; i < 25; i++)
            {
                status <<= 1;
                if (mapStatus[i]) status += 1;
            }
            for (int i = 0; i < 6 - _h; i++)
            {
                for (int j = 0; j < 6 - _w; j++)
                {
                    if ((status & _nextRange) == _nextRange) return true;
                    _nextRange >>= 1;
                }
                _nextRange >>= _w;
            }
            return false;
        }

        //取得敵人攻擊範圍
        public int getAttackRange()
        {
            if (_type == 0) _nextRange = _shape;
            return _nextRange;
        }

        //取得敵人攻擊範圍 並計算下次攻擊位置
        public int getAttackRangeWithChange()
        {
            int temp = _nextRange;
            CreateNextRange();
            return _nextRange;
        }

        //敵人用攻擊
        public void Attack()
        {
            int attackRange = getAttackRangeWithChange();
            int i = 24;
            DoBeforeAttack();
            while (attackRange != 0)
            {
                //攻擊範圍內再動作
                if (attackRange % 2 == 1)
                {
                    DoAttackForEachCube(i % 5, i / 5);
                }
                attackRange >>= 1;
                i -= 1;
            }
            DoAfterAttack();
        }

        //玩家用攻擊
        public bool Attack(Vector2Int position, bool[] mapStatus)
        {
            int status = 0, w = 6 - _w, h = 6 - _h, size = w * h;
            int pos = (position.y - _h + 1) * w + (position.x - _w + 1);
            bool[] isOK = new bool[size];
            _nextRange = _shape;
            //讀取地圖狀態
            for (int i = 0; i < 25; i++)
            {
                status <<= 1;
                if (mapStatus[i]) status += 1;
            }
            //確認符合區域
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    isOK[i * w + j] = ((status & _nextRange) == _nextRange);
                    _nextRange >>= 1;
                }
                _nextRange >>= _w-1;
            }
            //尋找適當的位置
            while (pos < 0) pos += w;
            for (int i = 0; i < size; i++)
            {
                if (isOK[(pos + i) % size])
                {
                    pos = (pos + i) % size;
                    break;
                }
            }
            //執行動作
            if (isOK[pos])
            {
                int shape = _shape;
                shape >>= (pos / w) * 5 + (pos % w);
                int i = 24;
                DoBeforeAttack();
                while (shape != 0)
                {
                    if (shape % 2 == 1)
                    {
                        DoAttackForEachCube(i % 5, i / 5);
                    }
                    shape >>= 1;
                    i -= 1;
                }
                DoAfterAttack();
                return true;
            }
            return false;
        }
    }

    public class Effect
    {
        
        private int _type;  //攻擊類型
        private int _times, _base;//倍率 基底
        // NewValue = OldValue * _times + _base

        private int _interval, _totalTime, _remaining;
        //間格時間 持續時間 剩餘時間

        public Func<bool> _condition;
        //觸發條件

        //初始化變數用
        private void Init(int type, int times, int baseValue, int interval, int totalTime, Func<bool> condiiton)
        {
            _type = type;
            _times = times;
            _base = baseValue;
            _interval = interval;
            _totalTime = totalTime;
            _remaining = -1;
            _condition = condiiton;
        }

        //空效果
        public Effect()
        {
            Init(0, 1, 0, 1, 0, () =>
            {
                return ((_totalTime - _remaining) % _interval) == 0;
            });
        }

        //單次觸發型(ex:受傷)
        //times是數值乘以地倍率
        //baseValue是再加上的基底
        private Effect(int times, int baseValue)
        {
            Init(1, times, baseValue, 1, 1, () =>
            {
                return ((_totalTime - _remaining) % _interval) == 0;
            });
        }

        //持續發作型(ex:減速)
        //times是數值乘以地倍率
        //baseValue是再加上的基底
        //totalTime是效果的持續時間
        private Effect(int times, int baseValue, int totalTime)
        {
            Init(1, times, baseValue, 1, totalTime, () =>
            {
                return ((_totalTime - _remaining) % _interval) == 0;
            });
        }

        //多次觸發型(ex:中毒)
        //times是數值乘以地倍率
        //baseValue是再加上的基底
        //totalTime是效果的持續時間
        //interval是效果觸發的間格時間
        private Effect(int times, int baseValue, int totalTime, int interval)
        {
            Init(1, times, baseValue, interval, totalTime, () =>
            {
                return ((_totalTime - _remaining) % _interval) == 0;
            });
        }

        //特殊觸發型(ex:反傷)
        //times是數值乘以地倍率
        //baseValue是再加上的基底
        //totalTime是效果的持續時間
        //conditoin是觸發的條件
        private Effect(int times, int baseValue, int totalTime, Func<bool> condition)
        {
            Init(1, times, baseValue, 1, totalTime, condition);
        }

        //開始倒數
        public void Reset()
        {
            _remaining = _totalTime;
        }

        //執行
        public bool Do(ref int value)
        {
            if (_remaining>0)
            {
                if (_condition())
                    value = value * _times + _base;
                _remaining--;
                return true;
            }
            return false;
        }
    }
}
