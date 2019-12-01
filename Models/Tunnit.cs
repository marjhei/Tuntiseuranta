using System;
using System.Collections.Generic;

namespace Tuntiseuranta.Models
{
    public partial class Tunnit
    {
        public int TuntiId { get; set; }
        public int KayttajaId { get; set; }
        public DateTime Paivamaara { get; set; }
        public decimal Tunnit1 { get; set; }
        public string Kuvaus { get; set; }
        public bool Laskutettava { get; set; }

        public virtual Kayttaja Kayttaja { get; set; }
    }
}
