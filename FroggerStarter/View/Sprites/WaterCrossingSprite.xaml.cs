
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using FroggerStarter.Model.DataObjects;

namespace FroggerStarter.View.Sprites
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WaterCrossingSprite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaterCrossingSprite"/> class.
        /// </summary>
        public WaterCrossingSprite()
        {
            this.InitializeComponent();
            this.waterCrossingCanvas.Height = GameSettings.LaneHeight;
            this.waterCrossingRectangle.Height = GameSettings.LaneHeight;
        }
    }
}
