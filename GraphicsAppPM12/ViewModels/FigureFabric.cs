using System;
using System.Collections.Generic;
using System.Linq;
using Geometry;
using System.Composition;
using System.Composition.Hosting;



namespace GraphicsApp.ViewModels
{
    class FigureMetadata
    {
        public string Name { get; }
    }

    public static class FigureFabric
    {
        class ImportInfo
        {
            [ImportMany]
            public IEnumerable<Lazy<IShape, FigureMetadata>> AvailableFigures { get; set; } = [];
        }

        private static readonly ImportInfo info;
        static FigureFabric()
        {
            var assemblies = new[] { typeof(MyEllipse).Assembly };
            var conf = new ContainerConfiguration();
            try
            {
                conf = conf.WithAssemblies(assemblies);
            }
            catch (Exception)
            {
                // ignored
            }

            var cont = conf.CreateContainer();
            info = new ImportInfo();
            cont.SatisfyImports(info);
        }

        public static IEnumerable<string> AvailableFigures => info.AvailableFigures.Select(f => f.Metadata.Name);
        public static IShape CreateFigure(string FigureName)
        {
            return info.AvailableFigures.First(f => f.Metadata.Name == FigureName).Value;
        }
    }
}
