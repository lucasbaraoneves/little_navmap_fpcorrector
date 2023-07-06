using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LittleNavMap_Fixer
{
    public static class PlnFixer
    {
        public static bool FixFlightPlan(string fileName, bool overrideOriginal = false)
        {
            var fileNameRes = ComposeFileName(fileName, overrideOriginal);

            XDocument doc = XDocument.Load(fileName);

            foreach (XElement item in doc.Descendants("ATCWaypoint"))
            {
                var waypointName = item.Attribute("id").Value;
                Console.WriteLine(waypointName);

                var icao = item.Element("ICAO");

                if (icao != null)
                {
                    var icaoident = icao.Elements("ICAOIdent");

                    try
                    {
                        Console.WriteLine(icaoident.ElementAt(0).Value);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    item.Add(new XElement("ICAO"));
                    item.Element("ICAO").Add(new XElement("ICAOIdent"));
                    item.Element("ICAO").Element("ICAOIdent").Value = waypointName;
                    Console.WriteLine(item.Element("ICAO").Element("ICAOIdent").Value);
                }
            }

            doc.Save(fileNameRes);

            return true;
        }

        private static string ComposeFileName(string filename, bool overrideOriginal)
        {
            if (overrideOriginal)
                return filename;
            else
            {
                var extractedPath = Path.GetDirectoryName(filename);
                var extractedFileName = Path.GetFileNameWithoutExtension(filename);

                if (!Directory.Exists(Path.Combine(extractedPath, "Fixed")))
                    Directory.CreateDirectory(Path.Combine(extractedPath, "Fixed"));

                var newFileName = Path.Combine(extractedPath, "Fixed", extractedFileName + " FIXED.pln");

                return newFileName;
            }
        }
    }
}
