using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using FroggerStarter.Model;
using FroggerStarter.Model.DataObjects;

namespace FroggerStarter.Utility
{
    public static class FileIOSerialization
    {
        public const string FilenameBinary = "HighScoreBoard.bin";

        public static async void BinarySerializer(HighScore score)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var fileToWrite =
                await storageFolder.CreateFileAsync(FilenameBinary,
                    CreationCollisionOption.OpenIfExists);

            var writer = new BinaryWriter(await fileToWrite.OpenStreamForWriteAsync());

            writer.Seek(0, SeekOrigin.End);
            writer.Write(score.Name);
            writer.Write(score.GameScore);
            writer.Write(score.GameLevel);

            writer.Flush();
            writer.Dispose();
        }

        public static async void BinaryDeserializer(HighScoreBoard board)
        {
            var folder = ApplicationData.Current.LocalFolder;
            StorageFile file;

            try
            {
                file = await folder.GetFileAsync(FilenameBinary);
            }
            catch (IOException)
            {
                await folder.CreateFileAsync(FilenameBinary);
                return;
            }

            var reader = new BinaryReader(await file.OpenStreamForReadAsync());

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var val1 = reader.ReadString();
                var val2 = reader.ReadInt32();
                var val3 = reader.ReadInt32();

                var highScore = new HighScore(val1, val2, val3);
                board.Add(highScore);
            }

            reader.Dispose();
        }

        public static async void BinaryFileClear()
        {
            var folder = ApplicationData.Current.LocalFolder;

            await folder.CreateFileAsync(FilenameBinary,
                    CreationCollisionOption.ReplaceExisting);
        }
    }
}
