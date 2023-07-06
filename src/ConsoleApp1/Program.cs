
// See https://aka.ms/new-console-template for more information
using System.Xml.Linq;

Console.WriteLine("Hello, World!");

string fileName = @"C:\Users\Lucas\AppData\Roaming\Microsoft Flight Simulator\VFR Araras (SDAA) to Sao Paulo Catarina (SBJH).pln";
string fileNameRes = @"C:\Users\Lucas\AppData\Roaming\Microsoft Flight Simulator\VFR Araras (SDAA) to Sao Paulo Catarina (SBJH) ALTERADO.pln";

XDocument doc = XDocument.Load(fileName);

// verifica se o documento foi gerado pelo little navmap
var valor = doc.Root.Element("FlightPlan.FlightPlan").Element("Descr").Value;

if (valor.ToLower().Contains("little navmap"))
    Console.WriteLine("Plano de voo gerado pelo little navmap");

Console.WriteLine(valor);
Console.ReadKey();

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

Console.WriteLine("Arquivo alterado com sucesso! Pressione qualquer tecla para continuar");

Console.ReadKey();


