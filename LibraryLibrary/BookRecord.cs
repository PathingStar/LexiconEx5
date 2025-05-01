using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLibrary
{
    public record BookRecord(string Titel, string Athor, int ISBN, string Category, bool Available=true)
    {
        public sealed override string ToString() => $"Titel: {Titel} Athor: {Athor} ISBN: {ISBN} Category: {Category} Available: {Available}";
    }
    
}
