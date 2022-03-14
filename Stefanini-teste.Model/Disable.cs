using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.Model
{
    public class Disable : IEquatable<Disable>
    {
        public long Id { get; set; }
        public bool IsDisabled { get; set; }

        public bool Equals(Disable? other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals (null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Disable)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
