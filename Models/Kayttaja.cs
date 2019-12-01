using System;
using System.Collections.Generic;

namespace Tuntiseuranta.Models
{
    public partial class Kayttaja
    {
        public Kayttaja()
        {
            Tunnit = new HashSet<Tunnit>();
        }

        public int Id { get; set; }
        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public string Osasto { get; set; }
        public string Tehtavanimike { get; set; }

        public virtual ICollection<Tunnit> Tunnit { get; set; }
    }
}
