using System;
using System.IO;
using Windows.Storage;
using FroggerStarter.Model;
using FroggerStarter.Model.DataObjects;

namespace FroggerStarter.Utility
{
    /// <summary>Handles reading/writing HighScore data to a binary file </summary>
    public static class FileIoSerialization
    {
        #region Data members

        /// <summary>The filename binary</summary>
        public const string FilenameBinary = "HighScoreBoard.bin";

        #endregion

        #region Methods

        /// <summary>
        ///     Binaries the serializer.
        ///     Precondition: none
        ///     Postcondition: writes the new score to the binary file.
        /// </summary>
        /// <param name="score">The score.</param>
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

        /// <summary>
        ///     Binaries the deserializer.
        ///     Precondition: none
        ///     Postcondition: reads the binary file data into the scoreboard.
        /// </summary>
        /// <param name="board">The board.</param>
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

        /// <summary>
        ///     Creates a new empty binary file.
        ///     Precondition: none
        ///     Postcondition: a new binary file is created.
        /// </summary>
        public static async void BinaryFileOverwrite()
        {
            var folder = ApplicationData.Current.LocalFolder;

            await folder.CreateFileAsync(FilenameBinary,
                CreationCollisionOption.ReplaceExisting);
        }

        #endregion
    }
}