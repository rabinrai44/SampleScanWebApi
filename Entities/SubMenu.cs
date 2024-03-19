namespace SampleScanWebApi.Entities;

public class SubMenu : BaseEntity
{
    public required string Name { get; set; }
    public int MenuItemId { get; set; }
    public MenuItem? MenuItem { get; set; }
}