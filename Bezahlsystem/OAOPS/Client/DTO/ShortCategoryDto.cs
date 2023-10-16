namespace OAOPS.Client.DTO
{
    public class ShortCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
