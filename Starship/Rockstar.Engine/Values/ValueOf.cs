namespace Rockstar.Engine.Values;

public abstract class ValueOf<T>(T value) : Value {
	public T Value => value;
	public override bool Equals(object? obj) => Equals(obj as ValueOf<T>);
	public override int GetHashCode() => this.Value.GetHashCode();
	public bool Equals(ValueOf<T>? that) => that != null && this.Value != null && this.Value.Equals(that.Value);
}