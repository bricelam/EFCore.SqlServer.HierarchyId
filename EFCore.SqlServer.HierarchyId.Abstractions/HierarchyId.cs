using System;
using Microsoft.SqlServer.Types;

namespace Bricelam.EntityFrameworkCore
{
    public class HierarchyId : IComparable
    {
        private SqlHierarchyId _value;

        private HierarchyId(SqlHierarchyId value)
        {
            if (value.IsNull)
                throw new ArgumentNullException(nameof(value));

            _value = value;
        }

        public static HierarchyId GetRoot()
            => new HierarchyId(SqlHierarchyId.GetRoot());

        public static HierarchyId Parse(string input)
            => Wrap(SqlHierarchyId.Parse(input));

        public int CompareTo(object obj)
            => _value.CompareTo(
                    obj is HierarchyId other
                        ? other._value
                        : obj);

        public override bool Equals(object obj)
            => _value.Equals(
                    obj is HierarchyId other
                        ? other._value
                        : obj);

        public HierarchyId GetAncestor(int n)
            => new HierarchyId(_value.GetAncestor(n));

        public HierarchyId GetDescendant(HierarchyId child1, HierarchyId child2)
            => Wrap(_value.GetDescendant(Unwrap(child1), Unwrap(child2)));

        public override int GetHashCode()
            => _value.GetHashCode();

        public short GetLevel()
            => _value.GetLevel().Value;

        public HierarchyId GetReparentedValue(HierarchyId oldRoot, HierarchyId newRoot)
            => new HierarchyId(_value.GetReparentedValue(Unwrap(oldRoot), Unwrap(newRoot)));

        public bool IsDescendantOf(HierarchyId parent)
        {
            if (parent == null)
                return false;

            return _value.IsDescendantOf(parent._value).Value;
        }

        public override string ToString()
            => _value.ToString();

        public static bool operator ==(HierarchyId hid1, HierarchyId hid2)
            => Unwrap(hid1).CompareTo(Unwrap(hid2)) == 0;

        public static bool operator !=(HierarchyId hid1, HierarchyId hid2)
            => Unwrap(hid1).CompareTo(Unwrap(hid2)) != 0;

        public static bool operator <(HierarchyId hid1, HierarchyId hid2)
            => Unwrap(hid1).CompareTo(Unwrap(hid2)) < 0;

        public static bool operator >(HierarchyId hid1, HierarchyId hid2)
            => Unwrap(hid1).CompareTo(Unwrap(hid2)) > 0;

        public static bool operator <=(HierarchyId hid1, HierarchyId hid2)
            => Unwrap(hid1).CompareTo(Unwrap(hid2)) <= 0;

        public static bool operator >=(HierarchyId hid1, HierarchyId hid2)
            => Unwrap(hid1).CompareTo(Unwrap(hid2)) >= 0;

        private static SqlHierarchyId Unwrap(HierarchyId value)
            => value?._value ?? SqlHierarchyId.Null;

        private static HierarchyId Wrap(SqlHierarchyId value)
            => value.IsNull ? null : new HierarchyId(value);
    }
}
