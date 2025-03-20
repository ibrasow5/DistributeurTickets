namespace Tickets;

class DistributeurTickets
{
    static string filePath = "/tmp/fnumero.txt";
    static Dictionary<string, int> numeros = new Dictionary<string, int>
    {
        {"V", 0}, // Versement
        {"R", 0}, // Retrait
        {"I", 0}  // Informations
    };
    
    static List<string> clients = new List<string>();

    static void Main()
    {
        ChargerNumeros();

        bool continuer = true;
        while (continuer)
        {
            Console.WriteLine("Bienvenue ! Quel type d'opération souhaitez-vous effectuer ?");
            Console.WriteLine("1 - Versement");
            Console.WriteLine("2 - Retrait");
            Console.WriteLine("3 - Informations");
            Console.Write("Votre choix : ");
            string choix = Console.ReadLine();

            string type = choix switch
            {
                "1" => "V",
                "2" => "R",
                "3" => "I",
                _ => null
            };

            if (type == null)
            {
                Console.WriteLine("Choix invalide. Veuillez recommencer.");
                continue;
            }

            Console.Write("Entrez votre numéro de compte : ");
            string numeroCompte = Console.ReadLine();
            Console.Write("Entrez votre prénom : ");
            string prenom = Console.ReadLine();
            Console.Write("Entrez votre nom : ");
            string nom = Console.ReadLine();

            numeros[type]++;
            string ticket = $"{type}-{numeros[type]}";
            clients.Add($"{prenom} {nom} ({numeroCompte}) - Ticket: {ticket}");

            int enAttente = numeros[type] - 1;
            Console.WriteLine($"\n🔹 Votre numéro est {ticket}. Il y a {enAttente} personnes avant vous. 🔹\n");

            SauvegarderNumeros();

            Console.Write("Souhaitez-vous prendre un autre numéro ? (o/n) : ");
            continuer = Console.ReadLine()?.ToLower() == "o";
        }

        Console.WriteLine("\nListe des clients servis :");
        foreach (var client in clients)
        {
            Console.WriteLine(client);
        }
    }

    static void ChargerNumeros()
    {
        if (File.Exists(filePath))
        {
            var lignes = File.ReadAllLines(filePath);
            foreach (var ligne in lignes)
            {
                var parts = ligne.Split(':');
                if (parts.Length == 2 && numeros.ContainsKey(parts[0]))
                {
                    numeros[parts[0]] = int.Parse(parts[1]);
                }
            }
        }
    }

    static void SauvegarderNumeros()
    {
        List<string> lignes = new List<string>();
        foreach (var kvp in numeros)
        {
            lignes.Add($"{kvp.Key}:{kvp.Value}");
        }
        File.WriteAllLines(filePath, lignes);
    }
}