namespace Server
{
    public class Fish
    {
        public bool IsHungry { get; set; }
        public bool IsCaught { get; set; }
        public int Id { get; set; }
        public Fish(bool isHungry, bool isCaught, int id)
        {
            IsHungry = isHungry;
            IsCaught = isCaught;
            Id = id;
        }

        public bool GetHungry()
        {
            return IsHungry;
        }

        public bool GetCaught()
        {
            return IsCaught;
        }
        public int GetId()
        {
            return Id;
        }

        public void SetHungry(bool newHunger)
        {
            IsHungry = newHunger;
        }

        public void SetCaught(bool newCaught)
        {
            IsCaught = newCaught;
        }
    }
}
