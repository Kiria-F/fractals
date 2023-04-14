using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Fractals {
    public struct PointD : IEquatable<PointD> {
        public static readonly PointD Empty;
        private double x; // Do not rename (binary serialization)
        private double y; // Do not rename (binary serialization)

        public PointD(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public PointD(Vector2 vector) {
            x = vector.X;
            y = vector.Y;
        }

        public Vector2 ToVector2() => new Vector2((float)x, (float)y);

        [Browsable(false)]
        public readonly bool IsEmpty => x == 0f && y == 0f;

        public double X {
            readonly get => x;
            set => x = value;
        }

        public double Y {
            readonly get => y;
            set => y = value;
        }

        public static explicit operator Vector2(PointD point) => point.ToVector2();

        public static explicit operator PointD(Vector2 vector) => new PointD(vector);

        public static PointD operator +(PointD pt, Size sz) => Add(pt, sz);

        public static PointD operator -(PointD pt, Size sz) => Subtract(pt, sz);

        public static PointD operator +(PointD pt, SizeF sz) => Add(pt, sz);

        public static PointD operator -(PointD pt, SizeF sz) => Subtract(pt, sz);

        public static bool operator ==(PointD left, PointD right) => left.X == right.X && left.Y == right.Y;

        public static bool operator !=(PointD left, PointD right) => !(left == right);

        public static PointD Add(PointD pt, Size sz) => new PointD(pt.X + sz.Width, pt.Y + sz.Height);

        public static PointD Subtract(PointD pt, Size sz) => new PointD(pt.X - sz.Width, pt.Y - sz.Height);

        public static PointD Add(PointD pt, SizeF sz) => new PointD(pt.X + sz.Width, pt.Y + sz.Height);

        public static PointD Subtract(PointD pt, SizeF sz) => new PointD(pt.X - sz.Width, pt.Y - sz.Height);

        public override readonly bool Equals([NotNullWhen(true)] object? obj) => obj is PointD && Equals((PointD)obj);

        public readonly bool Equals(PointD other) => this == other;

        public override readonly int GetHashCode() => HashCode.Combine(X.GetHashCode(), Y.GetHashCode());

        public override readonly string ToString() => $"{{X={x}, Y={y}}}";
    }
}
