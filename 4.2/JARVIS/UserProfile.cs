using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.IO;

namespace JARVIS
{
    /// <summary>
    /// Classe que vai armazenar dados do usuário.
    /// </summary>
    [Serializable] // serializável
    public class UserProfile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Voice { get; set; }

        public void Save(string file)
        {
            BinaryFormatter binary = new BinaryFormatter(); // formato do arquivo

            FileStream stream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None);

            binary.Serialize(stream, this);
        }

        public UserProfile Load(string file)
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None);

            return (UserProfile)binary.Deserialize(stream);
        }
    }
}
