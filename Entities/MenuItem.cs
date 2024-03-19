namespace SampleScanWebApi.Entities;

public class MenuItem : BaseEntity
{
    public required string Name { get; set; }
    public ICollection<SubMenu> SubMenus { get; set; } = [];
}
