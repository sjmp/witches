namespace Assets.Scripts
{
    public static class ItemGenerator
    {

        public static ItemScript CreateItem()
        {
            return new ItemScript()
            {
                Id = 1,
                Name = "Eye of Newt",
                For = "Placeholder"
            };
        }
    }
}
