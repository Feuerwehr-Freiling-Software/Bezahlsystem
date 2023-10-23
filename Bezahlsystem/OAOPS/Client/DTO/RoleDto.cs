namespace OAOPS.Client.DTO
{
    public class RoleDto
    {
        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }
    }
}
