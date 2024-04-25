using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using MonoGame.Extended.Serialization;
using Microsoft.Xna.Framework.Content;
using System.IO;
using StarScape.Source.World;

namespace StarScape.Source.JSON
{
    public static class JsonHandler
    {

        public static readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            WriteIndented = true,
            AllowTrailingCommas = true,
            IncludeFields = true
        };

        private static string ReadFile(string name)
        {
            string path = Path.Combine(LoadHelper.Content.RootDirectory, "data", name).ToString();

            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public static void LoadData(string name)
        {
            string file = ReadFile(name);

            using (JsonDocument doc = JsonDocument.Parse(file))
            {
                JsonElement root = doc.RootElement;
                foreach (var item in root.EnumerateArray())
                {
                    Type obj_class = Type.GetType("StarScape.Source.Types." + item.GetProperty("class").GetString());
                    string obj_id = item.GetProperty("id").GetString();

                    GameObject obj = (GameObject)JsonSerializer.Deserialize(item, obj_class);

                    GameObjects.RegisterGameObject(obj_id, obj);
				}
            }

        }

    }


}
