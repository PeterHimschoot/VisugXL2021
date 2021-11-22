namespace U2U.Testing.Abstractions;

public class FormValues
{
  private readonly List<KeyValuePair<string?, string?>> variables = new List<KeyValuePair<string?, string?>>();

  public IEnumerable<KeyValuePair<string?, string?>> Values => this.variables;

  public FormValues Add(Expression<Func<object?, string>> expression)
  {
    KeyValuePair<string?, string?> kv = new FormPropertyBuilder().Infer(expression);
    return Add(kv);
  }

  public FormValues Add(KeyValuePair<string?, string?> kv)
  {
    this.variables.Add(kv);
    return this;
  }

  public FormValues Add(string? key, string? value)
  => Add(new KeyValuePair<string?, string?>(key, value));

  public static implicit operator FormUrlEncodedContent(FormValues formValues)
    => new FormUrlEncodedContent(formValues.Values);
}

