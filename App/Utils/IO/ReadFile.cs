namespace App.Utils.IO
{
    public class ReadFile
    {
        /*
            Read's file and return a string that corresponds to the content of the file
        */
        public static async Task<string> RetrieveStringFromFile(string filename)
        {
            Char[] buffer;
            using(StreamReader stream_io = new StreamReader(filename))
            {
                buffer = new Char[stream_io.BaseStream.Length];
                await stream_io.ReadAsync(buffer,0,(int)stream_io.BaseStream.Length);
            }

            return new String(buffer);
        }
    }
}