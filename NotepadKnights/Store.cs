namespace NotepadKnights;

public class Store
    {
        public List<Item> Items { get; private set; }
        public Dictionary<Item, int> NumberOfItems { get; private set; }

        public Store()
        {
            Items = new List<Item>();
            NumberOfItems = new Dictionary<Item, int>();
            InitialItems();
        }

        public Item SellItem(int index)
        {
            NumberOfItems[Items[index]]--;
            return Items[index];
        }

        public void PurchaseItem(string itemName)
        {
            Item? foundItem = Items.Find(i => i.Name == itemName);
            if (foundItem != null)
            {
                if (!NumberOfItems.TryAdd(foundItem, 1))
                {
                    NumberOfItems[foundItem]++;
                }
            }
        }

        public void DisplayStore()
        {
            Console.WriteLine("[상점 아이템 목록]");
            int index = 1;
            foreach (Item item in Items)
            {
                string indexText = $"{index++}.";
                string typeText = item.Type switch
                {
                    ItemType.Weapon => "공격력",
                    ItemType.Armor => "방어력",
                    ItemType.Potion => "회복력",
                    _ => "?",
                };
                string goldText = $"{item.Price + "G", -7}";
                string countText = NumberOfItems[item] == 0 ? "Sold Out" : $"수량: {NumberOfItems[item]}"; 
                Console.WriteLine($"- {indexText} {item.Name} | {typeText} {"+" + item.Point, -3} | {item.State} | {countText} | {goldText}");
            }
        }

        private void InitialItems()
        {
            Items.Add(new Item($"{ "수련자 갑옷", -10 }\t", ItemType.Armor, 5, $"{ "수련에 도움을 주는 갑옷입니다.", -32 }\t\t", 1000));
            Items.Add(new Item($"{ "무쇠 갑옷", -10 }\t", ItemType.Armor, 9, $"{ "무쇠로 만들어져 튼튼한 갑옷입니다.", -32 }\t\t", 2200));
            Items.Add(new Item($"{ "스파르타 갑옷", -10 }\t", ItemType.Armor, 15, $"{ "스파르타 전사들이 사용했다는 전설의 갑옷입니다.", -32 }\t", 3500));
            Items.Add(new Item($"{ "낡은 검", -10 }\t", ItemType.Weapon, 2, $"{ "쉽게 볼 수 있는 낡은 검 입니다.", -32 }\t\t", 600));
            Items.Add(new Item($"{ "청동 도끼", -10 }\t", ItemType.Weapon, 5, $"{ "어디선가 사용됐던거 같은 도끼입니다.", -32 }\t", 1500));
            Items.Add(new Item($"{ "스파르타의 창", -10 }\t", ItemType.Weapon, 7, $"{ "스파르타 전사들이 사용했다는 전설의 창입니다.", -32 }\t", 3200));
            Items.Add(new Item($"{"회복 포션", -10}\t", ItemType.Potion, 30, $"{ "체력을 +30만큼 회복시켜주는 포션입니다.", -32 }\t", 300));

            foreach (var item in Items)
            {
                if (item.Type == ItemType.Potion) NumberOfItems.TryAdd(item, 5);
                else NumberOfItems.TryAdd(item, 1);
            }
        }
    }