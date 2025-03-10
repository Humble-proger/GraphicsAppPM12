using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform;
using Avalonia.Media.Imaging;
using Avalonia.Media.Imaging;

namespace Geometry
{
    class ModelMetadata
    {
        public required string Name { get; init; }  
        public required string LogoButton { get; init; }
        public Bitmap? Logo
        {
            get => LoadBitmap($"avares://GraphicsApp/Views/pictures/ToolBars/{LogoButton}");
        }

        private Bitmap LoadBitmap(string assetPath)
        {
            try
            {
                var uri = new Uri(assetPath);
                var asset = AssetLoader.Open(uri);
                return new Bitmap(asset);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки {assetPath}: {ex.Message}");
                return null;
            }
        }
    }
}
