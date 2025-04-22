namespace NotepadKnights;

public class Store
    {
        public List<Item> Items { get; private set; }
        public Dictionary<Item, int> NumberOfItems { get; private set; }

        public Store()
        {
            Items = new List<Item>();
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
                    ItemType.Attack => "공격력",
                    ItemType.Defense => "방어력",
                    ItemType.Potion => "회복력",
                    _ => "?",
                };
                string goldText = $"{item.Price + "G", -7}";
                string countText = NumberOfItems[item] == 0 ? "Sold Out" : $"수량: {NumberOfItems[item]}"; 
                Console.WriteLine($"- {indexText} {item.Name} | {typeText} {"+" + item.Point, -3} | {item.State} | {goldText} | {countText}");
            }
        }

        private void InitialItems()
        {
            Items.Add(new Item($"{ "수련자 갑옷", -10 }", ItemType.Defense, 5, $"{ "수련에 도움을 주는 갑옷입니다.", -30 }", 1000));
            Items.Add(new Item($"{ "무쇠 갑옷", -10 }", ItemType.Defense, 9, $"{ "무쇠로 만들어져 튼튼한 갑옷입니다.", -30 }", 2200));
            Items.Add(new Item($"{ "스파르타 갑옷", -10 }", ItemType.Defense, 15, $"{ "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", -30 }", 3500));
            Items.Add(new Item($"{ "낡은 검", -10 }", ItemType.Attack, 2, $"{ "쉽게 볼 수 있는 낡은 검 입니다.", -30 }", 600));
            Items.Add(new Item($"{ "청동 도끼", -10 }", ItemType.Attack, 5, $"{ "어디선가 사용됐던거 같은 도끼입니다.", -30 }", 1500));
            Items.Add(new Item($"{ "스파르타의 창", -10 }", ItemType.Attack, 7, $"{ "스파르타의 전사들이 사용했다는 전설의 창입니다.", -30 }", 3200));

            foreach (var item in Items)
            {
                NumberOfItems.TryAdd(item, 1);
            }
        }
    }