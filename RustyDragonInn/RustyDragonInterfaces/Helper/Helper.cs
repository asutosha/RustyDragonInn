using System;
using System.IO;
using RustyDragonBasesAndInterfaces.Exceptions;
using RustyDragonBasesAndInterfaces.Models;

namespace RustyDragonBasesAndInterfaces.Helper
{
    public static class Helper
    {
        public static CheeseTypes CheeseTypeMapper(string type)
        {
            CheeseTypes foundType;
            Enum.TryParse(type, true, out foundType); // I assumed that the type are found in the files are correct
            return foundType;
        }

        public static DateTime GetDateTime(char deliminator, string filePath, int index)
        {
            if (filePath == null || !File.Exists(filePath))
            {
                return DateTime.Now;
            }
            try
            {

                var dateString = Path.GetFileNameWithoutExtension(filePath).Split(deliminator)[index];
                var day = Convert.ToInt32(dateString.Substring(0, 2));
                var month = Convert.ToInt32(dateString.Substring(2, 2));
                var year = Convert.ToInt32(dateString.Substring(4, 4));
                return new DateTime(year, month, day);
            }
            catch
            {
                throw new DateTimeFormatException("WARNING :The date and time is in wrong formate. the correct format is DDMMYYYY. Please make sure that you named your file correctly.");
            }
        }
    }
}
