using System.Text.Json;

using Geometry;

namespace VecEditor.IO;

public class GeometryJsonSerializer<T>
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new ColorConverter() },
        WriteIndented = true,
    };

    public void SaveJson(string filename, IEnumerable<T> figures)
    {
        File.WriteAllText(filename, JsonSerializer.Serialize(figures, _jsonSerializerOptions));
    }

    public IEnumerable<T>? LoadJson(string filename)
    {
        if (!File.Exists(filename))
            return null;
        return JsonSerializer.Deserialize<IEnumerable<T>>(File.ReadAllText(filename), _jsonSerializerOptions);
    }
}