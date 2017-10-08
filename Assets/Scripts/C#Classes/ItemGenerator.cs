namespace Assets.Scripts
{
    public static class ItemGenerator
    {

        public static Item CreateSendItem()
        {
            return new Item()
            {
                Id = 1,
                ForId = 2
            };
        }

        public static int CreateWantItemId()
        {
            return 1;
        }
    }
}
