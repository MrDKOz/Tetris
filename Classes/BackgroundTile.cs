using System.Drawing;
using System.Windows.Forms;

namespace Tetris.Classes
{
    public class BackgroundTile
    {
        public Color BackgroundColor { get; set; }
        public Padding Padding { get; set; }

        /// <summary>
        /// Fetches the default background tile.
        /// </summary>
        /// <returns>A default background tile.</returns>
        public Panel Default()
        {
            return new Panel
            {
                BackColor = Color.White,
                Margin = Padding.Empty
            };
        }
    }
}
