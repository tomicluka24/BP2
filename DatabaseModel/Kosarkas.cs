//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kosarkas
    {
        public int IdKosarkasa { get; set; }
        public string ImeKosarkasa { get; set; }
        public string PrezimeKosarkasa { get; set; }
        public int BrojDresaKosarkasa { get; set; }
        public int Klub_IdKluba { get; set; }
    
        public virtual Klub Klub { get; set; }
    }
}
