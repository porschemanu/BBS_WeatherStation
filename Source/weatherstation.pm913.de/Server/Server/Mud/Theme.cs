using MudBlazor;

namespace Server.Mud
{
    public class Theme
    {
        public static MudTheme BBSTheme { get; set; } = new MudTheme()
        {
            Palette = new Palette()
            {
                AppbarBackground = "#ebf1dc",
                AppbarText = Colors.Shades.Black,
                Primary = "#a3c54d",
                PrimaryContrastText = Colors.Shades.White,
                DrawerBackground = "#a3c54d",
                DrawerText = Colors.Shades.White,
                ActionDefault = Colors.Shades.Black,
            },
            PaletteDark = new Palette()
            {
                Primary = Colors.Blue.Lighten1
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "250px"
            }
        };
    }
}
