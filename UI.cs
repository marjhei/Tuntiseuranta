using System;
using System.Collections.Generic;
using System.Text;
using Tuntiseuranta.Models;
using System.Linq;

namespace Tuntiseuranta
{
    class UI
    {
        Kayttaja k = new Kayttaja();

        public void LisaaKayttaja()
        {
            TuntiseurantaContext ts = new TuntiseurantaContext();

            Console.WriteLine("Etunimi:");
            string etunimi = Console.ReadLine();
            Console.WriteLine("Sukunimi:");
            string sukunimi = Console.ReadLine();
            Console.WriteLine("Osasto:");
            string osasto = Console.ReadLine();
            Console.WriteLine("Tehtävänimike:");
            string tehtavanimike = Console.ReadLine();

            Kayttaja uusi = new Kayttaja();
            {
                uusi.Id = k.Id;
                uusi.Etunimi = etunimi;
                uusi.Sukunimi = sukunimi;
                uusi.Osasto = osasto;
                uusi.Tehtavanimike = tehtavanimike;
            }

            k = uusi;

            ts.Kayttaja.Add(uusi);

            ts.SaveChanges();
        }

        public void KirjauduSisään()
        {
            TuntiseurantaContext ts = new TuntiseurantaContext();

            Console.WriteLine("Syötä käyttäjän id:");
            var syöte = int.Parse(Console.ReadLine().Trim());

            var käyttäjä = from k in ts.Kayttaja
                           where k.Id == syöte
                           select k;


            k = käyttäjä.First();
        }

        public void LisaaTunnit()
        {
            TuntiseurantaContext db = new TuntiseurantaContext();

            Console.WriteLine("Syötä päivämäärä:");
            string paivamaara = Console.ReadLine();
            DateTime pvm = DateTime.Parse(paivamaara);

            Console.WriteLine("Syötä tunnit:");
            decimal tunnit = decimal.Parse(Console.ReadLine());
            

            Console.WriteLine("Syötä kuvaus:");
            string kuvaus = Console.ReadLine();

            Console.WriteLine("Laskutettava:");
            string laskutettava = Console.ReadLine();
            bool laskutus = Boolean.Parse(laskutettava);


            Tunnit uusi = new Tunnit();
                {


                uusi.KayttajaId = k.Id;
                uusi.Paivamaara = pvm;
                uusi.Tunnit1 = tunnit;
                uusi.Kuvaus = kuvaus;
                uusi.Laskutettava = laskutus;

                }; 
            
            
            
            db.Tunnit.Add(uusi); 
            db.SaveChanges();



        }

        public void Raportointi()
        {
            Console.WriteLine("RAPORTOINTI");
            DateTime aloitusPvm;
            DateTime lopetusPvm;
            Console.WriteLine("Syötä aloituspäivämäärä muodossa PP.KK.VVVV");
            while (!DateTime.TryParse(Console.ReadLine(), out aloitusPvm))
            {
                Console.WriteLine("Jotain meni vikaan. Syötä päivämäärä uudelleen.");
            }
            Console.WriteLine("Syötä loppupäivämäärä muodossa PP.KK.VVVV");
            while (!DateTime.TryParse(Console.ReadLine(), out lopetusPvm))
            {
                Console.WriteLine("Jotain meni vikaan. Syötä päivämäärä uudelleen.");
            }
            Console.WriteLine("Syötä käyttäjä-ID, jos haluat katsella tietyn henkilön tunteja. Muuten paina ENTER.");
            var käyttäjä = Console.ReadLine();
            if (string.IsNullOrEmpty(käyttäjä))
            {
                var ts = new TuntiseurantaContext();

                var kaikkienTunnit = from t in ts.Tunnit
                                     where t.Paivamaara > aloitusPvm && t.Paivamaara < lopetusPvm
                                     select t.Tunnit1;

                Console.WriteLine($"Kaikkien tunnit aikavälillä: {kaikkienTunnit.Sum()}");

                var kaikkienLaskutettavat = from t in ts.Tunnit
                                            where t.Paivamaara > aloitusPvm && t.Paivamaara < lopetusPvm && t.Laskutettava
                                            select t.Tunnit1;

                Console.WriteLine($"Kaikkien laskutettavat tunnit aikavälillä: {kaikkienLaskutettavat.Sum()}");
            }
            else
            {
                var ts = new TuntiseurantaContext();
                var id = int.Parse(käyttäjä);

                var käyttäjänTunnit = from t in ts.Tunnit
                                      where t.Paivamaara > aloitusPvm && t.Paivamaara < lopetusPvm && t.KayttajaId == id
                                      select t.Tunnit1;

                Console.WriteLine($"Käyttäjän tunnit aikavälillä: {käyttäjänTunnit.Sum()}");

                var käyttäjänLaskutettavat = from t in ts.Tunnit
                                             where t.Paivamaara > aloitusPvm && t.Paivamaara < lopetusPvm && t.KayttajaId == id && t.Laskutettava
                                             select t.Tunnit1;

                Console.WriteLine($"Käyttäjän laskutettavat tunnit aikavälillä: {käyttäjänLaskutettavat.Sum()}");


            }
        }

    }
}
